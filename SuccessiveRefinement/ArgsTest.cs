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
    public void TestInvalidArgumentFormat()
    {
        try
        {
            new Args("f~", new string[] {});
            Assert.Fail("Args constructor shoudl have thrown exception");
        }
        catch (ArgsException e)
        {
            AssertEquals(ArgsException.ErrorCode.INVALID_FROMAT, e.GetErrorCode());
            AssertEquals('f', e.GetErrorArgumentId());
        }
    }

    [Test]
    public void TestSimpleBoolPresent()
    {
        var args = new Args("x", new string[] { "-x" });
        AssertEquals(1, args.Cardinality());
        AssertEquals(true, args.GetBool('x'));
    }

    [Test]
    public void TestSimpleStringPresent()
    {
        var args = new Args("x*", new string[] { "-x", "param" });
        AssertEquals(1, args.Cardinality());
        AssertTrue(args.Has('x'));
        AssertEquals("param", args.GetString('x'));
    }

    [Test]
    public void TestMissingStringArgument()
    {
        try
        {
            new Args("x*", new string[] { "-x" });
            Assert.Fail();
        }
        catch (ArgsException e)
        {
            AssertEquals(ArgsException.ErrorCode.MISSING_STRING, e.GetErrorCode());
            AssertEquals('x', e.GetErrorArgumentId());
        }
    }

    [Test]
    public void TestSpacesInFormat()
    {
        var args = new Args("x, y", new string[] { "-xy" });
        AssertEquals(2, args.Cardinality());
        AssertTrue(args.Has('x'));
        AssertTrue(args.Has('y'));
    }

    [Test]
    public void TestSimpleIntPresent()
    {
        var args = new Args("x#", new string[] { "-x", "42" });
        AssertEquals(1, args.Cardinality());
        AssertTrue(args.Has('x'));
        AssertEquals(42, args.GetInt('x'));
    }

    [Test]
    public void TestInvalidInteger()
    {
        try
        {
            new Args("x#", new string[] { "-x", "Forty two" });
            Assert.Fail();
        }
        catch (ArgsException e)
        {
            AssertEquals(ArgsException.ErrorCode.INVALID_INTEGER, e.GetErrorCode());
            AssertEquals('x', e.GetErrorArgumentId());
            AssertEquals("Forty two", e.GetErrorParameter());      
        }     
    }

    [Test]
    public void TestMissingInteger()
    {
        try
        {
            new Args("x#", new string[] { "-x" });
            Assert.Fail();
        }
        catch (ArgsException e)
        {
            AssertEquals(ArgsException.ErrorCode.MISSING_INTEGER, e.GetErrorCode());
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
        try
        {
            new Args("x##", new string[] {"-x", "Forty two"});
            Assert.Fail();
        } 
        catch (ArgsException e)
        {
            AssertEquals(ArgsException.ErrorCode.INVALID_DOUBLE, e.GetErrorCode());
            AssertEquals('x', e.GetErrorArgumentId());
            AssertEquals("Forty two", e.GetErrorParameter());  
            
        }
    }

    [Test]
    public void TestMissingDouble()
    {
        try
        {
            new Args("x##", new string[] {"-x"});
            Assert.Fail();
        }
        catch (ArgsException e)
        {
            AssertEquals(ArgsException.ErrorCode.MISSING_DOUBLE, e.GetErrorCode());
            AssertEquals('x', e.GetErrorArgumentId());        
        }
    }
  
}
