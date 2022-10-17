
using System;


namespace DotSQL.SQL {
    internal class SqliteExecutor: IExecutor {
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

        public System.Data.Common.DbConnection RawConnection() {
            return this.Con;
        }
    }

    internal class MySqlExecutor: IExecutor {
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

        public System.Data.Common.DbConnection RawConnection() {
            return this.Con;
        }
    }

    internal class MariadbExecutor: IExecutor {
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

        public System.Data.Common.DbConnection RawConnection() {
            return this.Con;
        }
    }


    public class Engine {
        private IExecutor Executor;

        public Engine(Builder.IBuilder builder) {
            if (builder is Builder.SqliteBuilder) {
                this.Executor = new SqliteExecutor(builder as Builder.SqliteBuilder);
            }
            else if (builder is Builder.MySqlBuilder) {
                this.Executor = new MySqlExecutor(builder as Builder.MySqlBuilder);
            }
            else if (builder is Builder.MariadbBuilder) {
                this.Executor = new MariadbExecutor(builder as Builder.MariadbBuilder);
            }
        }

        public Core.Result Execute(String query) {
            return this.Executor.Execute(query);
        }

        public System.Data.Common.DbConnection RawConnection() {
            return this.Executor.RawConnection();
        }
    }
}
