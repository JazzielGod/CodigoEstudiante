using CodigoEstudiante.Context;
using CodigoEstudiante.Entities;
using Microsoft.EntityFrameworkCore;
namespace CodigoEstudiante.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        private readonly AppDbContext _dbContext;
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task AddAsync(Order order)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            
            try
            {
                foreach(var detail in order.OrdenItems)
                {
                    var product = await _dbContext.Product.FindAsync(detail.ProductId);
                    product.stock -= detail.Quantity;

                }           

                await _dbContext.Order.AddAsync(order);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAllWithDetailAsync(int userId)
        {
            var orders = await _dbContext.Order
                .Where(x => x.UserId == userId)
                .Include(x => x.OrdenItems)
                .ThenInclude(x => x.Product).ToListAsync();
            return orders;
        }

    }
}
