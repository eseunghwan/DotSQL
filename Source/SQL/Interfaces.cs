
using System;


namespace DotSQL.SQL {
    internal interface IExecutor {
        Core.Result Execute(String query);

        System.Data.Common.DbConnection RawConnection();
    }
}
