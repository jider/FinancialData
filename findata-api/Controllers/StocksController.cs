using findata_api.Data;
using findata_api.DTOs.Stock;
using findata_api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace findata_api.Controllers;

[Route("api/stock")]
[ApiController]
public class StocksController(ApplicationDBContext dbContext) : ControllerBase
{
    private readonly ApplicationDBContext _dbContext = dbContext;

    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks = _dbContext.Stocks.ToList()
            .Select(stock => stock.ToStockDto());
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult Get([FromRoute] int id)
    {
        var stock = _dbContext.Stocks.Find(id);

        return stock == null ? NotFound() : Ok(stock.ToStockDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto createStockRequestDto)
    {
        var stock = createStockRequestDto.ToStockFromCreateDto();
        _dbContext.Stocks.Add(stock);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(Get), new { id = stock.Id }, stock.ToStockDto());
    }
}
