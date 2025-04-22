@echo off

set SHOGUN_RELEASE_DIR=.\Release
set SHOGUN_VER_DIR=..
set SHOGUN_VER_SRC=%SHOGUN_VER_DIR%\KankyouShougunR.version.txt
set SHOGUN_VER_TMP=%SHOGUN_VER_DIR%\KankyouShougunR.version.template.txt
set SHOGUN_VER_CMD=%~dp0\MakeVersionInfoTemplate.exe

echo %SHOGUN_VER_SRC%
echo �o�[�W�������t�@�C�����X�V���܂��B��낵���ł����H
pause

%SHOGUN_VER_CMD% %SHOGUN_VER_SRC% %SHOGUN_VER_TMP%
SubWCRev %SHOGUN_VER_DIR% %SHOGUN_VER_TMP% %SHOGUN_VER_SRC%

del %SHOGUN_VER_TMP%

echo �o�[�W�������t�@�C���͍X�V����܂����B�R�~�b�g���Ă�낵���ł����H
pause
TortoiseProc /command:commit /path:%SHOGUN_VER_SRC% /closeonend:0

echo �o�[�W�������t�@�C�����o�͐�t�H���_�ɃR�s�[���܂��B��낵���ł����H
pause
copy %SHOGUN_VER_SRC% %SHOGUN_RELEASE_DIR%
rem echo �o�[�W��������\�����܂��B��낵���ł����H
rem pause
%SHOGUN_RELEASE_DIR%\KankyouShougunR.exe /ver

echo ����
pause
