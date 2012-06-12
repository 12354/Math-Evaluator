/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
namespace MathEvaluator_Grapher.Marching
{
    class MarchingSquares
    {
        // A simple enumeration to represent the direction we
        // just moved, and the direction we will next move.
        private enum StepDirection
        {
            None,
            Up,
            Left,
            Down,
            Right
        }
        // The direction we previously stepped
        private StepDirection previousStep;

        // Our next step direction:
        private StepDirection nextStep;
 
        ///http://devblog.phillipspiess.com/2010/02/23/better-know-an-algorithm-1-marching-squares/
        public static void Polygonise(float[,] datablock, float isolevel, Vector3 start, Vector3 vStep, int steps)
        {
              
        }
        private static int Step(int x, int y,float[,] datablock,float isosurface)
        {
            bool upLeft = (x > 0 && y > 0) ? datablock[x - 1, y - 1] < isosurface : false;
            bool upRight = (y > 0) ? datablock[x, y - 1] < isosurface : false;
            bool downLeft = (x > 0) ? datablock[x - 1, y] < isosurface : false;
            bool downRight = datablock[x, y] < isosurface;
            
            int state = 0;
            if (upLeft)
                state |= 1;
            if (upRight)
                state |= 2;
            if (downLeft)
                state |= 4;
            if (downRight)
                state |= 8;
            Vector2 direction = Vector2.Zero;
            switch (state)
            {
                case 0:
                    direction.X = 1;
                    break;
                case 1:
                    direction.Y = 1;
                    break;
                case 2:
                    direction.X = 1;
                    break;
                case 3:
                    direction.X = 1;
                    break;
                case 4:
                    direction.X = -1;
                    break;
                case 5:
                    direction.Y = 1;
                    break;
                case 6:
                    direction.X = -1;
                    break;
                case 7:
                    direction.X = 1;
                    break;
                case 8:
                    direction.Y = 1;
                    break;
                case 9:
                    direction.Y = 1;
                    break;
                case 10:
                    direction.Y = -1;
                    break;
                case 11:
                    direction.Y = 1;
                    break;
                case 12:
                    direction.X = -1;
                    break;
                case 13:
                    direction.Y = 1;
                    break;
                case 14:
                    direction.X = -1;
                    break;
                case 15:
                default:
                    throw new Exception();
            }
        }
    }
}
*/