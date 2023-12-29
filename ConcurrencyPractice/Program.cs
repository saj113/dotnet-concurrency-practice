// See https://aka.ms/new-console-template for more information

using ConcurrencyPractice;
using ConcurrencyPractice.MonitorBlockingQueue;
using ConcurrencyPractice.MutexBlockingQueue;
using ConcurrencyPractice.TokenBucketFilter;

var testRunner = new Dictionary<string, ITestRunner>
{
    { "1", new MutexBlockingQueueTestRunner() },
    { "2", new MonitorBlockingQueueTestRunner() },
    { "3", new TokenBucketFilterTestRunner() }
};

Console.WriteLine("Choose a test to run:");
foreach (var (key, value) in testRunner)
{
    Console.WriteLine($"{key} - {value.Name}");
}

var input = Console.ReadLine();
if (testRunner.TryGetValue(input, out var test))
{
    test.Run();
}
else
{
    Console.WriteLine("Invalid input");
}