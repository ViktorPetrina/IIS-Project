using System;
using System.Threading.Tasks;

namespace SoapClientTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new SearchService.SearchServiceClient();

            Console.Write("Enter search keyword: ");
            var query = Console.ReadLine();

            try
            {
                var results = await client.SearchAsync(query);  

                Console.WriteLine($"\nResults for \"{query}\":");
                foreach (var spec in results)
                {
                    Console.WriteLine($"- Model: {spec.phoneDetails.brandValue}, {spec.phoneDetails.modelValue}, {spec.phoneDetails.yearValue}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            await client.CloseAsync();
        }
    }
}