using findata_api.DTOs.Comment;
using findata_api.Models;

namespace findata_api.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn,
            StockId = comment.StockId,
            Title = comment.Title
        };
    }

    public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto createCommentRequestDto, int stockId)
    {
        return new Comment
        {
            Content = createCommentRequestDto.Content,
            Title = createCommentRequestDto.Title,
            StockId = stockId
        };
    }
}
