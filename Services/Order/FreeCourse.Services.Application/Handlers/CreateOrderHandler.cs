using FreeCourse.Services.Application.Commend;
using FreeCourse.Services.Application.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Services.Order.Insfrastructure;
using FreeCourse.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand,ResponseDto< CreatedOrderDto>>
    {
        private readonly OrderDbContext _dbContext;

        public CreateOrderHandler(OrderDbContext dbContext)
        {
            _dbContext=dbContext;
        }



        public async Task<ResponseDto<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.AddressDto.Provice, request.AddressDto.District, request.AddressDto.Street, request.AddressDto.ZipCode, request.AddressDto.Line);

            FreeCourse.Services.Order.Domain.OrderAggregate.Order newOrder = new Order.Domain.OrderAggregate.Order(request.BuyerId, newAddress);

            foreach (var item in request.OrderItems)
            {
                newOrder.AddOrderItem(item.ProductId,item.Name, item.Price, item.ImageUrl);
            }

            await _dbContext.Orders.AddAsync(newOrder);

            var result = await _dbContext.SaveChangesAsync();


            return ResponseDto<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id },200);
        }
    }
}
