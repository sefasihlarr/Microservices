using AutoMapper;
using FreeCourse.Services.Catalog.Dtos.CategoryDtos;
using FreeCourse.Services.Catalog.Dtos.CourseDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    public class CourseService:ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMongoCollection<Course> _courseCollection, IMapper mapper, IDatabaseSettings databaseSettings, IMongoCollection<Category> categoryCollection)
        {
            var clinet = new MongoClient(databaseSettings.ConnnectionString);
            var database = clinet.GetDatabase(databaseSettings.DatabaseName);
            _mapper=mapper;
            _categoryCollection=database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        }


        public async Task<ResponseDto<List<CourseDto>>> GetAllAsycn()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category = await _categoryCollection.Find(x => x.Id == item.CategoryId).FirstOrDefaultAsync();
                }
            }

            else
            {
                courses = new List<Course>();
            }

            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);

        }

        public async Task<ResponseDto<CourseDto>> CreateAsync(Course course)
        {
            await _courseCollection.InsertOneAsync(course);
            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<ResponseDto<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();
            if (course==null)
            {
                return ResponseDto<CourseDto>.Fail("Category Not Found", 404);
            }

            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstOrDefaultAsync();

            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);


        }


        public async Task<ResponseDto<List<CourseDto>>> GetByUserId(string userId)
        {
            var courses = await _courseCollection.Find<Course>(x => x.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category = await _categoryCollection.Find(x => x.Id == item.CategoryId).FirstOrDefaultAsync();
                }
            }

            else
            {
                courses = new List<Course>();
            }

            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }


        public async Task<ResponseDto<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);
            if (result==null)
            {
                return ResponseDto<NoContent>.Fail("Course Not Found", 404);
            }

            return ResponseDto<NoContent>.Success(204);
        }


        public async Task<ResponseDto<NoContent>> DeleteAsycn(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount>0)
            {
                return ResponseDto<NoContent>.Success(204);
            }

            else
            {
                return ResponseDto<NoContent>.Fail("No Fail Delete operations",400);
            }
        }


    }
}
