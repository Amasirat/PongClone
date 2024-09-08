using Godot;
using PongClone.scripts;

public partial class OnePlayerEndPopUp : Control
{

    public override void _Ready()
    {
        sessionTime = GetNode<TimerUI>("Panel/SessionTime");
        bestTime = GetNode<TimerUI>("Panel/BestTime");
    }

    private void OnEndGame(TimerUI time)
    {
        Show();
        GetTree().Paused = true;
        sessionTime.AssignTime(time.Minutes, time.Seconds);
        bestTime.AssignTime((int)GameStateManager.Instance.BestTime[0],
            (int)GameStateManager.Instance.BestTime[1]);
        GameStateManager.Instance.RecordBestTime(new Vector2(sessionTime.Minutes, sessionTime.Seconds));
    }
    
    private void OnExitButtonPressed()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
    }

    private void OnTryButtonPressed()
    {
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }
    
    private TimerUI sessionTime;
    private TimerUI bestTime;
}
