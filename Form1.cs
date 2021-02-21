using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBrouzer1
{
    public partial class addrBox : Form
    {
        Database1Entities db = new Database1Entities();

        public addrBox()
        {
            InitializeComponent();
        }

        private void LoadCategories()
        {
            var categoriesList = db.Categories.ToList();
            comboBox1.Items.Clear();
            comboBox1.DataSource = categoriesList;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.bing.com");
            LoadCategories();
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            addrBox.Text = webBrowser1.Url.ToString();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            addrBox.SelectAll();
        }

        private void GoToSite()
        {
            string address = addrBox.Text;
            if(address=="")
            {
                MessageBox.Show("Не указан адрес страницы", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (address.IndexOf(".")==-1)
                {
                    address = $"https://www.google.com/search?q={address}";
                }
                else if(address.IndexOf("http")==-1)
                {
                    address = $"https://{address}";
                }
                webBrowser1.Navigate(address);
            }
        }
        

        private void goPage_Click(object sender, EventArgs e)
        {
            GoToSite();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                GoToSite();
        }

        private void prevPage_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        private void nextPage_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
        }

        private void homePage_Click(object sender, EventArgs e)
        {
            //webBrowser1.GoHome();
            webBrowser1.Navigate("https://www.bing.com");
        }

        private void comboBox1_SelectedIndexChange(object sender, EventsArgs e)
        {
            int K = comboBox1.SelectedIndex;
            if (K !=-1)
            {
                var sitesList = (comboBox1.SelectedItem as Categories).Sites.ToList();
                listBox1.Items.Clear();
                listBox1.DataSource = sitesList;
                listBox1.DisplayMember = "Name";
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int k = listBox1.SelectedIndex;
            if (k !=-1)
            {
                string url = (listBox1.SelectedItem as Sites).Address;
                webBrowser1.Navigate(url);
            }
        }
    }
}
