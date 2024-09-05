using Godot;
public partial class GameEndPopUp : Control
{
    [Signal]
    public delegate void AnotherGameEventHandler();
    public override void _Ready()
    {
        rightScoreLabel = GetNode<Label>("RightScore");
        leftScoreLabel = GetNode<Label>("LeftScore");
        winLabel = GetNode<Label>("WinLabel");
    }
    public void UpdateScoreLabels(int leftScore, int rightScore)
    {
        if (leftScore > rightScore)
        {
            winLabel.Text = "Left Wins!";
        }
        else if (leftScore == rightScore)
        {
            winLabel.Text = "It's a Draw!";
        }
        else
        {
            winLabel.Text = "Right Wins!";
        }
        
        leftScoreLabel.Text = leftScore.ToString();
        rightScoreLabel.Text = rightScore.ToString();
    }

    private void OnRejectPressed()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
    }

    private void OnConfirmPressed()
    {
        GetTree().Paused = false;
        EmitSignal(SignalName.AnotherGame);
        Hide();
    }
    
    private Label leftScoreLabel;
    private Label rightScoreLabel;
    private Label winLabel;
}
