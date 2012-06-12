using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathEvaluator;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace MathEvaluator_Grapher
{
    public class GrapherFunction
    {
        struct Vector3Color
        {
            public byte R , G, B, A;
            public Vector3 Position;

            public static int SizeInBytes = 16;
        }
        Vector3Color[] data;
        int data_count = 0;

        uint vbo_id;
        

        public MathExpression expression;

        public const float PLOT2D_DEPTH = 1.0f;
        public const float PLOT3D_DEPTH = 1.0f;
        public const float PLOT3D_MAX_DEPTH = 30.0f;
        //public const float 3DPLOT_DEPTH = 1.0f;
        private Color m_ColorA = Color.Black;
        private Color m_ColorB = Color.Red;
        private bool m_Locked = false;
        private Vector4 m_vColorA;
        private Vector4 m_vColorB;
        private bool m_ColorChanged = false;
        public List<Vector2> implicitdata;
        Stopwatch timer;
        bool m_PositionChanged = true;
        bool m_VboInvalidated = false;
        bool PositionChanged { get { return m_PositionChanged; } set { m_PositionChanged = value; } }
        const float LINE_WIDTH = 1.5f;
        public bool Visible { get; set; }
        public bool Is3D { get; set; }
        public bool IsImplicit { get; set; }
        public bool IsDensityPlot { get; set; }
        public bool IsAlphaFaded { get; set; }
        public bool IsLocked
        {
            get { return m_Locked; }
            set
            {
                m_Locked = value;
                if (timer.IsRunning && value)
                    timer.Stop();
                if (!timer.IsRunning && !value)
                    timer.Start();
            }
        }
        public string Expression { get { return expression.OriginalExpression; } }
        public float Time { get { return timer.ElapsedMilliseconds / 1000.0f; } }
        public Color ColorA { get { return m_ColorA; } set
        {
            m_ColorA = value;
            m_vColorA.X = value.R;
            m_vColorA.Y = value.G;
            m_vColorA.Z = value.B;
            m_vColorA.W = value.A;
            m_ColorChanged = true; } 
        }
        public Color ColorB
        {
            get { return m_ColorB; }
            set
            {
                m_ColorB = value;
                m_vColorB.X = value.R;
                m_vColorB.Y = value.G;
                m_vColorB.Z = value.B;
                m_vColorB.W = value.A;
                m_ColorChanged = true;
            }
        }
        public Stopwatch Timer { get { return timer; } set { timer = value; } }
        public bool IsRemoved { get; set; }
        public void Invalidate()
        {
            PositionChanged = true;
        }
        public void InvalidateVBO()
        {
            m_VboInvalidated = true;
        }
        public void Calculate(Vector2 start, Vector2 end, float zoom, float step)
        {
            if (m_VboInvalidated)
            {
                PositionChanged = true;
                vbo_id = 0;
                m_VboInvalidated = false;
            }
            if (!PositionChanged || IsLocked)
                return;
            expression.SetVariable("time", timer.ElapsedMilliseconds / 1000f);

            QfiWrapper.arg_count = 0;
            if (expression.ContainsVariable("x"))
                QfiWrapper.arg_count++;
            if (expression.ContainsVariable("y"))
                QfiWrapper.arg_count++;
            if (expression.ContainsVariable("z"))
                QfiWrapper.arg_count++;
            if (Is3D)
            {
                start.X -= zoom;
                end.X += zoom;
                Calculate3D(start, end, zoom, step);
            }
            else
                Calculate2D(start, end, zoom, step);
        }
        private void Calculate2D(Vector2 start,Vector2 end,float zoom, float step)
        {
            if (vbo_id == 0)
            {
                GL.GenBuffers(1, out vbo_id);
                if (IsImplicit)
                {
                    step = zoom / 100;
                    data = new Vector3Color[(int)(((2 * zoom) / step + 2) *((2* zoom ) / step+2) )];
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i].R = data[i].G = data[i].B = 0;
                        data[i].A = 255;
                    }
                }
                else
                {
                    data = new Vector3Color[(int)(2 * zoom / step + 2)];
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i].R = data[i].G = data[i].B = 0;
                        data[i].A = 255;
                    }
                    data_count = data.Length;
                }
            }
            if (m_ColorChanged)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    data[i].R = m_ColorA.R;
                    data[i].G = m_ColorA.G;
                    data[i].B = m_ColorA.B;
                    data[i].A = 255;
                }
            }
            if (expression != null)
            {
                int i = 0;
                float z_value = (Program.Frm1.Zoom2D) ? PLOT2D_DEPTH : zoom;
                try
                {
                    if (!IsImplicit)
                    {
                        for (float x = start.X - step; x <= end.X + step; x += step)
                        {
                            expression.SetVariable("x", x);
                            data[i].Position.X = x;
                            data[i].Position.Y = expression.Calculate();
                            data[i++].Position.Z = z_value;
                        }
                    }
                    else
                    {
                        step = zoom / 100.0f;
                        MathEvaluator.MathOperations.Operators.Equals.MAX_DIFFERENCE = step ;
                        
                        for (float x = start.X - step; x <= end.X + step; x += step)
                        {
                            for (float y = start.Y - step; y <= end.Y + step; y += step)
                            {
                                expression.SetVariable("x", x);
                                expression.SetVariable("y", y);
                                float result = expression.Calculate();
                                if (IsDensityPlot)
                                {
                                    if (IsAlphaFaded)
                                    {
                                        float alpha = 1 - result;
                                        if (alpha < 0)
                                            continue;
                                        alpha *= alpha * alpha;

                                        data[i].A = (byte)(alpha * 255);
                                        if (data[i].A == 0)
                                            continue;
                                        data[i].Position.X = x;
                                        data[i].Position.Y = y;
                                        data[i++].Position.Z = zoom;
                                    }
                                    else
                                    {
                                        if (result > 1)
                                            result = 1;
                                        if (result < 0)
                                            result = 0;
                                        float colA = 1 - result;
                                        float colB = result;
                                        Vector4 color ;
                                        if (m_ColorB.A == 0)
                                            color = m_vColorA * colA;
                                        else
                                            color = m_vColorA * colA + m_vColorB * colB;
                                        if (color.W <= 0)
                                            continue;
                                        data[i].Position.X = x;
                                        data[i].Position.Y = y;
                                        data[i].R = (byte)color.X;
                                        data[i].G = (byte)color.Y;
                                        data[i].B = (byte)color.Z;
                                        data[i].A = (byte)color.W;
                                        data[i++].Position.Z = zoom;

                                        
                                    }
                                }
                                else
                                {
                                    if (result != 0)
                                    {
                                        data[i].Position.X = x;
                                        data[i].Position.Y = y;
                                        data[i].R = m_ColorA.R;
                                        data[i].G = m_ColorA.G;
                                        data[i].B = m_ColorA.B;
                                        data[i].A = m_ColorA.A;
                                        data[i++].Position.Z = zoom;
                                    }
                                }
                                
                            }
                        }
                    }

                }
                catch (Exception)
                {
                }
                data_count = i;

            }
            if (!expression.ContainsVariable("time") && !expression.ContainsFunction("rnd"))
                PositionChanged = false;
        }
        private void Calculate3D(Vector2 start, Vector2 end, float zoom, float step)
        {
            if (vbo_id == 0)
            {
                GL.GenBuffers(1, out vbo_id);
                //step = zoom / 10f;
                float count = 400000;// ((2 * zoom) / step * ((2 * zoom) / step));
                data = new Vector3Color[(int)count];
            }
            float startz = 0.0f;
            float endz = 20.0f;
            float row = 0;//(((right - left) / step) + 1) * 4;

            int i = 0;
            Vector2 vStep = new Vector2(end.X - start.X,  endz-startz) / 100f;
            for (float x = start.X; x < end.X; x += vStep.X)
            {
                row += 4;
            }
            try
            {
                for (float z = startz; z < endz; z += vStep.Y)
                {
                    for (float x = start.X; x < end.X; x += vStep.X)
                    {

                        int last = i - (int)row;
                        if (last >= 0)
                            data[i++].Position = data[last + 1].Position;
                        else
                        {
                            expression.SetVariable("x", x);
                            expression.SetVariable("z", z);
                            data[i++].Position = new Vector3(x, expression.Calculate(), z + zoom);
                        }
                        if (x > start.X )
                            data[i++].Position = data[i - 3].Position;
                        else
                        {
                            expression.SetVariable("x", x);
                            expression.SetVariable("z", z + vStep.Y);
                            data[i++].Position = new Vector3(x, expression.Calculate(), z + vStep.Y + zoom);
                        }

                        expression.SetVariable("x", x + vStep.X);
                        expression.SetVariable("z", z + vStep.Y);
                        data[i++].Position = new Vector3(x + vStep.X, expression.Calculate(), z + vStep.Y + zoom);

                        if (last >= 0)
                            data[i++].Position = data[last + 2].Position;
                        else
                        {
                            expression.SetVariable("x", x + vStep.X);
                            expression.SetVariable("z", z);
                            data[i++].Position = new Vector3(x + vStep.X, expression.Calculate(), z + zoom);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            data_count = i -1;

            for (int j = 0; j < data_count; j++)
            {
                data[j].G = data[j].B = 0;
                float r = (((data[j].Position.Y + 1.0f) / 2) * 255.0f);
                if (r > 255)
                    r = 255;
                if (r < 0)
                    r = 0;
                data[j].R = (byte)r;
                data[j].A = 0xFF;
            }
            if (!expression.ContainsVariable("time") && !expression.ContainsFunction("rnd"))
                m_PositionChanged = false;
        }
        public void Draw(Vector2 start, Vector2 end, float step, float zoom)
        {
            if (!Visible)
                return;
            Calculate(start, end, zoom, step);
            DrawVBO(start, end, step, zoom);
        }
        private void DrawVBO(Vector2 start, Vector2 end, float step, float zoom)
        {
            if (Program.Frm1.AntiAliasing)
            {
                GL.Enable(EnableCap.LineSmooth);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            }
            else
            {
                GL.Disable(EnableCap.LineSmooth);
                GL.Disable(EnableCap.Blend);
                GL.Hint(HintTarget.LineSmoothHint, HintMode.Fastest);
            }
            GL.Disable(EnableCap.Multisample);
            
            GL.LineWidth(LINE_WIDTH);

            GL.PointSize(5f);
            GL.Disable(EnableCap.PointSmooth);

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_id);

            GL.ColorPointer(4, ColorPointerType.UnsignedByte, Vector3Color.SizeInBytes, (IntPtr)0);
            GL.VertexPointer(3, VertexPointerType.Float, Vector3Color.SizeInBytes, (IntPtr)(4 * sizeof(byte)));

            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vector3Color.SizeInBytes * data.Length), IntPtr.Zero, BufferUsageHint.StreamDraw);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vector3Color.SizeInBytes * data_count), data, BufferUsageHint.StreamDraw);

            if (Is3D)
                GL.DrawArrays(BeginMode.Quads, 0, data_count);
            else
                if(IsImplicit)
                    GL.DrawArrays(BeginMode.Points, 0, data_count);    
                else
                    GL.DrawArrays(BeginMode.LineStrip, 0, data_count);
        }
        public GrapherFunction(MathExpression ex)
        {
            IsAlphaFaded = true;
            ColorA = m_ColorA;
            ColorB = m_ColorB;
            Reset(ex);
        }
        public void Reset(MathExpression ex)
        {
            expression = ex;
            Is3D = ex.ContainsVariable("z");
            IsDensityPlot = expression.ExpressionStack.Peek() is Density;
            IsImplicit = ex.IsImplicit;
            if (IsImplicit)
                implicitdata = new List<Vector2>();
            timer = new Stopwatch();
            timer.Start();
            Visible = true;
            InvalidateVBO();
            IsLocked = false;
        }

    }
}
