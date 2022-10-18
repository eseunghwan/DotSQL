
using System;
using System.Threading.Tasks;


namespace DotSQL.Test {
    public class TestItem {
        public String NAME { get; set; }
        public String VALUE { get; set; }
    }

    internal class Program {
        static async Task Main(String[] args) {
            var engine = new Engine.Engine(new Builder.MariadbBuilder {
                UserID = "root", Password = "root", Database = "test"
            });
            var res = await engine.ExecuteAsync("select * from `test`;");

            var dictRes = res.AsDict();
            var dtRes = res.AsTable();
            var modelRes = res.AsModel<TestItem>();

            foreach (var model in modelRes) {
                Console.WriteLine($"NAME: {model.NAME}, VALUE: {model.VALUE}");
            }
        }
    }
}
