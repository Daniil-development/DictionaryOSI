using System.Windows.Forms;

namespace Client
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            CenterToParent();

            label1.Select();
        }
    }
}
