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
using System.Reflection;
using Seasar.Quill.Attrs;
using System.Windows.Forms;
using System.Data.SqlTypes;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using SyaryoSentaku.App;
using SyaryoSentaku.DAO;
using SyaryoSentaku.Const;
using r_framework.APP.PopUp.Base;
using System.Collections.ObjectModel;
using r_framework.Dto;
using SyaryoSentaku.DTO;
using Seasar.Framework.Exceptions;

namespace SyaryoSentaku.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic
    {
        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// システム設定
        /// </summary>
        internal M_SYS_INFO SysInfo { get; set; }

        #endregion

        #region フィールド
        /// <summary>
        /// パターン一覧のDao
        /// </summary>
        private MSYDaoCls MSYDaoPatern;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "SyaryoSentaku.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm MyForm;

        private MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao SysInfoDao;

        #endregion

        #region メソッド
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.MyForm = targetForm;
            this.MyForm.KeyPreview = true;
            this.MSYDaoPatern = DaoInitUtility.GetComponent<MSYDaoCls>();
            this.SysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit(out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart();

                SysInfo = this.SysInfoDao.GetAllDataForCode("0");

                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない start
                this.MyForm.KENCONDITION_ITEM.Text = "1";
                this.MyForm.KENCONDITION_ITEM.Focus();
                // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない end

                // 検索パラメタを取得
                string paramIn_GyosyaCode = "";
                string paramIn_CarCode = "";
                string paramIn_CarKind = "";
                string paramIn_UntenshaCode = "";
                string paramIn_ClosedDate = "";
                string paramIn_TekiyouFlg = "";
                string paramIn_GyoushaTekiyouFlg = "";
                string paramIn_GyoushaKbn = "";
                string paramIn_UnpanGyousyaKbn = "";
                string paramIn_ManiKbn = "";
                // #115564 車輛CDを検索方法を部分一致にしたい start
                string paramIn_SharyouFlg = "";
                // #115564 車輛CDを検索方法を部分一致にしたい end
                string paramIn_NaviKbn = "";
                Collection<PopupSearchSendParamDto> SendParams = this.MyForm.PopupSearchSendParams;
                foreach (PopupSearchSendParamDto dto in SendParams)
                {
                    object[] control = this.MyForm.controlUtil.FindControl(this.MyForm.ParentControls, new string[] { dto.Control });
                    if (dto.KeyName.Equals("key001")) paramIn_GyosyaCode = PropertyUtility.GetTextOrValue(control[0]);
                    if (dto.KeyName.Equals("key002")) paramIn_CarCode = PropertyUtility.GetTextOrValue(control[0]);
                    if (dto.KeyName.Equals("key003")) paramIn_CarKind = PropertyUtility.GetTextOrValue(control[0]);
                    if (dto.KeyName.Equals("key004")) paramIn_UntenshaCode = PropertyUtility.GetTextOrValue(control[0]);
                    if (dto.KeyName.Equals("key005")) paramIn_GyoushaKbn = dto.Value;
                    if (dto.KeyName.Equals("key006")) paramIn_SharyouFlg = dto.Value;
                    if (this.MyForm.WindowId == WINDOW_ID.M_SHARYOU_CLOSED)
                    {
                        if (dto.KeyName.Equals("SAGYOU_DATE")) paramIn_ClosedDate = PropertyUtility.GetTextOrValue(control[0]);
                        if (dto.KeyName.Equals("DENPYOU_DATE")) paramIn_ClosedDate = PropertyUtility.GetTextOrValue(control[0]);
                    }
                    if (dto.KeyName.Equals("TEKIYOU_FLG")) paramIn_TekiyouFlg = dto.Value;
                    if (dto.KeyName.Equals("GYOUSHA_TEKIYOU_FLG"))
                    {
                        if (control.Length > 0 && control[0] != null)
                        {
                            paramIn_GyoushaTekiyouFlg = "TRUE";
                            string value = PropertyUtility.GetTextOrValue(control[0]);
                            if(!string.IsNullOrEmpty(value))
                            {
                                DateTime time = this.MyForm.sysDate.Date;
                                if (DateTime.TryParse(value, out time))
                                {
                                    paramIn_GyoushaTekiyouFlg = time.ToString();
                                }
                            }
                        }
                        else
                        {
                            paramIn_GyoushaTekiyouFlg = dto.Value;
                        }
                    }

                    if (dto.KeyName.Equals("UNPAN_JUTAKUSHA_KAISHA_KBN")) paramIn_UnpanGyousyaKbn = dto.Value;
                    if (dto.KeyName.Equals("GYOUSHAKBN_MANI")) paramIn_ManiKbn = dto.Value;
                    if (dto.KeyName.Equals("NAVI_KBN")) paramIn_NaviKbn = dto.Value;
                }

                //パラメータ設定
                Properties.Settings.Default.ParamIn_GyosyaCode = paramIn_GyosyaCode;
                Properties.Settings.Default.ParamIn_CarCode = paramIn_CarCode;
                Properties.Settings.Default.ParamIn_CarKind = paramIn_CarKind;
                Properties.Settings.Default.ParamIn_UntenshaCode = paramIn_UntenshaCode;
                Properties.Settings.Default.ParamIn_ClosedDate = paramIn_ClosedDate;
                Properties.Settings.Default.paramIn_TekiyouFlg = paramIn_TekiyouFlg;
                Properties.Settings.Default.paramIn_GyoushaTekiyouFlg = paramIn_GyoushaTekiyouFlg;
                Properties.Settings.Default.paramIn_GyoushaKbn = paramIn_GyoushaKbn;
                Properties.Settings.Default.paramIn_UnpanGyousyaKbn = paramIn_UnpanGyousyaKbn;
                Properties.Settings.Default.paramIn_ManiKbn = paramIn_ManiKbn;
                // #115564 車輛CDを検索方法を部分一致にしたい start
                Properties.Settings.Default.paramIn_SharyouFlg = paramIn_SharyouFlg;
                // #115564 車輛CDを検索方法を部分一致にしたい end
                Properties.Settings.Default.paramIn_NaviKbn = paramIn_NaviKbn;
                Properties.Settings.Default.Save();

                // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない start
                // 検索実行
                int count = this.Search();
                if (count == -1)
                {
                    catchErr = false;
                }
                else if (count > 0)
                {
                    if (!this.SetIchiran())
                    {
                        catchErr = false;
                    }
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }
                // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない end
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(false, catchErr);
            return false;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        [Transaction]
        public virtual int Search()
        {
            int cnt = 0;
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResult = new DataTable();

                // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない start
                //M_SHARYOU srya = new M_SHARYOU();
                SerchParameterDtoCls sry = new SerchParameterDtoCls();
                sry.SHARYOU_CD = Properties.Settings.Default.ParamIn_CarCode;
                sry.GYOUSHA_CD = Properties.Settings.Default.ParamIn_GyosyaCode;
                sry.SHASYU_CD = Properties.Settings.Default.ParamIn_CarKind;
                sry.SHAIN_CD = Properties.Settings.Default.ParamIn_UntenshaCode;
                sry.GYOUSHAKBN = Properties.Settings.Default.paramIn_GyoushaKbn;
                if (this.MyForm.WindowId == WINDOW_ID.M_SHARYOU_CLOSED)
                {
                    sry.CLOSED_DATE = Properties.Settings.Default.ParamIn_ClosedDate;
                }

                if (!string.IsNullOrEmpty(this.MyForm.KENCONDITION_VALUE.Text))
                {
                    // シングルクォートは2つ重ねる
                    var condition = this.MyForm.KENCONDITION_VALUE.Text.Replace("'", "''");
                    sry.KENCONDITION_ITEM = this.MyForm.KENCONDITION_ITEM.Text;
                    sry.KENCONDITION_VALUE = condition;
                }
                // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない end

                if (!string.IsNullOrEmpty(Properties.Settings.Default.paramIn_TekiyouFlg)
                    && "FALSE".Equals(Properties.Settings.Default.paramIn_TekiyouFlg.ToUpper()))
                {
                    sry.DELETE_FLG = false;
                }

                if (!string.IsNullOrEmpty(Properties.Settings.Default.paramIn_GyoushaTekiyouFlg))
                {
                    if("FALSE".Equals(Properties.Settings.Default.paramIn_GyoushaTekiyouFlg.ToUpper()))
                    {
                        sry.GYOUSHA_TEKIYOU_FLG = false;
                    }
                    else if ("TRUE".Equals(Properties.Settings.Default.paramIn_GyoushaTekiyouFlg.ToUpper()))
                    {
                        sry.GYOUSHA_TEKIYOU_FLG = true;
                    }
                    else
                    {
                        sry.GYOUSHA_TEKIYOU_FLG = true;
                        sry.TEKIYOU_DATE = Properties.Settings.Default.paramIn_GyoushaTekiyouFlg;
                    }
                }

                if (!string.IsNullOrEmpty(Properties.Settings.Default.paramIn_UnpanGyousyaKbn)
                 && "TRUE".Equals(Properties.Settings.Default.paramIn_UnpanGyousyaKbn.ToUpper()))
                {
                    sry.UNPAN_JUTAKUSHA_KAISHA_KBN = true;
                }
                else
                {
                    sry.UNPAN_JUTAKUSHA_KAISHA_KBN = false;
                }

                if (!string.IsNullOrEmpty(Properties.Settings.Default.paramIn_ManiKbn)
                 && "TRUE".Equals(Properties.Settings.Default.paramIn_ManiKbn.ToUpper()))
                {
                    sry.GYOUSHAKBN_MANI = true;
                }
                else
                {
                    sry.GYOUSHAKBN_MANI = false;
                }
                // #115564 車輛CDを検索方法を部分一致にしたい start
                if (string.IsNullOrEmpty(Properties.Settings.Default.paramIn_SharyouFlg)
                    || "FALSE".Equals(Properties.Settings.Default.paramIn_SharyouFlg.ToUpper()))
                {
                    this.SearchResult = MSYDaoPatern.GetDataForEntity(sry);
                }
                else if (!string.IsNullOrEmpty(Properties.Settings.Default.paramIn_SharyouFlg)
                         && "TRUE".Equals(Properties.Settings.Default.paramIn_SharyouFlg.ToUpper()))
                {
                    if (!string.IsNullOrEmpty(sry.SHARYOU_CD) || (sry.KENCONDITION_ITEM == "1" && !string.IsNullOrEmpty(sry.KENCONDITION_VALUE)))
                    {
                        DataTable dtbtmp = MSYDaoPatern.GetDataForEntityMod(sry);
                        if (dtbtmp != null && dtbtmp.Rows.Count > 0)
                        {
                            string tmpSharyou = sry.SHARYOU_CD;
                            if (sry.KENCONDITION_ITEM == "1" && !string.IsNullOrEmpty(sry.KENCONDITION_VALUE))
                            {
                                tmpSharyou = sry.KENCONDITION_VALUE;
                            }
                            this.SearchResult = dtbtmp.Clone();

                            foreach (DataRow row in dtbtmp.Rows)
                            {
                                DataRow newRow = this.SearchResult.NewRow();
                                newRow.ItemArray = row.ItemArray;
                                this.SearchResult.Rows.Add(newRow);
                            }
                        }
                        else
                        {
                            this.SearchResult = new DataTable();
                        }
                    }
                    else
                    {
                        this.SearchResult = MSYDaoPatern.GetDataForEntity(sry);
                    }
                }

                //this.SearchResult = MSYDaoPatern.GetDataForEntity(sry);
                // #115564 車輛CDを検索方法を部分一致にしたい end
                if (!string.IsNullOrEmpty(Properties.Settings.Default.paramIn_NaviKbn)
 && "TRUE".Equals(Properties.Settings.Default.paramIn_NaviKbn.ToUpper()))
                {
                    this.SearchResult = MSYDaoPatern.GetNaviDataForEntity(sry);
                }
                else
                {
                    this.SearchResult = MSYDaoPatern.GetDataForEntity(sry);
                }

                cnt = this.SearchResult.Rows.Count;

                if (cnt == 0)
                {
                    MessageBox.Show(ConstCls.SearchEmptInfo, ConstCls.DialogTitle);
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                cnt = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245", "");
                cnt = -1;
            }

            LogUtility.DebugMethodEnd(cnt);
            return cnt;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (SuperPopupForm)this.MyForm;
            var controlUtil = new ControlUtility();

            foreach (var button in buttonSetting)
            {
                //設定対象のコントロールを探して名称の設定を行う
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                cont.Text = button.IchiranButtonName;
                cont.Tag = button.IchiranButtonHintText;
            }
            
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BasePopForm)this.MyForm.Parent;

            //新規ボタン(F2)イベント生成
            //this.MyForm.bt_func2.DialogResult = DialogResult.None;               // No.1865
            //this.MyForm.bt_func2.Click += new EventHandler(this.MyForm.NewAdd);  // No.1865

            // 条件入力ボタン(F5)イベント生成
            this.MyForm.bt_func5.Click += new EventHandler(this.MyForm.JoukennNyuuryoku);

            //検索条件クリア(F7)検索条件クリア
            this.MyForm.bt_func7.Click += new EventHandler(this.MyForm.FormClear);

            // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない start
            //検索ボタン(F8)イベント生成
            this.MyForm.bt_func8.Click += new EventHandler(this.MyForm.Search);
            // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない end

            //確定ボタン(F9)イベント生成
            this.MyForm.bt_func9.Click += new EventHandler(this.MyForm.FormConfirm);

            //並び替えボタン(F10)イベント生成
            this.MyForm.bt_func10.Click += new EventHandler(this.MyForm.Sort);

            //閉じるボタン(F12)イベント生成
            this.MyForm.bt_func12.DialogResult = DialogResult.Cancel;
            this.MyForm.bt_func12.Click += new EventHandler(this.MyForm.FormClose);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //前の結果をクリア
                int k = this.MyForm.CarIchiran.Rows.Count;
                for (int i = k; i >= 1; i--)
                {
                    this.MyForm.CarIchiran.Rows.RemoveAt(this.MyForm.CarIchiran.Rows[i - 1].Index);
                }

                //検索結果を設定する
                var table = this.SearchResult;
                this.MyForm.CarIchiran.DataSource = table;
                //table.BeginLoadData();
                this.MyForm.CarIchiran.Columns["車輌CD"].Width = 130;
                this.MyForm.CarIchiran.Columns["車輌名"].Width = 170;
                this.MyForm.CarIchiran.Columns["業者CD"].Width = 150;
                this.MyForm.CarIchiran.Columns["業者名"].Width = 160;
                this.MyForm.CarIchiran.Columns["空車重量"].Width = 150;
                this.MyForm.CarIchiran.Columns["運転者CD"].Width = 0;
                this.MyForm.CarIchiran.Columns["運転者名"].Width = 0;
                this.MyForm.CarIchiran.Columns["車輌CD"].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.MyForm.CarIchiran.Columns["車輌名"].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.MyForm.CarIchiran.Columns["業者CD"].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.MyForm.CarIchiran.Columns["業者名"].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.MyForm.CarIchiran.Columns["空車重量"].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.MyForm.CarIchiran.Columns["車輌CD"].ToolTipText = ConstCls.ToolTipText1;
                this.MyForm.CarIchiran.Columns["車輌名"].ToolTipText = ConstCls.ToolTipText2;
                this.MyForm.CarIchiran.Columns["業者CD"].ToolTipText = ConstCls.ToolTipText3;
                this.MyForm.CarIchiran.Columns["業者名"].ToolTipText = ConstCls.ToolTipText4;
                this.MyForm.CarIchiran.Columns["空車重量"].ToolTipText = ConstCls.ToolTipText5;

                this.MyForm.CarIchiran.Columns["車種CD"].Visible = false;
                this.MyForm.CarIchiran.Columns["車種名"].Visible = false;
                this.MyForm.CarIchiran.Columns["運転者CD"].Visible = false;
                this.MyForm.CarIchiran.Columns["運転者名"].Visible = false;

                //検索結果設定
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //    this.MyForm.CarIchiran.Rows.Add();
                    //    this.MyForm.CarIchiran.Rows[i].Cells["CAR_CODE"].Value = table.Rows[i]["SHARYOU_CD"];
                    //    this.MyForm.CarIchiran.Rows[i].Cells["CAR_NAME"].Value = table.Rows[i]["SHARYOU_NAME"];
                    //    this.MyForm.CarIchiran.Rows[i].Cells["GYOSYA_CODE"].Value = table.Rows[i]["GYOUSHA_CD"];
                    //    this.MyForm.CarIchiran.Rows[i].Cells["GYOSYA_NAME"].Value = table.Rows[i]["GYOUSHA_NAME_RYAKU"];
                    //    this.MyForm.CarIchiran.Rows[i].Cells["CAR_WEIGHT"].Value = table.Rows[i]["KUUSHA_JYURYO"];
                    this.MyForm.CarIchiran.Rows[i].Cells["車輌CD"].Tag = ConstCls.ToolTipText1;
                    this.MyForm.CarIchiran.Rows[i].Cells["車輌名"].Tag = ConstCls.ToolTipText2;
                    this.MyForm.CarIchiran.Rows[i].Cells["業者CD"].Tag = ConstCls.ToolTipText3;
                    this.MyForm.CarIchiran.Rows[i].Cells["業者名"].Tag = ConstCls.ToolTipText4;
                    this.MyForm.CarIchiran.Rows[i].Cells["空車重量"].Tag = ConstCls.ToolTipText5;
                }
                // 初期フォーカスを表示結果の一番上の行に設定する。
                this.MyForm.CarIchiran.Focus();
                if (table.Rows.Count > 0)
                {
                    this.MyForm.CarIchiran.CurrentCell = this.MyForm.CarIchiran.Rows[0].Cells["車輌CD"];
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        public void ClearCondition()
        {
            LogUtility.DebugMethodStart();

            Properties.Settings.Default.ParamIn_GyosyaCode = String.Empty;
            Properties.Settings.Default.ParamIn_CarCode = String.Empty;
            Properties.Settings.Default.ParamIn_CarKind = String.Empty;
            Properties.Settings.Default.Save();

            LogUtility.DebugMethodEnd();
        }

        // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない start
        /// <summary>
        /// 検索条件入力欄のIME制御
        /// </summary>
        internal void ImeControlCondition()
        {
            switch (this.MyForm.KENCONDITION_ITEM.Text)
            {
                case "1":
                    this.MyForm.KENCONDITION_VALUE.ImeMode = ImeMode.Disable;
                    break;

                case "2":
                    this.MyForm.KENCONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                case "3":
                    this.MyForm.KENCONDITION_VALUE.ImeMode = ImeMode.Disable;
                    break;

                case "4":
                    this.MyForm.KENCONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                case "5":
                    this.MyForm.KENCONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                default:
                    break;
            }
        }
        // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない end

        /// <summary>
        /// 適用処理
        /// </summary>
        public void FormConfirm()
        {
            LogUtility.DebugMethodStart();

            if (this.MyForm.CarIchiran.RowCount > 0)
            {
                this.MyForm.ParamOut_SyaryouCode = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["車輌CD"].Value.ToString();
                this.MyForm.ParamOut_SyaryouName = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["車輌名"].Value.ToString();
                this.MyForm.ParamOut_GyosyaCode = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["業者CD"].Value.ToString();
                this.MyForm.ParamOut_GyosyaName = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["業者名"].Value.ToString();
                this.MyForm.ParamOut_ShashuCode = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["車種CD"].Value.ToString();
                this.MyForm.ParamOut_ShashuName = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["車種名"].Value.ToString();
                this.MyForm.ParamOut_UntenCode = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["運転者CD"].Value.ToString();
                this.MyForm.ParamOut_UntenName = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["運転者名"].Value.ToString();
                this.MyForm.ParamOut_KuushaJyuryo = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["空車重量"].Value.ToString();    // No.3875
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 一覧明細のダブルクリック制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool CarIchiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.MyForm.CarIchiran.CurrentRow == null)
            {
                return true;
            }

            if (e.RowIndex != -1 && this.MyForm.CarIchiran.RowCount > 0)
            {
                this.MyForm.CarIchiran.CurrentCell = this.MyForm.CarIchiran.Rows[e.RowIndex].Cells["車輌CD"];

                this.MyForm.ParamOut_SyaryouCode = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["車輌CD"].Value.ToString();
                this.MyForm.ParamOut_SyaryouName = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["車輌名"].Value.ToString();
                this.MyForm.ParamOut_GyosyaCode = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["業者CD"].Value.ToString();
                this.MyForm.ParamOut_GyosyaName = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["業者名"].Value.ToString();
                this.MyForm.ParamOut_ShashuCode = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["車種CD"].Value.ToString();
                this.MyForm.ParamOut_ShashuName = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["車種名"].Value.ToString();
                this.MyForm.ParamOut_UntenCode = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["運転者CD"].Value.ToString();
                this.MyForm.ParamOut_UntenName = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["運転者名"].Value.ToString();
                this.MyForm.ParamOut_KuushaJyuryo = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["空車重量"].Value.ToString();   // No.3875

                return true;
            }
            return false;
        }

        internal bool ElementDecision()
        {

            if (this.MyForm.CarIchiran.CurrentRow != null && this.MyForm.CarIchiran.RowCount > 0)
            {
                this.MyForm.ParamOut_SyaryouCode = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["車輌CD"].Value.ToString();
                this.MyForm.ParamOut_SyaryouName = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["車輌名"].Value.ToString();
                this.MyForm.ParamOut_GyosyaCode = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["業者CD"].Value.ToString();
                this.MyForm.ParamOut_GyosyaName = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["業者名"].Value.ToString();
                this.MyForm.ParamOut_ShashuCode = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["車種CD"].Value.ToString();
                this.MyForm.ParamOut_ShashuName = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["車種名"].Value.ToString();
                this.MyForm.ParamOut_UntenCode = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["運転者CD"].Value.ToString();
                this.MyForm.ParamOut_UntenName = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["運転者名"].Value.ToString();
                this.MyForm.ParamOut_KuushaJyuryo = this.MyForm.CarIchiran.Rows[this.MyForm.CarIchiran.CurrentCell.RowIndex].Cells["空車重量"].Value.ToString();   // No.3875

                return true;
            }
            return false;

            //Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
            //List<PopupReturnParam> setParam;
            //for (int i = 1; i < this.MyForm.CarIchiran.Columns.Count; i++)
            //{
            //    PopupReturnParam popupParam = new PopupReturnParam();
            //    var setDate = this.MyForm.CarIchiran[i, this.MyForm.CarIchiran.CurrentRow.Index];

            //    var control = setDate as DataGridViewTextBoxCell;

            //    popupParam.Key = "Value";

            //    popupParam.Value = setDate.Value.ToString();

            //    if (setParamList.ContainsKey(i))
            //    {
            //        setParam = setParamList[i];
            //    }
            //    else
            //    {
            //        setParam = new List<PopupReturnParam>();
            //    }

            //    setParam.Add(popupParam);


            //    setParamList.Add(i, setParam);
            //}

            //this.MyForm.ReturnParams = setParamList;
            //this.MyForm.Close();
        }

        public void PhysicalDelete()
        {
            //機能なし
        }

        public void LogicalDelete()
        {
            //機能なし
        }

        public void Update(bool errorFlag)
        {
            //機能なし
        }

        public void Regist(bool errorFlag)
        {
            //機能なし
        }

        #endregion
    }
}
