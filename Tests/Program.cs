
using System;
using System.Threading.Tasks;


namespace DotSQL.Test {
    public class TestItem {
        public String NAME { get; set; }
        public String VALUE { get; set; }
    }

    internal class Program {
        static async Task Main(String[] args) {
            var engine = new Engines.SequentialEngine(new Builders.Sequential.MariadbBuilder {
                UserID = "root", Password = "root", Database = "test"
            });
            var res = await engine.ExecuteAsync("select * from `test`;");

            Console.WriteLine("\nAsDict");
            var dictRes = res.AsDict();
            foreach (var item in dictRes) {
                Console.WriteLine($"Key: {item.Key}, Value: [ {String.Join(",", item.Value)} ]");
            }

            Console.WriteLine("\nAsRecord");
            var recordRes = res.AsRecord();
            foreach (var record in recordRes) {
                foreach (var item in record) {
                    Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
                }
            }

            Console.WriteLine("\nAsJson(Dict)");
            var jsonResDict = res.AsJson("dict");
            Console.WriteLine(jsonResDict);

            Console.WriteLine("\nAsJson(Record)");
            var jsonResRecord = res.AsJson("record");
            Console.WriteLine(jsonResRecord);

            Console.WriteLine("\nAsDataFrame");
            var dfRes = res.AsDataFrame();
            Console.WriteLine(dfRes);

            Console.WriteLine("\nAsTable");
            var dtRes = res.AsTable();

            Console.WriteLine("\nAsModel");
            var modelRes = res.AsModel<TestItem>();
            foreach (var model in modelRes) {
                Console.WriteLine($"NAME: {model.NAME}, VALUE: {model.VALUE}");
            }
        }
    }
}
