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

<br />
Supported Platforms:

- Windows 10+ (x86, amd64, arm64)
- Linux Distos (amd64, arm64)
- OSX 11+ (amd64, arm64)

<br />
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
# or specific version
dotnet add package DotSQL --version {version}
```

<br />
<br />

# Usage
- Sequentials
```c#
using DotSQL.Builders.Sequential;
using DotSQL.Engines;

var engine = new SequentialEngine(new MariadbBuilder {
    UserID = "{UserID}", Password = "{Password}",
    Host = "{Host}", Port = {Port},
    Database = "{Database}"
});
var result = engine.Execute("{query}");
// use ExecuteAsync method also.
var result = await engine.ExecuteAsync("{query}");

result.AsRecord(); // to List of Dictionary
result.AsDict(); // to Dictionary of String and List
result.AsJson(); // to Json, Format of Dict and Record
result.AsDataFrame(); // to Pandas.NET.Dataframe
result.AsTable(); // to System.Data.DataTable
result.AsModel<{ModelClass}>(); // to List of ModelClass
```

<br />
<br />

# ToDo
- MSSQL Support
  - Cause no Windows machines
- Non-Sequential Support
