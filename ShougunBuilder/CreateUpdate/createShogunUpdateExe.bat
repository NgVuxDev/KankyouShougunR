@echo OFF
SET SHOGUN_RELEASE_DIR=..\Release
SET SHOGUN_TEMP_DIR= .\ReleaseTempUpd
SET ARCHIVE_NAME=.\ShogunUpdate.exe

REM ///file.listの削除(念の為)
REM del file.list

REM ///ReleaseTempの削除(念の為)
REM rd /s /q %SHOGUN_TEMP_DIR%

REM ///ReleaseTempの作成
mkdir %SHOGUN_TEMP_DIR%

REM echo ディレクトリ作成
REM pause

REM /// ReleaseフォルダをReleaseTempフォルダにコピーする
xcopy %SHOGUN_RELEASE_DIR% %SHOGUN_TEMP_DIR% /S /EXCLUDE:.\deny.list

REM echo コピー完了
REM pause

REM /// 所定の階層に存在するファイルを全てfile.listに吐き出す
cd %SHOGUN_TEMP_DIR%
FOR /F %%i in ('dir /b') do (echo %cd%\%%i >> ../file.list)

cd ../

REM echo リスト作成完了
REM pause

REM /// リスト記載のファイルを圧縮
".\7z\7z.exe" a -sfx7z.sfx %ARCHIVE_NAME% @file.list -scsWIN
REM pause

REM ///file.listのクリーンアップ
del file.list

REM echo リスト削除完了
REM pause

REM ///一時フォルダのクリーンアップ
rd /s /q %SHOGUN_TEMP_DIR%

REM echo 一時フォルダ削除完了
REM pause

REM echo 完了
REM pause