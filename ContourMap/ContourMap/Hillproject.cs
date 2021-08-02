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
    public partial class Hillproject : Form
    {
        public Hillproject()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteTask(-1);
        }

        private void volumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteTask(0);
        }

        private void optimalNumberOfTrucksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteTask(1);
        }

        private void hillProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteTask(2);
        }

        private void contourMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteTask(3);
        }


        private void ExecuteTask(int pressedMenu)
        {
            splitContainer1.Panel2.Controls.Clear();
            List<double[]> data = new List<double[]> { };
            if (pressedMenu == -1)
            {
                if (!ExecuteFileTask(data))
                {
                    return;
                }

                splitContainer1.Panel1.Controls.Clear();
                if(data.Count == 0)
                {
                    toolStripMenuItemCalculate.Enabled = false;
                    toolStripMenuItemDraw.Enabled = false;
                    return;
                }
                AddControlsForFileTask(data);
                toolStripMenuItemCalculate.Enabled = true;
                toolStripMenuItemDraw.Enabled = true;
                return;
            }

            int pointsInOneRow = 0;
            if(!PrepareVariables(data, ref pointsInOneRow))
            {
                return;
            }

            DetermineIntervalNewMeasuringPoints(ref data, ref pointsInOneRow);
            
            if (pressedMenu == 0) // calc volume
            {
                TextBox textBoxVolume = AddControlsForVolume();

                double volume = Calculation.CalculateVolume(data, pointsInOneRow);
                textBoxVolume.Text = volume.ToString();
            }

            else if (pressedMenu == 1) // calc trucks
            {
                DataGridView dataGridViewTrucks = AddDataGridForTrucks();
                double volume = Calculation.CalculateVolume(data, pointsInOneRow);
                FillDataGridViewTrucks(dataGridViewTrucks, volume);
            }

            else if (pressedMenu == 2) // draw profiles
            {
                TabControl tabControlHillProfiles = new TabControl()
                {
                    Location = new Point(55, 20),
                    Dock = DockStyle.Fill
                };
                splitContainer1.Panel2.Controls.Add(tabControlHillProfiles);

                for (int i = 0; i < pointsInOneRow; i++)
                {
                    AddAndFillTabPagesForProfiles(tabControlHillProfiles, data, pointsInOneRow, i);
                }
            }

            else if (pressedMenu == 3) // draw contour map
            {
                PlotModel model = AddPlotViewModelForContourMap();
                double maxHeight = EditingData.FindMaxHeight(data);
                Drawing.DetermineContourLines(data, model, pointsInOneRow, maxHeight);               
            }
        }

        private void DetermineIntervalNewMeasuringPoints(ref List<double[]> data, ref int pointsInOneRow)
        {
            if (toolStripComboBox1.SelectedIndex == toolStripComboBox1.Items.Count-1)
            {
                return;
            }
            else if (toolStripComboBox1.SelectedIndex == 0)
            {
                CreateMeasuredPoints(ref data, ref pointsInOneRow, 0.1f);
            }
            else if(toolStripComboBox1.SelectedIndex == 1)
            {
                CreateMeasuredPoints(ref data, ref pointsInOneRow, 0.2f);
            }
            else if (toolStripComboBox1.SelectedIndex == 2)
            {
                CreateMeasuredPoints(ref data, ref pointsInOneRow, 0.25f);
            }
            else if (toolStripComboBox1.SelectedIndex == 3)
            {
                CreateMeasuredPoints(ref data, ref pointsInOneRow, 0.5f);
            }
        }

        private void CreateMeasuredPoints(ref List<double[]> data, ref int pointsInOneRow, float interval)
        {
            float newSideLength = (float) (data[1][0] - data[0][0]) * interval; 
            List<double[]> extraPoints = new List<double[]> { };
            for (int i = 0; i < data.Count - 1; i++)
            {
                for (float j = newSideLength; j < data[i + 1][0] - data[i][0]; j += newSideLength)
                {

                    double[] newPoint = { data[i][0] + j * (data[i + 1][0] - data[i][0]), data[i][1], data[i][2] + j * (data[i + 1][2] - data[i][2]) };

                    extraPoints.Add(newPoint);
                }
            }
            foreach (double[] item in extraPoints)
            {
                data.Add(item);
            }
            extraPoints.Clear();
            EditingData.SortData(data);
            Calculation.DeterminePointsInOneRow(data, ref pointsInOneRow);
            for (int i = 0; i < data.Count - pointsInOneRow; i++)
            {
                for (float j = newSideLength; j < data[i + pointsInOneRow][1] - data[i][1]; j += newSideLength)
                {
                    double[] newPoint = { data[i][0], data[i][1] + j * (data[i + pointsInOneRow][1] - data[i][1]), data[i][2] + j * (data[i + pointsInOneRow][2] - data[i][2]) };

                    extraPoints.Add(newPoint);
                }
            }
            foreach (double[] item in extraPoints)
            {
                data.Add(item);
            }
            EditingData.SortData(data);
        }

        private void AddControlsForFileTask(List<double[]> data)
        {
            DataGridView dataGridViewFileContent = new DataGridView()
            {
                ColumnCount = 4,
                RowCount = data.Count,
                Location = new Point(10, 20),
                RowHeadersVisible = false,
                Dock = DockStyle.Fill

            };

            dataGridViewFileContent.Columns[0].HeaderText = "Point";
            dataGridViewFileContent.Columns[1].HeaderText = "x";
            dataGridViewFileContent.Columns[2].HeaderText = "y";
            dataGridViewFileContent.Columns[3].HeaderText = "z";

            splitContainer1.Panel1.Controls.Add(dataGridViewFileContent);

            for (int i = 0; i < data.Count; i++)
            {
                dataGridViewFileContent.Rows[i].Cells[0].Value = i;
                dataGridViewFileContent.Rows[i].Cells[1].Value = data[i][0];
                dataGridViewFileContent.Rows[i].Cells[2].Value = data[i][1];
                dataGridViewFileContent.Rows[i].Cells[3].Value = data[i][2];
            }
        }

        private bool ExecuteFileTask(List<double[]> data)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "txt files (*.txt)|*.txt";
            
            if(openDlg.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            string path = openDlg.FileName;
            toolStripStatusLabel1.Text = path;
            try
            {
                StreamReader str = new StreamReader(toolStripStatusLabel1.Text);
                string line = str.ReadLine();
                EditingData.FillData(line, data);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("The file " + ex.FileName + " could not be found, please check the file path.");
                return false;
            }
            catch(ArgumentException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        private bool PrepareVariables(List<double[]> data, ref int pointsInOneRow)
        {
            if (toolStripStatusLabel1.Text == string.Empty)
            {
                MessageBox.Show("Pls enter a file path.");
                return false;
            }

            try
            {
                StreamReader str = new StreamReader(toolStripStatusLabel1.Text);
                string line = str.ReadLine();
                EditingData.FillData(line, data);
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("The file " + e.FileName + " could not be found, please check the file path.");
                return false;
            }
            EditingData.SortData(data);
            EditingData.adjustHeight(data);
            Calculation.DeterminePointsInOneRow(data, ref pointsInOneRow);
            return true;
        }

        private TextBox AddControlsForVolume()
        {
            Label labelVol = new Label()
            {
                Location = new Point(50, 150),
                Text = "V:",
                Width = 20

            };
            Label labelMeter = new Label()
            {
                Location = new Point(162, 155),
                Text = "m"
            };
            TextBox textBoxVolume = new TextBox()
            {
                ReadOnly = true,
                Location = new Point(72, 150),
                Width = 100
            };

            splitContainer1.Panel2.Controls.Add(labelVol);
            splitContainer1.Panel2.Controls.Add(labelMeter);
            splitContainer1.Panel2.Controls.Add(textBoxVolume);
            return textBoxVolume;
        }

        private DataGridView AddDataGridForTrucks()
        {
            DataGridView dataGridViewTrucks = new DataGridView()
            {
                Location = new Point(55, 20),
                Dock = DockStyle.Fill,
                RowHeadersVisible = false,
                ColumnCount = 2
            };
            dataGridViewTrucks.Columns[0].HeaderText = "Number of trucks";
            dataGridViewTrucks.Columns[1].HeaderText = "Time";
            splitContainer1.Panel2.Controls.Add(dataGridViewTrucks);
            return dataGridViewTrucks;
        }

        private void FillDataGridViewTrucks(DataGridView dataGridViewTrucks, double volume)
        {
            int numberOfTrucks = 100;
            dataGridViewTrucks.RowCount = numberOfTrucks;
            for (int i = 1; i <= numberOfTrucks; i++)
            {
                dataGridViewTrucks.Rows[i - 1].Cells[0].Value = i;
                dataGridViewTrucks.Rows[i - 1].Cells[1].Value = Math.Ceiling((volume / 7) / i) * 30;
            }
        }

        private void AddAndFillTabPagesForProfiles(TabControl tabControlHillProfiles, List<double[]> data, int pointsInOneRow, int i)
        {
            string title = "Profile " + (tabControlHillProfiles.TabCount + 1).ToString();
            TabPage tabPage = new TabPage(title);
            tabControlHillProfiles.TabPages.Add(tabPage);

            PlotModel plot = new PlotModel();
            PlotView plotView2 = new PlotView() { BackColor = Color.White, Dock = DockStyle.Fill };
            plotView2.Model = plot;
            tabPage.Controls.Add(plotView2);
            double maxHeight = EditingData.FindMaxHeight(data);
            Drawing.DrawColumnSeries(ref plot, data, pointsInOneRow, maxHeight, i);
        }

        private PlotModel AddPlotViewModelForContourMap()
        {
            PlotView plotContour = new PlotView()
            {
                BackColor = Color.White,
                Location = new Point(55, 20),
                Dock = DockStyle.Fill
            };
            PlotModel model = new PlotModel();
            plotContour.Model = model;
            splitContainer1.Panel2.Controls.Add(plotContour);
            return model;
        }

    }
    
}
