using Godot;
using System;

//IMPORTANT:
//Remember to set Pause Mode:Process in the editor 
public class ButtonResume : TextureButton
{
    private TextureRect txtrPausedGame;
    private TextureButton buttonPause; 
    public override void _Ready()
    {
        buttonPause = (TextureButton)GetParent().GetNode("TxtrButtonPause");
        txtrPausedGame = (TextureRect)GetParent().GetNode("TxtrPausedGame");
    }

    private void OnButtonResumePressed()
    {
        txtrPausedGame.Visible = false;
        buttonPause.Visible = true;
        Visible = false;
        GetTree().Paused = false;
    }
}



