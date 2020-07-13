using System;
using System.Windows.Forms;
using WindowsFormsApp1.DbClasses;

namespace WindowsFormsApp1.Forms
{
    public partial class Bring_book : Form
    {
        public Bring_book()
        {
            InitializeComponent();
        }

        private void Bring_book_Load(object sender, EventArgs e)
        {
            button1.DialogResult = DialogResult.OK;
            numericUpDown1.Value = Common.BookId;
            numericUpDown2.Value = Common.UserId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.BookId = get_bookid();
            Common.UserId = get_userid();
            this.Close();
        }

        public int get_bookid()
        {
            var a = Convert.ToInt32(numericUpDown1.Value);
            return a;
        }
        
        public int get_userid()
        {
            var b = Convert.ToInt32(numericUpDown2.Value);
            return b;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}