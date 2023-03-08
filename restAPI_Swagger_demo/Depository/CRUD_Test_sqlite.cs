using Microsoft.EntityFrameworkCore;
using restAPI_Swagger_demo.DBContext;
using restAPI_Swagger_demo.Models;
using SQLitePCL;

namespace restAPI_Swagger_demo.Depository
{
    public class CRUD_Test_sqlite : ICRUD_sqlite
    {
        readonly SQLiteContext _sqliteContext;

        public CRUD_Test_sqlite(SQLiteContext sqliteContext)
        {
            _sqliteContext = sqliteContext;
        }

        public async Task<bool> Check(int id)
        {
            return await _sqliteContext.tBooks.AsNoTracking().AnyAsync(o => o.Id == id);
        }

        public async Task<List<tBook>> Gets()
        {
            return await _sqliteContext.tBooks.AsNoTracking().ToListAsync();
        }

        public async Task<tBook>? Get(int id)
        {
            return await _sqliteContext.tBooks.AsNoTracking().SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<string>? Post(tBook book)
        {
            await _sqliteContext.tBooks.AddAsync(book);
            var isOK = await _sqliteContext.SaveChangesAsync();

            if (isOK > 0)
            {
                return  string.Empty;
            }
            else
            {
                return "新增失敗";
            }
        }

        public async Task<string>? Put(int id, tBook book)
        {
            var isFind = await Check(id);

            if (!isFind)
            {
                return "不存在";
            }

            _sqliteContext.tBooks.Attach(book);
            _sqliteContext.tBooks.Update(book);
            var isOK = await _sqliteContext.SaveChangesAsync();

            if (isOK > 0)
            {
                return string.Empty;
            }
            else
            {
                return "更新失敗";
            }
        }

        public async Task<string>? Patch(int id, tBook book)
        {
            var isFind = await Check(id);
            if (!isFind)
            {
                return "不存在";
            }

            _sqliteContext.tBooks.Attach(book);
            _sqliteContext.tBooks.Update(book);
            var isOK = await _sqliteContext.SaveChangesAsync();

            if (isOK > 0)
            {
                return string.Empty;
            }
            else
            {
                return "更新失敗";
            }
        }

        public async Task<string> Delete(int id)
        {
            var isFind = await Get(id);

            if (isFind == null)
            {
                return "不存在";
            }

            _sqliteContext.tBooks.Remove(isFind);
            var isOK = await _sqliteContext.SaveChangesAsync();

            if (isOK > 0)
            {
                return string.Empty;
            }
            else
            {
                return "刪除失敗";
            }
        }
    }
}
