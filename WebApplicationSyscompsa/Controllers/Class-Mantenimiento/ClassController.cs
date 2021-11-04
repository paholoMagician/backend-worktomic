using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSyscompsa.Models;

namespace WebApplicationSyscompsa.Controllers.Class_Mantenimiento
{
    [Route("api/class")]
    [ApiController]
    public class ClassController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ClassController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpPost]
        [Route("save_classNotes")]
        public async Task<IActionResult> save_classNotes([FromBody] Clasnotes model)
        {

            if (ModelState.IsValid)
            {

                _context.clasnotes.Add(model);

                if (await _context.SaveChangesAsync() > 0) 
                {
                    return Ok(model);
                }
                else
                {
                    return BadRequest("Datos incorrectos");
                }
            }

            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("put_classNotes/{n_class}")]
        public async Task<IActionResult> put_classNotes([FromRoute] string n_class, [FromBody] Clasnotes model)
        {

            if (n_class != model.n_class)
            {
                return BadRequest("El ID del producto no es compatible, o no existe");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return Ok(model);

        }

        [HttpGet]
        [Route("Get_classNotes/{order}/{token}")]
        public ActionResult<DataTable> Get_classNotes([FromRoute] string order, [FromRoute] string token)
        {

            string Sentencia = "select * from clasnotes where token_user = @TOKEN order by n_class " + order;

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@TOKEN", token));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {
                return NotFound("");
            }

            return Ok(dt);

        }

        [HttpGet]
        [Route("Del_classNotes/{token}/{cclas}")]
        public ActionResult<DataTable> Del_classNotes([FromRoute] string token, [FromRoute] string cclas)
        {

            string Sentencia = "delete from clasnotes where token_user = @TOKEN and cod_class = @Cclass ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
            
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection)) 
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@TOKEN", token));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Cclass", cclas));
                    adapter.Fill(dt);
                }
            
            }

            if (dt == null)
            {
                return NotFound("");
            }

            return Ok(dt);

        }


    }
}
