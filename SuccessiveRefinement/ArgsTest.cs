using NUnit.Framework;

[TestFixture]
public class ArgsTest
{
    [Test]
    public void TestCreateWithNoSchemaOrArguments()
    {
        var args = new Args("", new string[0]);
        AssertEquals(0, args.Cardinality());
    }

    [Test]
    public void TestWithNoSchemaButWithOneArgument()
    {
        try
        {
            new Args("", new string[] { "-x" });
            Assert.Fail();
        }
        catch (ArgsException e)
        {
            AssertEquals(ArgsException.ErrorCode.UNEXPECTED_ARGUMENT, e.GetErrorCode());
            AssertEquals('x', e.GetErrorArgumentId());
        }     
    }

    [Test]
    public void TestWithNoSchemaButWithMultipleArguments()
    {
        try
        {
            new Args("", new string[] { "-x", "-y" });
            Assert.Fail();
        }
        catch (ArgsException e)
        {
            AssertEquals(ArgsException.ErrorCode.UNEXPECTED_ARGUMENT, e.GetErrorCode());
            AssertEquals('x', e.GetErrorArgumentId());
        }    
    }

    [Test]
    public void TestNonLetterSchema()
    {
        try
        {
            new Args("*", new string[] {});
            Assert.Fail("Args constructor should have thrown exception");
        }
        catch (ArgsException e)
        {
            AssertEquals(ArgsException.ErrorCode.INVALID_ARGUMENT_NAME, e.GetErrorCode());
            AssertEquals('x', e.GetErrorArgumentId());
        }        
    }






    

    [Test]
    public void TestSimpleDoublePresent()
    {
        var args = new Args("x##", new string[] {"-x" , "42.3"});
        AssertTrue(args.IsValid());
        AssertEquals(1, args.Cardinality());
        AssertTrue(args.Has('x'));
        AssertEquals(42.3, args.GetDouble('x'), .001);
    }

    [Test]
    public void TestInvalidDouble()
    {
        var args = new Args("x##", new string[] {"-x", "Forty two"});
        AssertFalse(args.IsValid());
        AssertEquals(0, args.Cardinality());
        AssertFalse(args.Has('x'));
        AssertEquals(0, args.GetInt('x'));
        AssertEquals("Argument -x expects a double but was \"Forty two\".", args.ErrorMessage());   
    }

    [Test]
    public void TestMissingDouble()
    {
        var args = new Args("x##", new string[] {"-x"});
        AssertFalse(args.IsValid());
        AssertEquals(0, args.Cardinality());
        AssertFalse(args.Has('x'));
        AssertEquals(0.0, args.GetDouble('x'), 0.01);
        AssertEquals("Could not find double parameter for -x.", args.ErrorMessage());
    }

    
}
