SELECT * FROM dbo.M_MY_FAVORITE
WHERE 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.BUSHO_CD != null*/AND BUSHO_CD = /*data.BUSHO_CD*//*END*/
/*IF data.SHAIN_CD != null*/AND SHAIN_CD = /*data.SHAIN_CD*//*END*/
/*IF data.INDEX_NO != null*/AND INDEX_NO = /*data.INDEX_NO*//*END*/
/*IF data.FORM_ID != null*/AND FORM_ID = /*data.FORM_ID*//*END*/
/*IF !data.MY_FAVORITE.IsNull*/ AND MY_FAVORITE = /*data.MY_FAVORITE*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
