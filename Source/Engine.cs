
using System;
using System.Threading.Tasks;


namespace DotSQL.Engine {
    public class Engine {
        private Executors.Interfaces.IExecutor Executor;

        public System.Data.Common.DbConnection RawConnection {
            get {
                var rawCon = this.Executor.RawConnection();
                try {
                    rawCon.Open();
                }
                catch {
                    throw new Exceptions.ConnectionFailedException("Raw Connection Failed!");
                }

                return rawCon;
            }
        }

        public Engine(Builder.Interfaces.IBuilder builder) {
            if (builder is Builder.SqliteBuilder) {
                this.Executor = new Executors.SqliteExecutor(builder as Builder.SqliteBuilder);
            }
            else if (builder is Builder.MySqlBuilder) {
                this.Executor = new Executors.MySqlExecutor(builder as Builder.MySqlBuilder);
            }
            else if (builder is Builder.MariadbBuilder) {
                this.Executor = new Executors.MariadbExecutor(builder as Builder.MariadbBuilder);
            }
        }

        public Core.Result Execute(String query) {
            return this.Executor.Execute(query);
        }

        public async Task<Core.Result> ExecuteAsync(String query) {
            return await this.Executor.ExecuteAsync(query);
        }
    }
}
