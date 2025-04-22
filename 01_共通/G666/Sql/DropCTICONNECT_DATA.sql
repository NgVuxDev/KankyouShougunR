-- 既に存在していれば削除
IF exists (SELECT * FROM sys.objects WHERE object_id = object_id('V_CTICONNECT_DATA'))
    DROP VIEW V_CTICONNECT_DATA