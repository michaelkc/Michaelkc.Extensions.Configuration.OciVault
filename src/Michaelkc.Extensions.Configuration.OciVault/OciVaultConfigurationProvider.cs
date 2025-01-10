using System.Text;
using Microsoft.Extensions.Configuration;
using Oci.SecretsService;
using Oci.SecretsService.Models;
using Oci.SecretsService.Requests;
using Oci.VaultService;
using Oci.VaultService.Requests;

namespace Michaelkc.Extensions.Configuration.OciVault;

public class OciVaultConfigurationProvider : ConfigurationProvider
{
    private const int PageSize = 50;
    private readonly SecretsClient _secretsClient;
    private readonly VaultsClient _vaultsClient;
    private readonly OciVaultConfigurationOptions _vaultOptions;

    public OciVaultConfigurationProvider(SecretsClient secretsClient, VaultsClient vaultsClient, OciVaultConfigurationOptions vaultOptions)
    {
        _secretsClient = secretsClient;
        _vaultsClient = vaultsClient;
        _vaultOptions = vaultOptions;
    }
    
    public override void Load()
    {
        LoadAsync().GetAwaiter().GetResult();
    }
    
    private async Task LoadAsync()
    {
        var listSecretsRequest = new ListSecretsRequest
        {
            CompartmentId = _vaultOptions.CompartmentId,
            VaultId = _vaultOptions.VaultId,
            Limit=PageSize,
        };
        var data = new Dictionary<string, string?>();
        do
        {
            var secrets = await _vaultsClient.ListSecrets(listSecretsRequest).ConfigureAwait(false);

            foreach (var secret in secrets.Items)
            {
                var secretBundle = await _secretsClient.GetSecretBundleByName(new GetSecretBundleByNameRequest
                {
                    SecretName = secret.SecretName,
                    VaultId = _vaultOptions.VaultId
                });
                Base64SecretBundleContentDetails bundleContent = (Base64SecretBundleContentDetails)secretBundle.SecretBundle.SecretBundleContent;
                var secretValue = Encoding.UTF8.GetString(Convert.FromBase64String(bundleContent.Content));
                data.Add(secret.SecretName, secretValue);
            }
            listSecretsRequest.Page = secrets.OpcNextPage;
        }
        while (!string.IsNullOrWhiteSpace(listSecretsRequest.Page));

        Data = data;
    }
}