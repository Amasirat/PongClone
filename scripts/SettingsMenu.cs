using System;
using System.Collections.Generic;
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
        // the game can not tolerate unset time limit
        if (GetNode<OptionButton>("TimeInput").Selected == -1)
            throw new Exception("There is no selected option");
        
        // assemble all states in control of the settings menu into one dictionary
        Dictionary<string, string> states = new Dictionary<string, string>();
        states.Add("fullscreen", fullscreen.ToString().ToLower());
        states.Add("time_limit", GetNode<OptionButton>("TimeInput").Text);
        states.Add("revert_controls", revertControls.ToString().ToLower());
        
        UpdateConfigState(states);
    }

    private void UpdateConfigState(Dictionary<string, string> states)
    {
        if (!FileAccess.FileExists(configPath))
        {
            throw new Exception("config file is corrupted or does not exist. You can delete any remnant of the config and " +
                                "try relaunching the game to generate new default configuration");
        }
        using var config = FileAccess.Open(configPath, FileAccess.ModeFlags.Write);
        foreach (var kvp in states)
        {
            string[] line = { kvp.Key, kvp.Value };
            config.StoreCsvLine(line);
        }
        
        config.Close();
    }
    public void OnFullscreenToggled(bool isFullscreen)
    {
        fullscreen = isFullscreen;
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
        revertControls = isToggled;
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
}
