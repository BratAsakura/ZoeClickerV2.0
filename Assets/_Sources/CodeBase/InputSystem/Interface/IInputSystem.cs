using System;
using UnityEngine;

public interface IInputSystem
{
    event Action<Vector2> Clicked;
}