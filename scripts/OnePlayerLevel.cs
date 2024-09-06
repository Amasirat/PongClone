using Godot;
using System;
using PongClone.scripts;

public partial class OnePlayerLevel : Node2D
{
    [Signal]
    public delegate void ExitEventHandler();
    [Signal]
    public delegate void EndGameEventHandler(OnePlayerTimerUI time);
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("exit"))
        {
            EmitSignal(SignalName.Exit);
        }
    }

    private void OnGoalArea()
    {
        EmitSignal(SignalName.EndGame, GetNode<OnePlayerTimerUI>("TimerUI"));
    }
}
