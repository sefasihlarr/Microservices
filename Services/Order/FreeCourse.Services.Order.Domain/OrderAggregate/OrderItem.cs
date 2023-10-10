using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class OrderItem:Entity
    {
        public OrderItem(string productId, string name, string ımageUrl, decimal price)
        {
            ProductId=productId;
            Name=name;
            ImageUrl=ımageUrl;
            Price=price;
        }

        public string ProductId { get;private set; }
        public string Name { get; private set; }
        public string ImageUrl { get; private set; }
        public decimal Price { get; private set; }


        public void UpdaterItem(string name, string imageUrl, decimal price)
        {
            Name = name;
            ImageUrl = imageUrl;
            Price = price;
       
        }
    }
}
