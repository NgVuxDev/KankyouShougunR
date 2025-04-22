using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data;

namespace r_framework.Dao
{
    [Bean(typeof(M_SMS_RECEIVER_LINK_GENBA))]
    public interface IM_SMS_RECEIVER_LINK_GENBADao : IchiranBaseDao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SMS_RECEIVER_LINK_GENBA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SMS_RECEIVER_LINK_GENBA data);

        int Delete(M_SMS_RECEIVER_LINK_GENBA data);

        /// <summary>
        /// 他の現場にも対象の携帯番号の存在チェック
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [Query("MOBILE_PHONE_NUMBER = /*data.MOBILE_PHONE_NUMBER*/ AND GYOUSHA_CD = /*data.GYOUSHA_CD*/ AND GENBA_CD = /*data.GENBA_CD*/")]
        M_SMS_RECEIVER_LINK_GENBA CheckOtherLinkGenba(M_SMS_RECEIVER_LINK_GENBA data);

        /// <summary>
        /// データ検索
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND MOBILE_PHONE_NUMBER = /*data.MOBILE_PHONE_NUMBER*/ AND GYOUSHA_CD = /*data.GYOUSHA_CD*/ AND GENBA_CD = /*data.GENBA_CD*/")]
        M_SMS_RECEIVER_LINK_GENBA GetLinkGenba(M_SMS_RECEIVER_LINK_GENBA data);

        /// <summary>
        /// リンクデータ削除（登録ボタン）
        /// </summary>
        /// <returns></returns>
        [Sql("DELETE FROM M_SMS_RECEIVER_LINK_GENBA WHERE SYSTEM_ID = /*data.SYSTEM_ID*/ AND MOBILE_PHONE_NUMBER = /*data.MOBILE_PHONE_NUMBER*/ AND GYOUSHA_CD = /*data.GYOUSHA_CD*/ AND GENBA_CD = /*data.GENBA_CD*/ ")]
        int DeleteLinkGenba(M_SMS_RECEIVER_LINK_GENBA data);

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ入力・一覧画面チェック用
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_SMS_RECEIVER_LINK_GENBA WHERE GYOUSHA_CD = /*gyoushaCd*/ AND GENBA_CD = /*genbaCd*/")]
        M_SMS_RECEIVER_LINK_GENBA CheckDataForSmsNyuuryoku(string gyoushaCd, string genbaCd);

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ入力画面呼び出し用
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_SMS_RECEIVER_LINK_GENBA WHERE GYOUSHA_CD = /*gyoushaCd*/ AND GENBA_CD = /*genbaCd*/")]
        List<M_SMS_RECEIVER_LINK_GENBA> GetDataForSmsNyuuryoku(string gyoushaCd, string genbaCd);
    }
}
