
bin\nuget.exe restore Xwt-Gtk3.sln

call msbuild Xwt-Gtk3.sln "/p:Configuration=Debug"

@PAUSE