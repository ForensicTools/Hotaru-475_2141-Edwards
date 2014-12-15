using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HotaruV2
{
    public partial class Form1 : Form
    {
        private ListViewColumnSorter lvwColumnSorter;
        private string view;

        public Form1()
        {
            InitializeComponent();
            PopulateTreeView();
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView.ListViewItemSorter = lvwColumnSorter;
        }

        private void PopulateTreeView()
        {
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    TreeNode rootNode = new TreeNode(drive.Name);
                    rootNode.ImageIndex = rootNode.SelectedImageIndex = 0;
                    rootNode.Tag = drive.Name;
                    treeView.Nodes.Add(rootNode);
                    rootNode.Nodes.Add("");
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static void ShowAllFoldersUnder(string path, TreeNode parent, int i)
        {
            int count = i;
            if (count < 2)
            {
                count++;
                try
                {
                    if (((File.GetAttributes(path) & FileAttributes.ReparsePoint)
                        != FileAttributes.ReparsePoint) /*&& ((File.GetAttributes(path) & FileAttributes.System) != FileAttributes.System)*/)
                    {
                        foreach (string folder in Directory.GetDirectories(path))
                        {
                            TreeNode child = new TreeNode(folder);
                            child.ImageIndex = child.SelectedImageIndex = 1;
                            child.Tag = folder;
                            ShowAllFoldersUnder(folder, child, count);
                            parent.Nodes.Add(child);
                        }
                    }
                }
                catch (UnauthorizedAccessException) { }
            }
        }

        private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.Nodes.Clear();
            ShowAllFoldersUnder(e.Node.Tag.ToString(), e.Node, 0);
        }


        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Clears the current list of files and displays all file information in the selected folder
            listView.Items.Clear();
            chart.Series["Files"].Points.Clear();
            Dictionary<string, int> fileDictionary = new Dictionary<string, int>();
            try
            {
                foreach (String file in Directory.GetFiles(e.Node.Tag.ToString()))
                {
                    ListViewItem lvi = new ListViewItem(Path.GetFileNameWithoutExtension(file));
                    lvi.SubItems.Add(Path.GetExtension(file));
                    lvi.SubItems.Add(file);
                    //Add each different filetype with counts to the dictionary to use for graphing
                    if (fileDictionary.ContainsKey(Path.GetExtension(file).ToLower()))
                    {
                        fileDictionary[Path.GetExtension(file).ToLower()]++;
                    }
                    else
                    {
                        fileDictionary.Add(Path.GetExtension(file).ToLower(), 1);
                    }
                    listView.Items.Add(lvi);
                }
            }
            catch (UnauthorizedAccessException) { }

            switch (view)
            {
                case "Bar":
                    loadBarGraph(fileDictionary);
                    break;
                case "Pie":
                    loadPieGraph(fileDictionary);
                    break;
                default:
                    loadBarGraph(fileDictionary);
                    break;
            }
        }

        private void loadBarGraph(Dictionary<string, int> dictionary)
        {
            //chart.Series["Files"].Points.Clear();
            chart.Series["Files"].ChartType = SeriesChartType.Column;
            chart.Series["Files"].IsVisibleInLegend = false;
            // Add each file type w/ count to the graph
            for (int i = 0; i < dictionary.Count; i++)
            {
                string key = dictionary.OrderByDescending(k => k.Value).ElementAt(i).Key;
                int value = dictionary.OrderByDescending(k => k.Value).ElementAt(i).Value;

                chart.Series["Files"].Points.AddXY(key, value);
                if (value >= 1 && value <= 5)
                {
                    chart.Series["Files"].Points[i].Color = Color.Blue;
                }
                else if (value >= 6 && value <= 15)
                {
                    chart.Series["Files"].Points[i].Color = Color.Green;
                }
                else
                {
                    chart.Series["Files"].Points[i].Color = Color.Red;
                }
            }
        }

        private void loadPieGraph(Dictionary<string, int> dictionary)
        {
            //chart.Series["Files"].Points.Clear();
            chart.Series["Files"].ChartType = SeriesChartType.Pie;
            chart.Series["Files"].Palette = ChartColorPalette.BrightPastel;
            chart.Series["Files"].IsVisibleInLegend = true;

            chart.Series["Files"].XValueType = ChartValueType.String;

            for (int i = 0; i < dictionary.Count; i++)
            {
                string key = dictionary.OrderByDescending(k => k.Value).ElementAt(i).Key;
                int value = dictionary.OrderByDescending(k => k.Value).ElementAt(i).Value;

                chart.Series["Files"].Points.AddXY(key, value);
            }

        }

        // Code for this funtion obtained from msdn.microsoft.com
        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView.Sort();
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 1)
            {

                Process.Start(listView.SelectedItems[0].SubItems[2].Text);                
            }
        }

        private void chart_MouseClick(object sender, MouseEventArgs e)
        {
            treeView_AfterSelect(sender, new TreeViewEventArgs(treeView.SelectedNode));
            HitTestResult result = chart.HitTest(e.X, e.Y);
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                foreach (ListViewItem lvi in listView.Items)
                {
                    if (lvi.SubItems[1].Text.ToLower() != result.Series.Points[result.PointIndex].AxisLabel)
                    {
                        lvi.Remove();
                    }
                }
            }
        }

        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            HitTestResult result = chart.HitTest(e.X, e.Y);

            foreach (DataPoint point in chart.Series["Files"].Points)
            {
                point.BorderDashStyle = ChartDashStyle.NotSet;
            }

            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                DataPoint point = chart.Series["Files"].Points[result.PointIndex];

                point.BorderColor = Color.Black;
                point.BorderDashStyle = ChartDashStyle.Solid;
                point.BorderWidth = 4;
            }
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void barToolStripMenuItem_Click(object sender, EventArgs e)
        {
            view = "Bar";
            treeView_AfterSelect(sender, new TreeViewEventArgs(treeView.SelectedNode));
        }

        private void pieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            view = "Pie";
            treeView_AfterSelect(sender, new TreeViewEventArgs(treeView.SelectedNode));
        }
    }
}
