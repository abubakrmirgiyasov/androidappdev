namespace AndroidAppDevelopment.SecondPart
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnPageLoaded(object sender, EventArgs e)
        {
            XEntry.TextChanged += XEntry_TextChanged;
            YEntry.TextChanged += YEntry_TextChanged;
        }

        private async void OnPlusClick(object sender, EventArgs e)
        {
            var isXDigit =int.TryParse(XEntry.Text, out int x);
            var isYDigit=int.TryParse(YEntry.Text, out int y);

            if (isXDigit && isYDigit)
            {
                Sum.Text = (x + y).ToString();
            }
            else
            {
                await DisplayAlert("Ошибка", "Введите цифры", "Ok");
            }
        }

        private void XEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sum.Text = XEntry.Text;
        }

        private void YEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sum.Text = YEntry.Text;
        }

        private async void OnMinusClick(object sender, EventArgs e)
        {
            var isXDigit = int.TryParse(XEntry.Text, out int x);
            var isYDigit = int.TryParse(YEntry.Text, out int y);

            if (isXDigit && isYDigit)
            {
                Sum.Text = (x - y).ToString();
            }
            else
            {
                await DisplayAlert("Ошибка", "Введите цифры", "Ok");
            }
        }
    }
}