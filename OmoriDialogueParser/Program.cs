using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text.Json;
using OmoriDialogueParser.Model;
using OmoriDialogueParser.Model.Data;
using OmoriDialogueParser.Model.MessageTextPayloads;
using System.Windows.Forms;

namespace OmoriDialogueParser
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Launch the main form
            Application.Run(new MainForm());
        }
    }
}
