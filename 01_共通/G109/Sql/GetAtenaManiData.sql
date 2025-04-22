SELECT
	/*IF printKubun == 1*/ATN.TORIHIKISAKI_CD AS CD,/*END*/
	/*IF printKubun == 2*/ATN.GYOUSHA_CD AS CD,/*END*/
	/*IF printKubun == 3*/ATN.GENBA_CD AS CD,/*END*/
	ATN.MANI_HENSOUSAKI_NAME1 AS NAME1,
	ATN.MANI_HENSOUSAKI_NAME2 AS NAME2,
	ATN.MANI_HENSOUSAKI_KEISHOU1 AS KEISHOU1,
	ATN.MANI_HENSOUSAKI_KEISHOU2 AS KEISHOU2,
	ATN.MANI_HENSOUSAKI_POST AS POST,
	ATN.MANI_HENSOUSAKI_ADDRESS1 AS ADDRESS1,
	ATN.MANI_HENSOUSAKI_ADDRESS2 AS ADDRESS2,
	ATN.MANI_HENSOUSAKI_BUSHO AS BUSHO,
	ATN.MANI_HENSOUSAKI_TANTOU AS TANTOU
FROM
	/*IF printKubun == 1*/M_TORIHIKISAKI AS ATN/*END*/
	/*IF printKubun == 2*/M_GYOUSHA AS ATN/*END*/
	/*IF printKubun == 3*/M_GENBA AS ATN/*END*/
WHERE
/*IF printHouhou == 1*/
	/*IF printKubun == 1 && kobetsuShitei != ''*/ATN.TORIHIKISAKI_CD IN /*$kobetsuShitei*//*END*/
	/*IF printKubun == 2 && kobetsuShitei != ''*/ATN.GYOUSHA_CD IN /*$kobetsuShitei*//*END*/
	/*IF printKubun == 3 && kobetsuShitei != '' && GyoushaCd != ''*/ATN.GYOUSHA_CD = /*GyoushaCd*/
						   AND ATN.GENBA_CD IN /*$kobetsuShitei*//*END*/
/*END*/
/*IF printHouhou == 2*/
	/*IF printKubun == 1 && TorihikisakiCd != ''*/ATN.TORIHIKISAKI_CD = /*TorihikisakiCd*//*END*/
	/*IF printKubun == 2 && GyoushaCd != ''*/ATN.GYOUSHA_CD = /*GyoushaCd*//*END*/
	/*IF printKubun == 3 && GyoushaCd != '' && GenbaCd != ''*/ATN.GYOUSHA_CD = /*GyoushaCd*/ AND ATN.GENBA_CD = /*GenbaCd*//*END*/
/*END*/

AND ATN.MANI_HENSOUSAKI_KBN = 1