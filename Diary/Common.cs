using System.Configuration;

namespace Diary
{
    public static class Common
    {
        /// <summary>
        /// 所有日记（文件名合法）
        /// </summary>
        public static readonly List<DiaryDto> DiarysAll = new();
        /// <summary>
        /// 所有非法文件名
        /// </summary>
        public static readonly List<string> DiaryInvalid = new();
        /// <summary>
        /// 文件路径
        /// </summary>
        public static readonly string? prePath = ConfigurationManager.AppSettings["DiaryPath"]?.ToString();
    }
}
