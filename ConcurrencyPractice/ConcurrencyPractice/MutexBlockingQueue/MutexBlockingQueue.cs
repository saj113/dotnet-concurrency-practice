namespace ConcurrencyPractice.MutexBlockingQueue;

public class MutexBlockingQueue<T>(int maxSize)
{
    private readonly List<T> _items = new(maxSize);
    private readonly Mutex _mutex = new();
    private int _currentSize = 0;

    public void Enqueue(T item)
    {
        _mutex.WaitOne();
        try
        {
            while (_currentSize == maxSize)
            {
                _mutex.ReleaseMutex();
                _mutex.WaitOne();
            }

            _items.Add(item);
            _currentSize++;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }

    public T Dequeue()
    {
        _mutex.WaitOne();
        try
        {
            while (_currentSize == 0)
            {
                _mutex.ReleaseMutex();
                _mutex.WaitOne();
            }

            var item = _items.First();
            _items.Remove(item);
            _currentSize--;
            return item;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }
}