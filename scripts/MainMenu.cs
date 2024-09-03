using System;
using System.Collections.Generic;
using Godot;
public partial class MainMenu : Control
{
    public override void _Ready()
    {
        CreateUserConfig();
    }
// a one time method to create a personal config file for the current user
    private void CreateUserConfig()
    {
        // The user's config file is only created on the game's first launch,
        // the config should be created if it is for whatever reason deleted, otherwise it should skip
        if (FileAccess.FileExists("user://config.csv")) return;
        
        var file = FileAccess.Open("res://config/default.csv", FileAccess.ModeFlags.Read);
        var config = FileAccess.Open("user://config.csv", FileAccess.ModeFlags.Write);
        while (file.GetPosition() < file.GetLength())
        {
            var line = file.GetCsvLine();
            config.StoreCsvLine(line);
        }
        file.Close();
        config.Close();
    }
    // Button Event methods
    private void OnExitButtonPressed()
    {
        GetTree().Quit();
    }

    private void OnSettingsButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/settings_menu.tscn");
    }
    
    private void OnStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/main.tscn");
    }
}
