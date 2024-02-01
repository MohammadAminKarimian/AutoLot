namespace AutoLot.Models.Entities.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasIndex(cr => new { cr.CustomerId, cr.CarId }).IsUnique(true);

            builder.HasQueryFilter(e => e.CarNavigation!.IsDrivable);

            builder.Property(e => e.TimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            builder.HasOne(o => o.CarNavigation)
                .WithMany(c => c.Orders)
                //.OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Orders_Inventory");

            builder.HasOne(o => o.CustomerNavigation)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Orders_Customers");

            builder.ToTable(b => b.IsTemporal(t =>
            {
                t.HasPeriodEnd("ValidTo");
                t.HasPeriodStart("ValidFrom");
                t.UseHistoryTable("OrdersAudit");
            }));
        }
    }
}
