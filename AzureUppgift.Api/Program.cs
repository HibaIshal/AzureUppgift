using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Lägger till tjänster
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Kopplar appen till Key Vault
var secretClient = new SecretClient(
    new Uri("https://azureuppgift-hiba-kv.vault.azure.net/"),
    new DefaultAzureCredential());

// Använder Swagger när appen körs lokalt
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Enkel lista med produkter
var products = new[]
{
    new { Id = 1, Name = "Laptop" },
    new { Id = 2, Name = "Tangentbord" },
    new { Id = 3, Name = "Mus" },
    new { Id = 4, Name = "Skärm" }

};

app.MapGet("/api/products", () => products);

// Testar att appen kan hämta hemligheten
app.MapGet("/api/secret", async () =>
{
    await secretClient.GetSecretAsync("apikey");

    // Skriver i loggen att det fungerade
    app.Logger.LogInformation("Hemligheten hämtades från Key Vault");

    // Visar inte själva hemligheten
    return Results.Ok(new
    {
        message = "Hemligheten kunde hämtas från Key Vault"
    });
});

app.Run();