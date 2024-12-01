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
            this.SuspendLayout();
            // 
            // listBoxSkis
            // 
            this.listBoxSkis.FormattingEnabled = true;
            this.listBoxSkis.Location = new System.Drawing.Point(12, 12);
            this.listBoxSkis.Name = "listBoxSkis";
            this.listBoxSkis.Size = new System.Drawing.Size(776, 121);
            this.listBoxSkis.TabIndex = 0;
            // 
            // buttonRent
            // 
            this.buttonRent.Location = new System.Drawing.Point(138, 174);
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
            this.buttonReturn.Location = new System.Drawing.Point(521, 174);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(124, 45);
            this.buttonReturn.TabIndex = 3;
            this.buttonReturn.Text = "Вернуть";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.listBoxRentedSkis);
            this.Controls.Add(this.buttonRent);
            this.Controls.Add(this.listBoxSkis);
            this.Name = "FormClient";
            this.Text = "FormClient";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxSkis;
        private System.Windows.Forms.Button buttonRent;
        private System.Windows.Forms.ListBox listBoxRentedSkis;
        private System.Windows.Forms.Button buttonReturn;
    }
}