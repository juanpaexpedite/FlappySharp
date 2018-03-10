using Godot;
using System;

public class ScoreLabel : Label
{

    public override void _Ready()
    {
        Settings.ScoreEvent +=  OnScoreEvent;
    }

    private void OnScoreEvent(object sender, EventArgs e)
    {
        if(sender is int score)
            {
                Text = score.ToString();
            }
    }

    //IMPORTANT:
    //This is required to avoid ptr nulls when the Game Stage is 'reloaded'
    //GetTree().ChangeScene(stagepath);
    public override void _ExitTree()
	{
	    Settings.ScoreEvent -=  OnScoreEvent;
	}
}
