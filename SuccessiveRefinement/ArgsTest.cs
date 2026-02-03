using NUnit.Framework;

[TestFixture]
public class ArgsTest
{
    [Test]
    public void TestCreateWithNoSchemaOrArguments()
    {
        var args = new Args("", new string[0]);
        Assert.AreEqual(0, args.Cardinality());
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
            Assert.AreEqual(ArgsException.ErrorCode.UNEXPECTED_ARGUMENT, e.GetErrorCode());
            Assert.AreEqual('x', e.GetErrorArgumentId());
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
            Assert.AreEqual(ArgsException.ErrorCode.UNEXPECTED_ARGUMENT, e.GetErrorCode());
            Assert.AreEqual('x', e.GetErrorArgumentId());
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
            Assert.AreEqual(ArgsException.ErrorCode.INVALID_ARGUMENT_NAME, e.GetErrorCode());
            Assert.AreEqual('x', e.GetErrorArgumentId());
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
            Assert.AreEqual(ArgsException.ErrorCode.INVALID_FROMAT, e.GetErrorCode());
            Assert.AreEqual('f', e.GetErrorArgumentId());
        }
    }

    [Test]
    public void TestSimpleBoolPresent()
    {
        var args = new Args("x", new string[] { "-x" });
        Assert.AreEqual(1, args.Cardinality());
        Assert.AreEqual(true, args.GetBool('x'));
    }

    [Test]
    public void TestSimpleStringPresent()
    {
        var args = new Args("x*", new string[] { "-x", "param" });
        Assert.AreEqual(1, args.Cardinality());
        Assert.IsTrue(args.Has('x'));
        Assert.AreEqual("param", args.GetString('x'));
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
            Assert.AreEqual(ArgsException.ErrorCode.MISSING_STRING, e.GetErrorCode());
            Assert.AreEqual('x', e.GetErrorArgumentId());
        }
    }

    [Test]
    public void TestSpacesInFormat()
    {
        var args = new Args("x, y", new string[] { "-xy" });
        Assert.AreEqual(2, args.Cardinality());
        Assert.IsTrue(args.Has('x'));
        Assert.IsTrue(args.Has('y'));
    }

    [Test]
    public void TestSimpleIntPresent()
    {
        var args = new Args("x#", new string[] { "-x", "42" });
        Assert.AreEqual(1, args.Cardinality());
        Assert.IsTrue(args.Has('x'));
        Assert.AreEqual(42, args.GetInt('x'));
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
            Assert.AreEqual(ArgsException.ErrorCode.INVALID_INTEGER, e.GetErrorCode());
            Assert.AreEqual('x', e.GetErrorArgumentId());
            Assert.AreEqual("Forty two", e.GetErrorParameter());
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
            Assert.AreEqual(ArgsException.ErrorCode.MISSING_INTEGER, e.GetErrorCode());
            Assert.AreEqual('x', e.GetErrorArgumentId());        
        } 
    }

    [Test]
    public void TestSimpleDoublePresent()
    {
        var args = new Args("x##", new string[] {"-x" , "42.3"});
        Assert.IsTrue(args.IsValid());
        Assert.AreEqual(1, args.Cardinality());
        Assert.IsTrue(args.Has('x'));
        Assert.AreEqual(42.3, args.GetDouble('x'), .001);
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
            Assert.AreEqual(ArgsException.ErrorCode.INVALID_DOUBLE, e.GetErrorCode());
            Assert.AreEqual('x', e.GetErrorArgumentId());
            Assert.AreEqual("Forty two", e.GetErrorParameter());  
            
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
            Assert.AreEqual(ArgsException.ErrorCode.MISSING_DOUBLE, e.GetErrorCode());
            Assert.AreEqual('x', e.GetErrorArgumentId());        
        }
    }
  
}
