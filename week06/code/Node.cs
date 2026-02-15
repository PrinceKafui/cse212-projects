using System;

public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // Problem 1: Insert Unique Values Only
        
        // If value equals current node's data, don't insert (unique values only)
        if (value == Data)
            return;
        
        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else // value > Data
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // Problem 2: Contains
        
        // If current node's data equals the value, we found it
        if (value == Data)
            return true;
        
        // If value is less than current node's data, search left subtree
        if (value < Data)
        {
            // If left child exists, recursively search it
            if (Left is not null)
                return Left.Contains(value);
            else
                return false; // No left child, value not found
        }
        else // value > Data
        {
            // If right child exists, recursively search it
            if (Right is not null)
                return Right.Contains(value);
            else
                return false; // No right child, value not found
        }
    }

    public int GetHeight()
    {
        // Problem 4: Tree Height
        
        // Calculate height of left subtree (0 if null)
        int leftHeight = Left?.GetHeight() ?? 0;
        
        // Calculate height of right subtree (0 if null)
        int rightHeight = Right?.GetHeight() ?? 0;
        
        // Height is 1 (current node) + maximum of left and right subtree heights
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}