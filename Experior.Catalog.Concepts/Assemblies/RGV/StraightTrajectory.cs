namespace Experior.Catalog.Concepts.Assemblies.RGV
{
    public class StraightTrajectory : ITrajectory
    {
        #region Constructor

        public StraightTrajectory(float length, float yaw)
        {
            Length = length;
            Yaw = yaw;
        }

        #endregion

        #region Public Properties

        public float Length { get; }

        public float Yaw { get; }

        #endregion

        #region Public Methods



        #endregion
    }
}
