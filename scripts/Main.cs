using System.Collections.Generic;
using System.Linq;
using Godot;
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
        states = new Dictionary<string, string>();
        DownloadConfigStates();
        ApplyConfigStates();
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

    private void DownloadConfigStates()
    {
        var config = FileAccess.Open(configPath, FileAccess.ModeFlags.Read);
        while (config.GetPosition() < config.GetLength())
        {
            var line = config.GetCsvLine();
            switch (line[0])
            {
                // only these two states are relevant to the scene
                case "time_limit":
                    states.Add("time_limit", line[1]);
                    break;
                case "revert_controls":
                    states.Add("revert_controls", line[1]);
                    break;
            }
        }
        config.Close();
    }

    private void ApplyConfigStates()
    {
        timerUI.EndTime = int.Parse(states["time_limit"]);
        
        if (bool.Parse(states["revert_controls"]))
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

    private void RespawnDot()
    {
        EmitSignal(SignalName.Respawn, dotPosition.Position, dotDirection);
    }

    private void OnTimerUIGameEnd()
    {
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
    
    [Export] public string configPath;
    // references to children nodes
    private Marker2D dotPosition;
    private Timer delay;
    private Timer timer;
    private TimerUI timerUI;
    private int dotDirection;
    private Guard leftGuard;
    private Guard rightGuard; 
    // holds data downloaded from user config file
    private Dictionary<string, string> states;
}
