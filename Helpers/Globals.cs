using System.Net;
using System.Text.RegularExpressions;
using RestSharp;

namespace TeknolojiOku.Helpers;

public static class Globals
{
    public static string GetRemoteContent(string url)
    {
        var client = new RestClient(url);
        client.Timeout = -1;
        client.FollowRedirects = false;
        var request = new RestRequest(Method.GET);
        IRestResponse response = client.Execute(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return response.Content;
        }
        else
        {
            return String.Empty;
        }
    }

    public static List<string> ExtractLinks(string content)
    {
        MatchCollection regex = new Regex("(<loc>)(.*?)(<\\/loc>)").Matches(content);

        List<string> links = new List<string>(regex.Count);

        for (int i = 0; i < regex.Count; i++)
        {
            links.Add(regex[i].Groups[2].Value);
        }

        return links;
    }
}