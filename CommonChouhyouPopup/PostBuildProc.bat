echo CommonChouhyouPopup�r���h�|�X�g�v���Z�X
echo C1�̃����^�C�����W���[�����r���h�o�͐�ɃR�s�[���܂��B
echo �ڍׁFCommonChouhyouPopup�̃v���p�e�B�u�r���C�x���g�v
if "%ProgramFiles(x86)%" EQU "" (
    set "srcDir=%ProgramFiles%"
) else (
    set "srcDir=%ProgramFiles(x86)%"
)
if exist "%srcDir%\ComponentOne\C1Reports\v4\C1.C1Report.CustomFields.4.dll" (
    set "subSrcDir1=\ComponentOne\C1Reports\v4"
) else (
    set "subSrcDir1=\ComponentOne\Apps\v4"
)
if exist "%srcDir%\ComponentOne\C1Reports\v4\GrapeCity.BarCode.4.dll" (
    set "subSrcDir2=\ComponentOne\C1Reports\v4"
) else (
    set "subSrcDir2=\ComponentOne\Apps\v4"
)
set srcDir1=%srcDir%%subSrcDir1%
set srcDir2=%srcDir%%subSrcDir2%
set dstDir=%1
echo �R�s�[���f�B���N�g��1 = %srcDir1%
echo �R�s�[���f�B���N�g��2 = %srcDir2%
echo �R�s�[��f�B���N�g�� = %dstDir%
copy /Y "%srcDir1%\C1.C1Report.CustomFields.4.dll" %dstDir%
copy /Y "%srcDir2%\GrapeCity.BarCode.4.dll" %dstDir%
