using System.Windows.Media;

namespace Experior.Catalog.Concepts
{
    public class MyCatalog : Experior.Core.Catalog
    {
        public MyCatalog()
            : base("Concepts")
        {
            Simulation = Experior.Core.Environment.Simulation.Physics;

            Common.EmbeddedResourceLoader = new Experior.Core.Resources.EmbeddedResourceLoader(System.Reflection.Assembly.GetExecutingAssembly());
            Common.EmbeddedImageLoader = new Experior.Core.Resources.EmbeddedImageLoader(System.Reflection.Assembly.GetExecutingAssembly());

            Add(Common.EmbeddedImageLoader.Get("Rgv"), "Rgv", "", Simulation, Create.Rgv);
        }

        public override ImageSource Logo => Common.EmbeddedImageLoader.Get("Logo");
    }
}