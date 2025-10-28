using System.Xml.Serialization;
using Microsoft.Maui.Graphics;

namespace Counter
{
    public class CounterViewModel
    {
        [XmlAttribute]
        public string Name { get; set; } = "";

        [XmlAttribute]
        public int Value { get; set; }

        [XmlAttribute]
        public int InitialValue { get; set; }

        [XmlAttribute]
        public int ColorR { get; set; }

        [XmlAttribute]
        public int ColorG { get; set; }

        [XmlAttribute]
        public int ColorB { get; set; }

        [XmlIgnore]
        public Color Color => Color.FromRgb(ColorR, ColorG, ColorB);
    }
}