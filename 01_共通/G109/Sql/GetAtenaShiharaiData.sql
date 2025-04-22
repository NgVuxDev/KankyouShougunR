SELECT
	/*IF printKubun == 1*/ATN.TORIHIKISAKI_CD AS CD,/*END*/
	/*IF printKubun == 2*/ATN.GYOUSHA_CD AS CD,/*END*/
	/*IF printKubun == 3*/ATN.GENBA_CD AS CD,/*END*/
	ATN.SHIHARAI_SOUFU_NAME1 AS NAME1,
	ATN.SHIHARAI_SOUFU_NAME2 AS NAME2,
	ATN.SHIHARAI_SOUFU_KEISHOU1 AS KEISHOU1,
	ATN.SHIHARAI_SOUFU_KEISHOU2 AS KEISHOU2,
	ATN.SHIHARAI_SOUFU_POST AS POST,
	ATN.SHIHARAI_SOUFU_ADDRESS1 AS ADDRESS1,
	ATN.SHIHARAI_SOUFU_ADDRESS2 AS ADDRESS2,
	ATN.SHIHARAI_SOUFU_BUSHO AS BUSHO,
	ATN.SHIHARAI_SOUFU_TANTOU AS TANTOU
FROM
	/*IF printKubun == 1*/M_TORIHIKISAKI AS MT 
						   LEFT JOIN M_TORIHIKISAKI_SHIHARAI AS ATN ON MT.TORIHIKISAKI_CD = ATN.TORIHIKISAKI_CD/*END*/
	/*IF printKubun == 2*/M_GYOUSHA AS ATN/*END*/
	/*IF printKubun == 3*/M_GENBA AS ATN/*END*/
WHERE
/*IF printHouhou == 1*/
	/*IF printKubun == 1 && kobetsuShitei != ''*/ATN.TORIHIKISAKI_CD IN /*$kobetsuShitei*/
						   AND ATN.TORIHIKI_KBN_CD = 2/*END*/
	/*IF printKubun == 2 && kobetsuShitei != ''*/ATN.GYOUSHA_CD IN /*$kobetsuShitei*//*END*/
	/*IF printKubun == 3 && kobetsuShitei != '' && GyoushaCd != ''*/ATN.GYOUSHA_CD = /*GyoushaCd*/
						   AND ATN.GENBA_CD IN /*$kobetsuShitei*//*END*/
/*END*/
/*IF printHouhou == 2*/
	/*IF printKubun == 1 && TorihikisakiCd != ''*/ATN.TORIHIKISAKI_CD = /*TorihikisakiCd*/
						   AND ATN.TORIHIKI_KBN_CD = 2/*END*/
	/*IF printKubun == 2 && GyoushaCd != ''*/ATN.GYOUSHA_CD = /*GyoushaCd*//*END*/
	/*IF printKubun == 3 && GyoushaCd != '' && GenbaCd != ''*/ATN.GYOUSHA_CD = /*GyoushaCd*/ AND ATN.GENBA_CD = /*GenbaCd*//*END*/
/*END*/