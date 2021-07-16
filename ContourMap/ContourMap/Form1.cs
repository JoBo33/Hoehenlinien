using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
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
        }

        
        public void FillData(string line, List<double[]> data)
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

        public void adjustHeight(List<double[]> data)
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


        public void DeterminePointsInOneRow(List<double[]> data, ref int pointsInOneRow)
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

        public double CalculateVolume(List<double[]> data, int pointsInOneRow)
        {
            double volume = 0;
            double ground = Math.Pow((data[1][0] - data[0][0]),2);
            for (int i = 0; i < data.Count - pointsInOneRow-1; i += (1))
		    {
                if((i % pointsInOneRow)-(pointsInOneRow-1) == 0 && i != 0)
                {
                    continue;
                }
                double height = (data[i][2] + data[i + 1][2] + data[i + pointsInOneRow][2] + data[i + pointsInOneRow + 1][2]) / 4;
                volume = volume + ground * height;
            }
            return volume;
        }

        public void DetermineContourLines(List<double[]> data, PlotModel model, int pointsInOneRow)
        {
            for (int i = 0; i <= data[2].Max(); i += (1)) // funktion für max hoehe implementieren!!
            {
                List<double[]> contourLine = new List<double[]> { };
                for (int j = 0; j < data.Count-1; j += (1))
                {
                    if (j < data.Count - pointsInOneRow)
                    {
                        if (data[j][2] < i && data[j + pointsInOneRow][2] > i || data[j][2] > i && data[j + pointsInOneRow][2] < i)
                        {
                            double tmp = (i - data[j][2]) / (data[j + pointsInOneRow][2] - data[j][2]);
                            double[] pointOnHeight = { data[j][0] + tmp * (data[j + pointsInOneRow][0] - data[j][0]), data[j][1] + tmp * (data[j + pointsInOneRow][1] - data[j][1]), i };
                            contourLine.Add(pointOnHeight); //add pointOnHeight to contourLine
                        }
                    }
                    if ((j % pointsInOneRow) - (pointsInOneRow - 1) == 0 && j != 0) //(j % pointsInOneRow == 0)
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
                if (contourLine.Count != 0)
                {
                    sortContourPoints(ref contourLine);
                    CatmullRomSplines(contourLine, model);

                }
                //draw contourLine;
            }
        }

        private void CatmullRomSplines(List<double[]> contourLine, PlotModel model)
        {
            LineSeries line = new LineSeries();
            for (float t = 0; t < contourLine.Count - 1; t += 0.01f)
            {
                int p1 = (int)t;
                int p2 = (p1 + 1) % (contourLine.Count - 1);
                int p3 = (p2 + 1) % (contourLine.Count - 1);
                int p0 = p1 >= 1 ? p1 - 1 : contourLine.Count - 1;

                float tmp = t - (int)t;
                float tt = tmp * tmp;
                float ttt = tmp * tmp * tmp;

                float q1 = -ttt + 2.0f * tt - tmp;
                float q2 = 3.0f * ttt - 5.0f * tt + 2;
                float q3 = -3.0f * ttt + 4.0f * tt + tmp;
                float q4 = ttt - tt;

                double x = 0.5 * (contourLine[p0][0] * q1 + contourLine[p1][0] * q2 + contourLine[p2][0] * q3 + contourLine[p3][0] * q4);
                double y = 0.5 * (contourLine[p0][1] * q1 + contourLine[p1][1] * q2 + contourLine[p2][1] * q3 + contourLine[p3][1] * q4);

                line.Points.Add(new DataPoint(x, y));
            }
            model.Series.Add(line);
            model.InvalidatePlot(true);
        }

        public void sortContourPoints(ref List<double[]> contourLine)
        {
            List<double[]> sortedContourLine = new List<double[]> { };
            for (int i = 0; i < contourLine.Count; i++)
            {
                for (int j = 0; j < contourLine.Count-1; j++)
                {
                    if(contourLine[j][0] > contourLine[j+1][0])
                    {
                       Swap(ref contourLine, j, j + 1);
                    }
                }
            }
            for (int i = 0; i < contourLine.Count-1; i++)
            {
                if (contourLine[i][0] == contourLine[i + 1][0] && contourLine[i][1] > contourLine[i + 1][1])
                {
                    Swap(ref contourLine, i, i + 1);
                }
            }
            for (int i = 0; i < contourLine.Count; i+=2)
            {
                sortedContourLine.Add(contourLine[i]);
            }
            for (int i = contourLine.Count-1; i >= 0; i -= 2)
            {
                sortedContourLine.Add(contourLine[i]);
            }
            contourLine = sortedContourLine;
        }
        public static void Swap(ref List<double[]> sortingList, int indexA, int indexB)
        {
            double[] temp = sortingList[indexA];
            sortingList[indexA] = sortingList[indexB];
            sortingList[indexB] = temp;
        }
            private void buttonVolume_Click(object sender, EventArgs e)
        {
            PrepareVariables(1);
        }

        private void buttonTrucks_Click(object sender, EventArgs e)
        {
            PrepareVariables(2);
        }

        private void buttonDiagram_Click(object sender, EventArgs e)
        {
            PrepareVariables(0);
        }


        private void PrepareVariables(int pressedButton)
        {

            List<double[]> data = new List<double[]> { };
            if (textBoxFilePath.Text == string.Empty)
            {
                MessageBox.Show("Pls enter a file path.");
                return;
            }

            StreamReader str;
            try
            {
                str = new StreamReader(textBoxFilePath.Text);
                string line = str.ReadLine();
                FillData(line, data);
            }
            catch(FileNotFoundException e)
            {
                MessageBox.Show("The file " + e.FileName+ " could not be found, please check the file path.");
                return;
            }
            int pointsInOneRow = 0;
            SortData(data);
            adjustHeight(data);
            DeterminePointsInOneRow(data, ref pointsInOneRow);


            if (pressedButton == 0)
            {
                PlotModel model = new PlotModel();
                plotViewContours.Model = model;
                DetermineContourLines(data, model, pointsInOneRow);

                PlotModel firstTabModel = new PlotModel();
                plotViewColumnSeries1.Model = firstTabModel;
                DrawColumnSeries(ref firstTabModel, data, pointsInOneRow);

                for (int i = 1; i < pointsInOneRow; i++)
                {
                    string title = "Profile " + (tabControlHillProfiles.TabCount + 1).ToString();
                    TabPage tabPageP2 = new TabPage(title);
                    tabControlHillProfiles.TabPages.Add(tabPageP2);

                    PlotModel plot = new PlotModel();
                    PlotView plotView2 = new PlotView() { BackColor = Color.White, Dock = DockStyle.Fill};
                    plotView2.Model = plot;
                    DrawColumnSeries(ref plot, data, pointsInOneRow, i);

                    tabPageP2.Controls.Add(plotView2);
                }
            }
            else if(pressedButton == 1)
            {
                double volume = CalculateVolume(data, pointsInOneRow);
                textBoxVolume.Text = volume.ToString();
            }
            else if(pressedButton == 2)
            {
                double volume = CalculateVolume(data, pointsInOneRow);
                int numberOfTrucks = 100;
                dataGridView1.RowCount = numberOfTrucks;
                for (int i = 1; i <= numberOfTrucks; i++)
                {
                    dataGridView1.Rows[i - 1].Cells[0].Value = i;
                    dataGridView1.Rows[i - 1].Cells[1].Value = Math.Ceiling((volume / 7) / i) * 30;
                }
            }
            else
            {
                MessageBox.Show("Sth. goes wrong.");
            }
        }

        private void SortData(List<double[]> data)
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

        private void DrawColumnSeries(ref PlotModel plot, List<double[]> data, int pointsInOneRow, int rowNumber = 0)
        {
            double maxHeight = FindMaxHeight(data);
            ColumnSeries columnSeries = new ColumnSeries();
            LinearAxis height = new LinearAxis { Minimum = 0, Maximum = maxHeight };
            height.IsZoomEnabled = false;
            plot.Axes.Add(height);

            CategoryAxis xAxis = new CategoryAxis { };
            xAxis.IsZoomEnabled = false;
            plot.Axes.Add(xAxis);

            for (int i = 0; i < pointsInOneRow; i++)
            {
                columnSeries.Items.Add(new ColumnItem(data[i + pointsInOneRow * rowNumber][2]));
            }
            plot.Series.Add(columnSeries);
            plot.InvalidatePlot(true);

        }

        private double FindMaxHeight(List<double[]> data)
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
