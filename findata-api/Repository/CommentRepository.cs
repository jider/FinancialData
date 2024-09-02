using findata_api.Data;
using findata_api.interfaces;
using findata_api.Models;
using Microsoft.EntityFrameworkCore;

namespace findata_api.Repository;

public class CommentRepository(ApplicationDBContext dBContext) : ICommentRepository
{

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await dBContext.AddAsync(comment);
        await dBContext.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await dBContext.Comments.FindAsync(id);

        if (comment is null) return null;

        dBContext.Comments.Remove(comment);
        await dBContext.SaveChangesAsync();

        return comment;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await dBContext.Comments.AsNoTracking().ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await dBContext.Comments.FindAsync(id);
    }
}
