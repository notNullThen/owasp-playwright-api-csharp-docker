namespace OwaspPlaywrightTests.Data;

public sealed record Product(string Name, string Description, float Price, bool SoldOut);

public static class ProductsData
{
    public static readonly Product RaspberryJuice = new(
        Name: "Raspberry Juice (1000ml)",
        Description: "Made from blended Raspberry Pi, water and sugar.",
        Price: 4.99f,
        SoldOut: false
    );
}
