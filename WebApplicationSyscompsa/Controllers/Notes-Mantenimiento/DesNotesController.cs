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

namespace WebApplicationSyscompsa.Controllers.Notes_Mantenimiento
{
    [Route("api/desNotes")]
    [ApiController]
    public class DesNotesController : ControllerBase
    {

        private readonly AppDbContext _context;

        public DesNotesController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpPost]
        [Route("save_desNotes")]
        public async Task<IActionResult> save_desNotes([FromBody] Des_notes model)
        {

            if (ModelState.IsValid)
            {

                _context.des_notes.Add(model);

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
        [Route("putDesnotes/{Id}")]
        public async Task<IActionResult> putDesnotes([FromRoute] int Id, [FromBody] Des_notes model)
        {

            if (Id != model.id)
            {
                return BadRequest("El ID del producto no es compatible, o no existe");
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(model);

        }

        [HttpGet]
        [Route("Del_desNotes/{id}")]
        public ActionResult<DataTable> Del_desNotes([FromRoute] int id)
        {
            string Sentencia = " delete from des_notes where id = @ID ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ID", id));
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
        [Route("Get_desNotes/{order}/{token}/{CodClass}")]
        public ActionResult<DataTable> Get_desNotes([FromRoute] string order, [FromRoute] string token, [FromRoute] string CodClass)
        {

            string Sentencia = " select * from des_notes where token_user = @TOKEN and cod_class = @codClass  order by id " + order;

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@TOKEN",    token));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@codClass", CodClass));
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
