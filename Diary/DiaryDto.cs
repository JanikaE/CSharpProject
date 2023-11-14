namespace Diary
{
    internal class DiaryDto
    {
        public DateTime date;
        public string topic;
        public string path;

        public DiaryDto(DateTime date, string topic, string path)
        {
            this.date = date;
            this.topic = topic;
            this.path = path;
        }
    }
}
