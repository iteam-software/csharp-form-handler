using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEAMConsulting.FormHandler
{
    public class MakeEmailsOptions
    {
        public string Title { get; set; } = "Form Data Submission";
        public string Description { get; set; } = "The following data was submitted by the user.";
        public string Font { get; set; } = "Arial, Verdana, sans-serif";
        public int FontSize { get; set; } = 14;
        public string FontColor { get; set; } = "#222222";
        public int CellPadding { get; set; } = 8;
        public int CellBorder { get; set; } = 1;
    }
}
