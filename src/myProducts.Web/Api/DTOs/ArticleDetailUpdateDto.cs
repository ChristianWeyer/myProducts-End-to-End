﻿namespace MyProducts.Web.Api.DTOs
{
    public class ArticleDetailUpdateDto : DtoBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
