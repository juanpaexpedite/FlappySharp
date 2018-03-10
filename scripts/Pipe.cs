using Godot;
using System;

public class Pipe : StaticBody2D
{
    public Pipe()
    {
        //DEPRECATED
        //It is really important set it here because we usually make the connection after instancing it!
        //AddUserSignal("PipeDestroyed");
    }
    private Camera2D camera;
    Node2D right;

    //DEPRECATED
    //[Signal]
    //delegate void PipeDestroyed();
    public event EventHandler PipeDestroyed;

    public override void _Ready()
    {
        right = (Node2D)GetNode("Right");
        camera  = (Camera2D)GetNode("/root/MainNode/MainCamera");
        SetProcess(true);
    }

        public override void _Process(float delta)
        {
            if(right.GetGlobalPosition().x <= (camera.GetGlobalPosition().x + camera.Offset.x))
            {
                QueueFree();
                
                //DEPRECATED
                //EmitSignal(nameof(PipeDestroyed));
                if(PipeDestroyed!=null)
                {
                    PipeDestroyed(this,null);
                }
            }
            
        }
}
