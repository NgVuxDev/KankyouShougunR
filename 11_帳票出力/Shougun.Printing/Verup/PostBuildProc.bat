echo --------------------------------------------------------------
echo [ PostBuildProc.bat ]
echo ���̃o�b�`�t�@�C����Shougun.Printing.VerUp�v���W�F�N�g�̃r���h�|�X�g�v���Z�X�Ŏ��s����܂��B
echo �o�b�`������TortoiseSVN��SubWCRev�R�}���h���Ăяo���܂��B
echo Shougun.Printing.Revision.txt��Shougun.Printing�f�B���N�g���̃��r�W�����ԍ��𖄂ߍ��݃r���h�o�̓p�X�ɃR�s�[���܂��B
echo Shougun.Printing.Revision.txt�̓N���E�h/�N���C�A���g�����PG�̃o�[�W�����A�b�v�̔���ɗ��p����܂��B

set WorkingCopyPath=%~dp0..
set SrcVersionFile=%~dp0Shougun.Printing.Revision.txt
set DstVersionFile=%1Shougun.Printing.Revision.txt

echo;
echo WorkingCopyPath = %WorkingCopyPath%
echo SrcVersionFile  = %SrcVersionFile%
echo DstVersionFile  = %DstVersionFile%
echo;

SubWCRev %WorkingCopyPath% %SrcVersionFile% %DstVersionFile%

echo;
echo ����(%DstVersionFile%)
type %DstVersionFile%
echo;
echo --------------------------------------------------------------
