using UnityEngine;

namespace Player
{
    public interface IInput
    {
        Vector3 MovementInput { get; }
        bool JumpInput { get; }
        bool IsInputActive();
    }
}