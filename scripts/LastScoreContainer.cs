using Godot;
using System;
using System.Collections;


//IMPORTANT (MIGHT BE ONLY IN MY COMPUTER):
//I am not sure what's happening, because when I run the game it is like and 'old' version and the 'new' version of this code are like
//running together, because the LastScoreLabel appears with the score value (I set it in code before adding the animation)
//and now stills there

public class LastScoreContainer : VBoxContainer
{
    private Label LastScoreLabel;
    private Label BestScoreLabel;

    private TextureRect TextNewRecord;

    private Medal MedalInstance;

    
    public override void _Ready()
    {
         //NOTE:
         //Becasuse we are in the LastScoreContainer to access to the TextNewRecord:
         TextNewRecord = (TextureRect)GetParent().GetNode(nameof(TextNewRecord));
         MedalInstance = (Medal)GetParent().GetNode("Medal");
         LastScoreLabel = (Label)GetNode(nameof(LastScoreLabel));
         LastScoreLabel.Text = "0";
         BestScoreLabel = (Label)GetNode(nameof(BestScoreLabel));
         BestScoreLabel.Text = Settings.LastBestScore.ToString();
         TextNewRecord.Hide();
         animScore = 0;
         animateScore = null;
    }

    /* This is how I think it should work in C# but I do not know the best way
    int score=0;
    public IEnumerator AnimateScore()
    {
        //INFORMATION:
        //This method is called from the GameOverAnimation from the AnimationPlayerGameOver
        //and works!
        GD.Print("animating score");

        LastScoreLabel.Text = score.ToString();
        score++;
        if(score > Settings.Score)
        {
            yield break;
        }
        else
        {
            yield return 0;
        }
    }
     */

    int animScore=0;
    bool? animateScore = null;
    public void AnimateScore()
    {
        GD.Print("playing animating score");
        LastScoreLabel.Text = "0";
        LastScoreLabel.Text ="0";
        animateScore = true;
    }

    int steps = 3;
    int times =0;
    public override void _Process(float delta)
    {
        if(animateScore == null)
        {
         LastScoreLabel.Text = "0";
         BestScoreLabel.Text = Settings.LastBestScore.ToString();
         return;
        }

        if(++times < steps)
            return;
        times=0;

        if(animateScore.Value)
        {
            LastScoreLabel.Text = animScore.ToString();
            animScore++;
            if(animScore > Settings.Score)
            {
                animateScore=false;
                if(Settings.BestScore > Settings.LastBestScore)
                {
                    TextNewRecord.Show();
                }
                BestScoreLabel.Text = Settings.BestScore.ToString();
                Settings.LastBestScore = Settings.BestScore;
            }
            MedalInstance.ShowMedal(Settings.Score);
        }
    }
}
