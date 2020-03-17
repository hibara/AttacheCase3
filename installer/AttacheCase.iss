#define MyAppVer GetFileVersion("bin\AttacheCase.exe")
#define MyAppVerNum StringChange(MyAppVer, ".", "")

[Languages]
Name:"en"; MessagesFile:"compiler:Default.isl"
Name:"jp"; MessagesFile:"compiler:Languages\Japanese.isl"

[CustomMessages]
en.AppName=AttacheCase#3
jp.AppName=アタッシェケース#3
en.AppComments=File and folder encryption software
jp.AppComments=ファイル・フォルダー暗号化ソフトウェア
;en.SetUpProgramDescription=Set up program for 'AttacheCase#3'
;jp.SetUpProgramDescription=「アタッシェケース#3」セットアッププログラム 
en.UnintallName=Uninstall
jp.UnintallName=アンインストール
en.MsgFailedToInstallDotNetFramework=Failed to install .NET Framework 4.0.%nPlease install the .NET Framework 4.0 such as from Windows update.%nAnd then please start this setup program again.
jp.MsgFailedToInstallDotNetFramework=.NET Framework 4.0 のインストールに失敗したようです。%nWindowsアップデートなどから .NET Frameworkをインストールして、%nセットアッププログラムを再度起動してください。
en.LaunchProgram=Launch AttacheCase3 after finishing installation.
jp.LaunchProgram=インストール完了後に、アタッシェケース#3 を起動します。
en.OpenFile=Open file in AttacheCase
jp.OpenFile=アタッシェケースでファイルを開く
en.DecodeFile=Decrypt AttacheCase file
jp.DecodeFile=アタッシェケースファイルを復号する

[Setup]
AppName={cm:AppName}
AppVersion={#MyAppVer}
AppVerName={cm:AppName} ver.{#MyAppVer}
DefaultGroupName={cm:AppName}
OutputBaseFilename=atcs{#MyAppVerNum}
DefaultDirName={pf}\AttacheCase3
UsePreviousAppDir=yes
AppendDefaultDirName=yes 
OutputDir=.\archives
TouchTime=00:00
ShowLanguageDialog=yes
UsePreviousLanguage=no
ChangesAssociations=yes
SignedUninstaller=yes
SignTool=MySignTool

;-----------------------------------
;インストーラプログラム
;-----------------------------------
VersionInfoVersion={#MyAppVer};VersionInfoDescription={cm:SetUpProgramDescription}
AppCopyright=Copyright(C) 2016-2019 M.Hibara, All rights reserved.
;SetupIconFile=icon\main_icon.ico
;ウィザードページに表示されるグラフィック（*.bmp: 164 x 314）
;Graphic in wizard page.
WizardImageFile=bmp\installer_pic_01.bmp
;ウィザードページに表示されるグラフィックが拡大されない
;Graphic in wizard page that is not expanded.
WizardImageStretch=no
;その隙間色
;Background color.
WizardImageBackColor=$ffffff 
;ウィザードページの右上部分のグラフィック（*.bmp: 55 x 58）
;Graphic in top-right window of wizard page.
WizardSmallImageFile=bmp\installer_pic_02.bmp
;進捗表示
;Progress.
ShowTasksTreeLines=yes

;------------------------------------------
;「プログラムの追加と削除」ダイアログ情報
;------------------------------------------
;配布元
AppPublisher=Mitsuhiro Hibara
;アプリケーション配布元 Webサイトの URL
AppPublisherURL=https://hibara.org
;連絡先
AppContact=m@hibara.org
;サポートサイトURL
AppSupportURL=https://hibara.org/software/
;ReadMeファイルパス
;AppReadmeFile="{app}\AttacheCase3\readme.txt"
;製品更新先のURL
AppUpdatesURL=https://hibara.org/software/AttacheCase/
;アプリケーションの説明
AppComments={cm:AppComments}
#include "idp.iss"

[Files]
Source:"bin\AttacheCase.exe"; DestDir:"{app}";Flags: ignoreversion touch
Source:"bin\AtcSetup.exe"; DestDir:"{app}";Flags: ignoreversion touch
;Source: "bin\Microsoft.WindowsAPICodePack.dll"; DestDir: "{app}"; Flags: ignoreversion touch
;Source: "bin\Microsoft.WindowsAPICodePack.Shell.dll"; DestDir: "{app}"; Flags: ignoreversion touch
;Source: "bin\ja-JP\AttacheCase.resources.dll"; DestDir: "{app}\ja-JP"; Flags: ignoreversion touch
;Source: "bin\readme.txt"; DestDir: "{userappdata}\AttacheCase3"; Flags: ignoreversion touch

[Icons]
Name:"{group}\AttacheCase"; Filename:"{app}\AttacheCase.exe"; WorkingDir:"{app}"
Name:"{group}\{cm:UnintallName}"; Filename:"{uninstallexe}"
Name:"{commondesktop}\{cm:AppName}"; Filename:"{app}\AttacheCase.exe"; WorkingDir:"{app}"; Tasks:desktopicon

[Tasks]
;"デスクトップ上にアイコンを作成する(&D)"
;Create a &desktop icon
Name: desktopicon; Description: {cm:CreateDesktopIcon};
;ファイル拡張子 %2 に %1 を関連付けます。
;&Associate %1 with the %2 file extension
Name: association; Description:{cm:AssocFileExtension,{cm:AppName},*.atc};

[Run]
;Filename:"{app}\AtcSetup.exe"; Parameters:"-t=0 -p=""{app}\AttacheCase.exe"""; Tasks:association; Flags:postinstall runascurrentuser skipifsilent shellexec
Filename:"{app}\AttacheCase.exe"; Description:{cm:LaunchProgram}; Flags:postinstall skipifsilent shellexec

[InstallDelete]
Type: filesandordirs; Name: {app}\ja-JP
Type: files; Name: {app}\Microsoft.WindowsAPICodePack.dll
Type: files; Name: {app}\Microsoft.WindowsAPICodePack.Shell.dll

[Registry]
;関連付け設定
;Associate *.atc file extension
Root: HKCR; Subkey: ".atc"; ValueData: "AttacheCase.DataFile"; Flags: uninsdeletevalue; ValueType: string; ValueName: ""; Tasks:association

Root:HKCR; Subkey:"AttacheCase.DataFile"; ValueData:""; Flags:uninsdeletekey; ValueType:string; ValueName:""; Tasks:association
Root:HKCR; Subkey:"AttacheCase.DataFile\DefaultIcon";ValueData:"{app}\AttacheCase.exe,0"; ValueType:string; ValueName:""; Tasks:association

Root:HKCR; Subkey:"AttacheCase.DataFile\shell\open\command"; ValueData:"""{app}\AttacheCase.exe"" ""%1"""; ValueType:string; ValueName:""; Tasks:association
;Root:HKCR; Subkey:"AttacheCase.DataFile\shell\decode\command"; ValueData:"""{app}\AttacheCase.exe"" ""%1"""; ValueType:string; ValueName:"{cm:DecodeFile}"; Tasks:association

;動作設定
;Options
Root:HKCU; Subkey:"Software\Hibara\AttacheCase3"; Flags:uninsdeletekey


[Code]
//
// http://klimov.software/innosetup-install-net-framework-during-setup/
//
#include "DetectNetVersion.iss"
 
const
  dotNetVersion = 'v4\Full';
  servicePack = 0;
  dotNetWebInstallerURL = 'http://download.microsoft.com/download/1/B/E/1BE39E79-7E39-46A3-96FF-047F95396215/dotNetFx40_Full_setup.exe';
 
var 
  installRequired : Boolean;
  PasswordStr: String;
 
procedure InitializeWizard();
begin
  if not IsDotNetDetected(dotNetVersion, servicePack) then
  begin
    idpAddFile(dotNetWebInstallerURL, ExpandConstant('{tmp}\NetFrameworkInstaller.exe'));    
    idpDownloadAfter(wpReady);
    installRequired := true;
  end
  else 
    installRequired := false;
end;


procedure InstallFramework;
var
  StatusText: string;
  ResultCode: Integer;
begin
  StatusText := WizardForm.StatusLabel.Caption;
  WizardForm.StatusLabel.Caption := 'Installing .NET Framework. This might take a few minutes...';
  WizardForm.ProgressGauge.Style := npbstMarquee;
  try
    if not Exec(ExpandConstant('{tmp}\NetFrameworkInstaller.exe'), '/passive /norestart', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then
    begin
      MsgBox('.NET installation failed with code: ' + IntToStr(ResultCode) + '.', mbError, MB_OK);
    end;
  finally
    WizardForm.StatusLabel.Caption := StatusText;
    WizardForm.ProgressGauge.Style := npbstNormal;
 
    DeleteFile(ExpandConstant('{tmp}\NetFrameworkInstaller.exe'));
  end;
end;

 
procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then 
  begin
    if installRequired then InstallFramework;
  end;
end;

 