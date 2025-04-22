@echo off

set SHOGUN_RELEASE_DIR=.\Release
set SHOGUN_VER_DIR=..
set SHOGUN_VER_SRC=%SHOGUN_VER_DIR%\KankyouShougunR.version.txt
set SHOGUN_VER_TMP=%SHOGUN_VER_DIR%\KankyouShougunR.version.template.txt
set SHOGUN_VER_CMD=%~dp0\MakeVersionInfoTemplate.exe

echo %SHOGUN_VER_SRC%
echo バージョン情報ファイルを更新します。よろしいですか？
pause

%SHOGUN_VER_CMD% %SHOGUN_VER_SRC% %SHOGUN_VER_TMP%
SubWCRev %SHOGUN_VER_DIR% %SHOGUN_VER_TMP% %SHOGUN_VER_SRC%

del %SHOGUN_VER_TMP%

echo バージョン情報ファイルは更新されました。コミットしてよろしいですか？
pause
TortoiseProc /command:commit /path:%SHOGUN_VER_SRC% /closeonend:0

echo バージョン情報ファイルを出力先フォルダにコピーします。よろしいですか？
pause
copy %SHOGUN_VER_SRC% %SHOGUN_RELEASE_DIR%
rem echo バージョン情報を表示します。よろしいですか？
rem pause
%SHOGUN_RELEASE_DIR%\KankyouShougunR.exe /ver

echo 完了
pause
