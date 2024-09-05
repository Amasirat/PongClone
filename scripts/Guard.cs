using Godot;
public partial class Guard : StaticBody2D
{
    public override void _Ready()
    {
        screenSize = GetViewportRect().Size;
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
        // Clamp the position of guard to not let it go out of views
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, screenSize.X),
            y: Mathf.Clamp(Position.Y, 0, screenSize.Y)
        );
    }
    
    // Use these variables to give custom actions to an instance.
    // It is meant make two player controls easier to configure
    [Export] public string upAction;
    [Export] public string downAction;
    [Export] public float speed;
    private Vector2 screenSize;
}
