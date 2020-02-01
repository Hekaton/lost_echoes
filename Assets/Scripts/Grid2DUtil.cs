
    using System;
    using UnityEngine;

    public static class Grid2DUtil {

        public static double CompareGrid(Color[] array_1, Color[] array_2)
        {
            double result = 0f;
            double matchPixel = 0f;
            double totalPixel = (double) array_1.Length;

            for (int i = 0; i < array_1.Length; i++)
            {
                matchPixel += (1 - array_1[i].grayscale) - (1 - array_2[i].grayscale);

                i++;
            }
            result = matchPixel / totalPixel;
            return result;
        }
        
    }
