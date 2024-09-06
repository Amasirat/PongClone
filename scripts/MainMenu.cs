using System;
using System.Collections.Generic;
using Godot;
using PongClone.scripts;

public partial class MainMenu : Control
{
    [Signal]
    public delegate void GameSelectEventHandler();
    private void HideControls()
    {
        GetNode<Button>("StartButton").Hide();
        GetNode<Button>("ExitButton").Hide();
        GetNode<Button>("SettingsButton").Hide();
        GetNode<Label>("Title").Hide();
    }

    private void ShowControls()
    {
        GetNode<Button>("StartButton").Show();
        GetNode<Button>("ExitButton").Show();
        GetNode<Button>("SettingsButton").Show();
        GetNode<Label>("Title").Show();
    }
    
    private void OnStartButtonPressed()
    {
        EmitSignal(SignalName.GameSelect);
        HideControls();
    }

    private void OnGameSelectExit()
    {
        ShowControls();
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
