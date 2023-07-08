using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using Experior.Core.Assemblies;
using Experior.Core.Mathematics;
using Experior.Core.Parts;

namespace Experior.Catalog.Concepts.Assemblies.RGV
{
    public class Rgv : Assembly
    {
        #region Fields

        private readonly RgvInfo _info;

        private readonly Experior.Core.Parts.Box _vehicle;

        private bool _move;
        private float _distanceTraveled;
        private readonly float _speed = 0.2f; // TODO: Change it later to use acceleration and deceleration...

        private float _totalDistance;
        private LinkedListNode<ITrajectory> _currentNode;
        private readonly LinkedList<ITrajectory> _trajectories = new LinkedList<ITrajectory>();

        #endregion

        #region Constructor

        public Rgv(RgvInfo info) : base(info)
        {
            _info = info;

            _trajectories.AddFirst(new StraightTrajectory(1f, 90f.ToRadians()));
            _trajectories.AddLast(new StraightTrajectory(1f, 90f.ToRadians()));
            _trajectories.AddLast(new StraightTrajectory(1f, 90f.ToRadians()));

            _currentNode = _trajectories.First;
            _totalDistance = _trajectories.First.Value.Length;

            _vehicle = new Box(Colors.Blue, 0.2f, 0.2f, 0.2f);
            Add(_vehicle);
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

            var delta = deltatime * _speed;

            if (_distanceTraveled + delta >= _totalDistance)
            {
                var d1 = _totalDistance - _distanceTraveled;
                var d2 = delta - d1;

                if (_currentNode.Next != null)
                {
                    delta = d1 + d2;
                    _currentNode = _currentNode.Next;
                    _totalDistance += _currentNode.Value.Length;
                }
                else
                {
                    delta = d1;
                    _move = false;
                }
            }
            else
            {
                _distanceTraveled += delta;
            }

            _vehicle.LocalPosition += Vector3.Transform(new Vector3(delta, 0f, 0f), Matrix4x4.CreateFromYawPitchRoll(_currentNode.Value.Yaw, 0f, 0f));
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