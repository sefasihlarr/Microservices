using FreeCourse.Services.Application.Commend;
using FreeCourse.Services.Application.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Services.Order.Insfrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreatedOrderDto>
    {
        private readonly OrderDbContext _dbContext;

        public CreateOrderHandler(OrderDbContext dbContext)
        {
            _dbContext=dbContext;
        }



        public Task<CreatedOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.AddressDto.Provice, request.AddressDto.District, request.AddressDto.Street, request.AddressDto.ZipCode, request.AddressDto.Line);

            FreeCourse.Services.Order.Domain.OrderAggregate.Order newOrder = new Order.Domain.OrderAggregate.Order(request.BuyerId, newAddress);

            newOrder.OrderItems.foreach (x =>
            {

                newOrder.AddOrderItem(x.ProductId, x.Name, x.Price, x.ImageUrl);



            }) ;
        }
    }
}
