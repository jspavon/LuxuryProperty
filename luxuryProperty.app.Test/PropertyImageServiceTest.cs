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
    public class PropertyImageServiceTest
    {
        private MockRepository _mockRepository;
        private Mock<IRepositoryData<PropertyImage>> _repository;
        private static IMapper _mapper;
        private static Mock<IUnitOfWork> _unitOfWork;

        #region Data

        public static readonly PropertyImage _propertyImage1 = new()
        {
            Id = "1",
            IdProperty = "1",
            File = "https://cdn.bhdw.net/im/minato-namikaze-naruto-shippuden-papel-pintado-55053_L.jpg",
            Enabled = true,
            CreationDate = DateTime.Now,
            CreationUser = GenericConstant.GENERIC_USER,
        };

        public static readonly PropertyImage _propertyImage2 = new()
        {
            Id = "2",
            IdProperty = "2",
            File = "https://www.cinepremiere.com.mx/wp-content/uploads/2021/05/Goku-articulo-900x491.jpg",
            Enabled = true,
            CreationDate = DateTime.Now,
            CreationUser = GenericConstant.GENERIC_USER
        };


        public static readonly List<PropertyImage> _listPropertyImage = new()
        {
            _propertyImage1,
            _propertyImage2
        };



        public static readonly PropertyImageDto _propertyImageDto1 = new()
        {
            IdProperty = "1",
            File = "https://cdn.bhdw.net/im/minato-namikaze-naruto-shippuden-papel-pintado-55053_L.jpg",
            Enabled = true,
            CreationDate = DateTime.Now,
            CreationUser = GenericConstant.GENERIC_USER
        };

        public static readonly PropertyImageUpdateDto _propertyImageUpdateDto1 = new()
        {
            Id = "2",
            IdProperty = "2",
            File = "https://www.cinepremiere.com.mx/wp-content/uploads/2021/05/Goku-articulo-900x491.jpg",
            Enabled = true,
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
            _repository = _mockRepository.Create<IRepositoryData<PropertyImage>>();
            _unitOfWork.Setup(sp => sp.CreateRepository<PropertyImage>()).Returns(_repository.Object);

        }

        private PropertyImageService CreateService()
        {
            return new PropertyImageService(_repository.Object, _unitOfWork.Object, _mapper);
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

            Equals(_listPropertyImage, result);
            _repository.VerifyAll();
        }

        [TestMethod]
        public async Task Post_Return_Ok()
        {
            //Arrange
            var service = this.CreateService();
            string Id = "0";
            PropertyImageDto entity = _propertyImageDto1;
            _repository.Setup(x => x.InsertAsync(It.IsAny<PropertyImage>())).Verifiable();
            _unitOfWork.Setup(s => s.CommitTransactionAsync());

            // Act
            var result = await service.Post(_propertyImageDto1);

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
            PropertyImageUpdateDto entity = _propertyImageUpdateDto1;

            _repository.Setup(x => x.FirstOrDefaultAsync(
                                       It.IsAny<Expression<Func<PropertyImage, bool>>>())
                           ).ReturnsAsync(_propertyImage1);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<PropertyImage>()));
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
                                      It.IsAny<Expression<Func<PropertyImage, bool>>>())
                          ).ReturnsAsync(_propertyImage2);

            _repository.Setup(x => x.DeleteAsync(It.IsAny<string>(), It.IsAny<bool>()));
            _unitOfWork.Setup(s => s.CommitTransactionAsync());

            var service = CreateService();
            string IdPropertyImage = "2";

            // Act
            var result = await service.DeleteAsync(IdPropertyImage);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(result);
            _repository.VerifyAll();
        }

    }
}
