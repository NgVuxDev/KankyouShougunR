select
    A1.SYSTEM_ID               as  RET_SYSTEM_ID             --システムID   --sort1、
   ,A1.ZAIKO_SHIME_DATE        as  RET_ZAIKO_SHIME_DATE      --在庫締実行日 --sort5、
   ,A1.GYOUSHA_CD              as  RET_GYOUSHA_CD            --業者CD       --画面、sort2
   ,A1.GENBA_CD                as  RET_GENBA_CD              --現場CD       --画面、CSV、帳票、sort3
   ,M1.GENBA_NAME_RYAKU        as  RET_GENBA_NAME_RYAKU      --現場名       --画面、CSV、帳票
   ,A2.ZAIKO_HINMEI_CD         as  RET_ZAIKO_HINMEI_CD       --在庫CD       --画面、CSV、帳票、sort4
   ,M2.ZAIKO_HINMEI_NAME_RYAKU as  RET_ZAIKO_HINMEI_RYAKU    --在庫品名     --画面、CSV、帳票
   ,A2.REMAIN_SUU              as  RET_REMAIN_SUU            --前月残数     --画面、CSV、帳票
   ,A2.ENTER_SUU               as  RET_ENTER_SUU             --当月受入数   --画面、CSV、帳票
   ,A2.OUT_SUU                 as  RET_OUT_SUU               --当月出荷量   --画面、CSV、帳票
   ,A2.ADJUST_SUU              as  RET_ADJUST_SUU            --調整量       --画面、CSV、帳票
   ,A2.TOTAL_SUU               as  RET_TOTAL_SUU             --当月在庫残   --画面、CSV、帳票
   ,A2.TANKA                   as  RET_TANKA                 --評価単価     --画面、CSV、帳票
   ,(A2.TOTAL_SUU * A2.TANKA)  as  RET_MULT                  --在庫金額     --画面、CSV、帳票
   ,A1.CREATE_USER
   ,A1.CREATE_DATE
   ,A1.UPDATE_USER
   ,A1.UPDATE_DATE
from
    T_ZAIKO_TANK         A1    --在庫締データ
   ,T_ZAIKO_TANK_DETAIL  A2    --在庫締明細
   ,M_GENBA              M1    --現場マスタ
   ,M_ZAIKO_HINMEI       M2    --在庫品名
where
        A1.ZAIKO_SHIME_DATE    >=    /*data.simeTaisyouKikanFrom*/
    and A1.ZAIKO_SHIME_DATE    <=    /*data.simeTaisyouKikanTo*/
/*IF data.gyoushaCD != null && data.gyoushaCD != ''*/
    and A1.GYOUSHA_CD          =     /*data.gyoushaCD*/
/*END*/
/*IF data.genbaCD != null && data.genbaCD != ''*/
    and A1.GENBA_CD            =     /*data.genbaCD*/
/*END*/
    and A1.GYOUSHA_CD          =     M1.GYOUSHA_CD 
    and A1.GENBA_CD            =     M1.GENBA_CD
    and A1.SYSTEM_ID           =     A2.SYSTEM_ID 
    and A2.ZAIKO_HINMEI_CD     =     M2.ZAIKO_HINMEI_CD
    and A1.DELETE_FLG          =     0
	and M2.DELETE_FLG          =     0 
	and M1.DELETE_FLG          =     0 
order by
    -- 業者CD,現場CD,在庫商品CD
    RET_GYOUSHA_CD, RET_GENBA_CD, RET_ZAIKO_HINMEI_CD