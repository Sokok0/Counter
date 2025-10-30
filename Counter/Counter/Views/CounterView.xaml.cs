using System.Collections.ObjectModel; //potrzebne do listy counters

namespace Counter
{
    public partial class CounterView : ContentView
    {
        public CounterViewModel Model { get; private set; }
        public event EventHandler? Removed;

        private ObservableCollection<CounterViewModel> countersRef; //referencja do listy liczników

        public CounterView(CounterViewModel model, ObservableCollection<CounterViewModel> counters) //nowy konstruktor
        {
            InitializeComponent();
            Model = model;
            countersRef = counters; //zapisujemy referencję
            UpdateUI();
        }

        private void UpdateUI()
        {
            TitleLabel.Text = Model.Name;
            NumberLabel.Text = $"Value: {Model.Value}";
            FrameContainer.BackgroundColor = Model.Color;
        }

        private void IncreaseClicked(object sender, EventArgs e)
        {
            Model.Value++;
            UpdateUI();
            CounterStorage.Save(countersRef); //zapis po zmianie
        }

        private void DecreaseClicked(object sender, EventArgs e)
        {
            Model.Value--;
            UpdateUI();
            CounterStorage.Save(countersRef); //zapis po zmianie
        }

        private void ResetClicked(object sender, EventArgs e)
        {
            Model.Value = Model.InitialValue;
            UpdateUI();
            CounterStorage.Save(countersRef); //zapis po zmianie
        }

        private async void RenameClicked(object sender, EventArgs e) //nowa metoda Rename
        {
            string result = await Application.Current.MainPage.DisplayPromptAsync(
                "Rename Counter",
                "Enter new name:",
                initialValue: Model.Name,
                maxLength: 50,
                keyboard: Keyboard.Text);

            if (!string.IsNullOrWhiteSpace(result))
            {
                Model.Name = result.Trim();
                UpdateUI();
                CounterStorage.Save(countersRef); //zapis po zmianie nazwy
            }
        }

        private void RemoveClicked(object sender, EventArgs e)
        {
            Removed?.Invoke(this, EventArgs.Empty);
        }
    }
}
