using System.Collections.Generic;
using System.Numerics;
using Experior.Core.Mathematics;
using Experior.Core.Parts;
using static Experior.Catalog.Concepts.Assemblies.RGV.RgvTrack;

namespace Experior.Catalog.Concepts.Assemblies.RGV
{
    public class Controller
    {
        #region Fields

        private readonly Rgv _vehicle;

        private LinkedListNode<TrackModel> _currentTrajectory;
        private readonly LinkedList<TrackModel> _trajectories;

        private float _distanceTraveled;
        private float _totalDistance;
        private Vector3 _residual = Vector3.Zero;

        #endregion

        #region Constructor

        public Controller(Rgv vehicle, LinkedList<TrackModel> trajectories)
        {
            _vehicle = vehicle;
            _trajectories = trajectories;

            CalculateTotalDistance();
            _currentTrajectory = _trajectories.First;
        }

        #endregion

        #region Public Methods

        public void Step(float deltaTime, float currentVelocity)
        {
            Move(deltaTime, currentVelocity);
        }

        #endregion

        #region Private Methods

        private void CalculateTotalDistance()
        {
            _totalDistance = _trajectories.First.Value.Length;

            //foreach (var item in _trajectories)
            //{
            //    _totalDistance += item.Length;
            //}
        }

        private void Move(float deltaTime, float currentVelocity)
        {
            float tempDelta;
            if (!_currentTrajectory.Value.Curve)
            {
                tempDelta = deltaTime * currentVelocity;
                var displacement = Vector3.Transform(new Vector3(tempDelta, 0f, 0f), _currentTrajectory.Value.StartLocalOrientation * -1);

                // Straight Section:
                if ((_vehicle.LocalPosition + displacement).Length() > _totalDistance)
                {
                    var nextLp = _vehicle.LocalPosition + displacement;
                    _vehicle.LocalPosition += _currentTrajectory.Value.EndLocalPosition - _vehicle.LocalPosition;
                    _residual = nextLp - _vehicle.LocalPosition;

                    if (_currentTrajectory.Next != null)
                    {
                        _currentTrajectory = _currentTrajectory.Next;
                        _totalDistance += _currentTrajectory.Value.Length;
                        Move(deltaTime, currentVelocity);
                    }
                    else
                    {
                        _vehicle.MoveVehicle = false;
                    }
                }
                else
                {
                    _vehicle.LocalPosition += displacement - _residual;
                    _residual = Vector3.Zero;
                }
            }
            else
            {
                //var angularVelocity = currentVelocity * curve.Radius;
                //tempDelta = deltaTime * angularVelocity;
                //var theta = (float)Math.Atan2(_)
            }
        }

        #endregion
    }
}
