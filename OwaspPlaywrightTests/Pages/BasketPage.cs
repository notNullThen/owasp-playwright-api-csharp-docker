using OwaspPlaywrightTests.Components;

namespace OwaspPlaywrightTests.Pages;

public class BasketPage() : PageBase("#/basket")
{
    public ProductRow Products => new();
}
