public static string RenderPageWithSetupsAndTeardowns(PageData pageData, bool isSuite) 
{
    try 
    {
        if (IsTestPage(pageData)) IncludeSetupAndTeardownPages(pageData, isSuite);
        return pageData.GetHtml();
    } catch (Exception e)
    {
        // Error handling code
    }
}
