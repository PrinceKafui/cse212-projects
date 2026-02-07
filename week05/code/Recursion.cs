using System;
using System.Collections.Generic;

public static class Recursion
{
    /* 
     * Problem 1: Recursive Squares Sum
     **/
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0)
            return 0;

        return n * n + SumSquaresRecursive(n - 1);
    }

    /*
     * Problem 2: Permutations Choose
     **/
    public static void PermutationsChoose(
        List<string> results,
        string letters,
        int size,
        string current = "")
    {
        if (current.Length == size)
        {
            results.Add(current);
            return;
        }

        for (int i = 0; i < letters.Length; i++)
        {
            char chosen = letters[i];
            string remaining =
                letters.Substring(0, i) + letters.Substring(i + 1);

            PermutationsChoose(results, remaining, size, current + chosen);
        }
    }

    /* 
     * Problem 3: Climbing Stairs (Memoized)
     **/
    public static decimal CountWaysToClimb(int s)
    {
        var remember = new Dictionary<int, decimal>();
        return CountWaysToClimb(s, remember);
    }

    private static decimal CountWaysToClimb(
        int s,
        Dictionary<int, decimal> remember)
    {
        if (s < 0)
            return 0;

        if (s == 0)
            return 1;

        if (remember.ContainsKey(s))
            return remember[s];

        decimal result =
            CountWaysToClimb(s - 1, remember) +
            CountWaysToClimb(s - 2, remember) +
            CountWaysToClimb(s - 3, remember);

        remember[s] = result;
        return result;
    }

    /* 
     * Problem 4: Wildcard Binary Patterns
     **/
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int index = pattern.IndexOf('*');

        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        WildcardBinary(
            pattern.Substring(0, index) + "0" + pattern.Substring(index + 1),
            results);

        WildcardBinary(
            pattern.Substring(0, index) + "1" + pattern.Substring(index + 1),
            results);
    }

    /*
     * Problem 5: Maze Solver
     **/
    public static void SolveMaze(List<string> results, Maze maze)
    {
        var currPath = new List<(int x, int y)>();
        SolveMaze(results, maze, currPath, 0, 0);
    }

    private static void SolveMaze(
        List<string> results,
        Maze maze,
        List<(int x, int y)> currPath,
        int x,
        int y)
    {
        // IMPORTANT: correct parameter order
        if (!maze.IsValidMove(currPath, x, y))
            return;

        currPath.Add((x, y));

        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // Explore all four directions
        SolveMaze(results, maze, currPath, x + 1, y);
        SolveMaze(results, maze, currPath, x - 1, y);
        SolveMaze(results, maze, currPath, x, y + 1);
        SolveMaze(results, maze, currPath, x, y - 1);

        // Backtrack
        currPath.RemoveAt(currPath.Count - 1);
    }
}
