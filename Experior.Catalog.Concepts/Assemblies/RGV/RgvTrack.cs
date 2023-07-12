using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using Experior.Catalog.Xcelgo.Track.Assemblies;
using Experior.Core.Assemblies;
using Experior.Core.Mathematics;
using Experior.Core.Parts;
using Experior.Rendering.Interfaces;

namespace Experior.Catalog.Concepts.Assemblies.RGV
{
    public class RgvTrack : Assembly
    {
        #region Fields

        private readonly RgvTrackInfo _info;

        private readonly Straight _section1, _section3;
        private readonly Curve _section2, _section4;

        #endregion

        #region Constructor

        public RgvTrack(RgvTrackInfo info) : base(info)
        {
            _info = info;

            _section1 = new Straight(new StraightInfo { Length = 1f, Width = 0.5f, visible = true });
            _section3 = new Straight(new StraightInfo { Length = 1f, Width = 0.5f, visible = true });

            //_section2 = new Curve(new CurveInfo {radius = 0.5f, Width = 0.5f, angle = 180f.ToRadians(), revolution = Revolution.Clockwise});
            //_section4 = new Curve(new CurveInfo { radius = 0.5f, Width = 0.5f, angle = 180f.ToRadians(), revolution = Revolution.Clockwise });

            Add(_section1);
            //Add(_section2);
            Add(_section3);
            //Add(_section4);

            Refresh();
            AddTracks();
        }

        #endregion

        #region Public Properties

        public override string Category => "RGV Track";

        public override ImageSource Image { get; }

        public LinkedList<TrackModel> Tracks { get; } = new LinkedList<TrackModel>();

        #endregion

        #region Public Methods

        public override void Refresh()
        {
            if (_info == null)
            {
                return;
            }

            //_section1.LocalPosition = new Vector3(_section1.Length / 2, 0f, 0f);
            //_section2.LocalPosition = _section1.LocalPosition + new Vector3(_section1.Length / 2, 0, -_section2.Radius);
            //_section3.LocalPosition = _section2.LocalPosition + new Vector3(-_section3.Length / 2, 0, -_section2.Radius);
            //_section4.LocalPosition = new Vector3(0, 0, -_section4.Radius);

            _section1.LocalPosition = new Vector3(_section1.Length / 2, 0f, 0f);
            _section3.LocalPosition = _section1.LocalPosition + new Vector3(_section1.Length / 2 + _section3.Length / 2, 0f, 0f);

            _section1.LocalYaw = 180f.ToRadians();
            _section3.LocalYaw = 180f.ToRadians();

            //_section1.LocalYaw = 180f.ToRadians();
            //_section4.LocalYaw = 180f.ToRadians();
        }

        public override void KeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                foreach (var assembly in Assemblies)
                {
                    if (assembly is Rgv vehicle)
                    {
                        vehicle.MoveVehicle = !vehicle.MoveVehicle;
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private void AddTracks()
        {
            Tracks.AddLast(new TrackModel(false, _section1.StartFixPoint, _section1.EndFixPoint, _section1.Length, 0f, 0f, _section1.LocalOrientation));
            //Tracks.AddLast(new TrackModel(true, _section2.StartFixPoint, _section2.EndFixPoint, _section2.Radius * _section2.Angle, _section2.Radius, _section2.Angle, _section2.LocalOrientation));
            Tracks.AddLast(new TrackModel(false, _section3.StartFixPoint, _section3.EndFixPoint, _section3.Length, 0f, 0f, _section3.LocalOrientation));
            //Tracks.AddLast(new TrackModel(true, _section4.StartFixPoint, _section4.EndFixPoint, _section4.Radius * _section4.Angle, _section4.Radius, _section4.Angle, _section4.LocalOrientation));

            ProcessTrackInfo();
        }

        private void ProcessTrackInfo()
        {
            foreach (var model in Tracks)
            {
                ConvertToLocalPosition(model);
            }
        }

        private void ConvertToLocalPosition(TrackModel model)
        {
            Trigonometry.GlobalToLocal(Position, Orientation, model.Start.Position, model.Start.Orientation, out var startVec, out var startOri);
            model.StartLocalPosition = startVec;
            model.StartLocalOrientation = startOri;

            Trigonometry.GlobalToLocal(Position, Orientation, model.End.Position, model.End.Orientation, out var endVec, out var endOri);
            model.EndLocalPosition = endVec;
            model.EndLocalOrientation = endOri;
        }

        #endregion

        #region Nested Types

        public class TrackModel
        {
            public TrackModel(bool curve, FixPoint start, FixPoint end, float length, float radius, float angle, Matrix4x4 localOrientation)
            {
                Start = start;
                End = end;
                Length = length;
                Radius = radius;
                Angle = angle;
                LocalOrientation = localOrientation;
                Curve = curve;
            }

            public bool Curve { get; }

            public FixPoint Start { get; }

            public FixPoint End { get; }

            public Vector3 StartLocalPosition { get; set; }

            public Vector3 EndLocalPosition { get; set; }

            public Matrix4x4 StartLocalOrientation { get; set; }

            public Matrix4x4 EndLocalOrientation { get; set; }

            public Matrix4x4 LocalOrientation { get; }

            public float Length { get; }

            public float Radius { get; }

            public float Angle { get; }
        }

        #endregion
    }

    [Serializable, XmlInclude(typeof(RgvTrackInfo)), XmlType(TypeName = "Experior.Catalog.Concepts.Assemblies.RgvTrackInfo")]
    public class RgvTrackInfo : AssemblyInfo
    {

    }
}
