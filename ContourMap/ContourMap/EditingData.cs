using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContourMap
{
    class EditingData
    {
        public static void FillData(string line, List<double[]> data)
        {
            string point = "";
            string number = "";

            for (int i = 0; i < line.Length; i++)
            {
                double tmp;
                if (double.TryParse(line[i].ToString(), out tmp) || line[i].ToString() == "." || line[i].ToString() == ",")
                {
                    point += line[i];
                }

                if (line[i].ToString() == ";")
                {
                    double[] coordinate = new double[3];
                    int count = 0;

                    for (int j = 0; j <= point.Length; j++)
                    {
                        if (j == point.Length)
                        {
                            coordinate[count] = Convert.ToDouble(number);
                            count++;
                            number = "";
                        }
                        else
                        {
                            if (double.TryParse(point[j].ToString(), out tmp) || point[j].ToString() == ".")
                            {
                                number += point[j];
                            }

                            if (point[j].ToString() == ",")
                            {
                                coordinate[count] = Convert.ToDouble(number);
                                count++;
                                number = "";
                            }
                        }
                    }

                    point = "";
                    data.Add(coordinate);
                }

            }
        }
        public static  void adjustHeight(List<double[]> data)
        {
            double smallestHeight = double.MaxValue;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i][2] < smallestHeight)
                {
                    smallestHeight = data[i][2];
                }
            }
            for (int i = 0; i < data.Count; i++)
            {
                data[i][2] -= smallestHeight;
            }
        }

        public static void Swap(ref List<double[]> sortingList, int indexA, int indexB)
        {
            double[] temp = sortingList[indexA];
            sortingList[indexA] = sortingList[indexB];
            sortingList[indexB] = temp;
        }

        public static void SortData(List<double[]> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data.Count - 1; j++)
                {
                    if (data[j][1] > data[j + 1][1])
                    {
                        Swap(ref data, j, j + 1);
                    }
                }
            }
            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i][1] == data[i + 1][1] && data[i][0] > data[i + 1][0])
                {
                    Swap(ref data, i, i + 1);
                }
            }
        }

        public static double FindMaxHeight(List<double[]> data)
        {
            double max = 0;
            for (int i = 0; i < data.Count; i++)
            {
                if (max < data[i][2])
                {
                    max = data[i][2];
                }
            }
            return max;
        }
    }
}
