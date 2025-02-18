namespace VirtualKeyboard;

static class Program
{
    [STAThread]
    public static void Main()
    {
        ApplicationConfiguration.Initialize();
        var settings = AppSettings.LoadFromConfig();
        Application.Run(new MainForm(settings));
    }
}
