using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public PlayerInputInfo GetInput()
        {
            return new PlayerInputInfo(Input.GetAxis("Forward"), Input.GetAxis("Side"), Input.GetButtonDown("Jump"));
        }
    }
}