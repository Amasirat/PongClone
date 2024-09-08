using Godot;
using PongClone.scripts;

public partial class SettingsMenu : Control
{
    public override void _Ready()
    {
        ApplyConfigToSettingsMenu();
    }

    private void ApplyConfigToSettingsMenu()
    {
        bool fullscreen = GameStateManager.Instance.Fullscreen;
        bool revertControls = GameStateManager.Instance.RevertControls;
        int timeLimit = GameStateManager.Instance.TimeLimit;
        
        // Apply states to settings
        GetNode<CheckButton>("Fullscreen").ButtonPressed = fullscreen;
        GetNode<CheckButton>("RevertControls").ButtonPressed = revertControls;
        SelectTimeLimit(timeLimit);
    }
    
    private void SelectTimeLimit(int timelimit)
    {
        var timeOption = GetNode<OptionButton>("TimeInput");
        switch (timelimit)
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
    // When the Back button is pressed
    private void OnBackButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
    }
// when the Apply button is pressed
    private void OnApplyButtonPressed()
    {
        // Change states
        GameStateManager.Instance.TimeLimit = int.Parse(GetNode<OptionButton>("TimeInput").Text);
        GameStateManager.Instance.RevertControls = GetNode<CheckButton>("RevertControls").ButtonPressed;
        GameStateManager.Instance.Fullscreen = GetNode<CheckButton>("Fullscreen").ButtonPressed;
        // Apply states
        GameStateManager.Instance.UploadStateToConfig();
        GameStateManager.Instance.CheckAndApplyFullscreen();
        GetTree().ReloadCurrentScene();
    }
    // This method is only meant to update the frontend, state is only changed in OnApplyButtonPressed
    private void OnRevertControlsToggled(bool isToggled)
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
    
////////// these methods only update control icons in the settings menu //////////////
 ///////// Backend logic can use these methods to apply these updates /////////////

// replace the right guard icons with WASD and left with the arrowed ones
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
// replace the right guards with arrowed ones and left one with WASD controls
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
