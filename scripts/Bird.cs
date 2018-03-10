using Godot;
using System;
using System.Threading.Tasks;

//Based on: https://bitbucket.org/EdwardAngeles/godot-engine-tutorial-flappy-bird/downloads/

public class Bird : RigidBody2D
{
	public float HSpeed = 50;
	private IBird bird;

	public BirdStates State;

	public EventHandler OnStateChanged;

 	private MainCamera camera;

	//LEARN:
	//If you use AudioStreamPlayer2D the audio sounds lower (might be because it has some kind of distance parameter)
	public AudioStreamPlayer WingAudioPlayer;
	public AudioStreamPlayer HitAudioPlayer;
	public AudioStreamPlayer DiedAudioPlayer;
	public AudioStreamPlayer ScoreAudioPlayer;

	

    public override void _Ready()
    {
		State = BirdStates.Flying;
		bird = new FlyingState(this);
		bird.Init();
		SetProcess(true);
		SetProcessInput(true);

		//LEARN:
		//This is possible because we added the Sounds Scene to Autoplay in the project settings.
		camera  = (MainCamera)GetNode("/root/MainNode/MainCamera");

		//LEARN:
		//This is possible because we added the Sounds Scene to Autoplay in the project settings.
		WingAudioPlayer = (AudioStreamPlayer)GetNode("/root/Sounds/WingAudio");
		HitAudioPlayer = (AudioStreamPlayer)GetNode("/root/Sounds/HitAudio");
		DiedAudioPlayer = (AudioStreamPlayer)GetNode("/root/Sounds/DieAudio");
		ScoreAudioPlayer = (AudioStreamPlayer)GetNode("/root/Sounds/PointAudio");
    }


	
	#region Collision
	//LEARN: This connection is done with the EDITOR and generates empty
	//[connection signal="body_entered" from="." to="." method="OnBirdBodyEntered"]
	private void OnBirdBodyEntered(Godot.Object body)
	{
		if(State == BirdStates.Flapping && body is Ground)
		{
			camera.Shake();
			HitAudioPlayer.Play();
			PlayDieAsync();
			GD.Print("Ground collision from Flapping");
			SetState(BirdStates.Ground);
		}
		else if(State == BirdStates.Flapping && body is Pipe)
		{
			camera.Shake();
			HitAudioPlayer.Play();
			GD.Print("Pipe collision from Flapping");
			SetState(BirdStates.Hit);
		}
		else if(State == BirdStates.Hit && body is Ground)
		{
			DiedAudioPlayer.Play();
			GD.Print("Ground collision from Hit");
			SetState(BirdStates.Ground);
		}
	}

	private async void PlayDieAsync()
	{
		await Task.Delay(TimeSpan.FromSeconds(0.2));
		DiedAudioPlayer.Play();
	}
	#endregion
	
	public override void _Input(InputEvent inputevent)
	{
		bird.Input(inputevent);
	}
		
   public override void _Process(float delta)
   {
     	bird.Update(delta);
   }

   public void SetState(BirdStates newstate)
   {
	   bird?.Exit();
	   State = newstate;
	   switch(State)
	   {
		   case BirdStates.Flapping:
		   bird = new FlappingState(this);
		   break;
		   case BirdStates.Flying:
		   bird = new FlyingState(this);
		   break;
		   case BirdStates.Ground:
		   bird = new GroundState(this);
		   break;
		   case BirdStates.Hit:
		   bird = new HitState(this);
		   break;
	   }
	   bird.Init();

		if(OnStateChanged!=null)
		{
			OnStateChanged(this,null);
		}
   }
}

public enum BirdStates
{
	Flying,
	Flapping,
	Hit,
	Ground
}

public interface IBird
{
	void Init();
	void Update(float delta);
	void Input(InputEvent inputEvent);
	void Exit();
}

public interface ICollision
{
	void OnCollision(object other);
}

public class FlappingState : IBird, ICollision
{
	private SpawnerPipe spawnerPipe;
	public Bird instance;
	private AnimationPlayer animation;
	private Camera2D camera;

	public FlappingState(Bird bird)
	{
		instance = bird;
	}
	public void Init()
	{
		instance.SetLinearVelocity(new Vector2(instance.HSpeed,instance.GetLinearVelocity().y));		
		animation = (AnimationPlayer)instance.GetNode("AnimationPlayer");
		spawnerPipe = (SpawnerPipe)instance.GetNode("/root/MainNode/SpawnerPipe");
		camera  = (Camera2D)instance.GetNode("/root/MainNode/MainCamera");
		Flap();
		spawnerPipe.Start();
	}

	public void Update(float delta)
	{
		if(instance.GetRotationDegrees() < -30)
		{
			instance.SetAngularVelocity(0);
			instance.SetRotationDegrees(-30);
		}

		if(instance.GetLinearVelocity().y > 0)
		{
			instance.SetAngularVelocity(1.5f);
		}   
	}

	public void Input(InputEvent inputevent)
	{
		if(inputevent.IsActionPressed("Flap"))
		{
			Flap();
		}

		if(inputevent is InputEventMouseButton mouseevent && mouseevent.IsPressed() && !mouseevent.IsEcho())
		{	
			if(mouseevent.ButtonIndex == (int)ButtonList.Left)
			{
				Flap();
			}
		}
	}

	private void Flap()
	{
		instance.WingAudioPlayer.Play();
		var v = instance.GetLinearVelocity();
		instance.SetLinearVelocity(new Vector2(v.x,-150));
		instance.SetAngularVelocity(-3);
		animation.Play("Flap");
	}

	public void Exit()
	{

	}

    public void OnCollision(object other)
    {
        GD.Print(other.ToString());
    }
}

public class FlyingState : IBird
{
	public Bird instance;
	private AnimationPlayer animation;

	private AnimatedSprite animsprite;
	private float prevGravity = 0;

	public FlyingState(Bird bird)
	{
		instance = bird;
		prevGravity = instance.GravityScale;
	}

  	public void Init()
    {
		instance.GravityScale = 0;
        animation = (AnimationPlayer)instance.GetNode("AnimationPlayer");
		animsprite = (AnimatedSprite)instance.GetNode("AnimatedSprite");
		animation.Play("Flying");
		instance.SetLinearVelocity(new Vector2(instance.HSpeed,instance.GetLinearVelocity().y));		
    }

    public void Exit()
    {
        instance.GravityScale = prevGravity;
		animation.Stop();
		animsprite.Position = new Vector2(0,0);
    }

  

    public void Input(InputEvent inputEvent)
    {
       
    }

    public void Update(float delta)
    {
        
    }

}

public class HitState : IBird
{
	public Bird instance;

	public HitState(Bird bird)
	{
		instance = bird;
	}

    public void Exit()
    {
       
    }

    public void Init()
    {
	
        instance.LinearVelocity = new Vector2(0,0);
		instance.AngularVelocity = 2;
		var pipe = (Node)instance.GetCollidingBodies()[0];
		instance.AddCollisionExceptionWith(pipe);
    }

    public void Input(InputEvent inputEvent)
    {
      
    }

    public void Update(float delta)
    {
       
    }

}

public class GroundState : IBird, ICollision
{
	public Bird instance;

	public GroundState(Bird bird)
	{
		instance = bird;
	}

    public void Exit()
    {
        
    }

    public void Init()
    {
       instance.LinearVelocity = new Vector2(0,0);
	   instance.AngularVelocity = 0;
    }

    public void Input(InputEvent inputEvent)
    {
        
    }

    public void Update(float delta)
    {
        
    }

	public void OnCollision(object other)
    {
       
    }
}



