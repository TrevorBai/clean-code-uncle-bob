using NUnit.Framework;

[TestFixture]
public class ArgsExceptionTest
{
    [Test]
    public void TestUnexpectedMessage()
    {
        var e = new ArgsException(ArgsException.ErrorCode.UNEXPECTED_ARGUMENT, 'x', null);
        AssertEquals("Argument -x unexpected.", e.ErrorMessage());       
    }

    [Test]
    public void TestMissingStringMessage()
    {
        var e = new ArgsException(ArgsException.ErrorCode.MISSING_STRING, 'x', null);
        AssertEquals("Could not find string parameter for -x.", e.ErrorMessage());
    }

    [Test]
    public void TestInvalidIntegerMessage()
    {
        var e = ArgsException(AgrsException.ErrorCode.INVALID_INTEGER, 'x', "Forty two");
        AssertEquals("Argument -x expects an integer but was \"Forty two\".", e.ErrorMessage());
    }

    [Test]
    public void TestMissingIntegerMessage()
    {
        var e = new ArgsException(grsException.ErrorCode.MISSING_INTEGER, 'x', null);
        AssertEquals("Could not find integer parameter for -x.", e.ErrorMessage());
    }

    [Test]
    public void TestInvalidDoubleMessage()
    {
        var e = new ArgsException(grsException.ErrorCode.INVALID_DOUBLE, 'x', "Forty two");
        AssertEquals("Argument -x expects a double but was \"Forty two\".", e.ErrorMessage());        
    }

    [Test]
    public void TestMissingDoubleMessage()
    {
        var e = new ArgsException(grsException.ErrorCode.MISSING_DOUBLE, 'x', null);
        AssertEquals("Could not find double parameter for -x.", e.ErrorMessage());  
    }

}
