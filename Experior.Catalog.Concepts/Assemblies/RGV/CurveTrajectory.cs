using System.Numerics;
using Experior.Rendering.Interfaces;

namespace Experior.Catalog.Concepts.Assemblies.RGV
{
    public class CurveTrajectory : ITrajectory
    {
        public CurveTrajectory(float radius, float angle, float yaw, Revolution revolution)
        {
            Radius = radius;
            Angle = angle; // radians
            Yaw = yaw;
            Revolution = revolution;

            Length = angle * radius;
        }

        public float Length { get; }

        public float Radius { get; }

        public float Angle { get; }

        public float Yaw { get; }

        public Revolution Revolution { get; }

        public Vector3 StartPoint { get; set; }

        public Vector3 EndPoint { get; set; }

        public Vector3 LocalPosition { get; set; }
    }
}
