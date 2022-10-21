<h1 align="center">
    <br />
    DotSQL
    <br />
    <font style="font-size:18px;">Integrated Database-Helper for .NET</font>
    <br />
    <br />
</h1>
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
- SQLite (amd64 only on Linux, OSX)

<br />
<hr>
<br />
<br />

# Install
```bash
dotnet add package DotSQL
```

<br />
<br />

# Usage
```c#
using DotSQL.Builder;
using DotSQL.SQL;

var engine = new Engine(new MariadbBuilder {
    UserID = "{UserID}", Password = "{Password}",
    Host = "{Host}", Port = {Port},
    Database = "{Database}"
});
var result = engine.Execute("{query}");
// use ExecuteAsync method also.
// var result = await engine.ExecuteAsync("{query}");

result.AsDict(); // to List of Dictionary
result.AsTable(); // to System.Data.DataTable
result.AsModel<{ModelClass}>(); // to List of ModelClass
```
