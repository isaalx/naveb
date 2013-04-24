namespace Nave_B
{
    partial class Juego
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Juego));
            this.Game = new System.Windows.Forms.PictureBox();
            this.back_time = new System.Windows.Forms.Timer(this.components);
            this.lpuntos = new System.Windows.Forms.Label();
            this.control = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Game)).BeginInit();
            this.SuspendLayout();
            // 
            // Game
            // 
            this.Game.InitialImage = ((System.Drawing.Image)(resources.GetObject("Game.InitialImage")));
            this.Game.Location = new System.Drawing.Point(0, 0);
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(800, 600);
            this.Game.TabIndex = 0;
            this.Game.TabStop = false;
            // 
            // back_time
            // 
            this.back_time.Tick += new System.EventHandler(this.back_time_Tick);
            // 
            // lpuntos
            // 
            this.lpuntos.AutoSize = true;
            this.lpuntos.BackColor = System.Drawing.Color.Transparent;
            this.lpuntos.CausesValidation = false;
            this.lpuntos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lpuntos.ForeColor = System.Drawing.Color.Yellow;
            this.lpuntos.Location = new System.Drawing.Point(695, 0);
            this.lpuntos.Name = "lpuntos";
            this.lpuntos.Size = new System.Drawing.Size(61, 13);
            this.lpuntos.TabIndex = 1;
            this.lpuntos.Text = "Puntos: 0";
            // 
            // control
            // 
            this.control.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.control.Location = new System.Drawing.Point(-1, 0);
            this.control.Name = "control";
            this.control.Size = new System.Drawing.Size(1, 13);
            this.control.TabIndex = 2;
            this.control.KeyDown += new System.Windows.Forms.KeyEventHandler(this.control_KeyDown);
            this.control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.control_KeyUp);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(727, 576);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Volver";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Juego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.control);
            this.Controls.Add(this.lpuntos);
            this.Controls.Add(this.Game);
            this.Name = "Juego";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.Game)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Game;
        private System.Windows.Forms.Timer back_time;
        private System.Windows.Forms.Label lpuntos;
        private System.Windows.Forms.TextBox control;
        private System.Windows.Forms.Button button1;
    }
}
