// AutoCADLI
// MathTools.cs
// 
// ============================================================
// 
// Created: 2018-07-22
// Last Updated: 2018-07-28-3:35 PM
// By: Adam Renaud
// 
// ============================================================
// 
// Purpose: To hold the Math tools required in the AutoCADLI Project

using System;

namespace AutoCADLI
{
    /// <summary>
    ///     Class for Mathematics functionality
    /// </summary>
    public static class MathTools
    {
        /// <summary>
        ///     Converts a double using preset types
        /// </summary>
        /// <param name="inputNumber">The number that you want to convert</param>
        /// <param name="type">The conversion type</param>
        /// <returns>The converted Number</returns>
        public static double Convert(double inputNumber, Conversions type)
        {
            switch (type)
            {
                // Hectares to Acres
                case Conversions.HaAc:
                    return inputNumber * 2.471;

                // Acres to Hectares
                case Conversions.AcHa:
                    return inputNumber / 2.471;

                // Hectares to meters squared
                case Conversions.HaM2:
                    return inputNumber * 10000;

                // Meters squared to Hectares
                case Conversions.M2Ha:
                    return inputNumber / 10000;

                // You should not ever reach this code
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