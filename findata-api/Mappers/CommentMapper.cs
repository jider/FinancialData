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
            CreatedBy = comment.AppUser.UserName,
            CreatedOn = comment.CreatedOn,
            StockId = comment.StockId,
            Title = comment.Title
        };
    }

    public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto createCommentRequestDto, AppUser appUser, int stockId)
    {
        return new Comment
        {
            AppUserId = appUser.Id,            
            Content = createCommentRequestDto.Content,
            Title = createCommentRequestDto.Title,
            StockId = stockId
        };
    }

    public static Comment ToCommentFromUpdateDto(this UpdateCommentRequestDto updateCommentRequestDto)
    {
        return new Comment
        {
            Content = updateCommentRequestDto.Content,
            Title = updateCommentRequestDto.Title,
        };
    }
}
