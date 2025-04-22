SELECT 
    count(DENPYOU_SYSTEM_ID)

FROM dbo.T_SEIKYUU_DETAIL 

WHERE 
 DENPYOU_SYSTEM_ID = /*denpyouSystemId*/0 /*END*/
AND DENPYOU_SEQ = /*denpyouSeq*/0 /*END*/
AND DETAIL_SYSTEM_ID = /*detailSystemId*/0 /*END*/
AND DENPYOU_NUMBER = /*denpyouNumber*/0 /*END*/
AND DENPYOU_SHURUI_CD = /*denpyouShuruiCd*/0 /*END*/



