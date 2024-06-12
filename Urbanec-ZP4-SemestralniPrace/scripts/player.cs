using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 130.0f;
	public const float JumpVelocity = -400.0f;
	 // Declare the AnimatedSprite2D variable
	public AnimatedSprite2D animatedSprite;
	public Vector2 SpawnPosition = new Vector2(-820,-40);
	public bool checkPointReached = false;
	public Timer checkTimer;
	public bool PlayerMoved = false;
	
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	
	public override void _Ready()
		{
		checkTimer = new Timer();
		AddChild(checkTimer);
		checkTimer.WaitTime = 1.0f; // Check every second
		checkTimer.Timeout += () => OnCheckTimerTimeout();
		checkTimer.Start();

			if (!checkPointReached){
				GlobalPosition = new Vector2(-820,-40);
				//GlobalPosition = new Vector2(15350,-300);
			}else{
				GlobalPosition = SpawnPosition;
			}
			
			// Initialize the animatedSprite variable
			animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "ui_up", "ui_down");
		//get my imput direction 1,0,-1
		if (direction.X > 0){
			animatedSprite.FlipH = false;
			PlayerMoved = true;
		}
		else if (direction.X < 0){
			animatedSprite.FlipH = true;
			PlayerMoved = true;
		}
		
		//play animation
		if(IsOnFloor()){
			if(direction.X == 0){
				animatedSprite.Play("idle");
			}
			else{
				animatedSprite.Play("run");
			}
		}
		else {
			animatedSprite.Play("jump");
		}
		
		//apply movement
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		
	}

	private void OnCheckTimerTimeout()
	{
		if (GlobalPosition.X >= 15470){
			SpawnPosition = new Vector2(15470,0);
			checkPointReached = true;
		}
		else if (GlobalPosition.X >= 10832)
		{
			SpawnPosition = new Vector2(10832, 0);
			checkPointReached = true;
		}
		else if (GlobalPosition.X >= 5168)
		{
			SpawnPosition = new Vector2(5168, -48);
			checkPointReached = true;
		}
		if (GlobalPosition.X >= 21300){
			GetTree().ChangeSceneToFile("res://scenes/endingScene.tscn");
		}
	}
}
