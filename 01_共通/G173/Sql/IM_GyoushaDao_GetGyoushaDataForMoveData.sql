SELECT 
	T1.*
FROM dbo.M_GYOUSHA AS T1 
WHERE 
1=1
--/*IF !torihikisakiCd.IsNull && ''!=torihikisakiCd*/AND T1.TORIHIKISAKI_CD = /*torihikisakiCd*/0 /*END*/
/*IF !gyoushaCd.IsNull && ''!=gyoushaCd*/AND T1.GYOUSHA_CD = /*gyoushaCd*/0 /*END*/
