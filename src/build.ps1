MyGet-Build-Solution -sln VolskNet.sln
#mkdir Build
#mkdir Build\lib
#mkdir Build\lib\net45

#%nuget% pack "src\VolskNet.Core\VolskNet.Core.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
