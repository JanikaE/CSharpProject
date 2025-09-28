using Utils.Mathematical;

namespace MoveSimulation
{
    public class MovePoint
    {
        public Vector2D Position { get; set; }

        public Vector2D Velocity { get; set; }

        public MovePoint(Vector2D position, Vector2D velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public MovePoint()
        {
            Position = new Vector2D(0, 0);
            Velocity = new Vector2D(0, 0);
        }

        public void Update(RelativePosition_8 direction)
        {
            if (Setting.Friction > 0)
            {
                if (Velocity.Length > Setting.Friction)
                {
                    Velocity *= (Velocity.Length - Setting.Friction) / Velocity.Length;
                }
                else
                {
                    Velocity = new Vector2D(0, 0);
                }
            }
            Velocity += Setting.GetAcceleration(direction);
            Position += Velocity;
        }

        public void Reset()
        {
            Position = new Vector2D(0, 0);
            Velocity = new Vector2D(0, 0);
        }
    }
}
