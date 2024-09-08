using Godot;
using PongClone.scripts;

public partial class OnePlayerLevel : Level
{
    [Signal] public delegate void EndGameEventHandler(TimerUI time);
    private void OnGoalArea()
    {
        EmitSignal(SignalName.EndGame, GetNode<TimerUI>("TimerUI"));
    }
}
