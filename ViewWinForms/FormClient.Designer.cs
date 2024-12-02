namespace ViewWinForms
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
            this.comboBoxOrderBy = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
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
            // comboBoxOrderBy
            // 
            this.comboBoxOrderBy.FormattingEnabled = true;
            this.comboBoxOrderBy.Location = new System.Drawing.Point(15, 25);
            this.comboBoxOrderBy.Name = "comboBoxOrderBy";
            this.comboBoxOrderBy.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOrderBy.TabIndex = 5;
            this.comboBoxOrderBy.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrderBy_SelectedIndexChanged);
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
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxOrderBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.listBoxRentedSkis);
            this.Controls.Add(this.buttonRent);
            this.Controls.Add(this.listBoxSkis);
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
        private System.Windows.Forms.ComboBox comboBoxOrderBy;
        private System.Windows.Forms.Label label3;
    }
}