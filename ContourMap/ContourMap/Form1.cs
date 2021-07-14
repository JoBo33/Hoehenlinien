using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContourMap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //StreamReader str = new StreamReader(textBox2.Text);
            //string line = str.ReadLine();
            //FillData(line);
            //adjustHeight();
            
        }

        List<double[]> data = new List<double[]> { };
        
        public void FillData(string line)
        {
            string point = "";
            string number = "";
            for (int i = 0; i < line.Length; i++)
            {
                double tmp;
                if(double.TryParse(line[i].ToString(), out tmp) || line[i].ToString() == "." || line[i].ToString() == ",")
                {
                    point += line[i];
                }
                if(line[i].ToString() == ";")
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

        public void adjustHeight()
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

        int pointsInOneRow = 0;
        public double CalculateVolume()
        {
            double volume = 0;
            for (int i = 0; i <= data.Count; i += (1))
		    {
                pointsInOneRow = pointsInOneRow + 1;
                if (data[i][1] != data[i + 1][1])
                {
                    break;
                }
            }
            double ground = Math.Pow((data[1][0] - data[0][0]),2);
            for (int i = 0; i <= data.Count - pointsInOneRow; i += (1))
		    {
                if (i % pointsInOneRow == 0)
                {
                    continue;
                }
                double height = (data[i][2] + data[i + 1][2] + data[i + pointsInOneRow][2] + data[i + pointsInOneRow + 1][2]) / 4;
                volume = volume + ground * height;
            }
            return volume;
        }

        public void DetermineContourLines()
        {
            for (int i = 0; i <= data[2].Max(); i += (1))
            {
                List<double[]> contourLine = new List<double[]> { };
                for (int j = 0; j <= data.Count - pointsInOneRow; j += (1))
                {
                    if (data[j][2] < i && data[j + pointsInOneRow][2] > i || data[j][2] > i && data[j + pointsInOneRow][2] < i)
                    {
                        double tmp = (i - data[j][2]) / (data[j + pointsInOneRow][2] - data[j][2]);
                        double[] pointOnHeight = { data[j][0] + tmp * (data[j + pointsInOneRow][0] - data[j][0]), data[j][1] + tmp * (data[j + pointsInOneRow][1] - data[j][1]), i };
                        contourLine.Add(pointOnHeight); //add pointOnHeight to contourLine
                    }
                    if (j % pointsInOneRow == 0)
                    {
                        continue;
                    }
                    if (data[j][2] < i && data[j + 1][2] > i || data[j][2] > i && data[j + 1][2] < i)
                    {
                        double tmp = (i - data[j][2]) / (data[j + 1][2] - data[j][2]);
                        double[] pointOnHeight = { data[j][0] + tmp * (data[j + 1][0] - data[j][0]), data[j][1] + tmp * (data[j + 1][1] - data[j][1]), i };
                        contourLine.Add(pointOnHeight);
                    }
                }
                //draw contourLine;
            }
        }

        private void buttonVolume_Click(object sender, EventArgs e)
        {
            StreamReader str = new StreamReader(textBox2.Text);
            string line = str.ReadLine();
            FillData(line);
            adjustHeight();
            double volume = CalculateVolume();
            textBox1.Text = volume.ToString();
        }

        private void buttonTrucks_Click(object sender, EventArgs e)
        {
            
        }
    }
}
