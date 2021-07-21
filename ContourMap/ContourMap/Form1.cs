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
            List<double[]> data = new List<double[]> { };
            
            if (pressedMenu == -1)
            {
                ExecuteFileTask(data);

                AddControlsForFileTask(data);               
            }

            int pointsInOneRow = 0;
            if(!PrepareVariables(data, ref pointsInOneRow))
            {
                return;
            }
            
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
                    Location = new Point(550, 30),
                    Width = 430,
                    Height = 329
                };
                this.Controls.Add(tabControlHillProfiles);

                for (int i = 0; i < pointsInOneRow; i++)
                {
                    AddAndFillTabPagesForProfiles(tabControlHillProfiles, data, pointsInOneRow, i);

                }
            }

            else if (pressedMenu == 3) // draw contour map
            {
                PlotModel model = AddPlotViewModelForContourMap();

                Drawing.DetermineContourLines(data, model, pointsInOneRow);               
            }
        }

        private void AddControlsForFileTask(List<double[]> data)
        {
            DataGridView dataGridViewFileContent = new DataGridView()
            {
                ColumnCount = 4,
                RowCount = data.Count,
                Location = new Point(20, 30),
                RowHeadersVisible = false,
                Width = 430,
                Height = 329

            };

            dataGridViewFileContent.Columns[0].HeaderText = "Point";
            dataGridViewFileContent.Columns[1].HeaderText = "x";
            dataGridViewFileContent.Columns[2].HeaderText = "y";
            dataGridViewFileContent.Columns[3].HeaderText = "z";

            this.Controls.Add(dataGridViewFileContent);

            for (int i = 0; i < data.Count; i++)
            {
                dataGridViewFileContent.Rows[i].Cells[0].Value = i;
                dataGridViewFileContent.Rows[i].Cells[1].Value = data[i][0];
                dataGridViewFileContent.Rows[i].Cells[2].Value = data[i][1];
                dataGridViewFileContent.Rows[i].Cells[3].Value = data[i][2];
            }
            label3.Visible = true;
            textBoxFilePath.Visible = true;
        }

        private void ExecuteFileTask(List<double[]> data)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.ShowDialog();
            string path = openDlg.FileName;
            textBoxFilePath.Text = path;
            try
            {
                StreamReader str = new StreamReader(textBoxFilePath.Text);
                string line = str.ReadLine();
                EditingData.FillData(line, data);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("The file " + ex.FileName + " could not be found, please check the file path.");
                return;
            }
        }

        private bool PrepareVariables(List<double[]> data, ref int pointsInOneRow)
        {
            if (textBoxFilePath.Text == string.Empty)
            {
                MessageBox.Show("Pls enter a file path.");
                return false;
            }

            try
            {
                StreamReader str = new StreamReader(textBoxFilePath.Text);
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
                Location = new Point(500, 205),
                Text = "V:",
                Width = 20

            };
            Label labelMeter = new Label()
            {
                Location = new Point(620, 205),
                Text = "m"
            };
            TextBox textBoxVolume = new TextBox()
            {
                ReadOnly = true,
                Location = new Point(520, 200),
                Width = 100
            };

            this.Controls.Add(labelVol);
            this.Controls.Add(labelMeter);
            this.Controls.Add(textBoxVolume);
            return textBoxVolume;
        }

        private DataGridView AddDataGridForTrucks()
        {
            DataGridView dataGridViewTrucks = new DataGridView()
            {
                Location = new Point(550, 30),
                Width = 200,
                Height = 329,
                RowHeadersVisible = false,
                ColumnCount = 2
            };
            dataGridViewTrucks.Columns[0].HeaderText = "Number of trucks";
            dataGridViewTrucks.Columns[1].HeaderText = "Time";
            this.Controls.Add(dataGridViewTrucks);
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
            Drawing.DrawColumnSeries(ref plot, data, pointsInOneRow, i);
        }

        private PlotModel AddPlotViewModelForContourMap()
        {
            PlotView plotContour = new PlotView()
            {
                BackColor = Color.White,
                Location = new Point(550, 30),
                Width = 430,
                Height = 329
            };
            PlotModel model = new PlotModel();
            plotContour.Model = model;
            this.Controls.Add(plotContour);
            return model;
        }

    }
    
}
