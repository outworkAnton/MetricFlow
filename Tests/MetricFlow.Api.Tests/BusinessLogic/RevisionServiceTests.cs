using System;
using System.Linq;

using AutoMapper;

using BusinessLogic;
using BusinessLogic.DI;

using DataAccess.Contract.Repositories;

using KellermanSoftware.CompareNetObjects;

using Moq;

using NUnit.Framework;
using BLI = BusinessLogic.Contract.Interfaces;
using BLM = BusinessLogic.Contract.Models;
using DACM = DataAccess.Contract.Models;

namespace MetricFlow.Api.Tests.BusinessLogic
{
    [TestFixture]
    public class RevisionServiceTests
    {
        #region Init

        private IDatabaseRevisionRepository _repository;
        private IMapper _mapper;

        private readonly BLI.IDatabaseRevision _expectedRevision =
            new BLM.DatabaseRevision("fc37fe97-c355-4f6e-80f7-3641787e0624", new DateTime(2018, 05, 22), 53216, 1);

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
            var repositoryMock = new Mock<IDatabaseRevisionRepository>();
            repositoryMock.Setup(repository => repository.Get())
                .ReturnsAsync(new []
                {
                    new DACM.DatabaseRevision()
                        {
                            Id = "62cff683-a485-4eab-8cba-0be75db507cf",
                                Modified = new DateTime(2018, 12, 24),
                                Size = 12345,
                                Changed = 0
                        },
                        new DACM.DatabaseRevision()
                        {
                            Id = "5b21ef66-d4d3-435c-835c-dac5e59b6dbf",
                                Modified = new DateTime(2017, 03, 05),
                                Size = 25874,
                                Changed = 1
                        },
                        new DACM.DatabaseRevision()
                        {
                            Id = "31d4f4bd-67ff-4f51-af70-59e3ce983a9c",
                                Modified = new DateTime(2018, 11, 22),
                                Size = 54321,
                                Changed = 0
                        },
                        new DACM.DatabaseRevision()
                        {
                            Id = "fc37fe97-c355-4f6e-80f7-3641787e0624",
                                Modified = new DateTime(2018, 05, 22),
                                Size = 53216,
                                Changed = 1
                        }
                });
            _repository = repositoryMock.Object;
        }

        #endregion

        [Test]
        public void GetRevisionById_HasValidId_Success()
        {
            var revisionService = new RevisionService(_repository, _mapper);
            var actualRevision = revisionService.GetRevisionById("fc37fe97-c355-4f6e-80f7-3641787e0624");
            Comparer.Compare(_expectedRevision, actualRevision);
        }
    }
}