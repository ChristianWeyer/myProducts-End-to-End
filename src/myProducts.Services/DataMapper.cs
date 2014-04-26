using AutoMapper;
using MyProducts.Model;
using MyProducts.Services.DTOs;
using Thinktecture.Applications.Framework;

namespace MyProducts.Services
{
    public class DataMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Article, ArticleDetailDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => Constants.ImagesFolder + "/" + src.ImageUrl))
                .IgnoreAllNonExisting();
            Mapper.CreateMap<Article, ArticleDto>().IgnoreAllNonExisting();
            Mapper.CreateMap<ArticleDetailUpdateDto, Article>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .IgnoreAllNonExisting();
            Mapper.CreateMap<Category, CategoryDto>().IgnoreAllNonExisting();
            Mapper.CreateMap<CategoryDto, Category>().IgnoreAllNonExisting();

            Mapper.AssertConfigurationIsValid();
        }
    }

    public static class DataMapper
    {
        public static ArticleDetailDto MapDetails(this Article entity)
        {
            return Mapper.Map<ArticleDetailDto>(entity);
        }

        public static ArticleDto Map(this Article entity)
        {
            return Mapper.Map<ArticleDto>(entity);
        }

        public static Article Map(this ArticleDetailUpdateDto dto)
        {
            return Mapper.Map<Article>(dto);
        }

        public static CategoryDto Map(this Category category)
        {
            return Mapper.Map<CategoryDto>(category);
        }
    }
}
