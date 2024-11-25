using Microsoft.Extensions.Configuration;
using Oci.VaultService;

namespace Michaelkc.Extensions.Configuration.OciVault;

public class OciVaultConfigurationSource : IConfigurationSource
{
    private readonly VaultsClient _vaultsClient;
    private readonly OciVaultConfigurationOptions _vaultOptions;

    public OciVaultConfigurationSource(VaultsClient vaultsClient, OciVaultConfigurationOptions vaultOptions)
    {
        _vaultsClient = vaultsClient;
        _vaultOptions = vaultOptions;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new OciVaultConfigurationProvider(_vaultsClient, _vaultOptions);
}
