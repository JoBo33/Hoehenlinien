using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourMap
{
    class Calculation
    {
        public static void DeterminePointsInOneRow(List<double[]> data, ref int pointsInOneRow)
        {
            for (int i = 0; i < data.Count; i += (1))
            {
                pointsInOneRow = pointsInOneRow + 1;
                if (data[i][1] != data[i + 1][1])
                {
                    break;
                }
            }
        }
        public static double CalculateVolume(List<double[]> data, int pointsInOneRow)
        {
            double volume = 0;
            double ground = Math.Pow((data[1][0] - data[0][0]), 2);

            for (int i = 0; i < data.Count - pointsInOneRow - 1; i += (1))
            {
                if ((i % pointsInOneRow) - (pointsInOneRow - 1) == 0 && i != 0)
                {
                    continue;
                }

                double height = (data[i][2] + data[i + 1][2] + data[i + pointsInOneRow][2] + data[i + pointsInOneRow + 1][2]) / 4;
                volume = volume + ground * height;
            }
            return volume;
        }
    }
}
