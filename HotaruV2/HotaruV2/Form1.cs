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

namespace HotaruV2
{
    public partial class Form1 : Form
    {
        private ListViewColumnSorter lvwColumnSorter;

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
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                TreeNode rootNode = new TreeNode(drive.Name);
                rootNode.ImageIndex = rootNode.SelectedImageIndex = 0;
                rootNode.Tag = drive.Name;
                treeView.Nodes.Add(rootNode);
                rootNode.Nodes.Add("");
            }
        }

        private static void ShowAllFoldersUnder(string path, TreeNode parent, int i)
        {
            int count = i;
            if (count < 3)
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

        //Clears the current list of files and displays all file information in the selected folder
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listView.Items.Clear();
            try
            {
                foreach (String file in Directory.GetFiles(e.Node.Tag.ToString()))
                {
                    ListViewItem lvi = new ListViewItem(Path.GetFileNameWithoutExtension(file));
                    lvi.SubItems.Add(Path.GetExtension(file));
                    lvi.SubItems.Add(file);
                    listView.Items.Add(lvi);
                }
            }
            catch (UnauthorizedAccessException) { }
        }

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
    }
}
