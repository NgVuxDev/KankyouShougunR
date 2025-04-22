SELECT 
	-- �Ǝҏ��
	sha.GYOUSHA_CD AS �Ǝ�CD
	,sha.GYOUSHA_NAME1 AS �ƎҖ�1
	,sha.GYOUSHA_NAME2 AS �ƎҖ�2
	,sha.POST AS �ƎҗX�֔ԍ�
	,sha.ADDRESS1 AS �ƎҏZ��1
	,sha.ADDRESS2 AS �ƎҏZ��2
	,sha.GYOUSHA_TEL AS �Ǝғd�b�ԍ�
	,sha.GYOUSHA_FAX AS �Ǝ�FAX
	,shaShain.SHAIN_NAME AS �Ǝ҉c�ƒS����
	-- ������
	,ba.GENBA_CD AS ����CD
	,ba.GENBA_NAME1 AS ���ꖼ1
	,ba.GENBA_NAME2 AS ���ꖼ2
	,ba.POST AS ����X�֔ԍ�
	,ba.ADDRESS1 AS ����Z��1
	,ba.ADDRESS2 AS ����Z��2
	,ba.GENBA_TEL AS ����d�b�ԍ�
	,ba.GENBA_FAX AS ����FAX
	,baShain.SHAIN_NAME AS ����c�ƒS����
	-- �Ǝ�-��{���
	,shaTorihiki.TORIHIKISAKI_CD AS �ƎҎ����CD
	,shaTorihiki.TORIHIKISAKI_NAME1 AS �ƎҎ���於1
	,shaTorihiki.TORIHIKISAKI_NAME2 AS �ƎҎ���於2
	,shaTorihiki.BUSHO AS �Ǝҕ���
	,shaTorihiki.TANTOUSHA AS �ƎҒS����
	,shaTorihiki.SHUUKEI_ITEM_CD AS �ƎҏW�v����CD
	,shaItem.FREE_ITEM_NAME AS �ƎҏW�v���ږ�
	,shaTorihiki.GYOUSHU_CD AS �ƎҋƎ�CD
	,shaG.GYOUSHU_NAME AS �ƎҋƎ햼
	,shaTorihiki.BIKOU1 AS �ƎҔ��l1
	,shaTorihiki.BIKOU2 AS �ƎҔ��l2
	,shaTorihiki.BIKOU3 AS �ƎҔ��l3
	,shaTorihiki.BIKOU4 AS �ƎҔ��l4
	-- ����-��{���
	,baTorihiki.TORIHIKISAKI_CD AS ��������CD
	,baTorihiki.TORIHIKISAKI_NAME1 AS �������於1
	,baTorihiki.TORIHIKISAKI_NAME2 AS �������於2
	,baTorihiki.BUSHO AS ���ꕔ��
	,baTorihiki.TANTOUSHA AS ����S����
	,baTorihiki.SHUUKEI_ITEM_CD AS ����W�v����CD
	,baItem.FREE_ITEM_NAME AS ����W�v���ږ�
	,baTorihiki.GYOUSHU_CD AS ����Ǝ�CD
	,baG.GYOUSHU_NAME AS ����Ǝ햼
	,baTorihiki.BIKOU1 AS ������l1
	,baTorihiki.BIKOU2 AS ������l2
	,baTorihiki.BIKOU3 AS ������l3
	,baTorihiki.BIKOU4 AS ������l4
	-- �Ǝ�-�������
	,shaTorihiki.SEIKYUU_SHIMEBI1 AS �ƎҒ���1
	,shaTorihiki.SEIKYUU_SHIMEBI2 AS �ƎҒ���2
	,shaTorihiki.SEIKYUU_SHIMEBI3 AS �ƎҒ���3
	,shaTorihiki.SEIKYUU_HICCHAKUBI AS �ƎҐ������K����
	,shaTorihiki.KAISHUU_MONTH AS �Ǝ҉����
	,shaTorihiki.KAISHUU_DAY AS �Ǝ҉����
	,shaK.NYUUSHUKKIN_KBN_NAME AS �Ǝ҉�����@
	,shaTorihiki.SEIKYUU_JOUHOU1 AS �ƎҐ������1
	,shaTorihiki.SEIKYUU_JOUHOU2 AS �ƎҐ������2
	,shaTorihiki.KAISHI_URIKAKE_ZANDAKA AS �ƎҊJ�n���|�c��
	,shaTorihiki.SEIKYUUSHO_SHOSHIKI AS �ƎҐ���������1
	,shaTorihiki.SEIKYUUSHO_SHOSHIKI_MEISAI AS �ƎҐ���������2
	-- ����-�������
	,baTorihiki.SEIKYUU_SHIMEBI1 AS �������1
	,baTorihiki.SEIKYUU_SHIMEBI2 AS �������2
	,baTorihiki.SEIKYUU_SHIMEBI3 AS �������3
	,baTorihiki.SEIKYUU_HICCHAKUBI AS ���ꐿ�����K����
	,baTorihiki.KAISHUU_MONTH AS ��������
	,baTorihiki.KAISHUU_DAY AS ��������
	,baK.NYUUSHUKKIN_KBN_NAME AS ���������@
	,baTorihiki.SEIKYUU_JOUHOU1 AS ���ꐿ�����1
	,baTorihiki.SEIKYUU_JOUHOU2 AS ���ꐿ�����2
	,baTorihiki.KAISHI_URIKAKE_ZANDAKA AS ����J�n���|�c��
	,baTorihiki.SEIKYUUSHO_SHOSHIKI AS ���ꐿ��������1
	,baTorihiki.SEIKYUUSHO_SHOSHIKI_MEISAI AS ���ꐿ��������2
FROM M_GYOUSHA AS sha 
	INNER JOIN M_GENBA AS ba ON sha.GYOUSHA_CD = ba.GENBA_CD 
	LEFT OUTER JOIN M_TORIHIKISAKI AS shaTorihiki ON sha.TORIHIKISAKI_CD = shaTorihiki.TORIHIKISAKI_CD 
	LEFT OUTER JOIN M_TORIHIKISAKI AS baTorihiki ON ba.TORIHIKISAKI_CD = baTorihiki.TORIHIKISAKI_CD 
	LEFT OUTER JOIN M_SHAIN AS shaShain ON sha.EIGYOU_TANTOU_CD = shaShain.SHAIN_CD 
	LEFT OUTER JOIN M_SHAIN AS baShain ON ba.EIGYOU_TANTOU_CD = baShain.SHAIN_CD 
	LEFT OUTER JOIN M_FREE_ITEM AS shaItem ON shaTorihiki.SHUUKEI_ITEM_CD = shaItem.FREE_ITEM_CD 
	LEFT OUTER JOIN M_FREE_ITEM AS baItem ON baTorihiki.SHUUKEI_ITEM_CD = baItem.FREE_ITEM_CD 
	LEFT OUTER JOIN M_GYOUSHU AS shaG ON shaTorihiki.GYOUSHU_CD = shaG.GYOUSHU_CD 
	LEFT OUTER JOIN M_GYOUSHU AS baG ON baTorihiki.GYOUSHU_CD = baG.GYOUSHU_CD
	LEFT OUTER JOIN M_NYUUSHUKKIN_KBN AS shaK ON shaTorihiki.KAISHUU_HOUHOU = shaK.NYUUSHUKKIN_KBN_CD
	LEFT OUTER JOIN M_NYUUSHUKKIN_KBN AS baK ON baTorihiki.KAISHUU_HOUHOU = baK.NYUUSHUKKIN_KBN_CD
WHERE 
	(ba.GYOUSHA_CD = /*gyoushaCD*/'') 
	AND (ba.GENBA_CD = /*genbaCD*/'')