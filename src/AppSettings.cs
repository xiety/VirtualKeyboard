using System.Configuration;

namespace VirtualKeyboard;

public class AppSettings
{
    public required Keys[] VirtualKeys { get; set; }
    public required Size ButtonSize { get; set; }
    public required float FontSize { get; set; }

    public static AppSettings LoadFromConfig()
    {
        var settings = ConfigurationManager.AppSettings;

        var keysConfig = settings["Keys"];
        var keysArray = keysConfig != null
            ? keysConfig.Split(',').Select(k => Enum.Parse<Keys>(k.Trim())).ToArray()
            : [Keys.Enter];

        int buttonWidth = int.TryParse(settings["ButtonWidth"], out var bw) ? bw : 100;
        int buttonHeight = int.TryParse(settings["ButtonHeight"], out var bh) ? bh : 50;
        float fontSize = float.TryParse(settings["FontSize"], out var fs) ? fs : 12f;

        return new()
        {
            VirtualKeys = keysArray,
            ButtonSize = new Size(buttonWidth, buttonHeight),
            FontSize = fontSize
        };
    }
}
