@echo OFF
SET SHOGUN_RELEASE_DIR=..\Release
SET SHOGUN_TEMP_DIR= .\ReleaseTempUpd
SET ARCHIVE_NAME=.\ShogunUpdate.exe

REM ///file.list�̍폜(�O�̈�)
REM del file.list

REM ///ReleaseTemp�̍폜(�O�̈�)
REM rd /s /q %SHOGUN_TEMP_DIR%

REM ///ReleaseTemp�̍쐬
mkdir %SHOGUN_TEMP_DIR%

REM echo �f�B���N�g���쐬
REM pause

REM /// Release�t�H���_��ReleaseTemp�t�H���_�ɃR�s�[����
xcopy %SHOGUN_RELEASE_DIR% %SHOGUN_TEMP_DIR% /S /EXCLUDE:.\deny.list

REM echo �R�s�[����
REM pause

REM /// ����̊K�w�ɑ��݂���t�@�C����S��file.list�ɓf���o��
cd %SHOGUN_TEMP_DIR%
FOR /F %%i in ('dir /b') do (echo %cd%\%%i >> ../file.list)

cd ../

REM echo ���X�g�쐬����
REM pause

REM /// ���X�g�L�ڂ̃t�@�C�������k
".\7z\7z.exe" a -sfx7z.sfx %ARCHIVE_NAME% @file.list -scsWIN
REM pause

REM ///file.list�̃N���[���A�b�v
del file.list

REM echo ���X�g�폜����
REM pause

REM ///�ꎞ�t�H���_�̃N���[���A�b�v
rd /s /q %SHOGUN_TEMP_DIR%

REM echo �ꎞ�t�H���_�폜����
REM pause

REM echo ����
REM pause