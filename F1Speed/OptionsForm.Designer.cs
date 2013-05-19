namespace F1Speed
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.button1 = new System.Windows.Forms.Button();
            this.AllowExternalConnectionsCheckBox = new System.Windows.Forms.CheckBox();
            this.LogPacketDataCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(266, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "&Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AllowExternalConnectionsCheckBox
            // 
            this.AllowExternalConnectionsCheckBox.AutoSize = true;
            this.AllowExternalConnectionsCheckBox.Location = new System.Drawing.Point(25, 40);
            this.AllowExternalConnectionsCheckBox.Name = "AllowExternalConnectionsCheckBox";
            this.AllowExternalConnectionsCheckBox.Size = new System.Drawing.Size(159, 17);
            this.AllowExternalConnectionsCheckBox.TabIndex = 1;
            this.AllowExternalConnectionsCheckBox.Text = "F1 2012 runs on another PC";
            this.AllowExternalConnectionsCheckBox.UseVisualStyleBackColor = true;
            // 
            // LogPacketDataCheckBox
            // 
            this.LogPacketDataCheckBox.AutoSize = true;
            this.LogPacketDataCheckBox.Location = new System.Drawing.Point(25, 63);
            this.LogPacketDataCheckBox.Name = "LogPacketDataCheckBox";
            this.LogPacketDataCheckBox.Size = new System.Drawing.Size(152, 17);
            this.LogPacketDataCheckBox.TabIndex = 2;
            this.LogPacketDataCheckBox.Text = "Log incoming Packet Data";
            this.LogPacketDataCheckBox.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 247);
            this.Controls.Add(this.LogPacketDataCheckBox);
            this.Controls.Add(this.AllowExternalConnectionsCheckBox);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsForm";
            this.Text = "Options";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Options_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox AllowExternalConnectionsCheckBox;
        private System.Windows.Forms.CheckBox LogPacketDataCheckBox;
    }
}