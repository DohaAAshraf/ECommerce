using ECommerce.DataAccess.Data;
using ECommerce.Entities.Models;
using ECommerce.Entities.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Implementation
{
    public class OrderHeaderRepository : GenericRepository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderHeader orderHeader)
        {
            // Check if the entity is already being tracked in the local context
            var existingOrder = _context.OrderHeaders.Local
                .FirstOrDefault(entry => entry.Id == orderHeader.Id);

            if (existingOrder != null)
            {
                // Detach the existing tracked entity to avoid conflicts
                _context.Entry(existingOrder).State = EntityState.Detached;
            }

            // Attach the new entity and mark it for an update
            _context.OrderHeaders.Update(orderHeader);
            _context.SaveChanges();
        }


        public async Task UpdateStatus(int id, string? orderStatus, string? paymentStatus)
        {
            var orderFromDB = await _context.OrderHeaders.FirstOrDefaultAsync(x => x.Id == id);

            if (orderFromDB is not null)
            {
                // Update the order status if provided
                if (!string.IsNullOrEmpty(orderStatus))
                {
                    orderFromDB.OrderStatus = orderStatus;
                }

                // Set the payment date to now if payment status is updated
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDB.PaymentStatus = paymentStatus;
                    orderFromDB.PaymentDate = DateTime.Now;
                }

                // Save the changes back to the database
                await _context.SaveChangesAsync();
            }
        }

    }
}
