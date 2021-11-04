using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationSyscompsa.Models
{
    public class User_web
    {
      public string nom_user        { get; set; }
      public string foto_user       { get; set; }
      public string token_user      { get; set; }
      public string dire_user       { get; set; }
      public string email_user      { get; set; } 
      public DateTime fcrea         { get; set; }
      public string estado_user     { get; set; }
      public string cod_labor_user  { get; set; }
      public string password        { get; set; }
    }
}
