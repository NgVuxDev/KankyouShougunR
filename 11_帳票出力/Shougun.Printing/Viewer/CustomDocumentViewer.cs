using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Xps;
using System.Printing;
using System.Printing.Interop;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;

namespace Shougun.Printing.Viewer
{
    internal class CustomDocumentViewer : DocumentViewer
    {
        internal event CommandEventHandler CommandEventHandler;

        protected override void OnPrintCommand()
        {
            if (this.CommandEventHandler != null)
            {
                this.CommandEventHandler(this, "Print");
            }
        }
    }
}