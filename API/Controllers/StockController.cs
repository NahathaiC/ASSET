using API.Data;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class StockController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        public StockController(StoreContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        {
            var stocks = await _context.Stocks
                .ToListAsync();
            
            return stocks;
        }
    }
}