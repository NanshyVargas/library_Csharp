using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp1.DbClasses;

namespace WindowsFormsApp1.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = Common.list_of_debtors(_books, _users);
            richTextBox1.Text += str;
        }

        /*private Book _book1;
        private Book _book2;
        private User _user;*/

        private List<Book> _books;
        private List<User> _users;

        private void FillListboxes()
        {
            listBox1.DataSource = _books;
            listBox2.DataSource = _users;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Common.connect();
            _books = Common.load_book();
            _users = Common.load_user();
            Common.BookId = 1;
            Common.UserId = 2;
            FillListboxes();
            //     var _book1 = new Book(
            //                     0,
            //                     "имя",
            //                     "author",
            //                     2023,
            //                     "ff",
            //                     0,
            //                     null);
            //                 
            //     var _book2 = new Book(
            //         1,
            //         "имя11",
            //         "author1",
            //         2023,
            //         "ff",
            //         0,
            //         null);
            //     _books = new List<Book>(){_book1, _book2};
            //     
            //     var _user = new User(
            //         1,
            //         "имя",
            //         DateTime.Now, 
            //         0,
            //         0,
            //         0);
            //     _users = new List<User>(){_user};
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            var book = (Book)listBox1.SelectedItem;
            var user = (User)listBox2.SelectedItem;
            string str1 = Common.bring_book(_books, _users, book, user);
            richTextBox1.Text += str1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var book = (Book)listBox1.SelectedItem;
            var user = (User)listBox2.SelectedItem;
            string str1 = Common.return_book(_books, _users, book, user);
            richTextBox1.Text += str1;
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Common.close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Common.Clear_base();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _books = Common.load_book();
            _users = Common.load_user();
            FillListboxes();
        }

        private ToolTip tooltype1;
        private void listBox1_MouseHover(object sender, EventArgs e)
        {
            var a = listBox1.SelectedItem;
            tooltype1 = tooltype1 ?? new ToolTip();
            tooltype1.SetToolTip(listBox1,a.ToString() );
        }
        
        private ToolTip tooltype2;
        private void listBox2_MouseHover(object sender, EventArgs e)
        {
            var a = listBox2.SelectedItem;
            tooltype2 = tooltype2 ?? new ToolTip();
            tooltype2.SetToolTip(listBox2,a.ToString() );
        }
    }
}