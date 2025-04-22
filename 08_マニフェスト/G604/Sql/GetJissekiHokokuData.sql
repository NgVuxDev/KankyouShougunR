SELECT
    TJHE.HOUKOKU_YEAR AS HOKOKU_NENDO              --報告年度
   ,TJHE.TEISHUTSU_CHIIKI_CD AS CHIIKI_CD          --提出先CD
   ,MC.CHIIKI_NAME AS CHIIKI_NAME                  --提出先
   --,CASE WHEN TJHE.GYOUSHA_KBN = 0 THEN '自社業種区分なし'
   --      WHEN TJHE.GYOUSHA_KBN = 1 THEN '中間処理'
   --      WHEN TJHE.GYOUSHA_KBN = 2 THEN '最終処分'
   --      WHEN TJHE.GYOUSHA_KBN = 3 THEN '収集運搬'
   --      ELSE NULL END AS GYOUSHA_KBN_NAME         --自社業種区分
   ,'自社業種区分なし' AS GYOUSHA_KBN_NAME         --自社業種区分
   ,TJHE.UPDATE_DATE AS UPDATE_DATE                --保存年月日 
   ,CASE WHEN TJHE.TOKUBETSU_KANRI_KBN = 1 THEN '産業廃棄物'
        WHEN TJHE.TOKUBETSU_KANRI_KBN = 2 THEN '特別管理型'
		ELSE '特管区分なし' END AS TOKUBETSU_KANRI_SYURUI       --特管区分 
   --,CASE WHEN TJHE.HOUKOKU_SHOSHIKI = 1 THEN '様式1-9号(普通産廃)'
   --      WHEN TJHE.HOUKOKU_SHOSHIKI = 2 THEN '様式1-10号(特管)'
   --      ELSE NULL END AS HOUKOKU_SHOSHIKI         --提出書式
   ,'様式1-9/1-10号' AS HOUKOKU_SHOSHIKI           --提出書式
   ,TJHE.KEN_KBN AS KEN_KBN                        --県区分
   ,CASE WHEN TJHE.KEN_KBN = 1 THEN '県内'
         WHEN TJHE.KEN_KBN = 2 THEN '県外'
         WHEN TJHE.KEN_KBN = 3 THEN '全て'
         ELSE NULL END AS KEN_KBN_NAME             --県名
   ,TJHE.HOZON_NAME AS HOZON_NAME                  --保存名
   ,MC.GOV_OR_MAY_NAME AS GOV_OR_MAY_NAME          --提出先名
   ,TJHE.SYSTEM_ID
   ,TJHE.SEQ
   ,TJHE.REPORT_ID AS REPORT_KBN
  --実績報告書　処分実績明細データ
   ,TJHSD.SHORI_SHISETSU_NAME               --廃棄物処理施設の種類
   ,TJHSD.SHORI_SHISETSU_CD                 --施設コード
   ,TJHSD.HAIKI_SHURUI_NAME1                --種類名1
   ,TJHSD.SBN_RYOU1                         --種類1
   ,TJHSD.HAIKI_SHURUI_CD1                  --処分量1
   ,TJHSD.HAIKI_SHURUI_NAME2                --種類名2
   ,TJHSD.SBN_RYOU2                         --種類2
   ,TJHSD.HAIKI_SHURUI_CD2                  --処分量2
   ,TJHSD.HAIKI_SHURUI_NAME3                --種類名3
   ,TJHSD.SBN_RYOU3                         --種類3
   ,TJHSD.HAIKI_SHURUI_CD3                  --処分量3
   ,TJHSD.HAIKI_SHURUI_NAME4                --種類名4
   ,TJHSD.SBN_RYOU4                         --種類4
   ,TJHSD.HAIKI_SHURUI_CD4                  --処分量4
   ,TJHSD.SBN_AFTER_HAIKI_NAME              --種類名
   ,TJHSD.HST_RYOU                          --排出量
   ,TJHSD.SHOBUN_HOUHOU_CD                  --処理方法
   ,TJHSD.SHOBUN_HOUHOU_NAME                --処理方法名
   ,TJHSD.SBN_RYOU                          --処分量
   ,TJHSD.DETAIL_SYSTEM_ID 
   ,TJHE.TEISHUTSU_CHIIKI_CD
   ,TJHSD.HST_JOU_CHIIKI_CD
   FROM
   T_JISSEKI_HOUKOKU_ENTRY TJHE
   LEFT JOIN M_CHIIKI MC
   ON  MC.CHIIKI_CD = TJHE.TEISHUTSU_CHIIKI_CD
   LEFT JOIN T_JISSEKI_HOUKOKU_SHORI_DETAIL TJHSD
   ON  TJHE.SYSTEM_ID = TJHSD.SYSTEM_ID
   AND  TJHE.SEQ = TJHSD.SEQ
WHERE
       TJHE.SYSTEM_ID = /*systemid*/0
   AND TJHE.DELETE_FLG = 0
ORDER BY TJHSD.PAGE_NO
       , TJHSD.HST_KEN_KBN
	   , TJHSD.SHORI_SHISETSU_CD
	   , TJHSD.HST_JOU_CHIIKI_CD
	   , TJHSD.SHOBUN_HOUHOU_CD
	   , TJHSD.SBN_AFTER_HAIKI_NAME
	   , TJHSD.UNIT_NAME