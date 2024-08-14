using Godot;
using System;

public partial class GoalArea : Area2D
{
    [Signal]
    public delegate void GoalEventHandler();

    private void OnBodyEntered(Node2D body)
    {
        GD.Print("goalareaentered!");
        EmitSignal(SignalName.Goal);
    }
}
