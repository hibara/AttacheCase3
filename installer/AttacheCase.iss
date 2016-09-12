#define MyAppVer GetFileVersion("bin\AttacheCase.exe")
#define MyAppVerNum StringChange(MyAppVer, ".", "")

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"
Name: "jp"; MessagesFile: "compiler:Languages\Japanese.isl"

[CustomMessages]
en.AppName=AttacheCase#3
jp.AppName=アタッシェケース#3
en.AppComments=File and folder encryption software
jp.AppComments=ファイル・フォルダー暗号化ソフトウェア
en.SetUpProgramDescription=Set up program for 'AttacheCase#3'
jp.SetUpProgramDescription=「アタッシェケース#3」セットアッププログラム 
en.UnintallName=Uninstall
jp.UnintallName=アンインストール
en.MsgFailedToInstallDotNetFramework=Failed to install .NET Framework 4.0.%nPlease install the .NET Framework 4.0 such as from Windows update.%nAnd then please start this setup program again.
jp.MsgFailedToInstallDotNetFramework=.NET Framework 4.0 のインストールに失敗したようです。%nWindowsアップデートなどから .NET Frameworkをインストールして、%nセットアッププログラムを再度起動してください。
en.mdSampleFile=help.md
jp.mdSampleFile=help-ja.md
en.LaunchProgram=Start AttacheCase3 after finishing installation.
jp.LaunchProgram=インストール完了後に、アタッシェケース#3 を起動します。

[Setup]
AppName={cm:AppName}
AppVersion={#MyAppVer}AppVerName={cm:AppName} ver.{#MyAppVer}
DefaultGroupName={cm:AppName}
OutputBaseFilename=atcs{#MyAppVerNum}
DefaultDirName={pf}\AttacheCase3
UsePreviousAppDir=yes
AppendDefaultDirName=yes 
OutputDir=.\archives
TouchTime=00:00
ShowLanguageDialog=yes
UsePreviousLanguage=no

;-----------------------------------
;インストーラプログラム
;-----------------------------------
VersionInfoVersion={#MyAppVer}
;VersionInfoDescription={cm:SetUpProgramDescription}
AppCopyright=Copyright(C) 2016 M.Hibara, All rights reserved.
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
;ReadMeファイルパス;AppReadmeFile="{app}\AttacheCase3\readme.txt"
;製品更新先のURL
AppUpdatesURL=https://hibara.org/software/AttacheCase/
;アプリケーションの説明
AppComments={cm:AppComments}

[Files]
Source: "bin\AttacheCase.exe"; DestDir: "{app}"; Flags: ignoreversion touch
Source: "bin\AtcSetup.exe"; DestDir: "{app}"; Flags: ignoreversion touch
Source: "bin\ja-JP\AttacheCase.resources.dll"; DestDir: "{app}\ja-JP"; Flags: ignoreversion touch
;Source: "bin\readme.txt"; DestDir: "{userappdata}\AttacheCase3"; Flags: ignoreversion touch

[Icons]
Name: "{group}\AttacheCase"; Filename: "{app}\AttacheCase.exe"; WorkingDir: "{app}"
Name: "{group}\{cm:UnintallName}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{cm:AppName}"; Filename: "{app}\AttacheCase.exe"; WorkingDir: "{app}"; Tasks: desktopicon

[Tasks]
;"デスクトップ上にアイコンを作成する(&D)"
;Create a &desktop icon
Name: desktopicon; Description: {cm:CreateDesktopIcon};
;ファイル拡張子 %2 に %1 を関連付けます。
;&Associate %1 with the %2 file extension
Name: association; Description: {cm:AssocFileExtension,*.atc,AttacheCase};

[Run]
Filename: "{app}\AtcSetup.exe"; Parameters:"-t=0 -p=""{app}\AttacheCase.exe"""; Tasks: association; Flags: nowait skipifsilent runascurrentuser
Filename: "{app}\AttacheCase.exe"; Description: {cm:LaunchProgram}; Flags: nowait postinstall skipifsilent

[UninstallDelete]


[Registry]
;（アンインストール時に）関連付け設定を削除
; Delete association *.md file extension with this application to uninstall.
Root: HKCR; Subkey: "AttacheCase3.DataFile"; Flags: uninsdeletekey
Root: HKCR; Subkey: ".atc"; Flags: uninsdeletekey

[Code]
function IsDotNetDetected(version: string; service: cardinal): boolean;
// Indicates whether the specified version and service pack of the .NET Framework is installed.
//
// version -- Specify one of these strings for the required .NET Framework version:
//    'v1.1.4322'     .NET Framework 1.1
//    'v2.0.50727'    .NET Framework 2.0
//    'v3.0'          .NET Framework 3.0
//    'v3.5'          .NET Framework 3.5
//    'v4\Client'     .NET Framework 4.0 Client Profile
//    'v4\Full'       .NET Framework 4.0 Full Installation
//
// service -- Specify any non-negative integer for the required service pack level:
//    0               No service packs required
//    1, 2, etc.      Service pack 1, 2, etc. required
var
  key: string;
  install, serviceCount: cardinal;
  success: boolean;
begin
  key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\' + version;
  // .NET 3.0 uses value InstallSuccess in subkey Setup
  if Pos('v3.0', version) = 1 then begin
    success := RegQueryDWordValue(HKLM, key + '\Setup', 'InstallSuccess', install);
  end else begin
    success := RegQueryDWordValue(HKLM, key, 'Install', install);
  end;
  // .NET 4.0 uses value Servicing instead of SP
  if Pos('v4', version) = 1 then begin
    success := success and RegQueryDWordValue(HKLM, key, 'Servicing', serviceCount);
  end else begin
    success := success and RegQueryDWordValue(HKLM, key, 'SP', serviceCount);
  end;
  result := success and (install = 1) and (serviceCount >= service);
end;

function InitializeSetup(): Boolean;
begin
  if not IsDotNetDetected('v4\Full', 0) then begin
    MsgBox('{cm:MsgFailedToInstallDotNetFramework}', mbInformation, MB_OK);
    result := false;
  end else
    result := true;
end;

