﻿using IDP.Domain.IRepository.Base;
using IDP.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace IDP.Infra.Repository.Command.Base;

public class CommandRepository<T> : IBaseCommonRepository<T> where T : class
{
    protected readonly ShopCommandDbContext _context;

    public CommandRepository(ShopCommandDbContext context)
    {
        _context = context;
    }
    public async Task<T> Insert(T entity)
    {

        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Update(T entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;    
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<bool> Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }


}
