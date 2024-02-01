using AutoLot.Dal.EfStructures;

namespace AutoLot.Dal.Repos;

public class RadioRepo : TemporalTableBaseRepo<Radio>, IRadioRepo
{
    // Used by ASP.NET Core and it's dependancy injection.
    public RadioRepo(ApplicationDbContext context) : base(context) { }

    // Used by WPF applications.
    internal RadioRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options)) { }
}
