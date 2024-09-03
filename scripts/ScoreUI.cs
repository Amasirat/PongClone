using Godot;
public partial class ScoreUI : Control
{
    public override void _Ready()
    {
        rightLabel = GetNode<Label>("RightScore");
        leftLabel = GetNode<Label>("LeftScore");
    }
    public void UpdateLeftScore()
    {
        LeftScore++;
        leftLabel.Text = LeftScore.ToString();
    }
    public void UpdateRightScore()
    {
        RightScore++;
        rightLabel.Text = RightScore.ToString();
    }
    
    public int LeftScore { get; set; }
    public int RightScore { get; set; }
    
    private Label rightLabel;
    private Label leftLabel;
}
