using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities and dequeue them
    // Expected Result: Items should be dequeued in order of highest priority first
    // Defect(s) Found: The dequeue operation doesn't correctly find the highest priority item.
    // It appears to be dequeuing from the front regardless of priority.
    public void TestPriorityQueue_BasicPriority()
    {
        var priorityQueue = new PriorityQueue();
        
        // Add items with different priorities
        priorityQueue.Enqueue("Low Priority", 1);
        priorityQueue.Enqueue("High Priority", 3);
        priorityQueue.Enqueue("Medium Priority", 2);
        
        // First dequeue should return "High Priority" (priority 3)
        var result1 = priorityQueue.Dequeue();
        Assert.AreEqual("High Priority", result1);
        
        // Second dequeue should return "Medium Priority" (priority 2)
        var result2 = priorityQueue.Dequeue();
        Assert.AreEqual("Medium Priority", result2);
        
        // Third dequeue should return "Low Priority" (priority 1)
        var result3 = priorityQueue.Dequeue();
        Assert.AreEqual("Low Priority", result3);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with same highest priority
    // Expected Result: Items with same priority should be dequeued in FIFO order
    // Defect(s) Found: When multiple items have same priority, they are not
    // being dequeued in FIFO order.
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        
        // Add items with same priority
        priorityQueue.Enqueue("First", 2);
        priorityQueue.Enqueue("Second", 2);
        priorityQueue.Enqueue("Third", 1);
        priorityQueue.Enqueue("Fourth", 2);
        
        // First dequeue should return "First" (first item with priority 2)
        var result1 = priorityQueue.Dequeue();
        Assert.AreEqual("First", result1);
        
        // Second dequeue should return "Second" (second item with priority 2)
        var result2 = priorityQueue.Dequeue();
        Assert.AreEqual("Second", result2);
        
        // Third dequeue should return "Fourth" (third item with priority 2)
        var result3 = priorityQueue.Dequeue();
        Assert.AreEqual("Fourth", result3);
        
        // Fourth dequeue should return "Third" (only priority 1 left)
        var result4 = priorityQueue.Dequeue();
        Assert.AreEqual("Third", result4);
    }

    [TestMethod]
    // Scenario: Enqueue items in random order with mixed priorities
    // Expected Result: Should always dequeue highest priority first,
    // with FIFO for same priorities
    // Defect(s) Found: The priority comparison logic is incorrect when
    // items are added in non-sorted order.
    public void TestPriorityQueue_MixedOrder()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);
        priorityQueue.Enqueue("D", 3);
        priorityQueue.Enqueue("E", 1);
        
        // First should be "B" (priority 3, first in queue with that priority)
        Assert.AreEqual("B", priorityQueue.Dequeue());
        
        // Second should be "D" (priority 3, second in queue with that priority)
        Assert.AreEqual("D", priorityQueue.Dequeue());
        
        // Third should be "C" (priority 2)
        Assert.AreEqual("C", priorityQueue.Dequeue());
        
        // Fourth should be "A" (priority 1, first in queue with that priority)
        Assert.AreEqual("A", priorityQueue.Dequeue());
        
        // Fifth should be "E" (priority 1, second in queue with that priority)
        Assert.AreEqual("E", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue
    // Expected Result: InvalidOperationException should be thrown with message "The queue is empty."
    // Defect(s) Found: No exception is thrown when dequeuing from empty queue,
    // or wrong exception type/message is thrown.
    public void TestPriorityQueue_DequeueEmpty()
    {
        var priorityQueue = new PriorityQueue();
        
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException was not thrown");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception type: {ex.GetType().Name}");
        }
    }

    [TestMethod]
    // Scenario: Add items, dequeue some, then add more items
    // Expected Result: The queue should maintain correct priority ordering
    // even after partial dequeues
    // Defect(s) Found: After partial dequeues, new items are not being
    // inserted correctly relative to remaining items.
    public void TestPriorityQueue_EnqueueAfterDequeue()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("A", 3);
        priorityQueue.Enqueue("B", 1);
        priorityQueue.Enqueue("C", 2);
        
        // Dequeue the highest priority
        Assert.AreEqual("A", priorityQueue.Dequeue());
        
        // Add new items
        priorityQueue.Enqueue("D", 4);  // New highest priority
        priorityQueue.Enqueue("E", 2);  // Same priority as C
        
        // Now D should come first (priority 4)
        Assert.AreEqual("D", priorityQueue.Dequeue());
        
        // Then C (priority 2, added before E)
        Assert.AreEqual("C", priorityQueue.Dequeue());
        
        // Then E (priority 2, added after C)
        Assert.AreEqual("E", priorityQueue.Dequeue());
        
        // Finally B (priority 1)
        Assert.AreEqual("B", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Multiple items with same priority added in different order
    // Expected Result: Should maintain strict FIFO for same priority items
    // Defect(s) Found: The queue may not preserve insertion order for
    // items with identical priority.
    public void TestPriorityQueue_ComplexSamePriority()
    {
        var priorityQueue = new PriorityQueue();
        
        // Add items with same priority in specific order
        priorityQueue.Enqueue("First", 2);
        priorityQueue.Enqueue("X", 3);
        priorityQueue.Enqueue("Second", 2);
        priorityQueue.Enqueue("Y", 1);
        priorityQueue.Enqueue("Third", 2);
        priorityQueue.Enqueue("Z", 3);
        
        // Order should be: X (3), Z (3), First (2), Second (2), Third (2), Y (1)
        Assert.AreEqual("X", priorityQueue.Dequeue());
        Assert.AreEqual("Z", priorityQueue.Dequeue());
        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
        Assert.AreEqual("Y", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Check if queue can handle negative priorities
    // Expected Result: Should work correctly with negative priorities
    // Defect(s) Found: May not handle negative priorities correctly
    public void TestPriorityQueue_NegativePriority()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Lowest", -2);
        priorityQueue.Enqueue("Highest", 2);
        priorityQueue.Enqueue("Middle", 0);
        priorityQueue.Enqueue("Low", -1);
        
        // Should dequeue in order: Highest (2), Middle (0), Low (-1), Lowest (-2)
        Assert.AreEqual("Highest", priorityQueue.Dequeue());
        Assert.AreEqual("Middle", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
        Assert.AreEqual("Lowest", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Single item in queue
    // Expected Result: Should return the single item when dequeued
    // Defect(s) Found: Basic single item case should work
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Single", 5);
        Assert.AreEqual("Single", priorityQueue.Dequeue());
        
        // Queue should now be empty
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should throw exception when dequeuing from empty queue");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
    }

    [TestMethod]
    // Scenario: All items have same priority
    // Expected Result: Should behave like a regular FIFO queue
    // Defect(s) Found: Should maintain strict insertion order when all priorities are equal
    public void TestPriorityQueue_AllSamePriority()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 1);
        priorityQueue.Enqueue("Third", 1);
        priorityQueue.Enqueue("Fourth", 1);
        
        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
        Assert.AreEqual("Fourth", priorityQueue.Dequeue());
    }
}