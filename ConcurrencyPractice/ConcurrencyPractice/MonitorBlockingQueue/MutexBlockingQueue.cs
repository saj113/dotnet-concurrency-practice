namespace ConcurrencyPractice.MonitorBlockingQueue;

public class MonitorBlockingQueue<T>(int maxSize)
{
    private readonly List<T> _items = new(maxSize);
    private int _currentSize = 0;

    public void Enqueue(T item)
    {
        Monitor.Enter(_items);
        try
        {
            while (_currentSize == maxSize)
            {
                Monitor.Wait(_items);
            }

            _items.Add(item);
            _currentSize++;
            Monitor.PulseAll(_items);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Monitor.Exit(_items);
        }
    }

    public T Dequeue()
    {
        Monitor.Enter(_items);
        try
        {
            while (_currentSize == 0)
            {
                Monitor.Wait(_items);
            }

            var item = _items.First();
            _items.Remove(item);
            _currentSize--;
            Monitor.PulseAll(_items);
            return item;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Monitor.Exit(_items);
        }
    }
}