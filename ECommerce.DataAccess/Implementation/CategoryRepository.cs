using AutoMapper;
using ECommerce.DataAccess.Data;
using ECommerce.Entities.Repositories;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Implementation
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext context, IMapper? mapper = null) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Update(Category category)
        {
            var categoryInDB = _context.Categories.FirstOrDefault(c => c.Id == category.Id);

            if (categoryInDB is not null)
            {
                categoryInDB = _mapper.Map<Category>(category);
                categoryInDB.CreatedTime = DateTime.Now;
            }
        }
    }
}
