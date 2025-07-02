using score_management_be.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;

namespace score_management_be.Repositories
{
    public interface IBaseRepository<T>
    {
        // có find all và find by condition rồi thì không cần Getallsync với getByIdAsync nữa
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition); // Điều kiện lọc dữ liệu dưới dạng lambda expression
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(Guid id);
    }

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //AsNoTracking for better performance if no updates are needed
        public virtual IQueryable<T> FindAll()
        // Trả về IQueryable<T> giúp LINQ có thể tiếp tục xử lý dữ liệu như Where(), OrderBy(), v.v.
        // IQueryable<T> là một interface trong LINQ giúp xây dựng truy vấn trước khi thực thi trên database.
        {
            //return _context.Set<T>().AsNoTracking();
            return _context.Set<T>().AsQueryable();
            // to get the DbSet<T> for the entity type T
            // AsQueryable() chuyển IEnumerable<T> thành IQueryable<T> nếu cần.
            // Nếu ta dùng ToList() trước rồi mới AsQueryable(), thì LINQ sẽ thực thi trên bộ nhớ (in-memory), thay vì trên SQL Server.
            // Khi bạn có một IEnumerable<T> (đang duyệt trên bộ nhớ) và muốn chuyển nó thành IQueryable<T> để tiếp tục truy vấn trên database.
            // Lúc này, Where() sẽ lọc trên bộ nhớ(in -memory), chứ không phải trên database.
        }

        //Get records based on a condition
        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition)
        => _context.Set<T>().Where(condition).AsNoTracking();
        // Where(condition): Lọc dữ liệu theo điều kiện truyền vào.
        // AsNoTracking() giúp tăng tốc nếu không cần theo dõi thay đổi của dữ liệu.



        public virtual async Task<T> CreateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException dbEx)
            {
                // Most common cause of SaveChanges() failure
                throw new Exception("Database update error: " + dbEx.InnerException?.Message, dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("General error during CreateAsync: " + ex.Message, ex);
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            // Đánh dấu entity là đã thay đổi và cập nhật vào database.
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            // Không chọn lọc cột, nếu muốn cập nhật một số trường cụ thể, nên dùng:
            //_context.Entry(entity).Property(e => e.Name).IsModified = true;
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id); // Tìm bản ghi theo id
            // không tìm thấy, ném lỗi KeyNotFoundException
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
            _context.Set<T>().Remove(entity); // Xóa bản ghi
            await _context.SaveChangesAsync();
            //Nếu cần xóa mềm(soft delete), có thể thêm cột IsDeleted thay vì xóa hẳn bản ghi.
        }
    }
}
