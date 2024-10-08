﻿using System.ComponentModel.DataAnnotations;

namespace findata_api.DTOs.Comment;

public class CreateCommentRequestDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Title must be at least of 5 characters")]
    [MaxLength(200, ErrorMessage = "Title cannot be over 200 characters")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(5, ErrorMessage = "Content must be at least of 5 characters")]
    [MaxLength(200, ErrorMessage = "Content cannot be over 200 characters")]
    public string Content { get; set; } = string.Empty;
}
