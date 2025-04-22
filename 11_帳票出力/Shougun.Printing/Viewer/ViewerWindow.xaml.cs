
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Xps.Packaging;
using System.Printing;
using System.Printing.Interop;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Documents;

namespace Shougun.Printing.Viewer
{
    public partial class ViewerWindow : Window
    {
        public event CommandEventHandler CommandEventHandler;
        private XpsDocument xpsDocument = null;
        public ViewerWindow() : base()
        {
            InitializeComponent();
            AddCommandBindings(ApplicationCommands.Close, CloseCommandHandler);
            this.docViewer.CommandEventHandler += this.OnDocViewerCommand;
        }

        public void ViewXpsFile(string path, string title)
        {
            this.closeXpsDocumen();
            this.xpsDocument = new XpsDocument(path, System.IO.FileAccess.Read);
            this.docViewer.Document = this.xpsDocument.GetFixedDocumentSequence();
            this.Title = title;
        }

        private void closeXpsDocumen()
        {
            if (this.xpsDocument != null)
            {
                this.docViewer.Document = null;
                this.xpsDocument.Close();
                this.xpsDocument = null;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.closeXpsDocumen();
            this.onCommandProc("Closed");
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.onCommandProc("Close");
        }

        /// <summary>
        ///     Registers menu commands (helper method).</summary>
        /// <param name="command"></param>
        /// <param name="handler"></param>
        private void AddCommandBindings(ICommand command, ExecutedRoutedEventHandler handler)
        {
            CommandBinding cmdBindings = new CommandBinding(command);
            cmdBindings.Executed += handler;
            CommandBindings.Add(cmdBindings);
        }

        private void OnDocViewerCommand(object sender, string command)
        {
            this.onCommandProc(command);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.onCommandProc("Activated");
        }

        private void onCommandProc(string command)
        {
            switch (command)
            {
                default:
                    if (this.CommandEventHandler != null)
                    {
                        this.CommandEventHandler(this, command);
                    }
                    break;

                case "PreviousPage":
                    this.docViewer.PreviousPage();
                    break;

                case "NextPage":
                    this.docViewer.NextPage();
                    break;

                case "Close":
                    this.Close();
                    break;
            }
        }

        private void prevPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.docViewer.PreviousPage();
        }

        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.docViewer.NextPage();
        }

        private void docViewer_PageViewsChanged(object sender, EventArgs e)
        {
            if (this.docViewer.PageViews.Count > 0)
            {
                this.pageLabel.Content = string.Format("page: {0}/{1}  ", this.docViewer.PageViews[0].PageNumber+1, this.docViewer.PageCount);
            }
//          this.F1Button.IsEnabled = !this.docViewer.CanGoToPreviousPage;
//          this.F2Button.IsEnabled = !this.docViewer.CanGoToNextPage;
        }

        private void prevContentButton_Click(object sender, RoutedEventArgs e)
        {
            this.onCommandProc("PreviousContent");
        }

        private void nextContentButton_Click(object sender, RoutedEventArgs e)
        {
            this.onCommandProc("NextContent");
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            this.onCommandProc("Delete");
        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            this.onCommandProc("Print");
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.onCommandProc("Close");
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.CommandEventHandler == null)
            {
                return;
            }

            if (e.IsRepeat)
            {
                return;
            }

            if (Keyboard.Modifiers != ModifierKeys.None)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.F1:
                    this.onCommandProc("PreviousPage");
                    break;
                case Key.F2:
                    this.onCommandProc("NextPage");
                    break;
                case Key.F3:
                    this.onCommandProc("PreviousContent");
                    break;
                case Key.F4:
                    this.onCommandProc("NextContent");
                    break;
                case Key.F7:
                    this.onCommandProc("Delete");
                    break;
                case Key.F9:
                    this.onCommandProc("Print");
                    break;
                case Key.F12:
                    this.onCommandProc("Close");
                    break;
                default:
                    return;
            }
            e.Handled = true;
        }
    }
}
