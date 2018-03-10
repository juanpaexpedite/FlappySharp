using Godot;
using System;

public class ButtonPause : TextureButton
{
    private TextureRect txtrPausedGame;
    private TextureButton buttonResume; 
    public override void _Ready()
    {
        buttonResume = (TextureButton)GetParent().GetNode("TxtrButtonResume");
        txtrPausedGame = (TextureRect)GetParent().GetNode("TxtrPausedGame");
    }

    private void OnButtonPausePressed()
    {
        txtrPausedGame.Visible = true;
        buttonResume.Visible = true;
        Visible = false;
        GetTree().Paused = true;
    }


}


