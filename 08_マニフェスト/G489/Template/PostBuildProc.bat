echo ���̃o�b�`�t�@�C���́u08_�}�j�t�F�X�g\G489\G489.csproj�v�̃r���h��Ɏ����I�Ɏ��s����܂��B
echo G489.csproj�̃v���p�e�B�u�r���h�C�x���g�v���Q�Ƃ��Ă��������B
echo �r���h�o�͐�̃p�X�������Ɏ��܂��B
echo �S�Y�A�E6�c�̔ł̕W���}�j�e���v���[�g���o�͐�ɃR�s�[���܂��B
set copySrc=%~dp0manifest_default\*
set copyDst=%1Template
echo �R�s�[���F%copySrc%
echo �R�s�[��F%copyDst%
copy /Y %copySrc% %copyDst%
