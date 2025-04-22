SELECT
    MCD.*
FROM
    M_COURSE_DETAIL AS MCD
/*BEGIN*/
WHERE
    /*IF !data.DAY_CD.IsNull*/
    MCD.DAY_CD = /*data.DAY_CD*/1
    /*END*/
    /*IF data.COURSE_NAME_CD != null*/
AND MCD.COURSE_NAME_CD = /*data.COURSE_NAME_CD*/'abc'
    /*END*/
/*END*/
ORDER BY
    MCD.ROW_NO