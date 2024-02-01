namespace AutoLot.Models.Entities;

[Table("CreditRisks", Schema = "dbo")]
[Index("CustomerId", Name = "IX_CreditRisks_CustomerId")]
[EntityTypeConfiguration(typeof(CreditRiskConfiguration))]
public partial class CreditRisk : BaseEntity
{
    public int CustomerId { get; set; }

    // Person-owned property
    public Person PersonInformation { get; set; } = new Person();

    [ForeignKey("CustomerId")]
    [InverseProperty(nameof(Customer.CreditRisks))]
    public virtual Customer CustomerNavigation { get; set; }
}
