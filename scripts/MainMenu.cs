using System;
using System.Collections.Generic;
using Godot;
using PongClone.scripts;

public partial class MainMenu : Control
{
    private void OnStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/main.tscn");
    }
    
    private void OnSettingsButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/settings_menu.tscn");
    }

    private void OnExitButtonPressed()
    {
        GetTree().Quit();
    }

}
