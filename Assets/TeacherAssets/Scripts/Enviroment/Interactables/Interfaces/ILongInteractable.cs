using System;

public interface ILongInteractable : IInteractable
{
	event EventHandler ProgressChanged;
	float Progress { get; }
	void StopInteraction();
}