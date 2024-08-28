using Godot;
using System;
// The TimerUI displays a timer on screen. The Minute and Seconds are tracked differently and once seconds reaches 60
// the value of minute is increased by 1.
public partial class TimerUI : Control
{
    public override void _Ready()
    {
        minutes = 0;
        seconds = 0;
        secondsLabel = GetNode<Label>("Seconds");
        minutesLabel = GetNode<Label>("Minutes");
        UpdateLabels();
    }
    // Needs to be hooked up to a timer object to call this method
    private void OnTimerTimeout()
    {
        // when seconds reaches 60, the modulus operator makes sure it becomes 0 again.
        seconds = (seconds + 1) % 60; 
        if (seconds == 0)
        {
            minutes++;
        }
        UpdateLabels();
        if (minutes == EndTime)
            EmitSignal(SignalName.GameEnd);
    }
    // Update Label Text
    private void UpdateLabels()
    {
        secondsLabel.Text = seconds.ToString("00");
        minutesLabel.Text = minutes.ToString("00");
    }

    [Export] public int EndTime;
    [Signal] public delegate void GameEndEventHandler();
    
    // Private fields
    private int seconds;
    private int minutes;
    private Label secondsLabel;
    private Label minutesLabel;
}
