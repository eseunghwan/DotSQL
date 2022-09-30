<h1 align="center">
    <br />
    DotSQL
    <br />
    <font style="font-size:18px;">Integrated Database-Helper for .NET</font>
</h1>
<br />
<hr>
<br />
Available for:

- .NET Standard 2.0
- .NET 5.0+


Supported Platforms:

- Windows 10+ (x86, amd64, arm64)
- Linux Distos (amd64, arm64)
- OSX 11+ (amd64, arm64)


Supported Databases:

- MySQL, MariaDB (MySQL Family)
- SQLite (x86, amd64 only on Linux, OSX)

<br />
<hr>
<br />
<br />

# Install
nuget package is not available not

<br />
<br />

# Usage
```c#
using DotSQL.Builder;
using DotSQL.SQL;

var sql = new DotSQL(new MariadbBuilder {
    UserID = "{UserID}", Password = "{Password}",
    Host = "{Host}", Port = {Port},
    Database = "{Database}"
});
var result = sql.Execute("{query}");

result.AsDict(); // to List of Dictionary
result.AsTable(); // to System.Data.DataTable
result.AsModel<{ModelClass}>(); // to List of ModelClass
```
