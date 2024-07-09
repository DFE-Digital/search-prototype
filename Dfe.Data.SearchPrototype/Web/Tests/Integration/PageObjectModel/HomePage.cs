﻿using AngleSharp.Html.Dom;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using OpenQA.Selenium;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel
{
    public sealed class HomePage : DocumentObjectModelExtractor
    {
        private readonly PageHeader _pageHeader;

        private const string PageName = "Home";
        private const string PrivacyLink = "Privacy";
        private const string MainHeadingClass = "govuk-header__link govuk-header__service-name";

        public By Heading => By.CssSelector("header div div:nth-of-type(2) a");

        public HomePage(WebApplicationFactory<Program> webApplicationFactory) :
            base(webApplicationFactory, PageName)
        {
            _pageHeader = PageHeader.Create(DocumentObjectModel);
        }

        public string GetHomePageHeading() =>
            _pageHeader.GetMainHeading(MainHeadingClass);

        public IHtmlAnchorElement GetHomePagePrivacyLink() =>
            _pageHeader.GetHeaderLink(PrivacyLink);

        public static HomePage Create(
            WebApplicationFactory<Program> webApplicationFactory) => new(webApplicationFactory);
    }
}