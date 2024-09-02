using Godot;
// todo Debug of Godot gives errors, find out what it is
public partial class SettingsMenu : Control
{
    public void OnBackButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
    }
}
