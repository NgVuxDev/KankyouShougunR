UPDATE dbo.M_GENBA
   SET MANI_HENSOUSAKI_POST = Replace(MANI_HENSOUSAKI_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, MANI_HENSOUSAKI_ADDRESS1 = Replace(MANI_HENSOUSAKI_ADDRESS1, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
       /*IF oldAddress != null && newAddress != null*/, MANI_HENSOUSAKI_ADDRESS2 = Replace(MANI_HENSOUSAKI_ADDRESS2, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
 WHERE GYOUSHA_CD = /*data.GYOUSHA_CD*/'000001'
   AND GENBA_CD = /*data.GENBA_CD*/'000001'