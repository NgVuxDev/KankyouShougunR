--最終処分終了報告の取消
SELECT DISTINCT
    --管理番号
    R18.KANRI_ID,
    --最新SEQ
    R18.LATEST_SEQ,
    --システムID
    TMR1.NEXT_SYSTEM_ID AS SYSTEM_ID,
    --タイムスタンプ
    R18.UPDATE_TS,
	CONVERT(binary(8), TLSS.TIME_STAMP) AS TIME_STAMP,
    --状態フラグ
    R18.STATUS_FLAG,
    --状態詳細フラグ
    R18.STATUS_DETAIL,
    --修正/取消中SEQ
    R18.APPROVAL_SEQ
FROM
    --電子マニフェスト基本拡張
    DT_R18_EX
    --マニフェスト紐付　AS　マニフェスト紐付（1次）
    INNER JOIN T_MANIFEST_RELATION TMR1 ON TMR1.NEXT_SYSTEM_ID = DT_R18_EX.SYSTEM_ID AND TMR1.NEXT_HAIKI_KBN_CD = 4 AND TMR1.DELETE_FLG = 0
    --電子マニフェスト基本拡張（１次）
    INNER JOIN (
        SELECT
            DMT.*,
            DT_R18.LAST_SBN_ENDREP_FLAG,
            CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL
                THEN DT_R18_MIX.DETAIL_SYSTEM_ID
                ELSE DT_R18_EX.SYSTEM_ID
            END
            AS SYSTEM_ID,
            CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL
                THEN DT_R18_MIX.DELETE_FLG
                ELSE DT_R18_EX.DELETE_FLG
            END
            AS DELETE_FLG
        FROM
            --マニフェスト目次情報
            DT_MF_TOC DMT
            --R18 マニフェスト情報
            INNER JOIN DT_R18 ON DT_R18.KANRI_ID = DMT.KANRI_ID
                             AND DT_R18.SEQ = DMT.LATEST_SEQ
            INNER JOIN DT_R18_EX ON DMT.KANRI_ID = DT_R18_EX.KANRI_ID
            LEFT JOIN DT_R18_MIX
            ON DT_R18_EX.SYSTEM_ID = DT_R18_MIX.SYSTEM_ID
            AND DT_R18_MIX.DELETE_FLG = 0
        WHERE
            DT_R18_EX.DELETE_FLG = 0
    ) AS R18 ON R18.SYSTEM_ID = TMR1.FIRST_SYSTEM_ID
                               AND TMR1.FIRST_HAIKI_KBN_CD = 4
                               AND R18.DELETE_FLG = 0
    --マニフェスト紐付　AS　マニフェスト紐付（2次）
    INNER JOIN T_MANIFEST_RELATION TMR2 ON TMR2.FIRST_SYSTEM_ID = TMR1.FIRST_SYSTEM_ID AND TMR2.FIRST_HAIKI_KBN_CD = TMR1.FIRST_HAIKI_KBN_CD AND TMR2.DELETE_FLG = 0
    --最終処分保留
    LEFT JOIN T_LAST_SBN_SUSPEND TLSS ON TLSS.SYSTEM_ID = TMR2.NEXT_SYSTEM_ID
                                     --AND TLSS.DELETE_FLG = 0
--電子マニフェスト基本拡張.管理番号　＝　現在表示中のマニフェストの管理番号
--電子マニフェスト基本拡張.削除フラグ　＝　0
--R18 マニフェスト情報.最終処分終了報告済フラグ　＝　1
--最終処分保留.システムID IS　NULL
WHERE DT_R18_EX.KANRI_ID =  /*data.KANRI_ID*/
  AND DT_R18_EX.DELETE_FLG = 0
  AND R18.LAST_SBN_ENDREP_FLAG = 1
  AND (TLSS.SYSTEM_ID IS NULL OR TLSS.DELETE_FLG != 0)