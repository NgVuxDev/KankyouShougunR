SELECT DISTINCT N'業者入力' AS NAME FROM M_GYOUSHA WHERE URIAGE_GURUPU_CD IN /*GURUPU_CD*/('') AND DELETE_FLG = 'False'
UNION
SELECT DISTINCT N'業者入力' AS NAME FROM M_GYOUSHA WHERE SHIHARAI_GURUPU_CD IN /*GURUPU_CD*/('') AND DELETE_FLG = 'False'
-- kiem tra GURUPU_CD co su dung o cac bang khac hay khong neu co thi tra ket qua la man hinh đang su dung va k cho xóa
-- neu khong thi xoa