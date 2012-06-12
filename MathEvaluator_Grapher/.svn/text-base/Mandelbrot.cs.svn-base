using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEvaluator_Grapher
{
    class Mandelbrot : MathEvaluator.MathOperations.Functions.Function
    {
        private class ComplexNumber
        {
            public float Real;
            public float Imaginary;
            public ComplexNumber(float real, float imaginary)
            {
                Real = real;
                Imaginary = imaginary;
            }
            public static ComplexNumber operator +(ComplexNumber c1, ComplexNumber c2)
            {
                return new ComplexNumber(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
            }
            public static ComplexNumber operator -(ComplexNumber c1, ComplexNumber c2)
            {
                return new ComplexNumber(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
            }
            public static ComplexNumber operator *(ComplexNumber c1, ComplexNumber c2)
            {
                return new ComplexNumber(c1.Real * c2.Real - c1.Imaginary * c2.Imaginary,c1.Imaginary * c2.Real + c1.Real * c2.Imaginary );
            }
            public float Length()
            {
                return (float)Math.Sqrt(Real * Real + Imaginary * Imaginary);
            }
            public float LengthSquared()
            {
                return Real * Real + Imaginary * Imaginary;
            }
        }
        private static ComplexNumber CalculateMandelbrot(ComplexNumber current, ComplexNumber start, int iterations)
        {
            if (iterations <= 0 )
                return current;
            return CalculateMandelbrot(current * current + start, start, --iterations);
        }
        private static int CalculateMandelbrotIterations(ComplexNumber current, ComplexNumber start, int iterations,int maxiterations)
        {
            if (iterations >= maxiterations)
                return maxiterations;
            if (current.LengthSquared() > 4f)
                return iterations;
            return CalculateMandelbrotIterations(current * current + start, start, ++iterations,maxiterations);
        }
        /*public override MathEvaluator.MathOperations.Number Calculate(float real, float imaginary,float iterations)
        {
            ComplexNumber start = new ComplexNumber(real, imaginary);
            return CalculateMandelbrot(start,start, (int)iterations).Abs();
        }*/
        public override MathEvaluator.MathOperations.Number Calculate(float real, float imaginary, float maxiterations)
        {
            ComplexNumber start = new ComplexNumber(real, imaginary);
            ComplexNumber current = start;
            int iteration = 0;
            for (; iteration < (int)maxiterations; iteration++)
            {
                if (current.LengthSquared() > 4.0f)
                    break;
                current = current * current + start;
            }
            return 3 - iteration / (float)((int)maxiterations);
            //return 3 - CalculateMandelbrotIterations(start, start, 0,(int)maxiterations)/(float)((int)maxiterations) ;
        }
        public override string ToString()
        {
            return "mandelbrot";
        }
    }
}
