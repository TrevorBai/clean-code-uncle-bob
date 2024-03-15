using System.Collections.Generic;

public class CodeAnalyzer : CSharpFileAnalysis
{
    private int _lineCount;
    private int _maxLineWidth;
    private int _widestLineNumber;
    private LineWidthHistogram _lineWidthHistogram;
    private int _totalChars;

    public CodeAnalyzer()
    {
        _lineWidthHistogram = new LineWidthHistogram();
    }

    public static List<File> FindCSharpFiles(File parentDirectory)
    {
        List<File> files = new List<File>();
        FindCSharpFiles(parentDirectory, files);
        return files;
    }

    private static void FindCSharpFiles(File parentDirectory, List<File> files)
    {
        foreach (File file in parentDirectory.ListFiles())
        {
            if (file.GetName().EndsWith(".cs"))
                files.Add(file);
            else if (file.IsDirectory())
                FindCSharpFiles(file, files);      
        } 
    }

    public void AnalyzeFile(File cSharpFile)
    {
        BufferedReader br = new BufferedReader(new FileReader(cSharpFile));
        string line;
        while ((line = br.ReadLine()) != null)
            MeasureLine(line);
    }

    private void MeasureLine(string line)
    {
        _lineCount++;
        int lineSize = line.Length();
        _totalChars += lineSize;
        _lineWidthHistogram.AddLine(lineSize, _lineCount);
        RecordWidestLine(lineSize);
    }

    private void RecordWidestLine(int lineSize)
    {
        if (lineSize > _maxLineWidth)
        {
            _maxLineWidth= lineSize;
            _widestLineNumber = _lineCount;
        }
    }

    public int GetLineCount() { return _lineCount; }

    public int GetMaxLineWidth() { return _maxLineWidth; }

    public int GetWidestLineNumber() { return _widestLineNumber; }

    public LineWidthHistogram GetLineWidthHistogram() { return _lineWidthHistogram; }

    public double GetMeanLineWidth()
    {
        return (double) _totalChars / _lineCount;
    }

    public int GetMedianLineWidth()
    {
        int[] sortedWidths = GetSortedWidths();
        int cumulativeLineCount = 0;
        foreach (int width in sortedWidths)
        {
            cumulativeLineCount += GetLineCountForWidth(width);
            if (cumulativeLineCount > _lineCount / 2) return width;
        }
        throw new Error("Cannot get here"); // Might need switch to c# exceptions
    }

    private int GetLineCountForWidth(int width)
    {
        return _lineWidthHistogram.GetLinesForWidth(width).Size();
    }

    private int[] GetSortedWidths()
    {
        HashSet<int> widths = _lineWidthHistogram.GetWidths();
        int[] sortedWidths = widths.ToArray(); // This might be java way only
        Arrays.Sort(sortedWidths);
        return sortedWidths;
    }

}
