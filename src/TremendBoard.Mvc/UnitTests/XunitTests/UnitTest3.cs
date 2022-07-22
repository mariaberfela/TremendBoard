using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Context;
using TremendBoard.Infrastructure.Services.Concrete;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;

namespace ProjectServiceTests.XunitTests
{
    public class UnitTest3
    {
        [Fact]
        public void GetProjectNameById_returns_correct_project_name()
        {
            var dbOption = new DbContextOptionsBuilder<TremendBoardDbContext>()
                .UseSqlServer("Server=localhost;Database=TremendBoard;Integrated Security=true;MultipleActiveResultSets=true")
                .Options;
            var context = new TremendBoardDbContext(dbOption);

            var projectRepo = new ProjectRepository(context);
            string result = projectRepo.GetProjectNameById("06f70952-2acd-4ed4-9f3d-3d03c182b89b");
            Assert.Equal("updated 4", result);

        }
    }
}
