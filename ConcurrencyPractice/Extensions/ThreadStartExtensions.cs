namespace ConcurrencyPractice.Extensions;

public static class ThreadStartExtensions
{
    public static Thread CreateBackgroundThread(this ThreadStart threadStart)
    {
        var thread = new Thread(threadStart)
        {
            IsBackground = true
        };
        return thread;
    }
}