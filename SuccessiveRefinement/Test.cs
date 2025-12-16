public void TestSimpleDoublePresent()
{
    var args = new Args("x##", new string[] {"-x" , "42.3"});
    AssertTrue(args.IsValid());
    AssertEquals(1, args.Cardinality());
    AssertTrue(args.Has('x'));
    AssertEquals(42.3, args.GetDouble('x'), .001);
}
