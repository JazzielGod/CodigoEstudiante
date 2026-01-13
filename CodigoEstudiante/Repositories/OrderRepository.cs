using CodigoEstudiante.Context;
using CodigoEstudiante.Entities;
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
    }
}
