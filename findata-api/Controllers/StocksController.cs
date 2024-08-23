using Microsoft.AspNetCore.Mvc;
using findata_api.DTOs.Stock;
using findata_api.interfaces;
using findata_api.Mappers;

namespace findata_api.Controllers;

[Route("api/stock")]
[ApiController]
public class StocksController(IStockRepository stockRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await stockRepository.GetAllAsync();
        var stocksDto = stocks.Select(stock => stock.ToStockDto());

        return Ok(stocksDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var stock = await stockRepository.GetAsync(id);

        return stock is null
            ? NotFound()
            : Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createStockRequestDto)
    {
        var stock = createStockRequestDto.ToStockFromCreateDto();
        var createdStock = await stockRepository.CreateAsync(stock);

        return CreatedAtAction(nameof(Get), new { id = createdStock.Id }, createdStock.ToStockDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
    {
        var stock = await stockRepository.UpdateAsync(id, updateStockRequestDto);

        return stock is null
            ? NotFound()
            : Ok(stock.ToStockDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await stockRepository.DeleteAsync(id);

        return stock is null
            ? NotFound()
            : NoContent();
    }
}
