namespace DataBase_Project
{
    partial class AddGroup
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
            this.label5 = new System.Windows.Forms.Label();
            this.SpecNames = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CodeBox = new System.Windows.Forms.TextBox();
            this.Accepted = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.YearAccepted = new System.Windows.Forms.ComboBox();
            this.FirstYear = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SecondYear = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ThirdYear = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.FourthYear = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.FifthYear = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Ended = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.StudyTime = new System.Windows.Forms.ComboBox();
            this.AddRecord = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(243, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(219, 23);
            this.label5.TabIndex = 50;
            this.label5.Text = "Обучается на специальности";
            // 
            // SpecNames
            // 
            this.SpecNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SpecNames.FormattingEnabled = true;
            this.SpecNames.Location = new System.Drawing.Point(158, 34);
            this.SpecNames.Name = "SpecNames";
            this.SpecNames.Size = new System.Drawing.Size(486, 26);
            this.SpecNames.TabIndex = 49;
            this.SpecNames.SelectedIndexChanged += new System.EventHandler(this.SpecNames_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(178, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 23);
            this.label4.TabIndex = 48;
            this.label4.Text = "Отделение";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 23);
            this.label2.TabIndex = 44;
            this.label2.Text = "Год поступления";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 23);
            this.label1.TabIndex = 42;
            this.label1.Text = "Код группы";
            // 
            // CodeBox
            // 
            this.CodeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CodeBox.Location = new System.Drawing.Point(4, 35);
            this.CodeBox.Name = "CodeBox";
            this.CodeBox.Size = new System.Drawing.Size(138, 24);
            this.CodeBox.TabIndex = 41;
            // 
            // Accepted
            // 
            this.Accepted.Location = new System.Drawing.Point(28, 171);
            this.Accepted.Name = "Accepted";
            this.Accepted.Size = new System.Drawing.Size(81, 20);
            this.Accepted.TabIndex = 52;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(12, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 48);
            this.label6.TabIndex = 51;
            this.label6.Text = "Принято на 1 курс (формально)";
            // 
            // YearAccepted
            // 
            this.YearAccepted.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.YearAccepted.FormattingEnabled = true;
            this.YearAccepted.Location = new System.Drawing.Point(15, 94);
            this.YearAccepted.Name = "YearAccepted";
            this.YearAccepted.Size = new System.Drawing.Size(109, 26);
            this.YearAccepted.TabIndex = 53;
            this.YearAccepted.SelectedIndexChanged += new System.EventHandler(this.YearAccepted_SelectedIndexChanged);
            // 
            // FirstYear
            // 
            this.FirstYear.Location = new System.Drawing.Point(163, 171);
            this.FirstYear.Name = "FirstYear";
            this.FirstYear.Size = new System.Drawing.Size(81, 20);
            this.FirstYear.TabIndex = 56;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(147, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 48);
            this.label7.TabIndex = 55;
            this.label7.Text = "Обучается на 1 курсе";
            // 
            // SecondYear
            // 
            this.SecondYear.Location = new System.Drawing.Point(281, 171);
            this.SecondYear.Name = "SecondYear";
            this.SecondYear.Size = new System.Drawing.Size(81, 20);
            this.SecondYear.TabIndex = 58;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(265, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 48);
            this.label8.TabIndex = 57;
            this.label8.Text = "Обучается на 2 курсе";
            // 
            // ThirdYear
            // 
            this.ThirdYear.Location = new System.Drawing.Point(401, 171);
            this.ThirdYear.Name = "ThirdYear";
            this.ThirdYear.Size = new System.Drawing.Size(81, 20);
            this.ThirdYear.TabIndex = 60;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(385, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 48);
            this.label9.TabIndex = 59;
            this.label9.Text = "Обучается на 3 курсе";
            // 
            // FourthYear
            // 
            this.FourthYear.Location = new System.Drawing.Point(521, 171);
            this.FourthYear.Name = "FourthYear";
            this.FourthYear.Size = new System.Drawing.Size(81, 20);
            this.FourthYear.TabIndex = 62;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(505, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 48);
            this.label10.TabIndex = 61;
            this.label10.Text = "Обучается на 4 курсе";
            // 
            // FifthYear
            // 
            this.FifthYear.Location = new System.Drawing.Point(28, 236);
            this.FifthYear.Name = "FifthYear";
            this.FifthYear.Size = new System.Drawing.Size(81, 20);
            this.FifthYear.TabIndex = 64;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(12, 195);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 48);
            this.label11.TabIndex = 63;
            this.label11.Text = "Обучается на 5 курсе";
            // 
            // Ended
            // 
            this.Ended.Location = new System.Drawing.Point(158, 236);
            this.Ended.Name = "Ended";
            this.Ended.Size = new System.Drawing.Size(81, 20);
            this.Ended.TabIndex = 66;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(160, 215);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 28);
            this.label12.TabIndex = 65;
            this.label12.Text = "Окончило";
            // 
            // StudyTime
            // 
            this.StudyTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StudyTime.FormattingEnabled = true;
            this.StudyTime.Location = new System.Drawing.Point(158, 94);
            this.StudyTime.Name = "StudyTime";
            this.StudyTime.Size = new System.Drawing.Size(127, 26);
            this.StudyTime.TabIndex = 67;
            // 
            // AddRecord
            // 
            this.AddRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddRecord.Location = new System.Drawing.Point(281, 215);
            this.AddRecord.Name = "AddRecord";
            this.AddRecord.Size = new System.Drawing.Size(181, 46);
            this.AddRecord.TabIndex = 68;
            this.AddRecord.Text = "Добавить запись";
            this.AddRecord.UseVisualStyleBackColor = true;
            this.AddRecord.Click += new System.EventHandler(this.AddRecord_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(308, 94);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 18);
            this.label13.TabIndex = 70;
            this.label13.Text = "label13";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(398, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 18);
            this.label3.TabIndex = 71;
            this.label3.Text = "label3";
            // 
            // AddGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 271);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.AddRecord);
            this.Controls.Add(this.StudyTime);
            this.Controls.Add(this.Ended);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.FifthYear);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.FourthYear);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ThirdYear);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.SecondYear);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.FirstYear);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.YearAccepted);
            this.Controls.Add(this.Accepted);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SpecNames);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CodeBox);
            this.Name = "AddGroup";
            this.Text = "Добавить группу";
            this.Load += new System.EventHandler(this.AddGroup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox SpecNames;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CodeBox;
        private System.Windows.Forms.TextBox Accepted;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox YearAccepted;
        private System.Windows.Forms.TextBox FirstYear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox SecondYear;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ThirdYear;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox FourthYear;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox FifthYear;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Ended;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox StudyTime;
        private System.Windows.Forms.Button AddRecord;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
    }
}