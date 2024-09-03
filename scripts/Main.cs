using Godot;
public partial class Main : Node2D
{
    [Signal]
    public delegate void RightUpdateEventHandler();
    [Signal]
    public delegate void LeftUpdateEventHandler();
    [Signal]
    public delegate void RespawnEventHandler(Vector2 pos, int direction);

    public override void _Ready()
    {
        dotPosition = GetNode<Marker2D>("DotPosition");
        delay = GetNode<Timer>("RespawnDelay");
        timer = GetNode<Timer>("Timer");
    }
    // Once the dot enters GoalArea, the main script sends a signal to the ScoreUI to change its values
    private void OnGoalAreaLeft()
    {
        EmitSignal(SignalName.LeftUpdate);
        dotDirection = -1;
        delay.Start();
    }

    private void OnGoalAreaRight()
    {
        EmitSignal(SignalName.RightUpdate);
        dotDirection = 1;
        delay.Start();
    }

    private void RespawnDot()
    {
        EmitSignal(SignalName.Respawn, dotPosition.Position, dotDirection);
    }

    private void OnTimerUIGameEnd()
    {
        GameEndPopUp popUp = GetNode<GameEndPopUp>("GameEndPopUp");
        int rightScore = GetNode<ScoreUI>("ScoreUI").RightScore;
        int leftScore = GetNode<ScoreUI>("ScoreUI").LeftScore;
        popUp.UpdateScoreLabels(leftScore, rightScore);
        popUp.Show();
    }

    private void OnPopUpAnotherGame()
    {
        GetTree().ReloadCurrentScene();
    }

    private Marker2D dotPosition;
    private Timer delay;
    private Timer timer;
    private int dotDirection;
}
