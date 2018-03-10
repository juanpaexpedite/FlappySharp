using Godot;
using System;

public class SpawnerGround : Node2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
    private PackedScene scn_ground;
    private int ground_width = 168;
    private int amount_to_fill_view = 2;
    private Camera2D camera;

    public override void _Ready()
    {
        scn_ground = (PackedScene)ResourceLoader.Load("res://scenes/Ground.tscn");
        
        camera  = (Camera2D)GetNode("/root/MainNode/MainCamera");

        for(int i=0;i<amount_to_fill_view;i++)
        {
            SpawnGround();
            GoNextPosition();
        }
    }

    private void OnGroundDestroyed(object sender, EventArgs e)
    {
        GD.Print("Ground Destroyed");
        SpawnGround();
        GoNextPosition();
    }

    private void SpawnGround()
    {
        var newGround = (Ground)scn_ground.Instance();
        
        //DEPRECATED
        //newGround.Connect("Destroyed", this, "OnDestroyed");
        
        newGround.GroundDestroyed += OnGroundDestroyed;
        newGround.Position = Position;
        GetNode("container").AddChild(newGround);
        
    }

    private void GoNextPosition()
    {
        Position = Position + new Vector2(ground_width,0);
       
    }

}
