namespace customer_manager_api_consumer
{
    public static class ThroughputCalculator
    {
        public static void Calculate(int totalRequests, long elapsedMilliseconds)
        {
            var seconds = elapsedMilliseconds / 1000;
            var throughPutPerSecond = totalRequests / seconds;
            Console.WriteLine($"Time elapsed: {elapsedMilliseconds}ms | Throughput {throughPutPerSecond}/sec");

            var estimatedDailyThrouhput = throughPutPerSecond * 60 * 60 * 24;
            Console.WriteLine($"This endpoint could handle {estimatedDailyThrouhput}  requests per day");
        }
    }
}
