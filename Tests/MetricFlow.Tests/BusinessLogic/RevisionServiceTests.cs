using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.Contract;
using BLI = BusinessLogic.Contract.Interfaces;
using BLM = BusinessLogic.Contract.Models;
using BusinessLogic.DI;
using DataAccess.Contract;
using DAI = DataAccess.Contract.Interfaces;
using DAM = DataAccess.Contract.Models;
using KellermanSoftware.CompareNetObjects;
using Moq;
using NUnit.Framework;

namespace MetricFlow.Tests.BusinessLogic
{
    [TestFixture]
    public class RevisionServiceTests
    {
        private IDatabaseRevisionRepository _repository;
        private IRevisionService _revisionService;
        private IMapper _mapper;

        private readonly BLI.IDatabaseRevision _expectedRevision =
            new BLM.DatabaseRevision("fc37fe97-c355-4f6e-80f7-3641787e0624", new DateTime(2018, 05, 22), 53216);

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
                          .Returns(Task.FromResult(new List<DAI.IDatabaseRevision>
                              {
                                  new DAM.DatabaseRevision()
                                  {
                                      Id = "62cff683-a485-4eab-8cba-0be75db507cf",
                                      Modified = new DateTime(2018, 12, 24),
                                      Size = 12345
                                  },
                                  new DAM.DatabaseRevision()
                                  {
                                      Id = "5b21ef66-d4d3-435c-835c-dac5e59b6dbf",
                                      Modified = new DateTime(2017, 03, 05),
                                      Size = 25874
                                  },
                                  new DAM.DatabaseRevision()
                                  {
                                      Id = "31d4f4bd-67ff-4f51-af70-59e3ce983a9c",
                                      Modified = new DateTime(2018, 11, 22),
                                      Size = 54321
                                  },
                                  new DAM.DatabaseRevision()
                                  {
                                      Id = "fc37fe97-c355-4f6e-80f7-3641787e0624",
                                      Modified = new DateTime(2018, 05, 22),
                                      Size = 53216
                                  }
                              }.AsEnumerable()));
            _repository = repositoryMock.Object;
            _revisionService = new Mock<IRevisionService>().Object;
        }

        [Test]
        public void GetRevisionById_HasValidId_Success()
        {
            var revisionService = new RevisionService(_repository, _mapper);
            var actualRevision = revisionService.GetRevisionById("fc37fe97-c355-4f6e-80f7-3641787e0624");
            Comparer.Compare(_expectedRevision, actualRevision);
        }
    }
}