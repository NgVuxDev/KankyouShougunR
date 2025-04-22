SELECT R24.TUUCHI_ID,R24.TUUCHI_CODE
--通知情報結果
   FROM DT_R24 R24
--INNER JOIN 通知種類
--   ON 通知情報結果.通知コード　＝　通知種類.通知コード
--  AND 通知種類.削除フラグ　＝　0(未削除)
  INNER JOIN M_JW_NOTIFICATION MJN
     ON R24.TUUCHI_CODE = MJN.NOTIFICATION_CODE
	AND MJN.DELETE_FLG = 0

--INNER JOIN マニフェスト目次情報
--   ON 通知情報結果.マニフェスト番号　＝　マニフェスト目次情報.マニフェスト／予約番号
  INNER JOIN DT_MF_TOC DMT
     ON R24.MANIFEST_ID = DMT.MANIFEST_ID

--INNER JOIN マニフェスト情報
--   ON マニフェスト目次情報.管理番号　＝　マニフェスト情報.管理番号
--  AND マニフェスト目次情報.最新SEQ　＝　マニフェスト情報.枝番
  INNER JOIN DT_R18 R18
     ON DMT.KANRI_ID = R18.KANRI_ID
	AND DMT.LATEST_SEQ = R18.SEQ

--INNER JOIN 加入者情報
--   ON 通知情報結果.加入者番号　＝　加入者情報.加入者番号
  INNER JOIN MS_JWNET_MEMBER MJM
     ON R24.MEMBER_ID = MJM.EDI_MEMBER_ID

--INNER JOIN システム設定
--   ON システム設定.ID　＝　0
  INNER JOIN M_SYS_INFO MSI
     ON MSI.SYS_ID = 0

  INNER JOIN DT_R18_EX R18EX
     ON R18EX.KANRI_ID = R18.KANRI_ID AND R18EX.DELETE_FLG = 0

  WHERE
--(非要請通知系)
--通知情報結果.通知情報ステータス　＝　1(重要)
--  AND 通知情報結果.通知コード　NOT　IN　(110, 113, 118, 121, 203, 206, 303, 306)
--  AND 通知情報結果.通知日　＞＝　システム日付から[システム設定].[マニフェスト].[電子マニフェスト].[開始通知日]を減算した日付
--  AND 通知情報結果.通知日　＜＝　システム日付から[システム設定].[マニフェスト].[電子マニフェスト].[終了通知日]を減算した日付
--  AND 通知情報結果.既読フラグ　＝　0(未読)
        R24.TUUCHI_STATUS = '1'
	AND R24.TUUCHI_CODE NOT IN (110, 113, 118, 121, 203, 206, 303, 306)
	AND R24.TUUCHI_DATE >= CONVERT(NVARCHAR, GETDATE() - ISNULL(MSI.MANIFEST_TUUCHI_BEGIN, 0), 112)
	AND R24.TUUCHI_DATE <= CONVERT(NVARCHAR, GETDATE() - ISNULL(MSI.MANIFEST_TUUCHI_END, 0), 112)
	AND R24.READ_FLAG = 0
UNION
SELECT R24.TUUCHI_ID,R24.TUUCHI_CODE
--通知情報結果
   FROM DT_R24 R24
--INNER JOIN 通知種類
--   ON 通知情報結果.通知コード　＝　通知種類.通知コード
--  AND 通知種類.削除フラグ　＝　0(未削除)
  INNER JOIN M_JW_NOTIFICATION MJN
     ON R24.TUUCHI_CODE = MJN.NOTIFICATION_CODE
	AND MJN.DELETE_FLG = 0

--INNER JOIN マニフェスト目次情報
--   ON 通知情報結果.マニフェスト番号　＝　マニフェスト目次情報.マニフェスト／予約番号
  INNER JOIN DT_MF_TOC DMT
     ON R24.MANIFEST_ID = DMT.MANIFEST_ID

--INNER JOIN マニフェスト情報
--   ON マニフェスト目次情報.管理番号　＝　マニフェスト情報.管理番号
--  AND マニフェスト目次情報.最新SEQ　＝　マニフェスト情報.枝番
  INNER JOIN DT_R18 R18
     ON DMT.KANRI_ID = R18.KANRI_ID
	AND DMT.LATEST_SEQ = R18.SEQ

--INNER JOIN 加入者情報
--   ON 通知情報結果.加入者番号　＝　加入者情報.加入者番号
  INNER JOIN MS_JWNET_MEMBER MJM
     ON R24.MEMBER_ID = MJM.EDI_MEMBER_ID

--INNER JOIN システム設定
--   ON システム設定.ID　＝　0
  INNER JOIN M_SYS_INFO MSI
     ON MSI.SYS_ID = 0

  INNER JOIN DT_R18_EX R18EX
     ON R18EX.KANRI_ID = R18.KANRI_ID AND R18EX.DELETE_FLG = 0

  WHERE
--(要請通知系)
--通知情報結果.通知情報ステータス　＝　1(重要)
--  AND 通知情報結果.通知コード　IN　(110, 113, 118, 121, 203, 206, 303, 306)
--  AND (通知情報結果.承認／否認フラグ　IS　NULL OR 通知情報結果.承認／否認フラグ　＝　0(未定))
--  AND マニフェスト目次情報.修正／取消中SEQ　IS　NOT　NULL
--  AND 通知情報結果.通知日　＞＝　システム日付から[システム設定].[マニフェスト].[電子マニフェスト].[開始通知日]を減算した日付
--  AND 通知情報結果.通知日　＜＝　システム日付から[システム設定].[マニフェスト].[電子マニフェスト].[終了通知日]を減算した日付
--  AND 通知情報結果.既読フラグ　＝　0(未読)
        R24.TUUCHI_STATUS = '1'
	AND R24.TUUCHI_CODE IN (110, 113, 118, 121, 203, 206, 303, 306)
	AND (R24.ACTION_FLAG IS NULL OR R24.ACTION_FLAG = '0')
	AND DMT.APPROVAL_SEQ IS NOT NULL
	AND R24.TUUCHI_DATE >= CONVERT(NVARCHAR, GETDATE() - ISNULL(MSI.MANIFEST_TUUCHI_BEGIN, 0), 112)
	AND R24.TUUCHI_DATE <= CONVERT(NVARCHAR, GETDATE() - ISNULL(MSI.MANIFEST_TUUCHI_END, 0), 112)
	AND R24.READ_FLAG = 0