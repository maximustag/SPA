﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using GenericLibsBase;
using GenericServices;
using GenericServices.Core;
using GenericServices.Services;
using GenericServices.ServicesAsync;

namespace Spa.Data.Infrastructure
{
    public class SpaRepository<TEntity, TDto, TDtoAsync> : ISpaRepository<TEntity, TDto, TDtoAsync>
        where TEntity : class, new()
        where TDto : EfGenericDto<TEntity, TDto>, new()
        where TDtoAsync : EfGenericDtoAsync<TEntity, TDtoAsync>, new()
    {
        private readonly IGenericServicesDbContext _db;

        public SpaRepository(IGenericServicesDbContext context)
        {
            _db = context;
        }

        #region Old methods
        public bool EntityExists(int key)
        {
            return _db.Set<TEntity>().Find(key) != null;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>();
        }

        public SingleResult<TEntity> Get(Func<TEntity, bool> predicate)
        {
            var entity = _db.Set<TEntity>().Where(predicate).AsQueryable();

            return SingleResult.Create(entity);
        }

        public async Task<TEntity> GetAsync(int key)
        {
            return await _db.Set<TEntity>().FindAsync(key);
        }

        public async Task<int> PostAsync(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> PatchAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task<int> PutAsync(TEntity update)
        {
            //_db.Entry(update).State = EntityState.Modified;
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            return await _db.SaveChangesAsync();
        }
        #endregion

        #region Service fields for property injections with <TEntity> generic
        public IListService<TEntity> ListService { get; set; }
        public IDetailServiceAsync<TEntity> DetailServiceAsync { get; set; }
        public IDetailService<TEntity> DetailService { get; set; }
        public ICreateServiceAsync<TEntity> CreateServiceAsync { get; set; }
        public IUpdateServiceAsync<TEntity> UpdateServiceAsync { get; set; }
        #endregion

        #region Service fields for property injections with <TEntity, TDto> generics
        public IListService<TEntity, TDto> ListServiceDto { get; set; }
        public IDetailServiceAsync<TEntity, TDtoAsync> DetailServiceDtoAsync { get; set; }
        public IDetailService<TEntity, TDto> DetailServiceDto { get; set; }
        public ICreateServiceAsync<TEntity, TDtoAsync> CreateServiceDtoAsync { get; set; }
        public IUpdateServiceAsync<TEntity, TDtoAsync> UpdateServiceDtosync { get; set; }
        #endregion

        #region CRUD methods with <TEntity> generic
        public IQueryable<TEntity> GetAll2()
        {
            return ListService.GetAll();
        }
        public async Task<ISuccessOrErrors<TEntity>> GetAsync2(int key)
        {
            return await DetailServiceAsync.GetDetailAsync(key);
        }
        public ISuccessOrErrors<TEntity> Get2(int key)
        {
            return DetailService.GetDetail(key);
        }

        public async Task<ISuccessOrErrors> PostAsync2(TEntity entity)
        {
            return await CreateServiceAsync.CreateAsync(entity);
        }

        public async Task<ISuccessOrErrors> PatchAsync2(TEntity entity)
        {
            return await UpdateServiceAsync.UpdateAsync(entity);
        }
        #endregion

        #region CRUD methods with <TDto, TDtoAsync> generic
        public IQueryable<TDto> GetAllDto()
        {
            return ListServiceDto.GetAll();
        }
        public async Task<ISuccessOrErrors<TDtoAsync>> GetDtoAsync(int key)
        {
            return await DetailServiceDtoAsync.GetDetailAsync();
        }
        public ISuccessOrErrors<TDto> GetDto(int key)
        {
            return DetailServiceDto.GetDetail(key);
        }

        public async Task<ISuccessOrErrors> PostDtoAsync(TDtoAsync dto)
        {
            return await CreateServiceDtoAsync.CreateAsync(dto);
        }

        public async Task<ISuccessOrErrors> PatchDtoAsync(TDtoAsync dto)
        {
            return await UpdateServiceDtosync.UpdateAsync(dto);
        }
        #endregion
    }
}