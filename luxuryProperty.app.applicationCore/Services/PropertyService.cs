// ***********************************************************************
// Assembly         : luxuryProperty.app
// Author           : Jhon Steven Pavon Bedoya 
// Created          : 26-01-2025
//
// Last Modified By : Jhon Steven Pavon Bedoya
// Last Modified On : 26-01-2025
// ***********************************************************************
// <copyright file="ResponseException.cs" company="luxuryProperty.app">
//     Copyright (c) Independiente. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using AutoMapper;
using luxuryProperty.app.applicationCore.Dtos;
using luxuryProperty.app.applicationCore.Interfaces;
using luxuryProperty.app.infraestructure.Entities;
using luxuryProperty.app.infraestructure.Repository;
using luxuryProperty.app.infraestructure.UnitOfWork;
using MongoDB.Bson;
using MongoDB.Driver;

namespace luxuryProperty.app.applicationCore.Services
{
    /// <summary>
    /// Class PropertyService.
    /// Implements the <see cref="luxuryProperty.app.applicationCore.Services" />
    /// </summary>
    /// <seealso cref="luxuryProperty.app.applicationCore.Services" />
    public class PropertyService : IPropertyService
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepositoryData<Property> _repository;

        private readonly IRepositoryData<PropertyImage> _repositoryImage;
        /// <summary>
        /// the IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="System.ArgumentNullException">mapper</exception>
        public PropertyService(IRepositoryData<Property> repository, IUnitOfWork unitOfWork, IMapper mapper, IRepositoryData<PropertyImage> repositoryImage)
        {
            _repository = unitOfWork.CreateRepository<Property>();
            _repositoryImage = unitOfWork.CreateRepository<PropertyImage>();
            _unitOfWork = unitOfWork;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// delete as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        public async Task<bool> DeleteAsync(string id)
        {
            var existingEntity = _repository.FirstOrDefaultAsync(x => x.Id == id).Result;
            if (existingEntity == null) return false;

            var result = await _repository.DeleteAsync(id, true);            
            await _unitOfWork.CommitTransactionAsync();
            return result;
        }

        /// <summary>
        /// get all as an asynchronous operation.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>Task&lt;IEnumerable&lt;PropertyDto&gt;&gt;.</returns>
        public async Task<List<PropertyUpdateDto>> GetAllAsync(int page, int limit, string name, string address, int minPrice, int maxPrice)
        {
            
            var filterBuilder = new FilterBuilder<Property>()
                                .WithRegex(nameof(Property.Name), name ?? string.Empty)
                                .WithRegex(nameof(Property.Address), address ?? string.Empty)
                                .WithRange<int>(nameof(Property.Price), minPrice > 0 ? minPrice : 0 , maxPrice > 0 ? maxPrice : 9999999)
                                .Build();

            var result = await _repository.GetAllAsync(
                filter: filterBuilder,
                ascending: true,
                page: page,
                pageSize: limit);

            var resultImages = await _repositoryImage.GetAllAsync();
                
            var mapper = _mapper.Map<List<PropertyUpdateDto>>(result.Data.ToList());


            mapper.ForEach(x =>
            {
                var images = resultImages.Data.Where(y => y.IdProperty == x.Id).ToList();
                var mapperImages = _mapper.Map<List<PropertyImageDto>>(images);
                x.PropertyImages.AddRange(mapperImages);
            });
            return mapper;
        }

        /// <summary>
        /// get by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;PropertyDto&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        public async Task<PropertyUpdateDto> GetByIdAsync(string id)
        {
            var result = await _repository.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
            var mapper = _mapper.Map<PropertyUpdateDto>(result);
            return mapper;
        }

        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>System.ValueTuple&lt;System.Boolean, System.Int32&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        public async Task<(bool status, string id)> Post(PropertyDto entity)
        {
            var obj = _mapper.Map<Property>(entity);
            var result = await _repository.InsertAsync(obj);
            await _unitOfWork.CommitTransactionAsync();
            return (result, obj.Id);
        }

        /// <summary>
        /// put as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        public async Task<bool> PutAsync(string id, PropertyUpdateDto entity)
        {
            var existingEntity = _repository.FirstOrDefaultAsync(x => x.Id == id).Result;
            if (existingEntity == null) return false;

            var obj = _mapper.Map(entity, existingEntity);
            var result = await _repository.UpdateAsync(id, obj);
            await _unitOfWork.CommitTransactionAsync();
            return result; 
        }
    }
}
