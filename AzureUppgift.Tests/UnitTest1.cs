using AzureUppgift.Api;

namespace AzureUppgift.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void AddVat_ShouldAdd25Percent()
        {
            // Skapar en PriceCalculator
            var calculator = new PriceCalculator();

            // Testar med priset 100 kr
            var result = calculator.AddVat(100m);

            // Kontrollerar att priset blir 125 kr
            Assert.Equal(125m, result);
        }
    }
}