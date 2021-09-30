@ECHO OFF

set DOTNETFX4=%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319
set PATH=%PATH%;%DOTNETFX4%

echo Installing SMS HTTP Outgoing Other...
echo ---------------------------------------------------
InstallUtil /i "ITSSMSHTTPOutgoing.exe"
echo ---------------------------------------------------
echo Done.