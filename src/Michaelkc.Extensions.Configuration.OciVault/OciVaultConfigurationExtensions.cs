using Microsoft.Extensions.Configuration;
using Oci.Common.Auth;
using Oci.SecretsService;
using Oci.VaultService;

namespace Michaelkc.Extensions.Configuration.OciVault;

public static class OciVaultConfigurationExtensions
{
    public static IConfigurationBuilder AddOciVault(
        this IConfigurationBuilder configurationBuilder,
        VaultsClient vaultsClient, 
        SecretsClient secretsClient,
        OciVaultConfigurationOptions vaultOptions)
    {
        configurationBuilder
        .Add(new OciVaultConfigurationSource(secretsClient, vaultsClient, vaultOptions));
        return configurationBuilder;
    }
}
