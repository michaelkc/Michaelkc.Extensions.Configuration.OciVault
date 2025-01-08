using Microsoft.Extensions.Configuration;
using Oci.SecretsService;
using Oci.VaultService;

namespace Michaelkc.Extensions.Configuration.OciVault;

public class OciVaultConfigurationSource : IConfigurationSource
{
    private readonly SecretsClient _secretsClient;
    private readonly VaultsClient _vaultsClient;
    private readonly OciVaultConfigurationOptions _vaultOptions;

    public OciVaultConfigurationSource(SecretsClient secretsClient, VaultsClient vaultsClient, OciVaultConfigurationOptions vaultOptions)
    {
        _secretsClient = secretsClient;
        _vaultsClient = vaultsClient;
        _vaultOptions = vaultOptions;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new OciVaultConfigurationProvider(_secretsClient, _vaultsClient, _vaultOptions);
}
