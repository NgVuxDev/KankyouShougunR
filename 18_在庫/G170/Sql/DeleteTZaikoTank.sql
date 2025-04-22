delete
from T_ZAIKO_TANK    --在庫締データ
where
      ZAIKO_SHIME_DATE    >=    /*data.simeTaisyouKikanFrom*/
  and ZAIKO_SHIME_DATE    <=    /*data.simeTaisyouKikanTo*/
/*IF data.gyoushaCD != null && data.gyoushaCD != ''*/
  and GYOUSHA_CD          =     /*data.gyoushaCD*/
/*END*/
/*IF data.genbaCD != null && data.genbaCD != ''*/
  and GENBA_CD            =     /*data.genbaCD*/
/*END*/
