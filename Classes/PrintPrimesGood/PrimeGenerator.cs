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

    private static void CheckOddNumbersForSubsequentPrimes()
    {
        int primeIndex = 1;
        for (int candidate = 3; primeIndex < _primes.Length; candidate += 2)
        {
            if (IsPrime(candidate)) _primes[primeIndex++] = candidate;
        }    
    }

    private static bool IsPrime(int candidate)
    {
        if (IsLeastRelevantMultipleOfNextLargerPrimeFactor(candidate))
        {
            _multiplesOfPrimeFactors.Add(candidate);
            return false;
        }
        return IsNotMultipleOfAnyPreviousPrimeFactor(candidate);    
    }

    private static bool IsLeastRelevantMultipleOfNextLargerPrimeFactor(int candidate)
    {
        int nextLargerPrimeFactor = _primes[_multiplesOfPrimeFactors.Count];
        int leastRelevantMultiple = nextLargerPrimeFactor * nextLargerPrimeFactor;
        return candidate == leastRelevantMultiple;
    }

    private static bool IsNotMultipleOfAnyPreviousPrimeFactor(int candidate)
    {
        for (int n = 1; n < _multiplesOfPrimeFactors.Count; n++)
        {
            if (IsMultipleOfNthPrimeFactor(candidate, n)) return false;
        }
        return true;
    }

    private static bool IsMultipleOfNthPrimeFactor(int candidate, int n)
    {
        return candidate == SmallestOddNthMultipleNotLessThanCandidate(candidate, n);
    }

    private static int SmallestOddNthMultipleNotLessThanCandidate(int candidate, int n)
    {
        int multiple = _multiplesOfPrimeFactors[n];
        while (multiple < candidate)
            multiple += 2 * _primes[n];
        _multiplesOfPrimeFactors[n] = multiple;
        return multiple;
    }
}
