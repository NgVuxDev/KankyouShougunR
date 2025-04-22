--�d�q
SELECT		--�񍬔p
	R18_EX.SYSTEM_ID											--�V�X�e��ID�@��\��
	,R18_EX.SEQ											        --SEQ�@��\��
	,'' AS DETAIL_SYSTEM_ID										--���׃V�X�e��ID�@��\��
	,4 AS HAIKI_KBN_CD											--�p�����敪CD�@��\��
	,'�d�q' AS HAIKI_KBN_NAME									--�p�����敪���@�\��
	,'0' AS ISKONGOU                                            --�����敪
	,CASE
		WHEN R18.HIKIWATASHI_DATE = '' THEN NULL
		ELSE CONVERT(DATETIME, R18.HIKIWATASHI_DATE) END AS KOUFU_DATE	--��t�N�����@�\��
	,R18.MANIFEST_ID AS MANIFEST_ID						        --��t�ԍ��@�\��
	,(R18.HAIKI_DAI_CODE + R18.HAIKI_CHU_CODE + R18.HAIKI_SHO_CODE) AS HAIKI_SHURUI_CD		--�p�������CD�@��\��
	,CASE R18.HAIKI_SAI_CODE
		WHEN '000' THEN HAIKI_SHU.HAIKI_SHURUI_NAME
		ELSE HAIKI_SAI.HAIKI_SHURUI_NAME END AS HAIKI_SHURUI_NAME	--�p������ޖ��@�\��
	,HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD								--�񍐏�����CD�@��\��
	,HOU_BUN.HOUKOKUSHO_BUNRUI_NAME_RYAKU AS HOUKOKUSHO_BUNRUI_NAME	--�񍐏����ޖ��@�\��
	,R18.HAIKI_SUU AS HAIKI_SUU						                --�p�����̐��ʁ@�\��
	,R18.HAIKI_UNIT_CODE AS HAIKI_UNIT_CD				            --�p�����̐��ʒP�ʃR�[�h�@��\��
	-- ���Z�p���ʂƒP��CD�i�񍬍��d�q�p�A��\���j start�@
	,R18.HAIKI_KAKUTEI_SUU AS HAIKI_KAKUTEI_SUU						--�p�����̊m�萔��
	,R18.HAIKI_KAKUTEI_UNIT_CODE AS HAIKI_KAKUTEI_UNIT_CODE			--�p�����̊m�萔�ʒP�ʃR�[�h�@

	,MEMBER_SU1.EDI_PASSWORD AS SU1_UPN_SHA_EDI_PASSWORD            -- �����ҏ��}�X�^(���W�^��1).EDI_EDI_PASSWORD
	,R19_SU1.UPN_SUU AS SU1_UPN_SUU                                 -- �^����(���W�^��1)
	,R19_SU1.UPN_UNIT_CODE AS SU1_UPN_UNIT_CODE                     -- �^���P�ʃR�[�h(���W�^��1)

	,MEMBER_SU2.EDI_PASSWORD AS SU2_UPN_SHA_EDI_PASSWORD            -- �����ҏ��}�X�^(���W�^��2).EDI_EDI_PASSWORD
	,R19_SU2.UPN_SUU AS SU2_UPN_SUU                                 -- �^����(���W�^��1)
	,R19_SU2.UPN_UNIT_CODE AS SU2_UPN_UNIT_CODE                     -- �^���P�ʃR�[�h(���W�^��2)

	,MEMBER_SU3.EDI_PASSWORD AS SU3_UPN_SHA_EDI_PASSWORD            -- �����ҏ��}�X�^(���W�^��3).EDI_EDI_PASSWORD
	,R19_SU3.UPN_SUU AS SU3_UPN_SUU                                 -- �^����(���W�^��3)
	,R19_SU3.UPN_UNIT_CODE AS SU3_UPN_UNIT_CODE                     -- �^���P�ʃR�[�h(���W�^��3)

	,MEMBER_SU4.EDI_PASSWORD AS SU4_UPN_SHA_EDI_PASSWORD            -- �����ҏ��}�X�^(���W�^��4).EDI_EDI_PASSWORD
	,R19_SU4.UPN_SUU AS SU4_UPN_SUU                                 -- �^����(���W�^��4)
	,R19_SU4.UPN_UNIT_CODE AS SU4_UPN_UNIT_CODE                     -- �^���P�ʃR�[�h(���W�^��4)

	,MEMBER_SU5.EDI_PASSWORD AS SU5_UPN_SHA_EDI_PASSWORD            -- �����ҏ��}�X�^(���W�^��5).EDI_EDI_PASSWORD
	,R19_SU5.UPN_SUU AS SU5_UPN_SUU                                 -- �^����(���W�^��5)
	,R19_SU5.UPN_UNIT_CODE AS SU5_UPN_UNIT_CODE                     -- �^���P�ʃR�[�h(���W�^��5)

	,R18.RECEPT_SUU AS RECEPT_SUU                                   -- ������
	,R18.RECEPT_UNIT_CODE AS RECEPT_UNIT_CODE                       -- �����P�ʃR�[�h
	-- ���Z�p���ʂƒP��CD end
	,HAIKI_UNIT.UNIT_NAME_RYAKU AS HAIKI_UNIT_NAME					--�p�����̐��ʒP�ʖ��@�\��
	,R18_EX.KANSAN_SUU AS OLD_KANSAN_SUU					    --���Z�㐔��(�ύX�O)�@�\��
	,R18_EX.GENNYOU_SUU AS OLD_GENNYOU_SUU					    --���e�㐔��(�ύX�O)�@�\��
	,R18_EX.HST_GYOUSHA_CD										--�r�o���Ǝ�CD�@��\��
	,HST_GYOUSHA.GYOUSHA_NAME_RYAKU AS HST_GYOUSHA_NAME			--�r�o���ƎҖ��@�\��
	,R18_EX.HST_GENBA_CD										--�r�o���Ə�CD�@��\��
	,HST_GENBA.GENBA_NAME_RYAKU AS HST_GENBA_NAME				--�r�o���Əꖼ�@�\��
	,R19_EX.UPN_GYOUSHA_CD										--(���)�^���Ǝ�CD�@��\��
	,UPN_GYOUSHA.GYOUSHA_NAME_RYAKU AS UPN_GYOUSHA_NAME			--(���)�^���ƎҖ��@�\��
	,R18_EX.SBN_GYOUSHA_CD										--�������Ǝ�CD�@��\��
	,SBN_GYOUSHA.GYOUSHA_NAME_RYAKU AS SBN_GYOUSHA_NAME			--�������ƎҖ��@�\��
	,R18_EX.HAIKI_NAME_CD										--�p��������CD�@��\��
	,R18.HAIKI_NAME AS HAIKI_NAME								--�p�������́@�\��
	,R18.NISUGATA_CODE AS NISUGATA_CD							--�׎pCD�@��\��
	,R18.NISUGATA_NAME AS NISUGATA_NAME							--�׎p���@�\��
	,R18_EX.SBN_HOUHOU_CD AS SBN_HOUHOU_CD						--�������@CD�@��\��
	,SBN_HOU.SHOBUN_HOUHOU_NAME_RYAKU AS SHOBUN_HOUHOU_NAME			--�������@���@�\��
    ,R18.SBN_WAY_CODE AS R18_SBN_HOUHOU_CD						--�������@CD�@�d
	,R18_SBN_HOU.SHOBUN_HOUHOU_NAME_RYAKU AS R18_SHOBUN_HOUHOU_NAME  --�������@���@�d

	/*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 1*/
	,CASE
		WHEN R18.HAIKI_KAKUTEI_SUU IS NULL THEN R18.HAIKI_SUU
		ELSE R18.HAIKI_KAKUTEI_SUU END AS DEN_OLD_KANSAN_SUU	--�d�}�j���Z�O�i���[���ʁj�@�\��
	,CASE
		WHEN R18.HAIKI_KAKUTEI_SUU IS NULL THEN HAIKI_UNIT.UNIT_NAME_RYAKU
		ELSE HAIKI_KAKUTEI_UNIT.UNIT_NAME_RYAKU END AS DEN_OLD_KANSAN_UNIT_NAME	--���Z�O�P�ʁ@�\��
	/*END*/

	/*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 2*/
	,R18.HAIKI_SUU AS DEN_OLD_KANSAN_SUU                       --�d�}�j���Z�O�i���[���ʁj�@�\��
	,HAIKI_UNIT.UNIT_NAME_RYAKU AS DEN_OLD_KANSAN_UNIT_NAME          --���Z�O�P�ʁ@�\��
	/*END*/

	/*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 3*/
	,CASE 
	 WHEN R19_SU5.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
	  CASE
	  WHEN R19_SU4.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
		  CASE
		  WHEN R19_SU3.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
			  CASE
		      WHEN R19_SU2.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
				  CASE
		          WHEN R19_SU1.UPN_SHA_EDI_MEMBER_ID IS NULL THEN ''
		          ELSE R19_SU1.UPN_SUU END
		      ELSE R19_SU2.UPN_SUU END
		  ELSE R19_SU3.UPN_SUU END
	   ELSE R19_SU4.UPN_SUU END
	 ELSE R19_SU5.UPN_SUU END AS DEN_OLD_KANSAN_SUU	            --�d�}�j���Z�O�i���[���ʁj�@�\��

	,CASE
	 WHEN R19_SU5.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
		  CASE
		  WHEN R19_SU4.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
			  CASE
		      WHEN R19_SU3.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
				  CASE
		          WHEN R19_SU2.UPN_SHA_EDI_MEMBER_ID IS NULL THEN 
					 CASE
		             WHEN R19_SU1.UPN_SHA_EDI_MEMBER_ID IS NULL THEN ''
		             ELSE SU1_UPN_UNIT.UNIT_NAME_RYAKU END
		          ELSE SU2_UPN_UNIT.UNIT_NAME_RYAKU END
		      ELSE SU3_UPN_UNIT.UNIT_NAME_RYAKU END
		  ELSE SU4_UPN_UNIT.UNIT_NAME_RYAKU END
	 ELSE SU5_UPN_UNIT.UNIT_NAME_RYAKU END AS DEN_OLD_KANSAN_UNIT_NAME	   --���Z�O�P�ʁ@�\��
	/*END*/

	/*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 4*/
	,R18.RECEPT_SUU AS DEN_OLD_KANSAN_SUU                       --�d�}�j���Z�O�i���[���ʁj�@�\��
	,RECEPT_UNIT.UNIT_NAME_RYAKU AS DEN_OLD_KANSAN_UNIT_NAME          --���Z�O�P�ʁ@�\��
	/*END*/

     --�񎟃}�j��t�ԍ�
	, '' AS NEXT_SYSTEM_ID
	, '' AS NEXT_HAIKI_KBN_CD

FROM
	DT_MF_TOC TOC
	INNER JOIN DT_R18 R18
		ON TOC.KANRI_ID = R18.KANRI_ID AND TOC.LATEST_SEQ = R18.SEQ
	INNER JOIN DT_R18_EX R18_EX
		ON R18.KANRI_ID = R18_EX.KANRI_ID AND R18_EX.DELETE_FLG = 0
	INNER JOIN DT_R19 R19
		ON TOC.KANRI_ID = R19.KANRI_ID AND TOC.LATEST_SEQ = R19.SEQ AND R19.UPN_ROUTE_NO = 1
	INNER JOIN DT_R19_EX R19_EX
		ON R19.KANRI_ID = R19_EX.KANRI_ID AND R19_EX.UPN_ROUTE_NO = 1 AND R19_EX.DELETE_FLG = 0

	LEFT JOIN M_DENSHI_HAIKI_SHURUI HAIKI_SHU
		ON (R18.HAIKI_DAI_CODE + R18.HAIKI_CHU_CODE + R18.HAIKI_SHO_CODE) = HAIKI_SHU.HAIKI_SHURUI_CD
	LEFT JOIN M_HOUKOKUSHO_BUNRUI HOU_BUN 
		ON HAIKI_SHU.HOUKOKUSHO_BUNRUI_CD = HOU_BUN.HOUKOKUSHO_BUNRUI_CD
	LEFT JOIN M_DENSHI_HAIKI_SHURUI_SAIBUNRUI HAIKI_SAI
		ON R18.HST_SHA_EDI_MEMBER_ID = HAIKI_SAI.EDI_MEMBER_ID AND (R18.HAIKI_DAI_CODE + R18.HAIKI_CHU_CODE + R18.HAIKI_SHO_CODE + R18.HAIKI_SAI_CODE) = (HAIKI_SAI.HAIKI_SHURUI_CD + HAIKI_SAI.HAIKI_SHURUI_SAIBUNRUI_CD)
	LEFT JOIN M_UNIT HAIKI_UNIT
		ON R18.HAIKI_UNIT_CODE = HAIKI_UNIT.UNIT_CD
	LEFT JOIN M_GYOUSHA HST_GYOUSHA
		ON R18_EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_GENBA HST_GENBA
		ON R18_EX.HST_GYOUSHA_CD = HST_GENBA.GYOUSHA_CD AND R18_EX.HST_GENBA_CD = HST_GENBA.GENBA_CD
	LEFT JOIN M_GYOUSHA UPN_GYOUSHA
		ON R19_EX.UPN_GYOUSHA_CD = UPN_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_GYOUSHA SBN_GYOUSHA
		ON R18_EX.SBN_GYOUSHA_CD = SBN_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_SHOBUN_HOUHOU SBN_HOU
		ON R18_EX.SBN_HOUHOU_CD = SBN_HOU.SHOBUN_HOUHOU_CD
    LEFT JOIN M_SHOBUN_HOUHOU R18_SBN_HOU
	    ON CONVERT(nvarchar(3), R18.SBN_WAY_CODE) = R18_SBN_HOU.SHOBUN_HOUHOU_CD

  --LEFT JOIN ���W�^�����(���W�^��1)
  --  ON �}�j�t�F�X�g���.�Ǘ��ԍ��@���@���W�^�����(���W�^��1).�Ǘ��ԍ�
  --  AND �}�j�t�F�X�g���.�}�ԁ@���@���W�^�����(���W�^��1).�}��
  --  AND ���W�^�����(���W�^��1).��Ԕԍ��@���@1(���1)
  LEFT JOIN DT_R19 R19_SU1 ON R18.KANRI_ID = R19_SU1.KANRI_ID
                          AND R18.SEQ = R19_SU1.SEQ
					      AND R19_SU1.UPN_ROUTE_NO = 1
  --LEFT JOIN �d�q���Ǝ҃}�X�^(���W�^��1)
  --  ON ���W�^�����(���W�^��1).���W�^���Ǝ҉����Ҕԍ��@���@�d�q���Ǝ҃}�X�^(���W�^��1).�����Ҕԍ�
  --  AND �d�q���Ǝ҃}�X�^(���W�^��1).�^���Ǝҋ敪�@���@1(���W�^���Ǝ�)
  LEFT JOIN M_DENSHI_JIGYOUSHA  JIGYOUSHA_SU1 ON R19_SU1.UPN_SHA_EDI_MEMBER_ID = JIGYOUSHA_SU1.EDI_MEMBER_ID
                                             AND JIGYOUSHA_SU1.UPN_KBN = 'True'

  -- LEFT JOIN �����ҏ��}�X�^(���W�^��1)
  LEFT JOIN MS_JWNET_MEMBER MEMBER_SU1 ON JIGYOUSHA_SU1.EDI_MEMBER_ID = MEMBER_SU1.EDI_MEMBER_ID

    --LEFT JOIN ���W�^�����(���W�^��2)
  --  ON �}�j�t�F�X�g���.�Ǘ��ԍ��@���@���W�^�����(���W�^��2).�Ǘ��ԍ�
  --  AND �}�j�t�F�X�g���.�}�ԁ@���@���W�^�����(���W�^��2).�}��
  --  AND ���W�^�����(���W�^��2).��Ԕԍ��@���@2(���2)
  LEFT JOIN DT_R19 R19_SU2 ON R18.KANRI_ID = R19_SU2.KANRI_ID
                          AND R18.SEQ = R19_SU2.SEQ
					      AND R19_SU2.UPN_ROUTE_NO = 2

  --LEFT JOIN �d�q���Ǝ҃}�X�^(���W�^��2)
  --  ON ���W�^�����(���W�^��2).���W�^���Ǝ҉����Ҕԍ��@���@�d�q���Ǝ҃}�X�^(���W�^��2).�����Ҕԍ�
  --  AND �d�q���Ǝ҃}�X�^(���W�^��2).�^���Ǝҋ敪�@���@1(���W�^���Ǝ�)
  LEFT JOIN M_DENSHI_JIGYOUSHA  JIGYOUSHA_SU2 ON R19_SU2.UPN_SHA_EDI_MEMBER_ID = JIGYOUSHA_SU2.EDI_MEMBER_ID
                                             AND JIGYOUSHA_SU2.UPN_KBN = 'True'

  -- LEFT JOIN �����ҏ��}�X�^(���W�^��2)
  LEFT JOIN MS_JWNET_MEMBER MEMBER_SU2 ON JIGYOUSHA_SU2.EDI_MEMBER_ID = MEMBER_SU2.EDI_MEMBER_ID

  --LEFT JOIN ���W�^�����(���W�^��3)
  --  ON �}�j�t�F�X�g���.�Ǘ��ԍ��@���@���W�^�����(���W�^��3).�Ǘ��ԍ�
  --  AND �}�j�t�F�X�g���.�}�ԁ@���@���W�^�����(���W�^��3).�}��
  --  AND ���W�^�����(���W�^��3).��Ԕԍ��@���@3(���3)
  LEFT JOIN DT_R19 R19_SU3 ON R18.KANRI_ID = R19_SU3.KANRI_ID
                          AND R18.SEQ = R19_SU3.SEQ
					      AND R19_SU3.UPN_ROUTE_NO = 3

  --LEFT JOIN �d�q���Ǝ҃}�X�^(���W�^��3)
  --  ON ���W�^�����(���W�^��3).���W�^���Ǝ҉����Ҕԍ��@���@�d�q���Ǝ҃}�X�^(���W�^��3).�����Ҕԍ�
  --  AND �d�q���Ǝ҃}�X�^(���W�^��3).�^���Ǝҋ敪�@���@1(���W�^���Ǝ�)
  LEFT JOIN M_DENSHI_JIGYOUSHA JIGYOUSHA_SU3 ON R19_SU3.UPN_SHA_EDI_MEMBER_ID = JIGYOUSHA_SU3.EDI_MEMBER_ID
                                            AND JIGYOUSHA_SU3.UPN_KBN = 'True'


  -- LEFT JOIN �����ҏ��}�X�^(���W�^��3)
  LEFT JOIN MS_JWNET_MEMBER MEMBER_SU3 ON JIGYOUSHA_SU3.EDI_MEMBER_ID = MEMBER_SU3.EDI_MEMBER_ID

  --LEFT JOIN ���W�^�����(���W�^��4)
  --  ON �}�j�t�F�X�g���.�Ǘ��ԍ��@���@���W�^�����(���W�^��4).�Ǘ��ԍ�
  --  AND �}�j�t�F�X�g���.�}�ԁ@���@���W�^�����(���W�^��4).�}��
  --  AND ���W�^�����(���W�^��4).��Ԕԍ��@���@4(���4)
  LEFT JOIN DT_R19 R19_SU4 ON R18.KANRI_ID = R19_SU4.KANRI_ID
                          AND R18.SEQ = R19_SU4.SEQ
					      AND R19_SU4.UPN_ROUTE_NO = 4

  --LEFT JOIN �d�q���Ǝ҃}�X�^(���W�^��4)
  --  ON ���W�^�����(���W�^��4).���W�^���Ǝ҉����Ҕԍ��@���@�d�q���Ǝ҃}�X�^(���W�^��4).�����Ҕԍ�
  --  AND �d�q���Ǝ҃}�X�^(���W�^��4).�^���Ǝҋ敪�@���@1(���W�^���Ǝ�)
  LEFT JOIN M_DENSHI_JIGYOUSHA JIGYOUSHA_SU4 ON R19_SU4.UPN_SHA_EDI_MEMBER_ID = JIGYOUSHA_SU4.EDI_MEMBER_ID
                                            AND JIGYOUSHA_SU4.UPN_KBN = 'True'

  -- LEFT JOIN �����ҏ��}�X�^(���W�^��4)
  LEFT JOIN MS_JWNET_MEMBER MEMBER_SU4 ON JIGYOUSHA_SU4.EDI_MEMBER_ID = MEMBER_SU4.EDI_MEMBER_ID

  --LEFT JOIN ���W�^�����(���W�^��5)
  --  ON �}�j�t�F�X�g���.�Ǘ��ԍ��@���@���W�^�����(���W�^��5).�Ǘ��ԍ�
  --  AND �}�j�t�F�X�g���.�}�ԁ@���@���W�^�����(���W�^��5).�}��
  --  AND ���W�^�����(���W�^��5).��Ԕԍ��@���@5(���5)
  LEFT JOIN DT_R19 R19_SU5 ON R18.KANRI_ID = R19_SU5.KANRI_ID
                          AND R18.SEQ = R19_SU5.SEQ
					      AND R19_SU5.UPN_ROUTE_NO = 5

  --LEFT JOIN �d�q���Ǝ҃}�X�^(���W�^��5)
  --  ON ���W�^�����(���W�^��5).���W�^���Ǝ҉����Ҕԍ��@���@�d�q���Ǝ҃}�X�^(���W�^��5).�����Ҕԍ�
  --  AND �d�q���Ǝ҃}�X�^(���W�^��5).�^���Ǝҋ敪�@���@1(���W�^���Ǝ�)
  LEFT JOIN M_DENSHI_JIGYOUSHA JIGYOUSHA_SU5 ON R19_SU5.UPN_SHA_EDI_MEMBER_ID = JIGYOUSHA_SU5.EDI_MEMBER_ID
                                            AND JIGYOUSHA_SU5.UPN_KBN = 'True'

  -- LEFT JOIN �����ҏ��}�X�^(���W�^��5)
  LEFT JOIN MS_JWNET_MEMBER MEMBER_SU5 ON JIGYOUSHA_SU5.EDI_MEMBER_ID = MEMBER_SU5.EDI_MEMBER_ID

  /*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 1*/
  LEFT JOIN M_UNIT HAIKI_KAKUTEI_UNIT
	ON R18.HAIKI_KAKUTEI_UNIT_CODE = HAIKI_KAKUTEI_UNIT.UNIT_CD
  /*END*/

  /*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 4*/
  LEFT JOIN M_UNIT RECEPT_UNIT
	ON R18.RECEPT_UNIT_CODE = RECEPT_UNIT.UNIT_CD
  /*END*/

  /*IF searchInfo.MANIFEST_REPORT_SUU_KBN == 3*/
  LEFT JOIN M_UNIT SU1_UPN_UNIT
	ON R19_SU1.UPN_UNIT_CODE = SU1_UPN_UNIT.UNIT_CD
  LEFT JOIN M_UNIT SU2_UPN_UNIT
	ON R19_SU2.UPN_UNIT_CODE = SU2_UPN_UNIT.UNIT_CD
  LEFT JOIN M_UNIT SU3_UPN_UNIT
	ON R19_SU3.UPN_UNIT_CODE = SU3_UPN_UNIT.UNIT_CD
  LEFT JOIN M_UNIT SU4_UPN_UNIT
	ON R19_SU4.UPN_UNIT_CODE = SU4_UPN_UNIT.UNIT_CD
  LEFT JOIN M_UNIT SU5_UPN_UNIT
	ON R19_SU5.UPN_UNIT_CODE = SU5_UPN_UNIT.UNIT_CD
/*END*/
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
	NOT EXISTS 
		(
			SELECT
				R18.KANRI_ID
			FROM
				DT_R18_MIX MIX2
			WHERE
				MIX2.DELETE_FLG = 0
				AND R18.KANRI_ID = MIX2.KANRI_ID
		)
	AND TOC.STATUS_FLAG in (3,4)
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
	AND R18.HAIKI_DAI_CODE = SUBSTRING(/*searchInfo.HAIKI_SHURUI_CD*/,1,2)
	AND R18.HAIKI_CHU_CODE = SUBSTRING(/*searchInfo.HAIKI_SHURUI_CD*/,3,1)
	AND R18.HAIKI_SHO_CODE = SUBSTRING(/*searchInfo.HAIKI_SHURUI_CD*/,4,1)
	/*END*/
	/*IF searchInfo.HAIKI_NAME_CD != ''*/
	AND R18_EX.HAIKI_NAME_CD = /*searchInfo.HAIKI_NAME_CD*/
	/*END*/
    /*IF searchInfo.SBN_HOUHOU_CD != ''*/
    AND R18_EX.SBN_HOUHOU_CD = /*searchInfo.SBN_HOUHOU_CD*/
    /*END*/
    /*IF searchInfo.SHOBUN_CHECK && searchInfo.SBN_HOUHOU_CD == '' */
    AND (R18_EX.SBN_HOUHOU_CD IS NULL OR R18_EX.SBN_HOUHOU_CD = '')
    /*END*/
ORDER BY
	R18.HIKIWATASHI_DATE
	,R18.MANIFEST_ID
