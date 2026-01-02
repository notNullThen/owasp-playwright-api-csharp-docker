using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

public class SearchBar() : ComponentBase("Search Bar", Test.Page.Locator("app-mat-search-bar"))
{
    public InputFormField SearchInput => new(componentName: "Search", parent: Body);

    public async Task SearchAsync(string query)
    {
        await Test.StepAsync(
            $"Perform search with query: \"{query}\"",
            async () =>
            {
                var resultsText = Page.GetByText($"Search Results - {query}");

                await Body.ClickAsync();
                await SearchInput.FillAsync(query);
                await SearchInput.PressEnterAsync();
                await resultsText.WaitForAsync(new() { State = WaitForSelectorState.Visible });
            }
        );
    }
}
