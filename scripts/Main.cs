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
    public void OnGoalAreaLeft()
    {
        EmitSignal(SignalName.LeftUpdate);
        dotDirection = -1;
        delay.Start();
    }

    public void OnGoalAreaRight()
    {
        EmitSignal(SignalName.RightUpdate);
        dotDirection = 1;
        delay.Start();
    }

    public void RespawnDot()
    {
        EmitSignal(SignalName.Respawn, dotPosition.Position, dotDirection);
    }

    private Marker2D dotPosition;
    private Timer delay;
    private Timer timer;
    private int dotDirection;
}
