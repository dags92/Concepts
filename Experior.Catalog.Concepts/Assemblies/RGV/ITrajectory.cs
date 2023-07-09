using System.Numerics;

namespace Experior.Catalog.Concepts.Assemblies.RGV
{
    public interface ITrajectory
    {
        float Length { get; }

        float Yaw { get; }

        Vector3 StartPoint { get; set; }

        Vector3 EndPoint { get; set; }

        Vector3 LocalPosition { get; set; }
    }
}
