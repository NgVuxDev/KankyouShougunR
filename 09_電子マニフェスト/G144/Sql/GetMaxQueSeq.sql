SELECT * FROM QUE_INFO
/*BEGIN*/
where 
/*IF data.Search_CD != null && data.Search_CD != ''*/
KANRI_ID = /*data.Search_CD*//*END*/
/*IF data.Search_CD != null && data.Search_CD != ''*/
AND QUE_SEQ = (SELECT Max(QUE_SEQ) AS QUE_SEQ FROM QUE_INFO WHERE KANRI_ID = /*data.Search_CD*/)
/*END*/
/*END*/
