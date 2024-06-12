using Godot;
using System;

public partial class EndingButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Connect the "pressed" signal to the OnPressed method
		this.Pressed  += () => OnPressed();
	}

	// Called when the button is pressed
	private void OnPressed()
	{
		// Exit the game
		GetTree().Quit();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
