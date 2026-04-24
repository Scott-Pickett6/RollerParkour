namespace Assets.Scripts.Player
{
    public readonly struct PlayerInputInfo
    {
        public float ForwardInput { get; private set; }
        public float SideInput { get; private set; }
        public bool JumpPressed { get; private set; }

        public PlayerInputInfo(float forwardInput, float sideInput, bool jumpPressed)
        {
            ForwardInput = forwardInput;
            SideInput = sideInput;
            JumpPressed = jumpPressed;
        }

        public bool HasInput()
        {
            return ForwardInput != 0 || SideInput != 0 || JumpPressed;
        }
    }
}