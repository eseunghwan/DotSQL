
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using PandasNet;
using static PandasNet.PandasApi;


namespace DotSQL.Core.Results {
    public class SequentialResultSet {
        public ReadOnlyCollection<DbColumn> Columns { get; set; }
        public List<List<dynamic>> Datas { get; set; }
    }


    public class SequentialResult {
        private SequentialResultSet Resultset;

        private static List<dynamic> ParseRow(DbDataReader reader, ReadOnlyCollection<DbColumn> columns) {
            var row = new List<dynamic>();
            for (var idx = 0; idx < columns.Count; idx++) {
                var columnType = columns[idx].DataType;
                if (columnType.Equals(typeof(Int16))) {
                    try {
                        row.Add(reader.GetInt16(idx));
                    }
                    catch (System.InvalidCastException) {
                        row.Add(null);
                    }
                }
                else if (columnType.Equals(typeof(Int32))) {
                    try {
                        row.Add(reader.GetInt32(idx));
                    }
                    catch (System.InvalidCastException) {
                        row.Add(null);
                    }
                }
                else if (columnType.Equals(typeof(Int64))) {
                    try {
                        row.Add(reader.GetInt64(idx));
                    }
                    catch (System.InvalidCastException) {
                        row.Add(null);
                    }
                }
                else if (columnType.Equals(typeof(Decimal))) {
                    try {
                        row.Add(reader.GetDecimal(idx));
                    }
                    catch (System.InvalidCastException) {
                        row.Add(null);
                    }
                }
                else if (columnType.Equals(typeof(Double))) {
                    try {
                        row.Add(reader.GetDouble(idx));
                    }
                    catch (System.InvalidCastException) {
                        row.Add(null);
                    }
                }
                else if (columnType.Equals(typeof(float))) {
                    try {
                        row.Add(reader.GetFloat(idx));
                    }
                    catch (System.InvalidCastException) {
                        row.Add(null);
                    }
                }
                else if (columnType.Equals(typeof(Boolean))) {
                    try {
                        row.Add(reader.GetBoolean(idx));
                    }
                    catch (System.InvalidCastException) {
                        row.Add(null);
                    }
                }
                else if (columnType.Equals(typeof(DateTime))) {
                    try {
                        row.Add(reader.GetDateTime(idx));
                    }
                    catch (System.InvalidCastException) {
                        row.Add(null);
                    }
                }
                else if (columnType.Equals(typeof(String)) || columnType.Equals(typeof(Char))) {
                    try {
                        row.Add(reader.GetString(idx));
                    }
                    catch (System.InvalidCastException) {
                        row.Add(null);
                    }
                }
                else {
                    row.Add(null);
                }
            }

            return row;
        }

        public static SequentialResult BuildSqliteResult(System.Data.SQLite.SQLiteDataReader reader) {
            try {
                var columns = reader.GetColumnSchema();
                var datas = new List<List<dynamic>>();
                while (reader.Read()) {
                    datas.Add(SequentialResult.ParseRow(reader, columns));
                }

                return new SequentialResult(new SequentialResultSet { Columns = columns, Datas = datas });
            }
            catch (Exception e) {
                throw new Exceptions.BuildResultFailedException(e.Message, e.InnerException);
            }
        }

        public static SequentialResult BuildMysqlResult(MySqlConnector.MySqlDataReader reader) {
            try {
                var columns = reader.GetColumnSchema();
                var datas = new List<List<dynamic>>();
                while (reader.Read()) {
                    datas.Add(SequentialResult.ParseRow(reader, columns));
                }

                return new SequentialResult(new SequentialResultSet { Columns = columns, Datas = datas });
            }
            catch (Exception e) {
                throw new Exceptions.BuildResultFailedException(e.Message, e.InnerException);
            }
        }

        public static SequentialResult BuildMariadbResult(MySqlConnector.MySqlDataReader reader) {
            return SequentialResult.BuildMysqlResult(reader);
        }

        public SequentialResult(SequentialResultSet resultSet) {
            this.Resultset = resultSet;
        }

        public List<Dictionary<String, dynamic>> AsRecord() {
            try {
                var result = new List<Dictionary<String, dynamic>>();
                foreach (var data in this.Resultset.Datas) {
                    var resultRow = new Dictionary<String, dynamic>();
                    for (var cidx = 0; cidx < this.Resultset.Columns.Count; cidx++) {
                        resultRow[this.Resultset.Columns[cidx].ColumnName] = data[cidx];
                    }

                    result.Add(resultRow);
                }

                return result;
            }
            catch (Exception e) {
                throw new Exceptions.ResultConversionFailedException(e.Message, e.InnerException);
            }
        }

        public Dictionary<String, List<dynamic>> AsDict() {
            try {
                var result = new Dictionary<String, List<dynamic>>();
                for (var cidx = 0; cidx < this.Resultset.Columns.Count; cidx++) {
                    var column = this.Resultset.Columns[cidx].ColumnName;
                    if (!result.ContainsKey(column)) {
                        result[column] = new List<dynamic>();
                    }

                    foreach (var data in this.Resultset.Datas) {
                        result[column].Add(data[cidx]);
                    }
                }

                return result;
            }
            catch (Exception e) {
                throw new Exceptions.ResultConversionFailedException(e.Message, e.InnerException);
            }
        }

        public String AsJson(String jsonType = "dict") {
            if (jsonType.ToLower() == "dict") {
                return JsonConvert.SerializeObject(this.AsDict());
            }
            else if (jsonType.ToLower() == "record") {
                return JsonConvert.SerializeObject(this.AsRecord());
            }
            else {
                throw new Exceptions.ResultConversionFailedException($"Unsupported type: {jsonType}");
            }
        }

        public DataTable AsTable() {
            try {
                var result = new DataTable();
                foreach (var column in this.Resultset.Columns) {
                    var dtCol = new DataColumn();
                    dtCol.ColumnName = column.ColumnName;
                    dtCol.DataType = column.DataType;
                    result.Columns.Add(dtCol);
                }
                foreach (var data in this.Resultset.Datas) {
                    var row = result.NewRow();
                    for (var cidx = 0; cidx < this.Resultset.Columns.Count; cidx++) {
                        row[this.Resultset.Columns[cidx].ColumnName] = data[cidx];
                    }

                    result.Rows.Add(row);
                }

                return result;
            }
            catch (Exception e) {
                throw new Exceptions.ResultConversionFailedException(e.Message, e.InnerException);
            }
        }

        public DataFrame AsDataFrame() {
            try {
                return pd.DataFrame.from_dict(this.AsJson("dict"));
            }
            catch (Exception e) {
                throw new Exceptions.ResultConversionFailedException(e.Message, e.InnerException);
            }
        }

        public List<T> AsModel<T>() {
            var result = new List<T>();
            foreach (var data in this.Resultset.Datas) {
                Type tt = typeof(T);
                T model = (T)Activator.CreateInstance(tt);
                for (var cidx = 0; cidx < this.Resultset.Columns.Count; cidx++) {
                    var column = this.Resultset.Columns[cidx];
                    var prop = tt.GetProperty(column.ColumnName);

                    if (prop != null && prop.PropertyType.Equals(column.DataType)) {
                        prop.SetValue(model, data[cidx]);
                    }
                    else if (prop != null) {
                        prop.SetValue(model, null);
                    }
                }

                result.Add(model);
            }

            return result;
        }
    }
}
