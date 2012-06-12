namespace MathEvaluator_Grapher
{
    partial class Form1
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
            this.functionBox = new System.Windows.Forms.TextBox();
            this.glControl = new MathEvaluator_Grapher.CustomGLControl();
            this.SuspendLayout();
            // 
            // functionBox
            // 
            this.functionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.functionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.functionBox.Location = new System.Drawing.Point(0, 599);
            this.functionBox.Name = "functionBox";
            this.functionBox.Size = new System.Drawing.Size(634, 13);
            this.functionBox.TabIndex = 1;
            this.functionBox.Text = "sin(x)";
            this.functionBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.functionBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(0, 0);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(634, 612);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(634, 612);
            this.Controls.Add(this.functionBox);
            this.Controls.Add(this.glControl);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomGLControl glControl;
        private System.Windows.Forms.TextBox functionBox;
    }
}