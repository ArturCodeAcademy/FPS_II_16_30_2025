using System;
using UnityEngine;

public interface IInformationContainer
{
    event EventHandler StateChanged;
    string MainInformation { get; }
    string Description { get; }
    Sprite? Icon { get; }
}
