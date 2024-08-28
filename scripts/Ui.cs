using Godot;
using System;

public partial class Ui : Control
{
    public override void _Ready()
    {
        rightLabel = GetNode<Label>("RightScore");
        leftLabel = GetNode<Label>("LeftScore");
    }
    public void UpdateLeftScore()
    {
        leftScore++;
        rightLabel.Text = leftScore.ToString();
    }
    public void UpdateRightScore()
    {
        rightScore++;
        leftLabel.Text = rightScore.ToString();
    }
    private int leftScore { get; set; }
    private int rightScore { get; set; }
    private Label rightLabel;
    private Label leftLabel;
}
