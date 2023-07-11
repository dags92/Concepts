using Experior.Catalog.Concepts.Assemblies.RGV;
using Experior.Core.Assemblies;

namespace Experior.Catalog.Concepts
{
    internal class Common
    {
        public static Experior.Core.Resources.EmbeddedImageLoader EmbeddedImageLoader;
        public static Experior.Core.Resources.EmbeddedResourceLoader EmbeddedResourceLoader;
    }

    public class Create
    {
        public static Assembly RgvTrack(string title, string subtitle, object properties)
        {
            var info = new RgvTrackInfo()
            {
                name = Experior.Core.Assemblies.Assembly.GetValidName("Rgv Track ")
            };

            var assembly = new RgvTrack(info);
            return assembly;
        }

        public static Assembly Rgv(string title, string subtitle, object properties)
        {
            var info = new RgvInfo
            {
                name = Experior.Core.Assemblies.Assembly.GetValidName("Rgv")
            };

            var assembly = new Rgv(info);
            return assembly;
        }
    }
}