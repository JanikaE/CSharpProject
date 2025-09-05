using Utils.Mathematical;

namespace MoveSimulation
{
    public static class Setting
    {
        public static float Friction { get; set; } = 1f;

        public static float AccelerationModulu { get; set; } = 2f;

        public static Vector2D GetAcceleration(RelativePosition_8 direction)
        {
            Vector2D acceleration = RelativePosition.ToVector2D(direction);
            return acceleration.ToNormal() * Setting.AccelerationModulu;
        }
    }
}
