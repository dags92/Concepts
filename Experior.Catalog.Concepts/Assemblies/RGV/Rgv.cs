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
        private string _trackName = "";
        private RgvTrack _track;

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
        }

        #endregion

        #region Public Properties

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

                Position = rgvTrack.Position;
            }
        }

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
        }

        public override void KeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                _move = !_move;
            }
        }

        #endregion
    }

    [Serializable, XmlInclude(typeof(RgvInfo)), XmlType(TypeName = "Experior.Catalog.Concepts.Assemblies.RgvInfo")]
    public class RgvInfo : Experior.Core.Assemblies.AssemblyInfo
    {
    }
}