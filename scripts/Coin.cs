using Godot;
using System;

public class Coin : Area2D
{
    public override void _Ready()
    {
       
    }

    private void OnCoinBodyEntered(Godot.Object body)
    {
        if(body is Bird bird)
        {
            GD.Print("Pipes passed");
            Settings.Score = Settings.Score +1;
            bird.ScoreAudioPlayer.Play();
        }
    }
}



