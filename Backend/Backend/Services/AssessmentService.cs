using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class AssessmentService
    {
        private readonly ApplicationContext _applicationContext;
        public AssessmentService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Create(string userId, string productId, byte value)
        {
            await _applicationContext.Assessments.AddAsync(new AssessmentDatabaseModel
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                ProductId = productId,
                Value = value
            });

            await _applicationContext.SaveChangesAsync();
        }

        public async Task Update(string userId, string productId, byte value)
        {
            _applicationContext.Assessments.FirstOrDefaultAsync(w => w.ProductId == productId && w.UserId == userId)
                .Result.Value = value;

            await _applicationContext.SaveChangesAsync();
        }

        public async Task<string> AverageValue(string productId)
        {
            var value = await _applicationContext.Assessments.Where(w => w.ProductId == productId).AverageAsync(s => s.Value);
            return Math.Round(value, 1).ToString(CultureInfo.InvariantCulture);
        }
    }
}