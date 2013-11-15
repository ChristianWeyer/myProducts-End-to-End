namespace MyProducts.Web.Api.DTOs
{
    public class ArticleDetailDto : DtoBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        // ... and so on ...
    }
}
