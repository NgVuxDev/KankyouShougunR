�yShougunBuilder����z
����		�F2014/03/25
�ŏI�X�V	�F2014/04/08


�y�t�@�C���\���z
- ShougunBuilder
	�� log
	��	��ShougunBuilder���s���̃��O���i�[����܂��B
	��
	�� BaseSolution.sln
	��	�ˊJ���p�\�����[�V�����ł��B
	��
	�� CopyFileInfo.xml
	��	�˃r���h���Ă��R�s�[����Ȃ��A���s�ɕK�v�ȃt�@�C�����L�q���Ă��܂��B
	��
	�� DllInfo.xml
	��	�˃��[�J���R�s�[����ė~����DLL�̃p�X�Ɩ��O���L�q���Ă��܂��B
	��
	�� MakeRSolution.exe
	��	��BaseSolution.sln���\�z������s�t�@�C���ł��B
	��	��ShougunBuilder.bat�����p���܂��B���ڎ��s���Ȃ��ł��������B
	��
	�� MakeVersionInfoTemplate.exe
	��	�˃o�[�W�������̍X�V���s���R�}���h�ł��B
	��	��UpdateVersion.bat�����p���܂��B���ڎ��s���Ȃ��ł��������B
	��
	�� ProjectInfo.xml
	��	�˃r���h�ΏۂƂȂ�v���W�F�N�g�̃p�X���L�q���Ă��܂��B
	��
	�� ShougunBuilder.bat
	��	�˃r���h���s�o�b�`�t�@�C���ł��B�r���h���s���ɂ́A����������s���Ă��������B
	��
	�� UpdateVersion.bat
	��	�˃o�[�W���������X�V����o�b�`�t�@�C���ł��B�r���h�̍Ō�Ɏ��s���܂��B
	��
	�� vsvars32.bat
	��	�˃R�}���h���C���r���h���s�����߂ɕK�v�Ȋ��ݒ���s���o�b�`�ł��B
	��	��ShougunBuilder.bat�����p���܂��B���ڎ��s���Ȃ��ł��������B
	��
	�� readme.txt
		�˓��t�@�C���ł��B


�y�r���h���s���@�z
1. ���L���`�F�b�N�A�E�g
�@�@http://developers.e-mall.co.jp/subversion/r-shougun/trunk

�`ShougunBuilder.bat��p����Debug/Release���������ɍ쐬����ꍇ�`
2. r-shougun/trunk/ShougunBuilder��"ShougunBuilder.bat"���G�f�B�^�ŊJ���A���L�̃f�B���N�g���̐ݒ���s��
	��Debug���o�̓f�B���N�g����(DBG_OUT_DIR)
	��Release���o�̓f�B���N�g����(REL_OUT_DIR)
	��ProjectInfo.xml�z�u�f�B���N�g����(PRJ_INFO_DIR)
	���f�t�H���g�͂��ꂼ�ꉺ�L�ɐݒ肳��Ă��܂�
		DBG_OUT_DIR		�FDebug
		REL_OUT_DIR		�FRelease
		PRJ_INFO_DIR	�FShougunBuilder.bat�Ɠ��K�w

3. "ShougunBuilder.bat"���R�}���h�v�����v�g������s
	�˓��K�w��BaseSolution.sln�t�@�C����Debug/Release���s�t�@�C�����쐬����܂��B
	�˃\�����[�V�����t�@�C���쐬�������̓r���h�̓v�����v�g��ŃX�L�b�v���鎖���\�ł��B
	������ShougunBuilder.bat�I�v�V��������
	>> ShougunBuilder.bat [Option1] [Option2]...
		[Option]	��
			-g		�� �e�v���W�F�N�g��GUID���Đݒ肵�܂��B
			-f		�� �r���h�˓����Ɋm�F���s���܂���B
			-h		�� Help��\�����܂��B

�`BaseSolution.sln���r���h�Ώۂ�C�ӂ̂��̂ɂ������ꍇ�`
2. BaseSolution.sln���J��
	�˃X�^�[�g�A�b�v�v���W�F�N�g�����O�C�����(G451)�ɐݒ�
	�˃\�����[�V�����\����Debug(��������Release)�ɕύX
	�˃r���h
	�˓��K�w�ɑI�������\�����[�V�����\���̎��s�t�@�C�����쐬����܂��B

