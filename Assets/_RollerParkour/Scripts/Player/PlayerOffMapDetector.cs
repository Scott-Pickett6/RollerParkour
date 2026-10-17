using UnityEngine;
using System;

namespace Player
{
    public class PlayerOffMapDetector : MonoBehaviour, IOffMapDetector
    {
        [SerializeField]
        float fallYThreshold = -10f;

        public event Action OnOffMap;

        bool hasFallen;

        void Update()
        {
            if (hasFallen) return;

            if (DetectFall())
            {
                hasFallen = true;
                OnOffMap?.Invoke();
            }
        }
        
        bool DetectFall()
        {
            if (transform.position.y < fallYThreshold)
            {
                return true;
            }
            return false;
        }
    }
}