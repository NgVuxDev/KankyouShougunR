select
    ZAIKO_BASE_TANKA as RET_ZAIKO_BASE_TANKA  --在庫基準単価
from
    M_ZAIKO_HINMEI A1  --在庫品名マスタ
where
    A1.ZAIKO_HINMEI_CD = /*zaikoHinmeiCd*/
    and A1.DELETE_FLG  = 0