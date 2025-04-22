SELECT            
RIGHT('000000' + CONVERT(nvarchar, MAX(CONVERT(int, GYOUSHA_CD)) + 1), 6) AS Expr1
FROM              
M_HIKIAI_GYOUSHA
WHERE             
(UPPER(GYOUSHA_CD) NOT LIKE '%[A-Z]%')