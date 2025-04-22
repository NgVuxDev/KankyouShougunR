SELECT * FROM dbo.M_HINMEI
WHERE
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.HINMEI_CD != null*/AND HINMEI_CD = /*data.HINMEI_CD*//*END*/
/*IF data.HINMEI_NAME != null*/AND HINMEI_NAME = /*data.HINMEI_NAME*//*END*/
/*IF data.HINMEI_NAME_RYAKU != null*/AND HINMEI_NAME_RYAKU = /*data.HINMEI_NAME_RYAKU*//*END*/
/*IF data.HINMEI_FURIGANA != null*/AND HINMEI_FURIGANA = /*data.HINMEI_FURIGANA*//*END*/
/*IF !data.UNIT_CD.IsNull*/AND UNIT_CD = /*data.UNIT_CD.Value*//*END*/
/*IF !data.DENSHU_KBN_CD.IsNull*/AND DENSHU_KBN_CD = /*data.DENSHU_KBN_CD.Value*//*END*/
/*IF !data.DENPYOU_KBN_CD.IsNull*/AND DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD.Value*//*END*/
/*IF !data.ZEI_KBN_CD.IsNull*/AND ZEI_KBN_CD = /*data.ZEI_KBN_CD.Value*//*END*/
/*IF data.SHURUI_CD != null*/AND SHURUI_CD = /*data.SHURUI_CD*//*END*/
/*IF data.BUNRUI_CD != null*/AND BUNRUI_CD = /*data.BUNRUI_CD*//*END*/
/*IF data.HOUKOKUSHO_BUNRUI_CD != null*/AND HOUKOKUSHO_BUNRUI_CD = /*data.HOUKOKUSHO_BUNRUI_CD*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
