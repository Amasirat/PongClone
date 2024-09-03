using Godot;
using FileAccess = Godot.FileAccess;

public partial class SettingsMenu : Control
{
    public override void _Ready()
    {
        ApplyConfigToSettingsMenu();
    }

    private void ApplyConfigToSettingsMenu()
    {
        var config = FileAccess.Open(configPath, FileAccess.ModeFlags.Read);
        while (config.GetPosition() < config.GetLength())
        {
            string[] line = config.GetCsvLine();
            if (line.Length == 0 || line.Length > 2)
                GD.Print("Recieved unexpected amount of attributes in user://config.csv");;
            switch (line[0])
            {
                case "resolution":
                    SelectResolution(line[1]);
                    break;
                case "fullscreen":
                    bool isFullscreen = bool.Parse(line[1]);
                    GetNode<CheckButton>("Fullscreen").ButtonPressed = isFullscreen;
                    break;
                case "time_limit":
                    SelectTimeLimit(line[1]);
                    break;
                case "revert_controls":
                    bool isReverted = bool.Parse(line[1]);
                    GetNode<CheckButton>("MovementExchange").ButtonPressed = isReverted;
                    break;
                default:
                    GD.Print("The config file is most likely corrupt, " +
                             "if so delete the config file and restart the game.");
                    break;
            }
        }
        config.Close();
    }

    private void SelectResolution(string resolution)
    {
        var resolutionOptions = GetNode<OptionButton>("ResolutionButton");
        switch (resolution)
        { // the space is necessary because the csv writer puts space after delimiter which makes switch mapping fail.
            case " 1920x1440":
                resolutionOptions.Selected = 0;
                break;
            case " 1280x720":
                resolutionOptions.Selected = 1;
                break;
            case " 920x720":
                resolutionOptions.Selected = 2;
                break;
            case " 480x320":
                resolutionOptions.Selected = 3;
                break;
            default:
                resolutionOptions.Selected = -1;
                GD.Print("Unsupported resolution");
                break;
        }
    }

    private void SelectTimeLimit(string timelimit)
    {
        int minute = int.Parse(timelimit);
        var timeOption = GetNode<OptionButton>("TimeInput");
        switch (minute)
        {
            case 2:
                timeOption.Selected = 0;
                break;
            case 5:
                timeOption.Selected = 1;
                break;
            case 8:
                timeOption.Selected = 2;
                break;
            case 10:
                timeOption.Selected = 3;
                break;
            case 15:
                timeOption.Selected = 4;
                break;
            case 20:
                timeOption.Selected = 5;
                break;
            default:
                timeOption.Selected = -1;
                GD.Print("Unsupported Time Limit");
                break;
        }
    }
    
    public void OnBackButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
    }

    public void OnApplyButtonPressed()
    {
        UploadStateToConfig();
        GetTree().ReloadCurrentScene();
    }

    private void UploadStateToConfig()
    {
        var config = FileAccess.Open(configPath, FileAccess.ModeFlags.Write);
        
        // todo: rewriting config with the states held within the button components
        
        config.Close();
    }
    public void OnFullscreenToggled(bool isFullscreen)
    {
        Window window = new Window();
        if (isFullscreen)
        {
            window.Mode = Window.ModeEnum.Fullscreen;
        }
        window.Mode = Window.ModeEnum.Windowed;
    }

    private void OnMovementExchangeToggled(bool isToggled)
    {
        if (isToggled)
        {
            MakeShowRightWASD();
        }
        else
        {
            MakeShowRightArrowed();
        }
    }

    private void MakeShowRightWASD()
    {
        GetNode<TextureRect>("LeftUpIcon").
            SetTexture(ResourceLoader.Load<Texture2D>(
                "res://assets/icons/icons8-page-up-button-50.png"));
        GetNode<TextureRect>("LeftDownIcon").
            SetTexture(ResourceLoader.Load<Texture2D>(
                "res://assets/icons/icons8-page-down-button-50.png"));
        GetNode<TextureRect>("RightUpIcon").
            SetTexture(ResourceLoader.Load<Texture2D>(
                "res://assets/icons/icons8-w-key-50.png"));
        GetNode<TextureRect>("RightDownIcon").
            SetTexture(ResourceLoader.Load<Texture2D>(
                "res://assets/icons/icons8-s-key-50.png"));
    }

    private void MakeShowRightArrowed()
    {
        GetNode<TextureRect>("LeftUpIcon").
            SetTexture(ResourceLoader.Load<Texture2D>(
                "res://assets/icons/icons8-w-key-50.png"));
        GetNode<TextureRect>("LeftDownIcon").
            SetTexture(ResourceLoader.Load<Texture2D>(
                "res://assets/icons/icons8-s-key-50.png"));
        GetNode<TextureRect>("RightUpIcon").
            SetTexture(ResourceLoader.Load<Texture2D>(
                "res://assets/icons/icons8-page-up-button-50.png"));
        GetNode<TextureRect>("RightDownIcon").
            SetTexture(ResourceLoader.Load<Texture2D>(
                "res://assets/icons/icons8-page-down-button-50.png"));
    }

    [Export] public string configPath;

    private bool fullscreen;
    private bool revertControls;
    private string resolution;
    private int timeLimit;
}
