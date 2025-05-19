namespace Prn212_HW;

public class LambdaLearning
{
    public static void Nigga()
    {
        int[] arr = [21, 234, 2134, 632, 123, 6432, 742];
        Print((x) => x > 400, arr);
    }

    static void Print(Func<int, bool> f, int[] arr)
    {
        foreach (var i in arr)
        {
            if (f(i))
            {
                Console.WriteLine($"{i}   ");
            }
        }
    }
}