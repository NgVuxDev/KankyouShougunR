@ECHO OFF
SETLOCAL ENABLEDELAYEDEXPANSION


REM /// �o�b�`���s�f�B���N�g���Ɉړ�
CD %~dp0


REM /// ��U��ʂ��N���A
cls


REM /// �ϐ��Z�b�g
REM Debug���o�̓f�B���N�g����
SET DBG_OUT_DIR=Debug
REM Release���o�̓f�B���N�g����
SET REL_OUT_DIR=Release
REM ProjectInfo.xml�z�u�f�B���N�g����
SET PRJ_INFO_DIR=%~dp0
REM ���O�o�͏ꏊ
SET FILEDATE=%date:~-10,4%%date:~-5,2%%date:~-2,2%
SET LOG_TXT=log\Log_%FILEDATE%.txt
REM �����n���ϐ�
SET ARG_STR=
REM �����r���h�t���O
SET FORCE_FLG=0
REM �S����
SET ARGS=%*


REM /// �J�n
ECHO **** %time% ShougunBuilder.bat�J�n...
ECHO.


REM /// �������w�肳��Ă��Ȃ��ꍇ�̓I�v�V�����`�F�b�N���s��Ȃ�
IF DEFINED ARGS (
	REM /// �I�v�V�����`�F�b�N
	FOR %%A IN ( %* ) DO (
		REM /// �I�v�V����[�w���v]�`�F�b�N
		IF /i %%A EQU -h GOTO HELP

		IF /i %%A EQU -g (
			REM /// �I�v�V����[GUIDOption]
			ECHO !ARG_STR! | FIND "%%A" >NUL
			IF ERRORLEVEL 1 (
				REM /// ���YOption������������ɓo�^����Ă��Ȃ��ꍇ�̂ݒǉ�����
				SET ARG_STR=!ARG_STR! %%A
			)
		) ELSE IF /i %%A EQU -f (
			REM /// �I�v�V����[�����r���h]
			SET FORCE_FLG=1
		) ELSE (
			REM /// ������Option
			GOTO OP_ERROR
		)
	)
)


REM /// �����r���h�t���OON�̏ꍇ�͎��s�m�F���s��Ȃ�
IF %FORCE_FLG% EQU 1 GOTO SLN_CREATE


:CREATE_CHECK
REM /// BaseSolution�쐬���s�m�F
SET /p INPUT=BaseSolution�̍쐬���s���܂����H[Y:���s/N:�I��/S:�X�L�b�v]
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
REM /// BaseSolution�쐬�J�n
ECHO **** BaseSolution�쐬�J�n...
IF DEFINED ARG_STR (
	REM /// Option��t�^������ԂŎ��s
	CALL MakeRSolution.exe %DBG_OUT_DIR% %REL_OUT_DIR% %PRJ_INFO_DIR% %ARG_STR%
) ELSE (
	REM /// Option��t�^�������s
	CALL MakeRSolution.exe %DBG_OUT_DIR% %REL_OUT_DIR% %PRJ_INFO_DIR%
)
ECHO **** ...����
ECHO.


REM /// �����r���h�t���OON�̏ꍇ�͎��s�m�F���s��Ȃ�
IF %FORCE_FLG% EQU 1 GOTO BUILD


:BUILD_CHECK
REM /// �r���h���s�m�F
SET /p INPUT=�r���h�����s���܂����H[Y:���s/N:�I��]
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
REM /// VS2010���ϐ��ݒ�
ECHO **** VS2010���ϐ��ݒ蒆...
CALL vsvars32.bat
ECHO **** ...����
ECHO.


REM /// Build�J�n
ECHO **** Build�J�n...
ECHO **** Build[Debug]�J�n...
Devenv /rebuild debug BaseSolution.sln >> %LOG_TXT% 2>&1
ECHO **** ...����
ECHO **** Build[Release]�J�n...
Devenv /rebuild release BaseSolution.sln >> %LOG_TXT% 2>&1
ECHO **** ...����
ECHO **** ...Build����
ECHO.
GOTO END


REM /// �I�v�V�����w��Error
:OP_ERROR
ECHO �����ȃI�v�V�������w�肳��܂���
GOTO HELP


REM /// �w���v
:HELP
ECHO.
ECHO ShougunBuilder.bat [Option1] [Option2]...
ECHO [Option] ��
ECHO    -g    �� �e�v���W�F�N�g��GUID���Đݒ肵�܂��B
ECHO    -f    �� �r���h�˓����Ɋm�F���s���܂���B
ECHO    -h    �� Help��\�����܂��B
ECHO.
GOTO END


REM /// �I��
:END
ECHO **** ...%time% ShougunBuilder.bat�I��
ECHO.


ENDLOCAL
