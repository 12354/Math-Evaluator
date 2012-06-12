using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenTK;

using OpenTK.Graphics.OpenGL;
namespace MathEvaluator_Grapher
{

    class DrawHelper
    {
        public static void DrawLine(Vector2d start, Vector2d end, float depth, Color color)
        {
            DrawLine((float)start.X, (float)start.Y, (float)end.X, (float)end.Y, depth, color);
        }
        public static void DrawLine(float startx, float starty, float endx, float endy, float depth, Color color)
        {
            GL.Begin(BeginMode.LineStrip);
            GL.Color3(color);
            GL.Vertex3(startx, starty, depth);
            GL.Vertex3(endx, endy, depth);
            GL.End();
        }
        public static void DrawLine(float startx, float starty, float endx, float endy, float startz,float endz, Color color)
        {
            GL.Begin(BeginMode.LineStrip);
            GL.Color3(color);
            GL.Vertex3(startx, starty, startz);
            GL.Vertex3(endx, endy, endz);
            GL.End();
        }
        public static void DrawLine(float depth, Color color, params Vector2d[] points)
        {
            GL.Begin(BeginMode.LineStrip);
            GL.Color3(color);
            foreach (Vector2d point in points)
            {

                GL.Vertex3(point.X, point.Y, depth);

            }
            GL.End();
        }
    }
}

