﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MathEvaluator_Grapher
{
    public partial class SettingsForm : Form
    {
        GrapherFunction tmpFunction;
        List<GrapherFunction> Functions;

        GrapherFunction activeFunction;
        public SettingsForm()
        {
            InitializeComponent();
        }
        
        public void UpdateFunctionBox(GrapherFunction _tmpFunction, List<GrapherFunction> _Functions)
        {
            tmpFunction = _tmpFunction;
            Functions = _Functions;

            FunctionListBox.Items.Clear();
            FunctionListBox.Items.Add(tmpFunction.Expression);
            _Functions.ForEach(x => FunctionListBox.Items.Add(x.Expression));
            UpdateFunctionDetails();
        }

        private void FunctionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFunctionDetails();
        }
        private void UpdateFunctionDetails()
        {
            if (FunctionListBox.SelectedIndex > 0)
                activeFunction = Functions[FunctionListBox.SelectedIndex - 1];
            else 
                activeFunction = tmpFunction;
            ExpressionBox.Text = activeFunction.Expression;
            
            ColorBoxA.Image = new Bitmap(ColorBoxA.Size.Width, ColorBoxA.Size.Height);
            Graphics.FromImage(ColorBoxA.Image).FillRectangle(new SolidBrush(activeFunction.ColorA), 0, 0, ColorBoxA.Size.Width, ColorBoxA.Size.Height);

            ColorBoxB.Image = new Bitmap(ColorBoxB.Size.Width, ColorBoxB.Size.Height);
            Graphics.FromImage(ColorBoxB.Image).FillRectangle(new SolidBrush(activeFunction.ColorB), 0, 0, ColorBoxB.Size.Width, ColorBoxB.Size.Height);

            checkBoxVisible.Checked = activeFunction.Visible;
            checkBox3D.Checked = activeFunction.Is3D;
            checkBoxImplicit.Checked = activeFunction.IsImplicit;
            checkBoxDensity.Checked = activeFunction.IsDensityPlot;
            RemoveButton.Visible = FunctionListBox.SelectedIndex > 0;
            checkBoxTransparent.Checked = activeFunction.IsAlphaFaded;
            checkBoxLocked.Checked = activeFunction.IsLocked;
        }
        private void ExpressionBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                activeFunction.Reset(Program.Frm1.Math.CreateExpression(ExpressionBox.Text));
                Program.Frm1.InvalidateFunctions();
            }
        }

        private void checkBoxVisible_CheckedChanged(object sender, EventArgs e)
        {
            activeFunction.Visible = checkBoxVisible.Checked;
            activeFunction.InvalidateVBO();
        }

        private void checkBoxImplicit_CheckedChanged(object sender, EventArgs e)
        {
            activeFunction.IsImplicit = checkBoxImplicit.Checked;
            activeFunction.InvalidateVBO();
        }

        private void checkBox3D_CheckedChanged(object sender, EventArgs e)
        {
            activeFunction.Is3D = checkBox3D.Checked;
            activeFunction.InvalidateVBO();
        }

        private void UpdateTime_Tick(object sender, EventArgs e)
        {
            if (activeFunction != null)
            {
                timeBox.Text = activeFunction.Time.ToString();
                ExpressionDebug.Text = activeFunction.expression.PostFixExpression;
            }
        }

        private void ResetTimeButton_Click(object sender, EventArgs e)
        {
            activeFunction.Timer.Restart();
        }

        private void DensityBox_CheckedChanged(object sender, EventArgs e)
        {
            activeFunction.IsDensityPlot = checkBoxDensity.Checked;
            activeFunction.InvalidateVBO();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            activeFunction.IsRemoved = true;

            //Program.Frm1.InvalidateFunctions();
        }
        
        private void checkBoxTransparent_CheckedChanged(object sender, EventArgs e)
        {
            activeFunction.IsAlphaFaded = checkBoxTransparent.Checked;
            activeFunction.InvalidateVBO();
        }

        private void checkBoxLocked_CheckedChanged(object sender, EventArgs e)
        {
            activeFunction.IsLocked = checkBoxLocked.Checked;
            checkBox3D.Enabled = !checkBoxLocked.Checked;
            checkBoxImplicit.Enabled = !checkBoxLocked.Checked;
            checkBoxDensity.Enabled = !checkBoxLocked.Checked;
            checkBoxVisible.Enabled = !checkBoxLocked.Checked;
            checkBoxTransparent.Enabled = !checkBoxLocked.Checked;
            ResetTimeButton.Enabled = !checkBoxLocked.Checked;
        }

        private void ColorBoxB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                colorDialog1.Color = activeFunction.ColorB;
                if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Color c = colorDialog1.Color;
                    if ((uint)c.ToArgb() == 0xFFFFFFFF)
                        c = Color.Transparent;
                    activeFunction.ColorB = c;
                    UpdateFunctionDetails();
                    Program.Frm1.InvalidateFunctions();
                }
            }
        }
        private void ColorBoxA_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                colorDialog1.Color = activeFunction.ColorA;
                if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Color c = colorDialog1.Color;
                    if ((uint)c.ToArgb() == 0xFFFFFFFF)
                        c = Color.Transparent;
                    activeFunction.ColorA = c;
                    UpdateFunctionDetails();
                    Program.Frm1.InvalidateFunctions();
                }
            }
        }

        private void ColorBoxB_UpdateAlpha(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                PictureBox box = (PictureBox)sender;
                float alpha = e.X / (float)box.Size.Width;
                if (alpha < 0)
                    alpha = 0;
                if (alpha > 1)
                    alpha = 1;
                activeFunction.ColorB = Color.FromArgb((int)(alpha * 255), activeFunction.ColorB);
                UpdateFunctionDetails();
                Program.Frm1.InvalidateFunctions();
            }
        }
        private void ColorBoxA_UpdateAlpha(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                PictureBox box = (PictureBox)sender;
                float alpha = e.X / (float)box.Size.Width;
                if (alpha < 0)
                    alpha = 0;
                if (alpha > 1)
                    alpha = 1;
                activeFunction.ColorA = Color.FromArgb((int)(alpha * 255), activeFunction.ColorA);
                UpdateFunctionDetails();
                Program.Frm1.InvalidateFunctions();
            }
        }

        DateTime last;
        System.Diagnostics.Stopwatch compileWatch = new System.Diagnostics.Stopwatch();
        bool compiled = false;
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            compiled = false;
            compileWatch.Restart();

            
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            QfiBox.SelectionTabs = new int[] { 20, 40, 80, 100 };

        }

        private void CompileTimer_Tick(object sender, EventArgs e)
        {
            if (!compiled && compileWatch.ElapsedMilliseconds > 150)
            {
                MathEvaluator_Grapher.QfiWrapper.Compile(QfiBox.Text);
                Program.Frm1.InvalidateFunctions();
                compiled = true;
            }
        }
        private void checkBoxZoom_CheckedChanged(object sender, EventArgs e)
        {
            Program.Frm1.Zoom2D = !Program.Frm1.Zoom2D;
        }

        private void checkBoxAA_CheckedChanged(object sender, EventArgs e)
        {
            Program.Frm1.AntiAliasing = checkBoxAA.Checked;
        }

        private void DeriveButton_Click(object sender, EventArgs e)
        {
            Program.Frm1.AddFunction(activeFunction.expression.Derive(Program.Frm1.Math).Optimize(Program.Frm1.Math));
            Program.Frm1.InvalidateFunctions();
        }
    }
}
