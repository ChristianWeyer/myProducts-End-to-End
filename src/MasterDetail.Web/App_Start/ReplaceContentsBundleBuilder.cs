using System.Collections.Generic;
using System.Web.Optimization;

namespace MasterDetail.Web.App_Start
{
    public class ReplaceContentsBundleBuilder : IBundleBuilder
    {
        private readonly string find;
        private readonly string replaceWith;
        private readonly IBundleBuilder builder;

        public ReplaceContentsBundleBuilder(string find, string replaceWith)
            : this(find, replaceWith, new DefaultBundleBuilder())
        {
        }

        public ReplaceContentsBundleBuilder(string find, string replaceWith, IBundleBuilder builder)
        {
            this.find = find;
            this.replaceWith = replaceWith;
            this.builder = builder;
        }

        public string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            string contents = builder.BuildBundleContent(bundle, context, files);

            return contents.Replace(find, replaceWith);
        }
    }
}