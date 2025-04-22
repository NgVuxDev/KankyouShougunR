
(
    -- ■１．受入：
    select 
       A1.NIOROSHI_GYOUSHA_CD     as  RET_GYOUSHA_CD          -- 業者CD
      ,A1.NIOROSHI_GENBA_CD       as  RET_GENBA_CD            -- 現場CD
      ,A1.NIOROSHI_GENBA_NAME     as  RET_GENBA_NAME          -- 現場名
      ,A2.ZAIKO_HINMEI_CD         as  RET_ZAIKO_HINMEI_CD     -- 在庫CD
      ,M1.ZAIKO_HINMEI_NAME_RYAKU as  RET_ZAIKO_HINMEI_NAME   -- 在庫品名
      ,A2.JYUURYOU                as  RET_JYUURYOU            -- 重量
      ,A2.TANKA                   as  RET_TANKA               -- 単価
      ,A2.KINGAKU                 as  RET_KINGAKU             -- 金額
      ,A1.DENPYOU_DATE            as  RET_DENPYOU_DATE        -- 伝票日付
      ,1                          as  RET_TARGET_FLG          -- 対象データフラグ
    from
      T_UKEIRE_ENTRY              A1,  -- 受入入力
      T_ZAIKO_UKEIRE_DETAIL       A2,  -- 在庫明細_受入
      M_ZAIKO_HINMEI              M1   -- 在庫品名マスタ
    where
          A1.DENPYOU_DATE         >=  /*data.simeTaisyouKikanFrom*/
      and A1.DENPYOU_DATE         <=  /*data.simeTaisyouKikanTo*/
	  and A1.NIOROSHI_GYOUSHA_CD IS NOT NULL 
	  and A1.NIOROSHI_GYOUSHA_CD != ''
	  and A1.NIOROSHI_GENBA_CD IS NOT NULL
	  and A1.NIOROSHI_GENBA_CD != ''
/*IF data.gyoushaCD != null && data.gyoushaCD != ''*/
      and A1.NIOROSHI_GYOUSHA_CD  =   /*data.gyoushaCD*/
/*END*/
/*IF data.genbaCD != null && data.genbaCD != ''*/
      and A1.NIOROSHI_GENBA_CD    =   /*data.genbaCD*/
/*END*/
      and A1.SYSTEM_ID            =   A2.SYSTEM_ID
      and A1.SEQ                  =   A2.SEQ
      and A2.ZAIKO_HINMEI_CD      =   M1.ZAIKO_HINMEI_CD
      and A1.DELETE_FLG           =   0
      and A2.DELETE_FLG           =   0
      and M1.DELETE_FLG           =   0
)
union all
(
    -- ■２．出荷：
    select 
       A1.NIZUMI_GYOUSHA_CD       as  RET_GYOUSHA_CD          -- 業者CD
      ,A1.NIZUMI_GENBA_CD         as  RET_GENBA_CD            -- 現場CD
      ,A1.NIZUMI_GENBA_NAME       as  RET_GENBA_NAME          -- 現場名
      ,A2.ZAIKO_HINMEI_CD         as  RET_ZAIKO_HINMEI_CD     -- 在庫CD
      ,M1.ZAIKO_HINMEI_NAME_RYAKU as  RET_ZAIKO_HINMEI_NAME   -- 在庫品名
      ,A2.JYUURYOU                as  RET_JYUURYOU            -- 重量
      ,A2.TANKA                   as  RET_TANKA               -- 単価
      ,A2.KINGAKU                 as  RET_KINGAKU             -- 金額
      ,A1.DENPYOU_DATE            as  RET_DENPYOU_DATE        -- 伝票日付
      ,2                          as  RET_TARGET_FLG          -- 対象データフラグ
    from
      T_SHUKKA_ENTRY              A1,  -- 出荷入力
      T_ZAIKO_SHUKKA_DETAIL       A2,  -- 在庫明細_出荷
      M_ZAIKO_HINMEI              M1   -- 在庫品名マスタ
    where
          A1.DENPYOU_DATE         >=  /*data.simeTaisyouKikanFrom*/
      and A1.DENPYOU_DATE         <=  /*data.simeTaisyouKikanTo*/
	  and A1.NIZUMI_GYOUSHA_CD IS NOT NULL 
	  and A1.NIZUMI_GYOUSHA_CD != ''
	  and A1.NIZUMI_GENBA_CD IS NOT NULL
	  and A1.NIZUMI_GENBA_CD != ''
/*IF data.gyoushaCD != null && data.gyoushaCD != ''*/
      and A1.NIZUMI_GYOUSHA_CD    =   /*data.gyoushaCD*/
/*END*/
/*IF data.genbaCD != null && data.genbaCD != ''*/
      and A1.NIZUMI_GENBA_CD      =   /*data.genbaCD*/
/*END*/
      and A1.SYSTEM_ID            =   A2.SYSTEM_ID
      and A1.SEQ                  =   A2.SEQ
      and A2.ZAIKO_HINMEI_CD      =   M1.ZAIKO_HINMEI_CD
      and A1.DELETE_FLG           =   0
      and A2.DELETE_FLG           =   0
      and M1.DELETE_FLG           =   0
)
union all
(
    -- ■３．在庫調整：
    select 
       A2.GYOUSHA_CD              as  RET_GYOUSHA_CD          -- 業者CD
      ,A2.GENBA_CD                as  RET_GENBA_CD            -- 現場CD
      ,M1.GENBA_NAME_RYAKU        as  RET_GENBA_NAME          -- 現場名
      ,A2.ZAIKO_HINMEI_CD         as  RET_ZAIKO_HINMEI_CD     -- 在庫CD
      ,M2.ZAIKO_HINMEI_NAME_RYAKU as  RET_ZAIKO_HINMEI_NAME   -- 在庫品名
      ,A2.JYUURYOU                as  RET_JYUURYOU            -- 重量
      ,A2.TANKA                   as  RET_TANKA               -- 単価
      ,A2.KINGAKU                 as  RET_KINGAKU             -- 金額
      ,A1.DENPYOU_DATE            as  RET_DENPYOU_DATE        -- 伝票日付
      ,3                          as  RET_TARGET_FLG          -- 対象データフラグ
    from
      T_ZAIKO_CHOUSEI_ENTRY       A1,  -- 出荷入力
      T_ZAIKO_CHOUSEI_DETAIL      A2,  -- 出荷明細
      M_GENBA                     M1,  -- 現場マスタ
      M_ZAIKO_HINMEI              M2,  -- 在庫品名マスタ
	  M_GYOUSHA					  M3   -- 業者
    where
          A1.DENPYOU_DATE         >=    /*data.simeTaisyouKikanFrom*/
      and A1.DENPYOU_DATE         <=    /*data.simeTaisyouKikanTo*/
      and A1.SYSTEM_ID            =     A2.SYSTEM_ID
      and A1.SEQ                  =     A2.SEQ
/*IF data.gyoushaCD != null && data.gyoushaCD != ''*/
      and A2.GYOUSHA_CD           =     /*data.gyoushaCD*/
/*END*/
/*IF data.genbaCD != null && data.genbaCD != ''*/
      and A2.GENBA_CD             =     /*data.genbaCD*/
/*END*/
      and A2.GYOUSHA_CD           =     M1.GYOUSHA_CD
      and A2.GENBA_CD             =     M1.GENBA_CD
      and A2.ZAIKO_HINMEI_CD      =     M2.ZAIKO_HINMEI_CD
      and A1.DELETE_FLG           =     0
      and A2.DELETE_FLG           =     0
      and M1.DELETE_FLG           =     0
      and M2.DELETE_FLG           =     0
	  and M3.DELETE_FLG           =     0
	  and M1.GYOUSHA_CD           =     M3.GYOUSHA_CD
	  and M1.JISHA_KBN            =     1
	  and M1.SHOBUN_NIOROSHI_GENBA_KBN   =     1	  
	  and M3.JISHA_KBN            =     1
	  and M3.SHOBUN_NIOROSHI_GYOUSHA_KBN =     1
)
order by
    -- 業者CD,現場CD,在庫CD,伝票日付
    RET_GYOUSHA_CD, RET_GENBA_CD, RET_ZAIKO_HINMEI_CD, RET_DENPYOU_DATE
