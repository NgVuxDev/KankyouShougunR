SELECT
  TSD.SEIKYUU_NUMBER                                               --�����ԍ�
  , TSDKE.KAGAMI_NUMBER                                            --�Ӕԍ�
  , TSDKE.ROW_NUMBER                                               --�s�ԍ�
  , TSDKE.DENPYOU_SHURUI_CD                                        --�`�[���CD
  , TSDKE.DENPYOU_SYSTEM_ID                                        --�`�[�V�X�e��ID
  , TSDKE.DENPYOU_SEQ                                              --�`�[�}��
  , TSDKE.DETAIL_SYSTEM_ID                                         --���׃V�X�e��ID
  , TSDKE.DENPYOU_NUMBER                                           --�`�[�ԍ�
  , TSDKE.DENPYOU_DATE                                             --�`�[���t
  , TSDKE.TSDE_TORIHIKISAKI_CD                                     --�����CD
  , TSDKE.TSDE_GYOUSHA_CD                                          --�Ǝ�CD
  , TSDKE.GYOUSHA_NAME1                                            --�ƎҖ�1
  , TSDKE.GYOUSHA_NAME2                                            --�ƎҖ�2
  , TSDKE.TSDE_GENBA_CD                                            --����CD
  , TSDKE.GENBA_NAME1                                              --���ꖼ1
  , TSDKE.GENBA_NAME2                                              --���ꖼ2
  , TSDKE.HINMEI_CD                                                --�i��CD
  , TSDKE.HINMEI_NAME                                              --�i��
  , TSDKE.SUURYOU                                                  --����
  , TSDKE.UNIT_CD                                                  --�P��CD
  , TSDKE.UNIT_NAME                                                --�P�ʖ�
  , TSDKE.TANKA						                               --�P��
  , ISNULL(TSDKE.KINGAKU,0) AS KINGAKU                             --���z
  , ISNULL(TSDKE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU                   --���Ŋz
  , ISNULL(TSDKE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU                   --�O�Ŋz
  , ISNULL(TSDKE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --�`�[���Ŋz
  , ISNULL(TSDKE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --�`�[�O�Ŋz
  , TSDKE.DENPYOU_ZEI_KBN_CD                                       --�`�[�ŋ敪CD
  , TSDKE.MEISAI_ZEI_KBN_CD                                        --���אŋ敪CD
  , TSDKE.MEISAI_BIKOU                                             --���ה��l
  , TSDKE.DENPYOU_ZEI_KEISAN_KBN_CD                                --�`�[�Ōv�Z�敪
  , TSDKE.TSDK_TORIHIKISAKI_CD                                     --�����CD
  , TSDKE.TSDK_GYOUSHA_CD                                          --�Ǝ�CD
  , TSDKE.TSDK_GENBA_CD                                            --����CD
  , TSDKE.DAIHYOU_PRINT_KBN                                        --��\�҈󎚋敪
  , TSDKE.CORP_NAME                                                --��Ж�
  , TSDKE.CORP_DAIHYOU                                             --��\�Җ�
  , TSDKE.KYOTEN_NAME_PRINT_KBN                                    --���_���󎚋敪
  , TSDKE.KYOTEN_CD                                                --���_CD
  , TSDKE.KYOTEN_NAME                                              --���_��
  , TSDKE.KYOTEN_DAIHYOU                                           --���_��\�Җ�
  , TSDKE.KYOTEN_POST                                              --���_�X�֔ԍ�
  , TSDKE.KYOTEN_ADDRESS1                                          --���_�Z��1
  , TSDKE.KYOTEN_ADDRESS2                                          --���_�Z��2
  , TSDKE.KYOTEN_TEL                                               --���_TEL
  , TSDKE.KYOTEN_FAX                                               --���_FAX
  , TSDKE.SEIKYUU_SOUFU_NAME1                                      --���������t��1
  , TSDKE.SEIKYUU_SOUFU_NAME2                                      --���������t��2
  , TSDKE.SEIKYUU_SOUFU_KEISHOU1                                   --���������t��h��1
  , TSDKE.SEIKYUU_SOUFU_KEISHOU2                                   --���������t��h��2
  , TSDKE.SEIKYUU_SOUFU_POST                                       --���������t��X�֔ԍ�
  , TSDKE.SEIKYUU_SOUFU_ADDRESS1                                   --���������t��Z��1
  , TSDKE.SEIKYUU_SOUFU_ADDRESS2                                   --���������t��Z��2
  , TSDKE.SEIKYUU_SOUFU_BUSHO                                      --���������t�敔��
  , TSDKE.SEIKYUU_SOUFU_TANTOU                                     --���������t��S����
  , TSDKE.SEIKYUU_SOUFU_TEL                                        --���������t��TEL
  , TSDKE.SEIKYUU_SOUFU_FAX                                        --���������t��FAX
  , TSDKE.SEIKYUU_TANTOU                                           --�����S����
  , TSDKE.BIKOU_1												  --���l1
  , TSDKE.BIKOU_2												  --���l2
  , ISNULL(TSDKE.KONKAI_URIAGE_GAKU,0) AS TSDK_KONKAI_URIAGE_GAKU            --���񔄏�z
  , ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_SEI_UTIZEI_GAKU    --���񐿓��Ŋz
  , ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_SEI_SOTOZEI_GAKU  --���񐿊O�Ŋz
  , ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) AS TSDK_KONKAI_DEN_UTIZEI_GAKU    --����`���Ŋz
  , ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSDK_KONKAI_DEN_SOTOZEI_GAKU  --����`�O�Ŋz
  , ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_MEI_UTIZEI_GAKU    --���񖾓��Ŋz
  , ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_MEI_SOTOZEI_GAKU  --���񖾊O�Ŋz
  , TSD.KYOTEN_CD AS TSD_KYOTEN_CD											--���_CD
  , TSD.SHIMEBI																--����
  , TSD.TORIHIKISAKI_CD AS TSD_TORIHIKISAKI_CD								--�����CD
  , TSD.SHOSHIKI_KBN														--�����敪
  , TSD.SHOSHIKI_MEISAI_KBN													--�������׋敪
  , TSD.SEIKYUU_KEITAI_KBN													--�����`�ԋ敪
  , TSD.NYUUKIN_MEISAI_KBN													--�������׋敪
  , TSD.YOUSHI_KBN															--�p���敪
  , TSD.SEIKYUU_DATE														--�������t
  , TSD.NYUUKIN_YOTEI_BI													--�����\���
  , ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) AS ZENKAI_KURIKOSI_GAKU              --�O��J�z�z
  , ISNULL(TSD.KONKAI_NYUUKIN_GAKU,0) AS KONKAI_NYUUKIN_GAKU                --��������z
  , ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0) AS KONKAI_CHOUSEI_GAKU                --���񒲐��z
  , ISNULL(TSD.KONKAI_URIAGE_GAKU,0) AS TSD_KONKAI_URIAGE_GAKU              --���񔄏�z
  , ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU,0) AS TSD_KONKAI_SEI_UTIZEI_GAKU      --���񐿓��Ŋz
  , ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_SEI_SOTOZEI_GAKU    --���񐿊O�Ŋz
  , ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU,0) AS TSD_KONKAI_DEN_UTIZEI_GAKU      --����`���Ŋz
  , ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSD_KONKAI_DEN_SOTOZEI_GAKU    --����`�O�Ŋz
  , ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU,0) AS TSD_KONKAI_MEI_UTIZEI_GAKU      --���񖾓��Ŋz
  , ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_MEI_SOTOZEI_GAKU    --���񖾊O�Ŋz
  , ISNULL(TSD.KONKAI_SEIKYU_GAKU,0) AS KONKAI_SEIKYU_GAKU                  --����䐿���z
  , TSD.FURIKOMI_BANK_CD                                          --�U����sCD
  , TSD.FURIKOMI_BANK_NAME                                        --�U����s��
  , TSD.FURIKOMI_BANK_SHITEN_CD                                   --�U����s�x�XCD
  , TSD.FURIKOMI_BANK_SHITEN_NAME                                 --�U����s�x�X��
  , TSD.KOUZA_SHURUI                                              --�������
  , TSD.KOUZA_NO                                                  --�����ԍ�
  , TSD.KOUZA_NAME                                                --�������`
  , TSD.FURIKOMI_BANK_CD_2                                        --�U����sCD2
  , TSD.FURIKOMI_BANK_NAME_2                                      --�U����s��2
  , TSD.FURIKOMI_BANK_SHITEN_CD_2                                 --�U����s�x�XCD2
  , TSD.FURIKOMI_BANK_SHITEN_NAME_2                               --�U����s�x�X��2
  , TSD.KOUZA_SHURUI_2                                            --�������2
  , TSD.KOUZA_NO_2                                                --�����ԍ�2
  , TSD.KOUZA_NAME_2                                              --�������`2
  , TSD.FURIKOMI_BANK_CD_3                                        --�U����sCD3
  , TSD.FURIKOMI_BANK_NAME_3                                      --�U����s��3
  , TSD.FURIKOMI_BANK_SHITEN_CD_3                                 --�U����s�x�XCD3
  , TSD.FURIKOMI_BANK_SHITEN_NAME_3                               --�U����s�x�X��3
  , TSD.KOUZA_SHURUI_3                                            --�������3
  , TSD.KOUZA_NO_3                                                --�����ԍ�3
  , TSD.KOUZA_NAME_3                                              --�������`3
  , TSD.HAKKOU_KBN                                                --���s�敪
  , TSD.SHIME_JIKKOU_NO                                           --�����s�ԍ�
  , (ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) - ISNULL(TSD.KONKAI_NYUUKIN_GAKU,0) - ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0)) AS SASIHIKIGAKU--�����J�z�z
  , (ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0)
		 + ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0)) AS SYOUHIZEIGAKU--����Ŋz
  , (ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) AS MEISEI_SYOHIZEI
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS RANK_DENPYO_1 --�`�[�����N
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS DENPYO_KINGAKU_1 --�`�[���z���v
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS RANK_GENBA_1 --���ꃉ���N
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_UCHIZEI --������ŏ���ō��v
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_SOTOZEI --����O�ŏ���ō��v
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_KINGAKU_1 --������z���v
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS RANK_GYOUSHA_1 --�Ǝ҃����N
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_UCHIZEI --�Ǝғ��ŏ���ō��v
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_SOTOZEI --�ƎҊO�ŏ���ō��v
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_KINGAKU_1 --�Ǝҋ��z���v
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER) AS RANK_SEIKYU_1 --���������N
  , TSD.TOUROKU_NO
  , TSD.INVOICE_KBN
  , TSDKE.KONKAI_KAZEI_KBN_1     --����ېŋ敪�P
  , TSDKE.KONKAI_KAZEI_RATE_1    --����ېŐŗ��P
  , TSDKE.KONKAI_KAZEI_GAKU_1    --����ېŐŔ����z�P
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_1 --����ېŐŊz�P
  , TSDKE.KONKAI_KAZEI_KBN_2     --����ېŋ敪�Q
  , TSDKE.KONKAI_KAZEI_RATE_2    --����ېŐŗ��Q
  , TSDKE.KONKAI_KAZEI_GAKU_2    --����ېŐŔ����z�Q
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_2 --����ېŐŊz�Q
  , TSDKE.KONKAI_KAZEI_KBN_3     --����ېŋ敪�R
  , TSDKE.KONKAI_KAZEI_RATE_3    --����ېŐŗ��R
  , TSDKE.KONKAI_KAZEI_GAKU_3    --����ېŐŔ����z�R
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_3 --����ېŐŊz�R
  , TSDKE.KONKAI_KAZEI_KBN_4     --����ېŋ敪�S
  , TSDKE.KONKAI_KAZEI_RATE_4    --����ېŐŗ��S
  , TSDKE.KONKAI_KAZEI_GAKU_4    --����ېŐŔ����z�S
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_4 --����ېŐŊz�S
  , TSDKE.KONKAI_HIKAZEI_KBN     --�����ېŋ敪
  , TSDKE.KONKAI_HIKAZEI_GAKU    --�����ېŊz
FROM
  T_SEIKYUU_DENPYOU TSD 
  INNER JOIN (
	SELECT
		TSDK.SEIKYUU_NUMBER                                             --�����ԍ�
		, TSDK.KAGAMI_NUMBER                                            --�Ӕԍ�
		, TSDE.ROW_NUMBER                                               --�s�ԍ�
		, TSDE.DENPYOU_SHURUI_CD                                        --�`�[���CD
		, TSDE.DENPYOU_SYSTEM_ID                                        --�`�[�V�X�e��ID
		, TSDE.DENPYOU_SEQ                                              --�`�[�}��
		, TSDE.DETAIL_SYSTEM_ID                                         --���׃V�X�e��ID
		, TSDE.DENPYOU_NUMBER                                           --�`�[�ԍ�
		, TSDE.DENPYOU_DATE                                             --�`�[���t
		, TSDE.TORIHIKISAKI_CD AS TSDE_TORIHIKISAKI_CD                  --�����CD
		, TSDE.GYOUSHA_CD AS TSDE_GYOUSHA_CD                            --�Ǝ�CD
		, TSDE.GYOUSHA_NAME1                                            --�ƎҖ�1
		, TSDE.GYOUSHA_NAME2                                            --�ƎҖ�2
		, TSDE.GENBA_CD AS TSDE_GENBA_CD                                --����CD
		, TSDE.GENBA_NAME1                                              --���ꖼ1
		, TSDE.GENBA_NAME2                                              --���ꖼ2
		, TSDE.HINMEI_CD                                                --�i��CD
		, TSDE.HINMEI_NAME                                              --�i��
		, TSDE.SUURYOU                                                  --����
		, TSDE.UNIT_CD                                                  --�P��CD
		, TSDE.UNIT_NAME                                                --�P�ʖ�
		, TSDE.TANKA					                                --�P��
		, TSDE.KINGAKU                                                  --���z
		, ISNULL(TSDE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU                   --���Ŋz
		, ISNULL(TSDE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU                   --�O�Ŋz
		, ISNULL(TSDE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --�`�[���Ŋz
		, ISNULL(TSDE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --�`�[�O�Ŋz
		, TSDE.DENPYOU_ZEI_KBN_CD                                       --�`�[�ŋ敪CD
		, TSDE.MEISAI_ZEI_KBN_CD                                        --���אŋ敪CD
		, TSDE.MEISAI_BIKOU                                             --���ה��l
		, TSDE.DENPYOU_ZEI_KEISAN_KBN_CD                                --�`�[�Ōv�Z�敪
		, TSDK.TORIHIKISAKI_CD AS TSDK_TORIHIKISAKI_CD                  --�����CD
		, TSDK.GYOUSHA_CD AS TSDK_GYOUSHA_CD                            --�Ǝ�CD
		, TSDK.GENBA_CD AS TSDK_GENBA_CD                                --����CD
		, TSDK.DAIHYOU_PRINT_KBN                                        --��\�҈󎚋敪
		, TSDK.CORP_NAME                                                --��Ж�
		, TSDK.CORP_DAIHYOU                                             --��\�Җ�
		, TSDK.KYOTEN_NAME_PRINT_KBN                                    --���_���󎚋敪
		, TSDK.KYOTEN_CD                                                --���_CD
		, TSDK.KYOTEN_NAME                                              --���_��
		, TSDK.KYOTEN_DAIHYOU                                           --���_��\�Җ�
		, TSDK.KYOTEN_POST                                              --���_�X�֔ԍ�
		, TSDK.KYOTEN_ADDRESS1                                          --���_�Z��1
		, TSDK.KYOTEN_ADDRESS2                                          --���_�Z��2
		, TSDK.KYOTEN_TEL                                               --���_TEL
		, TSDK.KYOTEN_FAX                                               --���_FAX
		, TSDK.SEIKYUU_SOUFU_NAME1                                      --���������t��1
		, TSDK.SEIKYUU_SOUFU_NAME2                                      --���������t��2
		, TSDK.SEIKYUU_SOUFU_KEISHOU1                                   --���������t��h��1
		, TSDK.SEIKYUU_SOUFU_KEISHOU2                                   --���������t��h��2
		, TSDK.SEIKYUU_SOUFU_POST                                       --���������t��X�֔ԍ�
		, TSDK.SEIKYUU_SOUFU_ADDRESS1                                   --���������t��Z��1
		, TSDK.SEIKYUU_SOUFU_ADDRESS2                                   --���������t��Z��2
		, TSDK.SEIKYUU_SOUFU_BUSHO                                      --���������t�敔��
		, TSDK.SEIKYUU_SOUFU_TANTOU                                     --���������t��S����
		, TSDK.SEIKYUU_SOUFU_TEL                                        --���������t��TEL
		, TSDK.SEIKYUU_SOUFU_FAX                                        --���������t��FAX
		, TSDK.SEIKYUU_TANTOU                                           --�����S����
		, ISNULL(TSDK.KONKAI_URIAGE_GAKU,0) AS KONKAI_URIAGE_GAKU            --���񔄏�z
		, ISNULL(TSDK.KONKAI_SEI_UTIZEI_GAKU,0) AS KONKAI_SEI_UTIZEI_GAKU    --���񐿓��Ŋz
		, ISNULL(TSDK.KONKAI_SEI_SOTOZEI_GAKU,0) AS KONKAI_SEI_SOTOZEI_GAKU  --���񐿊O�Ŋz
		, ISNULL(TSDK.KONKAI_DEN_UTIZEI_GAKU,0) AS KONKAI_DEN_UTIZEI_GAKU    --����`���Ŋz
		, ISNULL(TSDK.KONKAI_DEN_SOTOZEI_GAKU,0) AS KONKAI_DEN_SOTOZEI_GAKU  --����`�O�Ŋz
		, ISNULL(TSDK.KONKAI_MEI_UTIZEI_GAKU,0) AS KONKAI_MEI_UTIZEI_GAKU    --���񖾓��Ŋz
		, ISNULL(TSDK.KONKAI_MEI_SOTOZEI_GAKU,0) AS KONKAI_MEI_SOTOZEI_GAKU  --���񖾊O�Ŋz
		, TSDK.BIKOU_1														 --���l1
		, TSDK.BIKOU_2														 --���l2
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_1,0) AS KONKAI_KAZEI_KBN_1            --����ېŋ敪�P
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_1,0) AS KONKAI_KAZEI_RATE_1			 --����ېŐŗ��P
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_1,0) AS KONKAI_KAZEI_GAKU_1			 --����ېŐŔ����z�P
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_1,0) AS KONKAI_KAZEI_ZEIGAKU_1	 --����ېŐŊz�P
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_2,0) AS KONKAI_KAZEI_KBN_2            --����ېŋ敪�Q
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_2,0) AS KONKAI_KAZEI_RATE_2			 --����ېŐŗ��Q
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_2,0) AS KONKAI_KAZEI_GAKU_2			 --����ېŐŔ����z�Q
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_2,0) AS KONKAI_KAZEI_ZEIGAKU_2    --����ېŐŊz�Q
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_3,0) AS KONKAI_KAZEI_KBN_3            --����ېŋ敪�R
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_3,0) AS KONKAI_KAZEI_RATE_3			 --����ېŐŗ��R
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_3,0) AS KONKAI_KAZEI_GAKU_3			 --����ېŐŔ����z�R
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_3,0) AS KONKAI_KAZEI_ZEIGAKU_3    --����ېŐŊz�R
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_4,0) AS KONKAI_KAZEI_KBN_4            --����ېŋ敪�S
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_4,0) AS KONKAI_KAZEI_RATE_4			 --����ېŐŗ��S
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_4,0) AS KONKAI_KAZEI_GAKU_4			 --����ېŐŔ����z�S
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_4,0) AS KONKAI_KAZEI_ZEIGAKU_4    --����ېŐŊz�S
		, ISNULL(TSDK.KONKAI_HIKAZEI_KBN,0) AS KONKAI_HIKAZEI_KBN			 --�����ېŋ敪
		, ISNULL(TSDK.KONKAI_HIKAZEI_GAKU,0) AS KONKAI_HIKAZEI_GAKU			 --�����ېŊz
	FROM
		T_SEIKYUU_DENPYOU_KAGAMI TSDK
		LEFT OUTER JOIN 
        T_SEIKYUU_DETAIL TSDE 
        ON TSDK.SEIKYUU_NUMBER = TSDE.SEIKYUU_NUMBER AND TSDK.KAGAMI_NUMBER = TSDE.KAGAMI_NUMBER
  ) TSDKE 
  ON TSD.SEIKYUU_NUMBER = TSDKE.SEIKYUU_NUMBER
WHERE
  TSD.DELETE_FLG = 0
  AND TSD.SEIKYUU_NUMBER = /*seikyuNumber*/
 ORDER BY
   TSDKE.KAGAMI_NUMBER
   /*$orderBy*/
  , TSDKE.DENPYOU_DATE
  , TSDKE.DENPYOU_SHURUI_CD
  , TSDKE.DENPYOU_NUMBER
  , TSDKE.ROW_NUMBER
  