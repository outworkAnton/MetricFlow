using AutoMapper;
using DataAccess.DI;
using NUnit.Framework;

namespace MetricFlow.Tests.DataAccess
{
    [TestFixture]
    public class DataAccessAutoMapperProfileTests
    {
        [Test]
        public void DataAccessAutoMapperProfile()
        {
            new MapperConfiguration(expression => expression.AddProfile(new DataAccessAutoMapperProfile()))
                .AssertConfigurationIsValid<DataAccessAutoMapperProfile>();
        }
    }
}