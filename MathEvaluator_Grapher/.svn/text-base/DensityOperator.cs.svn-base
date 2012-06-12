using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathEvaluator.MathOperations;
using MathEvaluator.MathOperations.Operators;
namespace MathEvaluator_Grapher
{
    public class Density : Operator
    {
        public static float MAX_DIFFERENCE = 0.0001f;
        public override int Precedence
        {
            get
            {
                return 0;
            }
        }
        public override Association Association
        {
            get { return Association.Right; }
        }
        public override Number Calculate(float a, float b)
        {
            float dif = Math.Abs(a - b);
            return dif;
        }
        public override bool IsLogical { get { return true; } }
        public override string ToString()
        {
            return "~=";
        }
        public override void CopyToStack(MathEvaluator.ExpressionTree cur, MathEvaluator.StaticStack<MathOperation> DeriveStack)
        {
            throw new MathEvaluator.Exceptions.UndefinedResultException(ToString() + " is not derivable");
        }
        public override void DeriveToStack(MathEvaluator.ExpressionTree cur, MathEvaluator.MathState s, MathEvaluator.StaticStack<MathOperation> DeriveStack)
        {
            throw new MathEvaluator.Exceptions.UndefinedResultException(ToString() + " is not derivable");
        }

    }
}
