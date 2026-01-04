using OwaspPlaywrightTests.Components;

namespace OwaspPlaywrightTests.Pages;

public class HomePage() : PageBase("/#")
{
    public ProductTile ProductTiles => new();
}
