using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using Experior.Core.Assemblies;
using Experior.Core.Parts;

namespace Experior.Catalog.Concepts.Assemblies.RGV
{
    public class Rgv : Assembly
    {
        #region Fields

        private readonly RgvInfo _info;

        private Controller _controller;
        private string _trackName = "";
        private RgvTrack _track;

        private readonly float _speed = 0.2f; // TODO: Change it later to use acceleration and deceleration...

        #endregion

        #region Constructor

        public Rgv(RgvInfo info) : base(info)
        {
            _info = info;

            var vehicle = new Box(Colors.Blue, 0.2f, 0.2f, 0.2f);
            Add(vehicle);
        }

        #endregion

        #region Public Properties

        [Browsable(false)]
        public bool MoveVehicle { get; set; }

        [Category("Track")]
        public string TrackName
        {
            get => _trackName;
            set
            {
                var track = Items.Get(value);
                if (!(track is RgvTrack rgvTrack))
                {
                    return;
                }

                _trackName = value;
                _track = rgvTrack;
                _track.Add(this);
                _controller = new Controller(this, _track.Tracks);

                Position = rgvTrack.Position;
            }
        }

        public override string Category => "Concepts";

        public override ImageSource Image => Common.EmbeddedImageLoader.Get("Rgv");

        #endregion

        #region Public Methods

        public override void Step(float deltatime)
        {
            if (!MoveVehicle)
            {
                return;
            }

            _controller.Step(deltatime, _speed);
        }

        #endregion
    }

    [Serializable, XmlInclude(typeof(RgvInfo)), XmlType(TypeName = "Experior.Catalog.Concepts.Assemblies.RgvInfo")]
    public class RgvInfo : Experior.Core.Assemblies.AssemblyInfo
    {
    }
}