using System;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.Database;

namespace Backend.Services
{
    public class CommentService
    {
        private readonly ApplicationContext _applicationContext;

        public CommentService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Create(string userId, string productId, string text)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid().ToString(),
                DateTimeCreate = DateTime.Now,
                Text = text,
                ProductId = productId,
                UserId = userId
            };
            await _applicationContext.Comments.AddAsync(comment);

            await _applicationContext.SaveChangesAsync();
        }

        public async Task Delete(string commentId)
        {
            _applicationContext.Comments.Remove(_applicationContext.Comments.FindAsync(commentId).Result);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Update(string commentId, string text)
        {
            _applicationContext.Comments.FindAsync(commentId).Result.Text = text;
            await _applicationContext.SaveChangesAsync();
        }
    }
}