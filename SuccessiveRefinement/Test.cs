public void TestSimpleDoublePresent()
{
    var args = new Args("x##", new string[] {"-x" , "42.3"});
    AssertTrue(args.IsValid());
    AssertEquals(1, args.Cardinality());
    AssertTrue(args.Has('x'));
    AssertEquals(42.3, args.GetDouble('x'), .001);
}

public void TestInvalidDouble()
{
    var args = new Args("x##", new string[] {"-x", "Forty two"});
    AssertFalse(args.IsValid());
    AssertEquals(0, args.Cardinality());
    AssertFalse(args.Has('x'));
    AssertEquals(0, args.GetInt('x'));
    AssertEquals("Argument -x expects a double but was \"Forty two\".",
                args.ErrorMessage());   
}
