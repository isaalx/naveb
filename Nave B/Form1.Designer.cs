namespace Nave_B
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.BJugar = new System.Windows.Forms.Button();
            this.Workspace = new System.Windows.Forms.Panel();
            this.Workspace.SuspendLayout();
            this.SuspendLayout();
            // 
            // BJugar
            // 
            this.BJugar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BJugar.BackColor = System.Drawing.Color.Transparent;
            this.BJugar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BJugar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.BJugar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.BJugar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BJugar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BJugar.Location = new System.Drawing.Point(362, 288);
            this.BJugar.Name = "BJugar";
            this.BJugar.Size = new System.Drawing.Size(76, 24);
            this.BJugar.TabIndex = 0;
            this.BJugar.Text = "Jugar";
            this.BJugar.UseVisualStyleBackColor = false;
            this.BJugar.Click += new System.EventHandler(this.BJugar_Click);
            // 
            // Workspace
            // 
            this.Workspace.Controls.Add(this.BJugar);
            this.Workspace.Location = new System.Drawing.Point(2, 2);
            this.Workspace.Name = "Workspace";
            this.Workspace.Size = new System.Drawing.Size(800, 600);
            this.Workspace.TabIndex = 1;
    
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(804, 603);
            this.Controls.Add(this.Workspace);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Nave B";
            this.Workspace.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BJugar;
        private System.Windows.Forms.Panel Workspace;
    }
}

