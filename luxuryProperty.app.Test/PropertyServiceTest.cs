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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace luxuryProperty.app.Test
{
    [TestClass]
    public class PropertyServiceTest
    {
        private MockRepository _mockRepository;
        private Mock<IRepositoryData<Property>> _repository;
        private Mock<IRepositoryData<PropertyImage>> _repositoryImage;
        private static IMapper _mapper;
        private static Mock<IUnitOfWork> _unitOfWork;

        #region Data

        public static readonly Property _property1 = new()
        {
            Id = "1",
            Name = "casa lago blue",
            Price = 1200,
            IdOwner = "1",
            CreationDate = DateTime.Now,
            CreationUser = GenericConstant.GENERIC_USER
        };


        public static readonly Property _property2 = new()
        {
            Id = "2",
            Name = "casa lago red",
            Price = 1200,
            IdOwner = "2",
            CreationDate = DateTime.Now,
            CreationUser = GenericConstant.GENERIC_USER
        };


        public static readonly List<Property> _listProperty = new()
        {
            _property1,
            _property2
        };


        public static readonly PropertyDto _propertyDto1 = new()
        {
            Name = "casa lago blue",
            Price = 1200,
            IdOwner = "1",
            CreationDate = DateTime.Now,
            CreationUser = GenericConstant.GENERIC_USER
        };

        public static readonly PropertyUpdateDto _propertyUpdateDto1 = new()
        {
            Name = "casa lago blue",
            Price = 1200,
            IdOwner = "1",
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
            _repository = _mockRepository.Create<IRepositoryData<Property>>();
            _unitOfWork.Setup(sp => sp.CreateRepository<Property>()).Returns(_repository.Object);

        }

        private PropertyService CreateService()
        {
            return new PropertyService(_repository.Object, _unitOfWork.Object, _mapper, _repositoryImage.Object);
        }


        [TestMethod]
        public async Task GetAllAsync_Return_Ok()
        {
            _repository.Setup(x => x.GetAllAsync(null, null, true, It.IsAny<int>(), It.IsAny<int>()));
            _repositoryImage.Setup(x => x.GetAllAsync(null, null, true, It.IsAny<int>(), It.IsAny<int>()));

            var service = CreateService();
            int page = 1;
            int limit = 100;
            string name = string.Empty;
            string address = string.Empty;
            int minPrice = 0;
            int maxPrice = 0;
            var result = await service.GetAllAsync(page, limit, name, address, minPrice, maxPrice);

            Equals(_listProperty, result);
            _repository.VerifyAll();
        }

        [TestMethod]
        public async Task Post_Return_Ok()
        {
            //Arrange
            var service = this.CreateService();
            string Id = "0";
            PropertyDto entity = _propertyDto1;
            _repository.Setup(x => x.InsertAsync(It.IsAny<Property>())).Verifiable();
            _unitOfWork.Setup(s => s.CommitTransactionAsync());

            // Act
            var result = await service.Post(_propertyDto1);

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
            PropertyUpdateDto entity = _propertyUpdateDto1;

            _repository.Setup(x => x.FirstOrDefaultAsync(
                                       It.IsAny<Expression<Func<Property, bool>>>())
                           ).ReturnsAsync(_property1);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Property>()));
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
                                      It.IsAny<Expression<Func<Property, bool>>>())
                          ).ReturnsAsync(_property2);

            _repository.Setup(x => x.DeleteAsync(It.IsAny<string>(), It.IsAny<bool>()));
            _unitOfWork.Setup(s => s.CommitTransactionAsync());

            var service = CreateService();
            string IdProperty = "2";

            // Act
            var result = await service.DeleteAsync(IdProperty);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(result);
            _repository.VerifyAll();
        }

    }


}
