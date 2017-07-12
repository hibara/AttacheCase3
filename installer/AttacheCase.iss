#define MyAppVer GetFileVersion("bin\AttacheCase.exe")
#define MyAppVerNum StringChange(MyAppVer, ".", "")

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"
Name: "jp"; MessagesFile: "compiler:Languages\Japanese.isl"

[CustomMessages]
en.AppName=AttacheCase#3
jp.AppName=�A�^�b�V�F�P�[�X#3
en.AppComments=File and folder encryption software
jp.AppComments=�t�@�C���E�t�H���_�[�Í����\�t�g�E�F�A
en.SetUpProgramDescription=Set up program for 'AttacheCase#3'
jp.SetUpProgramDescription=�u�A�^�b�V�F�P�[�X#3�v�Z�b�g�A�b�v�v���O���� 
en.UnintallName=Uninstall
jp.UnintallName=�A���C���X�g�[��
en.MsgFailedToInstallDotNetFramework=Failed to install .NET Framework 4.0.%nPlease install the .NET Framework 4.0 such as from Windows update.%nAnd then please start this setup program again.
jp.MsgFailedToInstallDotNetFramework=.NET Framework 4.0 �̃C���X�g�[���Ɏ��s�����悤�ł��B%nWindows�A�b�v�f�[�g�Ȃǂ��� .NET Framework���C���X�g�[�����āA%n�Z�b�g�A�b�v�v���O�������ēx�N�����Ă��������B
en.mdSampleFile=help.md
jp.mdSampleFile=help-ja.md
en.LaunchProgram=Launch AttacheCase3 after finishing installation.
jp.LaunchProgram=�C���X�g�[��������ɁA�A�^�b�V�F�P�[�X#3 ���N�����܂��B

[Setup]AppName={cm:AppName}
AppVersion={#MyAppVer}AppVerName={cm:AppName} ver.{#MyAppVer}
DefaultGroupName={cm:AppName}
OutputBaseFilename=atcs{#MyAppVerNum}
DefaultDirName={pf}\AttacheCase3
UsePreviousAppDir=yes
AppendDefaultDirName=yes 
OutputDir=.\archives
TouchTime=00:00
ShowLanguageDialog=yes
UsePreviousLanguage=noSignedUninstaller=yes
SignTool=MySignTool

;-----------------------------------
;�C���X�g�[���v���O����
;-----------------------------------
VersionInfoVersion={#MyAppVer}
;VersionInfoDescription={cm:SetUpProgramDescription}
AppCopyright=Copyright(C) 2016 M.Hibara, All rights reserved.
;SetupIconFile=icon\main_icon.ico
;�E�B�U�[�h�y�[�W�ɕ\�������O���t�B�b�N�i*.bmp: 164 x 314�j
;Graphic in wizard page.
WizardImageFile=bmp\installer_pic_01.bmp
;�E�B�U�[�h�y�[�W�ɕ\�������O���t�B�b�N���g�傳��Ȃ�
;Graphic in wizard page that is not expanded.
WizardImageStretch=no
;���̌��ԐF
;Background color.
WizardImageBackColor=$ffffff 
;�E�B�U�[�h�y�[�W�̉E�㕔���̃O���t�B�b�N�i*.bmp: 55 x 58�j
;Graphic in top-right window of wizard page.
WizardSmallImageFile=bmp\installer_pic_02.bmp
;�i���\��
;Progress.
ShowTasksTreeLines=yes

;------------------------------------------
;�u�v���O�����̒ǉ��ƍ폜�v�_�C�A���O���
;------------------------------------------
;�z�z��
AppPublisher=Mitsuhiro Hibara
;�A�v���P�[�V�����z�z�� Web�T�C�g�� URL
AppPublisherURL=https://hibara.org
;�A����
AppContact=m@hibara.org
;�T�|�[�g�T�C�gURL
AppSupportURL=https://hibara.org/software/
;ReadMe�t�@�C���p�X;AppReadmeFile="{app}\AttacheCase3\readme.txt"
;���i�X�V���URL
AppUpdatesURL=https://hibara.org/software/AttacheCase/
;�A�v���P�[�V�����̐���
AppComments={cm:AppComments}


#include <idp.iss>

[Files]
Source: "bin\AttacheCase.exe"; DestDir: "{app}"; Flags: ignoreversion touch
Source: "bin\AtcSetup.exe"; DestDir: "{app}"; Flags: ignoreversion touch
Source: "bin\Microsoft.WindowsAPICodePack.dll"; DestDir: "{app}"; Flags: ignoreversion touch
Source: "bin\Microsoft.WindowsAPICodePack.Shell.dll"; DestDir: "{app}"; Flags: ignoreversion touch
Source: "bin\ja-JP\AttacheCase.resources.dll"; DestDir: "{app}\ja-JP"; Flags: ignoreversion touch
;Source: "bin\readme.txt"; DestDir: "{userappdata}\AttacheCase3"; Flags: ignoreversion touch

[Icons]
Name: "{group}\AttacheCase"; Filename: "{app}\AttacheCase.exe"; WorkingDir: "{app}"
Name: "{group}\{cm:UnintallName}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{cm:AppName}"; Filename: "{app}\AttacheCase.exe"; WorkingDir: "{app}"; Tasks: desktopicon

[Tasks]
;"�f�X�N�g�b�v��ɃA�C�R�����쐬����(&D)"
;Create a &desktop icon
Name: desktopicon; Description: {cm:CreateDesktopIcon};
;�t�@�C���g���q %2 �� %1 ���֘A�t���܂��B
;&Associate %1 with the %2 file extension
Name: association; Description: {cm:AssocFileExtension,*.atc,AttacheCase};

[Run]
Filename: "{app}\AtcSetup.exe"; Parameters:"-t=0 -p=""{app}\AttacheCase.exe"""; Tasks: association; Flags: postinstall runascurrentuser skipifsilent shellexec
Filename: "{app}\AttacheCase.exe"; Description: {cm:LaunchProgram}; Flags: postinstall skipifsilent shellexec


[UninstallDelete]


[Registry]
;�i�A���C���X�g�[�����Ɂj�֘A�t���ݒ���폜
; Delete association *.md file extension with this application to uninstall.
Root: HKCR; Subkey: "AttacheCase3.DataFile"; Flags: uninsdeletekey
Root: HKCR; Subkey: ".atc"; Flags: uninsdeletekey
;����ݒ���폜
Root: HKCU; Subkey: "Software\Hibara\AttacheCase3"; Flags: uninsdeletekey


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

 