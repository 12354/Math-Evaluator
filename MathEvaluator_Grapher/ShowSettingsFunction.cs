using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathEvaluator.MathOperations.Functions;
namespace MathEvaluator_Grapher
{
    class ShowSettingsFunction : Function
    {
        public override string ToString()
        {
            return "settings";
        }
        private static bool initialize = true;
        public override MathEvaluator.MathOperations.Number Calculate()
        {
            if (initialize)
            {
                initialize = false;
                return 1;
            }
            if(!Program.SettingsFrm.Visible)
                Program.Frm1.ShowSettings();
            return 0;
        }
    }
}
