using Godot;
using System;

public partial class Main : Node2D
{

    public void OnGoalAreaLeft()
    {
        GD.Print("Left");
    }

    public void OnGoalAreaRight()
    {
        GD.Print("Right");
    }

}
