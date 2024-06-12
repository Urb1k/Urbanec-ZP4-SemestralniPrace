using Godot;
using System;

public partial class coin : Area2D
{
	// Number of collected coins
	public static int COINS = 0;

	// Reference to the Label node
	private Label counterLabel;

	private bool isCollected = false;

	public override void _Ready()
	{
		// Connect the body_entered signal to the method _on_body_entered
		this.BodyEntered += (Node2D body) => _on_body_entered(body);

				// Find the CanvasLayer in the scene tree
		CanvasLayer canvasLayer = GetNode<CanvasLayer>("../../Canvas");

		// Check if canvasLayer is valid
		if (canvasLayer == null)
		{
			//GD.PrintErr("CanvasLayer not found");
			return;
		}

		// Get the reference to the Counter label within the CanvasLayer
		counterLabel = canvasLayer.GetNode<Label>("CounterLabelCanvas");

		// Check if counterLabel is valid
		if (counterLabel == null)
		{
			//GD.PrintErr("Counter label not found within CanvasLayer");
			return;
		}

		// Initialize the counter label text
		UpdateCounterLabel();
	}

	public void _on_body_entered(Node2D body)
	{
		if (isCollected)
			return;

		isCollected = true;

		COINS++;
		UpdateCounterLabel();
		QueueFree();
	}

	private void UpdateCounterLabel()
	{
		counterLabel.Text = $"coins {COINS}/39";
	}
}
