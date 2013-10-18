using System;

namespace MasterDetail.Web.Api.DTOs
{
    public class ArticleDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        // ... and so on ...
    }
}
