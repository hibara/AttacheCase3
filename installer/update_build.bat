@echo. ======================================================================
@echo. Batch process file that create installer to package.
@echo. 
@echo. * Required "Inno Setup 5" ( not ver.6 )
@echo. * Required "7Zip"
@echo. ======================================================================

@echo 
@echo -----------------------------------
@echo Clean up
@echo -----------------------------------

rmdir /s /q bin
mkdir bin


@echo 
@echo -----------------------------------
@echo Rebuild ExeOut.exe
@echo -----------------------------------

SET PATH="C:\Windows\Microsoft.NET\Framework\v4.0.30319";%PATH%

msbuild.exe /p:Configuration="Release" /p:DefineConstants="AESCRYPTO" /p:Platform="AnyCPU" /p:PostBuildEvent= /t:ReBuild /v:n ..\ExeOut\Exeout.csproj

..\tools\ExeToHex\ExeToHex\bin\Release\ExeToHex.exe ..\ExeOut\bin\Release\Exeout.exe ..\AttacheCase\ExeOut3.cs


@echo 
@echo -----------------------------------
@echo Rebuild AtcSetup.exe
@echo -----------------------------------

msbuild.exe /p:Configuration="Release" /p:Platform="AnyCPU" /t:ReBuild /v:n ..\AtcSetup\AtcSetup.csproj


@echo 
@echo -----------------------------------
@echo Rebuild AttacheCase.exe
@echo -----------------------------------

msbuild.exe /p:Configuration="Release" /p:DefineConstants="AESCRYPTO" /p:Platform="AnyCPU" /t:ReBuild /v:n ..\AttacheCase\AttacheCase.csproj

@rem Insert icons
..\tools\inserticons\inserticons.exe ..\AttacheCase\bin\Release\AttacheCase.exe ..\image\sub_icon\sub_icon00.ico;..\image\sub_icon\sub_icon01.ico;..\image\sub_icon\sub_icon02.ico;..\image\sub_icon\sub_icon03.ico


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
@rem copy ..\AttacheCase\bin\Release\Microsoft.WindowsAPICodePack.dll bin\Microsoft.WindowsAPICodePack.dll
@rem copy ..\AttacheCase\bin\Release\Microsoft.WindowsAPICodePack.Shell.dll bin\Microsoft.WindowsAPICodePack.Shell.dll
copy ..\AtcSetup\AtcSetup\bin\Release\AtcSetup.exe bin\AtcSetup.exe
@rem mkdir bin\ja-JP
@rem copy ..\AttacheCase\bin\Release\ja-JP\AttacheCase.resources.dll bin\ja-JP\AttacheCase.resources.dll


@echo 
@echo -----------------------------------
@echo Code signing to each exe file
@echo -----------------------------------

SET PATH="C:\Program Files\Microsoft SDKs\Windows\v7.0A\bin";%PATH%
SET PATH="C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin";%PATH%
SET PATH="C:\Program Files\Windows Kits\8.0\bin\x86";%PATH%

@rem signtool.exe sign /v /a /n "Mitsuhiro Hibara" /tr http://rfc3161timestamp.globalsign.com/advanced /td sha256 bin\AttacheCase.exe
@rem signtool.exe sign /v /a /n "Mitsuhiro Hibara" /tr http://rfc3161timestamp.globalsign.com/advanced /td sha256 bin\AtcSetup.exe

signtool.exe sign /a /v /n "Mitsuhiro Hibara" /t http://timestamp.comodoca.com/authenticode bin\AttacheCase.exe
signtool.exe sign /a /v /n "Mitsuhiro Hibara" /t http://timestamp.comodoca.com/authenticode bin\AtcSetup.exe

@echo. 
@echo. -----------------------------------
@echo. create installer package
@echo. -----------------------------------

if "%PROCESSOR_ARCHITECTURE%" == "AMD64" (
"%ProgramFiles(x86)%\Inno Setup 5\ISCC.exe" AttacheCase.iss
) else (
"%ProgramFiles%\Inno Setup 5\ISCC.exe" AttacheCase.iss
)

@echo "Error level:", %ERRORLEVEL%

@echo. 
@echo. -----------------------------------
@echo. Code signing to Installer
@echo. -----------------------------------

@rem signtool.exe sign /v /a /n "Mitsuhiro Hibara" /tr http://rfc3161timestamp.globalsign.com/advanced /td sha256 Archives\atc*.exe
signtool.exe sign /a /v /n "Mitsuhiro Hibara" /t http://timestamp.comodoca.com/authenticode Archives\atc*.exe

@echo. 
@echo. -----------------------------------
@echo. Create zip archive
@echo. -----------------------------------

@rem Get version number
for /F "delims=" %%s in ('..\tools\GetVer\GetVer\bin\Release\GetVer.exe bin\AttacheCase.exe') do @set NUM=%%s

echo "ver.%NUM%"

@rem ZIP
cd bin
7z a -tzip ..\Archives\atcs%NUM%.zip AttacheCase.exe AtcSetup.exe
cd ..\


@echo. 
@echo. -----------------------------------
@echo. make hash file
@echo. -----------------------------------

..\tools\GetHash\GetHash\bin\Release\GetHash.exe Archives\atcs%NUM%.exe
..\tools\GetHash\GetHash\bin\Release\GetHash.exe Archives\atcs%NUM%.zip


@echo. 
@echo. -----------------------------------
@echo. Delete orary directrory
@echo. -----------------------------------

@rem rd /s /q "bin"

:END

@echo 
@echo **********************************************************************
@echo Batch process finished.
@echo **********************************************************************
@echo 

pause

