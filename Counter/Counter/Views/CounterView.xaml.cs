namespace Counter
{
    public partial class CounterView : ContentView
    {
        public CounterViewModel Model { get; private set; }
        public event EventHandler? Removed;

        public CounterView(CounterViewModel model)
        {
            InitializeComponent();
            Model = model;
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
        }

        private void DecreaseClicked(object sender, EventArgs e)
        {
            Model.Value--;
            UpdateUI();
        }

        private void ResetClicked(object sender, EventArgs e)
        {
            Model.Value = Model.InitialValue;
            UpdateUI();
        }

        private void RemoveClicked(object sender, EventArgs e)
        {
            Removed?.Invoke(this, EventArgs.Empty);
        }
    }
}