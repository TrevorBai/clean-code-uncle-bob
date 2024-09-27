// Package LiteratePrimes;

public class RowColumnPagePrinter
{
    private readonly int _rowsPerPage;
    private readonly int _columnsPerPage;
    private readonly int _numbersPerPage;
    private readonly string _pageHeader;

    public RowColumnPagePrinter(int rowsPerPage, int columnsPerPage, string pageHeader)
    {
        _rowsPerPage = rowsPerPage;
        _columnsPerPage = columnsPerPage;
        _pageHeader = pageHeader;
        _numbersPerPage = rowsPerPage * columnsPerPage;
    }

    public void Print(int data[])
    {
        int pageNumber = 1;
        for (int firstIndexOnPage = 0; firstIndexOnPage < data.Length; firstIndexOnPage += _numbersPerPage)
        {
            int lastIndexOnPage = Math.Min(firstIndexOnPage + _numbersPerPage - 1, data.Length - 1);
            PrintPageHeader(_pageHeader, pageNumber);
            PrintPage(firstIndexOnPage, lastIndexOnPage, data);
            Console.WriteLine("\f");
            pageNumber++;
        }   
    }

    private void PrintPage(int firstIndexOnPage, int lastIndexOnPage, int[] data)
    {
        int firstIndexOfLastRowOnPage = firstIndexOnPage + _rowsPerPage - 1;
        for (int firstIndexInRow = firstIndexOnPage; firstIndexInRow <= firstIndexOfLastRowOnPage; firstIndexInRow++)
        {
            PrintRow(firstIndexInRow, lastIndexOnPage, data);
            Console.WriteLine("");
        }  
    }

    private void PrintRow(int firstIndexInRow, int lastIndexOnPage, int[] data)
    {
        for (int column = 0; column < _columnsPerPage; column++)
        {
            int index = firstIndexInRow + column * _rowsPerPage;
            if (index <= lastIndexOnPage) Console.WriteLine(string.Format("%10d", data[index]));
        }      
    }

    private void PrintPageHeader(string pageHeader, int pageNumber)
    {
        Console.WriteLine(pageHeader + " --- page " + pageNumber);
        Console.WriteLine("");
    }
}
