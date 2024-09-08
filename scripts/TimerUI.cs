using Godot;
using System;
// The TimerUI displays a timer on screen. The Minute and Seconds are tracked differently and once seconds reaches 60
// the value of minute is increased by 1.
public partial class TimerUI : Control
{
    public override void _Ready()
    {
        Minutes = 0;
        Seconds = 0;
        secondsLabel = GetNode<Label>("Seconds");
        minutesLabel = GetNode<Label>("Minutes");
        UpdateLabels();
    }

    public void AssignTime(int minutes, int seconds)
    {
        Seconds = seconds;
        Minutes = minutes;
        UpdateLabels();
    }
    // Needs to be hooked up to a timer object to call this method
    protected virtual void OnTimerTimeout()
    {
        // when seconds reaches 60, the modulus operator makes sure it becomes 0 again.
        Seconds = (Seconds + 1) % 60; 
        if (Seconds == 0)
        {
            Minutes++;
        }
        UpdateLabels();
        // if (minutes == EndTime)
        // {
        //     EmitSignal(SignalName.GameEnd);
        // }
    }
    // Update Label Text
    private void UpdateLabels()
    {
        secondsLabel.Text = Seconds.ToString("00");
        minutesLabel.Text = Minutes.ToString("00");
    }
    // [Export] public int EndTime;
    // [Signal] public delegate void GameEndEventHandler();
    
    public int Seconds { get; set; }
    public int Minutes { get; set; }
    private Label secondsLabel;
    private Label minutesLabel;
}
