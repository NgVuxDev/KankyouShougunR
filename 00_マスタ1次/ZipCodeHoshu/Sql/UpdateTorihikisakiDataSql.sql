UPDATE dbo.M_TORIHIKISAKI
   SET TORIHIKISAKI_POST = Replace(TORIHIKISAKI_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, TORIHIKISAKI_ADDRESS1 = Replace(TORIHIKISAKI_ADDRESS1, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
       /*IF oldAddress != null && newAddress != null*/, TORIHIKISAKI_ADDRESS2 = Replace(TORIHIKISAKI_ADDRESS2, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
 WHERE TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/'000001'