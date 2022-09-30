
using System;

namespace DotSQL.Test {
    public class TestItem {
        public String NAME { get; set; }
        public String VALUE { get; set; }
    }

    internal class Program {
        static void Main(String[] args) {
            var sql = new SQL.DotSQL(new Builder.MariadbBuilder {
                UserID = "root", Password = "root", Database = "test"
            });
            var res = sql.Execute("select * from `test`;");

            var dictRes = res.AsDict();
            var dtRes = res.AsTable();
            var modelRes = res.AsModel<TestItem>();

            foreach (var model in modelRes) {
                Console.WriteLine($"NAME: {model.NAME}, VALUE: {model.VALUE}");
            }
        }
    }
}
