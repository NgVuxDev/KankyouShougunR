SELECT TOP 1 * FROM M_SHOUHIZEI
WHERE
 DELETE_FLG = 0
  /*IF !date.IsNull */
 AND (
		(
			TEKIYOU_BEGIN <= /*date*/ 
			AND /*date*/ <= ISNULL(TEKIYOU_END,'9999/12/31')
		)
	 )
/*END*/