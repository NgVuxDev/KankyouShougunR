SELECT 
    QUE.KANRI_ID,
    QUE.QUE_SEQ,
    QUE.SEQ,REQUEST_CODE,
    QUE.EDI_RECORD_ID,
    QUE.FUNCTION_ID,
    QUE.UPN_ROUTE_NO,
    QUE.TUUCHI_ID,
    QUE.STATUS_FLAG,
    QUE.UPDATE_TS 
FROM 
    QUE_INFO QUE
    INNER JOIN (
        SELECT DISTINCT 
            KANRI_ID,
            MAX(QUE_SEQ) MAX_QUESEQ
        FROM 
            QUE_INFO QUE
        WHERE 
            FUNCTION_ID IN ('2000','2100')
            AND STATUS_FLAG IN (0,1,2,7)
        GROUP BY 
            KANRI_ID,
            SEQ
    )
    LAST_QUE 
    ON QUE.KANRI_ID = LAST_QUE.KANRI_ID
    AND QUE.QUE_SEQ = LAST_QUE.MAX_QUESEQ
WHERE 
    QUE.STATUS_FLAG IN (0,1,2,7)
    /*IF data.FUNCTION_ID != null*/
        AND /*data.FUNCTION_ID*/ = QUE.FUNCTION_ID
    /*END*/
    /*IF data.KANRI_ID != null*/
        AND /*data.KANRI_ID*/ = QUE.KANRI_ID
    /*END*/
    /*IF !data.SEQ.IsNulll*/
        AND /*data.SEQ*/ = QUE.SEQ
    /*END*/
