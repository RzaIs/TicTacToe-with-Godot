using Godot;
using System;

public class Menu : Control
{
    [Signal] public delegate void Reset();

    public Label scoreX;
    public Label scoreO;
    public Label turnOwner;
    public Label winnerLabel;
    public ColorRect winnerPanel;

    int xScore;
    int oScore;

    public override void _Ready()
    {
        scoreX = GetNode<Label>("LabelX");
        scoreO = GetNode<Label>("LabelO");
        turnOwner = GetNode<Label>("Turn");
        winnerPanel = GetNode<ColorRect>("WinnerPanel");
        winnerLabel = winnerPanel.GetNode<Label>("WinnerLabel");
        xScore = 0;
        oScore = 0;
    }

    public void UpdateScore(char winner)
    {
        if(winner == 'X')
        {
            xScore++;
            scoreX.Text = "Score of X : " + xScore;
        }
        else if(winner == 'O')
        {
            oScore++;
            scoreO.Text = "Score of O : " + oScore;
        }
    }

    public void ResetScore()
    {
        xScore = 0;
        oScore = 0;
        scoreX.Text = "Score of X : " + xScore;
        scoreO.Text = "Score of O : " + oScore;
    }

    public void OnResetGamePressed()
    {
        ResetScore();
        OnNewRoundPressed();
    }

    public void OnNewRoundPressed()
    {
        winnerPanel.Visible = false;
        EmitSignal(nameof(Reset));
    }

    public void ShowWinner(char winner)
    {
        winnerPanel.Visible = true;
        winnerLabel.Text = winner + " Wins";
    }

    public void ShowDraw()
    {
        winnerPanel.Visible = true;
        winnerLabel.Text = "Draw";
    }

    public void ShowTurnOwner(char owner)
    {
        turnOwner.Text = "Turn of\n" + owner;
    }
}
