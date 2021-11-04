﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationSyscompsa.Models;

namespace WebApplicationSyscompsa.Controllers
{
    [Route("api/UserLogin")]
    [ApiController]
    public class WebUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WebUserController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("GetUsuarios")]
        public IEnumerable<User_web> GetClientes()
        {
            return _context.user_web;
        }

        [HttpGet]
        [Route("getuser/{User}")]
        public ActionResult<DataTable> getReporteByParam([FromRoute] string User) {

            string Sentencia = " select * from user_web where email_user = @User ";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Sentencia, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@User", User));
                    adapter.Fill(dt);
                }
            }

            if (dt == null)
            {

                return NotFound("");

            }

            return Ok(dt);

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] User_web userInfo) {

            var result = await _context.user_web.FirstOrDefaultAsync(x =>
                         x.email_user == userInfo.email_user
                         && x.password == userInfo.password);

            if (result != null) {
                return Ok(result);
            }

            else {
                return BadRequest("Datos incorrectos");
            }

        }
    }
}