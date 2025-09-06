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


namespace luxuryProperty.app.applicationCore.Services
{
    /// <summary>
    /// Class OwnerService.
    /// Implements the <see cref="luxuryProperty.app.applicationCore.Services" />
    /// </summary>
    /// <seealso cref="luxuryProperty.app.applicationCore.Services" />
    public class OwnerService : IOwnerService
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepositoryData<Owner> _repository;
        /// <summary>
        /// the IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="System.ArgumentNullException">mapper</exception>
        public OwnerService(IRepositoryData<Owner> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.CreateRepository<Owner>();
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

            var result = await _repository.DeleteAsync(id);            
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
        /// <returns>Task&lt;IEnumerable&lt;OwnerDto&gt;&gt;.</returns>
        public async Task<List<OwnerDto>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true)
        {
            var filterBuilder = new FilterBuilder<Owner>().Build();

            var result = await _repository.GetAllAsync(
                filter: filterBuilder,
                orderBy: x => x.Name,
                ascending: ascending,
                page: page,
                pageSize: limit);

            var mapper = _mapper.Map<List<OwnerDto>>(result.Data.ToList());

            return mapper;
        }

        /// <summary>
        /// get by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;OwnerDto&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        public async Task<OwnerDto> GetByIdAsync(string id)
        {
            var result = await _repository.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
            var mapper = _mapper.Map<OwnerDto>(result);
            return mapper;
        }

        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>System.ValueTuple&lt;System.Boolean, System.Int32&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        public async Task<(bool status, string id)> Post(OwnerDto entity)
        {
            var obj = _mapper.Map<Owner>(entity);
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
        public async Task<bool> PutAsync(string id, OwnerUpdateDto entity)
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
