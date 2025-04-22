SELECT
      --実績報告書データ
       TJHE.HOUKOKU_SHOSHIKI
      --,CASE TJHE.HOUKOKU_SHOSHIKI WHEN 1 THEN '標準様式1-5号(普通産廃)'
      --                            WHEN 2 THEN '標準様式1-6号(特管)'
      --                            WHEN 3 THEN '標準様式1-7号(普通産廃集計表)'
      --                            WHEN 4 THEN '標準様式1-8号(特管集計表)'
      --                            ELSE NULL END AS HOUKOKU_SHOSHIKI_NAME
	  ,'標準様式1-5/1-6号' AS HOUKOKU_SHOSHIKI_NAME
      ,TJHE.HOUKOKU_YEAR
      ,TJHE.TEISHUTSU_CHIIKI_CD
      ,TJHE.GYOUSHA_KBN
      ,'収集運搬' AS GYOUSHA_KBN_NAME
      ,TJHE.UPDATE_DATE
      ,TJHE.TOKUBETSU_KANRI_KBN
      ,CASE TJHE.TOKUBETSU_KANRI_KBN WHEN 1 THEN '産業廃棄物'
                                     WHEN 2 THEN '特別管理産業廃棄物'
                                     ELSE NULL END AS TOKUBETSU_KANRI_SYURUI
      ,TJHE.KEN_KBN
      ,CASE TJHE.KEN_KBN WHEN 1 THEN '内→外'
                         WHEN 10 THEN '内→内'
                         WHEN 11 THEN '内→外, 内→内'
                         WHEN 100 THEN '外→内'
                         WHEN 101 THEN '外→内, 内→外'
                         WHEN 110 THEN '外→内, 内→内'
                         WHEN 111 THEN '外→内, 内→内, 内→外'
                         WHEN 1000 THEN '外→外'
                         WHEN 1001 THEN '外→外, 内→外'
                         WHEN 1010 THEN '外→外, 内→内'
                         WHEN 1011 THEN '外→外, 内→内, 内→外'
                         WHEN 1100 THEN '外→外, 外→内'
                         WHEN 1110 THEN '外→外, 外→内, 内→内'
                         WHEN 1111 THEN '全て'
                         ELSE NULL END AS KEN_KBN_NAME
      ,TJHE.HOZON_NAME
      ,TJHE.TEISHUTSU_NAME
      ,MC.CHIIKI_NAME
      ,MC.GOV_OR_MAY_NAME
      --実績報告書　処分実績明細データ
      ,TJHUD.HOUKOKUSHO_BUNRUI_CD
      ,TJHUD.HOUKOKUSHO_BUNRUI_NAME
      ,TJHUD.HST_GYOUSHA_NAME
      ,TJHUD.HST_GYOUSHA_ADDRESS
      ,TJHUD.HST_GENBA_NAME
      ,TJHUD.HST_GENBA_ADDRESS
      ,TJHUD.JYUTAKU_KBN
      ,TJHUD.JYUTAKU_RYOU
      ,TJHUD.SHOBUN_HOUHOU_NAME
      ,TJHUD.UPN_RYOU
      ,TJHUD.SBN_GENBA_ADDRESS
      ,TJHUD.HIKIWATASHISAKI_KYOKA_NO
      ,TJHUD.HIKIWATASHISAKI_NAME 
      ,TJHUD.HIKIWATASHISAKI_ADDRESS
      ,TJHUD.HIKIWATASHI_RYOU
      ,TJHUD.SYSTEM_ID
      ,TJHUD.SEQ
      ,TJHUD.DETAIL_SYSTEM_ID
      -- 帳票用データ
      ,TJHE.HOUKOKU_GYOUSHA_CD
      ,TJHE.HOUKOKU_GENBA_CD
      ,TJHE.HOUKOKU_TANTO_NAME
      ,TJHE.HOUKOKU_TITLE1
      ,TJHE.HOUKOKU_TITLE2
      ,TJHE.HST_GYOUSHA_NAME_DISP_KBN
      ,TJHUD.HST_GYOUSHA_CD
      ,TJHUD.HST_GENBA_CD
      ,TJHUD.HST_GENBA_CHIIKI_CD
      ,TJHUD.HST_KEN_KBN
      ,TJHUD.HST_JOU_TODOUFUKEN_CD
      ,TJHUD.HIKIWATASHI_KBN
	  ,TJHUD.SBN_GYOUSHA_CD
	  ,TJHUD.SBN_GYOUSHA_NAME
	  ,TJHUD.SBN_GYOUSHA_ADDRESS
      ,TJHUD.SBN_GENBA_CD
	  ,TJHUD.SBN_GENBA_NAME
      ,TJHUD.SBN_GENBA_ADDRESS
	  ,TJHUD.SBN_GENBA_CHIIKI_CD
  FROM T_JISSEKI_HOUKOKU_ENTRY AS TJHE
  LEFT JOIN T_JISSEKI_HOUKOKU_UPN_DETAIL AS TJHUD
         ON TJHE.SYSTEM_ID = TJHUD.SYSTEM_ID
        AND TJHE.SEQ = TJHUD.SEQ
  LEFT JOIN M_CHIIKI MC ON MC.CHIIKI_CD = TJHE.TEISHUTSU_CHIIKI_CD
 WHERE
       TJHE.SYSTEM_ID = /*systemid*/0
   AND TJHE.DELETE_FLG = 0
ORDER BY TJHUD.HST_KEN_KBN
        ,TJHUD.HST_GENBA_CHIIKI_CD
        ,TJHUD.HOUKOKUSHO_BUNRUI_CD
        ,TJHUD.HST_JOU_TODOUFUKEN_CD
        ,TJHUD.HST_GYOUSHA_CD
        ,TJHUD.HST_GENBA_CD
        ,TJHUD.SHOBUN_HOUHOU_CD
        ,TJHUD.SBN_GENBA_CHIIKI_CD
        ,TJHUD.SBN_GYOUSHA_CD
        ,TJHUD.SBN_GENBA_CD
        ,TJHUD.HIKIWATASHISAKI_CHIIKI_CD
        ,TJHUD.HIKIWATASHISAKI_CD