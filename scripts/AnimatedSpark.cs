using Godot;
using System;

public class AnimatedSpark : AnimatedSprite
{

    private float medalRadius = 11;
    private Random gen = new Random();

    private AnimationPlayer player;
    public override void _Ready()
    {
        SparkSetRandomPos();
        player = (AnimationPlayer)GetChild(0);
    }

    public void SparkSetRandomPos()
    {
        var angle = gen.Next(0,360) * (Math.PI / 180.0);
        var radius = gen.Next(0,11) ;
        var x = radius * Math.Cos(angle) + medalRadius;
        var y = radius * Math.Sin(angle) + medalRadius;
        Position = new Vector2((float)x,(float)y);

        if(player!=null)
        {
            player.Play("Shine");
        }
    }
}
