﻿namespace AutoLot.Api.Controllers;

public class CarsController : BaseCrudController<Car, CarsController>
{
    protected readonly ICarRepo CarRepo; 
    public CarsController(ICarRepo repo, IAppLogging<CarsController> logger) : base(repo, logger) 
    {
        CarRepo = repo;
    }

    /// <summary>
    /// Gets all cars by make
    /// </summary>
    /// <returns>All cars for a make</returns>
    /// <param name="id">Primary key of the make</param>
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was successful")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [ServiceFilter(typeof(LoggingResponseHeaderFilter))]
    [HttpGet("bymake/{id?}")]
    [ApiVersion("1.0")]
    public ActionResult<IEnumerable<Car>> GetCarsByMake(int? id)
    {
        if (id.HasValue && id.Value > 0)
        {
            return Ok(CarRepo.GetAllBy(id.Value));
        }
        return Ok(CarRepo.GetAllIgnoreQueryFilters());
    }
}
