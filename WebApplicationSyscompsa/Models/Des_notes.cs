using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationSyscompsa.Models
{
    public class Des_notes
    {
      public int        id          { get; set; }
      public DateTime   finit       { get; set; }
      public DateTime   ffin        { get; set; }
      public DateTime   freal       { get; set; }
      public decimal    time        { get; set; }
      public string     n_notes     { get; set; }
      public string     d_notes     { get; set; }
      public string     observacion { get; set; }
      public string     token_user  { get; set; }
      public string     cod_class   { get; set; }
      public string     campoA      { get; set; }
      public decimal    campoB      { get; set; }
      public decimal    cantidad    { get; set; }
    }
}
