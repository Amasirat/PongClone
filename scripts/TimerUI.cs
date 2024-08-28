using Godot;
using System;

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
    private void OnTimerTimeout()
    {
        seconds = (seconds + 1) % 60;
        GD.Print(seconds);
        if (seconds == 0)
        {
            minutes++;
        }
        UpdateLabels();
    }

    private void UpdateLabels()
    {
        secondsLabel.Text = minutes.ToString("00");
        minutesLabel.Text = minutes.ToString("00");
    }

    private int seconds;
    private int minutes;
    private Label secondsLabel;
    private Label minutesLabel;
}
