using Oci.Common;
using Oci.Common.Auth;
using Oci.SecretsService;
using Oci.VaultService;

namespace Michaelkc.Extensions.Configuration.OciVault;

public static class OciClientFactory
{
    public static VaultsClient CreateVaultsClientFromConfig(OciVaultConfigurationOptions config, ClientConfiguration clientConfiguration = null, string endpoint = null)
    {
        var provider = new SimpleAuthenticationDetailsProvider
        {
            UserId = config.UserId,
            Fingerprint = config.Fingerprint,
            PrivateKeySupplier = new PrivateKeySupplier(config.PrivateKeyPem),
            TenantId = config.CompartmentId,
            Region = Oci.Common.Region.FromRegionCodeOrId(config.Region)
        };
        return new VaultsClient(provider, clientConfiguration, endpoint);
    }

    public static SecretsClient CreateSecretsClientFromConfig(OciVaultConfigurationOptions config, ClientConfiguration clientConfiguration = null, string endpoint = null)
    {
        var provider = new SimpleAuthenticationDetailsProvider
        {
            UserId = config.UserId,
            Fingerprint = config.Fingerprint,
            PrivateKeySupplier = new PrivateKeySupplier(config.PrivateKeyPem),
            TenantId = config.CompartmentId,
            Region = Oci.Common.Region.FromRegionCodeOrId(config.Region)
        };
        return new SecretsClient(provider, clientConfiguration, endpoint);
    }
}