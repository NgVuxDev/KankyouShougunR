select top 1
	  t1.SEIKYUU_NUMBER as SEIKYUU_NUMBER
      ,'開始残高' as SEIKYUU_DATE
      ,t1.KAISHI_URIKAKE_ZANDAKA as SEIKYUU_GAKU
	  /*IF data.Nyukin_number != null && data.Nyukin_number != ''*/
      ,t4.KESHIKOMI_SEQ as KESHIKOMI_SEQ
      ,t4.KESHIKOMI_GAKU as KESHIKOMI_GAKU
	  ,t4.SYSTEM_ID as KESIKOMI_SYSTEM_ID
	  ,t4.NYUUKIN_SEQ AS NYUUKIN_SEQ
	  ,t4.NYUUKIN_NUMBER AS NYUUKIN_NUMBER
	  ,CAST(t4.TIME_STAMP AS int) AS TIME_STAMP
	  /*END*/
      ,(t1.KAISHI_URIKAKE_ZANDAKA - isnull(t3.KESHIKOMI_GAKU,0)) as MINYUUKIN_GAKU
FROM
	(select 0 as SEIKYUU_NUMBER
		,M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD as TORIHIKISAKI_CD
		,M_TORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA as KAISHI_URIKAKE_ZANDAKA
	 from M_TORIHIKISAKI_SEIKYUU
	 /*BEGIN*/
	 where 
	 /*IF data.Torihikisaki_cd != null && data.Torihikisaki_cd != ''*/
	 M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD = /*data.Torihikisaki_cd*/ 
	 /*END*/
	 /*END*/
    ) AS t1
	LEFT OUTER JOIN
       (select T_NYUUKIN_KESHIKOMI.SEIKYUU_NUMBER as SEIKYUU_NUMBER
              ,sum(T_NYUUKIN_KESHIKOMI.KESHIKOMI_GAKU) as KESHIKOMI_GAKU
          from T_NYUUKIN_KESHIKOMI
              ,T_NYUUKIN_ENTRY
		/*BEGIN*/
         where	
		/*IF !deletechuFlg*/
			T_NYUUKIN_KESHIKOMI.DELETE_FLG = 0
            and T_NYUUKIN_KESHIKOMI.SEIKYUU_NUMBER = 0
            and T_NYUUKIN_ENTRY.DELETE_FLG = 0
            and T_NYUUKIN_ENTRY.SYSTEM_ID = T_NYUUKIN_KESHIKOMI.SYSTEM_ID
            and T_NYUUKIN_ENTRY.SEQ = T_NYUUKIN_KESHIKOMI.NYUUKIN_SEQ
		/*END*/
		/*IF data.Torihikisaki_cd != null && data.Torihikisaki_cd != ''*/
            and T_NYUUKIN_ENTRY.TORIHIKISAKI_CD = /*data.Torihikisaki_cd*/
		/*END*/
		/*END*/
         group by T_NYUUKIN_KESHIKOMI.SEIKYUU_NUMBER
       ) AS t3  ON t1.SEIKYUU_NUMBER = t3.SEIKYUU_NUMBER
	   /*IF data.Nyukin_number != null && data.Nyukin_number != ''*/
       LEFT OUTER JOIN
       (select t41.SEIKYUU_NUMBER as SEIKYUU_NUMBER
              ,t41.KESHIKOMI_SEQ as KESHIKOMI_SEQ
              ,t41.KESHIKOMI_GAKU as KESHIKOMI_GAKU
			  ,t41.TIME_STAMP as TIME_STAMP
			  ,t41.SYSTEM_ID as SYSTEM_ID
			  ,t41.NYUUKIN_SEQ as NYUUKIN_SEQ
			  ,t41.NYUUKIN_NUMBER as NYUUKIN_NUMBER
          from T_NYUUKIN_KESHIKOMI t41
              ,T_NYUUKIN_ENTRY t42
         where t41.NYUUKIN_NUMBER = /*data.Nyukin_number*/
           and t41.DELETE_FLG = 0
           and t41.SYSTEM_ID = t42.SYSTEM_ID
           and t41.NYUUKIN_SEQ = t42.SEQ
           and t42.TORIHIKISAKI_CD = /*data.Torihikisaki_cd*/
		   and t42.DELETE_FLG = 0
       ) t4
       ON (t1.SEIKYUU_NUMBER = t4.SEIKYUU_NUMBER)

		/*END*/
		,M_TORIHIKISAKI t2
/*BEGIN*/	
where 
	/*IF !deletechuFlg*/
		t1.TORIHIKISAKI_CD = t2.TORIHIKISAKI_CD and 
		t2.DELETE_FLG = 0 and
		/*IF data.Nyukin_number != null && data.Nyukin_number != ''*/
		(
		/*END*/
		(t1.KAISHI_URIKAKE_ZANDAKA - isnull(t3.KESHIKOMI_GAKU,0)) <> 0 
		/*IF data.Nyukin_number != null && data.Nyukin_number != ''*/
			or (isnull(t4.KESHIKOMI_GAKU,0) <> 0 )
		   ) 
		/*END*/
	/*END*/
	/*IF data.Torihikisaki_cd != null && data.Torihikisaki_cd != ''*/
		and t1.TORIHIKISAKI_CD = /*data.Torihikisaki_cd*/ 
	/*END*/
	/*IF data.Denpyou_Date != null && data.Denpyou_Date != ''*/
AND	isnull(t2.TEKIYOU_BEGIN,'1900-01-01 00:00:00.000') <= /*data.Denpyou_Date*/
AND	isnull(t2.TEKIYOU_END,'2099-01-01 00:00:00.000') >= /*data.Denpyou_Date*//*END*/ 
	/*END*/	 
/*END*/