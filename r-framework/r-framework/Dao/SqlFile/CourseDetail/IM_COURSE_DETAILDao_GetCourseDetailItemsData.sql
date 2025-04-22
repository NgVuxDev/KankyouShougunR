SELECT
  items.*
FROM M_COURSE_DETAIL_ITEMS AS items
LEFT JOIN M_COURSE_DETAIL AS detail
  ON detail.DAY_CD = items.DAY_CD
 AND detail.COURSE_NAME_CD = items.COURSE_NAME_CD
 AND detail.REC_ID = items.REC_ID
WHERE
/*IF data.GYOUSHA_CD != null*/detail.GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
/*IF data.GENBA_CD != null*/AND detail.GENBA_CD = /*data.GENBA_CD*//*END*/