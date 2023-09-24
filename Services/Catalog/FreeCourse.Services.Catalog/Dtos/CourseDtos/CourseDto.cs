using FreeCourse.Services.Catalog.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using FreeCourse.Services.Catalog.Dtos.CategoryDtos;
using FreeCourse.Services.Catalog.Dtos.FeatureDtos;

namespace FreeCourse.Services.Catalog.Dtos.CourseDtos
{
    public class CourseDto
    {

        public string Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }

        public DateTime CreatedDate { get; set; }

        public FeatureDto Feature { get; set; }


        public string CategoryId { get; set; }


        public CategoryDto Category { get; set; }
    }
}
