using Godot;
using System;

public partial class ExitMenuPopUp : Control
{
    private void OnRejectPressed()
    {
        GetTree().Paused = false;
        Hide();
    }

    private void OnConfirmPressed()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
    }

    private void OnMainExitPressed()
    {
        GetTree().Paused = true;
        Show();
    }
}
