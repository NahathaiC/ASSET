using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.PRAggregate;

namespace API.DTOs.PODtos
{
    public class PODto
    {
        public string Creator { get; set; }
        public DateTime Bought_date { get; set; }
        public string ProdDesc { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
    }
}