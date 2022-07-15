using Moq;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;
using TremendBoard.Mvc;

namespace UnitTesting
{
    public class UnitTest1
    {
        [Fact]
        //[Theory]
        //[InlineData("Nume")]
        public void Test1()
        {
            SystemDateTime systemDateTime = new SystemDateTime();
            var x = systemDateTime.Now.Hour;
            Xunit.Assert.Equal(DateTime.Now.Hour, x);
            
        }
        //public void Test2()
        //{
        //    JobTestService jobTestService = new JobTestService();
        //    var x = "Hello from a Fire and Forget job!";
        //    jobTestService.FireAndForgetJob();
        //    var y = Console.ReadLine();
        //    Xunit.Assert.Equal(x, y);
        //}
        [Theory]
        [InlineData(2,"no")]
        [InlineData(4, "yes")]
        [InlineData(3, "no")]
        public void Test10(int number,string expected = "no")
        {
            Number value = new Number(number);
            string actual = value.IsEven();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test100()
        {
            var transientTimeServiceStub = new Mock<ITransientTimeService>();

            transientTimeServiceStub.Setup(x => x.GetCurrentTime()).Returns(DateTime.Now);
            var result = new TransientTimeService().GetCurrentTime();
            Assert.Equal(DateTime.Now.Hour, result.Hour);
        }
        
    }
}