using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;

namespace Counter
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<CounterViewModel> counters = new();

        public MainPage()
        {
            InitializeComponent();

            ColorPicker.SelectedIndexChanged += ColorPicker_SelectedIndexChanged;

            counters = CounterStorage.Load();
            RenderCounters();
        }

        private void ColorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            RGBInputs.IsVisible = ColorPicker.SelectedItem?.ToString() == "Custom RGB";
        }

        private void RenderCounters()
        {
            CountersContainer.Children.Clear();

            foreach (var model in counters)
            {
                var view = new CounterView(model, counters); //przekazujemy referencję do listy counters
                view.Removed += (s, e) =>
                {
                    counters.Remove(model);
                    RenderCounters();
                    CounterStorage.Save(counters);
                };

                CountersContainer.Children.Add(view);
            }
        }

        private void AddCounter(object sender, EventArgs e)
        {
            int initialValue = int.TryParse(InitialValueEntry.Text, out int val) ? val : 0;
            var color = GetSelectedColor();

            var model = new CounterViewModel
            {
                Name = string.IsNullOrWhiteSpace(NameEntry.Text) ? $"Counter {counters.Count + 1}" : NameEntry.Text,
                InitialValue = initialValue,
                Value = initialValue,
                ColorR = (int)(color.Red * 255),
                ColorG = (int)(color.Green * 255),
                ColorB = (int)(color.Blue * 255)
            };

            counters.Add(model);
            CounterStorage.Save(counters);
            RenderCounters();

            NameEntry.Text = "";
            InitialValueEntry.Text = "";
            ColorPicker.SelectedIndex = -1;
            RGBInputs.IsVisible = false;
        }

        private Color GetSelectedColor()
        {
            string? selected = ColorPicker.SelectedItem?.ToString();

            if (selected == "Red") return Colors.DarkRed;
            if (selected == "Green") return Colors.DarkGreen;
            if (selected == "Blue") return Colors.DarkBlue;

            if (selected == "Custom RGB")
            {
                int r = int.TryParse(RedEntry.Text, out int rv) ? rv : 255;
                int g = int.TryParse(GreenEntry.Text, out int gv) ? gv : 255;
                int b = int.TryParse(BlueEntry.Text, out int bv) ? bv : 255;
                return Color.FromRgb(r, g, b);
            }

            return Colors.White;
        }

        private void ResetAll(object sender, EventArgs e)
        {
            foreach (var c in counters)
            {
                c.Value = c.InitialValue;
            }
            CounterStorage.Save(counters);
            RenderCounters();
        }
    }
}
