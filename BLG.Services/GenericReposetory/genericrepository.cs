using System.Linq.Expressions;
using BLG.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BLG.Services.GenericReposetory;

public class genericrepository<TEntity> where TEntity :class
{
    private readonly ApplicationContext _context;
    private DbSet<TEntity> table;
    public genericrepository(ApplicationContext context)
    {
        _context = context;
        table = context.Set<TEntity>();
    }

    public virtual void insert(TEntity entity)
    {
        table.Add(entity);

    }

    public virtual void update(TEntity entity)
    {
        table.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual TEntity Getbyid(object id)
    {

        return table.Find(id);
    }

    public virtual IEnumerable<TEntity> get(Expression<Func<TEntity, bool>>? wherevaribale = null, string join = "")
    {
        IQueryable<TEntity> query = table;
        if (wherevaribale != null)
        {
            query = query.Where(wherevaribale);
        }

        if (join != "")
        {
            foreach (string item in join.Split(','))
            {
                query = query.Include(item);
            }
        }
          
        return query;
    }
    public virtual void Delete(TEntity entity)
    {

        if (_context.Entry(entity).State == EntityState.Detached)
        {
            table.Attach(entity);
        }

        table.Remove(entity);
    }

    public virtual void deletebyid(object id)
    {

        var resulet = Getbyid(id);
        Delete(resulet);
    }

    public virtual void DeleteRanges(Expression<Func<TEntity, bool>>? wherevariable = null)
    {
        IQueryable<TEntity> qury = table;

        if (wherevariable != null)
        {
            qury = qury.Where(wherevariable);
        }

        table.RemoveRange(qury);
    }
}