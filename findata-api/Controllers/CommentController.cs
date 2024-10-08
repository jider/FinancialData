﻿using findata_api.DTOs.Comment;
using findata_api.Extensions;
using findata_api.interfaces;
using findata_api.Mappers;
using findata_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace findata_api.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController(
    ICommentRepository commentRepository,
    IFMPService fmpService,
    IStockRepository stockRepository,
    UserManager<AppUser> userManager) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await commentRepository.GetAllAsync();
        var commentDtos = comments.Select(comment => comment.ToCommentDto()).ToList();

        return Ok(commentDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var comment = await commentRepository.GetByIdAsync(id);

        if (comment == null) return NotFound();

        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{symbol:alpha}")]
    public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentRequestDto createCommentRequestDto)
    {
        var stock = await stockRepository.GetBySymbolAsync(symbol);
        if (stock is null)
        {
            stock = await fmpService.FindStockBySymbolAsync(symbol);
            if (stock is null) return BadRequest("Stock does not exists");

            await stockRepository.CreateAsync(stock);
        }

        var appUser = await userManager.FindByNameAsync(User.GetUserName());
        var createdComment = await commentRepository.CreateAsync(createCommentRequestDto.ToCommentFromCreateDto(appUser, stock.Id));

        return CreatedAtAction(nameof(GetById), new { id = createdComment.Id }, createdComment.ToCommentDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
    {
        var updatedComment = await commentRepository.UpdateAsync(id, updateCommentRequestDto.ToCommentFromUpdateDto());

        return updatedComment == null
            ? NotFound()
            : Ok(updatedComment.ToCommentDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var comment = await commentRepository.DeleteAsync(id);

        return comment is null
            ? NotFound()
            : NoContent();
    }
}
