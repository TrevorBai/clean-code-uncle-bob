using System;

public class WikiPageResponder : SecureResponder
{
    protected WikiPage Page;
    protected PageData PageData;
    protected string PageTitle;
    protected Request Request;
    protected PageCrawler Crawler;

    public Response MakeResponse(FitNesseContext context, Request request)
    {
        string pageName = GetPageNameOrDefault(request, "FrontPage");
        LoadPage(pageName, context);
        if (Page == null) return NotFoundResponse(context, request);
        return MakePageResponse(context);  
    }

    private string GetPageNameOrDefault(Request request, string defaultPageName)
    {
        string pageName = request.GetResource();
        if (string.IsNullOrEmpty(pageName)) pageName = defaultPageName;
        return pageName;   
    }

    protected void LoadPage(string resource, FitNesseContext context)
    {
        WikiPagePath path = PathParser.Parse(resource);
        Crawler = context.Root.GetPageCrawler();
        Crawler.SetDeadEndStrategy(new VirtualEnabledPageCrawler());
        Page = Crawler.GetPage(context.Root, path);
        if (Page != null) PageData = Page.GetData(); 
    }

    private Response NotFoundResponse(FitNesseContext context, Request request)
    {
        return new NotFoundResponse().MakeResponse(context, request);
    }

    private SimpleResponse MakePageResponse(FitNesseContext context)
    {
        PageTitle = PathParser.Render(Crawler.GetFullPath(Page));
        string html = MakeHtml(context);

        SimpleResponse response = new SimpleResponse();
        response.SetMaxAge(0);
        response.SetContent(html);
        return response;      
    }
    
    // ...
}
