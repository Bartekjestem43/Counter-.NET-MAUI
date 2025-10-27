using System.Collections.ObjectModel;

namespace Counter

{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Counter> counters = new();

        public MainPage()
        {
            InitializeComponent();
            LoadCounters();
            counterList.ItemsSource = counters;

        }

        async void LoadCounters()
        {
            var loaded = await CounterData.LoadAsync();
            foreach (var c in loaded)
                counters.Add(c);
        }

        async void SaveCounters() => await CounterData.SaveAsync(counters.ToList());

        void OnAddCounterClicked(object sender, EventArgs e)
        {
            string name = nameEntry.Text ?? "Counter";
            int.TryParse(startValueEntry.Text, out int startVal);
            string color = colorEntry.Text ?? "LightBlue";

            var newCounter = new Counter
            {
                Name = name,
                Value = startVal,
                StartValue = startVal,
                Color = color
            };

            counters.Add(newCounter);
            SaveCounters();

            nameEntry.Text = "";
            startValueEntry.Text = "";
            colorEntry.Text = "";
        }

        void OnPlusClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is Counter counter)
            {
                counter.Value++;
                SaveCounters();
                counterList.ItemsSource = null;
                counterList.ItemsSource = counters;
            }
        }

        void OnMinusClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is Counter counter)
            {
                counter.Value--;
                SaveCounters();
                counterList.ItemsSource = null;
                counterList.ItemsSource = counters;
            }
        }

        void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is Counter counter)
            {
                counters.Remove(counter);
                SaveCounters();
            }
        }

        void OnResetAllClicked(object sender, EventArgs e)
        {
            foreach (var c in counters)
                c.Value = c.StartValue;

            SaveCounters();
            counterList.ItemsSource = null;
            counterList.ItemsSource = counters;
        }
    }
}
