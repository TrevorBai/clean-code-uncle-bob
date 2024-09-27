// Package LiteratePrimes;

public class PrimePrinter
{
    public static void Main(string[] args)
    {
        const int numberOfPrimes = 1000;
        int[] primes = PrimeGenerator.Generate(numberOfPrimes);
        const int rowsPerPage = 50;
        const int columnsPerPage = 4;
        var tablePrinter = new RowColumnPagePrinter(rowsPerPage, columnsPerPage,
                                                   "The First " + numberOfPrimes + " Prime Numbers");
        tablePrinter.Print(primes);       
    }
}
