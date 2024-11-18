using AutoMapper;
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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper = null) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Update(Product product)
        {
           var productFromDB = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

            if(productFromDB is not null)
            {
                productFromDB.Name = product.Name;
                productFromDB.Description = product.Description;
                productFromDB.Price = product.Price;
                productFromDB.Img = product.Img;
                productFromDB.CategoryId = product.CategoryId;

            }
        }
    }
}
