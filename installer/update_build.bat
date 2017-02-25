@echo. ======================================================================
@echo. Batch process file that create installer to package.
@echo. 
@echo. * Required "Inno Setup 5" later
@echo. * Required "7Zip"
@echo. ======================================================================

@echo 
@echo -----------------------------------
@echo Rebuild ExeOut.exe
@echo -----------------------------------

SET PATH="C:\Windows\Microsoft.NET\Framework\v4.0.30319";%PATH%

msbuild.exe /p:DefineConstants=RELEASE /p:DefineConstants="AESCRYPTO" /t:ReBuild /v:n ..\ExeOut\Exeout.csproj

..\tools\ExeToHex\ExeToHex\bin\Release\ExeToHex.exe ..\ExeOut\bin\Release\Exeout.exe ..\AttacheCase\ExeOut3.cs


@echo 
@echo -----------------------------------
@echo Rebuild AtcSetup.exe
@echo -----------------------------------

@rem Code signing
if exist "code_signing\_password.txt" (
set /p PASS=<code_signing\_password.txt

if exist "%ProgramFiles%Microsoft SDKs\Windows\v7.1A\Bin" (
SET PATH="%ProgramFiles%Microsoft SDKs\Windows\v7.1A\Bin";%PATH%
) else (
SET PATH="%ProgramFiles(x86)%Windows Kits\8.0\bin\x86";%PATH%
)
)

msbuild.exe /p:DefineConstants=RELEASE /p:DefineConstants=MS_STORE /t:ReBuild /v:n ..\AtcSetup\AtcSetup.csproj

@rem Code signing
signtool.exe sign /v /fd sha256 /f code_signing\OS201608304212.pfx /p %PASS% /t http://timestamp.globalsign.com/?signature=sha2 bin\AtcSetup.exe


@echo 
@echo -----------------------------------
@echo Rebuild AttacheCase.exe
@echo -----------------------------------

msbuild.exe /p:DefineConstants=RELEASE /p:DefineConstants="AESCRYPTO" /t:ReBuild /v:n ..\AttacheCase\AttacheCase.csproj

@rem Insert icons
..\tools\inserticons\inserticons.exe ..\AttacheCase3\AttacheCase\bin\Release\AttacheCase.exe ..\image\sub_icon\sub_icon00.ico;..\image\sub_icon\sub_icon01.ico;..\image\sub_icon\sub_icon02.ico;..\image\sub_icon\sub_icon03.ico

@rem Code signing
signtool.exe sign /v /fd sha256 /f code_signing\OS201608304212.pfx /p %PASS% /t http://timestamp.globalsign.com/?signature=sha2 bin\AttacheCase.exe


@echo 
@echo -----------------------------------
@echo Delete old files
@echo -----------------------------------

del /Q Archives\

@echo 
@echo -----------------------------------
@echo copy files
@echo -----------------------------------

mkdir bin

copy ..\AttacheCase\bin\Release\AttacheCase.exe bin\AttacheCase.exe
copy ..\AttacheCase\bin\Release\Microsoft.WindowsAPICodePack.dll bin\Microsoft.WindowsAPICodePack.dll
copy ..\AttacheCase\bin\Release\Microsoft.WindowsAPICodePack.Shell.dll bin\Microsoft.WindowsAPICodePack.Shell.dll
copy ..\AtcSetup\AtcSetup\bin\Release\AtcSetup.exe bin\AtcSetup.exe
mkdir bin\ja-JP
copy ..\AttacheCase\bin\Release\ja-JP\AttacheCase.resources.dll bin\ja-JP\AttacheCase.resources.dll
copy ..\images\main_icon\main_icon_48x48.png bin\

@echo 
@echo -----------------------------------
@echo Timestamp zero clear
@echo -----------------------------------

..\tools\setTimeZero\setTimeZero\bin\Release\setTimeZero.exe bin\AttacheCase.exe
..\tools\setTimeZero\setTimeZero\bin\Release\setTimeZero.exe bin\ja-JP\AttacheCase.resources.dll
..\tools\setTimeZero\setTimeZero\bin\Release\setTimeZero.exe bin\AtcSetup.exe

@echo. 
@echo. -----------------------------------
@echo. create installer package
@echo. -----------------------------------

if "%PROCESSOR_ARCHITECTURE%" == "AMD64" (
"%ProgramFiles(x86)%\Inno Setup 5\ISCC.exe" AttacheCase.iss
) else (
"%ProgramFiles%\Inno Setup 5\ISCC.exe" AttacheCase.iss
)

echo %ERRORLEVEL%


@echo. 
@echo. -----------------------------------
@echo. Code signing to Installer
@echo. -----------------------------------

if exist "code_signing\_password.txt" (

for /f "tokens=*" %%i in (code_signing\_password.txt) do Set PASS=%%i 

signtool.exe sign /v /fd sha256 /f code_signing\OS201608304212.pfx /p %PASS% /t http://timestamp.globalsign.com/?signature=sha2 Archives\atc*.exe

)


@echo. 
@echo. -----------------------------------
@echo. Create zip archive
@echo. -----------------------------------

@rem Get version number
for /F "delims=" %%s in ('..\tools\getver\getver\bin\Release\GetVer.exe bin\AttacheCase.exe') do @set NUM=%%s

@rem ZIP
cd bin
7z a -tzip ..\Archives\atcs%NUM%.zip AttacheCase.exe AtcSetup.exe Microsoft.WindowsAPICodePack.dll Microsoft.WindowsAPICodePack.Shell.dll ja-JP\AttacheCase.resources.dll
cd ..\


@echo. 
@echo. -----------------------------------
@echo. make hash file
@echo. -----------------------------------

..\tools\GetHash\GetHash\bin\Release\GetHash.exe Archives\atcs%NUM%.exe
..\tools\GetHash\GetHash\bin\Release\GetHash.exe Archives\atcs%NUM%.zip

@echo. 
@echo. -----------------------------------
@echo. Timestamp ( only time ) zero clear
@echo. -----------------------------------

..\tools\setTimeZero\setTimeZero\bin\Release\setTimeZero.exe /w Archives\*.exe
..\tools\setTimeZero\setTimeZero\bin\Release\setTimeZero.exe /w Archives\*.zip
..\tools\setTimeZero\setTimeZero\bin\Release\setTimeZero.exe /w Archives\*.md5
..\tools\setTimeZero\setTimeZero\bin\Release\setTimeZero.exe /w Archives\*.sha1


@echo. 
@echo. -----------------------------------
@echo. Delete temporary directrory
@echo. -----------------------------------

@rem rd /s /q "bin"

:END

@echo 
@echo **********************************************************************
@echo Batch process finished.
@echo **********************************************************************
@echo 


pause

