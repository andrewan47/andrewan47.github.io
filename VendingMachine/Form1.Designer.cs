namespace VendingMachine
{
    partial class VendingChoice
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
            this.DollarBox = new System.Windows.Forms.PictureBox();
            this.DimeBox = new System.Windows.Forms.PictureBox();
            this.QuarterBox = new System.Windows.Forms.PictureBox();
            this.NickelBox = new System.Windows.Forms.PictureBox();
            this.Item2Button = new System.Windows.Forms.RadioButton();
            this.Item1Button = new System.Windows.Forms.RadioButton();
            this.Item3Button = new System.Windows.Forms.RadioButton();
            this.Item4Button = new System.Windows.Forms.RadioButton();
            this.Display = new System.Windows.Forms.TextBox();
            this.VendingGroup = new System.Windows.Forms.GroupBox();
            this.PurchaseBox = new System.Windows.Forms.GroupBox();
            this.ReturnCash = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DollarBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DimeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuarterBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NickelBox)).BeginInit();
            this.VendingGroup.SuspendLayout();
            this.PurchaseBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DollarBox
            // 
            this.DollarBox.Image = global::VendingMachine.Properties.Resources.dollarWashington;
            this.DollarBox.Location = new System.Drawing.Point(6, 22);
            this.DollarBox.Name = "DollarBox";
            this.DollarBox.Size = new System.Drawing.Size(125, 88);
            this.DollarBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DollarBox.TabIndex = 3;
            this.DollarBox.TabStop = false;
            this.DollarBox.Click += new System.EventHandler(this.DollarBox_Click);
            // 
            // DimeBox
            // 
            this.DimeBox.Image = global::VendingMachine.Properties.Resources.Dime_Obverse_13;
            this.DimeBox.Location = new System.Drawing.Point(19, 118);
            this.DimeBox.Name = "DimeBox";
            this.DimeBox.Size = new System.Drawing.Size(100, 90);
            this.DimeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DimeBox.TabIndex = 2;
            this.DimeBox.TabStop = false;
            this.DimeBox.Click += new System.EventHandler(this.DimeBox_Click);
            // 
            // QuarterBox
            // 
            this.QuarterBox.Image = global::VendingMachine.Properties.Resources.quarter_obverse;
            this.QuarterBox.Location = new System.Drawing.Point(154, 22);
            this.QuarterBox.Name = "QuarterBox";
            this.QuarterBox.Size = new System.Drawing.Size(100, 90);
            this.QuarterBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.QuarterBox.TabIndex = 1;
            this.QuarterBox.TabStop = false;
            this.QuarterBox.Click += new System.EventHandler(this.QuarterBox_Click);
            // 
            // NickelBox
            // 
            this.NickelBox.Image = global::VendingMachine.Properties.Resources.Jefferson_Nickel_Unc_Obv;
            this.NickelBox.Location = new System.Drawing.Point(154, 118);
            this.NickelBox.Name = "NickelBox";
            this.NickelBox.Size = new System.Drawing.Size(100, 90);
            this.NickelBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.NickelBox.TabIndex = 0;
            this.NickelBox.TabStop = false;
            this.NickelBox.Click += new System.EventHandler(this.NickelBox_Click);
            // 
            // Item2Button
            // 
            this.Item2Button.AutoSize = true;
            this.Item2Button.Location = new System.Drawing.Point(6, 71);
            this.Item2Button.Name = "Item2Button";
            this.Item2Button.Size = new System.Drawing.Size(67, 17);
            this.Item2Button.TabIndex = 4;
            this.Item2Button.Text = "Crackers";
            this.Item2Button.UseVisualStyleBackColor = true;
            this.Item2Button.CheckedChanged += new System.EventHandler(this.Item2Button_CheckedChanged);
            // 
            // Item1Button
            // 
            this.Item1Button.AutoSize = true;
            this.Item1Button.Location = new System.Drawing.Point(6, 37);
            this.Item1Button.Name = "Item1Button";
            this.Item1Button.Size = new System.Drawing.Size(64, 17);
            this.Item1Button.TabIndex = 5;
            this.Item1Button.Text = "Peanuts";
            this.Item1Button.UseVisualStyleBackColor = true;
            this.Item1Button.CheckedChanged += new System.EventHandler(this.Item1Button_CheckedChanged);
            // 
            // Item3Button
            // 
            this.Item3Button.AutoSize = true;
            this.Item3Button.Location = new System.Drawing.Point(6, 108);
            this.Item3Button.Name = "Item3Button";
            this.Item3Button.Size = new System.Drawing.Size(74, 17);
            this.Item3Button.TabIndex = 6;
            this.Item3Button.Text = "Candy Bar";
            this.Item3Button.UseVisualStyleBackColor = true;
            this.Item3Button.CheckedChanged += new System.EventHandler(this.Item3Button_CheckedChanged);
            // 
            // Item4Button
            // 
            this.Item4Button.AutoSize = true;
            this.Item4Button.Location = new System.Drawing.Point(6, 143);
            this.Item4Button.Name = "Item4Button";
            this.Item4Button.Size = new System.Drawing.Size(63, 17);
            this.Item4Button.TabIndex = 7;
            this.Item4Button.Text = "Cookies";
            this.Item4Button.UseVisualStyleBackColor = true;
            this.Item4Button.CheckedChanged += new System.EventHandler(this.Item4Button_CheckedChanged);
            // 
            // Display
            // 
            this.Display.Location = new System.Drawing.Point(112, 12);
            this.Display.Name = "Display";
            this.Display.ReadOnly = true;
            this.Display.Size = new System.Drawing.Size(166, 20);
            this.Display.TabIndex = 8;
            // 
            // VendingGroup
            // 
            this.VendingGroup.Controls.Add(this.Item1Button);
            this.VendingGroup.Controls.Add(this.Item2Button);
            this.VendingGroup.Controls.Add(this.Item4Button);
            this.VendingGroup.Controls.Add(this.Item3Button);
            this.VendingGroup.Location = new System.Drawing.Point(12, 49);
            this.VendingGroup.Name = "VendingGroup";
            this.VendingGroup.Size = new System.Drawing.Size(82, 177);
            this.VendingGroup.TabIndex = 9;
            this.VendingGroup.TabStop = false;
            this.VendingGroup.Text = "Vending";
            this.VendingGroup.Enter += new System.EventHandler(this.VendingGroup_Enter);
            // 
            // PurchaseBox
            // 
            this.PurchaseBox.Controls.Add(this.ReturnCash);
            this.PurchaseBox.Controls.Add(this.DollarBox);
            this.PurchaseBox.Controls.Add(this.DimeBox);
            this.PurchaseBox.Controls.Add(this.QuarterBox);
            this.PurchaseBox.Controls.Add(this.NickelBox);
            this.PurchaseBox.Location = new System.Drawing.Point(112, 49);
            this.PurchaseBox.Name = "PurchaseBox";
            this.PurchaseBox.Size = new System.Drawing.Size(260, 243);
            this.PurchaseBox.TabIndex = 10;
            this.PurchaseBox.TabStop = false;
            this.PurchaseBox.Text = "Deposit";
            this.PurchaseBox.Enter += new System.EventHandler(this.PurchaseBox_Enter);
            // 
            // ReturnCash
            // 
            this.ReturnCash.Location = new System.Drawing.Point(91, 214);
            this.ReturnCash.Name = "ReturnCash";
            this.ReturnCash.Size = new System.Drawing.Size(75, 23);
            this.ReturnCash.TabIndex = 11;
            this.ReturnCash.Text = "Return";
            this.ReturnCash.UseVisualStyleBackColor = true;
            this.ReturnCash.Click += new System.EventHandler(this.ReturnCash_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(7, 263);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 11;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // VendingChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 305);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.PurchaseBox);
            this.Controls.Add(this.VendingGroup);
            this.Controls.Add(this.Display);
            this.Name = "VendingChoice";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.DollarBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DimeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuarterBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NickelBox)).EndInit();
            this.VendingGroup.ResumeLayout(false);
            this.VendingGroup.PerformLayout();
            this.PurchaseBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox NickelBox;
        private System.Windows.Forms.PictureBox QuarterBox;
        private System.Windows.Forms.PictureBox DimeBox;
        private System.Windows.Forms.PictureBox DollarBox;
        private System.Windows.Forms.RadioButton Item2Button;
        private System.Windows.Forms.RadioButton Item1Button;
        private System.Windows.Forms.RadioButton Item3Button;
        private System.Windows.Forms.RadioButton Item4Button;
        private System.Windows.Forms.TextBox Display;
        private System.Windows.Forms.GroupBox VendingGroup;
        private System.Windows.Forms.GroupBox PurchaseBox;
        private System.Windows.Forms.Button ReturnCash;
        private System.Windows.Forms.Button LoadButton;
    }
}

