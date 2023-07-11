using System;
using System.Collections.Generic;
using System.Numerics;
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

            _section1 = new Straight(new StraightInfo { Length = 1f, Width = 0.5f });
            _section3 = new Straight(new StraightInfo { Length = 1f, Width = 0.5f });

            _section2 = new Curve(new CurveInfo {radius = 0.5f, Width = 0.5f, angle = 180f.ToRadians(), revolution = Revolution.Clockwise});
            _section4 = new Curve(new CurveInfo { radius = 0.5f, Width = 0.5f, angle = 180f.ToRadians(), revolution = Revolution.Clockwise });

            Add(_section1);
            Add(_section2);
            Add(_section3);
            Add(_section4);

            Refresh();

            Tracks.Add(new TrackModel(false, _section1.StartFixPoint, _section1.EndFixPoint, _section1.Length, 0f, 0f));
            Tracks.Add(new TrackModel(true, _section2.StartFixPoint, _section2.EndFixPoint, _section2.Radius * _section2.Angle, _section2.Radius, _section2.Angle));
            Tracks.Add(new TrackModel(false, _section3.StartFixPoint, _section3.EndFixPoint, _section3.Length, 0f, 0f));
            Tracks.Add(new TrackModel(true, _section4.StartFixPoint, _section4.EndFixPoint, _section4.Radius * _section4.Angle, _section4.Radius, _section4.Angle));
        }

        #endregion

        #region Public Properties

        public override string Category => "RGV Track";

        public override ImageSource Image { get; }

        public List<TrackModel> Tracks { get; } = new List<TrackModel>();

        #endregion

        #region Public Methods

        public override void Refresh()
        {
            if (_info == null)
            {
                return;
            }

            _section1.LocalPosition = new Vector3(_section1.Length / 2, 0f, 0f);
            _section2.LocalPosition = _section1.LocalPosition + new Vector3(_section1.Length / 2, 0, -_section2.Radius);
            _section3.LocalPosition = _section2.LocalPosition + new Vector3(-_section3.Length / 2, 0, -_section2.Radius);
            _section4.LocalPosition = new Vector3(0, 0, -_section4.Radius);
            
            _section1.LocalYaw = 180f.ToRadians();
            _section4.LocalYaw = 180f.ToRadians();
        }

        #endregion

        #region Private Methods



        #endregion

        #region Nested Types

        public class TrackModel
        {
            public TrackModel(bool curve, FixPoint start, FixPoint end, float length, float radius, float angle)
            {
                Start = start;
                End = end;
                Length = length;
                Radius = radius;
                Angle = angle;
                Curve = curve;
            }

            public bool Curve { get; }

            public FixPoint Start { get; }

            public FixPoint End { get; }

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
