namespace VirtualKeyboard;

public partial class MainForm : Form
{
    protected override bool ShowWithoutActivation => true;

    public MainForm(AppSettings settings)
    {
        InitializeComponent();
        CreateKeyButtons(settings);
        WinApiHelper.SetNoActivateStyle(Handle);
    }

    private void CreateKeyButtons(AppSettings settings)
    {
        foreach (var key in settings.VirtualKeys)
        {
            var button = new Button
            {
                Text = key.ToString(),
                Tag = key,
                Size = settings.ButtonSize,
                Padding = new Padding(0),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true,
                Font = new Font(Font.FontFamily, settings.FontSize),
            };

            button.Click += KeyButton_Click;

            flowLayoutPanel.Controls.Add(button);
        }
    }

    private void KeyButton_Click(object? sender, EventArgs e)
    {
        if (sender is Button { Tag: Keys key })
            WinApiHelper.SendKeyPress(key);
    }
}
