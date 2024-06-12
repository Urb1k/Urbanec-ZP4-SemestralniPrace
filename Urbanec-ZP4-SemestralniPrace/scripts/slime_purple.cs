using Godot;
using System;

public partial class slime_purple : Node2D
{
	public int direction = 1;
	public int SPEED = 100;
	private RayCast2D RayCastLeft;
	private RayCast2D RayCastRight;
	private AnimatedSprite2D AnimatedSprite2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Assuming RayCastLeft and RayCastRight are children of slime_green node.
		RayCastLeft = GetNode<RayCast2D>("RayCastLeft");
		RayCastRight = GetNode<RayCast2D>("RayCastRight");
		AnimatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (RayCastRight.IsColliding()){
			direction = -1;
			AnimatedSprite2D.FlipH = true;
		}
		if (RayCastLeft.IsColliding()){
			direction = 1;
			AnimatedSprite2D.FlipH = false;
		}

		Position += new Vector2(direction * SPEED * (float)delta, 0);
	}
}
