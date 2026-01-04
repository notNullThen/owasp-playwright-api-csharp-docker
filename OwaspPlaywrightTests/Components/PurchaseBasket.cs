using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

public class PurchaseBasket()
    : ComponentBase("Purchase Basket", Test.Page.Locator("app-purchase-basket"))
{
    public ILocator Product => Body.Locator("mat-row");
}
