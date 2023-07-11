using System;
using System.Collections.Generic;
using Experior.Core.Mathematics;
using Experior.Core.Parts;
using static Experior.Catalog.Concepts.Assemblies.RGV.RgvTrack;

namespace Experior.Catalog.Concepts.Assemblies.RGV
{
    public class Controller
    {
        #region Fields

        private readonly Experior.Core.Parts.Box _vehicle;

        private LinkedListNode<TrackModel> _currentTrajectory;
        private readonly LinkedList<TrackModel> _trajectories = new LinkedList<TrackModel>();

        private float _totalDistance;

        #endregion

        #region Constructor

        public Controller(Box vehicle)
        {
            _vehicle = vehicle;

            CalculateTotalDistance();
            _currentTrajectory = _trajectories.First;
        }

        #endregion

        #region Public Methods

        public void Step(float deltaTime, float currentVelocity)
        {
            float tempDelta;
            if (!_currentTrajectory.Value.Curve)
            {
                tempDelta = deltaTime * currentVelocity;

                var diff = _currentTrajectory.Value.End.LocalPosition - _vehicle.LocalPosition;
                var tempOrien = Trigonometry.Direction(_vehicle.LocalPosition, _currentTrajectory.Value.End.LocalPosition);

            }
            else
            {
                //var angularVelocity = currentVelocity * curve.Radius;
                //tempDelta = deltaTime * angularVelocity;
                //var theta = (float)Math.Atan2(_)
            }
        }

        #endregion

        #region Private Methods

        private void CalculateTotalDistance()
        {
            _totalDistance = 0f;

            foreach (var item in _trajectories)
            {
                _totalDistance += item.Length;
            }
        }

        #endregion
    }
}
