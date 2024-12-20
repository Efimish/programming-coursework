﻿namespace ViewWinForms
{
    partial class FormClient
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
            this.listBoxSkis = new System.Windows.Forms.ListBox();
            this.buttonRent = new System.Windows.Forms.Button();
            this.listBoxRentedSkis = new System.Windows.Forms.ListBox();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxFilter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxSkis
            // 
            this.listBoxSkis.FormattingEnabled = true;
            this.listBoxSkis.Location = new System.Drawing.Point(12, 91);
            this.listBoxSkis.Name = "listBoxSkis";
            this.listBoxSkis.Size = new System.Drawing.Size(776, 134);
            this.listBoxSkis.TabIndex = 0;
            // 
            // buttonRent
            // 
            this.buttonRent.Location = new System.Drawing.Point(138, 244);
            this.buttonRent.Name = "buttonRent";
            this.buttonRent.Size = new System.Drawing.Size(124, 45);
            this.buttonRent.TabIndex = 1;
            this.buttonRent.Text = "Арендовать";
            this.buttonRent.UseVisualStyleBackColor = true;
            this.buttonRent.Click += new System.EventHandler(this.buttonRent_Click);
            // 
            // listBoxRentedSkis
            // 
            this.listBoxRentedSkis.FormattingEnabled = true;
            this.listBoxRentedSkis.Location = new System.Drawing.Point(12, 317);
            this.listBoxRentedSkis.Name = "listBoxRentedSkis";
            this.listBoxRentedSkis.Size = new System.Drawing.Size(776, 121);
            this.listBoxRentedSkis.TabIndex = 2;
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(521, 244);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(124, 45);
            this.buttonReturn.TabIndex = 3;
            this.buttonReturn.Text = "Вернуть";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Достпные для аренды лыжи";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Арендованные Вами лыжи";
            // 
            // comboBoxFilter
            // 
            this.comboBoxFilter.FormattingEnabled = true;
            this.comboBoxFilter.Location = new System.Drawing.Point(15, 25);
            this.comboBoxFilter.Name = "comboBoxFilter";
            this.comboBoxFilter.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFilter.TabIndex = 5;
            this.comboBoxFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrderBy_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Фильтрация";
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(664, 9);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(124, 45);
            this.buttonExit.TabIndex = 7;
            this.buttonExit.Text = "Выйти из системы";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(142, 25);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(100, 20);
            this.textBoxFilter.TabIndex = 8;
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(248, 25);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(75, 21);
            this.buttonFilter.TabIndex = 9;
            this.buttonFilter.Text = "применить";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.textBoxFilter);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.listBoxRentedSkis);
            this.Controls.Add(this.buttonRent);
            this.Controls.Add(this.listBoxSkis);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormClient";
            this.Text = "Аренда лыж";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxSkis;
        private System.Windows.Forms.Button buttonRent;
        private System.Windows.Forms.ListBox listBoxRentedSkis;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.Button buttonFilter;
    }
}