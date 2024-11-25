using Microsoft.Extensions.Configuration;
using Oci.Common.Auth;
using Oci.VaultService;

namespace Michaelkc.Extensions.Configuration.OciVault;

public static class OciVaultConfigurationExtensions
{
    public static IConfigurationBuilder AddOciVault(
        this IConfigurationBuilder configurationBuilder,
        VaultsClient vaultsClient, 
        OciVaultConfigurationOptions vaultOptions)
    {
        configurationBuilder
        .Add(new OciVaultConfigurationSource(vaultsClient, vaultOptions));
        return configurationBuilder;
    }
}
