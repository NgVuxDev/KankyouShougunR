--�d�q
SELECT		--���p
	R18_EX.SYSTEM_ID											--�V�X�e��ID�@��\��
	,R18_EX.SEQ											        --SEQ�@��\��
	,R18_MIX.DETAIL_SYSTEM_ID									--���׃V�X�e��ID�@��\��
	,4 AS HAIKI_KBN_CD											--�p�����敪CD�@��\��
	,'�d�q' AS HAIKI_KBN_NAME									--�p�����敪���@�\��
	,'1' AS ISKONGOU                                            --�����敪
	,CASE
		WHEN R18.HIKIWATASHI_DATE = '' THEN NULL
		ELSE CONVERT(DATETIME, R18.HIKIWATASHI_DATE) END AS KOUFU_DATE	--��t�N�����@�\��
	,R18.MANIFEST_ID AS MANIFEST_ID						        --��t�ԍ��@�\��
	--,R18_MIX.ROW_NO AS '�s'										--�U���s�ԍ��@�\��
	,(R18_MIX.HAIKI_DAI_CODE + R18_MIX.HAIKI_CHU_CODE + R18_MIX.HAIKI_SHO_CODE) AS HAIKI_SHURUI_CD		--�p�������CD�@��\��
	,CASE R18_MIX.HAIKI_SAI_CODE
		WHEN '000' THEN HAIKI_SHU.HAIKI_SHURUI_NAME
		ELSE HAIKI_SAI.HAIKI_SHURUI_NAME END AS HAIKI_SHURUI_NAME	--�p������ޖ��@�\��
	,HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD									--�񍐏�����CD�@��\��
	,HOU_BUN.HOUKOKUSHO_BUNRUI_NAME_RYAKU AS HOUKOKUSHO_BUNRUI_NAME		--�񍐏����ޖ��@�\��
	,R18_MIX.HAIKI_SUU AS HAIKI_SUU							    --�p�����̐��ʁ@�\��
	,R18_MIX.HAIKI_UNIT_CD										--�p�����̐��ʒP�ʃR�[�h�@��\��
	-- ���Z�p���ʂƒP��CD�i�񍬍��d�q�p�A��\���j start�@
	,'' AS HAIKI_KAKUTEI_SUU						            --�p�����̊m�萔��
	,'' AS HAIKI_KAKUTEI_UNIT_CODE			                    --�p�����̊m�萔�ʒP�ʃR�[�h�@

	,'' AS SU1_UPN_SHA_EDI_PASSWORD                             -- �����ҏ��}�X�^(���W�^��1).EDI_EDI_PASSWORD
	,'' AS SU1_UPN_SUU                                          -- �^����(���W�^��1)
	,'' AS SU1_UPN_UNIT_CODE                                    -- �^���P�ʃR�[�h(���W�^��1)

	,'' AS SU2_UPN_SHA_EDI_PASSWORD                             -- �����ҏ��}�X�^(���W�^��2).EDI_EDI_PASSWORD
	,'' AS SU2_UPN_SUU                                          -- �^����(���W�^��1)
	,'' AS SU2_UPN_UNIT_CODE                                    -- �^���P�ʃR�[�h(���W�^��2)

	,'' AS SU3_UPN_SHA_EDI_PASSWORD                             -- �����ҏ��}�X�^(���W�^��3).EDI_EDI_PASSWORD
	,'' AS SU3_UPN_SUU                                          -- �^����(���W�^��3)
	,'' AS SU3_UPN_UNIT_CODE                                    -- �^���P�ʃR�[�h(���W�^��3)

	,'' AS SU4_UPN_SHA_EDI_PASSWORD                             -- �����ҏ��}�X�^(���W�^��4).EDI_EDI_PASSWORD
	,'' AS SU4_UPN_SUU                                          -- �^����(���W�^��4)
	,'' AS SU4_UPN_UNIT_CODE                                    -- �^���P�ʃR�[�h(���W�^��4)

	,'' AS SU5_UPN_SHA_EDI_PASSWORD                             -- �����ҏ��}�X�^(���W�^��5).EDI_EDI_PASSWORD
	,'' AS SU5_UPN_SUU                                          -- �^����(���W�^��5)
	,'' AS SU5_UPN_UNIT_CODE                                    -- �^���P�ʃR�[�h(���W�^��5)

	,'' AS RECEPT_SUU                                           -- ������
	,'' AS RECEPT_UNIT_CODE                                     -- �����P�ʃR�[�h
	-- ���Z�p���ʂƒP��CD end
	,HAIKI_UNIT.UNIT_NAME_RYAKU AS HAIKI_UNIT_NAME				--�p�����̐��ʒP�ʖ��@�\��
	,R18_MIX.KANSAN_SUU AS OLD_KANSAN_SUU					    --���Z�㐔��(�ύX�O)�@�\��
	,R18_MIX.GENNYOU_SUU AS OLD_GENNYOU_SUU				        --���e�㐔��(�ύX�O)�@�\��
	,R18_EX.HST_GYOUSHA_CD										--�r�o���Ǝ�CD�@��\��
	,HST_GYOUSHA.GYOUSHA_NAME1 AS HST_GYOUSHA_NAME				--�r�o���ƎҖ��@�\��
	,R18_EX.HST_GENBA_CD										--�r�o���Ə�CD�@��\��
	,HST_GENBA.GENBA_NAME1 AS HST_GENBA_NAME					--�r�o���Əꖼ�@�\��
	,R19_EX.UPN_GYOUSHA_CD										--(���)�^���Ǝ�CD�@��\��
	,UPN_GYOUSHA.GYOUSHA_NAME1 AS UPN_GYOUSHA_NAME			    --(���)�^���ƎҖ��@�\��
	,R18_EX.SBN_GYOUSHA_CD										--�������Ǝ�CD�@��\��
	,SBN_GYOUSHA.GYOUSHA_NAME1 AS SBN_GYOUSHA_NAME				--�������ƎҖ��@�\��
	,R18_MIX.HAIKI_NAME_CD										--�p��������CD�@��\��
	,HAIKI_NAME.HAIKI_NAME AS HAIKI_NAME								--�p�������́@�\��
	,R18.NISUGATA_CODE AS NISUGATA_CD							--�׎pCD�@��\��
	,R18.NISUGATA_NAME AS NISUGATA_NAME							--�׎p���@�\��
	,R18_MIX.SBN_HOUHOU_CD										--�������@CD�@��\��
	,SBN_HOU.SHOBUN_HOUHOU_NAME_RYAKU AS SHOBUN_HOUHOU_NAME			--�������@���@�\��
    ,R18.SBN_WAY_CODE AS R18_SBN_HOUHOU_CD						--�������@CD�@�d
	,R18_SBN_HOU.SHOBUN_HOUHOU_NAME_RYAKU AS R18_SHOBUN_HOUHOU_NAME  --�������@���@�d

    ,'' AS DEN_OLD_KANSAN_SUU                                       --�d�}�j���Z�O�i���[���ʁj�@�\��
    ,'' AS DEN_OLD_KANSAN_UNIT_NAME                                       --���Z�O�P�ʁ@�\��
    --�񎟃}�j��t�ԍ�
	, '' AS NEXT_SYSTEM_ID
	, '' AS NEXT_HAIKI_KBN_CD

FROM
	DT_MF_TOC TOC
	INNER JOIN DT_R18 R18 
		ON TOC.KANRI_ID = R18.KANRI_ID AND TOC.LATEST_SEQ = R18.SEQ
	INNER JOIN DT_R18_MIX R18_MIX
		ON R18.KANRI_ID = R18_MIX.KANRI_ID AND R18_MIX.DELETE_FLG = 0
	INNER JOIN DT_R18_EX R18_EX 
		ON R18.KANRI_ID = R18_EX.KANRI_ID AND R18_EX.DELETE_FLG = 0
	INNER JOIN DT_R19 R19 
		ON TOC.KANRI_ID = R19.KANRI_ID AND TOC.LATEST_SEQ = R19.SEQ AND R19.UPN_ROUTE_NO = 1
	INNER JOIN DT_R19_EX R19_EX 
		ON R19.KANRI_ID = R19_EX.KANRI_ID AND R19_EX.UPN_ROUTE_NO = 1 AND R19_EX.DELETE_FLG = 0

	LEFT JOIN M_DENSHI_HAIKI_SHURUI HAIKI_SHU
		ON (R18_MIX.HAIKI_DAI_CODE + R18_MIX.HAIKI_CHU_CODE + R18_MIX.HAIKI_SHO_CODE) = HAIKI_SHU.HAIKI_SHURUI_CD
	LEFT JOIN M_DENSHI_HAIKI_SHURUI_SAIBUNRUI HAIKI_SAI
		ON R18.HST_SHA_EDI_MEMBER_ID = HAIKI_SAI.EDI_MEMBER_ID AND (R18_MIX.HAIKI_DAI_CODE + R18_MIX.HAIKI_CHU_CODE + R18_MIX.HAIKI_SHO_CODE + R18_MIX.HAIKI_SAI_CODE) = (HAIKI_SAI.HAIKI_SHURUI_CD + HAIKI_SAI.HAIKI_SHURUI_SAIBUNRUI_CD)
	LEFT JOIN M_HOUKOKUSHO_BUNRUI HOU_BUN
		ON HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD = HOU_BUN.HOUKOKUSHO_BUNRUI_CD
	LEFT JOIN M_UNIT HAIKI_UNIT
		ON R18_MIX.HAIKI_UNIT_CD = HAIKI_UNIT.UNIT_CD
	LEFT JOIN M_GYOUSHA HST_GYOUSHA 
		ON R18_EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_GENBA HST_GENBA 
		ON R18_EX.HST_GYOUSHA_CD = HST_GENBA.GYOUSHA_CD AND R18_EX.HST_GENBA_CD = HST_GENBA.GENBA_CD
	LEFT JOIN M_GYOUSHA UPN_GYOUSHA 
		ON R19_EX.UPN_GYOUSHA_CD = UPN_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_GYOUSHA SBN_GYOUSHA 
		ON R18_EX.SBN_GYOUSHA_CD = SBN_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_SHOBUN_HOUHOU SBN_HOU
		ON R18_MIX.SBN_HOUHOU_CD = SBN_HOU.SHOBUN_HOUHOU_CD
    LEFT JOIN M_DENSHI_HAIKI_NAME AS HAIKI_NAME
        ON R18.HST_SHA_EDI_MEMBER_ID = HAIKI_NAME.EDI_MEMBER_ID
        AND R18_MIX.HAIKI_NAME_CD = HAIKI_NAME.HAIKI_NAME_CD
    LEFT JOIN M_SHOBUN_HOUHOU R18_SBN_HOU
	    ON CONVERT(nvarchar(3), R18.SBN_WAY_CODE) = R18_SBN_HOU.SHOBUN_HOUHOU_CD
	/*IF SearchInfo.DATE_KBN == 2*/
	INNER JOIN ( 
		SELECT DISTINCT KANRI_ID, SEQ 
		  FROM DT_R19
		 WHERE  
			UPN_END_DATE >= /*SearchInfo.DATE_FR*/ 
			AND UPN_END_DATE <= /*SearchInfo.DATE_TO*/
	) AS UNPAN ON TOC.KANRI_ID = UNPAN.KANRI_ID AND TOC.LATEST_SEQ = UNPAN.SEQ 
	/*END*/
WHERE
	TOC.STATUS_FLAG in (3,4)
	AND R18.CANCEL_FLAG = 0
	/*IF searchInfo.DATE_KBN == 1 && searchInfo.DATE_FR != ''*/
	AND R18.HIKIWATASHI_DATE >= /*searchInfo.DATE_FR*/
	/*END*/
	/*IF searchInfo.DATE_KBN == 1 && searchInfo.DATE_TO != ''*/
	AND R18.HIKIWATASHI_DATE <= /*searchInfo.DATE_TO*/
	/*END*/	
	/*IF searchInfo.DATE_KBN == 3 && searchInfo.DATE_FR != ''*/
	AND R18.SBN_END_DATE >= /*searchInfo.DATE_FR*/
	/*END*/
	/*IF searchInfo.DATE_KBN == 3 && searchInfo.DATE_TO != ''*/
	AND R18.SBN_END_DATE <= /*searchInfo.DATE_TO*/
	/*END*/
	/*IF searchInfo.DATE_KBN == 4 && searchInfo.DATE_FR != ''*/
	AND R18.LAST_SBN_END_DATE >= /*searchInfo.DATE_FR*/
	/*END*/
	/*IF searchInfo.DATE_KBN == 4 && searchInfo.DATE_TO != ''*/
	AND R18.LAST_SBN_END_DATE <= /*searchInfo.DATE_TO*/
	/*END*/
	/*IF searchInfo.HST_GYOUSHA_CD != ''*/
	AND R18_EX.HST_GYOUSHA_CD = /*searchInfo.HST_GYOUSHA_CD*/
	/*END*/
	/*IF searchInfo.HST_GENBA_CD != ''*/
	AND R18_EX.HST_GENBA_CD = /*searchInfo.HST_GENBA_CD*/
	/*END*/
	/*IF searchInfo.UPN_GYOUSHA_CD != ''*/
	AND R19_EX.UPN_GYOUSHA_CD = /*searchInfo.UPN_GYOUSHA_CD*/
	/*END*/
	/*IF searchInfo.SBN_GYOUSHA_CD != ''*/
	AND R18_EX.SBN_GYOUSHA_CD = /*searchInfo.SBN_GYOUSHA_CD*/
	/*END*/
	/*IF searchInfo.SBN_GENBA_CD != ''*/
	AND R18_EX.SBN_GENBA_CD = /*searchInfo.SBN_GENBA_CD*/
	/*END*/
	/*IF searchInfo.HOUKOKUSHO_BUNRUI_CD != ''*/
	AND HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD = /*searchInfo.HOUKOKUSHO_BUNRUI_CD*/
	/*END*/
	/*IF (searchInfo.HAIKI_SHURUI_CD != '')*/
	AND R18_MIX.HAIKI_DAI_CODE = SUBSTRING(/*searchInfo.HAIKI_SHURUI_CD*/,1,2)
	AND R18_MIX.HAIKI_CHU_CODE = SUBSTRING(/*searchInfo.HAIKI_SHURUI_CD*/,3,1)
	AND R18_MIX.HAIKI_SHO_CODE = SUBSTRING(/*searchInfo.HAIKI_SHURUI_CD*/,4,1)
	/*END*/
	/*IF searchInfo.HAIKI_NAME_CD != ''*/
	AND R18_MIX.HAIKI_NAME_CD = /*searchInfo.HAIKI_NAME_CD*/
	/*END*/
   /*IF searchInfo.SBN_HOUHOU_CD != ''*/
    AND R18_MIX.SBN_HOUHOU_CD = /*searchInfo.SBN_HOUHOU_CD*/
    /*END*/
    /*IF searchInfo.SHOBUN_CHECK && searchInfo.SBN_HOUHOU_CD == '' */
    AND (R18_MIX.SBN_HOUHOU_CD IS NULL OR R18_MIX.SBN_HOUHOU_CD = '')
    /*END*/
ORDER BY
	R18.HIKIWATASHI_DATE
	,R18.MANIFEST_ID
	,R18_MIX.ROW_NO
