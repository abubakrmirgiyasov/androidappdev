namespace AndroidAppDevelopment.FirstPart;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
