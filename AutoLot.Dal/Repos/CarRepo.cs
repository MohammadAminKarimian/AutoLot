using AutoLot.Dal.EfStructures;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AutoLot.Dal.Repos;

public class CarRepo : TemporalTableBaseRepo<Car>, ICarRepo
{
    // Used by ASP.NET Core and it's dependancy injection.
    public CarRepo(ApplicationDbContext context) : base(context) { }

    // Used by WPF applications.
    internal CarRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options)) { }

    internal IOrderedQueryable<Car> BuildBaseQuery()
        => Table.Include(c => c.MakeNavigation).OrderBy(c => c.PetName);

    public override IEnumerable<Car> GetAll() => BuildBaseQuery();

    public override IEnumerable<Car> GetAllIgnoreQueryFilters() => BuildBaseQuery().IgnoreQueryFilters();

    public override Car Find(int? id) => BuildBaseQuery().IgnoreQueryFilters().Where(c => c.Id == id).FirstOrDefault();

    public IEnumerable<Car> GetAllBy(int makeId) => BuildBaseQuery().Where(c => c.MakeId == makeId);

    public string GetPetName(int id)
    {
        var parameterId = new SqlParameter
        {
            ParameterName = "@carId",
            SqlDbType = SqlDbType.Int,
            Value = id,
        };

        var parameterName = new SqlParameter
        {
            ParameterName = "@petName",
            SqlDbType = SqlDbType.NVarChar,
            Size = 50,
            Direction = ParameterDirection.Output
        };
        ExecuteParameterizedQuery("EXEC [dbo].[GetPetName] @carId, @petName OUTPUT", new[] { parameterId, parameterName });
        return (string)parameterName.Value;
    }
}
