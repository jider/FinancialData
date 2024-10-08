﻿using findata_api.DTOs.Comment;
using findata_api.Models;

namespace findata_api.interfaces;

public interface ICommentRepository
{
    Task<Comment> CreateAsync(Comment comment);

    Task<Comment?> DeleteAsync(int id);

    Task<List<Comment>> GetAllAsync();

    Task<Comment?> GetByIdAsync(int id);

    Task<Comment?> UpdateAsync(int id, Comment commentModel);
}
