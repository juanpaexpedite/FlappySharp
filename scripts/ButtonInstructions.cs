using Godot;
using System;

public class ButtonInstructions : TextureButton
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        Connect("pressed",this, "OnPressed");
        GrabFocus();
    }

    private void OnPressed()
    {
          var bird = (Bird)GetNode("/root/MainNode/Bird");
          if(bird!=null)
          {
              bird.SetState(BirdStates.Flapping);
              Hide();
          }
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
