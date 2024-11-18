using ECommerce.DataAccess.Data;
using ECommerce.Entities.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; set; }
        public IProductRepository Product { get; set; }
        public IShoppingCartRepository ShoppingCart { get; set; }
        public IOrderHeaderRepository OrderHeader { get; set; }
        public IOrderDetailRepository OrderDetail { get; set; }
        public IApplicationUserRepository ApplicationUser { get; set; }


        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
            ShoppingCart = new ShoppingCartRepository(_context);
            OrderHeader = new OrderHeaderRepository(_context);
            OrderDetail = new OrderDetailRepository(_context);
            ApplicationUser = new ApplicationUserRepository(_context);

        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                // Save changes to the database and return the number of affected rows
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency exceptions, possibly by reloading entities or notifying users
                // Log the exception as necessary
                throw new Exception("A concurrency error occurred while saving changes. Please try again.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Handle general update exceptions related to tracking conflicts or database issues
                // Log the exception as necessary
                throw new Exception("An error occurred while updating the database. Please check your data.", ex);
            }
            catch (Exception ex)
            {
                // Handle any other exceptions that may occur
                // Log the exception as necessary
                throw new Exception("An unexpected error occurred while saving changes. Please try again.", ex);
            }
        }


        public async ValueTask DisposeAsync()
            => await _context.DisposeAsync();
    }
}
