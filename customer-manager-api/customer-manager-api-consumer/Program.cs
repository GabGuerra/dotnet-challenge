using customer_management.Requests;
using customer_manager_api_consumer;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;

var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.ConnectionClose = false;
ServicePointManager.DefaultConnectionLimit = 100;

var createCustomersCommand = "1";
var getCustomersCommand = "2";
var startId = 0;
var apiUrl = "http://localhost:5000/customers";
Console.WriteLine("Welcome to the customer-manager-api Consumer");
Console.WriteLine("Please enter a command:");
Console.WriteLine("1. Create Customers in parallel");
Console.WriteLine("2. Get Customers in parallel");

var command = Console.ReadLine();
var availableOptions = new[] { createCustomersCommand, getCustomersCommand };

while (!availableOptions.Contains(command))
{
    Console.WriteLine("Invalid command. Please enter a valid command:");
    Console.WriteLine("1. Create Customers in parallel");
    Console.WriteLine("2. Get Customers in parallel");
    command = Console.ReadLine();
}

if (command == createCustomersCommand)
{
    Console.WriteLine("Randomly creating customers...");
    var stopwatch = new Stopwatch();
    stopwatch.Start();

    var tasks = new List<Task>();

    for (int i = startId; i < startId + 3000; i++)
    {
        var customers = new CreateCustomerRequest[]
        {
            CreateCustomerGenerator.Generate(i),
            CreateCustomerGenerator.Generate(i + 1)
        };

        tasks.Add(httpClient.PostAsync(apiUrl, new StringContent(JsonSerializer.Serialize(customers), Encoding.UTF8, "application/json")));
    }

    await Task.WhenAll(tasks);
    stopwatch.Stop();
    ThroughputCalculator.Calculate(tasks.Count, stopwatch.ElapsedMilliseconds);
}

if (command == getCustomersCommand)
{
    Console.WriteLine("Getting customers...");
    var stopwatch = new Stopwatch();
    stopwatch.Start();

    var tasks = Enumerable.Range(0, 1000).Select((x) => httpClient.GetAsync(apiUrl));

    var results = await Task.WhenAll(tasks);
    stopwatch.Stop();
    ThroughputCalculator.Calculate(tasks.Count(), stopwatch.ElapsedMilliseconds);    
}