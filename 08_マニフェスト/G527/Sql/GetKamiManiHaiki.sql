-- �}�j�t�F�X�g���ڕ\ ���}�j�t�F�X�g �p������ޕ�  --

SELECT
	MANI_DTL.HAIKI_SHURUI_CD AS HAIKI_SHURUI_CD
,	M_HIK_SRI.HAIKI_SHURUI_NAME AS HAIKI_SHURUI_NAME
,	SUBSTRING(CONVERT(VARCHAR, MANI_ETY.KOUFU_DATE, 111),1,7) AS KOUFU_YM
,	SUM(MANI_DTL.KANSAN_SUU) AS KANSAN_SUU
FROM      T_MANIFEST_ENTRY AS MANI_ETY 
--�}�j�t�F�X�g����
INNER JOIN
          T_MANIFEST_DETAIL AS MANI_DTL
  ON         MANI_ETY.SYSTEM_ID = MANI_DTL.SYSTEM_ID
  AND        MANI_ETY.SEQ = MANI_DTL.SEQ
  AND        MANI_DTL.KANSAN_SUU IS NOT NULL
  
--�}�j�t�F�X�g���W�^���Q JOIN
INNER JOIN
          (
             SELECT SYSTEM_ID, SEQ
             FROM T_MANIFEST_UPN 
             WHERE   1=1
             /*IF data.HST_UPN_GYOUSHA_CD_START != null && data.HST_UPN_GYOUSHA_CD_START != ''*/ 
             AND        UPN_GYOUSHA_CD >= /*data.HST_UPN_GYOUSHA_CD_START*/           -- ��������:�^�������From�i���͂�����ꍇ�j
             /*END*/

             /*IF data.HST_UPN_GYOUSHA_CD_END != null && data.HST_UPN_GYOUSHA_CD_END != ''*/ 
             AND        UPN_GYOUSHA_CD <= /*data.HST_UPN_GYOUSHA_CD_END*/             -- ��������:�^�������To�i���͂�����ꍇ�j
             /*END*/

             /*IF data.HST_UPN_SAKI_GYOUSHA_CD_START != null && data.HST_UPN_SAKI_GYOUSHA_CD_START != ''*/ 
             AND        UPN_SAKI_GYOUSHA_CD >= /*data.HST_UPN_SAKI_GYOUSHA_CD_START*/    -- ��������:���������From�i���͂�����ꍇ�j
             /*END*/

             /*IF data.HST_UPN_SAKI_GYOUSHA_CD_END != null && data.HST_UPN_SAKI_GYOUSHA_CD_END != ''*/ 
             AND        UPN_SAKI_GYOUSHA_CD <= /*data.HST_UPN_SAKI_GYOUSHA_CD_END*/      -- ��������:���������To�i���͂�����ꍇ�j
             /*END*/
             GROUP BY SYSTEM_ID, SEQ
           )  AS MANI_UPN2
             ON 1 = 1
--�}�j�t�F�X�g���W�^�� JOIN
INNER JOIN
          T_MANIFEST_UPN   AS MANI_UPN
   ON         MANI_UPN2.SYSTEM_ID = MANI_UPN.SYSTEM_ID
   AND        MANI_UPN2.SEQ       = MANI_UPN.SEQ
   AND        MANI_ETY.SYSTEM_ID  = MANI_UPN.SYSTEM_ID
   AND        MANI_ETY.SEQ        = MANI_UPN.SEQ
   AND        MANI_UPN.UPN_ROUTE_NO  = '1'

--�p������ރ}�X�^
INNER JOIN
          M_HAIKI_SHURUI AS M_HIK_SRI
  ON        MANI_ETY.HAIKI_KBN_CD = M_HIK_SRI.HAIKI_KBN_CD
  AND       MANI_DTL.HAIKI_SHURUI_CD = M_HIK_SRI.HAIKI_SHURUI_CD
  AND       M_HIK_SRI.DELETE_FLG = 0
  -- ���s�p�������CD�̓��͂�����ꍇ
  /*IF data.HST_HAIKI_SHURUI_CD1 != null && data.HST_HAIKI_SHURUI_CD1 != ''*/
  AND  MANI_ETY.HAIKI_KBN_CD = 1                                                  -- ��������:���s�Œ�
  AND  MANI_DTL.HAIKI_SHURUI_CD = /*data.HST_HAIKI_SHURUI_CD1*/                   -- ��������:���s�p�������CD�i���͂�����ꍇ�j
  /*END*/

  -- �ϑ֔p�������CD�̓��͂�����ꍇ
  /*IF data.HST_HAIKI_SHURUI_CD2 != null && data.HST_HAIKI_SHURUI_CD2 != ''*/
  AND  MANI_ETY.HAIKI_KBN_CD = 3                                                  -- ��������:�ϑ֌Œ�
  AND  MANI_DTL.HAIKI_SHURUI_CD = /*data.HST_HAIKI_SHURUI_CD2*/                   -- ��������:�ϑ֔p�������CD�i���͂�����ꍇ�j
  /*END*/

  -- ���p�p�������CD�̓��͂�����ꍇ
  /*IF data.HST_HAIKI_SHURUI_CD3 != null && data.HST_HAIKI_SHURUI_CD3 != ''*/
  AND  MANI_ETY.HAIKI_KBN_CD = 2                                                  -- ��������:���p�Œ�
  AND  MANI_DTL.HAIKI_SHURUI_CD = /*data.HST_HAIKI_SHURUI_CD3*/                   -- ��������:���p�p�������CD�i���͂�����ꍇ�j
  /*END*/

--�}�j�t�F�X�g
AND (
     MANI_ETY.KOUFU_DATE IS NOT NULL
     AND  MANI_ETY.KOUFU_DATE <> ''
     AND  MANI_ETY.KOUFU_DATE >= CONVERT (DATETIME, /*data.DATE_START*/, 120)   -- �N����From
     AND  MANI_ETY.KOUFU_DATE <= CONVERT (DATETIME, /*data.DATE_END*/, 120)     -- �N����To
    )
AND MANI_ETY.FIRST_MANIFEST_KBN = CONVERT(bit,(CONVERT(int,/*data.FIRST_MANIFEST_KBN*/)-1))                   -- ��������:�}�j�t�F�X�g�敪

/*IF data.KYOTEN_CD != null && data.KYOTEN_CD != ''*/
AND MANI_ETY.KYOTEN_CD = /*data.KYOTEN_CD*/                                     -- ��������:���_CD �i���͂�����ꍇ�j
/*END*/

/*IF data.HST_GYOUSHA_CD_START != null && data.HST_GYOUSHA_CD_START != ''*/
AND  MANI_ETY.HST_GYOUSHA_CD >= /*data.HST_GYOUSHA_CD_START*/                   -- ��������:�r�o���Ǝ�From�i���͂�����ꍇ�j
/*END*/

/*IF data.HST_GYOUSHA_CD_END != null && data.HST_GYOUSHA_CD_END != ''*/
AND  MANI_ETY.HST_GYOUSHA_CD <= /*data.HST_GYOUSHA_CD_END*/                     -- ��������:�r�o���Ǝ�To�i���͂�����ꍇ�j
/*END*/

/*IF data.HST_GENBA_CD_START != null && data.HST_GENBA_CD_START != ''*/
AND  MANI_ETY.HST_GENBA_CD >= /*data.HST_GENBA_CD_START*/                       -- ��������:�r�o���Ə�From�i���͂�����ꍇ�j
/*END*/

/*IF data.HST_GENBA_CD_END != null && data.HST_GENBA_CD_END != ''*/
AND  MANI_ETY.HST_GENBA_CD <= /*data.HST_GENBA_CD_END*/                         -- ��������:�r�o���Ə�To�i���͂�����ꍇ�j
/*END*/

/*IF data.HST_LAST_SBN_GENBA_CD_START != null && data.HST_LAST_SBN_GENBA_CD_START != ''*/
AND  MANI_ETY.LAST_SBN_GENBA_CD >= /*data.HST_LAST_SBN_GENBA_CD_START*/         -- ��������:�ŏI������CDFrom�i���͂�����ꍇ�j
/*END*/

/*IF data.HST_LAST_SBN_GENBA_CD_END != null && data.HST_LAST_SBN_GENBA_CD_END != ''*/
AND  MANI_ETY.LAST_SBN_GENBA_CD <= /*data.HST_LAST_SBN_GENBA_CD_END*/           -- ��������:�ŏI������CDTo�i���͂�����ꍇ�j
/*END*/

AND MANI_ETY.DELETE_FLG = 0

GROUP BY     SUBSTRING(CONVERT(VARCHAR, MANI_ETY.KOUFU_DATE, 111),1,7),MANI_DTL.HAIKI_SHURUI_CD, M_HIK_SRI.HAIKI_SHURUI_NAME
ORDER BY     MANI_DTL.HAIKI_SHURUI_CD, SUBSTRING(CONVERT(VARCHAR, MANI_ETY.KOUFU_DATE, 111),1,7)


