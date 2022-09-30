
using System;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace DotSQL.Core {
    public class ResultSet {
        public ReadOnlyCollection<DbColumn> Columns { get; set; }
        public List<List<dynamic>> Datas { get; set; }
    }


    public class Result {
        private ResultSet Resultset;

        private static List<dynamic> ParseRow(DbDataReader reader, ReadOnlyCollection<DbColumn> columns) {
            var row = new List<dynamic>();
            for (var idx = 0; idx < columns.Count; idx++) {
                var columnType = columns[idx].DataType;
                if (columnType.Equals(typeof(Int16))) {
                    row.Add(reader.GetInt16(idx));
                }
                else if (columnType.Equals(typeof(Int32))) {
                    row.Add(reader.GetInt32(idx));
                }
                else if (columnType.Equals(typeof(Int64))) {
                    row.Add(reader.GetInt64(idx));
                }
                else if (columnType.Equals(typeof(Decimal))) {
                    row.Add(reader.GetDecimal(idx));
                }
                else if (columnType.Equals(typeof(Double))) {
                    row.Add(reader.GetDouble(idx));
                }
                else if (columnType.Equals(typeof(float))) {
                    row.Add(reader.GetFloat(idx));
                }
                else if (columnType.Equals(typeof(Boolean))) {
                    row.Add(reader.GetBoolean(idx));
                }
                else if (columnType.Equals(typeof(DateTime))) {
                    row.Add(reader.GetDateTime(idx));
                }
                else if (columnType.Equals(typeof(String)) || columnType.Equals(typeof(Char))) {
                    row.Add(reader.GetString(idx));
                }
                else {
                    row.Add(Double.NaN);
                }
            }

            return row;
        }

        public static Result BuildSqliteResult(System.Data.SQLite.SQLiteDataReader reader) {
            var columns = reader.GetColumnSchema();
            var datas = new List<List<dynamic>>();
            while (reader.Read()) {
                datas.Add(Result.ParseRow(reader, columns));
            }

            return new Result(new ResultSet { Columns = columns, Datas = datas });
        }

        public static Result BuildMysqlResult(MySqlConnector.MySqlDataReader reader) {
            var columns = reader.GetColumnSchema();
            var datas = new List<List<dynamic>>();
            while (reader.Read()) {
                datas.Add(Result.ParseRow(reader, columns));
            }

            return new Result(new ResultSet { Columns = columns, Datas = datas });
        }

        public static Result BuildMariadbResult(MySqlConnector.MySqlDataReader reader) {
            return Result.BuildMysqlResult(reader);
        }

        public Result(ResultSet resultSet) {
            this.Resultset = resultSet;
        }

        public List<Dictionary<String, dynamic>> AsDict() {
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

        public DataTable AsTable() {
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

        public List<T> AsModel<T>() {
            var result = new List<T>();
            foreach (var data in this.Resultset.Datas) {
                Type tt = typeof(T);
                T model = (T)Activator.CreateInstance(tt);
                for (var cidx = 0; cidx < this.Resultset.Columns.Count; cidx++) {
                    var column = this.Resultset.Columns[cidx];
                    var prop = tt.GetProperty(column.ColumnName);
                    if (prop.PropertyType.Equals(column.DataType)) {
                        prop.SetValue(model, data[cidx]);
                    }
                    else {
                        prop.SetValue(model, null);
                    }
                }

                result.Add(model);
            }

            return result;
        }
    }
}
