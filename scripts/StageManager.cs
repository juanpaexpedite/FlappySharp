using Godot;
using System;
using System.Threading.Tasks;

public class StageManager : CanvasLayer
{
    public string StageGamePath = "res://stages/GameStage.tscn";
    private AnimationPlayer player;
    private TextureRect blackrect;
    public AudioStreamPlayer SwooshAudioPlayer;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        blackrect = (TextureRect)GetNode("BlackRect");

        //LEARN:
		//This is possible because we added the Sounds Scene to Autoplay in the project settings.
        SwooshAudioPlayer = (AudioStreamPlayer)GetNode("/root/Sounds/SwooshAudio");
    }

    public async void ChangeStage(string stagepath)
    {
        blackrect.Visible = true;
        //Fade to black
        var animation = player.GetAnimation("FadeIn");
        SwooshAudioPlayer.Play();
        player.Play("FadeIn");
        await Task.Delay(TimeSpan.FromSeconds(animation.Length));
        //INFORMATION
        //Might be there is a way with yield but have not found it.

        //Change State
        GetTree().ChangeScene(stagepath);

        //Fade from black
         animation = player.GetAnimation("FadeOut");
         player.Play("FadeOut");
         await Task.Delay(TimeSpan.FromSeconds(animation.Length));
         blackrect.Visible = false;
    }

}
