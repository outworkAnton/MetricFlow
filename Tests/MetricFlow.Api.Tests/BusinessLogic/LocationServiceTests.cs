using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using BusinessLogic;
using BusinessLogic.DI;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess.Contract.Repositories;

using KellermanSoftware.CompareNetObjects;

using Moq;

using NUnit.Framework;
using DACM = DataAccess.Contract.Models;

namespace MetricFlow.Api.Tests.BusinessLogic
{
    [TestFixture]
    public class LocationServiceTests
    {
        #region Init

        private ILocationRepository _repository;
        private IMapper _mapper;

        private readonly Location _expectedLocation =
            new Location("fc37fe97-c355-4f6e-80f7-3641787e0624", "Test location", 1);

        private static readonly CompareLogic Comparer = new CompareLogic(new ComparisonConfig()
        {
            ShowBreadcrumb = true,
                DifferenceCallback = x =>
                throw new Exception(
                        $"Property \"{x.PropertyName}\" is different: object 1 value is \"{x.Object1Value}\", object 2 value is \"{x.Object2Value}\""),
                    SkipInvalidIndexers = true,
                    DoublePrecision = 4,
        });

        [OneTimeSetUp]
        public void Init()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new BusinessLogicAutoMapperProfile()));
            _mapper = config.CreateMapper();
            var repositoryMock = new Mock<ILocationRepository>();
            repositoryMock.Setup(repository => repository.Get())
                .ReturnsAsync(new []
                {
                    new DACM.Location()
                        {
                            Id = "62cff683-a485-4eab-8cba-0be75db507cf",
                            Name = "Home",
                            Active = 0
                        },
                        new DACM.Location()
                        {
                            Id = "5b21ef66-d4d3-435c-835c-dac5e59b6dbf",
                            Name = "Work",
                            Active = 0
                        },
                        new DACM.Location()
                        {
                            Id = "31d4f4bd-67ff-4f51-af70-59e3ce983a9c",
                            Name = "Apartment",
                            Active = 0
                        },
                        new DACM.Location()
                        {
                            Id = "fc37fe97-c355-4f6e-80f7-3641787e0624",
                            Name = "Test location",
                            Active = 1
                        }
                });
            _repository = repositoryMock.Object;
        }

        #endregion

        [Test]
        public async Task GetLocationById_HasValidId_Success()
        {
            var locationService = new LocationService(_repository, _mapper);
            var actualLocation = await locationService.GetItemById("fc37fe97-c355-4f6e-80f7-3641787e0624").ConfigureAwait(false);
            Comparer.Compare(_expectedLocation, actualLocation);
        }
    }
}