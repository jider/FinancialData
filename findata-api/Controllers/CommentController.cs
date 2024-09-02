using findata_api.DTOs.Comment;
using findata_api.interfaces;
using findata_api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace findata_api.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController(ICommentRepository commentRepository, IStockRepository stockRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await commentRepository.GetAllAsync();
        var commentDtos = comments.Select(comment => comment.ToCommentDto()).ToList();

        return Ok(commentDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var comment = await commentRepository.GetByIdAsync(id);

        if (comment == null) return NotFound();

        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto createCommentRequestDto)
    {
        if (!await stockRepository.ExistAsync(stockId)) return BadRequest("Stock does not exist");

        var createdComment = await commentRepository.CreateAsync(createCommentRequestDto.ToCommentFromCreateDto(stockId));

        return CreatedAtAction(nameof(GetById), new { id = createdComment.Id }, createdComment.ToCommentDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var comment = await commentRepository.DeleteAsync(id);

        return comment is null
            ? NotFound()
            : NoContent();
    }
}
