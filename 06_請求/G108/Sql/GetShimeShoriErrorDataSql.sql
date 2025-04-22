SELECT            SHORI_KBN,
                  CHECK_KBN,
				  DENPYOU_SHURUI_CD,
				  SYSTEM_ID,
				  SEQ,
				  DETAIL_SYSTEM_ID,
				  GYO_NUMBER,
				  ERROR_NAIYOU,
				  RIYUU,
				  TIME_STAMP
FROM              T_SHIME_SHORI_ERROR
WHERE             SHORI_KBN = /*data.ShoriKbn*/0 AND
                  CHECK_KBN = /*data.CheckKbn*/0 AND
				  DENPYOU_SHURUI_CD = /*data.DenpyouShuruiCD*/0 AND
				  SYSTEM_ID = /*data.SystemId*/0 AND
				  SEQ = /*data.Seq*/0
/*IF data.DetailSystemId != null*/
				  AND
				  DETAIL_SYSTEM_ID = /*data.DetailSystemId*/0
/*END*/