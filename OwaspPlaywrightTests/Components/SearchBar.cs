using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

public class SearchBar() : ComponentBase("Search Bar", Test.Page.Locator("app-mat-search-bar")) { }
