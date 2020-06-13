namespace CourseWork
{
    partial class ColorHighlight
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelParam = new System.Windows.Forms.Label();
            this.cbParam = new System.Windows.Forms.ComboBox();
            this.labelCondition = new System.Windows.Forms.Label();
            this.tbCondition = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.labelColor = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnColor = new System.Windows.Forms.Button();
            this.cbCondition = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.showColor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelParam
            // 
            this.labelParam.AutoSize = true;
            this.labelParam.Location = new System.Drawing.Point(12, 37);
            this.labelParam.Name = "labelParam";
            this.labelParam.Size = new System.Drawing.Size(151, 17);
            this.labelParam.TabIndex = 0;
            this.labelParam.Text = "Параметр выделения";
            // 
            // cbParam
            // 
            this.cbParam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParam.FormattingEnabled = true;
            this.cbParam.Location = new System.Drawing.Point(172, 34);
            this.cbParam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbParam.Name = "cbParam";
            this.cbParam.Size = new System.Drawing.Size(158, 24);
            this.cbParam.TabIndex = 1;
            // 
            // labelCondition
            // 
            this.labelCondition.AutoSize = true;
            this.labelCondition.Location = new System.Drawing.Point(11, 94);
            this.labelCondition.Name = "labelCondition";
            this.labelCondition.Size = new System.Drawing.Size(63, 17);
            this.labelCondition.TabIndex = 2;
            this.labelCondition.Text = "Условие";
            // 
            // tbCondition
            // 
            this.tbCondition.Location = new System.Drawing.Point(172, 91);
            this.tbCondition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbCondition.Name = "tbCondition";
            this.tbCondition.Size = new System.Drawing.Size(158, 22);
            this.tbCondition.TabIndex = 3;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 11);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(72, 17);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Название";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(172, 8);
            this.tbName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(158, 22);
            this.tbName.TabIndex = 5;
            // 
            // labelColor
            // 
            this.labelColor.AutoSize = true;
            this.labelColor.Location = new System.Drawing.Point(12, 125);
            this.labelColor.Name = "labelColor";
            this.labelColor.Size = new System.Drawing.Size(41, 17);
            this.labelColor.TabIndex = 6;
            this.labelColor.Text = "Цвет";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(250, 153);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(80, 28);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(11, 153);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 28);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(172, 119);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(158, 29);
            this.btnColor.TabIndex = 10;
            this.btnColor.Text = "Настроить цвет";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btncolor_Click);
            // 
            // cbCondition
            // 
            this.cbCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCondition.FormattingEnabled = true;
            this.cbCondition.Items.AddRange(new object[] {
            "Равно",
            "Больше",
            "Меньше",
            "Меньше или равно",
            "Больше или равно"});
            this.cbCondition.Location = new System.Drawing.Point(172, 62);
            this.cbCondition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCondition.Name = "cbCondition";
            this.cbCondition.Size = new System.Drawing.Size(158, 24);
            this.cbCondition.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Сравнение";
            // 
            // showColor
            // 
            this.showColor.Enabled = false;
            this.showColor.Location = new System.Drawing.Point(59, 122);
            this.showColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.showColor.Name = "showColor";
            this.showColor.Size = new System.Drawing.Size(27, 22);
            this.showColor.TabIndex = 13;
            // 
            // ColorHighlight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(346, 200);
            this.Controls.Add(this.showColor);
            this.Controls.Add(this.cbCondition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelColor);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.tbCondition);
            this.Controls.Add(this.labelCondition);
            this.Controls.Add(this.cbParam);
            this.Controls.Add(this.labelParam);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(364, 247);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(364, 247);
            this.Name = "ColorHighlight";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Цветовое выделение";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ColorHighlight_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelParam;
        private System.Windows.Forms.ComboBox cbParam;
        private System.Windows.Forms.Label labelCondition;
        private System.Windows.Forms.TextBox tbCondition;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label labelColor;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.ComboBox cbCondition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox showColor;
    }
}