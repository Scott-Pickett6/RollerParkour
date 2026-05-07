using System;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour, IInput
    {
        public float SideInput { get; private set; }
        public float ForwardInput { get; private set; }
        public bool JumpInput { get; private set; }
        
        // returns new vector3 everytime get is called
        public Vector3 MovementInput => new Vector3(SideInput, 0, ForwardInput);
        
        public static event Action OnFirstMove;
        
        bool hasMoved = false;

        void Update()
        {
            SideInput = Input.GetAxis("Side");
            ForwardInput = Input.GetAxis("Forward");
            JumpInput = Input.GetButtonDown("Jump");
            if (!hasMoved && IsInputActive())
            {
                hasMoved = true;
                OnFirstMove?.Invoke();
            }
        }

        public bool IsInputActive()
        {
            return SideInput != 0f || ForwardInput != 0f || JumpInput;
        }
    
    }
}
