using Godot;
using System;

public partial class kill_zone : Area2D
{
	private Timer timer;
	private Timer timerMoveDeathLine;
	private Label deathLabel;
	private Label counterLabel;
	private Node2D player; 
	private Label FindLabel(Node node, string labelName)
	{
		foreach (Node child in node.GetChildren())
		{
			if (child is Label label && label.Name == labelName)
			{
				return label;
			}

			Label found = FindLabel(child, labelName);
			if (found != null)
			{
				return found;
			}
		}
		return null;
	}

	public override void _Ready()
	{
		timer = new Timer();
		timerMoveDeathLine = new Timer();
		AddChild(timerMoveDeathLine);
		AddChild(timer);
		timer.Timeout += () => _on_timer_timeout();
		timerMoveDeathLine.Timeout += () => _on_timerMoveDeathLine();

		this.BodyEntered += (Node2D body) => _on_body_entered(body);

		deathLabel = FindLabel(GetParent(), "DeathLabel");
		if (deathLabel == null)
		{
			deathLabel = FindLabel(GetTree().Root, "DeathLabel");
		}

		counterLabel = FindLabel(GetParent(), "CounterLabelCanvas");
		if (counterLabel == null)
		{
			counterLabel = FindLabel(GetTree().Root, "CounterLabelCanvas");
		}
	}

	private void _on_body_entered(Node2D body)
	{
		if (this.Name == "KillZoneDown" || this.Name == "KillZoneDown2"){
			this.Position = new Vector2(0,1000);
		}

		player = body as Node2D; // Store the player reference
		Vector2 playerPosition = player.GlobalPosition;

		Engine.TimeScale = 0.3;
		player.GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
		timer.WaitTime = 0.5f; // Set the wait time in seconds
		timer.OneShot = true;  // Ensure the timer only runs once
		timer.Start();

		deathLabel.Visible = true;
		deathLabel.GlobalPosition = new Vector2(playerPosition.X - 100, playerPosition.Y - 200); 
		timerMoveDeathLine.WaitTime = 0.8f;
		timerMoveDeathLine.OneShot = true;
		timerMoveDeathLine.Start();
	}
		
	private void _on_timer_timeout()
	{	
		player.GlobalPosition = (player as player).SpawnPosition;
		player.GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", false);
		// Hide the death message
		deathLabel.Visible = false;
		Engine.TimeScale = 1;
	}

	private void _on_timerMoveDeathLine(){
		if (this.Name == "KillZoneDown" || this.Name == "KillZoneDown2"){
			this.Position = new Vector2 (0,200);
			GD.Print("změna mrtveho bodu");
		}
	}

	private void UpdateCounterLabel()
	{
		if (counterLabel != null)
		{
			counterLabel.Text = $"coins {coin.COINS}/39";
		}
	}
}
