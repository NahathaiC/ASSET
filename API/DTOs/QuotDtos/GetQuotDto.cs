using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.QuotDtos
{
    public class GetQuotDto
    {
        public int Id { get; set; }
        public string Supplier { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public string Remark { get; set; }
    }
}