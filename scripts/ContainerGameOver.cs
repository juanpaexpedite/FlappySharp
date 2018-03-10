using Godot;
using System;

public class ContainerGameOver : Container
{
    private Bird bird;

    private AnimationPlayer player;

    private TextureButton ButtonPlay;

    public override void _Ready()
    {
         bird = (Bird)GetNode("/root/MainNode/Bird");
         player = (AnimationPlayer)GetNode("AnimationPlayerGameOver");
         ButtonPlay = (TextureButton)GetNode("ContainerButtons/ButtonPlay");
         
         bird.OnStateChanged += ShowGameOver;
    }

    private void ShowGameOver(object sender, EventArgs e)
    {
        if(bird.State == BirdStates.Ground)
        {
            ButtonPlay.GrabFocus();
            Show();
            player.Play("GameOverAnimation");
        }
    }
}
