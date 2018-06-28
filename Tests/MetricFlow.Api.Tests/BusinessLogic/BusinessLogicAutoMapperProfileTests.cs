using AutoMapper;
using BusinessLogic.DI;
using NUnit.Framework;

namespace MetricFlow.Api.Tests.BusinessLogic
{
    [TestFixture]
    public class BusinessLogicAutoMapperProfileTests
    {
        [Test]
        public void BusinessLogicAutoMapperProfile()
        {
            new MapperConfiguration(expression => expression.AddProfile(new BusinessLogicAutoMapperProfile()))
                .AssertConfigurationIsValid<BusinessLogicAutoMapperProfile>();
        }
    }
}