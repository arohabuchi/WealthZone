using Microsoft.EntityFrameworkCore;
using WealthZone.Data.Interface;
using WealthZone.Dto.Stock;
using WealthZone.Models;

namespace WealthZone.Data.Repository
{
    public class CommentRepoServices :ICommentRepo
    {
        private readonly AppDbContext context;
        public CommentRepoServices(AppDbContext _context)
        {
            this.context = _context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await this.context.comments.AddAsync(commentModel);
            await this.context.SaveChangesAsync();
            return commentModel;
        }


        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await context.comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null)
            {
                return null;
            }
            context.comments.Remove(commentModel);
            await context.SaveChangesAsync();
            return commentModel; // ?? null;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var AllComment = await context.comments.Include(a=>a.AppUser).ToListAsync();
            return AllComment;
        }

       public async Task<Comment?> GetByIdAsync(int id)
        {
            var commentDetails = await context.comments.Include(a => a.AppUser).FirstOrDefaultAsync(c=>c.Id==id);
            if (commentDetails == null)
            {
                return null;
            }
            return commentDetails;
        }

     
        public async Task<Comment?> updateAync(int id, Comment comment)
        {
             var existingModel = await context.comments.FindAsync(id);
            if (existingModel == null)
            {
                return null;
            }
            existingModel.Title = comment.Title;
            existingModel.Content = comment.Content;

            await context.SaveChangesAsync();
            return existingModel;

        }

       
    }
}