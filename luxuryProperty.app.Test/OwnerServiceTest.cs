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
    public class OwnerServiceTest
    {
        private MockRepository _mockRepository;
        private Mock<IRepositoryData<Owner>> _repository;
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


        public static readonly Owner _owner1 = new()
        {
            Id = "1",
            Name = "Pepito",
            Address = "Av siempre viva",
            CreationDate = DateTime.Now,
            CreationUser = GenericConstant.GENERIC_USER,
        };

        public static readonly Owner _owner2 = new()
        {
            Id = "2",
            Name = "Ramita",
            Address = "Av siempre viva",
            CreationDate = DateTime.Now,
            CreationUser = GenericConstant.GENERIC_USER
        };


        public static readonly List<Owner> _listOwner = new()
        {
            _owner1,
            _owner2
        };



        public static readonly OwnerDto _ownerDto1 = new()
        {
            Name = "Pepito",
            Address = "Av siempre viva",
            CreationDate = DateTime.Now,
            CreationUser = GenericConstant.GENERIC_USER
        };

        public static readonly OwnerUpdateDto _ownerUpdateDto1 = new()
        {
            Id = "1",
            Name = "Ramita",
            Address = "Av siempre Querer",
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
            _repository = _mockRepository.Create<IRepositoryData<Owner>>();
            _unitOfWork.Setup(sp => sp.CreateRepository<Owner>()).Returns(_repository.Object);

        }

        private OwnerService CreateService()
        {
            return new OwnerService(_repository.Object, _unitOfWork.Object, _mapper);
        }

        [TestMethod]
        public async Task GetAllAsync_Return_Ok()
        {
            _repository.Setup(x => x.GetAllAsync(null, null, true, It.IsAny<int>(), It.IsAny<int>()));

            var service = CreateService();
            int page = 1;
            int limit = 100;
            string orderBy = "Name";
            bool ascending = true;
            var result = await service.GetAllAsync(page, limit, orderBy, ascending);

            Equals(_listOwner, result);
            _repository.VerifyAll();
        }

        [TestMethod]
        public async Task Post_Return_Ok()
        {
            //Arrange
            var service = this.CreateService();
            string Id = "0";
            OwnerDto entity = _ownerDto1;
            _repository.Setup(x => x.InsertAsync(It.IsAny<Owner>())).Verifiable();
            _unitOfWork.Setup(s => s.CommitTransactionAsync());

            // Act
            var result =  await service.Post(_ownerDto1);

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
            OwnerUpdateDto entity = _ownerUpdateDto1;

            _repository.Setup(x => x.FirstOrDefaultAsync(
                                       It.IsAny<Expression<Func<Owner, bool>>>())
                           ).ReturnsAsync(_owner1);

            _repository.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Owner>()));
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
                                      It.IsAny<Expression<Func<Owner, bool>>>())
                          ).ReturnsAsync(_owner2);

            _repository.Setup(x => x.DeleteAsync(It.IsAny<string>(), It.IsAny<bool>()));
            _unitOfWork.Setup(s => s.CommitTransactionAsync());

            var service = CreateService();
            string IdOwner = "2";

            // Act
            var result = await service.DeleteAsync(IdOwner);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(result);
            _repository.VerifyAll();
        }


    }
}
