namespace Diary
{
    public class DiaryDto
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime date;
        /// <summary>
        /// 文件名（不含后缀）
        /// </summary>
        public string name;
        /// <summary>
        /// 主题
        /// </summary>
        public string topic;
        /// <summary>
        /// 文件完整路径
        /// </summary>
        public string path;

        public DiaryDto(DateTime date, string name, string topic, string path)
        {
            this.date = date;
            this.name = name;
            this.topic = topic;
            this.path = path;
        }
    }
}
