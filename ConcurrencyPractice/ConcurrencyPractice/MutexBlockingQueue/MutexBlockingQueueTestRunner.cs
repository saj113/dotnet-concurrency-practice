using ConcurrencyPractice.Extensions;

namespace ConcurrencyPractice.MutexBlockingQueue;

public class MutexBlockingQueueTestRunner : ITestRunner
{
    private readonly MutexBlockingQueue<int> _blockingQueue = new(1);

    public string Name => "Mutex Blocking Queue";
    public void Run()
    {
        var producerThreadStart = new ThreadStart(this.ProducerThread);
        var producer = producerThreadStart.CreateBackgroundThread();
        producer.Start();

        var producer2 = producerThreadStart.CreateBackgroundThread();
        producer2.Start();

        var producer3 = producerThreadStart.CreateBackgroundThread();
        producer3.Start();

        var consumerThreadStart = new ThreadStart(this.ConsumerThread);
        var consumer = consumerThreadStart.CreateBackgroundThread();
        consumer.Start();

        var consumer2 = consumerThreadStart.CreateBackgroundThread();
        consumer2.Start();

        var consumer3 = consumerThreadStart.CreateBackgroundThread();
        consumer3.Start();


        // Run simulation for 1 second
        Thread.Sleep(1000);
    }

    private void ProducerThread()
    {
        var data = 1;
        while (true)
        {
            _blockingQueue.Enqueue(data);
            Console.WriteLine("Thread with id " + Thread.CurrentThread.ManagedThreadId + " produced = " + data);
            data++;
            // Thread.Sleep(100);
        }
    }

    private void ConsumerThread()
    {
        while (true)
        {
            int data = _blockingQueue.Dequeue();
            Console.WriteLine("Thread with id " + Thread.CurrentThread.ManagedThreadId + " consumed = " + data);
            // Thread.Sleep(100);
        }
    }
}