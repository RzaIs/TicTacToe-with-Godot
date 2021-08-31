using Godot;
using System;

public class MainGame : Control
{
    Menu menu;
    Random rand;
    public char turn;
    public char startTurn;
    public char[] sides = { 'X', 'O' };


    public override void _Ready()
    {
        rand = new Random();
        menu = GetNode<Menu>("Menu");
        startTurn = sides[rand.Next(2)];
        turn = startTurn;
        StartGame();
    }

    public override void _Process(float delta)
    {

    }

    public void StartGame()
    {
        NextStartTurn();
        foreach (var button in GetNode("Grids").GetChildren())
        {
            XOButton btn = ((XOButton)button);
            btn.Text = "";
            btn.pressed = false;
        }
        menu.ShowTurnOwner(turn);
    }

    public void NextTurn()
    {
        if (turn == 'X') turn = 'O';
        else if (turn == 'O') turn = 'X';
        if(!CheckForWin())
            CheckDraw();
        menu.ShowTurnOwner(turn);
    }

    public void NextStartTurn()
    {
        if (startTurn == 'X') startTurn = 'O';
        else if (startTurn == 'O') startTurn = 'X';
        turn = startTurn;
    }

    public bool CheckForWin()
    {
        var xoButtons = GetNode("Grids");
        string[] values = new string[9];
        int i = 0;
        foreach (var button in xoButtons.GetChildren())
        {
            values[i] = ((XOButton)button).Text;
            i++;
        }

        string[] lines = new string[8];

        lines[0] = values[0] + values[1] + values[2];
        lines[1] = values[3] + values[4] + values[5];
        lines[2] = values[6] + values[7] + values[8];

        lines[3] = values[0] + values[3] + values[6];
        lines[4] = values[1] + values[4] + values[7];
        lines[5] = values[2] + values[5] + values[8];

        lines[6] = values[0] + values[4] + values[8];
        lines[7] = values[6] + values[4] + values[2];

        foreach (string line in lines)
        {
            if(IsSequential(line))
            {
                EndGame(line[0]);
                return true;
            }
        }
        return false;
    }

    public bool CheckDraw()
    {
        bool isDraw = true;
        foreach (var button in GetNode("Grids").GetChildren())
        {
            if (((XOButton)button).pressed == false)
            {
                isDraw = false;
            }
        }
        
        if(isDraw)
        {
            menu.ShowDraw();
        }
        return isDraw;
    }

    public bool IsSequential(string line)
    {
        if(line.Length == 3)
        {
            if (line[0] == line[1] && line[0] == line[2])
            {
                return true;
            }
        }
        return false;
    }

    public void EndGame(char winner)
    {
        foreach (var button in GetNode("Grids").GetChildren())
        {
            ((XOButton)button).pressed = true;
        }
        menu.ShowWinner(winner);
        menu.UpdateScore(winner);
    }

    public void OnMenuReset()
    {
        StartGame();
    }
}
