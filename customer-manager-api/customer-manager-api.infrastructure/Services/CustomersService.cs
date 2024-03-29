﻿using customer_management.Requests;
using customer_manager_api.application.Services;
using customer_manager_api.domain;
using customer_manager_api.domain.Constants;
using customer_manager_api.domain.Models;
using customer_manager_api.domain.Repositories;
using customer_manager_api.domain.Responses;
using Microsoft.Extensions.Caching.Memory;
using static customer_management.Requests.CreateCustomerRequest;

namespace customer_manager_api.infrastructure.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMemoryCache _cache;
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1); //due to race condition of the consumer, only allow 1 thread, to avoid PK error
        public CustomersService(ICustomerRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<ApiResponse> CreateCustomersAsync(IEnumerable<CreateCustomerRequest> customers)
        {
            try
            {
                await semaphore.WaitAsync();
                var customersToCreate = new List<Customer>();
                var errors = await ValidateAndPopulateCustomersAsync(customers, customersToCreate);
                var customerArray = customersToCreate.ToArray();

                if (customerArray.Any())
                {
                    SynchronizeCache(customerArray);
                    await _repository.AddRangeAsync(customerArray); //TODO: add at a queue and create a backgroundhosted service to read from that queue
                }

                return new ApiResponse
                {
                    Errors = errors,
                    Success = customersToCreate.Any(),
                    Message = errors.Any() ? WarningMessages.RequestHasErrors : SuccessMessages.RequestSucceeded
                };
            }
            catch
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = ErrorMessages.ServerError
                };
            }
            finally
            {
                semaphore.Release();
            }
        }

        private async Task<Dictionary<string, string[]>> ValidateAndPopulateCustomersAsync(IEnumerable<CreateCustomerRequest> customers, List<Customer> customersToCreate)
        {
            var validator = new CreateCustomerRequestValidator();
            var customerIds = customers.Select(c => c.Id).ToList();
            var customersWithIdsOnDatabase = await _repository.GetAllAsync(x => customerIds.Contains(x.Id));
            var errors = new Dictionary<string, string[]>();
            var count = 0;

            foreach (var c in customers)
            {
                var validation = await validator.ValidateAsync(c);
                if (!validation.IsValid)
                {

                    errors.Add(count.ToString(), validation.ToDictionary().Values.AsEnumerable().SelectMany((string[] x) => x).ToArray());
                    count++;
                    continue;
                }

                if (customersWithIdsOnDatabase.Any(x => x.Id == c.Id))
                {
                    errors.Add(count.ToString(), new string[] { ValidationMessages.IdMustBeUnique });
                    count++;
                    continue;
                }

                customersToCreate.Add((Customer)c);
                count++;
            }

            return errors;
        }

        private void HeapSortByName(Customer[] customers)
        {
            Customer aux;
            int i;
            var lastPosition = customers.Length - 1;
            for (i = (lastPosition / 2); i >= 0; i--)
            {
                CreateHeap(customers, i, lastPosition - 1);
            }

            for (i = lastPosition; i >= 1; i--)
            {
                aux = customers[0];
                customers[0] = customers[i];
                customers[i] = aux;
                CreateHeap(customers, 0, i - 1);
            }
        }

        private void CreateHeap(Customer[] customers, int start, int end)
        {
            var nodeRoot = customers[start];
            var nodeRootFullName = $"{nodeRoot.FirstName}{nodeRoot.LastName}";
            var j = start * 2 + 1;

            while (j <= end)
            {
                var currentFullName = $"{customers[j].FirstName}{customers[j].LastName}";
                var nextFullName = $"{customers[j + 1].FirstName}{customers[j + 1].LastName}";

                if (j < end)
                {
                    var nextNamePrecedsCurrent = currentFullName.CompareTo(nextFullName) < 0;

                    if (nextNamePrecedsCurrent)
                        j++;
                }

                var rootNodeNameSmallerThanNextNodeName = nodeRootFullName.CompareTo($"{customers[j].FirstName}{customers[j].LastName}") < 0;

                if (rootNodeNameSmallerThanNextNodeName)
                {
                    //if the child node is bigger than the aux node, swap them
                    customers[start] = customers[j];
                    start = j;
                    //move to the next child node
                    j = 2 * start + 1;
                }
                else
                {
                    j = end + 1;
                }
            }

            customers[start] = nodeRoot;
        }

        private void SynchronizeCache(Customer[] customers)
        {
            var existingCustomers = GetCachedCustomers();

            if (existingCustomers is null)
            {
                HeapSortByName(customers);
                _cache.Set(CacheKeys.Customers, customers);
                return;
            }

            var updatedArray = existingCustomers.Concat(customers).ToArray();
            HeapSortByName(updatedArray);

            _cache.Set(CacheKeys.Customers, updatedArray);
        }

        private Customer[] GetCachedCustomers()
        {
            return _cache.Get<Customer[]>(CacheKeys.Customers);
        }
        public async Task<DataResponse<Customer[]>> GetCustomersAsync()
        {
            var cachedCustomers = GetCachedCustomers();

            var noCachedCustomers = cachedCustomers is null || !cachedCustomers.Any();

            if (noCachedCustomers)
            {
                var customers = (await _repository.GetAllAsync()).ToArray();
                HeapSortByName(customers);
                _cache.Set(CacheKeys.Customers, customers);
                return new DataResponse<Customer[]>(customers);
            }

            return new DataResponse<Customer[]>(cachedCustomers);
        }
    }
}
