using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoApp.Core.DBEntities;
using TodoApp.Core.Interfaces;
using TodoApp.Web.EnpointModel;
using TodoApp.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;


namespace TodoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class TodoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ITodoService _todoService;
        private readonly IResponseGeneric _responseGeneric;
        private readonly ILogService _logService;
        private readonly IUserService _customerService;

        public TodoController(
           ITodoService todoService,
           IMapper mapper,
           IConfiguration configuration,
           IResponseGeneric responseGeneric,
           ILogService logService,
           IUserService customerService) : base(customerService)
        {
            _todoService = todoService;
            _mapper = mapper;
            _configuration = configuration;
            _responseGeneric = responseGeneric;
            _logService = logService;
            _customerService = customerService;
        }

        [HttpGet("getTodos")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetTodos()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Logging
                    await _logService.InsertLog(LogLevel.Information, "GetTodos");

                    var todos = await _todoService.GetAllAsync();

                    List<TodoResponse> listOfTodos = new List<TodoResponse>();
                    foreach (var dbItem in todos)
                    {
                        listOfTodos.Add(new TodoResponse()
                        {
                            Status = dbItem.Status,
                            Title = dbItem.Title,
                            UserId = dbItem.Customer.ApplicationUser.Id,
                            UserName = dbItem.Customer.Name

                        });
                    }

                    return Ok(_responseGeneric.Success(result: listOfTodos));
                }
                else
                {
                    return BadRequest(_responseGeneric.Error(result: ModelState));
                }

            }
            catch (Exception ex)
            {
                //Logging
                Log logDetail = _logService.InsertLog(LogLevel.Error, "GetTodos", ex.ToString()).Result;
                return BadRequest(_responseGeneric.Error(result: ex.Message.ToString(), log: logDetail));
            }
        }

        [HttpGet("getTodosByUserId")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetTodosByUserId(string userId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Logging
                    await _logService.InsertLog(LogLevel.Information, "GetTodosByUserId");

                    //Getting current user:
                    var currentUser = GetCurrentUser();

                    var todos = await _todoService.GetAllByUserIdAsync(currentUser.User.Id);

                    List<TodoResponse> listOfTodos = new List<TodoResponse>();
                    foreach (var dbItem in todos)
                    {
                        listOfTodos.Add(new TodoResponse()
                        {
                            TodoId=dbItem.Id,
                            Status = dbItem.Status,
                            Title = dbItem.Title,
                            UserId = dbItem.Customer.ApplicationUser.Id
                        });
                    }

                    return Ok(_responseGeneric.Success(result: listOfTodos));
                }
                else
                {
                    return BadRequest(_responseGeneric.Error(result: ModelState));
                }

            }
            catch (Exception ex)
            {
                //Logging
                Log logDetail = _logService.InsertLog(LogLevel.Error, "GetTodos", ex.ToString()).Result;
                return BadRequest(_responseGeneric.Error(result: ex.Message.ToString(), log: logDetail));
            }
        }
        
        [HttpPut("updateTodo")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateTodo([FromBody]TodoUpdateRequest todoData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Logging
                    await _logService.InsertLog(LogLevel.Information, "UpdateTodo");

                    //Getting current user:
                    var currentUser = GetCurrentUser();

                    //Get Todo
                    var todo = await _todoService.GetByIdAsync(todoData.TodoId);
                    if (todo == null)
                        return NotFound(_responseGeneric.Error("User not found"));

                    //Update
                    todo.Status = todoData.Status;
                    await _todoService.UpdateAsync(todo);

                    return Ok(_responseGeneric.Success());
                }
                else
                {
                    return BadRequest(_responseGeneric.Error(result: ModelState));
                }

            }
            catch (Exception ex)
            {
                //Logging
                Log logDetail = _logService.InsertLog(LogLevel.Error, "UpdateTodo", ex.ToString()).Result;
                return BadRequest(_responseGeneric.Error(result: ex.Message.ToString(), log: logDetail));
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TodoRequest todoRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region VALIDATIONS AND BASIC DATA
                    //Get the admin:
                    var currentUser = GetCurrentUser();

                    //Get the customer in request:
                    var customerInRequest = await _customerService.FindByIdAsync(todoRequest.UserId);
                    if (customerInRequest == null)
                        return NotFound(_responseGeneric.Error("User not found"));
                    #endregion

                    #region OPERATIONS
                    Todo todo = new Todo();
                    todo.Title = todoRequest.Title;
                    todo.Customer = customerInRequest.User;
                    todo.Status = TodoStatus.Pending;

                    //Save to DB and also handling the Concurrency issue:
                    await _todoService.AddAsync(todo);
                    #endregion

                    //Logging
                    await _logService.InsertLog(LogLevel.Information, "Order has successfully created", JsonSerializer.Serialize(todoRequest));

                    return Ok(_responseGeneric.Success("Order has successfully created"));
                }
                else
                {
                    return BadRequest(_responseGeneric.Error(result: ModelState));
                }

            

            }
            catch (Exception ex)
            {
                //Logging error
                Log logDetail = _logService.InsertLog(LogLevel.Error, "CreateOrder", ex.ToString()).Result;
                return BadRequest(_responseGeneric.Error(result: ex.Message.ToString(), log: logDetail));
            }
        }
    }

}
