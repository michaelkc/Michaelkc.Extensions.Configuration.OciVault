using Michaelkc.Extensions.Configuration.OciVault;
using Oci.SecretsService;
using Oci.VaultService;

var builder = WebApplication.CreateBuilder(args);

var vaultOptions = new OciVaultConfigurationOptions
{
    CompartmentId = string.Empty,
    Fingerprint = string.Empty,
    PrivateKeyPem = string.Empty,
    Region = string.Empty,
    UserId = string.Empty,
    VaultId = string.Empty,
};

builder.Configuration.GetSection("OciVaultConfigurationOptions").Bind(vaultOptions);

VaultsClient vaultsClient = OciClientFactory.CreateVaultsClientFromConfig(vaultOptions);
SecretsClient secretsClient = OciClientFactory.CreateSecretsClientFromConfig(vaultOptions);
builder.Configuration.AddOciVault(vaultsClient, secretsClient, vaultOptions);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
