using System;
using UnityEngine;

namespace Events
{
    public abstract class VoidEventChannel : ScriptableObject
    {
        public event Action OnEventRaised;
        
        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}