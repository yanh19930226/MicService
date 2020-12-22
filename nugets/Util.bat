@echo off
set name=Util
set alphaVersion=%date:~2,2%%date:~5,2%%date:~8,2%%time:~0,2%%time:~3,2%%time:~6,2%

echo pack nuget %name% %beatVersion%

rd/s/q ".\packs"
dotnet pack "..\src\%name%" -o ".\packs" -c release 
dotnet nuget push .\packs\*.nupkg -k f2e0afc9-d16a-3d0c-83ad-a919a1e0df43 -s http://120.78.1.82:8081/repository/nuget-hosted/

for %%i in (.\packs\*.nupkg) do ( 
    echo %%~ni 
    curl "https://oapi.dingtalk.com/robot/send?access_token=22aea5bd7b86036a4f62fc105316f25f6dfc9eb790294367588cfacc1fee187f" -H "Content-Type: application/json" -d "{'msgtype': 'text', 'text': {'content': 'nuget: %%~ni updated'}}"
)