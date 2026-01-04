using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

public class ProductRow() : ComponentBase("Row", Test.Page.Locator("app-purchase-basket mat-row"))
{
    public ILocator Cells => Body.GetByRole(AriaRole.Cell);
    public ILocator ImageCell => Cells.Nth(0);
    public ILocator NameCell => Cells.Nth(1);
    public ILocator QuantityCell => Cells.Nth(2);
    public ILocator PriceCell => Cells.Nth(3);
    public ILocator DeleteCell => Cells.Nth(4);

    public async Task<ProductRow> GetByName(string productName)
    {
        var rowsCount = await CountAsync();
        for (int i = 0; i < rowsCount; i++)
        {
            var row = new ProductRow().GetByIndex(i);
            var actualProductName = await row.NameCell.InnerTextAsync();

            if (productName.Equals(actualProductName, StringComparison.InvariantCultureIgnoreCase))
            {
                return row;
            }
        }

        throw new Exception($"Product with name '{productName}' not found in basket");
    }

    public ProductRow GetByIndex(int index)
    {
        return new ProductRow() { Body = Body.Nth(index) };
    }
}
