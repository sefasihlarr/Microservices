using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Application.Dtos
{
    public class OrderItemDto
    {
        public string ProductId { get; private set; }
        public string Name { get; private set; }
        public string ImageUrl { get; private set; }
        public decimal Price { get; private set; }
    }
}
