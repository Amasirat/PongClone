using Godot;
using System.Collections.Generic;

public partial class SettingsMenu : Control
{
    public override void _Ready()
    {
        
    }
    
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
}
