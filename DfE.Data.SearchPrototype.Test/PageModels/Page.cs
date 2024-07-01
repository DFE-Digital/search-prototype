using AngleSharp.Dom;

namespace DfE.Data.SearchPrototype.Test.PageModels;

public abstract class Page
{
    protected IDocument _DOM;

    protected Page(IDocument pageResponse)
    {
        _DOM = pageResponse;
    }

    public string Title {
        get => _DOM.GetElementsByTagName("h1").Single().InnerHtml;
    }

    public PageHeader Header {
        get => new PageHeader(_DOM.GetElementsByTagName("header").Single());
    }
}
