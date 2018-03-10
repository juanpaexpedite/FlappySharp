using Godot;
using System;

public class Medal : TextureRect
{
    int bronzeValue = 2;
    string bronzePath = "res://sprites/medal_bronze.png";
    Texture bronzeTxtr;

    int silverValue = 4;
    string silverPath = "res://sprites/medal_silver.png";
    Texture silverTxtr;

    int goldValue = 6;
    string goldPath = "res://sprites/medal_gold.png";
    Texture goldTxtr;

    int platinumValue = 8;
    string platinumPath = "res://sprites/medal_platinum.png";
    Texture platinumTxtr;

    AnimatedSprite sparks;
    AnimationPlayer sparksPlayer;
    public override void _Ready()
    {
        sparks = (AnimatedSprite)GetChild(0);
        sparksPlayer = (AnimationPlayer)sparks.GetChild(0);
        bronzeTxtr = (Texture)ResourceLoader.Load(bronzePath);
        silverTxtr = (Texture)ResourceLoader.Load(silverPath);
        goldTxtr = (Texture)ResourceLoader.Load(goldPath);
        platinumTxtr = (Texture)ResourceLoader.Load(platinumPath);
        GD.Print("medal textures loaded");
        Texture = null;
    }

    public void ShowMedal(int score)
    {
        if(score>=platinumValue)
        {
            Texture = platinumTxtr;
        }
        else if(score>=goldValue)
        {
            Texture = goldTxtr;
        }
        else if(score>=silverValue)
        {
            Texture = silverTxtr;
        }
        else if(score>=bronzeValue)
        {
            Texture = bronzeTxtr;
        }
        else
        {
            sparks.Hide();
            return;
        }

        sparks.Show();
        sparksPlayer.Play("Shine");
        Show();
    }
}
