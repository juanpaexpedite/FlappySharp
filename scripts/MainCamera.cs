using Godot;
using System;

public class MainCamera : Camera2D
{
    float time = 0;
    float times = 10;
    int magnitude = 3;
    bool shake=false;
    Random gen;

    Vector2 originaloffset;
    RigidBody2D birdinstance;
    public override void _Ready()
    {
        SetProcess(true);
        birdinstance= (RigidBody2D)GetNode("/root/MainNode/Bird");
        gen =new Random();
        originaloffset = Offset;
    }
    
    public override void _Process(float delta)
    {
        Position = new Vector2(birdinstance.Position.x,Position.y);

        if(shake)
        {
            time ++;
            if(time>times)
            {
                shake = false;
                Offset = originaloffset;
                return;
            }
            float x = gen.Next(-magnitude,magnitude);
            float y = gen.Next(-magnitude,magnitude);
            Offset = new Vector2(x + originaloffset.x,y);
        }
    }

    public void Shake()
    {
        shake = true;
    }

  
}
