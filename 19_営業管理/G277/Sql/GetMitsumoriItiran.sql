SELECT   
	SYSTEM_ID AS '�V�X�e��ID',
	SEQ AS '�}��',
	MITSUMORI_NUMBER AS '�`�[�ԍ�',
	MITSUMORI_DATE AS '�`�[���t',
	GYOUSHA_NAME AS '�ƎҖ�',
	GENBA_NAME AS '���ꖼ',
	KINGAKU_TOTAL AS '���z�v',
	CASE ZEI_KEISAN_KBN_CD WHEN 1 THEN TAX_SOTO WHEN 2 THEN TAX_SOTO_TOTAL ELSE '' END AS '�O��',
	CASE ZEI_KEISAN_KBN_CD WHEN 1 THEN TAX_UCHI WHEN 2 THEN TAX_UCHI_TOTAL ELSE '' END AS '����',
	CASE ZEI_KEISAN_KBN_CD WHEN 1 THEN KINGAKU_TOTAL - TAX_UCHI WHEN 2 THEN KINGAKU_TOTAL - TAX_UCHI_TOTAL ELSE '' END AS '�o��v',
	GOUKEI_KINGAKU_TOTAL AS '���v���z',
	BIKOU_1  AS '���l',
	UPDATE_DATE AS '�X�V���t',
	MITSUMORI_SHOSHIKI_KBN AS '���Ϗ����敪'
FROM T_MITSUMORI_ENTRY
WHERE DELETE_FLG = 0
/*IF ksjk.Jokyo_flg != null && ksjk.Jokyo_flg != ''*/
AND JOKYO_FLG = /*ksjk.Jokyo_flg*/
/*END*/
/*IF ksjk.Kyoten_cd != null && ksjk.Kyoten_cd != ''*/
 AND KYOTEN_CD = /*ksjk.Kyoten_cd*/ 
/*END*/
/*IF ksjk.Mitsumori_Fdate != null && ksjk.Mitsumori_Fdate != ''*/
 AND CONVERT(nvarchar, MITSUMORI_DATE, 111) >= /*ksjk.Mitsumori_Fdate*/
/*END*/
/*IF ksjk.Mitsumori_Tdate != null && ksjk.Mitsumori_Tdate != ''*/
 AND CONVERT(nvarchar, MITSUMORI_DATE, 111) <= /*ksjk.Mitsumori_Tdate*/
/*END*/
/*IF ksjk.Update_Fdate != null && ksjk.Update_Fdate != ''*/
 AND CONVERT(nvarchar, UPDATE_DATE, 111) >= /*ksjk.Update_Fdate*/
/*END*/
/*IF ksjk.Update_Tdate != null && ksjk.Update_Tdate != ''*/
 AND CONVERT(nvarchar, UPDATE_DATE, 111) <= /*ksjk.Update_Tdate*/
/*END*/
/*IF ksjk.Shain_cd != null && ksjk.Shain_cd != ''*/
 AND SHAIN_CD = /*ksjk.Shain_cd*/
/*END*/
/*IF ksjk.Torihikisaki_cd != null && ksjk.Torihikisaki_cd != ''*/
 AND TORIHIKISAKI_CD = /*ksjk.Torihikisaki_cd*/
/*IF ksjk.Hikiai_torihikisaki_flg != null && ksjk.Hikiai_torihikisaki_flg != ''*/
 AND HIKIAI_TORIHIKISAKI_FLG = /*ksjk.Hikiai_torihikisaki_flg*/
/*END*/
/*END*/
/*IF ksjk.Gyousha_cd != null && ksjk.Gyousha_cd != ''*/
 AND GYOUSHA_CD = /*ksjk.Gyousha_cd*/
/*IF ksjk.Hikiai_gyousha_flg != null && ksjk.Hikiai_gyousha_flg != ''*/
 AND HIKIAI_GYOUSHA_FLG = /*ksjk.Hikiai_gyousha_flg*/
/*END*/
/*END*/
/*IF ksjk.Genba_cd != null && ksjk.Genba_cd != ''*/
 AND GENBA_CD = /*ksjk.Genba_cd*/
/*IF ksjk.Hikiai_genba_flg != null && ksjk.Hikiai_genba_flg != ''*/
 AND HIKIAI_GENBA_FLG = /*ksjk.Hikiai_genba_flg*/
/*END*/
/*END*/
