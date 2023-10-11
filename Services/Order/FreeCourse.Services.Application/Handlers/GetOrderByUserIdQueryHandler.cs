using FreeCourse.Services.Application.Dtos;
using FreeCourse.Services.Application.Mapping;
using FreeCourse.Services.Application.Queries;
using FreeCourse.Services.Order.Insfrastructure;
using FreeCourse.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Application.Handlers
{
    public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrdersUserIdQuery, ResponseDto<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrderByUserIdQueryHandler(OrderDbContext context)
        {
            _context=context;
        }

        public async Task<ResponseDto<List<OrderDto>>> Handle(GetOrdersUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include (x=>x.OrderItems).Where(x=>x.BuyerId == request.UserId).ToListAsync();

            if (!orders.Any())
            {
                return ResponseDto<List<OrderDto>>.Success(new List<OrderDto>(),200);
            }


            var orderDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
            return ResponseDto<List<OrderDto>>.Success(orderDto, 200);
        }
    }
}
