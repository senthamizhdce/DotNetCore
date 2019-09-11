using DotNetCore.Others.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AJAXController : ControllerBase
    {
        //https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.2&tabs=visual-studio#call-the-web-api-with-javascript


        static List<TodoItem> TodoItemList;

        public AJAXController()
        {
            if (TodoItemList == null)
            {
                TodoItemList = new List<TodoItem> {
                new TodoItem() { Id = 1, IsComplete = true, Name = "Selvan" },
                new TodoItem() { Id = 2, IsComplete = true, Name = "Senthamizh" } };
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            return TodoItemList;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(long id)
        {
            var todoItem = TodoItemList.Find(a => a.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public ActionResult<TodoItem> PostTodoItem(TodoItem item)
        {

            item.Id = TodoItemList.Max(a => a.Id) + 1;

            TodoItemList.Add(item);

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var todoItem = TodoItemList.Find(a => a.Id == id);

            todoItem.Name = item.Name;
            todoItem.IsComplete = item.IsComplete;

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(long id)
        {
            var todoItem = TodoItemList.Find(a => a.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            TodoItemList.Remove(todoItem);

            return NoContent();
        }
    }
}