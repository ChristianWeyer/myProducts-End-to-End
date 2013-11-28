using System;

namespace MyProducts.Framework.ModelMetadata
{
    public class MetadataConventionsAttribute : Attribute
    {
        public Type ResourceType { get; set; }
    }
}