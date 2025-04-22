----通信履歴情報取得SQL文

SELECT  B.MANIFEST_ID,   --マニフェスト目次情報.マニフェスト/予約番号
        A.KANRI_ID,      --キュー情報.管理番号
        A.QUE_SEQ,       --キュー情報.レコード枝番
        A.SEQ,           --キュー情報.枝番
        A.UPN_ROUTE_NO,  --キュー情報.区間番号
        A.TUUCHI_ID,     --キュー情報.通知番号
        CASE             
          WHEN A.STATUS_FLAG = '0' THEN '送信待ち'
          WHEN A.STATUS_FLAG = '1' THEN '送信完了'
          WHEN A.STATUS_FLAG = '2' THEN 'JWNET正常完了'
          WHEN A.STATUS_FLAG = '6' THEN '保留削除'
          WHEN A.STATUS_FLAG = '7' THEN '送信保留'
          WHEN A.STATUS_FLAG = '8' THEN '送信失敗'
          WHEN A.STATUS_FLAG = '9' THEN 'JWNETエラー'
        END AS TUUSINN_STATUS, --通信状態
        CASE             
          WHEN A.FUNCTION_ID = '0101' THEN '予約の登録'
          WHEN A.FUNCTION_ID = '0102' THEN '予約の登録'
          WHEN A.FUNCTION_ID = '0201' THEN '予約の修正'
          WHEN A.FUNCTION_ID = '0202' THEN '予約の修正'
          WHEN A.FUNCTION_ID = '0203' THEN '予約の修正'
          WHEN A.FUNCTION_ID = '0204' THEN '予約の修正'
          WHEN A.FUNCTION_ID = '0300' THEN '予約の取消し'
          WHEN A.FUNCTION_ID = '0401' THEN '予約を利用したマニフェスト登録'
          WHEN A.FUNCTION_ID = '0402' THEN '予約を利用したマニフェスト登録'
          WHEN A.FUNCTION_ID = '0501' THEN 'マニフェストの登録'
          WHEN A.FUNCTION_ID = '0502' THEN 'マニフェストの登録'
          WHEN A.FUNCTION_ID = '0601' THEN 'マニフェストの修正'
          WHEN A.FUNCTION_ID = '0603' THEN 'マニフェストの修正'
          WHEN (A.FUNCTION_ID = '0701' or A.FUNCTION_ID = '0702') AND C.TUUCHI_CODE = '203' AND isnull(C.ACTION_FLAG,'') = '1' THEN 'マニフェストの修正の承認'
          WHEN (A.FUNCTION_ID = '0701' or A.FUNCTION_ID = '0702') AND C.TUUCHI_CODE = '203' AND isnull(C.ACTION_FLAG,'') = '2' THEN 'マニフェストの修正の否認'
          WHEN (A.FUNCTION_ID = '0701' or A.FUNCTION_ID = '0702') AND C.TUUCHI_CODE = '203' AND (isnull(C.ACTION_FLAG,'') <> '1' AND isnull(C.ACTION_FLAG,'') <> '2') THEN 'マニフェストの修正の承認（または否認）'
          WHEN (A.FUNCTION_ID = '0701' or A.FUNCTION_ID = '0702') AND C.TUUCHI_CODE = '206' AND isnull(C.ACTION_FLAG,'') = '1' THEN 'マニフェストの取消の承認'
          WHEN (A.FUNCTION_ID = '0701' or A.FUNCTION_ID = '0702') AND C.TUUCHI_CODE = '206' AND isnull(C.ACTION_FLAG,'') = '2' THEN 'マニフェストの取消の否認'
          WHEN (A.FUNCTION_ID = '0701' or A.FUNCTION_ID = '0702') AND C.TUUCHI_CODE = '206' AND (isnull(C.ACTION_FLAG,'') <> '1' AND isnull(C.ACTION_FLAG,'') <> '2') THEN 'マニフェストの取消の承認（または否認）'
          WHEN (A.FUNCTION_ID = '0701' or A.FUNCTION_ID = '0702') AND (C.TUUCHI_CODE <> '203' AND C.TUUCHI_CODE <> '206') AND isnull(C.ACTION_FLAG,'') = '1' THEN 'マニフェストの修正（または取消）の承認'
          WHEN (A.FUNCTION_ID = '0701' or A.FUNCTION_ID = '0702') AND (C.TUUCHI_CODE <> '203' AND C.TUUCHI_CODE <> '206') AND isnull(C.ACTION_FLAG,'') = '2' THEN 'マニフェストの修正（または取消）の否認'
          WHEN (A.FUNCTION_ID = '0701' or A.FUNCTION_ID = '0702') AND (C.TUUCHI_CODE <> '203' AND C.TUUCHI_CODE <> '206') AND (isnull(C.ACTION_FLAG,'') <> '1' AND isnull(C.ACTION_FLAG,'') <> '2') THEN 'マニフェストの修正（または取消）の承認（または否認）'
          WHEN A.FUNCTION_ID = '0800' THEN 'マニフェストの取消し'
          WHEN A.FUNCTION_ID = '1000' THEN '運搬終了報告:区間('+CONVERT(varchar,A.UPN_ROUTE_NO)+')'
          WHEN A.FUNCTION_ID = '1100' THEN '運搬終了報告の修正:区間('+CONVERT(varchar,A.UPN_ROUTE_NO)+')'
          WHEN A.FUNCTION_ID = '1200' AND C.TUUCHI_CODE = '110' AND isnull(C.ACTION_FLAG,'') = '1' THEN '運搬終了報告修正の承認'
          WHEN A.FUNCTION_ID = '1200' AND C.TUUCHI_CODE = '110' AND isnull(C.ACTION_FLAG,'') = '2' THEN '運搬終了報告修正の否認'
          WHEN A.FUNCTION_ID = '1200' AND C.TUUCHI_CODE = '110' AND (isnull(C.ACTION_FLAG,'') <> '1' AND isnull(C.ACTION_FLAG,'') <> '2') THEN '運搬終了報告修正の承認（または否認）：区間('+CONVERT(varchar,A.UPN_ROUTE_NO)+')'
          WHEN A.FUNCTION_ID = '1200' AND C.TUUCHI_CODE = '113' AND isnull(C.ACTION_FLAG,'') = '1' THEN '運搬終了報告取消の承認'
          WHEN A.FUNCTION_ID = '1200' AND C.TUUCHI_CODE = '113' AND isnull(C.ACTION_FLAG,'') = '2' THEN '運搬終了報告取消の否認'
          WHEN A.FUNCTION_ID = '1200' AND C.TUUCHI_CODE = '113' AND (isnull(C.ACTION_FLAG,'') <> '1' AND isnull(C.ACTION_FLAG,'') <> '2') THEN '運搬終了報告取消の承認（または否認）：区間('+CONVERT(varchar,A.UPN_ROUTE_NO)+')'
          WHEN A.FUNCTION_ID = '1200' AND C.TUUCHI_CODE = '118' AND isnull(C.ACTION_FLAG,'') = '1' THEN '処分終了報告修正の承認'
          WHEN A.FUNCTION_ID = '1200' AND C.TUUCHI_CODE = '118' AND isnull(C.ACTION_FLAG,'') = '2' THEN '処分終了報告修正の否認'
          WHEN A.FUNCTION_ID = '1200' AND C.TUUCHI_CODE = '118' AND (isnull(C.ACTION_FLAG,'') <> '1' AND isnull(C.ACTION_FLAG,'') <> '2') THEN '処分終了報告修正の承認（または否認）'
          WHEN A.FUNCTION_ID = '1200' AND C.TUUCHI_CODE = '121' AND isnull(C.ACTION_FLAG,'') = '1' THEN '処分終了報告取消の承認'
          WHEN A.FUNCTION_ID = '1200' AND C.TUUCHI_CODE = '121' AND isnull(C.ACTION_FLAG,'') = '2' THEN '処分終了報告取消の否認'
          WHEN A.FUNCTION_ID = '1200' AND C.TUUCHI_CODE = '121' AND (isnull(C.ACTION_FLAG,'') <> '1' AND isnull(C.ACTION_FLAG,'') <> '2') THEN '処分終了報告取消の承認（または否認）'
          WHEN A.FUNCTION_ID = '1300' THEN '運搬終了報告の取消:区間('+CONVERT(varchar,A.UPN_ROUTE_NO)+')'
          WHEN A.FUNCTION_ID = '1500' THEN '処分終了報告'
          WHEN A.FUNCTION_ID = '1600' THEN '処分終了報告の修正'
          WHEN A.FUNCTION_ID = '1800' THEN '処分終了報告の取消'
          WHEN A.FUNCTION_ID = '2000' THEN '最終処分終了報告'
          WHEN A.FUNCTION_ID = '2100' THEN '最終処分終了報告の取消'
          WHEN A.FUNCTION_ID = '3100' THEN 'マニフェストの照会'
        END AS NAIYO,    --内容
        A.UPDATE_TS,     --キュー情報.タイムスタンプ  
        A.CREATE_DATE    --キュー情報.レコード作成日時

FROM    QUE_INFO A
        LEFT JOIN DT_MF_TOC B
        ON A.KANRI_ID = B.KANRI_ID
        LEFT JOIN DT_R24 C
        ON A.TUUCHI_ID = C.TUUCHI_ID

WHERE   1 = 1
		/*IF data.STATUS_FLAG !='指定なし'*/AND A.STATUS_FLAG = /*data.STATUS_FLAG*//*END*/
		/*IF !data.MANIFEST_ID_FROM.IsNull && data.MANIFEST_ID_FROM !=''*/AND B.MANIFEST_ID >= /*data.MANIFEST_ID_FROM*//*END*/
		/*IF !data.MANIFEST_ID_TO.IsNull && data.MANIFEST_ID_TO !=''*/AND B.MANIFEST_ID <= /*data.MANIFEST_ID_TO*//*END*/
		/*IF !data.HIDZUKE_KBN.IsNull && data.HIDZUKE_KBN =='1'*/AND CONVERT(VARCHAR, A.CREATE_DATE, 111) >= CONVERT(VARCHAR, /*data.HIDZUKE_FROM*/, 111) AND CONVERT(VARCHAR, A.CREATE_DATE, 111) <= CONVERT(VARCHAR, /*data.HIDZUKE_TO*/, 111)/*END*/
		/*IF !data.HIDZUKE_KBN.IsNull && data.HIDZUKE_KBN =='2'*/AND CONVERT(VARCHAR, A.UPDATE_TS, 111) >= CONVERT(VARCHAR, /*data.HIDZUKE_FROM*/, 111) AND CONVERT(VARCHAR, A.UPDATE_TS, 111) <= CONVERT(VARCHAR, /*data.HIDZUKE_TO*/, 111)/*END*/

ORDER BY A.UPDATE_TS DESC, A.CREATE_DATE DESC