using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

public class SearchBar()
    : ComponentBase("Search Bar", TestContext.Page.Locator("app-mat-search-bar")) { }
