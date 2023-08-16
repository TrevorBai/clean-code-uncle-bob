// Using some namespaces
using System.Text;

// #################################################################
// Not sure what are the equivalent packages in .NET 
// package fitnesse.html
using fitnesse.responders.run.SuiteResponder;
using fitnesse.wiki.*;
// #################################################################

// Try to look at this coding style, it's aesthetic beautiful code.
public class SetupTeardownIncluder 
{
    private PageData _pageData;
    private bool _isSuite;
    private WikiPage _testPage;
    private StringBuilder _newPageContent;
    private PageCrawler _pageCrawler;

    public static string Render(PageData pageData) {
        return Render(pageData, false);
    }

    public static string Render(PageData pageData, bool isSuite) {
        return new SetupTeardownIncluder(pageData).Render(isSuite);
    }

    // Private constructor, hmmm?
    private SetupTeardownIncluder(PageData pageData) {
        _pageData = pageData;
        _testPage = pageData.GetWikiPage();
        _pageCrawler = _testPage.GetPageCrawler();
        _newPageContent = new StringBuilder();      
    }

    private string Render(bool isSuite) {
        _isSuite = isSuite;
        if (IsTestPage()) IncludeSetupAndTeardownPages();
        return _pageData.GetHtml();
    }

    private bool IsTestPage() {
        return _pageData.HasAttribute("Test");    
    }

    private void IncludeSetupAndTeardownPages() {
        IncludeSetupPages();
        IncludePageContent();
        IncludeTeardownPages();
        UpdatePageContent();
    }

    private void IncludeSetupPages() {
        if (_isSuite) IncludeSuiteSetupPage();
        IncludeSetupPage();
    }

    private void IncludeSuiteSetupPage() {
        Include(SuiteResponder.SUITE_SETUP_NAME, "-setup");      
    }

    private void IncludeSetupPage() {
        Include("SetUp", "-setup");
    }

    private void IncludePageContent() {
        _newPageContent.Append(_pageData.GetContent());
    }

    private void IncludeTeardownPages() {
        IncludeTeardownPage();
        if (_isSuite) IncludeSuiteTeardownPage();
    }

    private void IncludeTeardownPage() {
        Include("TearDown", "-teardown");
    }

    private void IncludeSuiteTeardownPage() {
        Include(SuiteResponder.SUITE_TEARDOWN_NAME, "-teardown");
    }

    private void UpdatePageContent() {
        _pageData.SetContent(_newPageContent.ToString());
    }

    private void Include(string pageName, string arg) {
        WikiPage inheritedPage = FindInheritedPage(pageName);
        if (inheritedPage == null) return;
        string pagePathName = GetPathNameForPage(inheritedPage);
        BuildIncludeDirective(pagePathName, arg);
    }

    private WikiPage FindInheritedPage(string pageName) {
        return PageCrawlerImpl.GetInheritedPage(pageName, _testPage);  
    }

    private string GetPathNameForPage(WikiPage page) {
        WikiPagePath pagePath = _pageCrawler.GetFullPath(page);
        return PathParser.Render(pagePath);
    }

    private void BuildIncludeDirective(string pagePathName, string arg) {
        _newPageContent
            .Append("\n!include ")
            .Append(arg)
            .Append(" .")
            .Append(pagePathName)
            .Append("\n");   
    }
}
