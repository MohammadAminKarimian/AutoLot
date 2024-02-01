namespace AutoLot.Models.Entities.Configuration
{
    public class MakeConfiguration : IEntityTypeConfiguration<Make>
    {
        public void Configure(EntityTypeBuilder<Make> builder)
        {
            builder.Property(e => e.TimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            builder.ToTable(
                m => m.IsTemporal(
                    t =>
                    {
                        t.HasPeriodEnd("ValidTo");
                        t.HasPeriodStart("ValidFrom");
                        t.UseHistoryTable("MakesAudit");
                    }));
        }
    }
}
