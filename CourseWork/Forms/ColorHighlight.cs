using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class ColorHighlight : Form
    {
        private Color UserColor = Color.Red;
        private int StartIndex;
        public ColorHighlight()
        {
            InitializeComponent();
            cbParam.DataSource = DBConnector.RussianNameColumn;
            colorDialog1.FullOpen = true;
            showColor.BackColor = UserColor;
        }

        public ColorHighlight(int index)
        {
            InitializeComponent();
            cbParam.DataSource = DBConnector.RussianNameColumn;
            colorDialog1.FullOpen = true;
            showColor.BackColor = UserColor;
            StartIndex = index;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "" || tbCondition.Text == "" || cbCondition.Text == "") 
            {
                MessageBox.Show("Пожалуйста, заполните все поля формы", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            object[] ans = new object[5];
            ans[0] = tbName.Text;
            ans[1] = cbParam.Text;
            ans[2] = cbCondition.Text;
            ans[3] = tbCondition.Text;
            ans[4] = UserColor;

            List<object[]> conditions = new List<object[]>();
            if (Owner is MainForm)
            {
                conditions = (Owner as MainForm).ColorConditions;

                foreach (object[] item in conditions)
                    if (item[0].ToString() == tbName.Text.Trim())
                    {
                        MessageBox.Show("Введенное вами название уже используется. Пожалуйста, измените название условия", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                conditions.Add(ans);
            }
            else if (Owner is ListColorHighlight)
            {
                conditions = (Owner.Owner as MainForm).ColorConditions;

                conditions[StartIndex] = ans;
            }
            
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ColorHighlight_Load(object sender, EventArgs e)
        {
            if (Owner is ListColorHighlight)
            {
                object[] condition = (Owner.Owner as MainForm).ColorConditions[StartIndex];
                tbName.Text = condition[0].ToString();
                cbParam.Text = condition[1].ToString();
                cbCondition.Text = condition[2].ToString();
                tbCondition.Text = condition[3].ToString();
                UserColor = (Color)condition[4];
                showColor.BackColor = UserColor;
            }
        }

        private void btncolor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            UserColor = colorDialog1.Color;
            showColor.BackColor = UserColor;
        }
    }
}
