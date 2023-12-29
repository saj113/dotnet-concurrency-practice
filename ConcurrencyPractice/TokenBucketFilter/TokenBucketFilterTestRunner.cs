namespace ConcurrencyPractice.TokenBucketFilter;

public class TokenBucketFilterTestRunner : ITestRunner
{
    private readonly TokenBucketFilter _tokenBucketFilter = new TokenBucketFilter(1);
    public string Name => "Token Bucket Filter";
    public void Run()
    {

        var threads = new Thread[10];
        for (var i = 0; i < 10; i++)
        {
            threads[i] = new Thread(GetToken);
            threads[i].Start();
        }

        for (var i = 0; i < 10; i++)
        {
            threads[i].Join();
        }
    }

    private void GetToken()
    {
        _tokenBucketFilter.GetToken();
        Console.WriteLine("Thread with id " + Environment.CurrentManagedThreadId + " got the token. Current second = " + DateTimeOffset.Now.ToUnixTimeSeconds());
    }
}