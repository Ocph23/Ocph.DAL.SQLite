using Ocph.DAL.Provider.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ocph.DAL.DbContext
{
   public class DataContext
    {
        public static IDataTable<T> GetDatatable<T>(IDbConnection connection) where T : class
        {
            IDataTable<T> c = null;
            c = new SQLiteDbContext<T>(connection);
            return c;
        }

    }
}
