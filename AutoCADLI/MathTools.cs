using System;

namespace AutoCADLI
{
    public static class MathTools
    {
        public static double Convert(double inputNumber, Conversions type)
        {
            switch (type)
            {
                case Conversions.HaAc:
                    return inputNumber*2.471; 
                case Conversions.AcHa:
                    return inputNumber/2.471;
                case Conversions.HaM2:
                    return inputNumber * 10000;
                case Conversions.M2Ha:
                    return inputNumber / 10000;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum Conversions
    {
        HaAc,
        AcHa,
        M2Ha,
        HaM2
    }
}
