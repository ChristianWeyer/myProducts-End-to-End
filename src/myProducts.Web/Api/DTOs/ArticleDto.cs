using System;

namespace MasterDetail.Web.Api.DTOs
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
