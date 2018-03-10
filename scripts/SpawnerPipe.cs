using Godot;
using System;

public class SpawnerPipe : Node2D
{
    private PackedScene scn_pipe;
    private int GROUND_HEIGHT = 55;
    private int PIPE_WIDTH = 26;
    private int OFFSET_Y = 55;
    private int OFFSET_X = 65;
    private int AMOUNT_TO_FILL_VIEW = 3;

    private bool AddPipe = false;

    private Camera2D camera;

    public override void _Ready()
    {
      scn_pipe = (PackedScene)ResourceLoader.Load("res://scenes/Pipe.tscn");
	  camera  = (Camera2D)GetNode("/root/MainNode/MainCamera");
      SetProcess(true);

     
    }

    public void SpandAndMove()
    {
        AddPipe = true;
    }

   public override void _Process(float delta)
   {
       if(AddPipe)
       {
            AddPipe = false;
            SpawnPipe();
            GoNextPosition();
            GD.Print("Adding Pipe");
       }
   }

    Random gen = new Random();

    public void Start()
    {
      GoInitPosition();

      for(int i=0;i<AMOUNT_TO_FILL_VIEW;i++)
      {
        SpawnPipe();
        GoNextPosition();
      }

     
    }

    public void GoInitPosition()
    {
        var initPosition = new Vector2();
        initPosition.x = GetViewportRect().Size.x + PIPE_WIDTH/2;
        initPosition.y = gen.Next(OFFSET_Y,(int)(GetViewportRect().Size.y - GROUND_HEIGHT-OFFSET_Y));
        if(camera!=null)
        {
            initPosition.x += camera.Position.x + camera.Offset.x;
        }
        Position = initPosition;
    }

    public void SpawnPipe()
    {
        var newPipe = (Pipe)scn_pipe.Instance();

        //DEPRECATED
        //newPipe.Connect("PipeDestroyed", this, "SpandAndMove");

        newPipe.PipeDestroyed += OnPipeDestroyed;

        newPipe.Position = Position;
        GetNode("Container").AddChild(newPipe);
    }

    private void OnPipeDestroyed(object sender, EventArgs e)
    {
        SpandAndMove();
    }

    public void GoNextPosition()
    {
        var nextPosition = GetPosition();
        nextPosition.x += PIPE_WIDTH/2 + OFFSET_X + PIPE_WIDTH/2;
        nextPosition.y = gen.Next(OFFSET_Y,(int)(GetViewportRect().Size.y - GROUND_HEIGHT-OFFSET_Y));
        Position = nextPosition;
    }

}
