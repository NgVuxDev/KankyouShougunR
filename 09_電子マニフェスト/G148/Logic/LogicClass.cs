using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Data;
using Shougun.Core.ElectronicManifest.TopHeNoJouhouKoukai.DTO;
using Shougun.Core.ElectronicManifest.TopHeNoJouhouKoukai.DAO;

namespace Shougun.Core.ElectronicManifest.TopHeNoJouhouKoukai.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        ///<summary>
        ///通知情報結果のDao
        ///</summary>
        private JTR24DaoCls JTR24Dao;

        ///<summary>
        ///通知情報結果のDao
        ///</summary>
        private OTR24DaoCls OTR24Dao;

        ///<summary>
        ///キュー情報のDao
        ///</summary>
        private QIDaoCls QIDao;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass()
        {
            LogUtility.DebugMethodStart();

            this.dto = new DTOClass();
            this.JTR24Dao = DaoInitUtility.GetComponent<JTR24DaoCls>();
            this.OTR24Dao = DaoInitUtility.GetComponent<OTR24DaoCls>();
            this.QIDao = DaoInitUtility.GetComponent<QIDaoCls>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 初期表示のデータ取得処理
        /// </summary>
        /// <returns>
        ///   string[] : nullの場合、メッセージを設定しない。
        /// 　string[0]：①nullの場合、メッセージを設定しない。
        /// 　           ②null以外の場合、重要な通知件数
        /// 　string[1]：①nullの場合、メッセージを設定しない。
        /// 　　　　　　 ②null以外の場合、お知らせ通知件数
        /// 　string[2]：①nullの場合、メッセージを設定しない。
        /// 　           ②null以外の場合、通信履歴件数
        /// </returns>
        public string[] SelectKensuuData()
        {
            LogUtility.DebugMethodStart();

            string[] kensuu = null;

            try
            {
                // 検索結果
                DataTable juuyouRst = new DataTable();
                DataTable oshiraseRst = new DataTable();
                DataTable queInfoRst = new DataTable();

                // システム設定を取得する
                M_SYS_INFO sysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllDataForCode("0");

                if (sysInfo != null)
                {
                    if (sysInfo.MANIFEST_JYUUYOU_DISP_KBN == 1 || sysInfo.MANIFEST_OSHIRASE_DISP_KBN == 1
                        || sysInfo.MANIFEST_RIREKI_DISP_KBN == 1)
                    {
                        kensuu = new string[3];
                    }

                    // [重要な通知]が[1.表示する]の場合
                    if (sysInfo.MANIFEST_JYUUYOU_DISP_KBN == 1)
                    {
                        // 重要な通知件数
                        juuyouRst = this.JTR24Dao.GetDataForEntity(dto);

                        kensuu[0] = juuyouRst.Rows.Count.ToString();
                    }

                    // [お知らせ通知]が[1.表示する]の場合
                    if (sysInfo.MANIFEST_OSHIRASE_DISP_KBN == 1)
                    {
                        // お知らせ通知件数
                        oshiraseRst = this.OTR24Dao.GetDataForEntity(dto);

                        kensuu[1] = oshiraseRst.Rows.Count.ToString();
                    }

                    // [通信履歴]が[1.表示する]の場合
                    if (sysInfo.MANIFEST_RIREKI_DISP_KBN == 1)
                    {
                        // 通信履歴件数
                        queInfoRst = this.QIDao.GetDataForEntity(dto);

                        if (queInfoRst.Rows.Count > 0)
                        {
                            kensuu[2] = queInfoRst.Rows.Count.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
            return kensuu;
        }

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
    }
}
