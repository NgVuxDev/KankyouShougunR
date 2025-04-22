UPDATE dbo.M_TORIHIKISAKI_SHIHARAI
   SET SHIHARAI_SOUFU_POST = Replace(SHIHARAI_SOUFU_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, SHIHARAI_SOUFU_ADDRESS1 = Replace(SHIHARAI_SOUFU_ADDRESS1, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
       /*IF oldAddress != null && newAddress != null*/, SHIHARAI_SOUFU_ADDRESS2 = Replace(SHIHARAI_SOUFU_ADDRESS2, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
 WHERE TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/'000001'