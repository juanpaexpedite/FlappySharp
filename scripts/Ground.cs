using Godot;
using System;

public class Ground : StaticBody2D
{
    public Ground()
    {
        //DEPRECATED
        //It is really important set it here because we usually make the connection after instancing it!
        //AddUserSignal("Destroyed");
    }

    Node2D bottomright;
    private Camera2D camera;

    public event EventHandler GroundDestroyed;

    //DEPRECATED
    // [Signal]
    // delegate void Destroyed();

    public override void _Ready()
    {
        bottomright = (Node2D)GetNode("BottomRight");
        camera  = (Camera2D)GetNode("/root/MainNode/MainCamera");
        SetProcess(true);

       
    }

    

    public override void _Process(float delta)
   {
       if(bottomright.GetGlobalPosition().x <= (camera.Position.x + camera.Offset.x))
       {
           QueueFree();

           if(GroundDestroyed!=null)
           {
               GroundDestroyed(this,null);
           }
           //DEPRECATED
           //EmitSignal(nameof(Destroyed));
       }
       
   }
}

