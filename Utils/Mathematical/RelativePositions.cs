namespace Utils.Mathematical
{
    public class RelativePosition
    {
        public static RelativePosition_4 Opposite(RelativePosition_4 position)
        {
            return position switch
            {
                RelativePosition_4.Up => RelativePosition_4.Down,
                RelativePosition_4.Down => RelativePosition_4.Up,
                RelativePosition_4.Left => RelativePosition_4.Right,
                RelativePosition_4.Right => RelativePosition_4.Left,
                _ => RelativePosition_4.None,
            };
        }

        public static RelativePosition_8 Opposite(RelativePosition_8 position)
        {
            return position switch
            {
                RelativePosition_8.Up => RelativePosition_8.Down,
                RelativePosition_8.Down => RelativePosition_8.Up,
                RelativePosition_8.Left => RelativePosition_8.Right,
                RelativePosition_8.Right => RelativePosition_8.Left,
                RelativePosition_8.UpLeft => RelativePosition_8.DownRight,
                RelativePosition_8.UpRight => RelativePosition_8.DownLeft,
                RelativePosition_8.DownLeft => RelativePosition_8.UpRight,
                RelativePosition_8.DownRight => RelativePosition_8.UpLeft,
                _ => RelativePosition_8.None,
            };
        }

        public static RelativePosition_6 Opposite(RelativePosition_6 position)
        {
            return position switch
            {
                RelativePosition_6.Right => RelativePosition_6.Left,
                RelativePosition_6.UpRight => RelativePosition_6.DownLeft,
                RelativePosition_6.UpLeft => RelativePosition_6.DownRight,
                RelativePosition_6.Left => RelativePosition_6.Right,
                RelativePosition_6.DownLeft => RelativePosition_6.UpRight,
                RelativePosition_6.DownRight => RelativePosition_6.UpLeft,
                _ => RelativePosition_6.None,
            };
        }

        public static Vector2D ToVector2D(RelativePosition_4 position)
        {
            return position switch
            {
                RelativePosition_4.Up => new Vector2D(0, -1),
                RelativePosition_4.Down => new Vector2D(0, 1),
                RelativePosition_4.Left => new Vector2D(-1, 0),
                RelativePosition_4.Right => new Vector2D(1, 0),
                _ => new Vector2D(0, 0),
            };
        }

        public static Vector2D ToVector2D(RelativePosition_8 position)
        {
            return position switch
            {
                RelativePosition_8.Up => new Vector2D(0, -1),
                RelativePosition_8.Down => new Vector2D(0, 1),
                RelativePosition_8.Left => new Vector2D(-1, 0),
                RelativePosition_8.Right => new Vector2D(1, 0),
                RelativePosition_8.UpLeft => new Vector2D(-1, -1),
                RelativePosition_8.UpRight => new Vector2D(1, -1),
                RelativePosition_8.DownLeft => new Vector2D(-1, 1),
                RelativePosition_8.DownRight => new Vector2D(1, 1),
                _ => new Vector2D(0, 0),
            };
        }

        public static Point2D ToPoint2D(RelativePosition_4 position)
        {
            return position switch
            {
                RelativePosition_4.Up => new Point2D(0, -1),
                RelativePosition_4.Down => new Point2D(0, 1),
                RelativePosition_4.Left => new Point2D(-1, 0),
                RelativePosition_4.Right => new Point2D(1, 0),
                _ => new Point2D(0, 0),
            };
        }

        public static Point2D ToPoint2D(RelativePosition_8 position)
        {
            return position switch
            {
                RelativePosition_8.Up => new Point2D(0, -1),
                RelativePosition_8.Down => new Point2D(0, 1),
                RelativePosition_8.Left => new Point2D(-1, 0),
                RelativePosition_8.Right => new Point2D(1, 0),
                RelativePosition_8.UpLeft => new Point2D(-1, -1),
                RelativePosition_8.UpRight => new Point2D(1, -1),
                RelativePosition_8.DownLeft => new Point2D(-1, 1),
                RelativePosition_8.DownRight => new Point2D(1, 1),
                _ => new Point2D(0, 0),
            };
        }

        public static Hexagon ToHexagon(RelativePosition_6 position)
        {
            return position switch
            {
                RelativePosition_6.Right => new Hexagon(1, 0),
                RelativePosition_6.UpRight => new Hexagon(1, -1),
                RelativePosition_6.UpLeft => new Hexagon(0, -1),
                RelativePosition_6.Left => new Hexagon(-1, 0),
                RelativePosition_6.DownLeft => new Hexagon(-1, 1),
                RelativePosition_6.DownRight => new Hexagon(0, 1),
                _ => new Hexagon(0, 0),
            };
        }

        public static RelativePosition_4 ToRelativePosition_4(Point2D point)
        {
            if (point.X == 0 && point.Y < 0) return RelativePosition_4.Up;
            if (point.X == 0 && point.Y > 0) return RelativePosition_4.Down;
            if (point.X < 0 && point.Y == 0) return RelativePosition_4.Left;
            if (point.X > 0 && point.Y == 0) return RelativePosition_4.Right;
            return RelativePosition_4.None;
        }

        public static RelativePosition_8 ToRelativePosition_8(Point2D point)
        {
            if (point.X == 0 && point.Y < 0) return RelativePosition_8.Up;
            if (point.X == 0 && point.Y > 0) return RelativePosition_8.Down;
            if (point.X < 0 && point.Y == 0) return RelativePosition_8.Left;
            if (point.X > 0 && point.Y == 0) return RelativePosition_8.Right;
            if (point.X < 0 && point.Y < 0) return RelativePosition_8.UpLeft;
            if (point.X > 0 && point.Y < 0) return RelativePosition_8.UpRight;
            if (point.X < 0 && point.Y > 0) return RelativePosition_8.DownLeft;
            if (point.X > 0 && point.Y > 0) return RelativePosition_8.DownRight;
            return RelativePosition_8.None;
        }

        public static RelativePosition_8 ToRelativePosition_8(RelativePosition_4 position)
        {
            return position switch
            {
                RelativePosition_4.Up => RelativePosition_8.Up,
                RelativePosition_4.Down => RelativePosition_8.Down,
                RelativePosition_4.Left => RelativePosition_8.Left,
                RelativePosition_4.Right => RelativePosition_8.Right,
                _ => RelativePosition_8.None,
            };
        }
    }

    public enum RelativePosition_4
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    public enum RelativePosition_8
    {
        None,
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }

    public enum RelativePosition_6
    {
        None,
        Right,
        UpRight,
        UpLeft,
        Left,
        DownLeft,
        DownRight
    }
}
