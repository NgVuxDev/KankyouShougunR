UPDATE dbo.M_TORIHIKISAKI_SEIKYUU
   SET SEIKYUU_SOUFU_POST = Replace(SEIKYUU_SOUFU_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, SEIKYUU_SOUFU_ADDRESS1 = Replace(SEIKYUU_SOUFU_ADDRESS1, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
       /*IF oldAddress != null && newAddress != null*/, SEIKYUU_SOUFU_ADDRESS2 = Replace(SEIKYUU_SOUFU_ADDRESS2, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
 WHERE TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/'000001'