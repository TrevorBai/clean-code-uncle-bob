// Package LiteratePrimes;

public class PrimeGenerator
{
    private static int[] _primes;
    private static List<int> _multiplesOfPrimeFactors;

    protected static int[] Generate(int n) 
    {
        _primes = new int[n];
        _multiplesOfPrimeFactors = new List<int>();
        Set2AsFirstPrime();
        CheckOddNumbersForSubsequentPrimes();
        return _primes;
    }

    private static void Set2AsFirstPrime()
    {
        _primes[0] = 2;
        _multiplesOfPrimeFactors.Add(2);
    }

    // ...

    
}
