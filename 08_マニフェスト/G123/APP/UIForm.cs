using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using System.Data.SqlTypes;

namespace Shougun.Core.PaperManifest.ManifestHimoduke
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass Logic;

        /// <summary>
        /// 並び替え文字列を格納します
        /// </summary>
        internal string dateTimeTypeOrder = string.Empty;

        /// <summary>
        /// 紐付呼び出し情報
        /// </summary>
        public ManiRelrationCallParameter relationParam { get; private set; }

        /// <summary>
        /// 紐付結果
        /// </summary>
        public ManiRelrationResult RelationResult { get; protected set; }

        internal bool isSearchErr { get; set; }
        internal bool isRegistErr { get; set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private string befHstGyousha = string.Empty;
        private string befSbnGyousha = string.Empty;

        #endregion

        public UIForm(ManiRelrationCallParameter param)
            : base(WINDOW_ID.T_MANIFEST_HIMODUKE, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);

            this.relationParam = param;

            // 20140519 kayo No.734 機能追加 start
            this.RelationResult = new ManiRelrationResult(param);
            // 20140519 kayo No.734 機能追加 end
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
            if (!this.Logic.WindowInit()) { return; }

            if (!isShown)
            {
                this.Height -= 10;
                isShown = true;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.cdgv_FirstMani != null)
            {
                this.cdgv_FirstMani.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            base.OnShown(e);
        }

        /// <summary>
        /// 画面からはこの関数を呼んでくださいrefパラメータに結果がセットされます
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="HaikiKbnCD"></param>
        /// <param name="second_system_id"></param>
        /// <param name="ctxt_KansangoTotalSuryo"></param>
        /// <param name="maniRelation"></param>
        /// <returns></returns>
        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
        //public static DialogResult DoRelation(WINDOW_TYPE mode, string HaikiKbnCD, string second_system_id, string ctxt_KansangoTotalSuryo, string second_kanri_id, ref ManiRelrationResult maniRelation)
        public static DialogResult DoRelation(WINDOW_TYPE mode, string HaikiKbnCD, string second_system_id, string second_detail_system_id, string ctxt_KansangoTotalSuryo, string second_kanri_id, ref ManiRelrationResult maniRelation)
        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end
        {
            // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
            //LogUtility.DebugMethodStart(mode, HaikiKbnCD, second_system_id, ctxt_KansangoTotalSuryo, second_kanri_id, maniRelation);
            LogUtility.DebugMethodStart(mode, HaikiKbnCD, second_system_id, second_detail_system_id, ctxt_KansangoTotalSuryo, second_kanri_id, maniRelation);
            // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end

            Shougun.Core.PaperManifest.ManifestHimoduke.UIForm callForm;

            // 20140519 kayo No.734 機能追加 start
            ////マニフェスト紐付画面を呼び出し
            //if (maniRelation == null)
            //{
            //    if (mode == WINDOW_TYPE.NEW_WINDOW_FLAG)
            //    {
            //        //新規
            //        callForm = new Shougun.Core.PaperManifest.ManifestHimoduke.UIForm(new ManifestHimoduke.ManiRelrationCallParameter(HaikiKbnCD, null, ctxt_KansangoTotalSuryo));
            //    }
            //    else
            //    {
            //        //更新
            //        callForm = new Shougun.Core.PaperManifest.ManifestHimoduke.UIForm(new ManifestHimoduke.ManiRelrationCallParameter(HaikiKbnCD, Int64.Parse(second_system_id), ctxt_KansangoTotalSuryo));
            //    }
            //}
            //else
            //{
            //    //2回目以降
            //    callForm = new Shougun.Core.PaperManifest.ManifestHimoduke.UIForm(new ManifestHimoduke.ManiRelrationCallParameter(maniRelation, ctxt_KansangoTotalSuryo));
            //}
            // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
            //callForm = new Shougun.Core.PaperManifest.ManifestHimoduke.UIForm(new ManifestHimoduke.ManiRelrationCallParameter(HaikiKbnCD, Int64.Parse(second_system_id), ctxt_KansangoTotalSuryo, second_kanri_id));
            callForm = new Shougun.Core.PaperManifest.ManifestHimoduke.UIForm(new ManifestHimoduke.ManiRelrationCallParameter(HaikiKbnCD, Int64.Parse(second_system_id), Int64.Parse(second_detail_system_id), ctxt_KansangoTotalSuryo, second_kanri_id));
            // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end
            // 20140519 kayo No.734 機能追加 end

            var callHeader = new Shougun.Core.PaperManifest.ManifestHimoduke.UIHeader();
            var popForm = new Shougun.Core.Common.BusinessCommon.Base.BaseForm.BasePopForm(callForm, callHeader);
            // 20150909 katen #12048 「システム日付」の基準作成、適用 start
            r_framework.Dao.GET_SYSDATEDao dao = DaoInitUtility.GetComponent<r_framework.Dao.GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT CONVERT(DATE, GETDATE()) AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                //取得した場合(基本的に取得できないのはありえない)
                DateTime sysDate = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);//取得した結果をDateTimeに転換する
                popForm.sysDate = sysDate;//画面フォームにDBサーバ日付を設定する
            }
            // 20150909 katen #12048 「システム日付」の基準作成、適用 end

            var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);

            if (!isExistForm)
            {
                popForm.ShowDialog();
                maniRelation = callForm.RelationResult;
                LogUtility.DebugMethodEnd(maniRelation.result, maniRelation);
                return maniRelation.result;
            }

            //画面を開けなかった場合 abortを返す
            LogUtility.DebugMethodEnd(DialogResult.Abort, null);
            return DialogResult.Abort;
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }

        #region メソッド
        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        public void HeaderCheckBoxSupport()
        {
            DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

            newColumn.Name = "";
            newColumn.Width = 25;
            DatagridViewCheckBoxHeaderCell newheader = new DatagridViewCheckBoxHeaderCell(0);

            newColumn.HeaderCell = newheader;

            if (cdgv_FirstMani.Columns.Count > 0)
            {
                cdgv_FirstMani.Columns.Insert(0, newColumn);
            }
            else
            {
                cdgv_FirstMani.Columns.Add(newColumn);
            }
        }
        #region コントロール値設定処理

        /// <summary>
        /// 検索結果をグリッドに設定
        /// </summary>
        /// <param name="HimodukeFlg">TRUE場合: 初期化時に紐付済みデータ表示する</param>
        public void SetDataToDgv(bool HimodukeFlg)
        {
            LogUtility.DebugMethodStart(HimodukeFlg);

            //全てDBNULL許可
            for (int i = 0; i < this.Logic.TotalResults.Columns.Count; i++)
            {
                this.Logic.TotalResults.Columns[i].AllowDBNull = true;
            }
            this.Logic.TotalResults.Clear();

            if (this.Logic.RelationResult != null)
            {
                for (int i = 0; i < this.Logic.RelationResult.Columns.Count; i++)
                {
                    this.Logic.RelationResult.Columns[i].AllowDBNull = true;
                }
                // 日付範囲によって並べ替え
                this.Logic.RelationResult = this.Logic.GetSortedDataTable(this.Logic.RelationResult, this.dateTimeTypeOrder);

                //常時 元紐付情報は表示する
                this.Logic.TotalResults.Merge(this.Logic.RelationResult);
            }

            if (this.Logic.SearchResult != null)
            {
                for (int i = 0; i < this.Logic.SearchResult.Columns.Count; i++)
                {
                    this.Logic.SearchResult.Columns[i].AllowDBNull = true;
                }
                this.Logic.TotalResults.Merge(this.Logic.SearchResult);
            }

            //紐付済みのデータは、チェック状態を検索前に合わせる

            // 明細のチェックボックスにフォーカスがあるとON/OFFの切り替えに不都合が生じるのでActiveControlをnullにしておく
            this.ActiveControl = null;
            //行を全て削除
            cdgv_FirstMani.Rows.Clear();
            //検索結果設定
            for (int i = 0; i < this.Logic.TotalResults.Rows.Count; i++)
            {
                cdgv_FirstMani.Rows.Add();

                //毎行のデータにシステムIDがTagに記憶する
                this.Logic.RelationInfo = new RelationInfo_DTOCls();


                if (DBNull.Value != this.Logic.TotalResults.Rows[i]["TIME_STAMP"])
                {
                    this.Logic.RelationInfo.HimodukeFlg = "1"; //紐付済データを記憶する
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)cdgv_FirstMani.Rows[i].Cells[0];
                    checkCell.Value = true;
                }
                else
                {
                    this.Logic.RelationInfo.HimodukeFlg = "0"; //紐付未データを記憶する
                }

                string maniType = this.Logic.TotalResults.Rows[i]["HAIKI_KBN_CD"].ToString();
                if (maniType == "4")//電子場合
                {
                    if (DBNull.Value != this.Logic.TotalResults.Rows[i]["SYSTEM_ID"])
                    {
                        this.Logic.RelationInfo.FIRST_SYSTEM_ID = this.Logic.TotalResults.Rows[i]["SYSTEM_ID"].ToString();
                    }
                    this.Logic.RelationInfo.MANIFEST_TYPE = this.Logic.TotalResults.Rows[i]["HAIKI_KBN_CD"].ToString(); //一次マニ種類(４：電子固定)
                    this.Logic.RelationInfo.KANRI_ID = this.Logic.TotalResults.Rows[i]["KANRI_ID"].ToString();          //管理番号
                    this.Logic.RelationInfo.MANIFEST_ID = this.Logic.TotalResults.Rows[i]["MANIFEST_ID"].ToString();    //交付番号
                    this.Logic.RelationInfo.HAIKI_NAME_CD = this.Logic.TotalResults.Rows[i]["HAIKI_NAME_CD"].ToString(); //廃棄物名称CD
                    this.Logic.RelationInfo.KANSAN_SUU = this.Logic.TotalResults.Rows[i]["KANSAN_SUU"].ToString();      //換算後数量


                    object tmp = this.Logic.TotalResults.Rows[i]["R18EX_SYSTEM_ID"];

                    if (tmp is DBNull)
                    {
                        this.Logic.RelationInfo.DT_R18_EX_SYSTEM_ID = SqlInt64.Null;
                    }
                    else
                    {
                        this.Logic.RelationInfo.DT_R18_EX_SYSTEM_ID = (Int64)tmp;
                    }

                    tmp = this.Logic.TotalResults.Rows[i]["R18EX_SEQ"];
                    if (tmp is DBNull)
                    {
                        this.Logic.RelationInfo.DT_R18_EX_SEQ = SqlInt32.Null;
                    }
                    else
                    {
                        this.Logic.RelationInfo.DT_R18_EX_SEQ = (Int32)tmp;
                    }

                    tmp = this.Logic.TotalResults.Rows[i]["R18EX_TIME_STAMP"];
                    if (tmp is DBNull)
                    {
                        this.Logic.RelationInfo.DT_R18_EX_TIME_STAMP = null;
                    }
                    else
                    {
                        this.Logic.RelationInfo.DT_R18_EX_TIME_STAMP = Shougun.Core.Common.BusinessCommon.Utility.ConvertStrByte.In32ToByteArray((Int32)tmp);
                    }

                    if (this.Logic.TotalResults.Rows[i]["LAST_SBN_GENBA_NAME_AND_ADDRESS"] != null)
                    {
                        this.Logic.RelationInfo.LAST_SBN_GENBA_NAME_AND_ADDRESS = this.Logic.TotalResults.Rows[i]["LAST_SBN_GENBA_NAME_AND_ADDRESS"].ToString();
                    }

                    if (this.Logic.TotalResults.Rows[i]["LAST_SBN_END_DATE"] != null)
                    {
                        this.Logic.RelationInfo.LAST_SBN_END_DATE = this.Logic.TotalResults.Rows[i]["LAST_SBN_END_DATE"].ToString();
                    }

                    if (this.Logic.TotalResults.Rows[i]["LATEST_SEQ"] != null)
                    {
                        this.Logic.RelationInfo.LATEST_SEQ = SqlInt32.Parse(this.Logic.TotalResults.Rows[i]["LATEST_SEQ"].ToString());
                    }

                    cdgv_FirstMani.Rows[i].Tag = this.Logic.RelationInfo;
                }
                else//紙の場合、明細システムIDとマニ種類記憶する
                {
                    this.Logic.RelationInfo.FIRST_SYSTEM_ID = this.Logic.TotalResults.Rows[i]["DETAIL_SYSTEM_ID"].ToString();//一次SYSTEM_ID
                    this.Logic.RelationInfo.MANIFEST_TYPE = this.Logic.TotalResults.Rows[i]["HAIKI_KBN_CD"].ToString(); //一次マニ種類
                    this.Logic.RelationInfo.MANIFEST_ID = this.Logic.TotalResults.Rows[i]["MANIFEST_ID"].ToString(); //交付番号

                    this.Logic.RelationInfo.TME_SYSTEM_ID = (Int64)this.Logic.TotalResults.Rows[i]["TME_SYSTEM_ID"];
                    this.Logic.RelationInfo.TME_SEQ = (Int32)this.Logic.TotalResults.Rows[i]["TME_SEQ"];
                    this.Logic.RelationInfo.TME_TIME_STAMP = Shougun.Core.Common.BusinessCommon.Utility.ConvertStrByte.In32ToByteArray((Int32)this.Logic.TotalResults.Rows[i]["TME_TIME_STAMP"]);

                    cdgv_FirstMani.Rows[i].Tag = this.Logic.RelationInfo;
                }

                for (int j = 1; j < cdgv_FirstMani.Columns.Count; j++)
                {
                    if (j == 1)//委託契約有無の設定
                    {
                        //仕様未明ですから、NULLを設定する
                        cdgv_FirstMani.Rows[i].Cells[j].Value = string.Empty;
                    }
                    else if (j == 3)//交付日付
                    {
                        if (this.Logic.TotalResults.Rows[i][j] is DBNull)
                        {
                            cdgv_FirstMani.Rows[i].Cells[j].Value = null;
                        }
                        else
                        {
                            String dateDay = this.Logic.TotalResults.Rows[i][j].ToString();
                            if (!string.IsNullOrEmpty(dateDay))
                            {
                                cdgv_FirstMani.Rows[i].Cells[j].Value = Convert.ToDateTime(dateDay).ToString("yyyy/MM/dd(ddd)");
                            }
                        }
                    }
                    else if (j == 4)//運搬終了日
                    {
                        if (this.Logic.TotalResults.Rows[i]["LAST_UPN_END_DATE"] is DBNull)
                        {
                            cdgv_FirstMani.Rows[i].Cells[j].Value = null;
                        }
                        else
                        {
                            String dateDay = this.Logic.TotalResults.Rows[i]["LAST_UPN_END_DATE"].ToString();
                            if (!string.IsNullOrEmpty(dateDay))
                            {
                                cdgv_FirstMani.Rows[i].Cells[j].Value = Convert.ToDateTime(dateDay).ToString("yyyy/MM/dd(ddd)");
                            }
                        }
                    }
                    else if (j == 5)//処分終了日
                    {
                        if (this.Logic.TotalResults.Rows[i]["SBN_END_DATE"] is DBNull)
                        {
                            cdgv_FirstMani.Rows[i].Cells[j].Value = null;
                        }
                        else
                        {
                            String dateDay = this.Logic.TotalResults.Rows[i]["SBN_END_DATE"].ToString();
                            if (!string.IsNullOrEmpty(dateDay))
                            {
                                cdgv_FirstMani.Rows[i].Cells[j].Value = Convert.ToDateTime(dateDay).ToString("yyyy/MM/dd(ddd)");
                            }
                        }
                    }
                    else if (j < 3)//
                    {
                        cdgv_FirstMani.Rows[i].Cells[j].Value = this.Logic.TotalResults.Rows[i][j].ToString();
                    }
                    else// if (j > 5)
                    {
                        cdgv_FirstMani.Rows[i].Cells[j].Value = this.Logic.TotalResults.Rows[i][j - 2].ToString();
                    }
                }

                // 20140519 kayo No.734 機能追加 start
                //チェック判断
                //if (HimodukeFlg)
                //{
                //    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)cdgv_FirstMani.Rows[i].Cells[0];
                //    if (this.relationParam.preResult == null || !this.relationParam.preResult.IsSelected())
                //    {
                //        //初回は紐付のみなのですべてチェックON
                //        checkCell.Value = true;
                //    }
                //    else
                //    {
                //        //2回目以降の初回呼び出しは 前回選択のみON

                //        if (this.Logic.RelationInfo.MANIFEST_TYPE != "4")
                //        {
                //            //紙
                //            if (this.relationParam.preResult.regist_relations
                //                    .Where(x =>
                //                        (x.FIRST_HAIKI_KBN_CD.ToString() == this.Logic.RelationInfo.MANIFEST_TYPE &&
                //                        x.FIRST_SYSTEM_ID.ToString() == this.Logic.RelationInfo.FIRST_SYSTEM_ID))
                //                    .Count() > 0)
                //            {
                //                //チェックON
                //                checkCell.Value = true;
                //            }
                //            else
                //            {
                //                checkCell.Value = false;
                //            }

                //        }
                //        else
                //        {

                //            //電子
                //            if (this.relationParam.preResult.elecEntriesIns != null &&
                //                this.relationParam.preResult.elecEntriesIns
                //                    .Where(x => x.KANRI_ID == this.Logic.RelationInfo.KANRI_ID)
                //                    .Count() > 0)
                //            {
                //                //チェックON
                //                checkCell.Value = true;
                //            }
                //            else if (this.relationParam.preResult.elecEntriesUpd != null &&
                //                     this.relationParam.preResult.elecEntriesUpd
                //                        .Where(x => x.KANRI_ID == this.Logic.RelationInfo.KANRI_ID)
                //                        .Count() > 0)
                //            {
                //                //チェックON
                //                checkCell.Value = true;
                //            }
                //            else
                //            {
                //                checkCell.Value = false;
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    //二回目以降は、従前値を引き継ぐ
                //    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)cdgv_FirstMani.Rows[i].Cells[0];
                //    bool cheked = false;

                //    if (this.Logic.RelationInfo.MANIFEST_TYPE == "4")
                //    {
                //        //電子の場合
                //        if (this.Logic.dtoMani.KANRI_ID.Contains(this.Logic.RelationInfo.KANRI_ID))
                //        {
                //            cheked = true;
                //        }
                //    }
                //    else
                //    {
                //        //紙の場合
                //        if (this.Logic.dtoMani.DETAIL_SYSTEM_ID.Contains(SqlInt64.Parse(this.Logic.RelationInfo.FIRST_SYSTEM_ID)))
                //        {
                //            cheked = true;
                //        }
                //    }


                //    checkCell.Value = cheked;
                //}
                //二回目以降は、従前値を引き継ぐ
                if (HimodukeFlg)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)cdgv_FirstMani.Rows[i].Cells[0];
                    //初回は紐付のみなのですべてチェックON
                    checkCell.Value = true;
                }
                else
                {
                    bool cheked = false;
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)cdgv_FirstMani.Rows[i].Cells[0];
                    foreach (var relationInfo in this.Logic.dtoMani.relationInfoDtoList)
                    {
                        if (relationInfo.FIRST_SYSTEM_ID.ToString().Equals(this.Logic.RelationInfo.FIRST_SYSTEM_ID)
                            && relationInfo.MANIFEST_TYPE.ToString().Equals(this.Logic.RelationInfo.MANIFEST_TYPE))
                        {
                            cheked = true;
                        }

                        if (cheked)
                        {
                            break;
                        }
                    }

                    checkCell.Value = cheked;
                }

                // 20140519 kayo No.734 機能追加 end
            }

            // 20140609 kayo 不具合#4694 選択外クリアを押してから、再検索の表示不正 start
            int rowNo = 0;
            // 選択外クリアボタンを押したかどうかをチェックし、押されてあったら、次の処理を行う。
            if (this.Logic.dtoMani.DSP_DETAIL_SYSTEM_ID != null &&
                (this.Logic.dtoMani.DSP_DETAIL_SYSTEM_ID.Count > 0 || this.Logic.dtoMani.DSP_KANRI_ID.Count > 0))
            {
                // グレードビューをループする
                for (int i = 0; i < cdgv_FirstMani.Rows.Count; i++)
                {
                    DataGridViewRow r = cdgv_FirstMani.Rows[i];

                    // チェックボックスがチェックされてあったら、次の処理を行う。
                    if (!Convert.ToBoolean(r.Cells[0].Value))
                    {
                        continue;
                    }

                    //チェック行
                    // クラスが間違えなかったら、次の処理を行う
                    if (!(r.Tag is RelationInfo_DTOCls))
                    {
                        continue;
                    }

                    RelationInfo_DTOCls dto = (RelationInfo_DTOCls)r.Tag;

                    // 選択外クリアボタンを押した時、取得したシステムIDリストにより、ソースする。
                    if (dto.MANIFEST_TYPE == "4")
                    {
                        if (this.Logic.dtoMani.DSP_KANRI_ID.Contains(dto.KANRI_ID))
                        {
                            cdgv_FirstMani.Rows.Remove(r);
                            cdgv_FirstMani.Rows.Insert(rowNo++, r);
                        }
                    }
                    else
                    {
                        if (this.Logic.dtoMani.DSP_DETAIL_SYSTEM_ID.Contains(SqlInt64.Parse(dto.FIRST_SYSTEM_ID)))
                        {
                            cdgv_FirstMani.Rows.Remove(r);
                            cdgv_FirstMani.Rows.Insert(rowNo++, r);
                        }
                    }
                }
            }
            // 20140609 kayo 不具合#4694 選択外クリアを押してから、再検索の表示不正 end

            //減容量の合算
            SumHaisyutuAmount("2");

            LogUtility.DebugMethodEnd(HimodukeFlg);
        }

        #endregion

        /// <summary>
        /// 検索ボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            // 日付チェック
            if (this.Logic.DateCheck())
            {
                return;
            }

            //検索条件DTOの作成
            SetFirstManiSearchCondition();

            //検索実行
            this.Logic.SearchLogic();
            if (this.isSearchErr) { return; }

            //画面に結果が反映する
            SetDataToDgv(false);

            if (this.Logic.SearchResult == null || this.Logic.SearchResult.Rows.Count <= 0)
            {
                this.Logic.msgLogic.MessageBoxShow("C001");
            }

        }

        // 20140519 kayo No.734 機能追加 start
        /// <summary>
        /// 選択外クリアボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void DoNondisplay(object sender, EventArgs e)
        {
            // 紐付チェックは入っている行のみを表示
            this.CheckOnlyDisplay();
        }

        /// <summary>
        /// 紐付チェックは入っている行のみを表示します
        /// </summary>
        public void CheckOnlyDisplay()
        {
            // 20140609 kayo 不具合#4694 選択外クリアを押してから、再検索の表示不正 start
            this.Logic.dtoMani.DSP_DETAIL_SYSTEM_ID = new List<SqlInt64>();
            this.Logic.dtoMani.DSP_KANRI_ID = new List<string>();
            // 20140609 kayo 不具合#4694 選択外クリアを押してから、再検索の表示不正 end

            //選択外のレコードを非表示にする
            foreach (DataGridViewRow r in cdgv_FirstMani.Rows)
            {
                if (!Convert.ToBoolean(r.Cells[0].Value))
                {
                    r.Visible = false;
                }
                // 20140609 kayo 不具合#4694 選択外クリアを押してから、再検索の表示不正 start
                else
                {
                    //チェック行
                    //紐付対象のシステムIDリストを取得
                    if (r.Tag is RelationInfo_DTOCls)
                    {
                        RelationInfo_DTOCls dto = (RelationInfo_DTOCls)r.Tag;

                        if (dto.MANIFEST_TYPE == "4")
                        {
                            //電子の場合
                            this.Logic.dtoMani.DSP_KANRI_ID.Add(dto.KANRI_ID);
                        }
                        else
                        {
                            //紙の場合
                            this.Logic.dtoMani.DSP_DETAIL_SYSTEM_ID.Add(SqlInt64.Parse(dto.FIRST_SYSTEM_ID));
                        }
                    }
                }
                // 20140609 kayo 不具合#4694 選択外クリアを押してから、再検索の表示不正 end
            }
        }

        // 20140519 kayo No.734 機能追加 end

        /// <summary>
        /// 検索DTOの設定処理
        /// </summary>
        private void SetFirstManiSearchCondition()
        {

            // 起動元二次マニ区分
            this.Logic.dtoMani.MANI_KBN = string.IsNullOrEmpty(this.relationParam.MANI_KBN) ? string.Empty : this.relationParam.MANI_KBN;

            //マニ種類
            if (cntxt_ManiType.Text.Trim() == "2")
            {
                this.Logic.dtoMani.MANI_TYPE = "3";
            }
            else if (cntxt_ManiType.Text.Trim() == "3")
            {
                this.Logic.dtoMani.MANI_TYPE = "2";
            }
            else
            {
                this.Logic.dtoMani.MANI_TYPE = cntxt_ManiType.Text.Trim();
            }

            //交付日付のタイプ
            this.Logic.dtoMani.DATETIME_TYPE = cntxt_DatetimeType.Text.Trim();

            //開始日付
            if (!string.IsNullOrWhiteSpace(cDtPicker_StartDay.Text.ToString()))
            {
                this.Logic.dtoMani.START_DATETIME = cDtPicker_StartDay.Text.Substring(0, 10).ToString();
            }
            else
            {
                this.Logic.dtoMani.START_DATETIME = "";
            }

            //終了日付
            if (!string.IsNullOrWhiteSpace(cDtPicker_EndDay.Text.ToString()))
            {
                this.Logic.dtoMani.END_DATETIME = cDtPicker_EndDay.Text.Substring(0, 10).ToString();
            }
            else
            {
                this.Logic.dtoMani.END_DATETIME = "";
            }

            if (cntxt_ManiType.Text.Trim() == "4")//電子の場合
            {
                //電子廃棄物種類CD
                this.Logic.dtoMani.HAIKI_SHURUI_CD = cantxt_ElecHaikibutuTypeCD.Text.Trim();
                //電子廃棄物名称CD
                this.Logic.dtoMani.HAIKI_NAME_CD = cantxt_ElecHaikibutuCD.Text.Trim();
            }
            else
            {
                //廃棄物種類CD
                this.Logic.dtoMani.HAIKI_SHURUI_CD = cantxt_HaikibutuTypeCD.Text.Trim();
                //廃棄物名称CD
                this.Logic.dtoMani.HAIKI_NAME_CD = cantxt_HaikibutuCD.Text.Trim();
            }

            // 20140519 kayo No.734 機能追加 start
            //最終処分場所抽出
            this.Logic.dtoMani.LAST_SBN_GENBA_TYPE = cntxt_LastShobunGenbaFlg.Text.Trim();
            this.Logic.dtoMani.NEXT_SYSTEM_ID = this.relationParam.SECOND_SYSTEM_ID;
            // 20140519 kayo No.734 機能追加 end

            //報告書分類CD;
            this.Logic.dtoMani.HOUKOKUSHO_BUNRUI_CD = cantxt_HoukokushoTypeCD.Text.Trim();

            //荷姿CD
            this.Logic.dtoMani.NISUGATA_CD = cantxt_NisugataCD.Text.Trim();

            //処分方法
            this.Logic.dtoMani.SBN_HOUHOU_CD = cantxt_ShobunHouhouCD.Text.Trim();

            //排出事業者
            this.Logic.dtoMani.HST_GYOUSHA_CD = cantxt_HaisyutugyoshaCD.Text.Trim();

            //排出事業場
            this.Logic.dtoMani.HST_GENBA_CD = cantxt_HaisyutugenbaCD.Text.Trim();

            //運搬受託者
            this.Logic.dtoMani.UPN_GYOUSHA_CD = cantxt_UnpangyoshaCD.Text.Trim();

            //処分受託者
            this.Logic.dtoMani.SBN_GYOUSHA_CD = cantxt_ShobungyoshaCD.Text.Trim();

            //処分事業場(最後運搬区間の運搬先事業場CD)
            this.Logic.dtoMani.UPN_SAKI_GENBA_CD = cantxt_ShobunGenbaCD.Text.Trim();

            // 20140519 kayo No.734 機能追加 start
            ////最終処分業者
            //this.Logic.dtoMani.LAST_SBN_GYOUSHA_CD = cantxt_LastShobunGyoShaCD.Text.Trim();

            ////最終処分の場所
            //this.Logic.dtoMani.LAST_SBN_GENBA_CD = cantxt_LastShobunGenbaCD.Text.Trim();
            // 20140519 kayo No.734 機能追加 end

            //既存のチェックを保存
            this.Logic.dtoMani.DETAIL_SYSTEM_ID = new List<SqlInt64>();
            this.Logic.dtoMani.relationInfoDtoList = new List<RelationInfo_DTOCls>();
            this.Logic.dtoMani.KANRI_ID = new List<string>();
            this.Logic.dtoMani.ELEC_SYSTEM_ID = new List<SqlInt64>();

            //画面の選択状況を保存
            foreach (DataGridViewRow r in cdgv_FirstMani.Rows)
            {
                if (Convert.ToBoolean(r.Cells[0].Value))
                {
                    //チェック行
                    //紐付対象のシステムIDリストを取得
                    if (r.Tag is RelationInfo_DTOCls)
                    {
                        RelationInfo_DTOCls dto = (RelationInfo_DTOCls)r.Tag;

                        this.Logic.dtoMani.relationInfoDtoList.Add(dto);
                        if (dto.MANIFEST_TYPE == "4")
                        {
                            //電子の場合
                            this.Logic.dtoMani.KANRI_ID.Add(dto.KANRI_ID);
                            this.Logic.dtoMani.ELEC_SYSTEM_ID.Add(SqlInt64.Parse(dto.FIRST_SYSTEM_ID));
                        }
                        else
                        {
                            //紙の場合
                            this.Logic.dtoMani.DETAIL_SYSTEM_ID.Add(SqlInt64.Parse(dto.FIRST_SYSTEM_ID));
                        }
                    }

                }
            }

            if (this.Logic.dtoMani.ELEC_SYSTEM_ID != null && this.Logic.dtoMani.ELEC_SYSTEM_ID.Count > 0)
                this.Logic.dtoMani.STRING_ELEC_SYSTEM_ID = string.Join("','", this.Logic.dtoMani.ELEC_SYSTEM_ID);
            if (this.Logic.dtoMani.DETAIL_SYSTEM_ID != null && this.Logic.dtoMani.DETAIL_SYSTEM_ID.Count > 0)
                this.Logic.dtoMani.STRING_DETAIL_SYSTEM_ID = string.Join("','", this.Logic.dtoMani.DETAIL_SYSTEM_ID);
        }

        /// <summary>
        /// 登録ボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RegistManifestRelationInfo(object sender, EventArgs e)
        {
            // 日付チェック
            if (this.Logic.DateCheck())
            {
                return;
            }

            this.Logic.RegistManiRelationInfo();
        }

        #endregion

        #region 画面イベント処理
        /// <summary>
        /// 契約類別エディタの入力禁止文字イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KeiyakuFlg_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        /// <summary>
        /// 契約類別変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_KeiyakuFlg_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 契約ありRadioボタンチェック状態変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRdbtn_KeiyakuFlg_1_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 契約無しRadioボタンチェック状態変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRdbtn_KeiyakuFlg_2_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 契約の有無全ての場合、Radioボタンチェック状態変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRdbtn_KeiyakuFlg_3_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// マニ種類エディターの入力禁止文字イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_ManiType_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        /// <summary>
        /// マニ種類エディタの変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_ManiType_TextChanged(object sender, EventArgs e)
        {
            //積替と建廃区分コードの交換
            if (cntxt_ManiType.Text == "2")
            {
                cntxt_HiddenManiType.Text = "3";
            }
            else if (cntxt_ManiType.Text == "3")
            {
                cntxt_HiddenManiType.Text = "2";
            }
            else
            {
                cntxt_HiddenManiType.Text = cntxt_ManiType.Text;
            }

            r_framework.Dto.JoinMethodDto dtoHaikibutuTypeMethod;
            dtoHaikibutuTypeMethod = cantxt_HaikibutuTypeCD.popupWindowSetting[0];
            r_framework.Dto.SearchConditionsDto dtoSearchCondition = dtoHaikibutuTypeMethod.SearchCondition[0];

            //廃棄物種類の検索条件(マニ区分追加)の仕様変更
            if (cntxt_ManiType.Text == "1")//廃棄物種類と名称CDが対応なControl状態(紙マニ)の設定
            {
                dtoSearchCondition.Value = "1";
            }
            else if (cntxt_ManiType.Text == "2")//廃棄物種類と名称CDが対応なControl状態(紙マニ)の設定
            {
                dtoSearchCondition.Value = cntxt_HiddenManiType.Text;
            }
            else if (cntxt_ManiType.Text == "3")//廃棄物種類と名称CDが対応なControl状態(紙マニ)の設定
            {
                dtoSearchCondition.Value = cntxt_HiddenManiType.Text;
            }
            else if (cntxt_ManiType.Text == "4")//電子の場合
            {

            }
            else if (cntxt_ManiType.Text == "5")//全て場合
            {

            }

            //廃棄物種類
            switch (this.cntxt_HiddenManiType.Text)
            {
                case "1"://産廃(直行)
                case "2"://産廃(積替)
                case "3"://建廃
                    this.cantxt_HaikibutuTypeCD.Visible = true;
                    this.cantxt_HaikibutuTypeCD.Enabled = true;

                    this.cantxt_ElecHaikibutuTypeCD.Visible = false;

                    this.ctxt_HaikibutuTypeName.Enabled = true;
                    break;

                case "4"://電子
                    this.cantxt_HaikibutuTypeCD.Visible = false;
                    this.cantxt_HaikibutuTypeCD.Enabled = false;

                    this.cantxt_ElecHaikibutuTypeCD.Visible = true;

                    this.ctxt_HaikibutuTypeName.Enabled = true;
                    break;

                case "5"://全て
                    this.cantxt_HaikibutuTypeCD.Visible = true;
                    this.cantxt_HaikibutuTypeCD.Enabled = false;

                    this.cantxt_ElecHaikibutuTypeCD.Visible = false;

                    this.ctxt_HaikibutuTypeName.Enabled = false;
                    break;

                default://その他
                    break;
            }
            //マニ区分変更場合、クリアする
            this.cantxt_HaikibutuTypeCD.Text = string.Empty;
            this.cantxt_ElecHaikibutuTypeCD.Text = string.Empty;
            this.ctxt_HaikibutuTypeName.Text = string.Empty;

            //廃棄物名称
            switch (this.cntxt_HiddenManiType.Text)
            {
                case "1"://産廃(直行)
                case "2"://産廃(積替)
                case "3"://建廃
                    this.cantxt_HaikibutuCD.Visible = true;
                    this.cantxt_HaikibutuCD.Enabled = true;

                    this.cantxt_ElecHaikibutuCD.Visible = false;

                    this.ctxt_HaikibutuName.Enabled = true;
                    break;

                case "4"://電子
                    this.cantxt_HaikibutuCD.Visible = false;
                    this.cantxt_HaikibutuCD.Enabled = false;

                    this.cantxt_ElecHaikibutuCD.Visible = true;

                    this.ctxt_HaikibutuName.Enabled = true;
                    break;

                case "5"://全て
                    this.cantxt_HaikibutuCD.Visible = true;
                    this.cantxt_HaikibutuCD.Enabled = false;

                    this.cantxt_ElecHaikibutuCD.Visible = false;

                    this.ctxt_HaikibutuName.Enabled = false;
                    break;

                default://その他
                    break;
            }
            //マニ区分変更場合、クリアする
            this.cantxt_HaikibutuCD.Text = string.Empty;
            this.cantxt_ElecHaikibutuCD.Text = string.Empty;
            this.ctxt_HaikibutuName.Text = string.Empty;
        }

        /// <summary>
        /// マニ種類:１．産廃(直行)Radioボタンチェック状態変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRobtn_ManiType_1_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// マニ種類:２．産廃(積替)Radioボタンチェック状態変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRobtn_ManiType_2_CheckedChanged(object sender, EventArgs e)
        {
            if (cRobtn_ManiType_2.Checked)
            {
                if (cntxt_ManiType.Text != "2")
                {
                    cntxt_HiddenManiType.Text = "3";
                }
            }
        }

        /// <summary>
        /// マニ種類:３．建廃Radioボタンチェック状態変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRobtn_ManiType_3_CheckedChanged(object sender, EventArgs e)
        {
            if (cRobtn_ManiType_3.Checked)
            {
                if (cntxt_ManiType.Text != "3")
                {
                    cntxt_HiddenManiType.Text = "2";
                }
            }
        }

        /// <summary>
        /// マニ種類:４．電子Radioボタンチェック状態変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRobtn_ManiType_4_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// マニ種類:５．全てRadioボタンチェック状態変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRobtn_ManiType_5_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 日付範囲エディター内容入力禁止文字イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_DatetimeType_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        /// <summary>
        /// 日付範囲エディター内容変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_DatetimeType_TextChanged(object sender, EventArgs e)
        {
            // 紐付チェックは入っている行のみを表示
            this.CheckOnlyDisplay();

            // 日付範囲によって並べ替え用の文字列を生成
            var order = string.Empty;
            switch (this.cntxt_DatetimeType.Text)
            {
                case ("1"): order = "KOUFU_DATE,"; break;
                case ("2"): order = "LAST_UPN_END_DATE,"; break;
                case ("3"): order = "SBN_END_DATE,"; break;
            }
            this.dateTimeTypeOrder = order + "MANIFEST_ID, HAIKI_SHURUI_CD, SYSTEM_ID, DETAIL_SYSTEM_ID";
        }


        /// <summary>
        /// ラジオボタンに応じてグリッドの列表示を制御
        /// </summary>
        private void ChangeYMD()
        {

            if (cRobtn_DatetimeType_3.Checked)
            {
                this.koufyYMD.Visible = false;
                this.upnYMD.Visible = false;
                this.sbnYMD.Visible = true;
            }
            else if (cRobtn_DatetimeType_2.Checked)
            {
                this.koufyYMD.Visible = false;
                this.upnYMD.Visible = true;
                this.sbnYMD.Visible = false;
            }
            else
            {
                this.koufyYMD.Visible = true;
                this.upnYMD.Visible = false;
                this.sbnYMD.Visible = false;
            }

        }

        /// <summary>
        /// 日付範囲:1.交付年月日チェック状態変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRobtn_DatetimeType_1_CheckedChanged(object sender, EventArgs e)
        {
            if (cRobtn_DatetimeType_1.Checked)
            {
                this.ChangeYMD();
            }
        }

        /// <summary>
        ///  日付範囲:2.運搬終了日チェック状態変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRobtn_DatetimeType_2_CheckedChanged(object sender, EventArgs e)
        {
            if (cRobtn_DatetimeType_2.Checked)
            {
                this.ChangeYMD();
            }
        }

        /// <summary>
        /// 日付範囲:3.処分終了日チェック状態変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRobtn_DatetimeType_3_CheckedChanged(object sender, EventArgs e)
        {
            if (cRobtn_DatetimeType_3.Checked)
            {
                this.ChangeYMD();
            }
        }

        /// <summary>
        /// 減容量の合計
        /// </summary>
        private void SumHaisyutuAmount(string kbn)
        {
            int count = Convert.ToInt16(cdgv_FirstMani.Rows.Count.ToString());
            decimal fGennyou_suu_1 = 0;//直行
            decimal fGennyou_suu_2 = 0;//積替
            decimal fGennyou_suu_3 = 0;//建廃
            decimal fGennyou_suu_4 = 0;//電子
            decimal fGennyou_suu_5 = 0;//全て
            for (int i = 0; i < count; i++)
            {
                if (!this.cdgv_FirstMani.Rows[i].Visible)
                {
                    continue;
                }
                string strAmt = "0";
                decimal tmp = 0;
                if (null != cdgv_FirstMani.Rows[i].Cells["gennyougousuuryou"].Value)
                {
                    strAmt = (cdgv_FirstMani.Rows[i].Cells["gennyougousuuryou"].Value.ToString());
                }
                decimal.TryParse(strAmt, out tmp);

                // 20140606 kayo 不具合#4693 チェックをつけているもののサマリーとする修正 start
                //if (null != cdgv_FirstMani.Rows[i].Cells["manisyurui"].Value)
                bool checkStatus = kbn == "1" ? (Boolean)(cdgv_FirstMani.Rows[i].Cells[0] as DataGridViewCheckBoxCell).EditedFormattedValue : (Boolean)cdgv_FirstMani.Rows[i].Cells[0].Value;
                if (null != cdgv_FirstMani.Rows[i].Cells["manisyurui"].Value && checkStatus)
                // 20140606 kayo 不具合#4693 チェックをつけているもののサマリーとする修正 end
                {
                    // 20140624 kayo 不具合#5027 マニフェスト紐付で紐づけたとき、産廃(直行)のみ減容後数量が計算されない start
                    //if (cdgv_FirstMani.Rows[i].Cells["manisyurui"].Value.ToString() == "直行")
                    RelationInfo_DTOCls row = (RelationInfo_DTOCls)cdgv_FirstMani.Rows[i].Tag;
                    if (row.MANIFEST_TYPE == "1")
                    // 20140624 kayo 不具合#5027 マニフェスト紐付で紐づけたとき、産廃(直行)のみ減容後数量が計算されない end
                    {
                        fGennyou_suu_1 += tmp;
                    }
                    // 20140624 kayo 不具合#5027 マニフェスト紐付で紐づけたとき、産廃(直行)のみ減容後数量が計算されない start
                    //else if (cdgv_FirstMani.Rows[i].Cells["manisyurui"].Value.ToString() == "積替")
                    else if (row.MANIFEST_TYPE == "3")
                    // 20140624 kayo 不具合#5027 マニフェスト紐付で紐づけたとき、産廃(直行)のみ減容後数量が計算されない end
                    {
                        fGennyou_suu_2 += tmp;
                    }
                    // 20140624 kayo 不具合#5027 マニフェスト紐付で紐づけたとき、産廃(直行)のみ減容後数量が計算されない start
                    //else if (cdgv_FirstMani.Rows[i].Cells["manisyurui"].Value.ToString() == "建廃")
                    else if (row.MANIFEST_TYPE == "2")
                    // 20140624 kayo 不具合#5027 マニフェスト紐付で紐づけたとき、産廃(直行)のみ減容後数量が計算されない end
                    {
                        fGennyou_suu_3 += tmp;
                    }
                    // 20140624 kayo 不具合#5027 マニフェスト紐付で紐づけたとき、産廃(直行)のみ減容後数量が計算されない start
                    //else if (cdgv_FirstMani.Rows[i].Cells["manisyurui"].Value.ToString() == "電子")
                    else if (row.MANIFEST_TYPE == "4")
                    // 20140624 kayo 不具合#5027 マニフェスト紐付で紐づけたとき、産廃(直行)のみ減容後数量が計算されない end
                    {
                        fGennyou_suu_4 += tmp;
                    }
                    fGennyou_suu_5 = fGennyou_suu_1 + fGennyou_suu_2 + fGennyou_suu_3 + fGennyou_suu_4;
                }
            }
            //合算後数量が画面に反映する
            int calcCD = 3;      // 端数CD：四捨五入
            cntxt_AmtChokko.Text = this.Logic.FractionCalc(fGennyou_suu_1, calcCD).ToString();
            cntxt_AmtDumikai.Text = this.Logic.FractionCalc(fGennyou_suu_2, calcCD).ToString();
            cntxt_AmtKenbai.Text = this.Logic.FractionCalc(fGennyou_suu_3, calcCD).ToString();
            cntxt_AmtDensi.Text = this.Logic.FractionCalc(fGennyou_suu_4, calcCD).ToString();
            cntxt_AmtTotal.Text = this.Logic.FractionCalc(fGennyou_suu_5, calcCD).ToString();
        }

        #endregion

        #region 現場の検索条件変更イベント処理

        /// <summary>
        /// 排出事業者コード変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutugyoshaCD_TextChanged(object sender, EventArgs e)
        {
            //if (cantxt_HaisyutugyoshaCD.Text != string.Empty)
            //{
            cantxt_HaisyutugenbaCD.Text = string.Empty;
            ctxt_HaisyutugenbaName.Text = string.Empty;
            //}
        }
        /// <summary>
        /// 処分受託者コード変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_ShobungyoshaCD_TextChanged(object sender, EventArgs e)
        {
            if (cantxt_ShobungyoshaCD.Text != string.Empty)
            {
                cantxt_ShobunGenbaCD.Text = string.Empty;
                ctxt_ShobunGenbaName.Text = string.Empty;
            }
        }
        // 20140519 kayo No.734 機能追加 start
        ///// <summary>
        ///// 最終処分事業者コード変更イベント処理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void cantxt_LastShobunGyoShaCD_TextChanged(object sender, EventArgs e)
        //{
        //    if (cantxt_LastShobunGyoShaCD.Text != string.Empty)
        //    {
        //        cantxt_LastShobunGenbaCD.Text = string.Empty;
        //        ctxt_LastShobunGenbaName.Text = string.Empty;
        //    }
        //}
        // 20140519 kayo No.734 機能追加 end

        #endregion

        #region 電子廃棄物種類と名称チェック処理
        /// <summary>
        ///  電子廃棄物名称チェックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_ElecHaikibutuCD_Leave(object sender, EventArgs e)
        {
            if (cantxt_ElecHaikibutuCD.Text.Trim() != string.Empty)
            {
                cantxt_ElecHaikibutuCD.Text = cantxt_ElecHaikibutuCD.Text.PadLeft(cantxt_ElecHaikibutuCD.MaxLength, '0');
                string SearchSQL = "HAIKI_NAME_CD='" + cantxt_ElecHaikibutuCD.Text.Trim() + "'";
                //行を検索
                DataRow[] drArr = this.Logic.DenshiHaikiNameCodeResult.Select(SearchSQL);//
                if (drArr.Length == 0)
                {
                    this.Logic.msgLogic.MessageBoxShow("E020", "電子廃棄物名称");
                    cantxt_ElecHaikibutuCD.Focus();
                    cantxt_ElecHaikibutuCD.SelectAll();
                    ctxt_HaikibutuName.Text = string.Empty;
                }
                else
                {
                    ctxt_HaikibutuName.Text = drArr[0]["HAIKI_NAME"].ToString();
                }
            }
            else
            {
                ctxt_HaikibutuName.Text = string.Empty;
            }
        }
        /// <summary>
        ///電子廃棄物種類チェックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_ElecHaikibutuTypeCD_Leave(object sender, EventArgs e)
        {
            if (cantxt_ElecHaikibutuTypeCD.Text.Trim() != string.Empty)
            {
                cantxt_ElecHaikibutuTypeCD.Text = cantxt_ElecHaikibutuTypeCD.Text.PadLeft(cantxt_ElecHaikibutuTypeCD.MaxLength, '0');

                string SearchSQL = "HAIKI_SHURUI_CD =" + cantxt_ElecHaikibutuTypeCD.Text.Trim();
                //行を検索
                DataRow[] drArr = this.Logic.DenshiHaikiShuruiCodeResult.Select(SearchSQL);//
                if (drArr.Length == 0)
                {
                    this.Logic.msgLogic.MessageBoxShow("E020", "電子廃棄物種類");
                    cantxt_ElecHaikibutuTypeCD.Focus();
                    cantxt_ElecHaikibutuTypeCD.SelectAll();
                    ctxt_HaikibutuTypeName.Text = string.Empty;
                }
                else
                {
                    ctxt_HaikibutuTypeName.Text = drArr[0]["HAIKI_SHURUI_NAME"].ToString();
                }
            }
            else
            {
                ctxt_HaikibutuTypeName.Text = string.Empty;
            }
        }
        #endregion

        // 20140606 kayo 不具合#4693 チェックをつけているもののサマリーとする修正 start
        /// <summary>
        /// グリッドCellContentClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cdgv_FirstMani_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //
        }

        #region 最終処分場チェック
        /// <summary>
        /// 最終処分場チェック(外部アクセスのためのメソッド)
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="catchErr"></param>
        /// <returns>true:紐付けOK, false:紐付けNG</returns>
        internal bool IsHimodukeOk(int rowIndex, out bool catchErr)
        {
            return this.Logic.IsHimodukeOk(rowIndex, out catchErr);
        }
        #endregion

        #region 最終処分終了報告状況チェック
        /// <summary>
        /// 最終処分終了報告状況チェック(外部アクセスのためのメソッド)
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns>True：最終処分終了報告無し、False：最終処分終了報告有り</returns>
        internal bool IsLastSbnEndFlgTrue(int rowIndex)
        {
            bool rtVal = true;
            if (this.Logic.RelationResult.Rows.Count > rowIndex)
            {
                string systemId = this.Logic.RelationResult.Rows[rowIndex]["SYSTEM_ID"].ToString();
                string haikiKbnCd = this.Logic.RelationResult.Rows[rowIndex]["HAIKI_KBN_CD"].ToString();
                if (this.Logic.ConstCls.MANIFEST_TYPE_DENSHI.Equals(haikiKbnCd))
                {
                    rtVal = this.Logic.CheckLastSbnEndFlg(systemId, false);
                }
            }
            return rtVal;
        }
        #endregion

        /// <summary>
        /// グリッドCellValueChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cdgv_FirstMani_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == this.cdgv_FirstMani.RowCount - 1)
            {
                SumHaisyutuAmount("2");
            }
        }
        // 20140606 kayo 不具合#4693 チェックをつけているもののサマリーとする修正 end

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // この画面では最大化/最小化ボタンを表示　また、画面サイズの変更も可能とする
            // （BasePopFormでプロパティが変更される為、ここで戻す）
            this.ParentForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.ParentForm.MinimizeBox = true;
            this.ParentForm.MaximizeBox = true;
        }

        /// <summary>
        /// チェックボックスのヘッダーセルクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cdgv_FirstMani_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn col = this.cdgv_FirstMani.Columns[e.ColumnIndex];
            if (col is DataGridViewCheckBoxColumn)
            {
                DatagridViewCheckBoxHeaderCell header = col.HeaderCell as DatagridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header.MouseClick(e);
                    this.cdgv_FirstMani.Refresh();
                    // 全チェックボタンクリック時にCellValueChangedが実行されないようになったため
                    // 減容後数量を再計算する。
                    SumHaisyutuAmount("2");
                }
            }
        }

        /// <summary>
        /// 排出事業者 PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_HaisyutuGyousyaCd_PopupBeforeExecuteMethod()
        {
            this.befHstGyousha = this.cantxt_HaisyutugyoshaCD.Text;
        }

        /// <summary>
        /// 排出事業者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod()
        {
            if (this.befHstGyousha != this.cantxt_HaisyutugyoshaCD.Text)
            {
                this.cantxt_HaisyutugenbaCD.Text = string.Empty;
                this.ctxt_HaisyutugenbaName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 処分受託者 PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyutakuNameCd_PopupBeforeExecuteMethod()
        {
            this.befSbnGyousha = this.cantxt_ShobungyoshaCD.Text;
        }

        /// <summary>
        /// 処分受託者 PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod()
        {
            if (this.befSbnGyousha != this.cantxt_ShobungyoshaCD.Text)
            {
                this.cantxt_ShobunGenbaCD.Text = string.Empty;
                this.ctxt_ShobunGenbaName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 明細データセルフォーマット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void cdgv_FirstMani_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string columnName = this.cdgv_FirstMani.Columns[e.ColumnIndex].Name;
                //換算後数量と減容後数量のフォーマット
                if (columnName == "suuryou" || columnName == "gennyougousuuryou")
                {
                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        decimal suuryou = Convert.ToDecimal(e.Value);
                        e.Value = suuryou.ToString(this.Logic.mSysInfo.MANIFEST_SUURYO_FORMAT.ToString());
                    }
                }
            }

        }

        /// <summary>
        /// チェックボックスセルをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cdgv_FirstMani_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                DataGridViewCell cell = this.cdgv_FirstMani.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell is DataGridViewCheckBoxCell)
                {
                    DataGridViewCheckBoxCell checkCell = cell as DataGridViewCheckBoxCell;
                    if (checkCell.ReadOnly)
                    {
                        return;
                    }

                    checkCell.Value = !Convert.ToBoolean(checkCell.Value == null ? 0 : checkCell.Value);
                    this.cdgv_FirstMani.RefreshEdit();
                    this.cdgv_FirstMani.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }

                bool catchErr = false;
                var checkBoxCell = this.cdgv_FirstMani.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                if (checkBoxCell != null
                    && (bool)checkBoxCell.Value
                    && !this.IsHimodukeOk(e.RowIndex, out catchErr))
                {
                    if (catchErr) { return; }
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    if (msgLogic.MessageBoxShow("C080") == DialogResult.Yes)
                    {
                        SumHaisyutuAmount("1");
                        checkBoxCell.Value = true;
                        checkBoxCell.EditingCellFormattedValue = true;
                    }
                    else
                    {
                        checkBoxCell.Value = false;
                        checkBoxCell.EditingCellFormattedValue = false;
                    }
                }
                else
                {
                    if (catchErr) { return; }
                    SumHaisyutuAmount("1");
                    // 最終処分終了報告状況のチェック
                    if (this.Logic.RelationResult.Rows.Count > e.RowIndex)
                    {
                        string systemId = this.Logic.RelationResult.Rows[e.RowIndex]["SYSTEM_ID"].ToString();
                        string haikiKbnCd = this.Logic.RelationResult.Rows[e.RowIndex]["HAIKI_KBN_CD"].ToString();
                        if (this.Logic.ConstCls.MANIFEST_TYPE_DENSHI.Equals(haikiKbnCd)
                            && !this.Logic.CheckLastSbnEndFlg(systemId, true))
                        {
                            checkBoxCell.Value = true;
                            checkBoxCell.EditingCellFormattedValue = true;
                        }
                    }
                }
            }

        }

        ///<summary>
        /// チェックボックスセルでスペースキー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_FirstMani_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyData & Keys.KeyCode) == Keys.Space)
            {
                DataGridViewCell cell = this.cdgv_FirstMani.CurrentCell;

                //明細行のチェックボックス
                if (cell is DataGridViewCheckBoxCell && cell.RowIndex >= 0 && cell.ColumnIndex == 0)
                {
                    //一度チェックを変更する
                    DataGridViewCheckBoxCell checkCell = cell as DataGridViewCheckBoxCell;
                    if (checkCell.ReadOnly)
                    {
                        return;
                    }
                    checkCell.Value = !Convert.ToBoolean(checkCell.Value == null ? 0 : checkCell.Value);
                    this.cdgv_FirstMani.RefreshEdit();
                    this.cdgv_FirstMani.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    //再計算を行う
                    bool catchErr = false;
                    var checkBoxCell = this.cdgv_FirstMani.Rows[cell.RowIndex].Cells[cell.ColumnIndex] as DataGridViewCheckBoxCell;
                    if (checkBoxCell != null
                        && (bool)checkBoxCell.Value
                        && !this.IsHimodukeOk(cell.RowIndex, out catchErr))
                    {
                        if (catchErr) { return; }
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        if (msgLogic.MessageBoxShow("C080") == DialogResult.Yes)
                        {
                            SumHaisyutuAmount("1");
                            checkBoxCell.Value = true;
                            checkBoxCell.EditingCellFormattedValue = true;
                        }
                        else
                        {
                            checkBoxCell.Value = false;
                            checkBoxCell.EditingCellFormattedValue = false;
                        }
                    }
                    else
                    {
                        if (catchErr) { return; }
                        SumHaisyutuAmount("1");
                        // 最終処分終了報告状況のチェック
                        if (this.Logic.RelationResult.Rows.Count > cell.RowIndex)
                        {
                            string systemId = this.Logic.RelationResult.Rows[cell.RowIndex]["SYSTEM_ID"].ToString();
                            string haikiKbnCd = this.Logic.RelationResult.Rows[cell.RowIndex]["HAIKI_KBN_CD"].ToString();
                            if (this.Logic.ConstCls.MANIFEST_TYPE_DENSHI.Equals(haikiKbnCd)
                                && !this.Logic.CheckLastSbnEndFlg(systemId, true))
                            {
                                checkBoxCell.Value = true;
                                checkBoxCell.EditingCellFormattedValue = true;
                            }
                        }
                    }

                    //一度チェックを戻す
                    DataGridViewCheckBoxCell checkCell2 = cell as DataGridViewCheckBoxCell;
                    if (checkCell2.ReadOnly)
                    {
                        return;
                    }
                    checkCell2.Value = !Convert.ToBoolean(checkCell2.Value == null ? 0 : checkCell2.Value);
                    this.cdgv_FirstMani.RefreshEdit();
                    this.cdgv_FirstMani.CommitEdit(DataGridViewDataErrorContexts.Commit);

                }
            }
        }


    }
}
