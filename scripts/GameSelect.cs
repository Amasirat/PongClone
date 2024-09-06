using Godot;
public partial class GameSelect : Control
{
    [Signal] public delegate void ExitEventHandler();
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("exit"))
        {
            Hide();
            EmitSignal(SignalName.Exit);
        }
    }
    private void OnTwoPlayerButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/level_select/two_player_level.tscn");
    }

    private void OnOnePlayerButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/level_select/one_player_level.tscn");
    }

    private void OnMainMenuGameSelect()
    {
        Show();
    }
    
}
