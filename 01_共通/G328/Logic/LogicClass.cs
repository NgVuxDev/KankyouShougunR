using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using MasterKyoutsuPopup2.APP;
using r_framework.CustomControl;
using System.Linq;

namespace Shougun.Core.Common.KaisyuuHinmeShousai
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Common.KaisyuuHinmeShousai.Setting.ButtonSetting.xml";

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// DAO
        /// </summary>
        public IM_HINMEIDao dao_GetHinmei;
        public IM_UNITDao dao_GetUnit;
        public IM_YOUKIDao dao_GetYouki;
        private IM_GYOUSHADao dao_Gyousha;
        private IM_TORIHIKISAKI_SEIKYUUDao dao_TorihikisakiSeikyuu;
        private IM_GENBADao dao_Genba;

        /// <summary>
        /// 回収品名詳細Form
        /// </summary>
        private UIForm form;
        private UIHeader header;

        private DBAccessor CommonDBAccessor;
        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 取引先請求マスタ
        /// </summary>
        private M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuu;

        /// <summary>
        /// システム設定エンティティ
        /// </summary>
        private M_SYS_INFO mSysInfo;

        private M_GENBA genba;
        #endregion

        #region プロパティ

        /// <summary>
        /// メッセージユーティリティ
        /// </summary>
        private MessageUtility Message { get; set; }

        /// <summary>
        /// 検索結果(品名)
        /// </summary>
        public DataTable Search_M_HINMEI { get; set; }

        /// <summary>
        /// 検索結果(単位)
        /// </summary>
        public DataTable Search_M_UNIT { get; set; }

        /// <summary>
        /// 検索結果(容器名)
        /// </summary>
        public DataTable Search_M_YOUKI { get; set; }

        /// <summary>
        /// 受取回収品名詳細リスト
        /// </summary>
        public List<DTOClass> KaishuHinmeiSyousaiList;

        /// <summary>
        /// 伝票区分リスト
        /// </summary>
        private List<M_DENPYOU_KBN> mDenpyouKbnList;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);
                this.form = targetForm;
                this.dto = new DTOClass();
                //DAO
                dao_GetHinmei = DaoInitUtility.GetComponent<IM_HINMEIDao>();
                dao_GetUnit = DaoInitUtility.GetComponent<IM_UNITDao>();
                dao_GetYouki = DaoInitUtility.GetComponent<IM_YOUKIDao>();
                dao_Gyousha = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
                dao_TorihikisakiSeikyuu = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKI_SEIKYUUDao>();
                torihikisakiSeikyuu = new M_TORIHIKISAKI_SEIKYUU();
                dao_Genba = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
                // メッセージ表示オブジェクト
                msgLogic = new MessageBoxShowLogic();
                this.CommonDBAccessor = new DBAccessor();

                // システム設定を取得
                this.mSysInfo = this.CommonDBAccessor.GetSysInfo();

                // 伝票区分リストを作成
                var denpyouKbnDao = DaoInitUtility.GetComponent<IM_DENPYOU_KBNDao>();
                var mDenpyouKbnKeyEntity = new M_DENPYOU_KBN();
                this.mDenpyouKbnList = new List<M_DENPYOU_KBN>(denpyouKbnDao.GetAllValidData(mDenpyouKbnKeyEntity));
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicClass", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 画面初期化処理

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var parentForm = (BasePopForm)this.form.Parent;
                //ヘッダーの初期化
                UIHeader targetHeader = (UIHeader)parentForm.headerForm;
                this.header = targetHeader;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HeaderInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var parentForm = (BusinessBaseForm)this.form.Parent;

                //行挿入ボタン(F1)イベント生成
                parentForm.bt_func1.Click += new EventHandler(bt_func1_Click);

                //確定ボタン(F9)イベント生成
                parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);
                parentForm.bt_func12.CausesValidation = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();


                // 回収品目詳細のデータのセット
                Search();
                // 参照モード
                if ("2".Equals(this.form.syoriMode))
                {
                    var parentForm = (BusinessBaseForm)this.form.Parent;

                    // DataGridViewを非活性にする
                    this.form.Ichiran.ReadOnly = true;
                    // DataGridViewで新規行を表示させない
                    this.form.Ichiran.AllowUserToAddRows = false;
                    // [F1]行挿入ボタンを非活性にする
                    parentForm.bt_func1.Enabled = false;
                    // [F2]行削除ボタンを非活性にする
                    parentForm.bt_func2.Enabled = false;
                    // [F9]確定ボタンを非活性にする
                    parentForm.bt_func9.Enabled = false;
                }
                this.form.Ichiran.AutoGenerateColumns = false;

                // 取引先請求マスタ取得
                var gyousha = this.dao_Gyousha.GetDataByCd(this.form.gyoushaCD);
                if (gyousha != null && !string.IsNullOrEmpty(gyousha.TORIHIKISAKI_CD))
                {
                    torihikisakiSeikyuu = this.dao_TorihikisakiSeikyuu.GetDataByCd(gyousha.TORIHIKISAKI_CD);
                }

                if (this.form.winId == "G030")
                {
                    this.form.Ichiran.Columns[ConstCls.TEKIYOU_BEGIN].Visible = false;
                    this.form.Ichiran.Columns[ConstCls.TEKIYOU_END].Visible = false;
                    this.form.Ichiran.Columns[ConstCls.GENBA_TEKIYOU_BEGIN].Visible = false;
                    this.form.Ichiran.Columns[ConstCls.GENBA_TEKIYOU_END].Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 明細一覧のcellを結合する。
        //明細一覧のcellを結合する。
        public void ChangeCell(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender,e);
                // ガード句
                if (e.RowIndex > -1)
                {
                    // ヘッダー以外は処理なし
                    return;
                }

                DataGridView dgv = (DataGridView)sender;

                // 契約区分設定
                this.SetChangeCell(dgv, e, 15, 17, "契約区分※　");

                // 契約区分設定
                this.SetChangeCell(dgv, e, 17, 19, "集計単位※　");

                // 結合セル以外は既定の描画を行う
                if (!(
                        (15 <= e.ColumnIndex && e.ColumnIndex <= 17)
                        || (17 <= e.ColumnIndex && e.ColumnIndex <= 18)

                    ))
                {
                    e.Paint(e.ClipBounds, e.PaintParts);
                }
                // イベントハンドラ内で処理を行ったことを通知
                e.Handled = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeCell", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        private void SetChangeCell(DataGridView dgv, DataGridViewCellPaintingEventArgs e,
            int startColIndex, int endColIndex, string title)
        {

            if (e.ColumnIndex == startColIndex)
            {
                // セルの矩形を取得
                Rectangle rect = e.CellBounds;


                // 6列目の幅を取得して、5列目の幅に足す
                for (int i = startColIndex; i < endColIndex; i++)
                {
                    rect.Width += dgv.Columns[i].Width;
                }
                rect.Y = e.CellBounds.Y + 1;

                // 背景、枠線、セルの値を描画
                using (SolidBrush brush = new SolidBrush(this.form.Ichiran.ColumnHeadersDefaultCellStyle.BackColor))
                {
                    // 背景の描画
                    e.Graphics.FillRectangle(brush, rect);

                    using (Pen pen = new Pen(dgv.GridColor))
                    {
                        // 枠線の描画
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                }

                // セルに表示するテキストを描画
                TextRenderer.DrawText(e.Graphics,
                                title,
                                e.CellStyle.Font,
                                rect,
                                e.CellStyle.ForeColor,
                                TextFormatFlags.HorizontalCenter
                                | TextFormatFlags.VerticalCenter);
            }
        }

        #endregion

        #region GridViewデートのセット処理
        /// <summary>
        /// 回収品目詳細のデータのセット
        /// </summary>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<DTOClass> SyousaiList = new List<DTOClass>();
                KaishuHinmeiSyousaiList = this.form.GetKaishuHinmeiSyousaiList;
                M_HINMEI hinmei;
                M_UNIT unit;
                M_GENBA_TEIKI_HINMEI data;
                M_GENBA_TEIKI_HINMEI mGenbaTeikiHinmei;
                int i = 0;

                M_GENBA genba = new M_GENBA();
                genba.GYOUSHA_CD = this.form.gyoushaCD;
                genba.GENBA_CD = this.form.genbaCD;
                this.genba = dao_Genba.GetDataByCd(genba);

                foreach (DTOClass conret in KaishuHinmeiSyousaiList.OrderBy(h => h.HINMEI_CD).OrderByDescending(h => h.INPUT_KBN))
                {
                    this.form.Ichiran.Rows.Add();
                    hinmei = new M_HINMEI();
                    unit = new M_UNIT();
                    //削除
                    this.form.Ichiran.Rows[i].Cells[ConstCls.DELETE_FLG].Value = conret.DELETE_FLG;
                    // 品名CD
                    this.form.Ichiran.Rows[i].Cells[ConstCls.HINMEI_CD].Value = conret.HINMEI_CD;
                    // 品名
                    if (!string.IsNullOrEmpty(conret.HINMEI_CD))
                    {
                        hinmei = dao_GetHinmei.GetDataByCd(conret.HINMEI_CD);
                        if (hinmei != null)
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.HINMEI_NAME].Value = hinmei.HINMEI_NAME_RYAKU;
                        }
                    }
                    // 単位
                    if (!conret.UNIT_CD.IsNull)
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.UNIT_CD].Value = conret.UNIT_CD;
                        unit = dao_GetUnit.GetDataByCd((int)(conret.UNIT_CD));
                        if (unit != null)
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.UNIT_NAME].Value = unit.UNIT_NAME_RYAKU;
                        }
                    }


                    // 伝票区分
                    this.form.Ichiran.Rows[i].Cells[ConstCls.DENPYOU_KBN_CD].Value = (!conret.DENPYOU_KBN_CD.IsNull ? conret.DENPYOU_KBN_CD.ToSqlString() : string.Empty);
                    if (!conret.DENPYOU_KBN_CD.IsNull)
                    {
                        short value = conret.DENPYOU_KBN_CD.Value;
                        if (value == 1)
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.DENPYOU_KBN_CD_NM].Value = "売上";
                        }
                        if (value == 2)
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.DENPYOU_KBN_CD_NM].Value = "支払";
                        }
                    }
                    else
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.DENPYOU_KBN_CD_NM].Value = string.Empty;
                    }


                    data = new M_GENBA_TEIKI_HINMEI();
                    data.GYOUSHA_CD = this.form.gyoushaCD;
                    data.GENBA_CD = this.form.genbaCD;
                    data.HINMEI_CD = conret.HINMEI_CD;
                    data.DENPYOU_KBN_CD = conret.DENPYOU_KBN_CD;
                    data.UNIT_CD = conret.UNIT_CD;

                    // 現場_定期品名データ
                    mGenbaTeikiHinmei = DaoInitUtility.GetComponent<IM_GENBA_TEIKI_HINMEI_POPUPDao>().GetAllValidData(data);
                    if (mGenbaTeikiHinmei == null)
                    {
                        // 換算値
                        this.form.Ichiran.Rows[i].Cells[ConstCls.KANSANCHI].Value = string.Empty;
                    }
                    else
                    {
                        // 換算値
                        if (!mGenbaTeikiHinmei.KANSANCHI.IsNull)
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KANSANCHI].Value = decimal.Parse(mGenbaTeikiHinmei.KANSANCHI.ToString());
                        }
                        else
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KANSANCHI].Value = string.Empty;
                        }
                    }

                    // 換算後単位CD
                    this.form.Ichiran.Rows[i].Cells[ConstCls.KANSAN_UNIT_CD].Value = string.Empty;
                    this.form.Ichiran.Rows[i].Cells[ConstCls.KANSAN_UNIT_NAME].Value = string.Empty;

                    string kansanUnitCd = !conret.KANSAN_UNIT_CD.IsNull ? conret.KANSAN_UNIT_CD.ToString() : string.Empty;
                    if (string.IsNullOrWhiteSpace(kansanUnitCd) && mGenbaTeikiHinmei != null)
                    {
                        kansanUnitCd = !mGenbaTeikiHinmei.KANSAN_UNIT_CD.IsNull ? mGenbaTeikiHinmei.KANSAN_UNIT_CD.ToString() : string.Empty;
                    }
                    if (!string.IsNullOrWhiteSpace(kansanUnitCd))
                    {
                        unit = new M_UNIT();
                        unit = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(int.Parse(kansanUnitCd));
                        // 換算後単位
                        if (unit != null)
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KANSAN_UNIT_CD].Value = kansanUnitCd;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KANSAN_UNIT_NAME].Value = (unit.UNIT_NAME_RYAKU != null ? unit.UNIT_NAME_RYAKU : string.Empty);
                        }
                        else
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KANSAN_UNIT_NAME].Value = string.Empty;
                        }
                    }

                    // 契約区分
                    this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN].Value = string.Empty;
                    this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN_NM].Value = string.Empty;
                    this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN].ReadOnly = true;

                    string keiyakuKbn = !conret.KEIYAKU_KBN.IsNull ? conret.KEIYAKU_KBN.ToString() : string.Empty;
                    if ("0".Equals(keiyakuKbn))
                    {
                        keiyakuKbn = string.Empty;
                    }
                    if (!string.IsNullOrWhiteSpace(keiyakuKbn))
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN].Value = keiyakuKbn;
                        if (keiyakuKbn.Equals("1"))
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN_NM].Value = "定期";
                        }
                        else if (keiyakuKbn.Equals("2"))
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN_NM].Value = "単価";
                            // 2:単価の場合
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN].ReadOnly = false;
                        }
                        else if (keiyakuKbn.Equals("3"))
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN_NM].Value = "回収のみ";
                        }
                    }

                    // 集計単位
                    string tsukigimeKbn = !conret.KEIJYOU_KBN.IsNull ? conret.KEIJYOU_KBN.ToString() : string.Empty;
                    if ("0".Equals(tsukigimeKbn))
                    {
                        tsukigimeKbn = string.Empty;
                    }
                    this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN].Value = tsukigimeKbn;
                    if ("1".Equals(tsukigimeKbn))
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN_NM].Value = "伝票";
                    }
                    else if ("2".Equals(tsukigimeKbn))
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN_NM].Value = "合算";
                    }
                    this.form.Ichiran.Rows[i].Cells[ConstCls.YOU_KINYU].Value = (conret.KANSAN_UNIT_MOBILE_OUTPUT_FLG == true ? 1 : 0); // No.3159
                    if (!conret.INPUT_KBN.IsNull)
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.INPUT_KBN].Value = conret.INPUT_KBN;
                        if (conret.INPUT_KBN.Value == 1)
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.INPUT_KBN_NAME].Value = ConstCls.INPUT_KBN_1;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KANSANCHI].ReadOnly = false;
                            // 換算値
                            if (!conret.KANSANCHI.IsNull)
                            {
                                this.form.Ichiran.Rows[i].Cells[ConstCls.KANSANCHI].Value = conret.KANSANCHI.Value;
                            }
                            else
                            {
                                this.form.Ichiran.Rows[i].Cells[ConstCls.KANSANCHI].Value = string.Empty;
                            }
                        }
                        else
                        {
                            data = new M_GENBA_TEIKI_HINMEI();
                            data.GYOUSHA_CD = this.form.gyoushaCD;
                            data.GENBA_CD = this.form.genbaCD;
                            data.HINMEI_CD = conret.HINMEI_CD;
                            data.DENPYOU_KBN_CD = conret.DENPYOU_KBN_CD;
                            data.UNIT_CD = conret.UNIT_CD;

                            // 現場_定期品名データ
                            mGenbaTeikiHinmei = DaoInitUtility.GetComponent<IM_GENBA_TEIKI_HINMEI_POPUPDao>().GetAllValidData(data);
                            if (mGenbaTeikiHinmei == null)
                            {
                                // 換算値
                                this.form.Ichiran.Rows[i].Cells[ConstCls.KANSANCHI].Value = string.Empty;
                            }
                            else
                            {
                                // 換算値
                                if (!mGenbaTeikiHinmei.KANSANCHI.IsNull)
                                {
                                    this.form.Ichiran.Rows[i].Cells[ConstCls.KANSANCHI].Value = decimal.Parse(mGenbaTeikiHinmei.KANSANCHI.ToString());
                                }
                                else
                                {
                                    this.form.Ichiran.Rows[i].Cells[ConstCls.KANSANCHI].Value = string.Empty;
                                }
                            }

                            this.form.Ichiran.Rows[i].Cells[ConstCls.INPUT_KBN_NAME].Value = ConstCls.INPUT_KBN_2;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.HINMEI_CD].ReadOnly = true;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.UNIT_CD].ReadOnly = true;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.UNIT_NAME].ReadOnly = true;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KANSANCHI].ReadOnly = true;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KANSAN_UNIT_CD].ReadOnly = true;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KANSAN_UNIT_NAME].ReadOnly = true;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN_NM].ReadOnly = true;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN_NM].ReadOnly = true;
                            // QN Tue Anh #158987 START
                            this.form.Ichiran.Rows[i].Cells[ConstCls.YOU_KINYU].ReadOnly = true;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.ANBUN_FLG].ReadOnly = true;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN].ReadOnly = true;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN].ReadOnly = true;
                            // QN Tue Anh #158987 END
                        }
                        if (conret.INPUT_KBN.Value == 1 && keiyakuKbn.Equals("1"))
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN_NM].Value = string.Empty;
                        }
                    }
                    else
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.INPUT_KBN].Value = "1";
                        this.form.Ichiran.Rows[i].Cells[ConstCls.INPUT_KBN_NAME].Value = ConstCls.INPUT_KBN_1;
                        this.form.Ichiran.Rows[i].Cells[ConstCls.KANSANCHI].ReadOnly = false;
                    }
                    if (!conret.NIOROSHI_NUMBER.IsNull)
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.NIOROSHI_NUMBER].Value = conret.NIOROSHI_NUMBER;
                    }
                    // 要記入
                    this.form.Ichiran.Rows[i].Cells[ConstCls.ANBUN_FLG].Value = (conret.ANBUN_FLG == true ? 1 : 0);
                    if (!conret.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.TEKIYOU_BEGIN].Value = conret.TEKIYOU_BEGIN;
                    }
                    else
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.TEKIYOU_BEGIN].Value = this.form.sysDate;
                    }
                    if (!conret.TEKIYOU_END.IsNull)
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.TEKIYOU_END].Value = conret.TEKIYOU_END;
                    }

                    if (this.genba != null && this.form.Ichiran.Rows[i].Cells[ConstCls.GENBA_TEKIYOU_BEGIN].Visible)
                    {
                        if (!this.genba.TEKIYOU_BEGIN.IsNull)
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.GENBA_TEKIYOU_BEGIN].Value = this.genba.TEKIYOU_BEGIN;
                        }
                        if (!this.genba.TEKIYOU_END.IsNull)
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.GENBA_TEKIYOU_END].Value = this.genba.TEKIYOU_END;
                        }
                    }

                    //参照モード時に背景色対応
                    if ("2".Equals(this.form.syoriMode))
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.NIOROSHI_NUMBER].ReadOnly = true;
                        this.form.Ichiran.Rows[i].Cells[ConstCls.HINMEI_CD].ReadOnly = true;
                        this.form.Ichiran.Rows[i].Cells[ConstCls.UNIT_CD].ReadOnly = true;
                        this.form.Ichiran.Rows[i].Cells[ConstCls.UNIT_NAME].ReadOnly = true;
                        this.form.Ichiran.Rows[i].Cells[ConstCls.KANSANCHI].ReadOnly = true;
                        this.form.Ichiran.Rows[i].Cells[ConstCls.KANSAN_UNIT_CD].ReadOnly = true;
                        this.form.Ichiran.Rows[i].Cells[ConstCls.KANSAN_UNIT_NAME].ReadOnly = true;
                        this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN_NM].ReadOnly = true;
                        this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN_NM].ReadOnly = true;
                        this.form.Ichiran.Rows[i].Cells[ConstCls.YOU_KINYU].ReadOnly = true; // QN Tue Anh #158987
                        this.form.Ichiran.Rows[i].Cells[ConstCls.ANBUN_FLG].ReadOnly = true;
                        this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN].ReadOnly = true;
                        this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN].ReadOnly = true;
                    }
                    i++;
                }
                return 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 返却回収品目詳細リスト作成
        /// <summary>
        /// 返却回収品目詳細リスト作成
        /// </summary>
        public void RetContenaResultListSakusei()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //回収品目詳細設定
                DTOClass syousai;
                long recseq = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    syousai = new DTOClass();

                    if ((this.form.Ichiran.Rows[i].Cells["HINMEI_CD"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["HINMEI_CD"].Value.ToString()))
                         && (this.form.Ichiran.Rows[i].Cells["HINMEI_NAME"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["HINMEI_NAME"].Value.ToString()))
                         && (this.form.Ichiran.Rows[i].Cells["INPUT_KBN"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["INPUT_KBN"].Value.ToString()))
                         && (this.form.Ichiran.Rows[i].Cells["NIOROSHI_NUMBER"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["NIOROSHI_NUMBER"].Value.ToString()))
                         && (this.form.Ichiran.Rows[i].Cells["UNIT_CD"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["UNIT_CD"].Value.ToString()))
                         && (this.form.Ichiran.Rows[i].Cells["UNIT_NAME"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["UNIT_NAME"].Value.ToString()))
                         && (this.form.Ichiran.Rows[i].Cells["KANSANCHI"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["KANSANCHI"].Value.ToString()))
                         && (this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_CD"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_CD"].Value.ToString()))
                         && (this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_NAME"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_NAME"].Value.ToString()))
                         && (this.form.Ichiran.Rows[i].Cells["ANBUN_FLG"].Value == null || "0".Equals(this.form.Ichiran.Rows[i].Cells["ANBUN_FLG"].Value.ToString()) || "False".Equals(this.form.Ichiran.Rows[i].Cells["ANBUN_FLG"].Value.ToString()))
                         && (this.form.Ichiran.Rows[i].Cells["TEKIYOU_BEGIN"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["TEKIYOU_BEGIN"].Value.ToString()) || this.form.Ichiran.Rows[i].Cells["TEKIYOU_BEGIN"].Value.ToString() == this.form.sysDate.ToString())
                         && (this.form.Ichiran.Rows[i].Cells["TEKIYOU_END"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["TEKIYOU_END"].Value.ToString()))

                        )
                    {
                        continue;
                    }
                    recseq++;
                    // レコードID
                    syousai.REC_ID = this.form.recID;
                    // レコードSEQ
                    syousai.REC_SEQ = recseq;
                    // 品名CD
                    if (this.form.Ichiran.Rows[i].Cells["HINMEI_CD"].Value != null)
                    {
                        syousai.HINMEI_CD = this.form.Ichiran.Rows[i].Cells["HINMEI_CD"].Value.ToString();
                    }
                    // 品名
                    if (this.form.Ichiran.Rows[i].Cells["HINMEI_NAME"].Value != null)
                    {
                        syousai.HINMEI_NAME = this.form.Ichiran.Rows[i].Cells["HINMEI_NAME"].Value.ToString();
                    }
                    // 単位CD
                    if (this.form.Ichiran.Rows[i].Cells["UNIT_CD"].Value != null && !string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["UNIT_CD"].Value.ToString()))
                    {
                        syousai.UNIT_CD = SqlInt16.Parse(this.form.Ichiran.Rows[i].Cells["UNIT_CD"].Value.ToString());
                    }
                    // 単位
                    if (this.form.Ichiran.Rows[i].Cells["UNIT_NAME"].Value != null)
                    {
                        syousai.UNIT_NAME = this.form.Ichiran.Rows[i].Cells["UNIT_NAME"].Value.ToString();
                    }
                    // 換算値
                    if (this.form.Ichiran.Rows[i].Cells["KANSANCHI"].Value != null
                        && !string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["KANSANCHI"].Value.ToString()))
                    {
                        syousai.KANSANCHI = SqlDecimal.Parse(this.form.Ichiran.Rows[i].Cells["KANSANCHI"].Value.ToString().Replace(",", ""));
                    }
                    // 換算後単位CD
                    syousai.KANSAN_UNIT_CD = SqlInt16.Null;
                    syousai.KANSAN_UNIT_NAME = string.Empty;
                    if (this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_CD"].Value != null
                        && !string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_CD"].Value.ToString()))
                    {
                        syousai.KANSAN_UNIT_CD = SqlInt16.Parse(this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_CD"].Value.ToString());
                        // (換算後)単位名
                        if (this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_NAME"].Value != null)
                        {
                            syousai.KANSAN_UNIT_NAME = this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_NAME"].Value.ToString();
                        }
                    }
                    // 削除
                    if (this.form.Ichiran.Rows[i].Cells["DELETE_FLG"].Value != null
                        && ("1".Equals(this.form.Ichiran.Rows[i].Cells["DELETE_FLG"].Value.ToString())
                            || "True".Equals(this.form.Ichiran.Rows[i].Cells["DELETE_FLG"].Value.ToString())))
                    {
                        syousai.DELETE_FLG = 1;
                    }
                    else
                    {
                        syousai.DELETE_FLG = 0;
                    }

                    // 伝票区分CD
                    if (this.form.Ichiran.Rows[i].Cells[ConstCls.DENPYOU_KBN_CD].Value != null
                        && !string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells[ConstCls.DENPYOU_KBN_CD].Value.ToString()))
                    {
                        syousai.DENPYOU_KBN_CD = SqlInt16.Parse(this.form.Ichiran.Rows[i].Cells[ConstCls.DENPYOU_KBN_CD].Value.ToString());
                    }
                    // 契約区分CD
                    if (this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN].Value != null
                        && !string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN].Value.ToString()))
                    {
                        syousai.KEIYAKU_KBN = SqlInt16.Parse(this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN].Value.ToString());
                    }
                    // 月極区分
                    if (this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN].Value != null
                        && !string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN].Value.ToString()))
                    {
                        syousai.KEIJYOU_KBN = SqlInt16.Parse(this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN].Value.ToString());
                    }
                    // 要記入
                    syousai.KANSAN_UNIT_MOBILE_OUTPUT_FLG = false;
                    if (this.form.Ichiran.Rows[i].Cells[ConstCls.YOU_KINYU].Value != null)
                    {
                        if (true.Equals(this.form.Ichiran.Rows[i].Cells[ConstCls.YOU_KINYU].Value)
                           || this.form.Ichiran.Rows[i].Cells[ConstCls.YOU_KINYU].Value.ToString().Equals("1"))
                        {
                            syousai.KANSAN_UNIT_MOBILE_OUTPUT_FLG = true;
                        }
                    }

                    // 入力区分
                    if (!string.IsNullOrEmpty(Convert.ToString(this.form.Ichiran.Rows[i].Cells["INPUT_KBN"].Value)))
                    {
                        syousai.INPUT_KBN = SqlInt16.Parse(this.form.Ichiran.Rows[i].Cells["INPUT_KBN"].Value.ToString());
                    }
                    // 荷降No
                    if (!string.IsNullOrEmpty(Convert.ToString(this.form.Ichiran.Rows[i].Cells["NIOROSHI_NUMBER"].Value)))
                    {
                        syousai.NIOROSHI_NUMBER = SqlInt32.Parse(this.form.Ichiran.Rows[i].Cells["NIOROSHI_NUMBER"].Value.ToString());
                    }
                    // 実数
                    if (!string.IsNullOrEmpty(Convert.ToString(this.form.Ichiran.Rows[i].Cells["ANBUN_FLG"].Value)))
                    {
                        syousai.ANBUN_FLG = SqlBoolean.Parse(this.form.Ichiran.Rows[i].Cells["ANBUN_FLG"].Value.ToString());
                    }
                    // 適用開始日
                    string date = Convert.ToString(this.form.Ichiran.Rows[i].Cells["TEKIYOU_BEGIN"].Value);
                    if (!string.IsNullOrEmpty(date))
                    {
                        date = date.Split('(')[0];
                        syousai.TEKIYOU_BEGIN = SqlDateTime.Parse(date);
                    }
                    // 適用終了日
                    date = Convert.ToString(this.form.Ichiran.Rows[i].Cells["TEKIYOU_END"].Value);
                    if (!string.IsNullOrEmpty(date))
                    {
                        date = date.Split('(')[0];
                        syousai.TEKIYOU_END = SqlDateTime.Parse(date);
                    }
                    // 現場適用開始日
                    date = Convert.ToString(this.form.Ichiran.Rows[i].Cells["GENBA_TEKIYOU_BEGIN"].Value);
                    if (!string.IsNullOrEmpty(date))
                    {
                        date = date.Split('(')[0];
                        syousai.GENBA_TEKIYOU_BEGIN = SqlDateTime.Parse(date);
                    }
                    // 現場適用終了日
                    date = Convert.ToString(this.form.Ichiran.Rows[i].Cells["GENBA_TEKIYOU_END"].Value);
                    if (!string.IsNullOrEmpty(date))
                    {
                        date = date.Split('(')[0];
                        syousai.GENBA_TEKIYOU_END = SqlDateTime.Parse(date);
                    }

                    this.form.RetKaishuHinmeiSyousaiList.Add(syousai);

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RetContenaResultListSakusei", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 品名CD、単位CDの重複チェック
        /// <summary>
        /// 品名CD、単位CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string hinmeiCd, string unitCd)
        {
            try
            {
                LogUtility.DebugMethodStart(hinmeiCd, unitCd);

                // 画面で種類CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    if (hinmeiCd.Equals(Convert.ToString(this.form.Ichiran.Rows[i].Cells[ConstCls.HINMEI_CD].Value))
                        && unitCd.Equals(Convert.ToString(this.form.Ichiran.Rows[i].Cells[ConstCls.UNIT_CD].Value)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region [F1]行挿入処理
        /// <summary>
        /// [F1]行挿入処理
        /// </summary>
        internal void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 行挿入
                this.form.Ichiran.RowsAdded -= this.form.Ichiran_RowsAdded;
                int index = this.form.Ichiran.CurrentRow.Index;
                this.form.Ichiran.Rows.Insert(this.form.Ichiran.CurrentRow.Index, 1);
                var row = this.form.Ichiran.Rows[index];
                row.Cells[ConstCls.INPUT_KBN].Value = "1";
                row.Cells[ConstCls.INPUT_KBN_NAME].Value = ConstCls.INPUT_KBN_1;
                row.Cells[ConstCls.KANSANCHI].ReadOnly = false;
                row.Cells[ConstCls.TEKIYOU_BEGIN].Value = this.form.sysDate;
                if (!this.genba.TEKIYOU_BEGIN.IsNull)
                {
                    row.Cells[ConstCls.GENBA_TEKIYOU_BEGIN].Value = this.genba.TEKIYOU_BEGIN;
                }
                if (!this.genba.TEKIYOU_END.IsNull)
                {
                    row.Cells[ConstCls.GENBA_TEKIYOU_END].Value = this.genba.TEKIYOU_END;
                }
                row.Cells[ConstCls.KEIJYOU_KBN].ReadOnly = true;
                this.form.Ichiran.RowsAdded += this.form.Ichiran_RowsAdded;
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 確定
        /// <summary>
        /// 「F9 確定ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.form.Parent;
                // 編集モード
                if ("1".Equals(this.form.syoriMode))
                {
                    // 明細行が空行１行の場合、アラートを表示し処理を中断。
                    if (this.form.Ichiran.Rows.Count == 1)
                    {
                        parentForm.Close();                                             // No.2407
                        return;
                    }
                    string[] errColName = new string[8];
                    bool unitError = false;
                    bool tekiyouError = false;
                    bool genbaTekiyouError = false;

                    // 必須チェック
                    for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                    {
                        if ((this.form.Ichiran.Rows[i].Cells["DELETE_FLG"].Value != null
                                && ("1".Equals(this.form.Ichiran.Rows[i].Cells["DELETE_FLG"].Value.ToString())
                                || "True".Equals(this.form.Ichiran.Rows[i].Cells["DELETE_FLG"].Value.ToString())))
                             || ((this.form.Ichiran.Rows[i].Cells["HINMEI_CD"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["HINMEI_CD"].Value.ToString()))
                                 && (this.form.Ichiran.Rows[i].Cells["HINMEI_NAME"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["HINMEI_NAME"].Value.ToString()))
                                 && (this.form.Ichiran.Rows[i].Cells["INPUT_KBN"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["INPUT_KBN"].Value.ToString()))
                                 && (this.form.Ichiran.Rows[i].Cells["NIOROSHI_NUMBER"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["NIOROSHI_NUMBER"].Value.ToString()))
                                 && (this.form.Ichiran.Rows[i].Cells["UNIT_CD"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["UNIT_CD"].Value.ToString()))
                                 && (this.form.Ichiran.Rows[i].Cells["UNIT_NAME"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["UNIT_NAME"].Value.ToString()))
                                 && (this.form.Ichiran.Rows[i].Cells["KANSANCHI"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["KANSANCHI"].Value.ToString()))
                                 && (this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_CD"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_CD"].Value.ToString()))
                                 && (this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_NAME"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["KANSAN_UNIT_NAME"].Value.ToString()))
                                 && (this.form.Ichiran.Rows[i].Cells["ANBUN_FLG"].Value == null || "0".Equals(this.form.Ichiran.Rows[i].Cells["ANBUN_FLG"].Value.ToString()) || "False".Equals(this.form.Ichiran.Rows[i].Cells["MONDAY"].Value.ToString()))
                                 && (this.form.Ichiran.Rows[i].Cells["TEKIYOU_BEGIN"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["TEKIYOU_BEGIN"].Value.ToString()) || this.form.Ichiran.Rows[i].Cells["TEKIYOU_BEGIN"].Value.ToString() == this.form.sysDate.ToString())
                                 && (this.form.Ichiran.Rows[i].Cells["TEKIYOU_END"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["TEKIYOU_END"].Value.ToString()))
                            )
                        )
                        {
                            this.form.Ichiran.Rows[i].Cells[ConstCls.HINMEI_CD].Style.BackColor = Color.White;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.HINMEI_NAME].Style.BackColor = Color.White;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.UNIT_CD].Style.BackColor = Color.White;
                            this.form.Ichiran.Rows[i].Cells[ConstCls.UNIT_NAME].Style.BackColor = Color.White;
                            continue;
                        }
                        else
                        {
                            var hinmeiCd = this.form.Ichiran.Rows[i].Cells[ConstCls.HINMEI_CD].Value;
                            if (hinmeiCd == null || string.IsNullOrEmpty(hinmeiCd.ToString()))
                            {
                                ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells[ConstCls.HINMEI_CD], true);
                                errColName[0] = "品名CD";
                            }
                            var unitCd = this.form.Ichiran.Rows[i].Cells[ConstCls.UNIT_CD].Value;
                            if (unitCd == null || string.IsNullOrEmpty(unitCd.ToString()))
                            {
                                errColName[1] = "単位CD";
                                ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells[ConstCls.UNIT_CD], true);
                            }

                            var kansanUnitCd = this.form.Ichiran.Rows[i].Cells[ConstCls.KANSAN_UNIT_CD].Value;
                            int uCd = 0;
                            int kCd = 0;
                            if (unitCd != null && !string.IsNullOrEmpty(unitCd.ToString()))
                            {
                                int.TryParse(unitCd.ToString(), out uCd);
                            }

                            if (kansanUnitCd != null && !string.IsNullOrEmpty(kansanUnitCd.ToString()))
                            {
                                int.TryParse(kansanUnitCd.ToString(), out kCd);
                            }

                            if (uCd != 3 && kCd != 3)
                            {
                                ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells[ConstCls.UNIT_CD], true);
                                ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells[ConstCls.KANSAN_UNIT_CD], true);
                                unitError = true;
                            }

                            var youKinyu = this.form.Ichiran.Rows[i].Cells[ConstCls.YOU_KINYU].Value;
                            if ((youKinyu != null && ("1".Equals(youKinyu.ToString()) || "True".Equals(youKinyu.ToString())))
                                && (kansanUnitCd == null || string.IsNullOrEmpty(kansanUnitCd.ToString())))
                            {
                                errColName[2] = "要記入を選択済みの場合、換算後単位";
                                ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells[ConstCls.KANSAN_UNIT_CD], true);
                            }

                            // 伝票区分
                            var denpyoKbn = this.form.Ichiran.Rows[i].Cells[ConstCls.DENPYOU_KBN_CD].Value;
                            if (denpyoKbn == null || string.IsNullOrEmpty(denpyoKbn.ToString()))
                            {
                                errColName[3] = "伝票区分";
                            }

                            // 契約区分
                            var keiyakuKbn = this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN].Value;
                            if (keiyakuKbn == null || string.IsNullOrEmpty(keiyakuKbn.ToString()))
                            {
                                errColName[4] = "契約区分";
                                ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells[ConstCls.KEIYAKU_KBN], true);
                            }

                            // 集計単位
                            if (keiyakuKbn != null && "2".Equals(keiyakuKbn.ToString()))
                            {
                                var keijouKbn = this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN].Value;
                                if (keijouKbn == null || string.IsNullOrEmpty(keijouKbn.ToString()))
                                {
                                    errColName[5] = "集計単位";
                                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells[ConstCls.KEIJYOU_KBN], true);
                                }
                            }

                            if (this.form.winId != "G030")
                            {
                                var tekiyouBegin = this.form.Ichiran.Rows[i].Cells[ConstCls.TEKIYOU_BEGIN].Value;
                                if (string.IsNullOrEmpty(Convert.ToString(tekiyouBegin)))
                                {
                                    errColName[6] = "適用開始日";
                                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells[ConstCls.TEKIYOU_BEGIN], true);
                                }
                                else
                                {
                                    DateTime begin = DateTime.Parse(Convert.ToString(tekiyouBegin));
                                    if (!this.genba.TEKIYOU_BEGIN.IsNull && begin.CompareTo(this.genba.TEKIYOU_BEGIN.Value) < 0)
                                    {
                                        genbaTekiyouError = true;
                                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells[ConstCls.TEKIYOU_BEGIN], true);
                                    }
                                }

                                var tekiyouEnd = this.form.Ichiran.Rows[i].Cells[ConstCls.TEKIYOU_END].Value;

                                if (!string.IsNullOrEmpty(Convert.ToString(tekiyouEnd)))
                                {
                                    DateTime end = DateTime.Parse(Convert.ToString(tekiyouEnd));
                                    if (!this.genba.TEKIYOU_END.IsNull && end.CompareTo(this.genba.TEKIYOU_END.Value) > 0)
                                    {
                                        genbaTekiyouError = true;
                                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells[ConstCls.TEKIYOU_END], true);
                                    }
                                }
                                else if (!this.genba.TEKIYOU_END.IsNull)
                                {
                                    genbaTekiyouError = true;
                                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells[ConstCls.TEKIYOU_END], true);
                                }

                                if (!string.IsNullOrEmpty(Convert.ToString(tekiyouBegin)) && !string.IsNullOrEmpty(Convert.ToString(tekiyouEnd)))
                                {
                                    DateTime begin = DateTime.Parse(Convert.ToString(tekiyouBegin));
                                    DateTime end = DateTime.Parse(Convert.ToString(tekiyouEnd));
                                    if (begin.CompareTo(end) > 0)
                                    {
                                        tekiyouError = true;
                                    }
                                }
                            }
                        }
                    }

                    bool nioroshiErr = false;
                    foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }
                        string nioroshiNum = Convert.ToString(row.Cells[ConstCls.NIOROSHI_NUMBER].Value);
                        if (!this.form.NioroshiList.Contains(nioroshiNum) && !string.IsNullOrEmpty(nioroshiNum))
                        {
                            ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[ConstCls.NIOROSHI_NUMBER], true);
                            nioroshiErr = true;
                        }
                    }

                    //ControlUtility.ShowPopup();
                    MessageUtility messageUtility = new MessageUtility();
                    string message = messageUtility.GetMessage("E001").MESSAGE;

                    string errMsg = "";
                    foreach (string colName in errColName)
                    {
                        if (errMsg.Length > 0)
                        {
                            errMsg += "\n";
                        }
                        if (!string.IsNullOrEmpty(colName))
                        {
                            errMsg += message.Replace("{0}", colName);
                        }
                    }

                    if (unitError)
                    {
                        if (string.IsNullOrWhiteSpace(errMsg))
                        {
                            errMsg += "\n";
                        }
                        errMsg += messageUtility.GetMessage("E145").MESSAGE.Replace("{0}", "単位と換算単位のいずれかに単位CD='3'(kg)");
                    }

                    if (nioroshiErr)
                    {
                        if (string.IsNullOrWhiteSpace(errMsg))
                        {
                            errMsg += "\n";
                        }
                        errMsg += messageUtility.GetMessage("E062").MESSAGE.Replace("{0}", "荷降明細の荷降No");
                    }

                    if (tekiyouError)
                    {
                        if (string.IsNullOrWhiteSpace(errMsg))
                        {
                            errMsg += "\n";
                        }
                        string msg = messageUtility.GetMessage("E030").MESSAGE.Replace("{0}", "適用開始日");
                        msg = msg.Replace("{1}", "適用終了日");
                        errMsg += msg;
                    }

                    if (genbaTekiyouError)
                    {
                        if (string.IsNullOrWhiteSpace(errMsg))
                        {
                            errMsg += "\n";
                        }
                        string msg = messageUtility.GetMessage("E256").MESSAGE.Replace("{0}", "適用期間");
                        msg = msg.Replace("{1}", "現場適用期間");
                        errMsg += msg;
                    }

                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        MessageBox.Show(errMsg, "アラート", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.form.Ichiran.BeginEdit(false);
                    }
                    else
                    {
                        // 返却回収品目詳細リスト作成
                        RetContenaResultListSakusei();
                        parentForm.Close();
                    }
                }
                else
                {
                    // 参照モード
                    parentForm.Close();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func9_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 閉じる
        /// <summary>
        /// 「F12 閉じるボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 自動生成
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

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion


        /// <summary>
        /// ポップアップ判定処理
        /// </summary>
        /// <param name="e"></param>
        public void CheckPopup(KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            if (e.KeyCode == Keys.Space)
            {
                if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("KEIJYOU_KBN")
                    && this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells[ConstCls.KEIJYOU_KBN].ReadOnly == false)
                {

                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    DataRow row;
                    row = dt.NewRow();
                    row["CD"] = "1";
                    row["VALUE"] = "伝票";
                    dt.Rows.Add(row);
                    if (!torihikisakiSeikyuu.TORIHIKI_KBN_CD.Equals(ConstCls.TORIHIKI_KBN_GENKIN))
                    {
                        // 取引区分CDが現金以外の場合のみ
                        row = dt.NewRow();
                        row["CD"] = "2";
                        row["VALUE"] = "合算";
                        dt.Rows.Add(row);
                    }
                    dt.TableName = "集計単位";
                    form.table = dt;
                    form.PopupTitleLabel = "集計単位";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "集計単位CD", "集計単位" };


                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.Ichiran.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["KEIJYOU_KBN_NM"].Value = form.ReturnParams[1][0].Value.ToString();
                    }
                }
                else if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("KEIYAKU_KBN")
                    && !this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells[ConstCls.KEIYAKU_KBN].ReadOnly)
                {

                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    DataRow row;
                    string inputKbn = Convert.ToString(this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["INPUT_KBN"].Value);
                    if (!torihikisakiSeikyuu.TORIHIKI_KBN_CD.Equals(ConstCls.TORIHIKI_KBN_GENKIN)
                        && inputKbn == "2")
                    {
                        // 取引区分CDが現金以外且つ直接入力ではないの場合のみ
                        row = dt.NewRow();
                        row["CD"] = "1";
                        row["VALUE"] = "定期";
                        dt.Rows.Add(row);
                    }
                    row = dt.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = "単価";
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "3";
                    row["VALUE"] = "回収のみ";
                    dt.Rows.Add(row);
                    dt.TableName = "契約区分";
                    form.table = dt;
                    form.PopupTitleLabel = "契約区分";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "契約区分CD", "契約区分" };

                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.Ichiran.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["KEIYAKU_KBN_NM"].Value = form.ReturnParams[1][0].Value.ToString();
                    }
                }
                else if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("DENPYOU_KBN_CD"))
                {

                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    DataRow row;
                    row = dt.NewRow();
                    row["CD"] = "1";
                    row["VALUE"] = "売上";
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = "支払";
                    dt.Rows.Add(row);
                    dt.TableName = "伝票区分";
                    form.table = dt;
                    form.PopupTitleLabel = "伝票区分";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "伝票区分CD", "伝票区分名" };

                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.Ichiran.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = form.ReturnParams[1][0].Value.ToString();
                    }
                }
            }
        }

        public void isHinmeiCdChkOk(DataGridViewCellEventArgs e)
        {
            bool result = true;
            try
            {
                LogUtility.DebugMethodStart(e);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                DataGridViewRow dgvRow = this.form.Ichiran.Rows[e.RowIndex];

                string hinmeiCd = dgvRow.Cells["HINMEI_CD"].FormattedValue.ToString();
                if (!string.IsNullOrEmpty(hinmeiCd))
                {

                    //string strDENPYOU_KBN_CD = dgvRow.Cells["DENPYOU_KBN_CD"].FormattedValue.ToString();
                    //if (string.IsNullOrEmpty(strDENPYOU_KBN_CD))
                    //{
                    //品名情報
                    M_HINMEI HimeiInfo = DaoInitUtility.GetComponent<IM_HINMEIDao>().GetDataByCd(hinmeiCd);
                    if (HimeiInfo != null)
                    {
                        // 下記列の場合
                        switch (HimeiInfo.DENPYOU_KBN_CD.ToString())
                        {
                            // 伝票区分CDの場合
                            case "1":
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = HimeiInfo.DENPYOU_KBN_CD.ToString();
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = "売上";

                                break;
                            //  換算後単位CDの場合
                            case "2":
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = HimeiInfo.DENPYOU_KBN_CD.ToString();
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = "支払";
                                break;
                            default:
                                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                                DataTable dt = new DataTable();
                                dt.Columns.Add("CD", typeof(string));
                                dt.Columns.Add("VALUE", typeof(string));
                                dt.Columns[0].ReadOnly = true;
                                dt.Columns[1].ReadOnly = true;
                                DataRow row;
                                row = dt.NewRow();
                                row["CD"] = "1";
                                row["VALUE"] = "売上";
                                dt.Rows.Add(row);
                                row = dt.NewRow();
                                row["CD"] = "2";
                                row["VALUE"] = "支払";
                                dt.Rows.Add(row);
                                dt.TableName = "伝票区分";
                                form.table = dt;
                                form.PopupTitleLabel = "伝票区分";
                                form.PopupGetMasterField = "CD,VALUE";
                                form.PopupDataHeaderTitle = new string[] { "伝票区分CD", "伝票区分名" };

                                form.ShowDialog();
                                if (form.ReturnParams != null)
                                {
                                    this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = form.ReturnParams[0][0].Value.ToString();
                                    this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = form.ReturnParams[1][0].Value.ToString();
                                }
                                result = true;
                                break;
                        }
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E020", "品名");
                    }
                    //}
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }

        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>       
        /// <returns>object</returns>
        public string GetCellValue(DataGridViewCell obj)
        {
            if (obj.Value == null)
            {
                return string.Empty;
            }
            else
            {
                return obj.Value.ToString();
            }
        }

        /// <summary>
        /// 品名から伝票区分をセットします（伝票区分が「共通」の場合は、選択ポップアップを表示します）
        /// </summary>
        /// <param name="mHinmei">品名エンティティ</param>
        /// <returns>伝票区分がセットされなければ、false</returns>
        internal bool SetDenpyouKbn(M_HINMEI mHinmei)
        {
            LogUtility.DebugMethodStart(mHinmei);

            bool ret = false;


            var denpyouKbnCdCell = this.form.Ichiran.CurrentRow.Cells[ConstCls.DENPYOU_KBN_CD];
            var denpyouKbnNameCell = this.form.Ichiran.CurrentRow.Cells[ConstCls.DENPYOU_KBN_CD_NM];

            // 初期化
            denpyouKbnCdCell.Value = String.Empty;
            denpyouKbnNameCell.Value = String.Empty;

            if (ConstCls.DENPYOU_KBN_CD_URIAGE == mHinmei.DENPYOU_KBN_CD.ToString() || ConstCls.DENPYOU_KBN_CD_SHIHARAI == mHinmei.DENPYOU_KBN_CD.ToString())
            {
                // 売上 or 支払
                denpyouKbnCdCell.Value = mHinmei.DENPYOU_KBN_CD;
                denpyouKbnNameCell.Value = this.mDenpyouKbnList.Where(d => 0 == d.DENPYOU_KBN_CD.CompareTo(mHinmei.DENPYOU_KBN_CD)).Select(d => d.DENPYOU_KBN_NAME_RYAKU).FirstOrDefault();

                ret = true;
            }
            else
            {
                // 上記以外
                var denpyouKbn = this.ShowDenpyouKbnPopup();
                if (!String.IsNullOrEmpty(denpyouKbn))
                {
                    denpyouKbnCdCell.Value = denpyouKbn;
                    denpyouKbnNameCell.Value = this.mDenpyouKbnList.Where(d => d.DENPYOU_KBN_CD.ToString() == denpyouKbn).Select(d => d.DENPYOU_KBN_NAME_RYAKU).FirstOrDefault();

                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 伝票区分選択のポップアップを表示します
        /// </summary>
        /// <returns>選択された伝票区分CD</returns>
        private string ShowDenpyouKbnPopup()
        {
            LogUtility.DebugMethodStart();

            string ret = String.Empty;

            var dt = new DataTable();
            dt.TableName = "伝票区分";
            dt.Columns.Add("CD", typeof(String));
            dt.Columns.Add("VALUE", typeof(String));
            dt.Columns[0].ReadOnly = true;
            dt.Columns[1].ReadOnly = true;
            foreach (var mDenpyouKbn in this.mDenpyouKbnList.Where(d => d.DENPYOU_KBN_CD.ToString() != ConstCls.DENPYOU_KBN_CD_KYOUTU))
            {
                var row = dt.NewRow();
                row["CD"] = mDenpyouKbn.DENPYOU_KBN_CD;
                row["VALUE"] = mDenpyouKbn.DENPYOU_KBN_NAME_RYAKU;
                dt.Rows.Add(row);
            }

            var popup = new MasterKyoutsuPopupForm();
            popup.table = dt;
            popup.PopupTitleLabel = "伝票区分";
            popup.PopupGetMasterField = "CD,VALUE";
            popup.PopupDataHeaderTitle = new string[] { "伝票区分CD", "伝票区分名" };
            popup.ShowDialog();
            if (null != popup.ReturnParams)
            {
                ret = popup.ReturnParams[0][0].Value.ToString();
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 伝票区分選択のポップアップを表示します
        /// </summary>
        /// <returns>選択された伝票区分CD</returns>
        internal void Ichiran_RowsAdded()
        {
            LogUtility.DebugMethodStart();

            var row = this.form.Ichiran.CurrentRow;
            if (row == null)
            {
                return;
            }

            row.Cells[ConstCls.INPUT_KBN].Value = "1";
            row.Cells[ConstCls.INPUT_KBN_NAME].Value = ConstCls.INPUT_KBN_1;
            row.Cells[ConstCls.KANSANCHI].ReadOnly = false;
            if (row.Cells[ConstCls.TEKIYOU_BEGIN].Visible)
            {
                row.Cells[ConstCls.TEKIYOU_BEGIN].Value = this.form.sysDate;
                row.Cells[ConstCls.TEKIYOU_END].Value = string.Empty;
            }

            if (this.genba != null && row.Cells[ConstCls.GENBA_TEKIYOU_BEGIN].Visible)
            {
                if (!this.genba.TEKIYOU_BEGIN.IsNull)
                {
                    row.Cells[ConstCls.GENBA_TEKIYOU_BEGIN].Value = this.genba.TEKIYOU_BEGIN;
                }
                if (!this.genba.TEKIYOU_END.IsNull)
                {
                    row.Cells[ConstCls.GENBA_TEKIYOU_END].Value = this.genba.TEKIYOU_END;
                }
            }


            LogUtility.DebugMethodEnd();

            return;
        }
    }
}
