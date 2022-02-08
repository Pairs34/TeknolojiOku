using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using TeknolojiOku.Entities;
using static TeknolojiOku.Helpers.Globals;

string site_url = "https://www.teknolojioku.com/sitemap.xml";

string sitemap_content = GetRemoteContent(site_url);

List<string> sitemap_links = ExtractLinks(sitemap_content);

List<string> news_links = new List<string>();

foreach (var sitemapLink in sitemap_links)
{
    string content = GetRemoteContent(sitemapLink);
    
    var extracted_links = ExtractLinks(content);

    if (extracted_links.Any())
    {
        news_links.AddRange(extracted_links);
    }
    
    break;
}

var doc = new HtmlDocument();

List<News> News = new List<News>();

foreach (var newsLink in news_links)
{
    string page_content = GetRemoteContent(newsLink);
    doc.LoadHtml(page_content);

    doc.DocumentNode.QuerySelector("");

    string title = doc.DocumentNode.QuerySelector("h1").InnerText;
    string news_detail = doc.DocumentNode.QuerySelector("article div.row").OuterHtml;
    
    News.Add(new News()
    {
        title = title,
        news_detail = news_detail,
        news_url = newsLink
    });

    Console.WriteLine(newsLink);
}

Console.WriteLine($"Haber Sayısı = {news_links}");
Console.ReadLine();