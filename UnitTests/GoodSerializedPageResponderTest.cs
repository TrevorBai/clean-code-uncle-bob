// My gut feeling is, code below encapsulates a lot of no-need-to-know details and make 
// human-readable methods name which is very helpful even for a guy who doesn't know
// web tech.

// Without using fancy terms, I guess this is the SINGLE key factor or art of programming,
// i.e. make the code human readable, encapsulate details away, and build a hierarchical
// code structure, which is what I am striving for as my day job.

// There is a design pattern or a code pattern used here for writing a unit test, which is
// build-operate-check pattern. Tbh, I used it to write my test just without knowing the name
// of it.

// It has three parts:
// 1. build up the test data (model).
// 2. operate on the test data (model).
// 3. checks the operation yielded the expected results.

public void TestGetPageHierarchyAsXml() 
{
    MakePages("PageOne", "PageOne.ChildOne", "PageTwo");

    SubmitRequest("root", "type:pages");

    AssertResponseIsXML();
    AssertResponseContains("<name>PageOne</name>", "<name>PageTwo</name>", "<name>ChildOne</name>");
}

public void TestSymbolicLinksAreNotInXmlPageHierarchy()
{
    Wikipage page = MakePage("PageOne");
    MakePages("PageOne.ChildOne", "PageTwo");

    AddLinkTo(page, "PageTwo", "SymPage");

    SubmitRequest("root", "type:pages");

    AssertResponseIsXML();
    AssertResponseContains("<name>PageOne</name>", "<name>PageTwo</name>", "<name>ChildOne</name>");
    AssertResponseDoesNotContain("SymPage");
}

public void TestGetDataAsXml()
{
    MakePageWithContent("TestPageOne", "test page");

    SubmitRequest("TestPageOne", "type:data");

    AssertResponseIsXML();
    AssertResponseContains("test page", "<Test");
}
