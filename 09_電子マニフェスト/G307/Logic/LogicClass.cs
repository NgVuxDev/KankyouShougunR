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
using Seasar.Framework.Exceptions;
using Shougun.Core.Message;
using Seasar.Dao;

namespace Shougun.Core.ElectronicManifest.SyobunnShuryouHoukokuIkkatuNyuuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        // 20151110 katen #12048 「システム日付」の基準作成、適用 start
        ///// <summary>
        ///// BaseForm
        ///// </summary>
        //internal BusinessBaseForm parentForm;
        internal DateTime now;
        // 20151110 katen #12048 「システム日付」の基準作成、適用 start

        /// <summary>
        /// DTO
        /// </summary>
        private GetInputInfoDTOCls inputInfodto;


        /// <summary>
        /// 紐付したい対象検索結果
        /// </summary>
        private DataTable SearchResult { get; set; }

        /// <summary>
        /// 一括入力内容取得検索Dao
        /// </summary>
        private GetInputInfoDaoCls GetInputInfoDao;

        ///<summary>
        ///車輌マスタのDao
        ///</summary>
        private GetSyaryouDaoCls SyaryouDao;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            form = targetForm;
            this.inputInfodto = new GetInputInfoDTOCls();
            this.GetInputInfoDao = DaoInitUtility.GetComponent<GetInputInfoDaoCls>();
            this.SyaryouDao = DaoInitUtility.GetComponent<GetSyaryouDaoCls>();

            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20151110 katen #12048 「システム日付」の基準作成、適用 start
                //this.parentForm = (BusinessBaseForm)this.form.Parent;
                if (this.form.Parent == null)
                {
                    this.now = (this.form as SuperForm).sysDate;
                }
                else
                {
                    this.now = (this.form.Parent as BusinessBaseForm).sysDate;
                }
                // 20151110 katen #12048 「システム日付」の基準作成、適用 end

                // 報告区分
                form.cantxt_houkokuKubunn.Text = "1";
                // 処分終了日区分
                form.cantxt_SyobunnSyuuryouhi.Text = "1";
                // 廃棄物受領日区分
                form.cantxt_haikibutuJyuryouhi.Text = "1";
                // 運搬担当者区分
                form.cantxt_unnnpannTanntousya.Text = "1";
                // 車輌番号区分
                form.cantxt_Syaryou.Text = "1";
                // 受入量区分
                form.cantxt_ukeireryouKbn.Text = "1";

                // ボタンを初期化
                form.bt_allSelect.Text = "[F7]\r\n全て選択";
                form.bt_Input.Text = "[F9]\r\n入力";
                form.bt_Erase.Text = "[F11]\r\n消去";
                form.bt_Close.Text = "[F12]\r\n閉じる";

                //画面では、最初の行の情報を初期利用（通常利用では自社のみなので 1業者だけになる想定。複数自社はパッケージでは現状想定無しの方針）

                // 運搬業者の加入者番号
                form.cantxt_hideEdiId.Text = form.inputInfo[0].GYOUSYA_EDI_MEMBER_ID;
                // 運搬先業者の加入者番号
                form.cantxt_hideToEdiId.Text = form.inputInfo[0].SAKI_GYOUSYA_EDI_MEMBER_ID;
                // 運搬業者の業者CD
                form.cantxt_hideGyoushaCd.Text = form.inputInfo[0].GYOUSHA_CD;

                //処分終了日
                // 20151110 katen #12048 「システム日付」の基準作成、適用 start
                //form.cDt_SyobunnSyuuryouhi.Value = this.parentForm.sysDate;
                form.cDt_SyobunnSyuuryouhi.Value = now;
                // 20151110 katen #12048 「システム日付」の基準作成、適用 end
                //廃棄物受領日
                // 20151110 katen #12048 「システム日付」の基準作成、適用 start
                //form.cDt_haikibutuJyuryouhi.Value = this.parentForm.sysDate;
                form.cDt_haikibutuJyuryouhi.Value = this.now;
                // 20151110 katen #12048 「システム日付」の基準作成、適用 end
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面クリア処理
        /// </summary>
        public void clearWindow()
        {
            LogUtility.DebugMethodStart();

            //報告区分
            form.cantxt_houkokuKubunn.Text = "1";
            //処分終了日
            // 20151110 katen #12048 「システム日付」の基準作成、適用 start
            //form.cDt_SyobunnSyuuryouhi.Value = this.parentForm.sysDate;
            form.cDt_SyobunnSyuuryouhi.Value = this.now;
            // 20151110 katen #12048 「システム日付」の基準作成、適用 end
            //処分終了日区分
            form.cantxt_SyobunnSyuuryouhi.Text = "1";
            //処分担当者コード
            form.cantxt_syobunnTanntousyaCD.Text = string.Empty;
            //処分担当者名称
            form.cantxt_syobunnTanntousyaName.Text = string.Empty;
            //報告担当者コード
            form.cantxt_HoukokuTanntousyaCD.Text = string.Empty;
            //報告担当者名称
            form.ctxt_HoukokuTanntousyaName.Text = string.Empty;
            //廃棄物受領日
            // 20151110 katen #12048 「システム日付」の基準作成、適用 start
            form.cDt_haikibutuJyuryouhi.Value = this.now;
            // 20151110 katen #12048 「システム日付」の基準作成、適用 end
            //廃棄物受領日区分
            form.cantxt_haikibutuJyuryouhi.Text = "1";
            //運搬担当者コード
            form.cantxt_UnnpannTanntousyaCD.Text = string.Empty;
            //運搬担当者名称
            form.ctxt_UnnpannTanntousyaName.Text = string.Empty;
            //運搬担当者区分
            form.cantxt_unnnpannTanntousya.Text = "1";
            //車輌番号コード
            form.cantxt_SyaryouCD.Text = string.Empty;
            //車輌名称
            form.ctxt_SyaryouName.Text = string.Empty;
            //車輌番号区分
            form.cantxt_Syaryou.Text = "1";
            //受入量
            form.cantxt_ukeireryou.Text = string.Empty;
            //受入量区分
            form.cantxt_ukeireryouKbn.Text = "1";
            //受入量単位コード
            form.cantxt_ukeireryouTanniCD.Text = string.Empty;
            //受入量単位名称
            form.ctxt_ukeireryouTanniName.Text = string.Empty;
            //備考
            form.ctxt_bikou.Text = string.Empty;

            // 名称を全て選択に変更します
            form.bt_allSelect.Text = "[F7]\r\n全て選択";

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 全て選択処理
        /// </summary>
        public void selectAllorNo()
        {
            LogUtility.DebugMethodStart();

            if (form.bt_allSelect.Text.Equals("[F7]\r\n全て選択"))
            {
                //報告区分
                form.cantxt_houkokuKubunn.Text = "2";
                //処分終了日区分
                form.cantxt_SyobunnSyuuryouhi.Text = "2";
                //廃棄物受領日区分
                form.cantxt_haikibutuJyuryouhi.Text = "2";
                //運搬担当者区分
                form.cantxt_unnnpannTanntousya.Text = "2";
                //車輌番号区分
                form.cantxt_Syaryou.Text = "2";
                //受入量区分
                form.cantxt_ukeireryouKbn.Text = "2";
                // 名称を全て解除に変更します
                form.bt_allSelect.Text = "[F7]\r\n全て解除";
            }
            else
            {
                //報告区分
                form.cantxt_houkokuKubunn.Text = "1";
                //処分終了日区分
                form.cantxt_SyobunnSyuuryouhi.Text = "1";
                //廃棄物受領日区分
                form.cantxt_haikibutuJyuryouhi.Text = "1";
                //運搬担当者区分
                form.cantxt_unnnpannTanntousya.Text = "1";
                //受入量区分
                form.cantxt_ukeireryouKbn.Text = "1";
                //車輌番号区分
                form.cantxt_Syaryou.Text = "1";
                // 名称を全て選択に変更します
                form.bt_allSelect.Text = "[F7]\r\n全て選択";
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 一括出力実行
        /// </summary>
        public bool allOutputExe()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var l = new List<OutputInfoDTOCls>();

                foreach (var dto in this.form.inputInfo)
                {
                    var output = new OutputInfoDTOCls();
                    l.Add(output);

                    output.KANRI_ID = dto.KANRI_ID;
                    output.SEQ = dto.SEQ;

                    inputInfodto = new GetInputInfoDTOCls()
                    {
                        KANRI_ID = dto.KANRI_ID
                        ,
                        SEQ = dto.SEQ
                        ,
                        GYOUSHA_CD = dto.GYOUSHA_CD
                    };

                    // 一括入力内容検索
                    inputInfoSerch();

                    if (SearchResult == null || SearchResult.Rows.Count == 0)
                    {
                        MessageBoxShowLogic msg = new MessageBoxShowLogic();
                        msg.MessageBoxShow("E252", "");
                        return false;
                    }

                    // 報告区分
                    output.HOUKOKU_KUBUNN = form.cantxt_houkokuKubunn.Text;
                    // 処分終了日区分
                    if (form.cantxt_SyobunnSyuuryouhi.Text.Equals("1"))
                    {
                        // 処分終了日
                        if (!String.IsNullOrEmpty(form.cDt_SyobunnSyuuryouhi.Text))
                        {
                            output.SYOBUNN_SYUURYOUHI = form.cDt_SyobunnSyuuryouhi.Text.Replace("/", "").Substring(0, 8);
                        }
                        else
                        {
                            output.SYOBUNN_SYUURYOUHI = "";
                        }
                    }
                    else if (form.cantxt_SyobunnSyuuryouhi.Text.Equals("2"))
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 処分終了日
                            output.SYOBUNN_SYUURYOUHI = SearchResult.Rows[0]["R18_SYOBUNN_SYUURYOUHI"].ToString();
                        }
                    }
                    else if (form.cantxt_SyobunnSyuuryouhi.Text.Equals("3"))
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 処分終了日
                            output.SYOBUNN_SYUURYOUHI = SearchResult.Rows[0]["R19_SYOBUNN_SYUURYOUHI"].ToString();
                        }
                    }
                    // 処分担当者コード
                    output.SYOBUNN_TANNTOUSYA_CD = form.cantxt_syobunnTanntousyaCD.Text;
                    // 処分担当者名称
                    output.SYOBUNN_TANNTOUSYA_NAME = form.cantxt_syobunnTanntousyaName.Text;
                    // 報告担当者コード
                    output.HOUKOKU_TANNTOUSYA_CD = form.cantxt_HoukokuTanntousyaCD.Text;
                    // 報告担当者名称
                    output.HOUKOKU_TANNTOUSYA_NAME = form.ctxt_HoukokuTanntousyaName.Text;
                    // 廃棄物受領日区分
                    if (form.cantxt_haikibutuJyuryouhi.Text.Equals("1"))
                    {
                        // 廃棄物受領日
                        if (!String.IsNullOrEmpty(form.cDt_haikibutuJyuryouhi.Text))
                        {
                            output.HAIKIBUTU_JYURYOUHI = form.cDt_haikibutuJyuryouhi.Text.Replace("/", "").Substring(0, 8);
                        }
                        else
                        {
                            output.HAIKIBUTU_JYURYOUHI = "";
                        }


                    }
                    else if (form.cantxt_haikibutuJyuryouhi.Text.Equals("2"))
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 廃棄物受領日
                            output.HAIKIBUTU_JYURYOUHI = SearchResult.Rows[0]["R18_HAIKIBUTU_JYURYOUHI"].ToString();
                        }
                    }
                    else if (form.cantxt_haikibutuJyuryouhi.Text.Equals("3"))
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 廃棄物受領日
                            output.HAIKIBUTU_JYURYOUHI = SearchResult.Rows[0]["R19_HAIKIBUTU_JYURYOUHI"].ToString();
                        }
                    }
                    // 運搬担当者区分
                    if (form.cantxt_unnnpannTanntousya.Text.Equals("1"))
                    {
                        // 運搬担当者コード
                        output.UNNPANN_TANNTOUSYA_CD = form.cantxt_UnnpannTanntousyaCD.Text;
                        // 運搬担当者名称
                        output.UNNPANN_TANNTOUSYA_NAME = form.ctxt_UnnpannTanntousyaName.Text;
                    }
                    else if (form.cantxt_unnnpannTanntousya.Text.Equals("2"))
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 運搬担当者名称
                            output.UNNPANN_TANNTOUSYA_NAME = SearchResult.Rows[0]["R19_UNNPANN_TANNTOUSYA_NAME1"].ToString();
                        }
                    }
                    else if (form.cantxt_unnnpannTanntousya.Text.Equals("3"))
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 運搬担当者名称
                            output.UNNPANN_TANNTOUSYA_NAME = SearchResult.Rows[0]["R19_UNNPANN_TANNTOUSYA_NAME2"].ToString();
                        }
                    }
                    // 車輌番号区分
                    if (form.cantxt_Syaryou.Text.Equals("1"))
                    {
                        // 車輌番号コード
                        output.SYARYOU_CD = form.cantxt_SyaryouCD.Text;
                        // 車輌名称
                        output.SYARYOU_NAME = form.ctxt_SyaryouName.Text;
                    }
                    else if (form.cantxt_Syaryou.Text.Equals("2"))
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 車輌番号コード
                            output.SYARYOU_CD = SearchResult.Rows[0]["SYARYOU_CD"].ToString();
                            // 車輌名称
                            output.SYARYOU_NAME = SearchResult.Rows[0]["SYARYOU_NAME"].ToString();
                        }
                    }
                    else if (form.cantxt_Syaryou.Text.Equals("3"))
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 車輌番号コード
                            output.SYARYOU_CD = SearchResult.Rows[0]["UPN_SYARYOU_CD"].ToString();
                            // 車輌名称
                            output.SYARYOU_NAME = SearchResult.Rows[0]["UPN_SYARYOU_NAME"].ToString();
                        }
                    }
                    //受入量区分
                    if (form.cantxt_ukeireryouKbn.Text.Equals("1"))
                    {
                        // 受入量
                        output.UKEIRERYOU = form.cantxt_ukeireryou.Text;
                        // 受入量単位コード
                        output.UKEIRERYOU_TANNI_CD = form.cantxt_ukeireryouTanniCD.Text;
                        // 受入量単位名称
                        output.UKEIRERYOU_TANNI_NAME = form.ctxt_ukeireryouTanniName.Text;
                    }
                    else if (form.cantxt_ukeireryouKbn.Text.Equals("2"))
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 受入量
                            output.UKEIRERYOU = SearchResult.Rows[0]["R18_UKEIRERYOU"].ToString();
                            // 受入量単位コード
                            output.UKEIRERYOU_TANNI_CD = SearchResult.Rows[0]["R18_SYARYOU_CD"].ToString();
                            // 受入量単位名称
                            output.UKEIRERYOU_TANNI_NAME = SearchResult.Rows[0]["R18_SYARYOU_NAME"].ToString();
                        }
                    }
                    else if (form.cantxt_ukeireryouKbn.Text.Equals("3"))
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 受入量
                            output.UKEIRERYOU = SearchResult.Rows[0]["R19_UKEIRERYOU"].ToString();
                            // 受入量単位コード
                            output.UKEIRERYOU_TANNI_CD = SearchResult.Rows[0]["R19_SYARYOU_CD"].ToString();
                            // 受入量単位名称
                            output.UKEIRERYOU_TANNI_NAME = SearchResult.Rows[0]["R19_SYARYOU_NAME"].ToString();
                        }
                    }
                    // 備考
                    output.BIKO = form.ctxt_bikou.Text;

                    // 登録情報承認待ちフラグ
                    if (SearchResult.Rows.Count > 0)
                    {
                        output.KIND = SearchResult.Rows[0]["KIND"] != DBNull.Value ? (Decimal)SearchResult.Rows[0]["KIND"] : Decimal.Zero;
                    }
                }

                // 出力内容
                this.form.outputInfo = l.ToArray();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("allOutputExe", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("allOutputExe", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }


        /// <summary>
        /// 入力内容をDBから検索する
        /// </summary>
        internal void inputInfoSerch()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // 検索実行する
                this.SearchResult = GetInputInfoDao.GetDataForEntity(inputInfodto);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌マスタのチェック
        /// </summary>
        /// <param name="syaryouCD">車輌CD</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <returns>DataTable</returns>
        public DataTable Chksyaryou(string syaryouCD, string gyoushaCd, out bool catchErr)
        {
            catchErr = false;
            try
            {
                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                DataTable result = new DataTable();

                sql.Append(" select M_SHARYOU.SHARYOU_CD AS SHARYOU_CD, ");
                sql.Append(" M_SHARYOU.SHARYOU_NAME_RYAKU AS SHARYOU_NAME_RYAKU ");
                sql.Append(" FROM M_SHARYOU ");
                sql.Append(" WHERE 1 = 1 ");
                // 業者CD
                if (!string.Empty.Equals(gyoushaCd))
                {
                    sql.Append(" AND GYOUSHA_CD = '" + gyoushaCd.PadLeft(6, '0') + "'");
                }
                // 車輌CD
                if (!string.Empty.Equals(syaryouCD))
                {
                    sql.Append(" AND SHARYOU_CD = '" + syaryouCD.PadLeft(6, '0') + "'");
                }
                sql.Append(" group by M_SHARYOU.SHARYOU_CD, M_SHARYOU.SHARYOU_NAME_RYAKU ");
                // 検索結果
                result = SyaryouDao.GetDataForEntity(sql.ToString());

                return result;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Chksyaryou", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
                catchErr = true;
                return null;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Chksyaryou", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = true;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Chksyaryou", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
                return null;
            }
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
