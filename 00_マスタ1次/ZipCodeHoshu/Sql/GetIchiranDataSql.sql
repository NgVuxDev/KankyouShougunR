SELECT 
	*
FROM 
    dbo.S_ZIP_CODE
/*BEGIN*/WHERE
 /*IF data.POST3 != null*/
 POST3 LIKE '%' + /*data.POST3*/'100' + '%'
 /*END*/
 /*IF data.POST7 != null*/AND POST7 LIKE '%' + /*data.POST7*/'100-0001' + '%'/*END*/
/*END*/
ORDER BY POST7
