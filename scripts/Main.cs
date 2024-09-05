using Godot;
using PongClone.scripts;
/// <summary>
/// The main script handles the ongoings of a Pong game session. It contains relevant children nodes
/// and assigns references to them in InitializeReferenceNodes method.
/// ApplyConfigStates uses the GameStateManager singleton to retrieve its stored states and apply them, I.E
/// TimeLimit and revertControls.
///
/// Signals:
///     RightUpdate: Orders ScoreUI to update the RightScore
///     LeftUpdate: Orders ScoreUI to update the LeftScore
///     Respawn: It orders the dot to respawn itself.
/// </summary>
public partial class Main : Node2D
{
    [Signal]
    public delegate void RightUpdateEventHandler();
    [Signal]
    public delegate void LeftUpdateEventHandler();
    [Signal]
    public delegate void RespawnEventHandler(Vector2 pos, int direction);

    public override void _Ready()
    {
        InitializeReferenceNodes();
        // ApplyConfigStates();
    }
    // do all code references of main's child nodes here, meant to be called from _Ready method
    private void InitializeReferenceNodes()
    {
        dotPosition = GetNode<Marker2D>("DotPosition");
        delay = GetNode<Timer>("RespawnDelay");
        timer = GetNode<Timer>("Timer");
        timerUI = GetNode<TimerUI>("TimerUI");
        leftGuard = GetNode<Guard>("LeftGuard");
        rightGuard = GetNode<Guard>("RightGuard");
    }
    private void ApplyConfigStates()
    {
        timerUI.EndTime = GameStateManager.Instance.TimeLimit;
        
        if (GameStateManager.Instance.RevertControls)
        {
            leftGuard.upAction = "arrow_move_up";
            leftGuard.downAction = "arrow_move_down";

            rightGuard.upAction = "wasd_move_up";
            rightGuard.downAction = "wasd_move_down";
        }
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
    private TimerUI timerUI;
    private int dotDirection;
    private Guard leftGuard;
    private Guard rightGuard; 
}
