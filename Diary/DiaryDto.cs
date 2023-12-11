namespace Diary
{
    public class DiaryDto
    {
        public DateTime date;
        public string name;
        public string topic;
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
