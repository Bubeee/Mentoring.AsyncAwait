using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the url to download, e - to exit");
            var url = Console.ReadLine();

            var cancellationTokenSource = new CancellationTokenSource();

            do
            {
                if (url.ToLower() == "c")
                {
                    cancellationTokenSource.Cancel();
                    cancellationTokenSource = new CancellationTokenSource();
                }
                
                Task.Run(() => DownloadAsync(url, cancellationTokenSource.Token), cancellationTokenSource.Token);

                Console.WriteLine("Enter the url to download or c - to cancel all current downloads, e - to exit");
                url = Console.ReadLine();
            }
            while (url.ToLower() != "e");

            Console.WriteLine("The end");
            Console.ReadLine();
        }

        private static async Task DownloadAsync(string url, CancellationToken ct)
        {
            var uri = new Uri(url);
            using (var client = new WebClient())
            {
                var result = await client.DownloadStringTaskAsync(uri);
                Thread.Sleep(5000);
                ct.ThrowIfCancellationRequested();
                Console.WriteLine(result);
                Console.WriteLine("Enter the url to download or c - to cancel all current downloads, e - to exit");
            }
        }
    }
}
