namespace ConcurrencyPractice;

public interface ITestRunner
{
    string Name { get; }

    void Run();
}