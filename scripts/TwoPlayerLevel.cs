using Godot;
using PongClone.scripts;
public sealed partial class TwoPlayerLevel : Level
{
    [Signal]
    public delegate void RightUpdateEventHandler();
    [Signal]
    public delegate void LeftUpdateEventHandler();
    [Signal]
    public delegate void RespawnEventHandler(Vector2 pos, int direction);
    protected override void InitializeReferenceNodes()
    {
        base.InitializeReferenceNodes();
        dotPosition = GetNode<Marker2D>("DotPosition");
        delay = GetNode<Timer>("RespawnDelay");
        timer = GetNode<Timer>("Timer");
        timerUI = GetNode<TwoPlayerTimerUI>("TimerUI");
    }
    protected override void ApplyConfigStates()
    {
        base.ApplyConfigStates();
        timerUI.EndTime = GameStateManager.Instance.TimeLimit;
    }
    // Once the dot enters GoalArea, the main script sends a signal to the ScoreUI to change its values
    private void OnGoalAreaLeft()
    {
        EmitSignal(SignalName.RightUpdate);
        // for respawing the dot correctly. Records the direction the dot went into the goal area
        dotDirection = -1;
        delay.Start();
    }

    private void OnGoalAreaRight()
    {
        EmitSignal(SignalName.LeftUpdate);
        // for respawing the dot correctly. Records the direction the dot went into the goal area
        dotDirection = 1;
        delay.Start();
    }
    
    private void OnTimerUIGameEnd()
    {
        GetTree().Paused = true;
        GameEndPopUp popUp = GetNode<GameEndPopUp>("GameEndPopUp");
        int rightScore = GetNode<ScoreUI>("ScoreUI").RightScore;
        int leftScore = GetNode<ScoreUI>("ScoreUI").LeftScore;
        popUp.UpdateScoreLabels(leftScore, rightScore);
        popUp.Show();
    }

    private void OnPopUpAnotherGame()
    {
        GetTree().ReloadCurrentScene();
    }

    private void RespawnDot()
    {
        EmitSignal(SignalName.Respawn, dotPosition.Position, dotDirection);
    }
    
    // references to children nodes
    
    private Marker2D dotPosition;
    private Timer delay;
    private Timer timer;
    private TwoPlayerTimerUI timerUI;
    private int dotDirection;
}
