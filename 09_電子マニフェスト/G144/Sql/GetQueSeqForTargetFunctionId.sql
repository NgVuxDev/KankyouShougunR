SELECT
    *
FROM
    QUE_INFO
/*BEGIN*/
WHERE
    /*IF data.Search_CD != null && data.Search_CD != ''*/
        KANRI_ID = /*data.Search_CD*/
    /*END*/
    /*IF data.Search_CD != null && data.Search_CD != ''*/
    AND QUE_SEQ = (
        SELECT
            Max(QUE_SEQ) AS QUE_SEQ
        FROM
            QUE_INFO
        WHERE
            KANRI_ID = /*data.Search_CD*/
            /*IF !data.QUE_SEQ.IsNull*/
                AND QUE_SEQ = /*data.QUE_SEQ*/
            /*END*/
            /*IF data.FUNCTION_ID != null && data.FUNCTION_ID != ''*/
                AND FUNCTION_ID = /*data.FUNCTION_ID*/
            /*END*/
        )
    /*END*/
/*END*/
