SELECT 
    /*IF isPrevious*/MAX(DENPYOU_NUMBER)/*END*/
    /*IF !isPrevious*/MIN(DENPYOU_NUMBER)/*END*/
FROM 
    T_ITAKU_MEMO_IKKATSU_ENTRY
WHERE 
    DELETE_FLG = 0
    /*IF isPrevious && number != 0*/AND DENPYOU_NUMBER < /*number*//*END*/
    /*IF !isPrevious && number != 0*/AND DENPYOU_NUMBER > /*number*//*END*/