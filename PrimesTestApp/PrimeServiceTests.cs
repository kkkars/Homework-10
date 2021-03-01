using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrimesTestApp
{
    static class PrimeServiceTests
    {
        public static async Task StartPointTest(HttpClient client)
        {
            Console.WriteLine("Test: StartPointTest\n");

            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, client.BaseAddress));
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"  Request: {{/}}\n  Expected-> Status code: {{200}}\n  Actual-> Status code: {{{(int)response.StatusCode}}}\n  Success: {(int)response.StatusCode == 200}");
            Console.WriteLine($"  Context: {content}");

            Console.WriteLine();
        }

        public static async Task IsPrimeTest(HttpClient client)
        {
            Console.WriteLine("Test: IsPrimeTest\n");

            Console.WriteLine("[IsPrimeNumberCase]:\n");
            await PrimeNumberCase(client, "primes/5", 200);

            Console.WriteLine("[IsNotPrimeNumberCase]:\n");
            await PrimeNumberCase(client, "primes/0", 404);
        }

        public static async Task GetPrimesInRange(HttpClient client)
        {
            Console.WriteLine("Test: GetPrimesInRangeTest\n");

            await RangeWithResultCase(client);
            await RangeWithEmptyResultCase(client);
            await WrongRangeCase(client);
        }

        private static async Task PrimeNumberCase(HttpClient client, string requestUrl, int expectedCode)
        {
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{client.BaseAddress}{requestUrl}"));

            Console.WriteLine($"  Request: {{/{requestUrl}}}\n  Expected-> Status code: {{{expectedCode}}}\n  Actual-> Status code: {{{(int)response.StatusCode}}}\n  Success: {(int)response.StatusCode == expectedCode}");
            Console.WriteLine();
        }

        private static async Task RangeWithResultCase(HttpClient client)
        {
            Console.WriteLine("[RangeWithResultCase]:\n");

            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{client.BaseAddress}primes?from=10&to=50"));
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"  Request: {{/primes/?from=10&to=50}}\n  Expected-> Status code: {{200}}\n  Actual-> Status code: {{{(int)response.StatusCode}}}\n  Success: {(int)response.StatusCode == 200}");
            Console.WriteLine($"  Context: {content}");
            Console.WriteLine();
        }

        private static async Task RangeWithEmptyResultCase(HttpClient client)
        {
            Console.WriteLine("[RangeWithEmptyResultCase]:\n");

            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{client.BaseAddress}primes?from=-10&to=1"));
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"  Request: {{/primes?from=-10&to=1}}\n  Expected-> Status code: {{200}}\n             IsEmpty: True\n  Actual-> Status code: {{{(int)response.StatusCode}}}\n           IsEmpty: {content.Length == 2}\n  Success: {content.Length == 2}");
            Console.WriteLine($"  Context: {content}");
            Console.WriteLine();
        }

        private static async Task WrongRangeCase(HttpClient client)
        {
            Console.WriteLine("[WrongRangeCase]:\n");

            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{client.BaseAddress}primes?to=abcd"));

            Console.WriteLine($"  Request: {{/primes?to=abcd}}\n  Expected-> Status code: {{400}}\n  Actual-> Status code: {{{(int)response.StatusCode}}}\n  Success: {(int)response.StatusCode == 400}");
            Console.WriteLine();
        }
    }
}