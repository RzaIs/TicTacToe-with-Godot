using Godot;
using System;

public class XOButton : Button
{
    public MainGame main;
    public bool pressed;

    public override void _Ready()
    {
        pressed = false;
        main = GetParent().GetParent<MainGame>();
    }

    public void ChangeProperty()
    {
        Text = main.turn.ToString();
        main.NextTurn();
    }

    public void OnButtonPressed()
    {
        if(!pressed)
        {
            pressed = true;
            ChangeProperty();
        }
    }


}
