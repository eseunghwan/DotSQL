
using System;
using System.Threading.Tasks;


namespace DotSQL.Executors {
    namespace Interfaces {
        internal interface IExecutor {
            Core.Result Execute(String query);
            Task<Core.Result> ExecuteAsync(String query);

            System.Data.Common.DbConnection RawConnection();
        }
    }

    internal class SqliteExecutor: Interfaces.IExecutor {
        private System.Data.SQLite.SQLiteConnection Con;

        public SqliteExecutor(Builder.SqliteBuilder builder) {
            this.Con = new System.Data.SQLite.SQLiteConnection(builder.ConnectionString);
        }

        public Core.Result Execute(String query) {
            this.Con.Open();

            var cmd = this.Con.CreateCommand();
            cmd.CommandText = query;

            var result = Core.Result.BuildSqliteResult(cmd.ExecuteReader());

            this.Con.Close();

            return result;
        }

        public async Task<Core.Result> ExecuteAsync(String query) {
            this.Con.Open();

            var cmd = this.Con.CreateCommand();
            cmd.CommandText = query;

            var result = Core.Result.BuildSqliteResult((await cmd.ExecuteReaderAsync()) as System.Data.SQLite.SQLiteDataReader);

            this.Con.Close();

            return result;
        }

        public System.Data.Common.DbConnection RawConnection() {
            return this.Con;
        }
    }

    internal class MySqlExecutor: Interfaces.IExecutor {
        private MySqlConnector.MySqlConnection Con;

        public MySqlExecutor(Builder.MySqlBuilder builder) {
            this.Con = new MySqlConnector.MySqlConnection(builder.ConnectionString);
        }

        public Core.Result Execute(String query) {
            this.Con.Open();
            
            var cmd = this.Con.CreateCommand();
            cmd.CommandText = query;

            var result = Core.Result.BuildMysqlResult(cmd.ExecuteReader());

            this.Con.Close();

            return result;
        }

        public async Task<Core.Result> ExecuteAsync(String query) {
            this.Con.Open();
            
            var cmd = this.Con.CreateCommand();
            cmd.CommandText = query;

            var result = Core.Result.BuildMysqlResult((await cmd.ExecuteReaderAsync()) as MySqlConnector.MySqlDataReader);

            this.Con.Close();

            return result;
        }

        public System.Data.Common.DbConnection RawConnection() {
            return this.Con;
        }
    }

    internal class MariadbExecutor: Interfaces.IExecutor {
        private MySqlConnector.MySqlConnection Con;

        public MariadbExecutor(Builder.MariadbBuilder builder) {
            this.Con = new MySqlConnector.MySqlConnection(builder.ConnectionString);
        }

        public Core.Result Execute(String query) {
            this.Con.Open();
            
            var cmd = this.Con.CreateCommand();
            cmd.CommandText = query;

            var result = Core.Result.BuildMariadbResult(cmd.ExecuteReader());

            this.Con.Close();

            return result;
        }

        public async Task<Core.Result> ExecuteAsync(String query) {
            this.Con.Open();
            
            var cmd = this.Con.CreateCommand();
            cmd.CommandText = query;

            var result = Core.Result.BuildMariadbResult((await cmd.ExecuteReaderAsync()) as MySqlConnector.MySqlDataReader);

            this.Con.Close();

            return result;
        }

        public System.Data.Common.DbConnection RawConnection() {
            return this.Con;
        }
    }
}
