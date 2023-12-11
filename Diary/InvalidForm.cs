namespace Diary
{
    public partial class InvalidForm : Form
    {
        public InvalidForm()
        {
            InitializeComponent();

            ListBoxInvalid.Items.Clear();
            if (DiaryInvalid.Count == 0)
            {
                ListBoxInvalid.Items.Add("No invalid name.");
            }
            else
            {
                foreach (string name in DiaryInvalid)
                {
                    ListBoxInvalid.Items.Add(name);
                }
            }           
        }
    }
}
