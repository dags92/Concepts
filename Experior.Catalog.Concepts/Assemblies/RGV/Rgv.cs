using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using Experior.Core.Assemblies;
using Experior.Core.Mathematics;
using Experior.Core.Parts;
using Experior.Rendering.Interfaces;

namespace Experior.Catalog.Concepts.Assemblies.RGV
{
    public class Rgv : Assembly
    {
        #region Fields

        private readonly RgvInfo _info;

        private readonly Experior.Core.Parts.Box _vehicle;

        private readonly Controller _controller;

        private bool _move;
        private readonly float _speed = 0.2f; // TODO: Change it later to use acceleration and deceleration...

        #endregion

        #region Constructor

        public Rgv(RgvInfo info) : base(info)
        {
            _info = info;

            _vehicle = new Box(Colors.Blue, 0.2f, 0.2f, 0.2f);
            Add(_vehicle);

            _controller = new Controller(_vehicle);

            //_trajectories.AddFirst(new StraightTrajectory(1f, 0f, Vector3.Zero));
            //_trajectories.AddLast(new CurveTrajectory(0.6f, 90f.ToRadians(), 0f, new Vector3(1f, 0f, 0f)));
            //_trajectories.AddLast(new StraightTrajectory(1f, 0f));
        }

        #endregion

        #region Public Properties

        public override string Category => "Concepts";

        public override ImageSource Image => Common.EmbeddedImageLoader.Get("Rgv");

        #endregion

        #region Public Methods

        public override void Step(float deltatime)
        {
            if (!_move)
            {
                return;
            }

            _controller.Step(deltatime, _speed);


            //if (!_move)
            //{
            //    return;
            //}

            //float tempDelta;
            //if (_currentNode.Value is StraightTrajectory currentStraight)
            //{
            //    tempDelta = deltatime * _speed;
            //    _delta = new Vector3(tempDelta, 0f, 0f);
            //}
            //else if (_currentNode.Value is CurveTrajectory currentCurve)
            //{
            //    var angularVel = _speed / currentCurve.Radius;
            //    tempDelta = deltatime * angularVel;
            //    _angleTraveled += tempDelta;

            //    _delta = new Vector3(currentCurve.Radius * (float)Math.Sin(_angleTraveled), 0f, currentCurve.Radius * (float)Math.Cos(_angleTraveled));
            //    _delta += new Vector3(0, 0, -currentCurve.Radius);
            //}
            //else
            //{
            //    return;
            //}

            //if (_distanceTraveled + delta >= _totalDistance)
            //{
            //    var d1 = _totalDistance - _distanceTraveled;

            //    if (_currentNode.Next != null)
            //    {
            //        var d2 = delta - d1;
            //        _currentNode = _currentNode.Next;

            //        if (_currentNode.Value is StraightTrajectory straight)
            //        {
            //            delta = d1 + d2;
            //        }
            //        else
            //        {
                        
            //        }

            //        _totalDistance += _currentNode.Value.Length;
            //    }
            //    else
            //    {
            //        delta = d1;
            //        _move = false;
            //    }
            //}
            //else
            //{
            //    _distanceTraveled += delta;
            //}

            //_vehicle.LocalPosition += _delta - _vehicle.LocalPosition;
            //_vehicle.LocalPosition += Vector3.Transform(_delta, Matrix4x4.CreateFromYawPitchRoll(_currentNode.Value.Yaw, 0f, 0f));
        }

        public override void KeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                _move = !_move;
            }
        }

        #endregion

        #region Private Methods



        #endregion
    }

    [Serializable, XmlInclude(typeof(RgvInfo)), XmlType(TypeName = "Experior.Catalog.Concepts.Assemblies.RgvInfo")]
    public class RgvInfo : Experior.Core.Assemblies.AssemblyInfo
    {
    }
}