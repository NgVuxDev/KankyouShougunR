SELECT            MTS.TORIHIKISAKI_CD, 
                  MT.TORIHIKISAKI_NAME_RYAKU, 
				  MTS.SHIMEBI1, 
				  MTS.SHIMEBI2, 
                  MTS.SHIMEBI3, 
				  MT.TORIHIKISAKI_ADDRESS1, 
				  MT.TORIHIKISAKI_ADDRESS2, 
				  MT.TORIHIKISAKI_TEL, 
                  MT.TORIHIKISAKI_FAX
FROM              M_TORIHIKISAKI_SEIKYUU MTS LEFT OUTER JOIN
                  M_TORIHIKISAKI MT ON 
				  MTS.TORIHIKISAKI_CD = MT.TORIHIKISAKI_CD
WHERE
1 = 1
/*IF data.TorihikisakiCd != null*/ AND MTS.TORIHIKISAKI_CD = /*data.TorihikisakiCd*/0/*END*/
/*IF data.Shimebi1 != null*/
             AND (MTS.SHIMEBI1 = /*data.Shimebi1*/31 OR
				   MTS.SHIMEBI2 = /*data.Shimebi2*/31 OR
				   MTS.SHIMEBI3 = /*data.Shimebi3*/31)
/*END*/

ORDER BY MTS.TORIHIKISAKI_CD