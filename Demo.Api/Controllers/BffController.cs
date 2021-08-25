using AutoMapper;
using Demo.Api.Attributes;
using Demo.Api.Dtos;
using Demo.Core.Entities;
using Demo.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;

namespace Demo.Api.Controllers
{
    [ApiController]
    [Route("bff")]
    public class BffController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly IRedisClientsManager _redisManager;
        private readonly ILogger<BffController> _logger;

        public BffController(IDepartmentService departmentService, 
                                IStudentService studentService, 
                                IRedisClientsManager redisManager, 
                                IMapper mapper,
                                ILogger<BffController> logger)
        {
            _departmentService = departmentService;
            _studentService = studentService;
            _mapper = mapper;
            _redisManager = redisManager;
            _logger = logger;
        }

        [HttpGet("departments")]
        //[Cache("departments", typeof(DepartmentDto))]
        public IActionResult GetDepartments()
        {
            using (var client = _redisManager.GetClient())
            {
                var cachedDepartments = client.Get<IEnumerable<DepartmentDto>>("departments");
                if (cachedDepartments != null)
                {
                    return Ok(cachedDepartments);
                }

                var result = _departmentService.GetDepartments();
                var departments = _mapper.Map<List<DepartmentDto>>(result);

                client.Set("departments", departments);

                return Ok(departments);
            }
        }

        [HttpPost("department")]
        [InvalidateCache("department")]
        public IActionResult PostDepartment([FromBody] DepartmentDto department)
        {
            var dept = _departmentService.AddDepartment(_mapper.Map<Department>(department));
            return Ok(_mapper.Map<DepartmentDto>(dept));
        }

        [HttpGet("students")]
        //[Authorize("user")]
        public IActionResult GetStudents()
        {
            var result = _mapper.Map<IEnumerable<StudentSummaryDto>>(_studentService.GetStudents());
            return Ok(result);
        }

        [HttpGet("student/{id}")]
        [Authorize("user")]
        public IActionResult GetStudentDetail(int id)
        {
            var result = _mapper.Map<StudentDetailDto>(_studentService.GetStudentById(id));
            return Ok(result);
        }

        [HttpPost("student")]
        [Authorize("user")]
        public IActionResult PostStudent([FromBody] StudentDetailDto student)
        {
            var _student = _mapper.Map<Student>(student);
            return Ok(_mapper.Map<StudentDetailDto>(_studentService.AddStudent(_student)));
        }

        [HttpDelete("student/{id}")]
        [Authorize("user")]
        public IActionResult DeleteStudent(int id)
        {
            _studentService.DeleteStudent(id);
            return Ok();
        }

        [HttpPut("student")]
        [Authorize("user")]
        public IActionResult UpdateStudent([FromBody] StudentDetailDto student)
        {
            // TODO
            var result = _studentService.UpdateStudent(_mapper.Map<Student>(student));
            return Ok(_mapper.Map<StudentDetailDto>(result));
        }

        [HttpPost("filterStudent")]
        [Authorize("user")]
        public IActionResult FilterStudent([FromBody] StudentFilterDto studentFilter)
        {
            // TODO
            var result = _mapper.Map<IEnumerable<StudentSummaryDto>>(_studentService.GetStudents());
            return Ok(result);
        }

        [HttpGet("randomResult")]
        public IActionResult RandomResult()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            var dice = rand.Next(1, 3);
            switch (dice)
            {
                case 1:
                    return NotFound();
                case 2:
                    return BadRequest();
                case 3:
                    return NoContent();
            }

            return NoContent();
        }
    }
}
