using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;
using System.Text.Json;

namespace FreeCourse.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService=redisService;
        }

        public async Task<ResponseDto<bool>> Delete(string userId)
        {
            var sataus = await _redisService.GetDb().KeyDeleteAsync(userId);
            return sataus ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("not found ", 404);

        }

        public async Task<ResponseDto<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(existBasket))
            {
                return ResponseDto<BasketDto>.Fail("Basket Not Found", 404);
            }

            return ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket),200);

        }



        public async Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto)
        {
          
                var serializedBasket = JsonSerializer.Serialize(basketDto);
                var sataus  = await _redisService.GetDb().StringSetAsync(basketDto.UserId, serializedBasket);


            return sataus ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket coluld not update or save", 500);
               
          
          
        }
    }
}
