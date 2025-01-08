namespace Michaelkc.Extensions.Configuration.OciVault;

public class OciVaultConfigurationOptions
{
    public required string VaultId { get; set; }            // E.g. ocid1.vault.oc1.eu-frankfurt-1.entt<more chars>
    public required string CompartmentId { get; set; }
}
