using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Printing.Viewer
{
    public delegate void CommandEventHandler(object sender, string command);
    public delegate void ActivateEventHandler(object sender);
    public delegate void PrinteEventHandler(object sender);
}
