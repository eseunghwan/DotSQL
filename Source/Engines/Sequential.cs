
using System;
using System.Threading.Tasks;


namespace DotSQL.Engines {
    public class SequentialEngine {
        private Executors.Sequential.Interfaces.IExecutor Executor;

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

        public SequentialEngine(Builders.Sequential.Interfaces.IBuilder builder) {
            if (builder is Builders.Sequential.SqliteBuilder) {
                this.Executor = new Executors.Sequential.SqliteExecutor(builder as Builders.Sequential.SqliteBuilder);
            }
            else if (builder is Builders.Sequential.MySqlBuilder) {
                this.Executor = new Executors.Sequential.MySqlExecutor(builder as Builders.Sequential.MySqlBuilder);
            }
            else if (builder is Builders.Sequential.MariadbBuilder) {
                this.Executor = new Executors.Sequential.MariadbExecutor(builder as Builders.Sequential.MariadbBuilder);
            }
        }

        public Core.Results.SequentialResult Execute(String query) {
            return this.Executor.Execute(query);
        }

        public async Task<Core.Results.SequentialResult> ExecuteAsync(String query) {
            return await this.Executor.ExecuteAsync(query);
        }
    }
}
