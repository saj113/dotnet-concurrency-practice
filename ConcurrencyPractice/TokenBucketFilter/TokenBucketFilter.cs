namespace ConcurrencyPractice.TokenBucketFilter;

public class TokenBucketFilter(int maxTokens)
{
    private int _tokens = 0;
    private long _lastRefillTime =  DateTimeOffset.Now.ToUnixTimeMilliseconds();

    public void GetToken()
    {
        lock (this)
        {
            if (_tokens == 0)
            {
                var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                var milliseconds = now - _lastRefillTime;

                if (milliseconds > 1000)
                {
                    var newTokens = (int) (milliseconds / 1000);
                    _tokens = Math.Min(newTokens, maxTokens);
                    Console.WriteLine(_tokens);
                    _lastRefillTime = now;
                }
                else
                {
                    Thread.Sleep(1000 - (int)milliseconds);
                    _tokens = 1;
                    _lastRefillTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                }
            }

            _tokens--;
        }
    }
}