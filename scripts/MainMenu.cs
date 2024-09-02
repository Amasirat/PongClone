using Godot;
public partial class MainMenu : Control
{
    public void OnExitButtonPressed()
    {
        GetTree().Quit();
    }

    public void OnSettingsButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/settings_menu.tscn");
    }
    
    public void OnStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/main.tscn");
    }
}
