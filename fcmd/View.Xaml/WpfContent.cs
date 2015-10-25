using fcmd.base_plugins.fs;
using fcmd.View.ctrl;
using pluginner;
using pluginner.Widgets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace fcmd.View.Xaml
{
    public static class WpfContent
    {
        public static void Load(this PanelWpf panelContainer, string url, string fileProcol = "file://")
        {
            string path = url;
            string fullpath = null;
            string[] parts = path.Split(new[] { ':' });
            var protocol = parts[0];

            try
            {
                if (parts.Length > 1 && protocol.Length > 1)
                {
                    protocol += "://";
                    fullpath = parts[1];
                }
                else
                {
                    protocol = base_plugins.fs.localFileSystem.FilePrefix;
                    fullpath = path.StartsWith(fileProcol)
                            ? Path.GetFullPath(path.Substring(fileProcol.Length)) : Path.GetFullPath(path);
                    Directory.SetCurrentDirectory(fullpath);
                }
            }
            catch (Exception ex) { MessageDialog.ShowError(ex.Message); }

            if (string.IsNullOrWhiteSpace(fullpath))
                return;

            App.ConsoleWriteLine("Widget:LoadDir " + fullpath);

            IFileListPanel panel = panelContainer.PanelDataWpf;
            var dataGrid = panelContainer.data as ListView2DataGrid;
            pluginner.IFSPlugin fs = null;

            if (protocol != base_plugins.fs.localFileSystem.FilePrefix)
            {
                pluginfinder pf = new pluginfinder();
                fs = pf.GetFSplugin(url);

                if (fs != null)
                {
                    var currentContent = panelContainer.contentPanel.Content;
                    if (dataGrid != null && dataGrid.ItemsSource != null)
                        dataGrid.ItemsSource = null;

                    if (fs is pluginner.IVisualPlugin)
                    {
                        var fsVisual = fs as pluginner.IVisualPlugin;

                        var control = fsVisual.AttachToPanel(panel, panelContainer.browser, panelContainer.browser.Browser) as ListView2WebBrowser;

                        if (control != null)
                        {
                            panelContainer.contentPanel.Content = control.Browser;

                            if (dataGrid != null)
                                dataGrid.Visible = false;
                            control.Visible = true;
                        }
                    }
                    else
                    {
                        if (currentContent is UIElement
                            && !(currentContent is ListView2DataGrid))
                            (currentContent as UIElement).Visibility = Visibility.Collapsed;

                        panelContainer.contentPanel.Content = dataGrid;
                        dataGrid.Visible = true;
                    }

                    try
                    {
                        fs.CurrentDirectory = url;
                        //var status = new FileSystemOperationStatus();
                        //fs.GetDirectoryContent(status);

                        panel.UrlBox.Text = fs.CurrentDirectory;
                    }
                    catch (Exception ex)
                    {
                        if (fs != null)
                            fs.LastError = ex;
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else if (dataGrid != null)
            {
                if (panelContainer.contentPanel.Content != dataGrid) // is System.Windows.Controls.UserControl)
                    panelContainer.contentPanel.Content = dataGrid;
                if (!dataGrid.Visible ?? false)
                    dataGrid.Visibility = Visibility.Visible;

                if (dataGrid.FileList != null)
                    panel.UrlBox.Text = dataGrid.FileList.FS.Prefix + fullpath;
            }

            if (dataGrid != null && (dataGrid.Visible ?? false))
            {
                var fsPanel = dataGrid.FileList;
                if (fsPanel == null)
                {
                    panelContainer.PanelDataWpf.Initialize(panelContainer.Side);
                    fsPanel = dataGrid.FileList;
                }

                if (fsPanel != null)
                {
                    if (fs == null)
                        // fsPanel.LoadDir(dataGrid.FileList.FS.Prefix + fullpath);
                        fsPanel.LoadDirThen(fullpath, null, () => dataGrid.Bind());
                    else
                        fsPanel.LoadPluginFs(fs, fileProcol + fullpath,
                            () => dataGrid.Bind());
                }
            }
        }

        public static void AddVisual(this PanelWpf panel, IControl control, string[] protocols)
        {
            var dataGridVisual = new PanelWpf.PluginsVisual { Control = control, Protocols = protocols };
            panel.Visuals.Add(dataGridVisual);
            dataGridVisual.Control.Visible = true;
        }
    }
}
