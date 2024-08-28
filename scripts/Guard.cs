using Godot;
using System;

public partial class Guard : StaticBody2D
{
    public override void _Ready()
    {
    }
    public override void _Process(double delta) 
    {
        int direction = 0;
        if(Input.IsActionPressed(upAction))
        {
            direction = -1;
        }

        if(Input.IsActionPressed(downAction))
        {
            direction = 1;
        }

        Vector2 pos = new Vector2(0, direction);
        Position += pos * speed * (float)delta;
    }
    
    [Export]
    public string upAction;
    [Export]
    public string downAction;
    [Export]
    public float speed;
}
