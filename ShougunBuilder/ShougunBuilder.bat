@ECHO OFF
SETLOCAL ENABLEDELAYEDEXPANSION


REM /// バッチ実行ディレクトリに移動
CD %~dp0


REM /// 一旦画面をクリア
cls


REM /// 変数セット
REM Debug環境出力ディレクトリ名
SET DBG_OUT_DIR=Debug
REM Release環境出力ディレクトリ名
SET REL_OUT_DIR=Release
REM ProjectInfo.xml配置ディレクトリ名
SET PRJ_INFO_DIR=%~dp0
REM ログ出力場所
SET FILEDATE=%date:~-10,4%%date:~-5,2%%date:~-2,2%
SET LOG_TXT=log\Log_%FILEDATE%.txt
REM 引数渡し変数
SET ARG_STR=
REM 強制ビルドフラグ
SET FORCE_FLG=0
REM 全引数
SET ARGS=%*


REM /// 開始
ECHO **** %time% ShougunBuilder.bat開始...
ECHO.


REM /// 引数が指定されていない場合はオプションチェックを行わない
IF DEFINED ARGS (
	REM /// オプションチェック
	FOR %%A IN ( %* ) DO (
		REM /// オプション[ヘルプ]チェック
		IF /i %%A EQU -h GOTO HELP

		IF /i %%A EQU -g (
			REM /// オプション[GUIDOption]
			ECHO !ARG_STR! | FIND "%%A" >NUL
			IF ERRORLEVEL 1 (
				REM /// 当該Optionが引数文字列に登録されていない場合のみ追加する
				SET ARG_STR=!ARG_STR! %%A
			)
		) ELSE IF /i %%A EQU -f (
			REM /// オプション[強制ビルド]
			SET FORCE_FLG=1
		) ELSE (
			REM /// 無効なOption
			GOTO OP_ERROR
		)
	)
)


REM /// 強制ビルドフラグONの場合は実行確認を行わない
IF %FORCE_FLG% EQU 1 GOTO SLN_CREATE


:CREATE_CHECK
REM /// BaseSolution作成実行確認
SET /p INPUT=BaseSolutionの作成を行いますか？[Y:実行/N:終了/S:スキップ]
IF NOT DEFINED INPUT GOTO CREATE_CHECK
IF /i !INPUT! EQU Y (
	GOTO SLN_CREATE
) ELSE IF /i !INPUT! EQU N (
	ECHO.
	GOTO END
) ELSE IF /i !INPUT! EQU S (
	GOTO BUILD_CHECK
) ELSE (
	GOTO CREATE_CHECK
)


:SLN_CREATE
REM /// BaseSolution作成開始
ECHO **** BaseSolution作成開始...
IF DEFINED ARG_STR (
	REM /// Optionを付与した状態で実行
	CALL MakeRSolution.exe %DBG_OUT_DIR% %REL_OUT_DIR% %PRJ_INFO_DIR% %ARG_STR%
) ELSE (
	REM /// Optionを付与せず実行
	CALL MakeRSolution.exe %DBG_OUT_DIR% %REL_OUT_DIR% %PRJ_INFO_DIR%
)
ECHO **** ...完了
ECHO.


REM /// 強制ビルドフラグONの場合は実行確認を行わない
IF %FORCE_FLG% EQU 1 GOTO BUILD


:BUILD_CHECK
REM /// ビルド実行確認
SET /p INPUT=ビルドを実行しますか？[Y:実行/N:終了]
IF NOT DEFINED INPUT GOTO BUILD_CHECK
IF /i !INPUT! EQU Y (
	GOTO BUILD
) ELSE IF /i !INPUT! EQU N (
	ECHO.
	GOTO END
) ELSE (
	GOTO BUILD_CHECK
)


:BUILD
REM /// VS2010環境変数設定
ECHO **** VS2010環境変数設定中...
CALL vsvars32.bat
ECHO **** ...完了
ECHO.


REM /// Build開始
ECHO **** Build開始...
ECHO **** Build[Debug]開始...
Devenv /rebuild debug BaseSolution.sln >> %LOG_TXT% 2>&1
ECHO **** ...完了
ECHO **** Build[Release]開始...
Devenv /rebuild release BaseSolution.sln >> %LOG_TXT% 2>&1
ECHO **** ...完了
ECHO **** ...Build完了
ECHO.
GOTO END


REM /// オプション指定Error
:OP_ERROR
ECHO 無効なオプションが指定されました
GOTO HELP


REM /// ヘルプ
:HELP
ECHO.
ECHO ShougunBuilder.bat [Option1] [Option2]...
ECHO [Option] │
ECHO    -g    │ 各プロジェクトのGUIDを再設定します。
ECHO    -f    │ ビルド突入時に確認を行いません。
ECHO    -h    │ Helpを表示します。
ECHO.
GOTO END


REM /// 終了
:END
ECHO **** ...%time% ShougunBuilder.bat終了
ECHO.


ENDLOCAL
