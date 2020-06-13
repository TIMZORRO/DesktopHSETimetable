using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class ListColorHighlight : Form
    {
        private List<string> Data { get; set; } = new List<string>();
        public ListColorHighlight()
        {
            InitializeComponent();
        }

        private void ListColorHighlight_Load(object sender, EventArgs e)
        {
            foreach (object[] item in (Owner as MainForm).ColorConditions)
                Data.Add(item[0].ToString());
            listBox1.DataSource = Data;
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            ColorHighlight form = new ColorHighlight(listBox1.SelectedIndex);
            form.Owner = this;
            form.ShowDialog();
            Data[listBox1.SelectedIndex] = (Owner as MainForm).ColorConditions[listBox1.SelectedIndex][0].ToString();
            listBox1.DataSource = null;
            listBox1.DataSource = Data;
            //listBox1.ResetText();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы собираетесь безвозвратно удалить выбранный элемент. Продолжить?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                (Owner as MainForm).ColorConditions.RemoveAt(listBox1.SelectedIndex);
                Data.RemoveAt(listBox1.SelectedIndex);
                listBox1.DataSource = null;
                listBox1.DataSource = Data;
            }
        }
    }
}
