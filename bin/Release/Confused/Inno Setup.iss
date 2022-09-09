[Setup]
AppName=UGN Client
AppVersion=1.3.8.4
DefaultDirName={pf}\UGN Client
; Since no icons will be created in "{group}", we don't need the wizard
; to ask for a Start Menu folder name:
SetupIconFile=ugn_logo.ico
UninstallDisplayIcon={app}\UGN Client.exe
OutputDir=Inno Setup Output
OutputBaseFilename=UGN Client [1.3.8.4] Installer
UninstallDisplayName=UGN Client
PrivilegesRequired=poweruser
;WizardImageFile=ugn_logo.bmp
;WizardImageStretch=no
;WizardImageBackColor=$f8f8f8
;WizardSmallImageFile=ugn_logo_small.bmp

DisableWelcomePage=yes
DisableDirPage=yes
DisableProgramGroupPage=yes
DisableReadyMemo=yes
DisableReadyPage=yes
DisableStartupPrompt=yes
DisableFinishedPage=yes

[Code]   

const
  WM_CLOSE = $0010;
  WM_KEYDOWN = $0100;
  WM_KEYUP = $0101;
  VK_RETURN = 13;

procedure InitializeWizard();

begin
  WizardForm.BorderStyle := bsNone;
  WizardForm.Width := 0;
  WizardForm.Height := 0;

  // Pressing the default "Install" button to continue the silent install
  PostMessage(WizardForm.Handle, WM_KEYDOWN, VK_RETURN, 0);
  PostMessage(WizardForm.Handle, WM_KEYUP, VK_RETURN, 0);
end;

[InstallDelete]
; using a dangerous wildcard here to delete all files, change this later to only specific files
Type: files; Name: "{app}\*.*"

[Files]
Source: "UGN Client.exe"; DestDir: "{app}";

[Icons]
Name: "{commonprograms}\UGN Client"; Filename: "{app}\UGN Client.exe"
Name: "{commondesktop}\UGN Client"; Filename: "{app}\UGN Client.exe"

[CustomMessages]
AppName=UGN Client
LaunchProgram=Launch UGN Client

[Run]
Filename: {app}\UGN Client.exe; Description: {cm:LaunchProgram,{cm:AppName}}; Flags: nowait postinstall skipifsilent shellexec