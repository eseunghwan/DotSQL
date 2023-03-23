
using System;
using System.Threading.Tasks;


namespace DotSQL.Executors.Sequential {
    namespace Interfaces {
        internal interface IExecutor {
            public Core.Results.SequentialResult Execute(String query);
            public Task<Core.Results.SequentialResult> ExecuteAsync(String query);

            public System.Data.Common.DbConnection RawConnection();
        }
    }

    internal class SqliteExecutor: Interfaces.IExecutor {
        private System.Data.SQLite.SQLiteConnection Con;

        public SqliteExecutor(Builders.Sequential.SqliteBuilder builder) {
            this.Con = new System.Data.SQLite.SQLiteConnection(builder.ConnectionString);
        }

        public Core.Results.SequentialResult Execute(String query) {
            try {
                this.Con.Open();
            }
            catch (System.Data.SQLite.SQLiteException e) {
                throw new Exceptions.ConnectionFailedException(e.Message, e.InnerException);
            }

            var cmd = this.Con.CreateCommand();
            cmd.CommandText = query;

            var result = Core.Results.SequentialResult.BuildSqliteResult(cmd.ExecuteReader());

            this.Con.Close();

            return result;
        }

        public async Task<Core.Results.SequentialResult> ExecuteAsync(String query) {
            try {
                this.Con.Open();
            }
            catch (System.Data.SQLite.SQLiteException e) {
                throw new Exceptions.ConnectionFailedException(e.Message, e.InnerException);
            }

            var cmd = this.Con.CreateCommand();
            cmd.CommandText = query;

            var result = Core.Results.SequentialResult.BuildSqliteResult((await cmd.ExecuteReaderAsync()) as System.Data.SQLite.SQLiteDataReader);

            this.Con.Close();

            return result;
        }

        public System.Data.Common.DbConnection RawConnection() {
            return this.Con;
        }
    }

    internal class MySqlExecutor: Interfaces.IExecutor {
        private MySqlConnector.MySqlConnection Con;

        public MySqlExecutor(Builders.Sequential.MySqlBuilder builder) {
            this.Con = new MySqlConnector.MySqlConnection(builder.ConnectionString);
        }

        public Core.Results.SequentialResult Execute(String query) {
            try {
                this.Con.Open();
            }
            catch (MySqlConnector.MySqlException e) {
                throw new Exceptions.ConnectionFailedException(e.Message, e.InnerException);
            }
            
            var cmd = this.Con.CreateCommand();
            cmd.CommandText = query;

            var result = Core.Results.SequentialResult.BuildMysqlResult(cmd.ExecuteReader());

            this.Con.Close();

            return result;
        }

        public async Task<Core.Results.SequentialResult> ExecuteAsync(String query) {
            try {
                this.Con.Open();
            }
            catch (MySqlConnector.MySqlException e) {
                throw new Exceptions.ConnectionFailedException(e.Message, e.InnerException);
            }
            
            var cmd = this.Con.CreateCommand();
            cmd.CommandText = query;

            var result = Core.Results.SequentialResult.BuildMysqlResult((await cmd.ExecuteReaderAsync()) as MySqlConnector.MySqlDataReader);

            this.Con.Close();

            return result;
        }

        public System.Data.Common.DbConnection RawConnection() {
            return this.Con;
        }
    }

    internal class MariadbExecutor: Interfaces.IExecutor {
        private MySqlConnector.MySqlConnection Con;

        public MariadbExecutor(Builders.Sequential.MariadbBuilder builder) {
            this.Con = new MySqlConnector.MySqlConnection(builder.ConnectionString);
        }

        public Core.Results.SequentialResult Execute(String query) {
            try {
                this.Con.Open();
            }
            catch (MySqlConnector.MySqlException e) {
                throw new Exceptions.ConnectionFailedException(e.Message, e.InnerException);
            }
            
            var cmd = this.Con.CreateCommand();
            cmd.CommandText = query;

            var result = Core.Results.SequentialResult.BuildMariadbResult(cmd.ExecuteReader());

            this.Con.Close();

            return result;
        }

        public async Task<Core.Results.SequentialResult> ExecuteAsync(String query) {
            try {
                this.Con.Open();
            }
            catch (MySqlConnector.MySqlException e) {
                throw new Exceptions.ConnectionFailedException(e.Message, e.InnerException);
            }
            
            var cmd = this.Con.CreateCommand();
            cmd.CommandText = query;

            var result = Core.Results.SequentialResult.BuildMariadbResult((await cmd.ExecuteReaderAsync()) as MySqlConnector.MySqlDataReader);

            this.Con.Close();

            return result;
        }

        public System.Data.Common.DbConnection RawConnection() {
            return this.Con;
        }
    }
}
