using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGenerator.UI.Models
{
    public class SVG_model
    {        
        public int Id { get; set; }
        public string svgId { get; set; }
        public int svgRow { get; set; }
        public int svgX { get; set; }
        public int svgY { get; set; }
        public int svgCol { get; set; }
        public bool svgReserved { get; set; }

    }
}