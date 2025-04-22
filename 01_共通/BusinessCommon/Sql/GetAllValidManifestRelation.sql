-- 引数T_MANIFEST_RELATIONの情報で絞り込んで、有効なデータを検索する
SELECT
    *
FROM
    T_MANIFEST_RELATION
WHERE
    DELETE_FLG = 0
    /*IF !data.NEXT_SYSTEM_ID.IsNull*/ AND NEXT_SYSTEM_ID = /*data.NEXT_SYSTEM_ID*/ /*END*/
    /*IF !data.SEQ.IsNull*/ AND SEQ = /*data.SEQ*/ /*END*/
    /*IF !data.REC_SEQ.IsNull*/ AND REC_SEQ = /*data.REC_SEQ*/ /*END*/
    /*IF !data.NEXT_HAIKI_KBN_CD.IsNull*/ AND NEXT_HAIKI_KBN_CD = /*data.NEXT_HAIKI_KBN_CD*/ /*END*/
    /*IF !data.FIRST_SYSTEM_ID.IsNull*/ AND FIRST_SYSTEM_ID = /*data.FIRST_SYSTEM_ID*/ /*END*/
    /*IF !data.FIRST_HAIKI_KBN_CD.IsNull*/ AND FIRST_HAIKI_KBN_CD = /*data.FIRST_HAIKI_KBN_CD*/ /*END*/