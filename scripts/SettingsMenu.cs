using Godot;

public partial class SettingsMenu : Control
{
    public void OnBackButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
    }

    public void OnApplyButtonPressed()
    {
        
        GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
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
        
    }
}
