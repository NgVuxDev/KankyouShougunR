SELECT
  MMK.KANSANSHIKI
  , MMK.KANSANCHI 
FROM
  M_MANIFEST_KANSAN MMK 
  left join M_HAIKI_SHURUI MHS 
    ON ( 
      MHS.HAIKI_SHURUI_CD = 2 
      AND MHS.HOUKOKUSHO_BUNRUI_CD = MMK.HOUKOKUSHO_BUNRUI_CD
    ) 
WHERE
  MMK.DELETE_FLG = 0 
  AND ( 
    MMK.tekiyou_begin <= getdate() 
    OR MMK.tekiyou_begin is null
  ) 
  AND ( 
    MMK.tekiyou_end >= getdate() 
    OR MMK.tekiyou_end is null
  ) 
  AND MMK.HAIKI_NAME_CD = /*data.HAIKI_NAME_CD*/
  AND MMK.UNIT_CD = /*data.UNIT_CD*/
  AND MMK.NISUGATA_CD = /*data.NISUGATA_CD*/
order by
  MMK.tekiyou_begin desc