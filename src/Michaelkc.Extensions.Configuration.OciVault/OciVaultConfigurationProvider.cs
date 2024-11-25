using Microsoft.Extensions.Configuration;
using Oci.VaultService;

namespace Michaelkc.Extensions.Configuration.OciVault;

public class OciVaultConfigurationProvider : ConfigurationProvider
{
    private readonly VaultsClient _vaultsClient;
    private readonly OciVaultConfigurationOptions _vaultOptions;

    public OciVaultConfigurationProvider(VaultsClient vaultsClient, OciVaultConfigurationOptions vaultOptions)
    {
        _vaultsClient = vaultsClient;
        _vaultOptions = vaultOptions;
    }

    public override void Load()
    {
        // TODO: See code in AzureKeyVaultConfigurationProvider.cs for how to handle async load code
        var secrets = new Dictionary<string, string>();
        throw new NotImplementedException();
        //var request = new ListSecretsRequest
        //{
        //    VaultId = _vaultOptions.VaultId
        //};

        //var response = _vaultsClient.ListSecrets(request);

        //foreach (var secretSummary in response.Items)
        //{
        //    var secretRequest = new GetSecretBundleRequest
        //    {
        //        SecretId = secretSummary.Id
        //    };

        //    var secretResponse = _vaultsClient.GetSecretBundle(secretRequest);
        //    var secretValue = secretResponse.SecretBundle.SecretBundleContent.Content;

        //    secrets[secretSummary.SecretName] = secretValue;
        //}

        //Data = secrets;
    }

}