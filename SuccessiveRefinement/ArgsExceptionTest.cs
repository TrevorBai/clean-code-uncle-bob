using NUnit.Framework;

[TestFixture]
public class ArgsExceptionTest
{
    [Test]
    public void TestUnexpectedMessage()
    {
        var e = new ArgsException(ArgsException.ErrorCode.UNEXPECTED_ARGUMENT, 'x', null);
        Assert.AreEqual("Argument -x unexpected.", e.ErrorMessage());       
    }

    [Test]
    public void TestMissingStringMessage()
    {
        var e = new ArgsException(ArgsException.ErrorCode.MISSING_STRING, 'x', null);
        Assert.AreEqual("Could not find string parameter for -x.", e.ErrorMessage());
    }

    [Test]
    public void TestInvalidIntegerMessage()
    {
        var e = ArgsException(AgrsException.ErrorCode.INVALID_INTEGER, 'x', "Forty two");
        Assert.AreEqual("Argument -x expects an integer but was \"Forty two\".", e.ErrorMessage());
    }

    [Test]
    public void TestMissingIntegerMessage()
    {
        var e = new ArgsException(grsException.ErrorCode.MISSING_INTEGER, 'x', null);
        Assert.AreEqual("Could not find integer parameter for -x.", e.ErrorMessage());
    }

    [Test]
    public void TestInvalidDoubleMessage()
    {
        var e = new ArgsException(grsException.ErrorCode.INVALID_DOUBLE, 'x', "Forty two");
        Assert.AreEqual("Argument -x expects a double but was \"Forty two\".", e.ErrorMessage());        
    }

    [Test]
    public void TestMissingDoubleMessage()
    {
        var e = new ArgsException(grsException.ErrorCode.MISSING_DOUBLE, 'x', null);
        Assert.AreEqual("Could not find double parameter for -x.", e.ErrorMessage());  
    }

}
