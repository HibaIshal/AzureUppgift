namespace AzureUppgift.Api
{
    public class PriceCalculator
    {
        public decimal AddVat(decimal price)
        {
            return price * 1.25m; // Kontrollerar att 100 kr blir 125 kr med moms.
        }
    }
}