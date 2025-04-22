SELECT * FROM dbo.M_KOBETSU_HINMEI
WHERE 1 = 1
/*IF data.GYOUSHA_CD != null*/AND GYOUSHA_CD = /*data.GYOUSHA_CD*/''/*END*/
/*IF data.GENBA_CD != null*/AND GENBA_CD = /*data.GENBA_CD*/''/*END*/
/*IF data.HINMEI_CD != null*/AND HINMEI_CD = /*data.HINMEI_CD*/''/*END*/