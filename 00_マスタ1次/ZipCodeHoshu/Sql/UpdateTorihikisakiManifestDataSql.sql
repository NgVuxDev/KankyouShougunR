UPDATE dbo.M_TORIHIKISAKI
   SET MANI_HENSOUSAKI_POST = Replace(MANI_HENSOUSAKI_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, MANI_HENSOUSAKI_ADDRESS1 = Replace(MANI_HENSOUSAKI_ADDRESS1, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
       /*IF oldAddress != null && newAddress != null*/, MANI_HENSOUSAKI_ADDRESS2 = Replace(MANI_HENSOUSAKI_ADDRESS2, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
 WHERE TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/'000001'