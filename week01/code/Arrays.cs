public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.
    /// </summary>
    public static double[] MultiplesOf(double number, int length)
    {
        // Step 1: Create a new array of size 'length' to store the multiples
        double[] result = new double[length];

        // Step 2: Loop through each index of the array
        for (int i = 0; i < length; i++)
        {
            // Step 3: Calculate the multiple
            result[i] = number * (i + 1);
        }

        // Step 4: Return the completed array
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Step 1: Find where the slice begins
        int startIndex = data.Count - amount;

        // Step 2: Copy the last 'amount' elements
        List<int> tail = data.GetRange(startIndex, amount);

        // Step 3: Remove those elements from the end
        data.RemoveRange(startIndex, amount);

        // Step 4: Insert them at the beginning
        data.InsertRange(0, tail);
    }
}
