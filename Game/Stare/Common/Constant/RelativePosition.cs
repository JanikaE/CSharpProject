namespace Stare.Common.Constant
{
    /// <summary>
    /// 玩家在房间中与其他玩家的相对位置
    /// </summary>
    public enum RelativePosition
    {
        None,
        /// <summary>自家</summary>
        Self,
        /// <summary>上家</summary>
        Previous,
        /// <summary>下家</summary>
        Next,
        /// <summary>对家</summary>
        Opposite
    }
}
