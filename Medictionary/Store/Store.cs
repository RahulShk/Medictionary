using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Medictionary.Models;
using Medictionary.Store.Interface;
using Medictionary.Services.Interfaces;
using Medictionary.Data;

namespace Medictionary.Store
{
    public class Store<TDocument> : IStore<TDocument>
        where TDocument : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TDocument> _dbSet;
        private readonly IFileService _fileService;

        public Store(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _dbSet = _context.Set<TDocument>();
            _fileService = fileService;
        }

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _dbSet.Where(filterExpression).AsEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _dbSet.Where(filterExpression).Select(projectionExpression).AsEnumerable();
        }

        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _dbSet.FirstOrDefault(filterExpression);
        }

        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _dbSet.FirstOrDefaultAsync(filterExpression);
        }

        public virtual TDocument FindById(string id)
        {
            return _dbSet.Find(id);
        }

        public virtual Task<TDocument> FindByIdAsync(string id)
        {
            return _dbSet.FindAsync(id).AsTask();
        }

        public virtual void InsertOne(TDocument document)
        {
            _dbSet.Add(document);
            _context.SaveChanges();
        }

        public virtual async Task InsertOneAsync(TDocument document)
        {
            await _dbSet.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public void InsertMany(ICollection<TDocument> documents)
        {
            _dbSet.AddRange(documents);
            _context.SaveChanges();
        }

        public async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await _dbSet.AddRangeAsync(documents);
            await _context.SaveChangesAsync();
        }

        public void ReplaceOne(TDocument document)
        {
            _dbSet.Update(document);
            _context.SaveChanges();
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            _dbSet.Update(document);
            await _context.SaveChangesAsync();
        }

        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            var document = _dbSet.FirstOrDefault(filterExpression);
            if (document != null)
            {
                _dbSet.Remove(document);
                _context.SaveChanges();
            }
        }

        public async Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            var document = await _dbSet.FirstOrDefaultAsync(filterExpression);
            if (document != null)
            {
                _dbSet.Remove(document);
                await _context.SaveChangesAsync();
            }
        }

        public void DeleteById(string id)
        {
            var document = _dbSet.Find(id);
            if (document != null)
            {
                _dbSet.Remove(document);
                _context.SaveChanges();
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            var document = await _dbSet.FindAsync(id);
            if (document != null)
            {
                _dbSet.Remove(document);
                await _context.SaveChangesAsync();
            }
        }

        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            var documents = _dbSet.Where(filterExpression).ToList();
            _dbSet.RemoveRange(documents);
            _context.SaveChanges();
        }

        public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            var documents = await _dbSet.Where(filterExpression).ToListAsync();
            _dbSet.RemoveRange(documents);
            await _context.SaveChangesAsync();
        }
    }
}
