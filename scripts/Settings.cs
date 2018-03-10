using Godot;
using System;

public static class Settings
{

    public static event EventHandler ScoreEvent;
    private static int _score = 0;
    public static int Score
    {
        get { return _score;}
        set 
        { 
            GD.Print($"Change score to {value}");
            _score = value;
             if(_score > BestScore)
            {
                 GD.Print($"Change best score to {value}");
                BestScore = _score;
            }
            if (ScoreEvent != null) 
            {
                ScoreEvent(_score, EventArgs.Empty);
            }
        }
    }
    
    public static event EventHandler BestScoreEvent;

    public static int LastBestScore = 0;
    private static int _bestscore;
    public static int BestScore
    {
        get { return _bestscore;}
        set
        {
            _bestscore = value;
            if (BestScoreEvent != null) 
            {
             BestScoreEvent(_bestscore, EventArgs.Empty);
            }
        }
    }

    
}