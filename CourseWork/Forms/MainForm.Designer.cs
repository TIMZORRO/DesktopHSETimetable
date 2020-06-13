namespace CourseWork
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.цветовоеВыделениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.праметрыРасписанияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКонфигурациюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьНастройкиИзСохраненныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelTeacher = new System.Windows.Forms.Label();
            this.labelDateStart = new System.Windows.Forms.Label();
            this.cbTeacher = new System.Windows.Forms.ComboBox();
            this.dateStart = new System.Windows.Forms.DateTimePicker();
            this.labelCourse = new System.Windows.Forms.Label();
            this.cbCourse = new System.Windows.Forms.ComboBox();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.labelDateEnd = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.teachersTimetableDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timetableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timebutton = new System.Windows.Forms.Button();
            this.dBConnectorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teachersTimetableDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timetableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBConnectorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкаToolStripMenuItem,
            this.обновитьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(978, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкаToolStripMenuItem
            // 
            this.настройкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.цветовоеВыделениеToolStripMenuItem,
            this.праметрыРасписанияToolStripMenuItem,
            this.сохранитьКонфигурациюToolStripMenuItem,
            this.выбратьНастройкиИзСохраненныхToolStripMenuItem});
            this.настройкаToolStripMenuItem.Name = "настройкаToolStripMenuItem";
            this.настройкаToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
            this.настройкаToolStripMenuItem.Text = "Настройка";
            // 
            // цветовоеВыделениеToolStripMenuItem
            // 
            this.цветовоеВыделениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.изменитьToolStripMenuItem});
            this.цветовоеВыделениеToolStripMenuItem.Name = "цветовоеВыделениеToolStripMenuItem";
            this.цветовоеВыделениеToolStripMenuItem.Size = new System.Drawing.Size(346, 26);
            this.цветовоеВыделениеToolStripMenuItem.Text = "Цветовое выделение";
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click);
            // 
            // изменитьToolStripMenuItem
            // 
            this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.изменитьToolStripMenuItem.Text = "Изменить или удалить";
            this.изменитьToolStripMenuItem.Click += new System.EventHandler(this.изменитьToolStripMenuItem_Click);
            // 
            // праметрыРасписанияToolStripMenuItem
            // 
            this.праметрыРасписанияToolStripMenuItem.Name = "праметрыРасписанияToolStripMenuItem";
            this.праметрыРасписанияToolStripMenuItem.Size = new System.Drawing.Size(346, 26);
            this.праметрыРасписанияToolStripMenuItem.Text = "Параметры расписания";
            this.праметрыРасписанияToolStripMenuItem.Click += new System.EventHandler(this.параметрыРасписанияToolStripMenuItem_Click);
            // 
            // сохранитьКонфигурациюToolStripMenuItem
            // 
            this.сохранитьКонфигурациюToolStripMenuItem.Name = "сохранитьКонфигурациюToolStripMenuItem";
            this.сохранитьКонфигурациюToolStripMenuItem.Size = new System.Drawing.Size(346, 26);
            this.сохранитьКонфигурациюToolStripMenuItem.Text = "Сохранить настройки";
            this.сохранитьКонфигурациюToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКонфигурациюToolStripMenuItem_Click);
            // 
            // выбратьНастройкиИзСохраненныхToolStripMenuItem
            // 
            this.выбратьНастройкиИзСохраненныхToolStripMenuItem.Name = "выбратьНастройкиИзСохраненныхToolStripMenuItem";
            this.выбратьНастройкиИзСохраненныхToolStripMenuItem.Size = new System.Drawing.Size(346, 26);
            this.выбратьНастройкиИзСохраненныхToolStripMenuItem.Text = "Выбрать настройки из сохраненных";
            this.выбратьНастройкиИзСохраненныхToolStripMenuItem.Click += new System.EventHandler(this.выбратьНастройкиИзСохраненныхToolStripMenuItem_Click);
            // 
            // обновитьToolStripMenuItem
            // 
            this.обновитьToolStripMenuItem.Name = "обновитьToolStripMenuItem";
            this.обновитьToolStripMenuItem.Size = new System.Drawing.Size(92, 24);
            this.обновитьToolStripMenuItem.Text = "Обновить";
            this.обновитьToolStripMenuItem.Click += new System.EventHandler(this.обновитьToolStripMenuItem_Click);
            // 
            // labelTeacher
            // 
            this.labelTeacher.AutoSize = true;
            this.labelTeacher.Location = new System.Drawing.Point(11, 32);
            this.labelTeacher.Name = "labelTeacher";
            this.labelTeacher.Size = new System.Drawing.Size(111, 17);
            this.labelTeacher.TabIndex = 1;
            this.labelTeacher.Text = "Преподаватель";
            // 
            // labelDateStart
            // 
            this.labelDateStart.AutoSize = true;
            this.labelDateStart.Location = new System.Drawing.Point(11, 60);
            this.labelDateStart.Name = "labelDateStart";
            this.labelDateStart.Size = new System.Drawing.Size(58, 17);
            this.labelDateStart.TabIndex = 2;
            this.labelDateStart.Text = "Начало";
            // 
            // cbTeacher
            // 
            this.cbTeacher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTeacher.FormattingEnabled = true;
            this.cbTeacher.Location = new System.Drawing.Point(135, 30);
            this.cbTeacher.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbTeacher.Name = "cbTeacher";
            this.cbTeacher.Size = new System.Drawing.Size(218, 24);
            this.cbTeacher.TabIndex = 3;
            this.cbTeacher.SelectionChangeCommitted += new System.EventHandler(this.cbTeacher_SelectionChangeCommitted);
            // 
            // dateStart
            // 
            this.dateStart.Location = new System.Drawing.Point(76, 58);
            this.dateStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(160, 22);
            this.dateStart.TabIndex = 4;
            // 
            // labelCourse
            // 
            this.labelCourse.AutoSize = true;
            this.labelCourse.Location = new System.Drawing.Point(359, 32);
            this.labelCourse.Name = "labelCourse";
            this.labelCourse.Size = new System.Drawing.Size(90, 17);
            this.labelCourse.TabIndex = 5;
            this.labelCourse.Text = "Дисциплина";
            // 
            // cbCourse
            // 
            this.cbCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCourse.FormattingEnabled = true;
            this.cbCourse.Location = new System.Drawing.Point(455, 30);
            this.cbCourse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCourse.Name = "cbCourse";
            this.cbCourse.Size = new System.Drawing.Size(320, 24);
            this.cbCourse.TabIndex = 6;
            this.cbCourse.SelectionChangeCommitted += new System.EventHandler(this.cbCourse_SelectionChangeCommitted);
            // 
            // dateEnd
            // 
            this.dateEnd.Location = new System.Drawing.Point(328, 58);
            this.dateEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(160, 22);
            this.dateEnd.TabIndex = 8;
            // 
            // labelDateEnd
            // 
            this.labelDateEnd.AutoSize = true;
            this.labelDateEnd.Location = new System.Drawing.Point(241, 60);
            this.labelDateEnd.Name = "labelDateEnd";
            this.labelDateEnd.Size = new System.Drawing.Size(82, 17);
            this.labelDateEnd.TabIndex = 7;
            this.labelDateEnd.Text = "Окончание";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(14, 90);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(952, 423);
            this.dataGridView1.TabIndex = 9;
            // 
            // timebutton
            // 
            this.timebutton.Location = new System.Drawing.Point(505, 56);
            this.timebutton.Name = "timebutton";
            this.timebutton.Size = new System.Drawing.Size(139, 25);
            this.timebutton.TabIndex = 10;
            this.timebutton.Text = "Задать диапазон";
            this.timebutton.UseVisualStyleBackColor = true;
            this.timebutton.Click += new System.EventHandler(this.timebutton_Click);
            // 
            // dBConnectorBindingSource
            // 
            this.dBConnectorBindingSource.DataSource = typeof(CourseWork.DBConnector);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 524);
            this.Controls.Add(this.timebutton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dateEnd);
            this.Controls.Add(this.labelDateEnd);
            this.Controls.Add(this.cbCourse);
            this.Controls.Add(this.labelCourse);
            this.Controls.Add(this.dateStart);
            this.Controls.Add(this.cbTeacher);
            this.Controls.Add(this.labelDateStart);
            this.Controls.Add(this.labelTeacher);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "MainForm";
            this.Text = "Расписание";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teachersTimetableDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timetableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBConnectorBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem цветовоеВыделениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem праметрыРасписанияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обновитьToolStripMenuItem;
        private System.Windows.Forms.Label labelTeacher;
        private System.Windows.Forms.Label labelDateStart;
        private System.Windows.Forms.ComboBox cbTeacher;
        private System.Windows.Forms.DateTimePicker dateStart;
        private System.Windows.Forms.Label labelCourse;
        private System.Windows.Forms.ComboBox cbCourse;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.Label labelDateEnd;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource teachersTimetableDataSetBindingSource;
        private System.Windows.Forms.BindingSource timetableBindingSource;
        private System.Windows.Forms.BindingSource dBConnectorBindingSource;
        private System.Windows.Forms.Button timebutton;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКонфигурациюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьНастройкиИзСохраненныхToolStripMenuItem;
    }
}

