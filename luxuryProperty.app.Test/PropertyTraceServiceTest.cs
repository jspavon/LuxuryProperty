using AutoMapper;
using luxuryProperty.app.application.Mapper;
using luxuryProperty.app.applicationCore.Dtos;
using luxuryProperty.app.applicationCore.Services;
using luxuryProperty.app.commons.Constants;
using luxuryProperty.app.infraestructure.Entities;
using luxuryProperty.app.infraestructure.Repository;
using luxuryProperty.app.infraestructure.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace luxuryProperty.app.Test
{
    [TestClass]
    public class PropertyTraceServiceTest
    {
        private MockRepository _mockRepository;
        private Mock<IRepositoryData<PropertyTrace>> _repository;
        private static IMapper _mapper;
        private static Mock<IUnitOfWork> _unitOfWork;

        #region Data

        public static readonly PropertyTrace _propertyTrace1 = new()
        {
            Id = "1",
            DateSale = DateTime.Now.AddDays(-5),
            Name = "Pepito",
            Value = 6500,
            Tax = 09,
            IdProperty = 1,
            CreationDate = DateTime.Now,
            CreationUser = GenericConstant.GENERIC_USER,
        };

        public static readonly PropertyTrace _propertyTrace2 = new()
        {
            Id = "2",
            DateSale = DateTime.Now.AddDays(-10),
            Name = "Ramita",
            Value = 5500,
            Tax = 05,
            IdProperty = 2,
            CreationDate = DateTime.Now,
            CreationUser = GenericConstant.GENERIC_USER
        };


        public static readonly List<PropertyTrace> _listPropertyTrace = new()
        {
            _propertyTrace1,
            _propertyTrace2
        };



        public static readonly PropertyTraceDto _propertyTraceDto1 = new()
        {
            DateSale = DateTime.Now.AddDays(-3),
            Name = "Pepito",
            Value = 6500,
            Tax = 05,
            IdProperty = "1",
            CreationDate = DateTime.Now,
            CreationUser = GenericConstant.GENERIC_USER
        };

        public static readonly PropertyTraceUpdateDto _propertyTraceUpdateDto1 = new()
        {
            Id = "1",
            DateSale = DateTime.Now,
            Name = "Ramita",
            Value = 6500,
            Tax = 05,
            IdProperty = "1",
            CreationDate = DateTime.Now.AddDays(-1),
            CreationUser = GenericConstant.GENERIC_USER,
            ModificationDate = DateTime.Now,
            ModificationUser = GenericConstant.GENERIC_USER,
            Deleted = false
        };



        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapping());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _unitOfWork = new Mock<IUnitOfWork>();
            _repository = _mockRepository.Create<IRepositoryData<PropertyTrace>>();
            _unitOfWork.Setup(sp => sp.CreateRepository<PropertyTrace>()).Returns(_repository.Object);

        }

        private PropertyTraceService CreateService()
        {
            return new PropertyTraceService(_repository.Object, _unitOfWork.Object, _mapper);
        }


        [TestMethod]
        public async Task GetAllAsync_Return_Ok()
        {
            _repository.Setup(x => x.GetAllAsync(null, null, true, It.IsAny<int>(), It.IsAny<int>()));

            var service = CreateService();
            int page = 1;
            int limit = 100;
            string orderBy = "Id";
            bool ascending = true;
            var result = await service.GetAllAsync(page, limit, orderBy, ascending);

            Equals(_listPropertyTrace, result);
            _repository.VerifyAll();
        }

        [TestMethod]
        public async Task Post_Return_Ok()
        {
            //Arrange
            var service = this.CreateService();
            string Id = "0";
            PropertyTraceDto entity = _propertyTraceDto1;
            _repository.Setup(x => x.InsertAsync(It.IsAny<PropertyTrace>())).Verifiable();
            _unitOfWork.Setup(s => s.CommitTransactionAsync());

            // Act
            var result = await service.Post(_propertyTraceDto1);

            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Id, result.id);
            _repository.VerifyAll();
        }


        [TestMethod]
        public async Task PutAsync_ReturnsFalse_WhenIdExistsAndUpdateData()
        {
            // Arrange
            var service = CreateService();

            string Id = "1";
            PropertyTraceUpdateDto entity = _propertyTraceUpdateDto1;

            _repository.Setup(x => x.FirstOrDefaultAsync(
                                       It.IsAny<Expression<Func<PropertyTrace, bool>>>())
                           ).ReturnsAsync(_propertyTrace1);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<PropertyTrace>()));
            _unitOfWork.Setup(s => s.CommitTransactionAsync());

            // Act
            var result = await service.PutAsync(Id, entity);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(result);
            _repository.VerifyAll();
        }


        [TestMethod]
        public async Task DeleteAsync_ReturnsFalse_WhenIdExistsAndDeleteData()
        {
            // Arrange
            _repository.Setup(x => x.FirstOrDefaultAsync(
                                      It.IsAny<Expression<Func<PropertyTrace, bool>>>())
                          ).ReturnsAsync(_propertyTrace2);

            _repository.Setup(x => x.DeleteAsync(It.IsAny<string>(), It.IsAny<bool>()));
            _unitOfWork.Setup(s => s.CommitTransactionAsync());

            var service = CreateService();
            string IdPropertyTrace = "2";

            // Act
            var result = await service.DeleteAsync(IdPropertyTrace);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(result);
            _repository.VerifyAll();
        }

    }

}
