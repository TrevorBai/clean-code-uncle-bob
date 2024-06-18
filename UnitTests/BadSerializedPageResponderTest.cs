using System;

public void TestGetPageHierarchyAsXml() 
{
    _crawler.AddPage(root, PathParser.Parse("PageOne"));
    _crawler.AddPage(root, PathParser.Parse("PageOne.ChildOne"));
    _crawler.AddPage(root, PathParser.Parse("PageTwo"));

    _request.SetResource("root");
    _request.AddInput("type", "pages");
    Responder responder = new SerializedPageResponder();
    SimpleResponse response = (SimpleResponse) responder.MakeResopnse(
        new FitNesseContext(root), _request
    );
    string xml = response.GetContent();

    AssertEquals("text/xml", response.GetContentType());    
    AssertSubString("<name>PageOne</name>", xml);
    AssertSubString("<name>PageTwo</name>", xml);
    AssertSubString("<name>ChildOne</name>", xml);
}

public void TestGetPageHierarchyAsXmlDoesntContainSymbolicLinks()
{
    WikePage pageOne = _crawler.AddPage(root, PathParser.Parse("PageOne"));
    _crawler.AddPage(root, PathParser.Parse("PageOne.ChildOne"));
    _crawler.AddPage(root, PathParser.Parse("PageTwo"));

    PageData data = pageOne.GetData();
    WikiPageProperties properties = data.GetProperties();
    WikipageProperty symLinks = properties.Set(SymbolicPage.PROPERTY_NAME);
    symLinks.Set("SymPage", "PageTwo");
    pageOne.Commit(data);

    _request.SetResource("root");
    _request.AddInput("type", "pages");
    Responder responder = new SerializedPageResponder();
    SimpleResponse response = (SimpleResponse) responder.MakeResopnse(
        new FitNesseContext(root), _request
    );
    string xml = response.GetContent();
    
    AssertEquals("text/xml", response.GetContentType());    
    AssertSubString("<name>PageOne</name>", xml);
    AssertSubString("<name>PageTwo</name>", xml);
    AssertSubString("<name>ChildOne</name>", xml);
    AssertNotSubString("SymPage", xml);
}

public void TestGetDataAsHtml()
{
    _crawler.AddPage(root, PathParser.Parse("TestPageOne"), "test page");
    
    _request.SetResource("TestPageOne");
    _request.AddInput("type", "data");
    Responder responder = new SerializedPageResponder();
    SimpleResponse response = (SimpleResponse) responder.MakeResopnse(
        new FitNesseContext(root), _request
    );
    string xml = response.GetContent();
    
    AssertEquals("text/xml", response.GetContentType());    
    AssertSubString("test page", xml);
    AssertSubString("<Test", xml);
}



