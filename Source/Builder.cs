
using System;

namespace DotSQL.Builder {
    namespace Interfaces {
        public interface IBuilder {
            String ConnectionString { get; }
        }
    }

    public class SqliteBuilder: Interfaces.IBuilder {
        public String Source { get; set; }
        public String Password { get; set; } = null;

        public String ConnectionString {
            get {
                System.Data.SQLite.SQLiteConnectionStringBuilder builder;
                if (this.Password == null) {
                    builder = new System.Data.SQLite.SQLiteConnectionStringBuilder {
                        DataSource = this.Source
                    };
                }
                else {
                    builder = new System.Data.SQLite.SQLiteConnectionStringBuilder {
                        DataSource = this.Source, Password = this.Password
                    };
                }

                return builder.ConnectionString;
            }
        }
    }

    public class MySqlBuilder: Interfaces.IBuilder {
        public String Host { get; set; } = "127.0.0.1";
        public Int32 Port { get; set; } = 3306;
        public String UserID { get; set; }
        public String Password { get; set; }
        public String Database { get; set; }
        public String Charset { get; set; } = "utf8mb4";

        public String ConnectionString {
            get {
                MySqlConnector.MySqlConnectionStringBuilder builder;
                if (this.Database == null) {
                    builder = new MySqlConnector.MySqlConnectionStringBuilder {
                        Server = this.Host, Port = (uint)this.Port, UserID = this.UserID, Password = this.Password, CharacterSet = this.Charset
                    };
                }
                else {
                    builder = new MySqlConnector.MySqlConnectionStringBuilder {
                        Server = this.Host, Port = (uint)this.Port, UserID = this.UserID, Password = this.Password, Database = this.Database, CharacterSet = this.Charset
                    };
                }

                return builder.ConnectionString;
            }
        }
    }

    public class MariadbBuilder: MySqlBuilder {}
}

