using System;
using System.Collections.Generic;

public static class ListExtensions
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            // pick a random index 0 ≤ k ≤ n
            int k = rng.Next(n + 1);
            // swap list[k] and list[n]
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }
}
