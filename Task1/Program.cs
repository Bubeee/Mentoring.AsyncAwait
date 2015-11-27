using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;

            Console.WriteLine("Enter the natural number N");
            int.TryParse(Console.ReadLine(), out n);
            var cancellationTokenSource = new CancellationTokenSource();

            do
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource = new CancellationTokenSource();
                var result = Task.Run(() => SumAsync(n, cancellationTokenSource.Token));
                result.ContinueWith(
                    task => Console.WriteLine("The result is: {0}", task.Result),
                    TaskContinuationOptions.NotOnCanceled);

                Console.WriteLine("Enter the natural number N");
            }
            while (int.TryParse(Console.ReadLine(), out n));

            Console.WriteLine("The end");
            Console.ReadLine();
        }

        private static int SumAsync(int n, CancellationToken ct)
        {
            var sum = 0;

            for (var i = 0; i <= n; i++)
            {
                sum += i;
            }

            Thread.Sleep(3000);
            ct.ThrowIfCancellationRequested();
            return sum;
        }
    }
}