using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.Database;
using Backend.Models.View;
using Microsoft.EntityFrameworkCore;

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
            var comment = new CommentDatabaseModel
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

        public async Task Update(string commentId, string text)
        {
            _applicationContext.Comments.FindAsync(commentId).Result.Text = text;
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Delete(string commentId)
        {
            _applicationContext.Comments.Remove(_applicationContext.Comments.FindAsync(commentId).Result);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<List<CommentViewModel>> GetByProduct(string productId)
        {
            var commentDb = await _applicationContext.Comments.Where(w => w.ProductId == productId).ToListAsync();
            var commentView = new List<CommentViewModel>();
            foreach (var comment in commentDb)
            {
                commentView.Add(new CommentViewModel
                {
                    Id = comment.Id,
                    UserId = comment.UserId,
                    ProductId = comment.ProductId,
                    Text = comment.Text
                });
            }

            return commentView;
        }
    }
}