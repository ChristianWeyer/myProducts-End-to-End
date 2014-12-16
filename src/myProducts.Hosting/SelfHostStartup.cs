using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace MyProducts.Hosting
{
    public class SelfHostStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var clientOptions = new FileServerOptions
            {
                RequestPath = new PathString(""),
                FileSystem = new PhysicalFileSystem(@"client"),
                EnableDefaultFiles = true
            };
            clientOptions.DefaultFilesOptions.DefaultFileNames.Add("index.html");
            clientOptions.StaticFileOptions.ServeUnknownFileTypes = true;

            var imagesOptions = new FileServerOptions
            {
                RequestPath = new PathString("/images"),
                FileSystem = new PhysicalFileSystem("images"),
            };

            app.UseFileServer(clientOptions);
            app.UseFileServer(imagesOptions);

            var startup = new Startup();
            startup.Configuration(app);
        }
    }
}