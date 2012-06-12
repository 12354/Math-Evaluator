using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using OpenTK;

using MathEvaluator;
namespace MathEvaluator_Grapher
{
    public partial class Form1 : Form
    {
        Vector3 center = new Vector3(0, 0,0);
        //Vector3 camera_offset = new Vector3(0, 0, 0);
        Point LastMouse = new Point(0, 0);

        float zoom = 1.0f;
        Matrix4 projection;
        Matrix4 modelview;

        MathState math = new MathState();
        List<GrapherFunction> Functions = new List<GrapherFunction>();
        GrapherFunction tmpFunction;
        public MathState Math { get { return math; } }
        public bool Zoom2D = false;
        public bool AntiAliasing = true;
        public Form1()
        {
            InitializeComponent();
            tmpFunction = new GrapherFunction(math.CreateExpression(functionBox.Text));
        }
        public void AddFunction(MathExpression ex )
        {
            Functions.Add(new GrapherFunction(ex));
            UpdateSettingsForm();
        }
        public void AddFunction(GrapherFunction fc)
        {
            Functions.Add(fc);
            UpdateSettingsForm();
        }
        public void InvalidateFunctions()
        {
            if (tmpFunction != null)
                tmpFunction.Invalidate();
            
            Functions.ForEach((func) => func.Invalidate());
            functionBox.Text = tmpFunction.Expression;
        }
        public void ShowSettings()
        {
            Program.SettingsFrm.Show();
            Program.SettingsFrm.UpdateFunctionBox(tmpFunction, Functions);
        }
        private void DrawFunctions(Vector2 start, Vector2 end, float step, float zoom)
        {
            if (tmpFunction != null)
                tmpFunction.Draw(start,end, step, zoom);
            int removed = Functions.RemoveAll(x => x.IsRemoved);
            if (removed > 0)
                UpdateSettingsForm();
            Functions.ForEach((func) => func.Draw(start,end,step,zoom));
        }
        private bool Is3D()
        {
            if(!tmpFunction.Is3D)
               return Functions.Find((func) => func.Is3D) != null;
            return true;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //glControl.KeyDown += new KeyEventHandler(glControl_KeyDown);
            glControl.Resize += new EventHandler(glControl_Resize);
            glControl.Paint += new PaintEventHandler(glControl_Paint);
            glControl.MouseWheel += new MouseEventHandler(glControl_MouseWheel);
            glControl.MouseMove += new MouseEventHandler(glControl_MouseMove);

            Text = "MathEvaluator_Grapher";

            GL.ClearColor(1.0f, 1.0f, 1.0f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            
            Application.Idle += Application_Idle;

            // Ensure that the viewport and projection matrix are set correctly.
            glControl_Resize(glControl, EventArgs.Empty);
        }
        void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            Text = ScreenToWorld(new Vector2d(e.Location.X, e.Location.Y)).ToString();
            Point delta = new Point(e.Location.X - LastMouse.X,e.Location.Y - LastMouse.Y);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Vector2d dCenter = WorldToScreen(new Vector2d(center.X, center.Y));
                
                dCenter.X -= delta.X;
                dCenter.Y -= delta.Y;
                dCenter = ScreenToWorld(dCenter);
                center = new Vector3((float)dCenter.X, (float)dCenter.Y, 0);
                InvalidateFunctions();
            }
            LastMouse = e.Location;
        }

        void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if(tmpFunction.IsLocked || Functions.Exists(x=>x.IsLocked))
                return;
            if (Zoom2D)
            {
                if (e.Delta > 0)
                    zoom /= 1 + e.Delta / 1000f;
                else
                    zoom *= 1 + e.Delta / -1000f;
                if (zoom < 0.0005)
                    zoom = 0.0005f;
                InvalidateFunctions();
                Matrix4.CreatePerspectiveOffCenter(zoom, -zoom, -zoom, zoom, 0.9999f, 640);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadMatrix(ref projection);
            }
            else
            {
                zoom -= e.Delta / 100.0f;
                if (zoom > 63.0f)
                    zoom = 63.0f;
                if (zoom < 1.0f)
                    zoom = 1f;
                InvalidateFunctions();
            }
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            Application.Idle -= Application_Idle;
            base.OnClosing(e);
        }

        void Application_Idle(object sender, EventArgs e)
        {
            while (glControl.IsIdle)
            {
                Render();
            }
        }

        void glControl_Resize(object sender, EventArgs e)
        {
            OpenTK.GLControl c = sender as OpenTK.GLControl;

            if (c.ClientSize.Height == 0)
                c.ClientSize = new System.Drawing.Size(c.ClientSize.Width, 1);

            GL.Viewport(0, 0, c.ClientSize.Width, c.ClientSize.Height);

            if(Zoom2D)
                projection = Matrix4.CreatePerspectiveOffCenter(zoom, -zoom, -zoom, zoom, 0.9999f, 640);
            else
                projection = Matrix4.CreatePerspectiveOffCenter(1f, -1f, -1f, 1f, 0.9999f, 64f);
            
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

 /*       void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    camera_offset.X += 1;
                    break;
                case Keys.Down:
                    camera_offset.X -= 1;
                    break;
                case Keys.Left:
                    camera_offset.Z += 1;
                    break;
                case Keys.Right:
                    camera_offset.Z -= 1;
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }*/

        void glControl_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        private void Render()
        {

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            float camera_offset = 0;
            if (Zoom2D)
                camera_offset = zoom;
            modelview = Matrix4.LookAt(center-new Vector3(0,0,camera_offset),  new Vector3(center.X,center.Y,1) , Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);



            DrawAxes();
            float step = (zoom) / 1000f;
            if (Is3D())
            {
                if (zoom < 5)
                    step = 0.1f;
                else
                    step = 0.25f;
            }
            DrawFunctions(new Vector2(center.X - zoom,center.Y - zoom), new Vector2(center.X + zoom ,center.Y + zoom ),step, zoom);
            

            glControl.SwapBuffers();
        }
        public void DrawAxes()
        {
            if (Zoom2D)
            {
                Color AxisColor = Color.Gray;
                GL.Disable(EnableCap.LineSmooth);
                GL.Disable(EnableCap.DepthTest);
                GL.LineWidth(1.0f);
                float z = zoom;
                DrawHelper.DrawLine(center.X - zoom, 0, center.X + zoom, 0, GrapherFunction.PLOT2D_DEPTH, AxisColor);
                DrawHelper.DrawLine(0, center.Y - zoom, 0, center.Y + zoom, GrapherFunction.PLOT2D_DEPTH, AxisColor);
                GL.Enable(EnableCap.DepthTest);
                if (Is3D())
                    DrawHelper.DrawLine(0, 0, 0, 0, GrapherFunction.PLOT3D_DEPTH, GrapherFunction.PLOT3D_MAX_DEPTH, AxisColor);
            }
            else
            {
                GL.Disable(EnableCap.LineSmooth);
                GL.Disable(EnableCap.DepthTest);
                GL.LineWidth(1.0f);

                DrawHelper.DrawLine(center.X - zoom, 0, center.X + zoom, 0, zoom, Color.Gray);
                DrawHelper.DrawLine(0, center.Y - zoom, 0, center.Y + zoom, zoom, Color.Gray);

                GL.Enable(EnableCap.DepthTest);
                if (Is3D())
                    DrawHelper.DrawLine(0, 0, 0, 0, zoom, 30.0f, Color.Gray);
            }
        }
        
        

        public Vector2d WorldToScreen(Vector2d world)
        {
            Vector2d outVector = world;
            outVector -= new Vector2d(center.X, center.Y);
            outVector /= 2;
            outVector.Y *= -1;
            outVector /= zoom;
            outVector.X *= ClientRectangle.Width;
            outVector.Y *= ClientRectangle.Height;
            outVector.X += ClientRectangle.Width / 2;
            outVector.Y += ClientRectangle.Height / 2;
            return outVector;
        }
        public Vector2d ScreenToWorld(Vector2d screen)
        {
            Vector2d outVector = screen;
            outVector.X -= ClientRectangle.Width / 2;
            outVector.Y -= ClientRectangle.Height / 2;
            outVector.X /= ClientRectangle.Width;
            outVector.Y /= ClientRectangle.Height;
            outVector *= zoom;
            outVector.Y *= -1;
            outVector *= 2;
            outVector += new Vector2d(center.X, center.Y);
            return outVector;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MathExpression m;
            if (math.TryCreateExpression(functionBox.Text, out m))
            {
                tmpFunction.Reset(m);
                //tmpFunction = new GrapherFunction(m);
                InvalidateFunctions();
                UpdateSettingsForm();
            }
        }
        private void UpdateSettingsForm()
        {
            if(Program.SettingsFrm.Visible)
                Program.SettingsFrm.UpdateFunctionBox(tmpFunction, Functions);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            math.AddSingleOperatorOrFunction(typeof(Mandelbrot));
            math.AddSingleOperatorOrFunction(typeof(Density));
            math.AddSingleOperatorOrFunction(typeof(ShowSettingsFunction));
            math.AddSingleOperatorOrFunction(typeof(Gauss));
            math.AddSingleOperatorOrFunction(typeof(QfiWrapper));

            QfiWrapper.AddFunctions(math.Functions);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                AddFunction(tmpFunction);
                tmpFunction = new GrapherFunction(math.CreateExpression(""));
                functionBox.Text = "";
                
            }
        }
        Size lastSize;
        private void Form1_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;
            if (control.Size.Height != control.Size.Width)
            {
                control.Size = new Size(control.Size.Height, control.Size.Height);
            }
            lastSize = control.Size;
        }


    }
}
