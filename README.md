## Find different files

Finds files that are not in the target directory, ignoring subfolders.

> I know that such a search can be implemented through shell commands or other small programs. I just practiced c#.


## Publication
### Win 64
`dotnet publish --configuration Release -r win-x64 -p:PublishSingleFile=true --self-contained false`

### Linux 64
`dotnet publish --configuration Release -r linux-x64 -p:PublishSingleFile=true --self-contained false`

***

(c) ilinei