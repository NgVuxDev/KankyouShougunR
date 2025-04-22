SELECT 
    *
FROM 
    dbo.M_MY_FAVORITE
/*BEGIN*/WHERE 
/*IF data.BUSHO_CD != null*/BUSHO_CD = /*data.BUSHO_CD*/''/*END*/
/*IF data.SHAIN_CD != null*/AND SHAIN_CD = /*data.SHAIN_CD*/''/*END*/
/*IF data.INDEX_NO != null*/AND INDEX_NO = /*data.INDEX_NO*/''/*END*/
/*IF data.FORM_ID != null*/AND FORM_ID = /*data.FORM_ID*/''/*END*/
/*END*/
ORDER BY BUSHO_CD, SHAIN_CD, INDEX_NO, FORM_ID