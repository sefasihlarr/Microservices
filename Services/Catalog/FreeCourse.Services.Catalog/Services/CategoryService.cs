using AutoMapper;
using FreeCourse.Services.Catalog.Dtos.CategoryDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
         
            var newconnectionString = "mongodb://localhost:27017";
            var databasename = "CatalogDb";
            var client = new MongoClient(newconnectionString);
            var database = client.GetDatabase(databasename);
            var CatogoriesCollectionname = "category";
            _mapper = mapper;
            _categoryCollection = database.GetCollection<Category>(CatogoriesCollectionname);
        }


        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsycn()
        {
            var cagories = await _categoryCollection.Find(category => true).ToListAsync();
            return ResponseDto<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(cagories), 200);

        }

        public async Task<ResponseDto<CategoryDto>> CreateAsync(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();
            if (category==null)
            {
                return ResponseDto<CategoryDto>.Fail("Category Not Found", 404);
            }

            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);


        }




    }


}
