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
            VaultId = _vaultOptions.VaultId
        };
        var secrets = await _vaultsClient.ListSecrets(listSecretsRequest).ConfigureAwait(false);


        var getSecretRequest = new GetSecretRequest{
            SecretId = secrets.Items.First().Id,
        };  
        var secret = await _vaultsClient.GetSecret(getSecretRequest).ConfigureAwait(false);
        var currentVersion = secret.Secret.CurrentVersionNumber;

        var getSecretVersionRequest = new GetSecretVersionRequest{
            SecretId = secrets.Items.First().Id,
            SecretVersionNumber = currentVersion,
        };
        var secretContents = await _vaultsClient.GetSecretVersion(getSecretVersionRequest).ConfigureAwait(false);
        var secretBundle = await _secretsClient.GetSecretBundleByName(new GetSecretBundleByNameRequest{
            SecretName = "mysecret1",
            VaultId = _vaultOptions.VaultId
        });
        Base64SecretBundleContentDetails bundleContent = (Base64SecretBundleContentDetails)secretBundle.SecretBundle.SecretBundleContent;
        var secretValue = Encoding.UTF8.GetString(Convert.FromBase64String(bundleContent.Content));
        throw new NotImplementedException();    
    }
}