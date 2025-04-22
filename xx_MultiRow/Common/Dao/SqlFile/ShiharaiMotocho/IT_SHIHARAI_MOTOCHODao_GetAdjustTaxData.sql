SELECT 
    MAS.TORIHIKISAKI_CD,
    MAS.YEAR,
    MAS.MONTH,
    MAS.ADJUST_TAX AS ADJUST_TAX
FROM T_MONTHLY_ADJUST_SH MAS
WHERE MAS.DELETE_FLG = 0
  /*IF startCD != null && startCD != ''*/AND MAS.TORIHIKISAKI_CD >= /*startCD*/'700001'/*END*/
  /*IF endCD != null && endCD != ''*/AND MAS.TORIHIKISAKI_CD <= /*endCD*/'700001'/*END*/
  /*IF toYear != null && toYear != '' && toMonth != null && toMonth != ''*/
  AND CONVERT(datetime, (convert(varchar,MAS.YEAR)+'-'+convert(varchar,MAS.MONTH)+'-01')) <= CONVERT(datetime, (convert(varchar,/*toYear*/2019)+'-'+convert(varchar,/*toMonth*/10)+'-01'))
  /*END*/
  /*IF fromYear != null && fromYear != '' && fromMonth != null && fromMonth != ''*/
  AND CONVERT(datetime, (convert(varchar,MAS.YEAR)+'-'+convert(varchar,MAS.MONTH)+'-01')) >= CONVERT(datetime, (convert(varchar,/*fromYear*/2019)+'-'+convert(varchar,/*fromMonth*/10)+'-01'))
  /*END*/