using Dapper;
using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration=configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<ResponseDto<NoContent>> Delete(int id)
        {
            var status  = await _dbConnection.ExecuteAsync("delete from discount where id=@Id",new {Id  = id});
            if (status<0)
            {
                return ResponseDto<NoContent>.Fail("hata verildi", 200);
            }

            return ResponseDto<NoContent>.Success(204);
        }

        public async Task<ResponseDto<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");
            return ResponseDto<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<ResponseDto<Models.Discount>> GetByCodeAnduserId(string code, string userId)
        {
            var dicount = await _dbConnection.QueryAsync<Models.Discount>("select * from dicount where userid=@UserId and code=@Code", new { UserId = userId, Code = code });

            var hasDiscount = dicount.FirstOrDefault();

            if (hasDiscount == null)
            {
                return ResponseDto<Models.Discount>.Fail("Found discount", 404);
            }

            return ResponseDto<Models.Discount>.Success(hasDiscount, 204);
        }

        public async Task<ResponseDto<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select *from discount where id@Id",new { Id=id })).SingleOrDefault();

            if (discount==null)
            {
                return ResponseDto<Models.Discount>.Fail("not found",404);
            }

            return ResponseDto<Models.Discount>.Success(discount, 200);
        }

        public async Task<ResponseDto<NoContent>> Save(Models.Discount discount)
        {
            var Savestatus = await _dbConnection.ExecuteAsync("INSERT INTO discount(userid,rate,code) VALUES(@UserId,@Rate,@Code)",discount);
            if (Savestatus>0)
            {
                return ResponseDto<NoContent>.Success(204);
            }

            return ResponseDto<NoContent>.Fail("Bir hata meydana geldi", 500);
        }

        public async Task<ResponseDto<NoContent>> Update(Models.Discount discount)
        {
            var status  = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,code=@Code,rate=@Rate where id=@Id",
                new {Id=discount.Id,UserId = discount.UserId,Code = discount.Code,Rate= discount.Rate});

            if (status>0)
            {
                return ResponseDto<NoContent>.Success(204);
            }

            return ResponseDto<NoContent>.Fail("Update işlemli gerçekleştirlilemedi",404);
        }
    }
}
