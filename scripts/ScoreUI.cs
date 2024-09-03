using Godot;
public partial class ScoreUI : Control
{
    public override void _Ready()
    {
        rightLabel = GetNode<Label>("RightScore");
        leftLabel = GetNode<Label>("LeftScore");
    }
    // Increasing the Left score means the right has advantage, therefore the score of the RightLabel must increase
    public void UpdateLeftScore()
    {
        leftScore++;
        rightLabel.Text = leftScore.ToString();
    }
    // Reverse is true here
    public void UpdateRightScore()
    {
        rightScore++;
        leftLabel.Text = rightScore.ToString();
    }
    
    public int LeftScore
    {
        get { return leftScore; }
    }

    public int RightScore
    {
        get { return rightScore; }
    }
    private int leftScore { get; set; }
    private int rightScore { get; set; }
    private Label rightLabel;
    private Label leftLabel;
}
