SELECT
right('000000' + convert(nvarchar, MAX(CONVERT(int, GENBA_CD)) + 1), 6)
FROM
M_HIKIAI_GENBA
WHERE upper(GENBA_CD) not like '%[A-Z]%'