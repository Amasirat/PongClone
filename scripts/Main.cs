using Godot;
public partial class Main : Node2D
{
    [Signal]
    public delegate void RightUpdateEventHandler();
    [Signal]
    public delegate void LeftUpdateEventHandler();

    public override void _Ready()
    {
        dotPosition = GetNode<Marker2D>("DotPosition");
        delay = GetNode<Timer>("RespawnDelay");
        timer = GetNode<Timer>("Timer");
    }

    public override void _Process(double delta)
    {   
        GD.Print($"{timer.TimeLeft}");
    }
    public void OnGoalAreaLeft()
    {
        EmitSignal(SignalName.LeftUpdate);
        delay.Start();
    }

    public void OnGoalAreaRight()
    {
        EmitSignal(SignalName.RightUpdate);
        delay.Start();
    }

    public void RespawnDot()
    {
        var dot = GetNode<Node2D>("Dot");
        dot.Position = dotPosition.Position;
    }

    private Marker2D dotPosition;
    private Timer delay;
    private Timer timer;
}
