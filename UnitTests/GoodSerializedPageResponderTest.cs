// My gut feeling is, code below encapsulates a lot of no-need-to-know details and make 
// human-readable methods name which is very helpful even for a guy who doesn't know
// web tech.

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
