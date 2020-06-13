using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class Parameters : Form
    {
        private string[] Queries { get; } 
        public Parameters()
        {
            InitializeComponent();
            Queries = DBConnector.QueriesBuilding;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0)
                checkedListBox1.SetItemCheckState(checkedListBox1.SelectedIndex, CheckState.Indeterminate);
            else if (checkedListBox1.CheckedItems.Count == 1)
                checkedListBox1.SetItemCheckState(checkedListBox1.Items.IndexOf(checkedListBox1.CheckedItems[0]), CheckState.Indeterminate);
            else if (checkedListBox1.CheckedItems.Count == 2)
                for (int i = 0; i < checkedListBox1.CheckedItems.Count;i++)
                    checkedListBox1.SetItemCheckState(checkedListBox1.Items.IndexOf(checkedListBox1.CheckedItems[i]), CheckState.Checked);
        }

        private void Parameters_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                if ((Owner as MainForm).Colomns.Contains(checkedListBox1.Items[i]))
                    checkedListBox1.SetItemChecked(i, true);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            (Owner as MainForm).Colomns = new List<string>();
            (Owner as MainForm).toQuery = new List<string>();
            foreach (string item in checkedListBox1.CheckedItems)
            {
                (Owner as MainForm).Colomns.Add(item);
                switch (item)
                {
                    case "Дисциплина":
                        (Owner as MainForm).toQuery.Add(Queries[0]);
                        break;
                    case "Дата и время":
                        (Owner as MainForm).toQuery.Add(Queries[1]);
                        break;
                    case "Учебная группа":
                        (Owner as MainForm).toQuery.Add(Queries[2]);
                        break;
                    case "Подгруппа":
                        (Owner as MainForm).toQuery.Add(Queries[3]);
                        break;
                    case "Аудитория":
                        (Owner as MainForm).toQuery.Add(Queries[4]);
                        break;
                    case "Корпус":
                        (Owner as MainForm).toQuery.Add(Queries[5]);
                        break;
                    case "Тип занятия":
                        (Owner as MainForm).toQuery.Add(Queries[6]);
                        break;
                    case "Преподаватель":
                        (Owner as MainForm).toQuery.Add(Queries[7]);
                        break;
                    case "Тип обучения":
                        (Owner as MainForm).toQuery.Add(Queries[8]);
                        (Owner as MainForm).toQuery.Add("edu_prog|ID|-|-|edu_groups|edu_prog");
                        (Owner as MainForm).toQuery.Add("edu_groups|ID|-|-|timetable|edu_group");
                        break;
                    case "Образовательная программа":
                        (Owner as MainForm).toQuery.Add(Queries[9]);
                        (Owner as MainForm).toQuery.Add("edu_groups|ID|-|-|timetable|edu_group");
                        break;
                    case "Курс":
                        (Owner as MainForm).toQuery.Add(Queries[10]);
                        break;
                }
            }

            DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void checkedListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0)
                checkedListBox1.SetItemCheckState(checkedListBox1.SelectedIndex, CheckState.Indeterminate);
        }
    }
}
