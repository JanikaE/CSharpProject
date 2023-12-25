namespace Diary
{
    public partial class InvalidForm : Form
    {
        public InvalidForm()
        {
            InitializeComponent();

            ListBoxInvalid.Items.Clear();
            if (MainForm.DiaryInvalid.Count == 0)
            {
                ListBoxInvalid.Items.Add("No invalid name.");
            }
            else
            {
                foreach (string name in MainForm.DiaryInvalid)
                {
                    ListBoxInvalid.Items.Add(name);
                }
            }           
        }
    }
}
