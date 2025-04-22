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

namespace Shougun.Core.ElectronicManifest.UnpanShuryouHoukokuIkkatuNyuuryoku
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

                // ボタンを初期化
                form.bt_allSelect.Text = "[F7]\r\n全て選択";
                form.bt_Input.Text = "[F9]\r\n入力";
                form.bt_Erase.Text = "[F11]\r\n消去";
                form.bt_Close.Text = "[F12]\r\n閉じる";
                // 引渡し日使用有無
                form.ccb_hikiwatasiHi.Checked = false;
                // 登録時運搬担当者使用有無
                form.ccb_hikiwatasiHi.Checked = false;
                // 引渡し量・単位使用有無
                form.ccb_hikiwatasiHi.Checked = false;
                // 登録時車輌番号使用有無
                form.ccb_hikiwatasiHi.Checked = false;

                //画面では、最初の行の情報を初期利用（通常利用では自社のみなので 1業者だけになる想定。複数自社はパッケージでは現状想定無しの方針）

                // 加入者番号
                form.cantxt_hideEdiId.Text = form.inputInfo[0].EDI_MEMBER_ID;
                // 運搬業者の業者CD
                form.cantxt_hideGyoushaCd.Text = form.inputInfo[0].GYOUSHA_CD;

                //運搬終了日
                // 20151110 katen #12048 「システム日付」の基準作成、適用 start
                //form.cDt_UnnpannSyuryouhi.Value = this.parentForm.sysDate;
                form.cDt_UnnpannSyuryouhi.Value = now;
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
            try
            {
                LogUtility.DebugMethodStart();

                // 運搬終了日
                // 20151110 katen #12048 「システム日付」の基準作成、適用 start
                //form.cDt_UnnpannSyuryouhi.Value = this.parentForm.sysDate;
                form.cDt_UnnpannSyuryouhi.Value = this.now;
                // 20151110 katen #12048 「システム日付」の基準作成、適用 end
                // 運搬担当者コード
                form.cantxt_UnnpannTanntousyaCD.Text = string.Empty;
                // 運搬担当者名
                form.ctxt_UnnpannTanntousyaName.Text = string.Empty;
                // 報告担当者CD
                form.cantxt_HoukokuTanntousyaCD.Text = string.Empty;
                // 報告担当者名
                form.ctxt_HoukokuTanntousyaName.Text = string.Empty;
                // 運搬量
                form.cantxt_Unnpannryou.Text = string.Empty;
                // 運搬量単位コード
                form.cantxt_UnnpannryouTanniCD.Text = string.Empty;
                // 運搬量単位名称
                form.ctxt_UnnpannryouTanniName.Text = string.Empty;
                // 有価物拾集量
                form.cantxt_YukabutuJyuusyuuryou.Text = string.Empty;
                // 有価物拾集量単位コード
                form.cantxt_YukabutuJyuusyuuryouTanniCD.Text = string.Empty;
                // 有価物拾集量単位名称
                form.ctxt_YukabutuJyuusyuuryouTanniName.Text = string.Empty;
                // 車輌番号
                form.cantxt_SyaryouCD.Text = string.Empty;
                // 車輌名称
                form.ctxt_SyaryouName.Text = string.Empty;
                // 備考
                form.ctxt_bikou.Text = string.Empty;
                // 引渡し日使用有無
                form.ccb_UnnpannTanntousya.Checked = false;
                // 登録時運搬担当者使用有無
                form.ccb_hikiwatasiRyou.Checked = false;
                // 引渡し量・単位使用有無
                form.ccb_hikiwatasiHi.Checked = false;
                // 登録時車輌番号使用有無
                form.ccb_SyaryouCD.Checked = false;
                // 名称を全て選択に変更します
                form.bt_allSelect.Text = "[F7]\r\n全て選択";
            }
            catch (Exception ex)
            {
                LogUtility.Error("clearWindow", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 全て選択処理
        /// </summary>
        public void selectAllorNo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (form.bt_allSelect.Text.Equals("[F7]\r\n全て選択"))
                {
                    // 引渡し日使用有無
                    form.ccb_hikiwatasiHi.Checked = true;
                    // 登録時運搬担当者使用有無
                    form.ccb_UnnpannTanntousya.Checked = true;
                    // 引渡し量・単位使用有無
                    form.ccb_hikiwatasiRyou.Checked = true;
                    // 登録時車輌番号使用有無
                    form.ccb_SyaryouCD.Checked = true;
                    // 名称を全て解除に変更します
                    form.bt_allSelect.Text = "[F7]\r\n全て解除";
                }
                else
                {
                    // 引渡し日使用有無
                    form.ccb_UnnpannTanntousya.Checked = false;
                    // 登録時運搬担当者使用有無
                    form.ccb_hikiwatasiRyou.Checked = false;
                    // 引渡し量・単位使用有無
                    form.ccb_hikiwatasiHi.Checked = false;
                    // 登録時車輌番号使用有無
                    form.ccb_SyaryouCD.Checked = false;
                    // 名称を全て選択に変更します
                    form.bt_allSelect.Text = "[F7]\r\n全て選択";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("selectAllorNo", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
                    output.UPN_ROUTE_NO = dto.UPN_ROUTE_NO;

                    inputInfodto = new GetInputInfoDTOCls()
                    {
                        KANRI_ID = dto.KANRI_ID
                        ,
                        SEQ = dto.SEQ
                        ,
                        UPN_ROUTE_NO = dto.UPN_ROUTE_NO
                        ,
                        GYOUSHA_CD = dto.GYOUSHA_CD
                    };

                    //一括入力内容検索
                    inputInfoSerch();

                    if (SearchResult == null || SearchResult.Rows.Count == 0)
                    {
                        MessageBoxShowLogic msg = new MessageBoxShowLogic();
                        msg.MessageBoxShow("E252", "");
                        return false;
                    }

                    // 引渡し日使用有無
                    if (form.ccb_hikiwatasiHi.Checked)
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 運搬終了日
                            output.UNNPANN_SYURYOUHI = SearchResult.Rows[0]["UNNPANN_SYURYOUHI"].ToString();
                        }
                    }
                    else
                    {
                        // 運搬終了日
                        if (!String.IsNullOrEmpty(form.cDt_UnnpannSyuryouhi.Text))
                        {
                            output.UNNPANN_SYURYOUHI = form.cDt_UnnpannSyuryouhi.Text.Replace("/", "").Substring(0, 8);
                        }
                        else
                        {
                            output.UNNPANN_SYURYOUHI = "";
                        }
                    }
                    // 登録時運搬担当者使用有無
                    if (form.ccb_UnnpannTanntousya.Checked)
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 運搬担当者名
                            output.UNNPANN_TANNTOUSYA_NAME = SearchResult.Rows[0]["UNNPANN_TANNTOUSYA_NAME"].ToString();
                        }
                    }
                    else
                    {
                        // 運搬担当者コード
                        output.UNNPANN_TANNTOUSYA_CD = form.cantxt_UnnpannTanntousyaCD.Text;
                        // 運搬担当者名
                        output.UNNPANN_TANNTOUSYA_NAME = form.ctxt_UnnpannTanntousyaName.Text;
                    }
                    // 報告担当者CD
                    output.HOUKOKU_TANNTOUSYA_CD = form.cantxt_HoukokuTanntousyaCD.Text;
                    // 報告担当者名
                    output.HOUKOKU_TANNTOUSYA_NAME = form.ctxt_HoukokuTanntousyaName.Text;
                    // 引渡し量・単位使用有無
                    if (form.ccb_hikiwatasiRyou.Checked)
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 運搬量
                            output.UNNPANN_RYOU = SearchResult.Rows[0]["UNNPANN_RYOU"].ToString();
                            // 運搬量単位コード
                            output.UNNPANNRYOU_TANNI_CD = SearchResult.Rows[0]["UNNPANNRYOU_TANNI_CD"].ToString();
                            // 運搬量単位名称
                            output.UNNPANNRYOU_TANNI_NAME = SearchResult.Rows[0]["UNNPANNRYOU_TANNI_NAME"].ToString();
                        }
                    }
                    else
                    {
                        // 運搬量
                        output.UNNPANN_RYOU = form.cantxt_Unnpannryou.Text;
                        // 運搬量単位コード
                        output.UNNPANNRYOU_TANNI_CD = form.cantxt_UnnpannryouTanniCD.Text;
                        // 運搬量単位名称
                        output.UNNPANNRYOU_TANNI_NAME = form.ctxt_UnnpannryouTanniName.Text;
                    }
                    // 有価物拾集量
                    output.YUKABUTU_JYUUSYUU_RYOU = form.cantxt_YukabutuJyuusyuuryou.Text;
                    // 有価物拾集量単位コード
                    output.YUKABUTU_JYUUSYUURYOU_TANNI_CD = form.cantxt_YukabutuJyuusyuuryouTanniCD.Text;
                    // 有価物拾集量単位名称
                    output.YUKABUTU_JYUUSYUURYOU_TANNI_NAME = form.ctxt_YukabutuJyuusyuuryouTanniName.Text;
                    // 登録時車輌番号使用有無
                    if (form.ccb_SyaryouCD.Checked)
                    {
                        if (SearchResult.Rows.Count > 0)
                        {
                            // 車輌番号
                            output.SYARYOU_CD = SearchResult.Rows[0]["SYARYOU_CD"].ToString();
                            // 車輌名称
                            output.SYARYOU_NAME = SearchResult.Rows[0]["SYARYOU_NAME"].ToString();
                        }
                    }
                    else
                    {
                        // 車輌番号
                        output.SYARYOU_CD = form.cantxt_SyaryouCD.Text;
                        // 車輌名称
                        output.SYARYOU_NAME = form.ctxt_SyaryouName.Text;
                    }
                    // 備考
                    output.BIKO = form.ctxt_bikou.Text;
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
                //検索結果テーブル
                this.SearchResult = new DataTable();
                //検索実行
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
