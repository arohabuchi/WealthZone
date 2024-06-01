using WealthZone.Dto.Stock;
using WealthZone.Models;

namespace WealthZone.Data.Interface
{
    public interface ICommentRepo
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> updateAync (int id, Comment comment);
        Task<Comment?> DeleteAsync(int id);
    }
}
