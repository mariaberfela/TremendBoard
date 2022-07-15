using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;

namespace UnitTesting
{
    public class UnitTest2
    {
        [Fact]
        public void FireAndForgetJob_Message_True()
        {
            IDateTime dateTime;
            JobTestService jobTestService = new JobTestService();
            var message = "Hello from a Fire and Forget job!";
            var stringWriter = new StringWriter();
            

            string stringWriter2;
            Console.SetOut(stringWriter);
            jobTestService.FireAndForgetJob();
            int index = stringWriter.ToString().LastIndexOf("!");
            if (index > 0)
            {
                stringWriter2 = stringWriter.ToString();
                stringWriter2 = stringWriter2.Substring(0, index+1);
                Xunit.Assert.Equal(message, stringWriter2);
            }
            else
            Xunit.Assert.Equal(message, stringWriter.ToString());
            stringWriter.Dispose();

        }
        [Fact]
        public void GetLowerCase_String_True()
        {
            
            var jobTestService = new Mock<IJobTestService>();
            jobTestService.Setup(x => x.UnitTestingJob("ana are mere")).Returns("ANA ARE MERE");
            var lowerCaseService = new LowerCaseService(jobTestService.Object);
            string result = lowerCaseService.GetLoweCase();

            //Assert.Equal("ana are mere",lowerCaseService.GetLoweCase());
            bool hasUpperCase = result.Equals(result.ToLower());
            Assert.True(hasUpperCase);
            
        }
        
    }
}
