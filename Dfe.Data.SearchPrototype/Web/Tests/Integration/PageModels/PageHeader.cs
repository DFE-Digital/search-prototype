using AngleSharp.Dom;

namespace DfE.Data.SearchPrototype.Test.PageModels;

public class PageHeader : HtmlElement
{
    public PageHeader(IElement element) : base(element)
    {
    }

    public IEnumerable<Link> Links { get => _element.GetElementsByTagName("a").Select(x => new Link(x)); }

    public Link PrivacyLink => Links.Single(link => link.TextContent == "Privacy");
}
