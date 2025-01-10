namespace Michaelkc.Extensions.Configuration.OciVault;

public class OciVaultConfigurationOptions
{
    public required string UserId { get; set; }             // E.g. ocid1.user.oc1..aaaa<more chars>
    public required string Fingerprint { get; set; }        // E.g. de:ad:be:ef:00:00:00:00:00:00:00:00:00:00:00:00
    public required string PrivateKeyPem { get; set; }      // E.g. -----BEGIN PRIVATE KEY-----<more chars>. Must be associated with user in OCI, cannot handle single-line PEM without headers AFAICT
    public required string CompartmentId { get; set; }      // E.g. ocid1.tenancy.oc1..aaaa<more chars>
    public required string Region { get; set; }             // E.g. EU_FRANKFURT_1
    public required string VaultId { get; set; }            // E.g. ocid1.vault.oc1.eu-frankfurt-1.entt<more chars>
}
