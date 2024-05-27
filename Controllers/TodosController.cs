using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ToDo_App_Backend.Models;
using ToDo_App_Backend.MyAppDbContext;

namespace ToDo_App_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly MyDbContext _context;

        public TodosController(MyDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDo>>> GetToDos()
        {
            var todos = await _context.ToDos.ToListAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetToDo(int id)
        {
            var todo = await _context.ToDos.FirstOrDefaultAsync(e => e.Id == id);
            if (todo != null)
            {
                return Ok(todo);
            }
            return NotFound(new { error = "Todos not found" });
        }

        [HttpPost]
        public async Task<ActionResult<List<ToDo>>> AddToDo(ToDo newToDo)
        {
            if (newToDo != null)
            {
                _context.ToDos.Add(newToDo);
                await _context.SaveChangesAsync();


                var todos = await _context.ToDos.ToListAsync();
                return Ok(todos);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult<List<ToDo>>> DeleteToDo(int id)
        {
            var todo = await _context.ToDos.FirstOrDefaultAsync(e => e.Id == id);
            if (todo != null)
            {
                _context.ToDos.Remove(todo);
                await _context.SaveChangesAsync();

                var todos = await _context.ToDos.ToListAsync();
                return Ok(todos);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<ToDo>> UpdateToDo(ToDo updatedToDo)
        {
            if (updatedToDo != null)
            {
                var todo = await _context.ToDos.FirstOrDefaultAsync(e => e.Id == updatedToDo.Id);
                if (todo != null)
                {
                    todo.TodoName = updatedToDo.TodoName;
                    todo.Description = updatedToDo.Description;
                    await _context.SaveChangesAsync();

                    var todos = await _context.ToDos.ToListAsync();
                    return Ok(todos);
                }
                return NotFound(new { error = "Todo not found" });
            }
            return BadRequest();
        }

        //update status
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoStatus(int id)
        {
            var existingTodo = await _context.ToDos.FindAsync(id);
            if (existingTodo == null)
            {
                return NotFound(new { error = "Todo not found" });
            }

            existingTodo.IsCompleted = !existingTodo.IsCompleted;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            return Ok(existingTodo);
        }

    }
}
