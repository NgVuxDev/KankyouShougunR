select TOP 1					
       t5.ZENKAI_SEIKYUUGAKU					
  from (select t1.SEIKYUU_DATE as SEIKYUU_DATE,
			   t1.SEIKYUU_NUMBER as SEIKYUU_NUMBER					
              ,(case t1.SEIKYUU_KEITAI_KBN					
                when '1'					
                then t1.KONKAI_URIAGE_GAKU + t1.KONKAI_SEI_UTIZEI_GAKU					
                                           + t1.KONKAI_SEI_SOTOZEI_GAKU					
                                           + t1.KONKAI_DEN_UTIZEI_GAKU					
                                           + t1.KONKAI_DEN_SOTOZEI_GAKU					
                                           + t1.KONKAI_MEI_UTIZEI_GAKU					
                                           + t1.KONKAI_MEI_SOTOZEI_GAKU					
                else t1.KONKAI_SEIKYU_GAKU end					
               ) as ZENKAI_SEIKYUUGAKU					
          from T_SEIKYUU_DENPYOU t1						
		 /*BEGIN*/				
         where 
		 /*IF !deletechuFlg*/
		  t1.DELETE_FLG = 0
		 /*END*/
		 /*IF data.Torihikisaki_cd != null && data.Torihikisaki_cd != ''*/
		   and t1.TORIHIKISAKI_CD = /*data.Torihikisaki_cd*/ /*END*/
		 /*IF data.Denpyou_Date != null && data.Denpyou_Date != ''*/					
           and t1.SEIKYUU_DATE <= /*data.Denpyou_Date*//*END*/				
		 /*END*/					
        union					
        select '1900-01-01 00:00:00' as SEIKYUU_DATE,	
				0 AS SEIKYUU_NUMBER	
              ,t4.KAISHI_URIKAKE_ZANDAKA as ZENKAI_SEIKYUUGAKU					
          from M_TORIHIKISAKI t3					
              ,M_TORIHIKISAKI_SEIKYUU t4	
		/*BEGIN*/		  				
         where 
		 /*IF !deletechuFlg*/
		    t3.TORIHIKISAKI_CD = t4.TORIHIKISAKI_CD					
            and t3.DELETE_FLG = 0
		 /*END*/
		 /*IF data.Torihikisaki_cd != null && data.Torihikisaki_cd != ''*/
		 and t4.TORIHIKISAKI_CD = /*data.Torihikisaki_cd*/ /*END*/					
		/*END*/			
       ) t5					
 order by t5.SEIKYUU_NUMBER DESC					
