## 0.0.1
- Initial release.

<br />

## 0.0.2
- change 'DotSQL' of 'DotSQL.SQL' to 'Engine'.
- update files

<br />

## 0.0.3
- fix bug on 'AsModel'.
- change 'SQL' namespace to 'Engine'.
- merge 'Classes.cs' and 'Interfaces.cs' into one file.
- add 'ExecuteAsync' to Engine.
- add Exceptions.

<br />

## 0.0.4
- add exception catch to 'Result.ParseRow'.
  - now add null value correctly.

<br />

## 0.0.5
- change 'RawConnection' from method to getter.
  - before return, check connection and if failed, throw 'ConnectionFailedException'.
- add license to nuget package.
- add 'README.md' to nuget package.
- add github url to nuget package.
