using System;

namespace Thinktecture.Applications.Framework.WebApi.ModelMetadata
{
    public class MetadataConventionsAttribute : Attribute
    {
        public Type ResourceType { get; set; }
    }
}