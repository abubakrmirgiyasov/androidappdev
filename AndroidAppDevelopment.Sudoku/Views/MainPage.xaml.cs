namespace AndroidAppDevelopment.Sudoku
{
    public partial class MainPage : ContentPage
    {
        private readonly List<Entry> _entries;
        private int _random = 0;

        public MainPage()
        {
            InitializeComponent();

            _entries = new List<Entry>();
        }

        private async void ContentPage_Loaded(object sender, EventArgs e)
        {
            try
            {
                CreateControls();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "Cancel");
            }
        }

        private void CreateControls()
        {
            var grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                }
            };

            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {
                for (int j = 0; j < grid.ColumnDefinitions.Count; j++)
                {
                    var text = new Entry()
                    {
                        BackgroundColor = Colors.Gray,
                        FontSize = 26,
                        FontAttributes = FontAttributes.Bold,
                        Margin = 5,
                    };

                    text.TextChanged += Text_TextChanged;

                    _entries.Add(text);

                    grid.Add(text, i, j);

                    GenerateRandomValues(text, j);
                }
            }

            var button = new Button()
            {
                Margin = 5,
                Text = "Проверить",
                FontSize = 22,
            };

            button.Clicked += Button_Clicked;

            grid.Add(button, 0, 6);

            grid.SetColumnSpan(button, 5);

            Content = grid;
        }

        private void GenerateRandomValues(Entry entry, int row)
        {
            if (_random == new Random().Next(1, 6))
                return;

            _random = new Random().Next(1, 6);

            if (_random == row)
            {
                entry.IsEnabled = false;
              
                int newRandom = new Random().Next(1, 6);

                entry.Text = newRandom.ToString();
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                var col = _entries.Select(x => x.Text == "" ? "0" : x.Text).ToList();
                bool isSuccess = false;

                for (int i = 0; i < 5; i++)
                {
                    int columnIndex = i + 1;

                    var list = new List<int>();

                    for (int j = 0; j < 5; j++)
                    {
                        int index = i  * 5 + j;

                        if (i == columnIndex - 1)
                        {
                            list.Add(int.Parse(col[j]));
                        }
                    }

                    if (list.Distinct().Count() == 5)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;

                        var temp = await DisplayAlert("Ошибка", "Ячейка не заполнена или содержит одинаковые комбинации.", "Restart", "Cancel");

                        if (temp)
                        {
                            ContentPage_Loaded(sender, e);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (isSuccess)
                {
                    var temp = await DisplayAlert("Информация", "УРА!!! Вы выиграли.", "Restart", "Cancel");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "Cancel");
            }
        }

        private async void Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (e.NewTextValue != "")
                {
                    int value = int.Parse(e.NewTextValue);

                    if (value > 6 || value < 1)
                    {
                        await DisplayAlert("Ошибка", "Макс. 6, Мин. 1", "OK");

                        ((Entry)sender).Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "Cancel");
            }
        }
    }
}