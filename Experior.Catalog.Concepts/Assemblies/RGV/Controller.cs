using System;
using Experior.Core.Parts.Lines;
using System.Collections.Generic;
using System.Numerics;
using Experior.Core.Mathematics;
using Experior.Core.Parts;
using Experior.Rendering.Interfaces;

namespace Experior.Catalog.Concepts.Assemblies.RGV
{
    public class Controller
    {
        #region Fields

        private readonly Experior.Core.Parts.Box _vehicle;

        private LinkedListNode<ITrajectory> _currentTrajectory;
        private readonly LinkedList<ITrajectory> _trajectories = new LinkedList<ITrajectory>();

        private float _totalDistance;

        #endregion

        #region Constructor

        public Controller(Box vehicle)
        {
            _vehicle = vehicle;

            AddTrajectory(new StraightTrajectory(1f, 0f));
            AddTrajectory(new StraightTrajectory(2f, 0f));

            CalculateTotalDistance();
            CalculateLocalPoints();

            _currentTrajectory = _trajectories.First;
        }

        #endregion

        #region Public Methods

        public void AddTrajectory(ITrajectory trajectory)
        {
            _trajectories.AddLast(trajectory);
        }

        public void Step(float deltaTime, float currentVelocity)
        {
            //float tempDelta;
            //if (_currentTrajectory.Value is StraightTrajectory straight)
            //{
            //    tempDelta = deltaTime * currentVelocity;
            //}
            //else if (_currentTrajectory.Value is CurveTrajectory curve)
            //{
            //    var angularVelocity = currentVelocity * curve.Radius;
            //    tempDelta = deltaTime * angularVelocity;
            //    var theta = (float)Math.Atan2(_) 
            //}
            //else
            //{
            //    return;
            //}


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

        private void CalculateLocalPoints()
        {
            var start = Vector3.Zero;
            var totalYaw = 0f;

            foreach (var item in _trajectories)
            {
                if (!item.Yaw.IsEffectivelyEqual(totalYaw))
                {
                    totalYaw += item.Yaw;
                }

                var orientation = Matrix4x4.CreateFromYawPitchRoll(item.Yaw, 0f, 0f);

                Vector3 localPosition;
                Vector3 end;
                switch (item)
                {
                    case StraightTrajectory straight:

                        end = start + Vector3.Transform(new Vector3(item.Length, 0f, 0f), orientation); // TODO: TEST IT !
                        localPosition = (start + end) / 2f;

                        break;
                    
                    case CurveTrajectory curve:

                        var radiusPoint = new Vector3(0, 0, curve.Radius);
                        var sign = curve.Revolution == Revolution.Clockwise ? -1 : 1;
                        radiusPoint *= sign;
                        var localRadiusPoint = start + Vector3.Transform(radiusPoint, orientation);

                        end = localRadiusPoint + Vector3.Transform(radiusPoint, Matrix4x4.CreateFromYawPitchRoll(curve.Angle, 0f, 0f));
                        localPosition = localRadiusPoint;
                        break;

                    default:

                        return;
                }

                item.StartPoint = start;
                item.EndPoint = end;
                item.LocalPosition = localPosition;

                start = end;

                Log.Write($"SP = {item.StartPoint}");
                Log.Write($"EP = {item.EndPoint}");
                Log.Write($"LP = {item.LocalPosition}");
            }
        }

        #endregion
    }
}
