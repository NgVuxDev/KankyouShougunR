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
WHERE 
    QUE.STATUS_FLAG IN (6,9)
    /*IF kanriid != null*/
        AND KANRI_ID = /*kanriid*/
    /*END*/
    /*IF approvalSeq != null*/
        AND SEQ = /*approvalSeq*/ 
    /*END*/