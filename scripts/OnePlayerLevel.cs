using Godot;
using PongClone.scripts;

public partial class OnePlayerLevel : Level
{
    [Signal] public delegate void EndGameEventHandler(OnePlayerTimerUI time);
    private void OnGoalArea()
    {
        EmitSignal(SignalName.EndGame, GetNode<OnePlayerTimerUI>("TimerUI"));
    }
}
