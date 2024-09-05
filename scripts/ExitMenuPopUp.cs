using Godot;
using System;

public partial class ExitMenuPopUp : Control
{
    public void OnRejectPressed()
    {
        Hide();
    }

    public void OnConfirmPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
    }
    
}
