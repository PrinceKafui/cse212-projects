public class SimpleQueueSolution
{
    public static void Run()
    {
        // Test 1
        Console.WriteLine("Test 1");
        var queue = new SimpleQueueSolution();
        queue.Enqueue(100);
        var value = queue.Dequeue();
        Console.WriteLine(value);

        Console.WriteLine("------------");

        // Test 2
        Console.WriteLine("Test 2");
        queue = new SimpleQueueSolution();
        queue.Enqueue(200);
        queue.Enqueue(300);
        queue.Enqueue(400);
        Console.WriteLine(queue.Dequeue());
        Console.WriteLine(queue.Dequeue());
        Console.WriteLine(queue.Dequeue());

        Console.WriteLine("------------");

        // Test 3
        Console.WriteLine("Test 3");
        queue = new SimpleQueueSolution();
        try
        {
            queue.Dequeue();
            Console.WriteLine("Oops ... This shouldn't have worked.");
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("I got the exception as expected.");
        }
    }

    private readonly List<int> _queue = new();

    /// <summary>
    /// Enqueue the value provided into the queue
    /// </summary>
    private void Enqueue(int value)
    {
        // FIX: Add to the back of the queue
        _queue.Add(value);
    }

    /// <summary>
    /// Dequeue the next value and return it
    /// </summary>
    private int Dequeue()
    {
        // FIX: Throw exception if queue is empty
        if (_queue.Count == 0)
            throw new IndexOutOfRangeException();

        // FIX: Remove from the front of the queue
        int value = _queue[0];
        _queue.RemoveAt(0);
        return value;
    }
}
