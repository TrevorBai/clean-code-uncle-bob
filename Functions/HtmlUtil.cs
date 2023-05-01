public static string RenderPageWithSetupsAndTeardowns(PageData pageData, bool isSuite) 
{
    try 
    {
        var isTestPage = pageData.HasAttribute("Test");
        if (isTestPage) {
            var testPage = pageData.GetWikiPage();
            var newPageContent = new StringBuilder();
            
            IncludeSetupPages(testPage, newPageContent, isSuite);            
            newPageContent.Append(pageData.GetContent());
            
            IncludeTeardownPages(testPage, newPageContent, isSuite);
            pageData.SetContent(newPageContent.ToString());
        }
        
        return pageData.GetHtml();
    } catch (Exception e)
    {
        // Error handling code
    }
}
