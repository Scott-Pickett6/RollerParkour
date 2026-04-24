namespace Assets.Scripts.Player
{
    public readonly struct PlayerInputInfo
    {
        public readonly float ForwardInput { get; }
        public readonly float SideInput { get; }
        public readonly bool JumpPressed { get; }

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