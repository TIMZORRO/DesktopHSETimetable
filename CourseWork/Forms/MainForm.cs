using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class MainForm : Form
    {
        public DBConnector DBC { get; set; } = new DBConnector();
        public List<string> Colomns { get; set; } = new List<string>();// { "Дисциплина", "Преподаватель", "Аудитория", "Корпус", "Учебная группа", "Подгруппа", "Тип занятия", "Дата и время" };
        public List<string> toQuery { get; set; } = new List<string>();
        public List<string> Filters { get; set; } = new List<string>();
        public List<object[]> ColorConditions { get; set; } = new List<object[]>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Start();
        }

        #region StartUpMethods
        private void Start()
        {
            ConfigBuilder configBuilder = new ConfigBuilder();
            configBuilder.StartConfig();
            Colomns = configBuilder.Colomns;
            ColorConditions = configBuilder.ColorConditions;
            Filters = configBuilder.Filters;

            FilterApplication();
        }

        private void FilterApplication()
        {
            foreach (string item in Colomns)
                for (int i = 0; i < DBConnector.RussianNameColumn.Count(); i++)
                    if (item == DBConnector.RussianNameColumn[i])
                    {
                        toQuery.Add(DBConnector.QueriesBuilding[i]);
                        break;
                    }

            toQuery.AddRange(Filters);
            DBC.Query(toQuery);
            DBC.ListUpdate();
            dataGridView1.DataSource = DBC.dt;
            cbTeacher.DataSource = DBC.Professors;
            cbCourse.DataSource = DBC.Courses;

            foreach (string item in Filters)
                if (item.Split('|')[0] == "professors")
                    cbTeacher.SelectedIndex = cbTeacher.Items.IndexOf(item.Split('\'')[1]);
                else if (item.Split('|')[0] == "courses")
                    cbCourse.SelectedIndex = cbCourse.Items.IndexOf(item.Split('\'')[1]);
                else if (item.Split('|')[0] == "timetable")
                {
                    dateStart.Value = Convert.ToDateTime(item.Split('|')[7]);
                    dateEnd.Value = Convert.ToDateTime(item.Split('|')[8]);
                }

            foreach (DataGridViewRow row in dataGridView1.Rows)
                row.DefaultCellStyle.BackColor = Color.White;

            for (int i = 0; i < ColorConditions.Count; i++)
                Coloring(i);
        }

        private void Coloring(int index)
        {
            object[] arr = ColorConditions[index];
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                if (dataGridView1.Columns[i].HeaderCell.Value.ToString() == arr[1].ToString())
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        switch (arr[2].ToString())
                        {
                            case "Равно":
                                if (dataGridView1[i, j].FormattedValue.ToString().Trim() == arr[3].ToString())
                                    dataGridView1.Rows[j].DefaultCellStyle.BackColor = (Color)arr[4];
                                break;
                            case "Больше":
                                if (Int32.TryParse(dataGridView1[i, j].FormattedValue.ToString(), out _) && (Int32.TryParse(arr[3].ToString(), out _)))
                                    if (Convert.ToInt32(dataGridView1[i, j].FormattedValue.ToString()) > Convert.ToInt32(arr[3].ToString()))
                                        dataGridView1.Rows[j].DefaultCellStyle.BackColor = (Color)arr[4];
                                break;
                            case "Меньше":
                                if (Int32.TryParse(dataGridView1[i, j].FormattedValue.ToString(), out _) && (Int32.TryParse(arr[3].ToString(), out _)))
                                    if (Convert.ToInt32(dataGridView1[i, j].FormattedValue.ToString()) < Convert.ToInt32(arr[3].ToString()))
                                        dataGridView1.Rows[j].DefaultCellStyle.BackColor = (Color)arr[4];
                                break;
                            case "Больше или равно":
                                if (Int32.TryParse(dataGridView1[i, j].FormattedValue.ToString(), out _) && (Int32.TryParse(arr[3].ToString(), out _)))
                                    if (Convert.ToInt32(dataGridView1[i, j].FormattedValue.ToString()) >= Convert.ToInt32(arr[3].ToString()))
                                        dataGridView1.Rows[j].DefaultCellStyle.BackColor = (Color)arr[4];
                                break;
                            case "Меньше или равно":
                                if (Int32.TryParse(dataGridView1[i, j].FormattedValue.ToString(), out _) && (Int32.TryParse(arr[3].ToString(), out _)))
                                    if (Convert.ToInt32(dataGridView1[i, j].FormattedValue.ToString()) <= Convert.ToInt32(arr[3].ToString()))
                                        dataGridView1.Rows[j].DefaultCellStyle.BackColor = (Color)arr[4];
                                break;
                        }
        }
        #endregion StartUpMethods

        #region StripMenuItems
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorHighlight form = new ColorHighlight();
            form.Owner = (Form)this;
            form.ShowDialog();

            if (form.DialogResult != DialogResult.Cancel)
                Coloring(ColorConditions.Count - 1);
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListColorHighlight form = new ListColorHighlight();
            form.ShowDialog(this);

            foreach (DataGridViewRow row in dataGridView1.Rows)
                row.DefaultCellStyle.BackColor = Color.White;

            for (int i = 0; i < ColorConditions.Count; i++)
                Coloring(i);
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы собираетесь обновить базу данных, добавив в нее свежие данные о расписании. Это займет несколько минут. Продолжить?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                DBC.DBUpdate();
                DBC.Query(toQuery);
                DBC.ListUpdate();
                //dataGridView1.DataSource = DBC.dt;
                //for (int i = 0; i < ColorConditions.Count; i++)
                //    Coloring(i);
                FilterApplication();

                MessageBox.Show("Данные обновлены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void параметрыРасписанияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parameters form = new Parameters();
            form.Owner = this;
            form.ShowDialog();
            if (form.DialogResult != DialogResult.Cancel)
            {
                toQuery.AddRange(Filters);
                DBC.Query(toQuery);
                dataGridView1.DataSource = DBC.dt;
                for (int i = 0; i < ColorConditions.Count; i++)
                    Coloring(i);
            }
        }

        private void сохранитьКонфигурациюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigBuilder cb = new ConfigBuilder();
            cb.Colomns = Colomns;
            cb.Filters = Filters;
            cb.ColorConditions = ColorConditions;

            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = "Config Files|*.config";
            SFD.Title = "Сохранение файла с текущими настройками";
            SFD.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"config";
            if (SFD.ShowDialog()==DialogResult.OK)
                cb.ConfigWriter(SFD.FileName);
        }

        private void выбратьНастройкиИзСохраненныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "Config Files|*.config";
            OFD.Title = "Открытие файла с сохраненными настройками";
            OFD.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"config";
            if (OFD.ShowDialog() == DialogResult.OK) 
            {
                ConfigBuilder cb = new ConfigBuilder();
                cb.ConfigReader(OFD.FileName);
                Colomns = cb.Colomns;
                ColorConditions = cb.ColorConditions;
                Filters = cb.Filters;
                cb.ChangeStartPath(OFD.FileName);
                toQuery = new List<string>();

                FilterApplication();
            }
        }
        #endregion StripMenuItems

        #region MainFormFilters
        private void cbTeacher_SelectionChangeCommitted(object sender, EventArgs e)
        {
            foreach (string item in toQuery)
            {
                string[] partitem = item.Split('|');
                if (partitem.Length > 6 && partitem[0] == "professors")
                {
                    toQuery.Remove(item);
                    break;
                }
            }
            foreach (string item in Filters)
            {
                string[] partitem = item.Split('|');
                if (partitem.Length > 6 && partitem[0] == "professors")
                {
                    Filters.Remove(item);
                    break;
                }
            }
            toQuery.Add("professors|ID|-|-|timetable|professor|name|'" + (cbTeacher.SelectedItem as string).Trim() + "'");
            Filters.Add("professors|ID|-|-|timetable|professor|name|'" + (cbTeacher.SelectedItem as string).Trim() + "'");
            DBC.Query(toQuery);
            dataGridView1.DataSource = DBC.dt;
        }

        private void cbCourse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            foreach (string item in toQuery)
            {
                string[] partitem = item.Split('|');
                if (partitem.Length > 6 && partitem[0] == "courses")
                {
                    toQuery.Remove(item);
                    break;
                }
            }
            foreach (string item in Filters)
            {
                string[] partitem = item.Split('|');
                if (partitem.Length > 6 && partitem[0] == "courses")
                {
                    Filters.Remove(item);
                    break;
                }
            }
            toQuery.Add("courses|ID|-|-|timetable|course|name|'" + (cbCourse.SelectedItem as string).Trim() + "'");
            Filters.Add("courses|ID|-|-|timetable|course|name|'" + (cbCourse.SelectedItem as string).Trim() + "'");
            DBC.Query(toQuery);
            dataGridView1.DataSource = DBC.dt;
        }

        private void timebutton_Click(object sender, EventArgs e)
        {
            if (dateStart.Value > dateEnd.Value)
                dateStart.Value = dateEnd.Value;
            foreach (string item in toQuery)
            {
                string[] partitem = item.Split('|');
                if (partitem.Length == 9)
                {
                    toQuery.Remove(item);
                    break;
                }
            }
            foreach (string item in Filters)
            {
                string[] partitem = item.Split('|');
                if (partitem.Length == 9)
                {
                    Filters.Remove(item);
                    break;
                }
            }
            toQuery.Add("timetable|-|-|-|-|-|date|" + dateStart.Value + "|" + dateEnd.Value);
            Filters.Add("timetable|-|-|-|-|-|date|" + dateStart.Value + "|" + dateEnd.Value);
            DBC.Query(toQuery);
            dataGridView1.DataSource = DBC.dt;
        }
        #endregion MainFormFilters
    }
}
