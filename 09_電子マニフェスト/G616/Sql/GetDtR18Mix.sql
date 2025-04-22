SELECT
    *
FROM
    DT_R18_MIX
WHERE
    DELETE_FLG = 0
    /*IF !data.SYSTEM_ID.IsNull*/AND SYSTEM_ID = /*data.SYSTEM_ID*//*END*/
    /*IF !data.DETAIL_SYSTEM_ID.IsNull*/AND DETAIL_SYSTEM_ID = /*data.DETAIL_SYSTEM_ID*//*END*/
    /*IF !data.SEQ.IsNull*/AND SEQ = /*data.SEQ*//*END*/
    /*IF data.KANRI_ID != null && data.KANRI_ID != ''*/AND KANRI_ID = /*data.KANRI_ID*//*END*/