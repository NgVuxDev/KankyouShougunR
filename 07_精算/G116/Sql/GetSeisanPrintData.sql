SELECT
    TSD.SEISAN_NUMBER                              --���Z�ԍ�
  , TSDKE.KAGAMI_NUMBER                            --�Ӕԍ�
  , TSDKE.ROW_NUMBER                               --�s�ԍ�
  , TSDKE.DENPYOU_SHURUI_CD                        --�`�[���CD
  , TSDKE.DENPYOU_SYSTEM_ID                        --�`�[�V�X�e��ID
  , TSDKE.DENPYOU_SEQ                              --�`�[�}��
  , TSDKE.DETAIL_SYSTEM_ID                         --���׃V�X�e��ID
  , TSDKE.DENPYOU_NUMBER                           --�`�[�ԍ�
  , TSDKE.DENPYOU_DATE                             --�`�[���t
  , TSDKE.TORIHIKISAKI_CD						   --�����CD
  , TSDKE.GYOUSHA_CD                               --�Ǝ�CD
  , TSDKE.GYOUSHA_NAME1                            --�ƎҖ�1
  , TSDKE.GYOUSHA_NAME2                            --�ƎҖ�2
  , TSDKE.GENBA_CD                                 --����CD
  , TSDKE.GENBA_NAME1                              --���ꖼ1
  , TSDKE.GENBA_NAME2                              --���ꖼ2
  , TSDKE.HINMEI_CD                                --�i��CD
  , TSDKE.HINMEI_NAME                              --�i��
  , TSDKE.SUURYOU                                  --����
  , TSDKE.UNIT_CD                                  --�P��CD
  , TSDKE.UNIT_NAME                                --�P�ʖ�
  , TSDKE.TANKA							           --�P��
  , ISNULL(TSDKE.KINGAKU,0) AS KINGAKU             --���z
  , ISNULL(TSDKE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU   --���Ŋz
  , ISNULL(TSDKE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU   --�O�Ŋz
  , ISNULL(TSDKE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --�`�[���Ŋz
  , ISNULL(TSDKE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --�`�[�O�Ŋz
  , TSDKE.DENPYOU_ZEI_KBN_CD                       --�`�[�ŋ敪CD
  , TSDKE.MEISAI_ZEI_KBN_CD                        --���אŋ敪CD
  , TSDKE.MEISAI_BIKOU                             --���ה��l
  , TSDKE.DENPYOU_ZEI_KEISAN_KBN_CD                --�`�[�Ōv�Z�敪
  , TSDKE.DAIHYOU_PRINT_KBN                        --��\�҈󎚋敪
  , TSDKE.CORP_NAME                                --��Ж�
  , TSDKE.CORP_DAIHYOU                             --��\�Җ�
  , TSDKE.KYOTEN_NAME_PRINT_KBN                    --���_���󎚋敪
  , TSDKE.TSDK_KYOTEN_CD                           --���_CD
  , TSDKE.KYOTEN_NAME                              --���_��
  , TSDKE.KYOTEN_DAIHYOU                           --���_��\�Җ�
  , TSDKE.KYOTEN_POST                              --���_�X�֔ԍ�
  , TSDKE.KYOTEN_ADDRESS1                          --���_�Z��1
  , TSDKE.KYOTEN_ADDRESS2                          --���_�Z��2
  , TSDKE.KYOTEN_TEL                               --���_TEL
  , TSDKE.KYOTEN_FAX                               --���_FAX
  , TSDKE.SHIHARAI_SOUFU_NAME1                     --�x�����׏����t��1
  , TSDKE.SHIHARAI_SOUFU_NAME2                     --�x�����׏����t��2
  , TSDKE.SHIHARAI_SOUFU_KEISHOU1                  --�x�����׏����t��h��1
  , TSDKE.SHIHARAI_SOUFU_KEISHOU2                  --�x�����׏����t��h��2
  , TSDKE.SHIHARAI_SOUFU_POST                      --�x�����׏����t��X�֔ԍ�
  , TSDKE.SHIHARAI_SOUFU_ADDRESS1                  --�x�����׏����t��Z��1
  , TSDKE.SHIHARAI_SOUFU_ADDRESS2                  --�x�����׏����t��Z��2
  , TSDKE.SHIHARAI_SOUFU_BUSHO                     --�x�����׏����t�敔��
  , TSDKE.SHIHARAI_SOUFU_TANTOU                    --�x�����׏����t��S����
  , TSDKE.SHIHARAI_SOUFU_TEL                       --�x�����׏����t��TEL
  , TSDKE.SHIHARAI_SOUFU_FAX                       --�x�����׏����t��FAX
  , ISNULL(TSDKE.KONKAI_SHIHARAI_GAKU,0) AS TSDK_KONKAI_SHIHARAI_GAKU        --����x���z
  , ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_SEI_UTIZEI_GAKU    --���񐿓��Ŋz
  , ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_SEI_SOTOZEI_GAKU  --���񐿊O�Ŋz
  , ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) AS TSDK_KONKAI_DEN_UTIZEI_GAKU    --����`���Ŋz
  , ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSDK_KONKAI_DEN_SOTOZEI_GAKU  --����`�O�Ŋz
  , ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_MEI_UTIZEI_GAKU    --���񖾓��Ŋz
  , ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_MEI_SOTOZEI_GAKU  --���񖾊O�Ŋz
  , TSD.KYOTEN_CD                                 --���_CD
  , TSD.SHIMEBI                                   --����
  , TSD.TORIHIKISAKI_CD AS TSD_TORIHIKISAKI_CD    --�����CD
  , TSD.SHOSHIKI_KBN                              --�����敪
  , TSD.SHOSHIKI_MEISAI_KBN                       --�������׋敪
  , TSD.SHOSHIKI_GENBA_KBN						  --�x�����׏�����3
  , TSD.SHIHARAI_KEITAI_KBN                       --�x���`�ԋ敪
  , TSD.SHUKKIN_MEISAI_KBN                        --�������׋敪
  , TSD.YOUSHI_KBN                                --�p���敪
  , TSD.SEISAN_DATE                               --���Z���t
  , TSD.SHUKKIN_YOTEI_BI                          --�o���\���
  , TSDKE.BIKOU_1								  --���l1
  , TSDKE.BIKOU_2								  --���l2
  , ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) AS ZENKAI_KURIKOSI_GAKU              --�O��J�z�z
  , ISNULL(TSD.KONKAI_SHUKKIN_GAKU,0) AS KONKAI_SHUKKIN_GAKU                --����o���z
  , ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0) AS KONKAI_CHOUSEI_GAKU                --���񒲐��z
  , ISNULL(TSD.KONKAI_SHIHARAI_GAKU,0) AS TSD_KONKAI_SHIHARAI_GAKU          --����x���z
  , ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU,0) AS TSD_KONKAI_SEI_UTIZEI_GAKU      --���񐿓��Ŋz
  , ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_SEI_SOTOZEI_GAKU    --���񐿊O�Ŋz
  , ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU,0) AS TSD_KONKAI_DEN_UTIZEI_GAKU      --����`���Ŋz
  , ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSD_KONKAI_DEN_SOTOZEI_GAKU    --����`�O�Ŋz
  , ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU,0) AS TSD_KONKAI_MEI_UTIZEI_GAKU      --���񖾓��Ŋz
  , ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_MEI_SOTOZEI_GAKU    --���񖾊O�Ŋz
  , ISNULL(TSD.KONKAI_SEISAN_GAKU,0) AS KONKAI_SEISAN_GAKU                  --����䐸�Z�z
  , TSD.HAKKOU_KBN                                --���s�敪
  , TSD.SHIME_JIKKOU_NO                           --�����s�ԍ�
  , (ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) - ISNULL(TSD.KONKAI_SHUKKIN_GAKU,0) - ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0)) AS SASIHIKIGAKU --�����J�z�z
  , (ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) 
        + ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0)) AS SYOUHIZEIGAKU --����Ŋz
  , (ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) AS MEISEI_SYOHIZEI
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS RANK_DENPYO_1 --�`�[�����N
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS DENPYO_KINGAKU_1 --�`�[���z���v
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD) AS RANK_GENBA_1 --���ꃉ���N
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD) AS GENBA_UCHIZEI --������ŏ���ō��v
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD) AS GENBA_SOTOZEI --����O�ŏ���ō��v
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD) AS GENBA_KINGAKU_1 --������z���v
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD) AS RANK_GYOUSHA_1 --�Ǝ҃����N
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD) AS GYOUSHA_UCHIZEI --�Ǝғ��ŏ���ō��v
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD) AS GYOUSHA_SOTOZEI --�ƎҊO�ŏ���ō��v
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD) AS GYOUSHA_KINGAKU_1 --�Ǝҋ��z���v
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER) AS RANK_SEISAN_1 --���Z�����N
  , TSD.TOUROKU_NO
  , TSD.INVOICE_KBN
  , ISNULL(TSDKE.KONKAI_KAZEI_KBN_1,0) AS KONKAI_KAZEI_KBN_1            --����ېŋ敪�P
  , ISNULL(TSDKE.KONKAI_KAZEI_RATE_1,0) AS KONKAI_KAZEI_RATE_1          --����ېŗ��P
  , ISNULL(TSDKE.KONKAI_KAZEI_GAKU_1,0) AS KONKAI_KAZEI_GAKU_1          --����ېŐŔ����z�P
  , ISNULL(TSDKE.KONKAI_KAZEI_ZEIGAKU_1,0) AS KONKAI_KAZEI_ZEIGAKU_1    --����ېŐŊz�P
  , ISNULL(TSDKE.KONKAI_KAZEI_KBN_2,0) AS KONKAI_KAZEI_KBN_2            --����ېŋ敪�Q
  , ISNULL(TSDKE.KONKAI_KAZEI_RATE_2,0) AS KONKAI_KAZEI_RATE_2          --����ېŗ��Q
  , ISNULL(TSDKE.KONKAI_KAZEI_GAKU_2,0) AS KONKAI_KAZEI_GAKU_2          --����ېŐŔ����z�Q
  , ISNULL(TSDKE.KONKAI_KAZEI_ZEIGAKU_2,0) AS KONKAI_KAZEI_ZEIGAKU_2    --����ېŐŊz�Q
  , ISNULL(TSDKE.KONKAI_KAZEI_KBN_3,0) AS KONKAI_KAZEI_KBN_3            --����ېŋ敪�R
  , ISNULL(TSDKE.KONKAI_KAZEI_RATE_3,0) AS KONKAI_KAZEI_RATE_3          --����ېŗ��R
  , ISNULL(TSDKE.KONKAI_KAZEI_GAKU_3,0) AS KONKAI_KAZEI_GAKU_3          --����ېŐŔ����z�R
  , ISNULL(TSDKE.KONKAI_KAZEI_ZEIGAKU_3,0) AS KONKAI_KAZEI_ZEIGAKU_3    --����ېŐŊz�R
  , ISNULL(TSDKE.KONKAI_KAZEI_KBN_4,0) AS KONKAI_KAZEI_KBN_4            --����ېŋ敪�S
  , ISNULL(TSDKE.KONKAI_KAZEI_RATE_4,0) AS KONKAI_KAZEI_RATE_4          --����ېŗ��S
  , ISNULL(TSDKE.KONKAI_KAZEI_GAKU_4,0) AS KONKAI_KAZEI_GAKU_4          --����ېŐŔ����z�S
  , ISNULL(TSDKE.KONKAI_KAZEI_ZEIGAKU_4,0) AS KONKAI_KAZEI_ZEIGAKU_4    --����ېŐŊz�S
  , ISNULL(TSDKE.KONKAI_HIKAZEI_KBN,0) AS KONKAI_HIKAZEI_KBN            --�����ېŋ敪
  , ISNULL(TSDKE.KONKAI_HIKAZEI_GAKU,0) AS KONKAI_HIKAZEI_GAKU          --�����ېŊz
  , ISNULL(TSDKE.SHOUHIZEI_RATE,0) AS SHOUHIZEI_RATE                    --����ŗ�
FROM
  T_SEISAN_DENPYOU TSD 
  INNER JOIN (
    SELECT
        TSDK.SEISAN_NUMBER
        , TSDK.KAGAMI_NUMBER                            --�Ӕԍ�
        , TSDE.ROW_NUMBER                               --�s�ԍ�
        , TSDE.DENPYOU_SHURUI_CD                        --�`�[���CD
        , TSDE.DENPYOU_SYSTEM_ID                        --�`�[�V�X�e��ID
        , TSDE.DENPYOU_SEQ                              --�`�[�}��
        , TSDE.DETAIL_SYSTEM_ID                         --���׃V�X�e��ID
        , TSDE.DENPYOU_NUMBER                           --�`�[�ԍ�
        , TSDE.DENPYOU_DATE                             --�`�[���t
        --, TSDE.TORIHIKISAKI_CD						--�����CD
        , TSDE.GYOUSHA_CD                               --�Ǝ�CD
        , TSDE.GYOUSHA_NAME1                            --�ƎҖ�1
        , TSDE.GYOUSHA_NAME2                            --�ƎҖ�2
        , TSDE.GENBA_CD                                 --����CD
        , TSDE.GENBA_NAME1                              --���ꖼ1
        , TSDE.GENBA_NAME2                              --���ꖼ2
        , TSDE.HINMEI_CD                                --�i��CD
        , TSDE.HINMEI_NAME                              --�i��
        , TSDE.SUURYOU                                  --����
        , TSDE.UNIT_CD                                  --�P��CD
        , TSDE.UNIT_NAME                                --�P�ʖ�
        , TSDE.TANKA						            --�P��
        , TSDE.KINGAKU                                  --���z
        , ISNULL(TSDE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU   --���Ŋz
        , ISNULL(TSDE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU   --�O�Ŋz
        , ISNULL(TSDE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --�`�[���Ŋz
        , ISNULL(TSDE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --�`�[�O�Ŋz
        , TSDE.DENPYOU_ZEI_KBN_CD                       --�`�[�ŋ敪CD
        , TSDE.MEISAI_ZEI_KBN_CD                        --���אŋ敪CD
        , TSDE.MEISAI_BIKOU                             --���ה��l
        , TSDE.DENPYOU_ZEI_KEISAN_KBN_CD                --�`�[�Ōv�Z�敪
        , TSDK.TORIHIKISAKI_CD						    --�����CD
        --, TSDK.GYOUSHA_CD                             --�Ǝ�CD
        --, TSDK.GENBA_CD                               --����CD
        , TSDK.DAIHYOU_PRINT_KBN                        --��\�҈󎚋敪
        , TSDK.CORP_NAME                                --��Ж�
        , TSDK.CORP_DAIHYOU                             --��\�Җ�
        , TSDK.KYOTEN_NAME_PRINT_KBN                    --���_���󎚋敪
        , TSDK.KYOTEN_CD AS TSDK_KYOTEN_CD              --���_CD
        , TSDK.KYOTEN_NAME                              --���_��
        , TSDK.KYOTEN_DAIHYOU                           --���_��\�Җ�
        , TSDK.KYOTEN_POST                              --���_�X�֔ԍ�
        , TSDK.KYOTEN_ADDRESS1                          --���_�Z��1
        , TSDK.KYOTEN_ADDRESS2                          --���_�Z��2
        , TSDK.KYOTEN_TEL                               --���_TEL
        , TSDK.KYOTEN_FAX                               --���_FAX
        , TSDK.SHIHARAI_SOUFU_NAME1                     --�x�����׏����t��1
        , TSDK.SHIHARAI_SOUFU_NAME2                     --�x�����׏����t��2
        , TSDK.SHIHARAI_SOUFU_KEISHOU1                  --�x�����׏����t��h��1
        , TSDK.SHIHARAI_SOUFU_KEISHOU2                  --�x�����׏����t��h��2
        , TSDK.SHIHARAI_SOUFU_POST                      --�x�����׏����t��X�֔ԍ�
        , TSDK.SHIHARAI_SOUFU_ADDRESS1                  --�x�����׏����t��Z��1
        , TSDK.SHIHARAI_SOUFU_ADDRESS2                  --�x�����׏����t��Z��2
        , TSDK.SHIHARAI_SOUFU_BUSHO                     --�x�����׏����t�敔��
        , TSDK.SHIHARAI_SOUFU_TANTOU                    --�x�����׏����t��S����
        , TSDK.SHIHARAI_SOUFU_TEL                       --�x�����׏����t��TEL
        , TSDK.SHIHARAI_SOUFU_FAX                       --�x�����׏����t��FAX
        , ISNULL(TSDK.KONKAI_SHIHARAI_GAKU,0) AS KONKAI_SHIHARAI_GAKU        --����x���z
        , ISNULL(TSDK.KONKAI_SEI_UTIZEI_GAKU,0) AS KONKAI_SEI_UTIZEI_GAKU    --���񐿓��Ŋz
        , ISNULL(TSDK.KONKAI_SEI_SOTOZEI_GAKU,0) AS KONKAI_SEI_SOTOZEI_GAKU  --���񐿊O�Ŋz
        , ISNULL(TSDK.KONKAI_DEN_UTIZEI_GAKU,0) AS KONKAI_DEN_UTIZEI_GAKU    --����`���Ŋz
        , ISNULL(TSDK.KONKAI_DEN_SOTOZEI_GAKU,0) AS KONKAI_DEN_SOTOZEI_GAKU  --����`�O�Ŋz
        , ISNULL(TSDK.KONKAI_MEI_UTIZEI_GAKU,0) AS KONKAI_MEI_UTIZEI_GAKU    --���񖾓��Ŋz
        , ISNULL(TSDK.KONKAI_MEI_SOTOZEI_GAKU,0) AS KONKAI_MEI_SOTOZEI_GAKU  --���񖾊O�Ŋz
		, TSDK.BIKOU_1								  --���l1
		, TSDK.BIKOU_2								  --���l2
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_1,0) AS KONKAI_KAZEI_KBN_1            --����ېŋ敪�P
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_1,0) AS KONKAI_KAZEI_RATE_1          --����ېŗ��P
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_1,0) AS KONKAI_KAZEI_GAKU_1          --����ېŐŔ����z�P
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_1,0) AS KONKAI_KAZEI_ZEIGAKU_1    --����ېŐŊz�P
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_2,0) AS KONKAI_KAZEI_KBN_2            --����ېŋ敪�Q
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_2,0) AS KONKAI_KAZEI_RATE_2          --����ېŗ��Q
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_2,0) AS KONKAI_KAZEI_GAKU_2          --����ېŐŔ����z�Q
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_2,0) AS KONKAI_KAZEI_ZEIGAKU_2    --����ېŐŊz�Q
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_3,0) AS KONKAI_KAZEI_KBN_3            --����ېŋ敪�R
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_3,0) AS KONKAI_KAZEI_RATE_3          --����ېŗ��R
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_3,0) AS KONKAI_KAZEI_GAKU_3          --����ېŐŔ����z�R
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_3,0) AS KONKAI_KAZEI_ZEIGAKU_3    --����ېŐŊz�R
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_4,0) AS KONKAI_KAZEI_KBN_4            --����ېŋ敪�S
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_4,0) AS KONKAI_KAZEI_RATE_4          --����ېŗ��S
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_4,0) AS KONKAI_KAZEI_GAKU_4          --����ېŐŔ����z�S
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_4,0) AS KONKAI_KAZEI_ZEIGAKU_4    --����ېŐŊz�S
		, ISNULL(TSDK.KONKAI_HIKAZEI_KBN,0) AS KONKAI_HIKAZEI_KBN            --�����ېŋ敪
		, ISNULL(TSDK.KONKAI_HIKAZEI_GAKU,0) AS KONKAI_HIKAZEI_GAKU          --�����ېŊz
		, ISNULL(TSDE.SHOUHIZEI_RATE,0) AS SHOUHIZEI_RATE					 --����ŗ�
    FROM
        T_SEISAN_DENPYOU_KAGAMI TSDK 
        LEFT JOIN T_SEISAN_DETAIL TSDE 
        ON TSDK.SEISAN_NUMBER = TSDE.SEISAN_NUMBER AND TSDK.KAGAMI_NUMBER = TSDE.KAGAMI_NUMBER
    )TSDKE 
    ON TSD.SEISAN_NUMBER = TSDKE.SEISAN_NUMBER
 WHERE
  TSD.SEISAN_NUMBER = /*seisanNumber*/
  AND TSD.DELETE_FLG = 0
  /*IF IsZeroKingakuTaishogai*/
  AND (
		 (TSD.SHOSHIKI_KBN != 1 
		 AND (ISNULL(TSDKE.KONKAI_SHIHARAI_GAKU,0) + 
			  ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) + 
			  ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) + 
			  ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) + 
			  ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) + 
			  ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) + 
			  ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0) <> 0))
		OR
		(TSD.SHOSHIKI_KBN = 1
		 AND (CASE TSD.SHIHARAI_KEITAI_KBN 
				WHEN 2 THEN ISNULL(TSD.KONKAI_SEISAN_GAKU, 0)
				ELSE (ISNULL(TSD.KONKAI_SHIHARAI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU,0)+ 
					  ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU,0))
				END) <> 0))
 /*END*/
 ORDER BY
   TSDKE.KAGAMI_NUMBER
   /*$orderBy*/
  , TSDKE.DENPYOU_DATE
  , TSDKE.DENPYOU_SHURUI_CD
  , TSDKE.DENPYOU_NUMBER
  , TSDKE.ROW_NUMBER
  