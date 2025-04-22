SELECT 
    BANK.BANK_CD as BANK_CD
    ,BANK.BANK_NAME_RYAKU as BANK_NAME_RYAKU
FROM 
    dbo.M_BANK BANK
/*BEGIN*/WHERE
 /*IF data.BANK_CD != null && data.BANK_CD != ''*/
 BANK.BANK_CD LIKE '%' + /*data.BANK_CD*/'0001' + '%'/*END*/
/*END*/