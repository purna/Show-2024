@echo off

ech
start "" /max "C:\Users\nigel.morris\Show 2024\Test1\Show 2024.exe"



if "%1" == "" (
	echo No parameter specified. Usage : open_app.bat [application_number]
	exit /b
)


setlocal enabledelayedexpansion
set "application[0]=C:\Users\nigel.morris\Show 2024\Test1\Show 2024.exe"
set "application[1]=C:\Users\nigel.morris\Show 2024\Test1\Show 2024.exe"
set "application[2]=C:\Users\nigel.morris\Show 2024\Test1\Show 2024.exe"
set "application[3]=C:\Users\nigel.morris\Show 2024\Test1\Show 2024.exe"
set "application[4]=C:\Users\nigel.morris\Show 2024\Test1\Show 2024.exe"
set "application[5]=C:\Users\nigel.morris\Show 2024\Test1\Show 2024.exe"
set "application[6]=C:\Users\nigel.morris\Show 2024\Test1\Show 2024.exe"
set "application[7]=C:\Users\nigel.morris\Show 2024\Test1\Show 2024.exe"
set "application[8]=C:\Users\nigel.morris\Show 2024\Test1\Show 2024.exe"

REM Comment
REM echo Opening shortcut...
REM start "" "path_to_shortcut.lnk"
REM echo Shortcut opened

if not defined application[%1] (
	echo Invalid application number.
	exit /b
)

echo Opening application %1: !application[%1]!
start "" /max "!application[%1]!"
