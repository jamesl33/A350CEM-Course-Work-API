using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Xunit;
using A350CEM_Course_Work.Models;
using A350CEM_Course_Work.Controllers;

namespace A350CEM_Course_Work_API.Tests
{
    public class EmployeeControllerTest
    {
        private Context _context;

        public EmployeeControllerTest()
        {
            var builder = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase("database");
            var context = new Context(builder.Options);
            _context = context;
        }

        [Fact]
        public async void TestGetNonexistentEmployee()
        {
            var controller = new EmployeeController(_context);
            ActionResult<Employee> result = await controller.GetEmployeeByEmployeeNumber(0);
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
