namespace PongClone.scripts;
using Godot;
public partial class TwoPlayerTimerUI : TimerUI
{
    [Signal] public delegate void GameEndEventHandler();
    protected override void OnTimerTimeout()
    {
        base.OnTimerTimeout();
        if (Minutes == EndTime)
        {
            EmitSignal(SignalName.GameEnd);
        }
    }

    [Export] public int EndTime;
}