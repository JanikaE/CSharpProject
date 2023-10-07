namespace Reversi
{
    public static class Common
    {
        public static Chess Opposite(this Chess chess)
        {
            return chess switch
            {
                Chess.Black => Chess.White,
                Chess.White => Chess.Black,
                _ => Chess.None
            };
        }

        public static string ToChar(this Chess chess)
        {
            return chess switch
            {
                Chess.Black => "○",
                Chess.White => "●",
                _ => "  "
            };
        }
    }

    public enum Chess
    {
        None,
        Black,
        White,
    }

    public enum State
    {
        None,
        Continue,
        End,
    }
}
