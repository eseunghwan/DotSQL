
using System;
using System.Data.SQLite;

namespace DotSQL.Builder {
    public class SqliteBuilder: IBuilder {
        public String Source { get; set; }
        public String Password { get; set; } = null;

        public String ConnectionString {
            get {
                SQLiteConnectionStringBuilder builder;
                if (this.Password == null) {
                    builder = new SQLiteConnectionStringBuilder {
                        DataSource = this.Source
                    };
                }
                else {
                    builder = new SQLiteConnectionStringBuilder {
                        DataSource = this.Source, Password = this.Password
                    };
                }

                return builder.ConnectionString;
            }
        }
    }

    public class MySqlBuilder: IBuilder {
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

        // public MySqlBuilder(String userid, String password, String database, String host = "127.0.0.1", Int32 port = 3306, String charset = "utf8mb4") {
        //     this.Host = host;
        //     this.Port = port;
        //     this.UserID = userid;
        //     this.Password = password;
        //     this.Charset = charset;
        // }

        // public String ToConnectionString() {
        //     var builder = new MySqlConnector.MySqlConnectionStringBuilder {
        //         Server = this.Host, Port = (uint)this.Port, UserID = this.UserID, Password = this.Password, Database = this.Database, CharacterSet = this.Charset
        //     };
            
        //     return builder.ConnectionString;
        // }
    }

    public class MariadbBuilder: MySqlBuilder {}
}
