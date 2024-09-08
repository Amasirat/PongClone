using Godot;

namespace PongClone.scripts;
// Abstract class for all scripts for level scenes.
// by default, it contains an exit signal.
// It also contains:
// InitializeReferenceNodes => Gets Nodes from its children nodes
// ApplyConfigStates => Applies the action strings to guard nodes
public abstract partial class Level : Node2D
{
    [Signal]
    public delegate void ExitEventHandler();
    public override void _Ready()
    {
        InitializeReferenceNodes();
        ApplyConfigStates();
    }
    // The process checks for exit action to emit the exit signal
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("exit"))
        {
            EmitSignal(SignalName.Exit);
        }
    }
    protected virtual void InitializeReferenceNodes()
    {
        leftGuard = GetNode<Guard>("LeftGuard"); 
        rightGuard = GetNode<Guard>("RightGuard");
    }
    
    protected virtual void ApplyConfigStates()
    {
        if (GameStateManager.Instance.RevertControls)
        {
            leftGuard.upAction = "arrow_move_up";
            leftGuard.downAction = "arrow_move_down";

            rightGuard.upAction = "wasd_move_up";
            rightGuard.downAction = "wasd_move_down";
        }
    }
    protected Guard leftGuard;
    protected Guard rightGuard;
}