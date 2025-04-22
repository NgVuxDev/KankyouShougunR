using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Report;
using Shougun.Core.PaperManifest.Manifestsuiihyo.APP;
using Microsoft.VisualBasic;

namespace Shougun.Core.PaperManifest.Manifestsuiihyo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.Manifestsuiihyo.Setting.ButtonSetting.xml";

        /// <summary>
        /// DTO
        /// </summary>
        private SerchCheckManiDtoCls serchCMDto;

        /// <summary>
        /// DAO
        /// </summary>
        private IM_CORP_NAMEDaoCls corpNameDao;
        private GET_UNIT_DaoCls getUnitDao;
        private GET_DATA_DaoCls getDataDao;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm parentbaseform { get; set; }

        /// <summary>
        /// HeaderForm
        /// </summary>
        public UIHeader headerform { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public JoukenParam JoukenParam { get; set; }

        /// <summary>
        /// 帳票表示用会社名
        /// </summary>
        private string corpName = string.Empty;

        /// <summary>
        /// 帳票表示単位名
        /// </summary>
        private string unit = string.Empty;

        /// <summary>
        /// 帳票表示単位名
        /// </summary>
        private string[] tsukikei;

        private DateTime dateFrom;
        private DateTime dateTo;
        private long monthsBetween = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.serchCMDto = new SerchCheckManiDtoCls();
            this.corpNameDao = DaoInitUtility.GetComponent<IM_CORP_NAMEDaoCls>();
            this.getUnitDao = DaoInitUtility.GetComponent<GET_UNIT_DaoCls>();
            this.getDataDao = DaoInitUtility.GetComponent<GET_DATA_DaoCls>();

            // 会社名取得処理
            this.corpName = this.GetCorpName();

            // 単位取得
            this.unit = this.GetUnit();

            LogUtility.DebugMethodEnd(targetForm);
        }

        /// <summary>
        /// 会社名を取得取得
        /// </summary>
        /// <returns>会社名</returns>
        private string GetCorpName()
        {
            LogUtility.DebugMethodStart();

            string corpName = string.Empty;

            corpName = this.corpNameDao.GetCorpName();

            LogUtility.DebugMethodEnd(corpName);

            return corpName;
        }

        /// <summary>
        /// 単位を取得取得
        /// </summary>
        /// <returns>単位</returns>
        private string GetUnit()
        {
            LogUtility.DebugMethodStart();

            string Unit = string.Empty;

            Unit = this.getUnitDao.GetUnit();

            LogUtility.DebugMethodEnd(Unit);

            return Unit;
        }

        #region 共通継承
        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <summary>
        /// 画面初期処理
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();

            // フォームインスタンスを取得
            this.parentbaseform = (BusinessBaseForm)this.form.Parent;
            this.headerform = (UIHeader)this.parentbaseform.headerForm;

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 初期表示時のデータグリッドのカラム表示非表示の制御
        /// </summary>
        private void CustumDataGridViewInitDisp()
        {
            LogUtility.DebugMethodStart();

            #region - 月計以外のカラムの表示非表示制御 -
            switch (this.JoukenParam.syuturyokuNaiyoiu)
            {
                case "1":
                    #region - 出力内容：排出 -
                    this.form.customDataGridView1.Columns["MANIKUBUN"].Visible = true;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_MEISHOU"].Visible = true;                    
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_CD"].Visible = true;                    
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["TANI"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_KUBUN"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_MEISHOU"].Visible = false;
                    #endregion
                    break;

                case "2":
                    #region - 出力内容：運搬 -
                    this.form.customDataGridView1.Columns["MANIKUBUN"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_KUBUN"].Visible = true;                    
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["TANI"].Visible = true;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_MEISHOU"].Visible = false;
                    #endregion
                    break;

                case "3":
                    #region - 出力内容：処分 -
                    this.form.customDataGridView1.Columns["MANIKUBUN"].Visible = true;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_MEISHOU"].Visible = true; 
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["TANI"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_KUBUN"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_MEISHOU"].Visible = false;                                        
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_MEISHOU"].Visible = false;
                    #endregion
                    break;

                case "4":
                    #region - 出力内容：最終 -
                    this.form.customDataGridView1.Columns["MANIKUBUN"].Visible = true;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["TANI"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_KUBUN"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_MEISHOU"].Visible = false;
                    #endregion
                    break;

                case "5":
                    #region - 出力内容：廃棄種類 -
                    this.form.customDataGridView1.Columns["MANIKUBUN"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["TANI"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_KUBUN"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_MEISHOU"].Visible = false;
                    #endregion
                    break;

                default:
                    break;
            }
            #endregion

            #region - 月計カラムの表示非表示制御 -

            // From-Toの分だけ表示し、
            int indexOffset = 17;
            DateTime.TryParse(this.JoukenParam.nengappiFrom, out dateFrom);
            DateTime.TryParse(this.JoukenParam.nengappiTo, out dateTo);
            monthsBetween = DateAndTime.DateDiff(DateInterval.Month, dateFrom, dateTo, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);

            for (int i = 0; i < monthsBetween + 1; i++ )
            {
                this.form.customDataGridView1.Columns[i + indexOffset].Visible = true;
                DateTime dt = dateFrom.AddMonths(i);
                this.form.customDataGridView1.Columns[i + indexOffset].HeaderText = (dt.Month).ToString() + "月";
                this.form.customDataGridView1.Columns[i + indexOffset].Name = "TUKIKEI_" + (dt.Month).ToString();
                this.form.customDataGridView1.Columns[i + indexOffset].DataPropertyName = "TUKIKEI_" + (dt.Month).ToString();
            }
            #endregion

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// グリッドの初期設定を行う
        /// </summary>
        private void MakeCustumDataGridView()
        {
            LogUtility.DebugMethodStart();

            DataGridViewTextBoxColumn column;

            // 1.一次マニフェスト区分
            column = new DataGridViewTextBoxColumn();
            column.Name = "MANIKUBUN";
            column.Width = 180;
            column.HeaderText = "一次マニフェスト区分";
            column.DataPropertyName = "MANIKUBUN";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 2.廃棄物区分
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAIKIBUTU_KUBUN";
            column.Width = 110;
            column.HeaderText = "廃棄物区分";
            column.DataPropertyName = "HAIKIBUTU_KUBUN";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 3.排出事業者CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAISHUTU_JIGYOUSHA_CD";
            column.Width = 120;
            column.HeaderText = "排出事業者CD";
            column.DataPropertyName = "HAISHUTU_JIGYOUSHA_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 4.排出事業者名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAISHUTU_JIGYOUSHA_MEISHOU";
            column.Width = 160;
            column.HeaderText = "排出事業者名称";
            column.DataPropertyName = "HAISHUTU_JIGYOUSHA_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 5.排出事業場CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAISHUTU_JIGYOUJOU_CD";
            column.Width = 120;
            column.HeaderText = "排出事業場CD";
            column.DataPropertyName = "HAISHUTU_JIGYOUJOU_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 6.排出事業場名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAISHUTU_JIGYOUJOU_MEISHOU";
            column.Width = 160;
            column.HeaderText = "排出事業場名称";
            column.DataPropertyName = "HAISHUTU_JIGYOUJOU_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 7.処分受託者CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "SHOBUN_JUTAKUSHA_CD";
            column.Width = 120;
            column.HeaderText = "処分受託者CD";
            column.DataPropertyName = "SHOBUN_JUTAKUSHA_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 8.処分受託者名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "SHOBUN_JUTAKUSHA_MEISHOU";
            column.Width = 160;
            column.HeaderText = "処分受託者名称";
            column.DataPropertyName = "SHOBUN_JUTAKUSHA_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 9.処分事業場CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "SHOBUN_JIGYOUJOU_CD";
            column.Width = 120;
            column.HeaderText = "処分事業場CD";
            column.DataPropertyName = "SHOBUN_JIGYOUJOU_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 10.処分事業場名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "SHOBUN_JIGYOUJOU_MEISHOU";
            column.Width = 160;
            column.HeaderText = "処分事業場名称";
            column.DataPropertyName = "SHOBUN_JIGYOUJOU_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 11.運搬受託者CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "UNPAN_JUTAKUSHA_CD";
            column.Width = 120;
            column.HeaderText = "運搬受託者CD";
            column.DataPropertyName = "UNPAN_JUTAKUSHA_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 12.運搬受託者名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "UNPAN_JUTAKUSHA_MEISHOU";
            column.Width = 160;
            column.HeaderText = "運搬受託者名称";
            column.DataPropertyName = "UNPAN_JUTAKUSHA_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 13.最終処分場CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "SAISHUU_SHOBUNJOU_CD";
            column.Width = 120;
            column.HeaderText = "最終処分場CD";
            column.DataPropertyName = "SAISHUU_SHOBUNJOU_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 14.最終処分場名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "SAISHUU_SHOBUNJOU_MEISHOU";
            column.Width = 160;
            column.HeaderText = "最終処分場名称";
            column.DataPropertyName = "SAISHUU_SHOBUNJOU_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 15.廃棄物種類CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAIKIBUTU_SHURUI_CD";
            column.Width = 120;
            column.HeaderText = "廃棄物種類CD";
            column.DataPropertyName = "HAIKIBUTU_SHURUI_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 16.廃棄物種類名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAIKIBUTU_SHURUI_MEISHOU";
            column.Width = 160;
            column.HeaderText = "廃棄物種類名称";
            column.DataPropertyName = "HAIKIBUTU_SHURUI_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 17.単位
            column = new DataGridViewTextBoxColumn();
            column.Name = "TANI";
            column.Width = 60;
            column.HeaderText = "単位";
            column.DataPropertyName = "TANI";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 18.月計1
            column = new DataGridViewTextBoxColumn();
            column.Width = 90;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 19.月計2
            column = new DataGridViewTextBoxColumn();
            column.Width = 90;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 20.月計3
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.ReadOnly = true;
            column.Visible = false;
            column.Width = 90;
            this.form.customDataGridView1.Columns.Add(column);

            // 21.月計4
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 22.月計5
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 23.月計6
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 24.月計7
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 25.月計8
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 26.月計9
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 27.月計10
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 28.月計11
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 29.月計12
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);
            
            // 新規行追加不可
            this.form.customDataGridView1.AllowUserToAddRows = false;
            this.form.customDataGridView1.AutoGenerateColumns = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベント
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            // 印刷ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);
            // CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.ButtonFunc6_Clicked);
            // 検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.ButtonFunc8_Clicked);
            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

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

            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath));
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// 印刷ボタン押下時処理
        /// </summary>
        internal void Func5()
        {
            LogUtility.DebugMethodStart();

            ReportInfoR391 report_r391 = new ReportInfoR391();
            DataTable printData = new DataTable();

            if ("1".Equals(this.JoukenParam.syuturyokuNaiyoiu))
            {
                report_r391.OutputFormLayout = "LAYOUT1";
                // 排出事業者別
                printData = this.MekePrintDataHaishutu();
            }
            else if ("2".Equals(this.JoukenParam.syuturyokuNaiyoiu))
            {
                report_r391.OutputFormLayout = "LAYOUT3";
                // 運搬受託者別
                printData = this.MekePrintDataUnpan();
            }
            else if ("3".Equals(this.JoukenParam.syuturyokuNaiyoiu))
            {
                report_r391.OutputFormLayout = "LAYOUT1";
                // 処分受託者別
                printData = this.MekePrintDataShobun();
            }
            else if ("4".Equals(this.JoukenParam.syuturyokuNaiyoiu))
            {
                report_r391.OutputFormLayout = "LAYOUT2";
                // 最終処分場所別
                printData = this.MekePrintDataSaishuu();
            }
            else if ("5".Equals(this.JoukenParam.syuturyokuNaiyoiu))
            {
                report_r391.OutputFormLayout = "LAYOUT2";
                // 廃棄物種類別
                printData = this.MekePrintDataHaiki();
            }

            // 画面側から渡ってきた帳票用データテーブルを引数へ設定
            report_r391.R391_Report(printData);

            // ファイル名、レイアウトを変更する場合はここで設定する
            //report_r382.OutputFormFullPathName = "R382_R387-Form.xml";
            //report_r382.OutputFormLayout = "LAYOUT1";

            // 印刷ポツプアップ画面表示
            using (FormReportPrintPopup report = new FormReportPrintPopup(report_r391))
            {
                report.ShowDialog();
                report.Dispose();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 印刷用のデータを作成する（排出事業者別）
        /// </summary>
        /// <returns></returns>
        private DataTable MekePrintDataHaishutu()
        {
            LogUtility.DebugMethodStart();
            string mesiData = string.Empty;

            DataTable gridData = new DataTable();
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add();

            // ０－１
            DataRow dr;

            // レイアウトNo、会社名
            mesiData = "\"0-1\",\"1\",\"" + this.corpName + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // １－１
            // 一次二次区分、月カラム
            mesiData = "\"1-1\",\"" + this.GetManikbnNm() + "\",\"" + this.GetTukiTitle() + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            gridData = (DataTable)this.form.customDataGridView1.DataSource;

            // 排出事業者CD
            string haishutuJigyoushaCd = this.form.customDataGridView1.Rows[0].Cells["HAISHUTU_JIGYOUSHA_CD"].Value.ToString();

            string condtion = "HAISHUTU_JIGYOUSHA_CD = '" + haishutuJigyoushaCd + "'";
            DataRow[] dataRow = gridData.Select(condtion);

            // ２－１
            mesiData = "\"2-1\",";

            // 排出事業者CD
            mesiData += "\"" + this.GetDbValue(dataRow[0]["HAISHUTU_JIGYOUSHA_CD"]) + "\",";
            // 排出事業者名
            mesiData += "\"" + this.GetDbValue(dataRow[0]["HAISHUTU_JIGYOUSHA_MEISHOU"]) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // ２－２
            for (int i = 0; i < dataRow.Length; i++)
            {
                // 廃棄物種類-月別
                string haikibutuTukibetu = string.Empty;
                mesiData = "\"2-2\",";

                // 排出事業場CD
                mesiData += "\"" + this.GetDbValue(dataRow[i]["HAISHUTU_JIGYOUJOU_CD"]) + "\",";
                // 排出事業場名
                mesiData += "\"" + this.GetDbValue(dataRow[i]["HAISHUTU_JIGYOUJOU_MEISHOU"]) + "\",";
                // 廃棄物種類
                mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                // 単位
                mesiData += "\"" + this.GetDbValue(dataRow[i]["TANI"]) + "\",";

                // 廃棄物種類-月別
                for (int j = 8; j < gridData.Columns.Count; j++)
                {
                    haikibutuTukibetu += this.GetDbValue(dataRow[i][j]) + ",";
                }

                haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                
                // 廃棄物種類-月別
                mesiData += "\"" + haikibutuTukibetu + "\",";
                // 廃棄物種類-合計
                mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                dr = returnTable.NewRow();
                dr[0] = mesiData;
                returnTable.Rows.Add(dr);
            }

            // ２－３
            mesiData = "\"2-3\",";

            // 排出事業者計-月別
            string haishutuTukibetu = string.Empty;

            for (int i = 8; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < dataRow.Length; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(dataRow[j][i]));
                }
                haishutuTukibetu += dataRowVal.ToString() + ",";
            }

            haishutuTukibetu = this.Get12Tukibetu(haishutuTukibetu);
            
            // 排出事業者計-月別
            mesiData += "\"" + haishutuTukibetu + "\",";
            // 排出事業者計-合計
            mesiData += "\"" + this.GetGoukei(haishutuTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            for (int k = 0; k < this.form.customDataGridView1.Rows.Count; k++)
            {
                if (!haishutuJigyoushaCd.Equals(
                    this.form.customDataGridView1.Rows[k].Cells["HAISHUTU_JIGYOUSHA_CD"].Value.ToString()))
                {
                    // 排出事業者CD
                    haishutuJigyoushaCd = this.form.customDataGridView1.Rows[k].Cells["HAISHUTU_JIGYOUSHA_CD"].Value.ToString();

                    condtion = "HAISHUTU_JIGYOUSHA_CD = '" + haishutuJigyoushaCd + "'";
                    dataRow = gridData.Select(condtion);

                    // ２－１
                    mesiData = "\"2-1\",";

                    // 排出事業者CD
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["HAISHUTU_JIGYOUSHA_CD"]) + "\",";

                    // 排出事業者名
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["HAISHUTU_JIGYOUSHA_MEISHOU"]) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);

                    // ２－２
                    for (int i = 0; i < dataRow.Length; i++)
                    {
                        // 廃棄物種類-月別
                        string haikibutuTukibetu = string.Empty;
                        mesiData = "\"2-2\",";

                        // 排出事業場CD
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["HAISHUTU_JIGYOUJOU_CD"]) + "\",";
                        // 排出事業場名
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["HAISHUTU_JIGYOUJOU_MEISHOU"]) + "\",";
                        // 廃棄物種類
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                        // 単位
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["TANI"]) + "\",";

                        // 廃棄物種類-月別
                        for (int j = 8; j < gridData.Columns.Count; j++)
                        {
                            haikibutuTukibetu += this.GetDbValue(dataRow[i][j]) + ",";
                        }

                        haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                        
                        // 廃棄物種類-月別
                        mesiData += "\"" + haikibutuTukibetu + "\",";
                        // 廃棄物種類-合計
                        mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                        dr = returnTable.NewRow();
                        dr[0] = mesiData;
                        returnTable.Rows.Add(dr);
                    }

                    // ２－３
                    mesiData = "\"2-3\",";

                    // 排出事業者計-月別
                    haishutuTukibetu = string.Empty;

                    for (int i = 8; i < gridData.Columns.Count; i++)
                    {
                        decimal dataRowVal = 0;
                        for (int j = 0; j < dataRow.Length; j++)
                        {
                            dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(dataRow[j][i]));
                        }
                        haishutuTukibetu += dataRowVal.ToString() + ",";
                    }

                    haishutuTukibetu = this.Get12Tukibetu(haishutuTukibetu);

                    // 排出事業者計-月別
                    mesiData += "\"" + haishutuTukibetu + "\",";
                    // 排出事業者計-合計
                    mesiData += "\"" + this.GetGoukei(haishutuTukibetu) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);
                }
            }

            // ２－４
            mesiData = "\"2-4\",";

            // 総合計-月別
            string sougoukeiTukibetu = string.Empty;
            for (int i = 8; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < gridData.Rows.Count; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(gridData.Rows[j][i]));
                }
                sougoukeiTukibetu += dataRowVal.ToString() + ",";
            }
            sougoukeiTukibetu = this.Get12Tukibetu(sougoukeiTukibetu);

            // 総合計-月別
            mesiData += "\"" + sougoukeiTukibetu + "\",";
            // 総合計-合計
            mesiData += "\"" + this.GetGoukei(sougoukeiTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            LogUtility.DebugMethodEnd();
            return returnTable;
        }

        /// <summary>
        /// 印刷用のデータを作成する（運搬受託者別）
        /// </summary>
        /// <returns></returns>
        private DataTable MekePrintDataUnpan()
        {

            LogUtility.DebugMethodStart();

            string mesiData = string.Empty;
            // 合計-月別
            string goukeiTukibetu = string.Empty;

            DataTable gridData = new DataTable();
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add();

            // ０－１
            DataRow dr;
            // レイアウトNo、会社名
            mesiData = "\"0-1\",\"2\",\"" + this.corpName + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // １－１
            // 一次二次区分、月カラム
            mesiData = "\"1-1\",\"" + this.GetManikbnNm() + "\",\"" + this.GetTukiTitle() + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            gridData = (DataTable)this.form.customDataGridView1.DataSource;

            // 廃棄物区分
            string haikibutuKubun = this.form.customDataGridView1.Rows[0].Cells["HAIKIBUTU_KUBUN"].Value.ToString();
            // 運搬受託者CD
            string unpanJutakushaCd = this.form.customDataGridView1.Rows[0].Cells["UNPAN_JUTAKUSHA_CD"].Value.ToString();

            string condtion = "HAIKIBUTU_KUBUN = '" + haikibutuKubun + "' and UNPAN_JUTAKUSHA_CD = '" + unpanJutakushaCd + "'";
            DataRow[] dataRow = gridData.Select(condtion);

            // ２－１
            for (int j = 0; j < dataRow.Length; j++)
            {
                // 廃棄物種類-月別
                string haikibutuTukibetu = string.Empty;
                mesiData = "\"2-1\",";

                // 廃棄物区分ラベル
                mesiData += "\"" + this.GetDbValue(dataRow[j]["HAIKIBUTU_KUBUN"]) + "用" + "\",";

                // 運搬受託者CD
                mesiData += "\"" + this.GetDbValue(dataRow[j]["UNPAN_JUTAKUSHA_CD"]) + "\",";

                // 運搬受託者名
                mesiData += "\"" + this.GetDbValue(dataRow[j]["UNPAN_JUTAKUSHA_MEISHOU"]) + "\",";

                // 廃棄物種類
                mesiData += "\"" + this.GetDbValue(dataRow[j]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";

                // 単位
                mesiData += "\"" + this.GetDbValue(dataRow[j]["TANI"]) + "\",";

                // 廃棄物種類-月別
                for (int k = 7; k < gridData.Columns.Count; k++)
                {
                    haikibutuTukibetu += this.GetDbValue(dataRow[j][k]) + ",";
                }
                haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                mesiData += "\"" + haikibutuTukibetu + "\",";

                // 廃棄物種類-合計
                mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                dr = returnTable.NewRow();
                dr[0] = mesiData;
                returnTable.Rows.Add(dr);
            }

            // ２－２
            mesiData = "\"2-2\",";

            // 運搬受託者計-月別
            string unpanTukibetu = string.Empty;

            for (int j = 7; j < gridData.Columns.Count; j++)
            {
                decimal dataRowVal = 0;
                for (int k = 0; k < dataRow.Length; k++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(dataRow[k][j]));
                }
                unpanTukibetu += dataRowVal.ToString() + ",";
            }
            unpanTukibetu = this.Get12Tukibetu(unpanTukibetu);

            // 運搬受託者計ラベル
            mesiData += "\"" + "運搬受託者計" + "\",";

            // 運搬受託者計-月別
            mesiData += "\"" + unpanTukibetu + "\",";
            // 運搬受託者計-合計
            mesiData += "\"" + this.GetGoukei(unpanTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
            {
                // 廃棄物区分が変わる
                if (!haikibutuKubun.Equals(this.form.customDataGridView1.Rows[i].Cells["HAIKIBUTU_KUBUN"].Value.ToString()))
                {
                    // 廃棄物区分
                    haikibutuKubun = this.form.customDataGridView1.Rows[i - 1].Cells["HAIKIBUTU_KUBUN"].Value.ToString();

                    // ２－３
                    mesiData = "\"2-3\",";

                    condtion = "HAIKIBUTU_KUBUN = '" + haikibutuKubun + "'";
                    dataRow = gridData.Select(condtion);

                    // 合計-月別
                    goukeiTukibetu = string.Empty;
                    for (int j = 7; j < gridData.Columns.Count; j++)
                    {
                        decimal dataRowVal = 0;
                        for (int k = 0; k < dataRow.Length; k++)
                        {
                            dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(dataRow[k][j]));
                        }
                        goukeiTukibetu += dataRowVal.ToString() + ",";
                    }
                    goukeiTukibetu = this.Get12Tukibetu(goukeiTukibetu);

                    // 合計ラベル
                    if ("直行".Equals(haikibutuKubun))
                    {
                        mesiData += "\"" + "直行用合計" + "\",";
                    }
                    else if ("建廃".Equals(haikibutuKubun))
                    {
                        mesiData += "\"" + "建廃用合計" + "\",";
                    }
                    else if ("積替".Equals(haikibutuKubun))
                    {
                        mesiData += "\"" + "積替用合計" + "\",";
                    }
                    else if ("電子".Equals(haikibutuKubun))
                    {
                        mesiData += "\"" + "電子用合計" + "\",";
                    }

                    // 合計-月別
                    mesiData += "\"" + goukeiTukibetu + "\",";
                    // 合計-合計
                    mesiData += "\"" + this.GetGoukei(goukeiTukibetu) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);

                    // 廃棄物区分
                    haikibutuKubun = this.form.customDataGridView1.Rows[i].Cells["HAIKIBUTU_KUBUN"].Value.ToString();
                    // 運搬受託者CD
                    unpanJutakushaCd = this.form.customDataGridView1.Rows[i].Cells["UNPAN_JUTAKUSHA_CD"].Value.ToString();

                    condtion = "HAIKIBUTU_KUBUN = '" + haikibutuKubun
                           + "' and UNPAN_JUTAKUSHA_CD = '" + unpanJutakushaCd + "'";
                    dataRow = gridData.Select(condtion);

                    // ２－１
                    for (int j = 0; j < dataRow.Length; j++)
                    {
                        // 廃棄物種類-月別
                        string haikibutuTukibetu = string.Empty;
                        mesiData = "\"2-1\",";

                        // 廃棄物区分ラベル
                        mesiData += "\"" + this.GetDbValue(dataRow[j]["HAIKIBUTU_KUBUN"]) + "用" + "\",";

                        // 運搬受託者CD
                        mesiData += "\"" + this.GetDbValue(dataRow[j]["UNPAN_JUTAKUSHA_CD"]) + "\",";

                        // 運搬受託者名
                        mesiData += "\"" + this.GetDbValue(dataRow[j]["UNPAN_JUTAKUSHA_MEISHOU"]) + "\",";

                        // 廃棄物種類
                        mesiData += "\"" + this.GetDbValue(dataRow[j]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";

                        // 単位
                        mesiData += "\"" + this.GetDbValue(dataRow[j]["TANI"]) + "\",";

                        // 廃棄物種類-月別
                        for (int k = 7; k < gridData.Columns.Count; k++)
                        {
                            haikibutuTukibetu += this.GetDbValue(dataRow[j][k]) + ",";
                        }
                        haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                        mesiData += "\"" + haikibutuTukibetu + "\",";

                        // 廃棄物種類-合計
                        mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                        dr = returnTable.NewRow();
                        dr[0] = mesiData;
                        returnTable.Rows.Add(dr);
                    }

                    // ２－２
                    mesiData = "\"2-2\",";

                    // 運搬受託者計-月別
                    unpanTukibetu = string.Empty;

                    for (int j = 7; j < gridData.Columns.Count; j++)
                    {
                        decimal dataRowVal = 0;
                        for (int k = 0; k < dataRow.Length; k++)
                        {
                            dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(dataRow[k][j]));
                        }
                        unpanTukibetu += dataRowVal.ToString() + ",";
                    }
                    unpanTukibetu = this.Get12Tukibetu(unpanTukibetu);

                    // 運搬受託者計ラベル
                    mesiData += "\"" + "運搬受託者計" + "\",";

                    // 運搬受託者計-月別
                    mesiData += "\"" + unpanTukibetu + "\",";
                    // 運搬受託者計-合計
                    mesiData += "\"" + this.GetGoukei(unpanTukibetu) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);
                }
                else
                {
                    // 運搬受託者CDが変わる
                    if (!unpanJutakushaCd.Equals(
                        this.form.customDataGridView1.Rows[i].Cells["UNPAN_JUTAKUSHA_CD"].Value.ToString()))
                    {
                        // 運搬受託者CD
                        unpanJutakushaCd = this.form.customDataGridView1.Rows[i].Cells["UNPAN_JUTAKUSHA_CD"].Value.ToString();

                        condtion = "HAIKIBUTU_KUBUN = '" + haikibutuKubun
                               + "' and UNPAN_JUTAKUSHA_CD = '" + unpanJutakushaCd + "'";
                        dataRow = gridData.Select(condtion);

                        // ２－１
                        for (int j = 0; j < dataRow.Length; j++)
                        {
                            // 廃棄物種類-月別
                            string haikibutuTukibetu = string.Empty;
                            mesiData = "\"2-1\",";

                            // 廃棄物区分ラベル
                            mesiData += "\"" + this.GetDbValue(dataRow[j]["HAIKIBUTU_KUBUN"]) + "用" + "\",";

                            // 運搬受託者CD
                            mesiData += "\"" + this.GetDbValue(dataRow[j]["UNPAN_JUTAKUSHA_CD"]) + "\",";

                            // 運搬受託者名
                            mesiData += "\"" + this.GetDbValue(dataRow[j]["UNPAN_JUTAKUSHA_MEISHOU"]) + "\",";

                            // 廃棄物種類
                            mesiData += "\"" + this.GetDbValue(dataRow[j]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";

                            // 単位
                            mesiData += "\"" + this.GetDbValue(dataRow[j]["TANI"]) + "\",";

                            // 廃棄物種類-月別
                            for (int k = 7; k < gridData.Columns.Count; k++)
                            {
                                haikibutuTukibetu += this.GetDbValue(dataRow[j][k]) + ",";
                            }
                            haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                            mesiData += "\"" + haikibutuTukibetu + "\",";

                            // 廃棄物種類-合計
                            mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                            dr = returnTable.NewRow();
                            dr[0] = mesiData;
                            returnTable.Rows.Add(dr);
                        }

                        // ２－２
                        mesiData = "\"2-2\",";

                        // 運搬受託者計-月別
                        unpanTukibetu = string.Empty;

                        for (int j = 7; j < gridData.Columns.Count; j++)
                        {
                            decimal dataRowVal = 0;
                            for (int k = 0; k < dataRow.Length; k++)
                            {
                                dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(dataRow[k][j]));
                            }
                            unpanTukibetu += dataRowVal.ToString() + ",";
                        }
                        unpanTukibetu = this.Get12Tukibetu(unpanTukibetu);

                        // 運搬受託者計ラベル
                        mesiData += "\"" + "運搬受託者計" + "\",";

                        // 運搬受託者計-月別
                        mesiData += "\"" + unpanTukibetu + "\",";
                        // 運搬受託者計-合計
                        mesiData += "\"" + this.GetGoukei(unpanTukibetu) + "\"";

                        dr = returnTable.NewRow();
                        dr[0] = mesiData;
                        returnTable.Rows.Add(dr);
                    }
                }
            }

            // ２－３
            mesiData = "\"2-3\",";

            condtion = "HAIKIBUTU_KUBUN = '" + haikibutuKubun + "'";
            dataRow = gridData.Select(condtion);

            // 合計-月別
            goukeiTukibetu = string.Empty;
            for (int j = 7; j < gridData.Columns.Count; j++)
            {
                decimal dataRowVal = 0;
                for (int k = 0; k < dataRow.Length; k++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(dataRow[k][j]));
                }
                goukeiTukibetu += dataRowVal.ToString() + ",";
            }
            goukeiTukibetu = this.Get12Tukibetu(goukeiTukibetu);

            // 合計ラベル
            if ("直行".Equals(haikibutuKubun))
            {
                mesiData += "\"" + "直行用合計" + "\",";
            }
            else if ("建廃".Equals(haikibutuKubun))
            {
                mesiData += "\"" + "建廃用合計" + "\",";
            }
            else if ("積替".Equals(haikibutuKubun))
            {
                mesiData += "\"" + "積替用合計" + "\",";
            }
            else if ("電子".Equals(haikibutuKubun))
            {
                mesiData += "\"" + "電子用合計" + "\",";
            }

            // 合計-月別
            mesiData += "\"" + goukeiTukibetu + "\",";
            // 合計-合計
            mesiData += "\"" + this.GetGoukei(goukeiTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // ２－４
            mesiData = "\"2-4\",";

            // 総合計-月別
            string sougoukeiTukibetu = string.Empty;
            for (int i = 7; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < gridData.Rows.Count; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(gridData.Rows[j][i]));
                }
                sougoukeiTukibetu += dataRowVal.ToString() + ",";
            }
            sougoukeiTukibetu = this.Get12Tukibetu(sougoukeiTukibetu);

            // 総合計-月別
            mesiData += "\"" + sougoukeiTukibetu + "\",";
            // 総合計-合計
            mesiData += "\"" + this.GetGoukei(sougoukeiTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            LogUtility.DebugMethodEnd();

            return returnTable;
        }

        /// <summary>
        /// 印刷用のデータを作成する（処分受託者別）
        /// </summary>
        /// <returns></returns>
        private DataTable MekePrintDataShobun()
        {
            LogUtility.DebugMethodStart();
            string mesiData = string.Empty;

            DataTable gridData = new DataTable();
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add();

            // ０－１
            DataRow dr;
            dr = returnTable.NewRow();

            // レイアウトNo、会社名
            mesiData = "\"0-1\",\"3\",\"" + this.corpName + "\"";
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // １－１
            dr = returnTable.NewRow();
            // 一次二次区分、月カラム
            mesiData = "\"1-1\",\"" + this.GetManikbnNm() + "\",\"" + this.GetTukiTitle() + "\"";
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            gridData = (DataTable)this.form.customDataGridView1.DataSource;

            // 処分受託者CD
            string shobunJutakushaCd = this.form.customDataGridView1.Rows[0].Cells["SHOBUN_JUTAKUSHA_CD"].Value.ToString();

            string condtion = "SHOBUN_JUTAKUSHA_CD = '" + shobunJutakushaCd + "'";
            DataRow[] dataRow = gridData.Select(condtion);

            // ２－１
            mesiData = "\"2-1\",";

            // 処分受託者CD
            mesiData += "\"" + this.GetDbValue(dataRow[0]["SHOBUN_JUTAKUSHA_CD"]) + "\",";
            // 処分受託者名
            mesiData += "\"" + this.GetDbValue(dataRow[0]["SHOBUN_JUTAKUSHA_MEISHOU"]) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // ２－２
            for (int i = 0; i < dataRow.Length; i++)
            {
                // 廃棄物種類-月別
                string haikibutuTukibetu = string.Empty;
                mesiData = "\"2-2\",";

                // 処分事業場CD
                mesiData += "\"" + this.GetDbValue(dataRow[i]["SHOBUN_JIGYOUJOU_CD"]) + "\",";
                // 処分事業場名
                mesiData += "\"" + this.GetDbValue(dataRow[i]["SHOBUN_JIGYOUJOU_MEISHOU"]) + "\",";
                // 廃棄物種類
                mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                // 単位
                mesiData += "\"" + this.GetDbValue(dataRow[i]["TANI"]) + "\",";

                // 廃棄物種類-月別
                for (int j = 8; j < gridData.Columns.Count; j++)
                {
                    haikibutuTukibetu += this.GetDbValue(dataRow[i][j]) + ",";
                }

                haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                // 廃棄物種類-月別
                mesiData += "\"" + haikibutuTukibetu + "\",";
                // 廃棄物種類-合計
                mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                dr = returnTable.NewRow();
                dr[0] = mesiData;
                returnTable.Rows.Add(dr);
            }

            // ２－３
            mesiData = "\"2-3\",";

            // 処分受託者計-月別
            string shobunTukibetu = string.Empty;

            for (int i = 8; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < dataRow.Length; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(dataRow[j][i]));
                }
                shobunTukibetu += dataRowVal.ToString() + ",";
            }

            shobunTukibetu = this.Get12Tukibetu(shobunTukibetu);

            // 処分受託者計-月別
            mesiData += "\"" + shobunTukibetu + "\",";
            // 処分受託者計-合計
            mesiData += "\"" + this.GetGoukei(shobunTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            for (int k = 0; k < this.form.customDataGridView1.Rows.Count; k++)
            {
                if (!shobunJutakushaCd.Equals(
                    this.form.customDataGridView1.Rows[k].Cells["SHOBUN_JUTAKUSHA_CD"].Value.ToString()))
                {
                    // 処分受託者CD
                    shobunJutakushaCd = this.form.customDataGridView1.Rows[k].Cells["SHOBUN_JUTAKUSHA_CD"].Value.ToString();

                    condtion = "SHOBUN_JUTAKUSHA_CD = '" + shobunJutakushaCd + "'";
                    dataRow = gridData.Select(condtion);

                    // ２－１
                    mesiData = "\"2-1\",";

                    // 処分受託者CD
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["SHOBUN_JUTAKUSHA_CD"]) + "\",";
                    // 処分受託者名
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["SHOBUN_JUTAKUSHA_MEISHOU"]) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);

                    // ２－２
                    for (int i = 0; i < dataRow.Length; i++)
                    {
                        // 廃棄物種類-月別
                        string haikibutuTukibetu = string.Empty;
                        mesiData = "\"2-2\",";

                        // 処分事業場CD
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["SHOBUN_JIGYOUJOU_CD"]) + "\",";
                        // 処分事業場名
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["SHOBUN_JIGYOUJOU_MEISHOU"]) + "\",";
                        // 廃棄物種類
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                        // 単位
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["TANI"]) + "\",";

                        // 廃棄物種類-月別
                        for (int j = 8; j < gridData.Columns.Count; j++)
                        {
                            haikibutuTukibetu += this.GetDbValue(dataRow[i][j]) + ",";
                        }

                        haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                        // 廃棄物種類-月別
                        mesiData += "\"" + haikibutuTukibetu + "\",";
                        // 廃棄物種類-合計
                        mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                        dr = returnTable.NewRow();
                        dr[0] = mesiData;
                        returnTable.Rows.Add(dr);
                    }

                    // ２－３
                    mesiData = "\"2-3\",";

                    // 処分受託者計-月別
                    shobunTukibetu = string.Empty;

                    for (int i = 8; i < gridData.Columns.Count; i++)
                    {
                        decimal dataRowVal = 0;
                        for (int j = 0; j < dataRow.Length; j++)
                        {
                            dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(dataRow[j][i]));
                        }
                        shobunTukibetu += dataRowVal.ToString() + ",";
                    }

                    shobunTukibetu = this.Get12Tukibetu(shobunTukibetu);

                    // 処分受託者計-月別
                    mesiData += "\"" + shobunTukibetu + "\",";
                    // 処分受託者計-合計
                    mesiData += "\"" + this.GetGoukei(shobunTukibetu) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);
                }
            }

            // ２－４
            mesiData = "\"2-4\",";

            // 総合計-月別
            string sougoukeiTukibetu = string.Empty;
            for (int i = 8; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < gridData.Rows.Count; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(gridData.Rows[j][i]));
                }
                sougoukeiTukibetu += dataRowVal.ToString() + ",";
            }
            sougoukeiTukibetu = this.Get12Tukibetu(sougoukeiTukibetu);

            // 総合計-月別
            mesiData += "\"" + sougoukeiTukibetu + "\",";
            // 総合計-合計
            mesiData += "\"" + this.GetGoukei(sougoukeiTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            LogUtility.DebugMethodEnd();
            return returnTable;
        }

        /// <summary>
        /// 印刷用のデータを作成する（最終処分場所別）
        /// </summary>
        /// <returns></returns>
        private DataTable MekePrintDataSaishuu()
        {
            LogUtility.DebugMethodStart();
            string mesiData = string.Empty;

            DataTable gridData = new DataTable();
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add();

            // ０－１
            DataRow dr;
            dr = returnTable.NewRow();

            // レイアウトNo、会社名
            mesiData = "\"0-1\",\"4\",\"" + this.corpName + "\"";
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // １－１
            // 一次二次区分、月カラム
            mesiData = "\"1-1\",\"" + this.GetManikbnNm() + "\",\"" + this.GetTukiTitle() + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            gridData = (DataTable)this.form.customDataGridView1.DataSource;

            // 最終処分場所CD
            string saishuuShobunjouCd = this.form.customDataGridView1.Rows[0].Cells["SAISHUU_SHOBUNJOU_CD"].Value.ToString();

            string condtion = "SAISHUU_SHOBUNJOU_CD = '" + saishuuShobunjouCd + "'";
            DataRow[] dataRow = gridData.Select(condtion);

            // ２－１
            mesiData = "\"2-1\",";

            // 最終処分場所CD
            mesiData += "\"" + this.GetDbValue(dataRow[0]["SAISHUU_SHOBUNJOU_CD"]) + "\",";
            // 最終処分場所
            mesiData += "\"" + this.GetDbValue(dataRow[0]["SAISHUU_SHOBUNJOU_MEISHOU"]) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // ２－２
            for (int i = 0; i < dataRow.Length; i++)
            {
                // 廃棄物種類-月別
                string haikibutuTukibetu = string.Empty;
                mesiData = "\"2-2\",";

                // 廃棄物種類CD
                mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_CD"]) + "\",";
                // 廃棄物種類
                mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                // 単位
                mesiData += "\"" + this.GetDbValue(dataRow[i]["TANI"]) + "\",";

                // 廃棄物種類-月別
                for (int j = 6; j < gridData.Columns.Count; j++)
                {
                    haikibutuTukibetu += this.GetDbValue(dataRow[i][j]) + ",";
                }

                haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                // 廃棄物種類-月別
                mesiData += "\"" + haikibutuTukibetu + "\",";
                // 廃棄物種類-合計
                mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                dr = returnTable.NewRow();
                dr[0] = mesiData;
                returnTable.Rows.Add(dr);
            }

            // ２－３
            mesiData = "\"2-3\",";

            // 最終処分場所計-月別
            string saishuuTukibetu = string.Empty;

            for (int i = 6; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < dataRow.Length; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(dataRow[j][i]));
                }
                saishuuTukibetu += dataRowVal.ToString() + ",";
            }

            saishuuTukibetu = this.Get12Tukibetu(saishuuTukibetu);

            // 最終処分場所計-月別
            mesiData += "\"" + saishuuTukibetu + "\",";
            // 最終処分場所計-合計
            mesiData += "\"" + this.GetGoukei(saishuuTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            for (int k = 0; k < this.form.customDataGridView1.Rows.Count; k++)
            {
                if (!saishuuShobunjouCd.Equals(
                    this.form.customDataGridView1.Rows[k].Cells["SAISHUU_SHOBUNJOU_CD"].Value.ToString()))
                {
                    // 最終処分場所CD
                    saishuuShobunjouCd = this.form.customDataGridView1.Rows[k].Cells["SAISHUU_SHOBUNJOU_CD"].Value.ToString();

                    condtion = "SAISHUU_SHOBUNJOU_CD = '" + saishuuShobunjouCd + "'";
                    dataRow = gridData.Select(condtion);

                    // ２－１
                    mesiData = "\"2-1\",";

                    // 最終処分場所CD
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["SAISHUU_SHOBUNJOU_CD"]) + "\",";
                    // 最終処分場所
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["SAISHUU_SHOBUNJOU_MEISHOU"]) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);

                    // ２－２
                    for (int i = 0; i < dataRow.Length; i++)
                    {
                        // 廃棄物種類-月別
                        string haikibutuTukibetu = string.Empty;
                        mesiData = "\"2-2\",";

                        // 廃棄物種類CD
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_CD"]) + "\",";
                        // 廃棄物種類
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                        // 単位
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["TANI"]) + "\",";

                        // 廃棄物種類-月別
                        for (int j = 6; j < gridData.Columns.Count; j++)
                        {
                            haikibutuTukibetu += this.GetDbValue(dataRow[i][j]) + ",";
                        }

                        haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                        // 廃棄物種類-月別
                        mesiData += "\"" + haikibutuTukibetu + "\",";
                        // 廃棄物種類-合計
                        mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                        dr = returnTable.NewRow();
                        dr[0] = mesiData;
                        returnTable.Rows.Add(dr);
                    }

                    // ２－３
                    mesiData = "\"2-3\",";

                    // 最終処分場所計-月別
                    saishuuTukibetu = string.Empty;

                    for (int i = 6; i < gridData.Columns.Count; i++)
                    {
                        decimal dataRowVal = 0;
                        for (int j = 0; j < dataRow.Length; j++)
                        {
                            dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(dataRow[j][i]));
                        }
                        saishuuTukibetu += dataRowVal.ToString() + ",";
                    }

                    saishuuTukibetu = this.Get12Tukibetu(saishuuTukibetu);

                    // 最終処分場所計-月別
                    mesiData += "\"" + saishuuTukibetu + "\",";
                    // 最終処分場所計-合計
                    mesiData += "\"" + this.GetGoukei(saishuuTukibetu) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);
                }
            }

            // ２－４
            mesiData = "\"2-4\",";

            // 総合計-月別
            string sougoukeiTukibetu = string.Empty;
            for (int i = 6; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < gridData.Rows.Count; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(gridData.Rows[j][i]));
                }
                sougoukeiTukibetu += dataRowVal.ToString() + ",";
            }
            sougoukeiTukibetu = this.Get12Tukibetu(sougoukeiTukibetu);

            // 総合計-月別
            mesiData += "\"" + sougoukeiTukibetu + "\",";
            // 総合計-合計
            mesiData += "\"" + this.GetGoukei(sougoukeiTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            LogUtility.DebugMethodEnd();
            return returnTable;
        }

        /// <summary>
        /// 印刷用のデータを作成する（廃棄物種類別）
        /// </summary>
        /// <returns></returns>
        private DataTable MekePrintDataHaiki()
        {
            LogUtility.DebugMethodStart();
            string mesiData = string.Empty;

            DataTable gridData = new DataTable();
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add();

            // ０－１
            DataRow dr;

            // レイアウトNo、会社名
            mesiData = "\"0-1\",\"5\",\"" + this.corpName + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // １－１
            // 一次二次区分、月カラム
            mesiData = "\"1-1\",\"" + this.GetManikbnNm() + "\",\"" + this.GetTukiTitle() + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            gridData = (DataTable)this.form.customDataGridView1.DataSource;

            // ２－１
            for (int i = 0; i < gridData.Rows.Count; i++)
            {
                // 廃棄物種類-月別
                string haikibutuTukibetu = string.Empty;
                mesiData = "\"2-1\",";

                // 廃棄物種類CD
                mesiData += "\"" + this.GetDbValue(gridData.Rows[i]["HAIKIBUTU_SHURUI_CD"]) + "\",";
                // 廃棄物種類
                mesiData += "\"" + this.GetDbValue(gridData.Rows[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                // 単位
                mesiData += "\"" + this.GetDbValue(gridData.Rows[i]["TANI"]) + "\",";

                // 廃棄物種類-月別
                for (int j = 4; j < gridData.Columns.Count; j++)
                {
                    haikibutuTukibetu += this.GetDbValue(gridData.Rows[i][j]) + ",";
                }

                haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                // 廃棄物種類-月別
                mesiData += "\"" + haikibutuTukibetu + "\",";
                // 廃棄物種類-合計
                mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                dr = returnTable.NewRow();
                dr[0] = mesiData;
                returnTable.Rows.Add(dr);
            }

            // ２－２
            mesiData = "\"2-2\",";

            // 総合計-月別
            string sougoukeiTukibetu = string.Empty;
            for (int i = 4; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < gridData.Rows.Count; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDouble(gridData.Rows[j][i]));
                }
                sougoukeiTukibetu += dataRowVal.ToString() + ",";
            }
            sougoukeiTukibetu = this.Get12Tukibetu(sougoukeiTukibetu);

            // 総合計-月別
            mesiData += "\"" + sougoukeiTukibetu + "\",";
            // 総合計-合計
            mesiData += "\"" + this.GetGoukei(sougoukeiTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            LogUtility.DebugMethodEnd();
            return returnTable;
        }

        /// <summary>
        /// 合計計算
        /// </summary>
        private string GetGoukei(string tukikei)
        {
            decimal goukei = 0;

            // 廃棄物種類-合計
            string[] arrayGoukei = tukikei.Split(',');
            for (int i = 0; i < arrayGoukei.Length; i++)
            {
                if (!string.Empty.Equals(arrayGoukei[i]))
                {
                    goukei += Convert.ToDecimal(arrayGoukei[i]);
                }
            }

            return goukei.ToString();
        }

        /// <summary>
        /// 一次二次区分を取得
        /// </summary>
        private string GetManikbnNm()
        {
            string maniKbnNm = string.Empty;
            // 一時二次区分
            if ("1".Equals(this.JoukenParam.ichijinijiKbn))
            {
                maniKbnNm = "一次マニフェスト";
            }
            else
            {
                maniKbnNm = "二次マニフェスト";
            }
            return maniKbnNm;
        }

        /// <summary>
        /// 月カラムタイトルを取得
        /// </summary>
        private string GetTukiTitle()
        {
            string tukiTitle = string.Empty;
            // タイトル月カラム
            for (int i = 17; i < this.form.customDataGridView1.Columns.Count; i++)
            {
                if (this.form.customDataGridView1.Columns[i].Visible == true)
                {
                    tukiTitle += this.form.customDataGridView1.Columns[i].HeaderText + ",";
                }
                else
                {
                    tukiTitle += ",";
                }
            }
            tukiTitle += ",";

            return tukiTitle;
        }

        /// <summary>
        /// １２月変換
        /// </summary>
        private string Get12Tukibetu(string tukibetu)
        {
            tukibetu = tukibetu.Substring(0, tukibetu.Length - 1);
            int len = tukibetu.Split(',').Length;

            // １２月未満の場合
            if (len < 12)
            {
                for (int i = 0; i < 12 - len; i++)
                {
                    tukibetu += "," + string.Empty;
                }
            }

            return tukibetu;
        }

        /// <summary>
        /// DBデータを取得
        /// </summary>
        private string GetDbValue(object obj)
        {
            if (obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return string.Empty;
            }

            return obj.ToString();
        }

        /// <summary>
        /// DBデータを取得
        /// </summary>
        private decimal GetDbValueDouble(object obj)
        {
            if (obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return 0.0m;
            }

            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 条件指定ポップアップ
        /// </summary>
        /// <param name="param">param</param>
        public void ShowPopUp(JoukenParam param)
        {
            LogUtility.DebugMethodStart(param);

            // 条件がnullならデフォルト値を設定する
            if (param == null || param.ichijinijiKbn == null)
            {
                param = this.CreateParams();
            }

            // 売上範囲条件指定画面表示
            JoukenPopupForm popUpForm = new JoukenPopupForm(param);
            popUpForm.ShowDialog();

            Cursor.Current = Cursors.WaitCursor;

            // 実行結果
            switch (popUpForm.DialogResult)
            {
                case DialogResult.OK:

                    // 子画面で入力された条件をセット
                    this.JoukenParam = popUpForm.joken;

                    // グリッドの設定
                    this.form.customDataGridView1.DataSource = null;
                    this.form.customDataGridView1.Columns.Clear();
                    this.MakeCustumDataGridView();
                    this.CustumDataGridViewInitDisp();

                    // データ取得
                    DataTable dt = new DataTable();
                    dt = this.MakeGridData();

                    // グリッドのデータソースを指定
                    this.form.customDataGridView1.DataSource = dt;
                    // 画面再描画
                    this.form.customDataGridView1.Refresh();

                    break;

                case DialogResult.Cancel:
                    // 何もしない
                    break;

                default:
                    break;
            }

            Cursor.Current = Cursors.Default;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面表示データの元となる情報をDBより取得
        /// </summary>
        /// <returns> 画面表示データの元となる情報 </returns>
        private DataTable MakeGridData()
        {
            LogUtility.DebugMethodStart();

            DataTable returnDt = new DataTable();

            // 条件を設定する
            this.serchCMDto.FIRST_MANIFEST_KBN = JoukenParam.ichijinijiKbn;                   // 一時二次区分
            this.serchCMDto.DATE_START = JoukenParam.nengappiFrom;                            // 年月日開始
            this.serchCMDto.DATE_END = JoukenParam.nengappiTo;                                // 年月日終了
            this.serchCMDto.KYOTEN_CD = JoukenParam.kyoten;                                   // 拠点CD
            this.serchCMDto.HST_GYOUSHA_CD_START = JoukenParam.haiJigyouShaFrom;              // 排出事業者CD開始
            this.serchCMDto.HST_GYOUSHA_CD_END = JoukenParam.haiJigyouShaTo;                  // 排出事業者CD終了
            this.serchCMDto.HST_GENBA_CD_START = JoukenParam.haiJigyouBaFrom;                 // 排出事業場CD開始
            this.serchCMDto.HST_GENBA_CD_END = JoukenParam.haiJigyouBaTo;                     // 排出事業場CD終了
            this.serchCMDto.HST_UPN_GYOUSHA_CD_START = JoukenParam.unpanJutakuShaFrom;        // 運搬受託者CD開始
            this.serchCMDto.HST_UPN_GYOUSHA_CD_END = JoukenParam.unpanJutakuShaTo;            // 運搬受託者CD終了
            this.serchCMDto.HST_UPN_SAKI_GYOUSHA_CD_START = JoukenParam.shobunJutakuShaFrom;  // 処分受託者CD開始
            this.serchCMDto.HST_UPN_SAKI_GYOUSHA_CD_END = JoukenParam.shobunJutakuShaTo;      // 処分受託者CD終了
            this.serchCMDto.HST_LAST_SBN_GENBA_CD_START = JoukenParam.saisyuuShobunBashoFrom; // 最終処分場所CD開始
            this.serchCMDto.HST_LAST_SBN_GENBA_CD_END = JoukenParam.saisyuuShobunBashoTo;     // 最終処分場所CD終了
            this.serchCMDto.HST_HAIKI_SHURUI_CD1 = JoukenParam.chokkouHaikibutuSyurui;        // 産廃（直行）廃棄物種類CD
            this.serchCMDto.HST_HAIKI_SHURUI_CD2 = JoukenParam.tsumikaeHaikibutuSyurui;       // 産廃（積替）廃棄物種類CD
            this.serchCMDto.HST_HAIKI_SHURUI_CD3 = JoukenParam.kenpaiHaikibutuSyurui;         // 建廃廃棄物種類CD
            this.serchCMDto.HAIKIBUTU_DENSHI = JoukenParam.denshiHaikibutuSyurui;             // 電子廃棄物種類CD
            this.serchCMDto.SHUTURYOKU_NAIYOU = JoukenParam.syuturyokuNaiyoiu;                // 出力内容
            this.serchCMDto.SHUTURYOKU_KUBUN = JoukenParam.syuturyokuKubun;                   // 出力区分

            // 取得データ格納用のデータテーブルは出力内容によって切り替える
            switch (JoukenParam.syuturyokuNaiyoiu)
            { 
                case ("1"):
                    // 排出
                    returnDt = this.GetHaishutuData();
                    break;

                case ("2"):
                    // 運搬
                    returnDt = this.GetUnpanData();
                    break;

                case ("3"):
                    // 処分
                    returnDt = this.GetShobunData();
                    break;

                case ("4"):
                    // 最終
                    returnDt = this.GetSaishuuData();
                    break;

                case ("5"):
                    // 廃棄
                    returnDt = this.GetHaikiData();
                    break;
                
                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
            
            return returnDt;
        }

        /// <summary>
        /// 排出データ取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetHaishutuData()
        {
            LogUtility.DebugMethodStart();

            DataTable returnData = new DataTable();
            try
            {
                DataTable temp = new DataTable();

                temp.Columns.Add("HST_GYOUSHA_CD");
                temp.Columns.Add("GYOUSHA_NAME_RYAKU");
                temp.Columns.Add("HST_GENBA_CD");
                temp.Columns.Add("GENBA_NAME_RYAKU");
                temp.Columns.Add("HAIKI_SHURUI_CD");
                temp.Columns.Add("HAIKI_SHURUI_NAME");
                temp.Columns.Add("KOUFU_YM");
                temp.Columns.Add("KANSAN_SUU");

                DataTable getDataTable;

                // 合算、紙
                if (!JoukenParam.syuturyokuKubun.Equals("3"))
                {
                    // データの取得
                    getDataTable = new DataTable();
                    getDataTable = getDataDao.GetKamiHaishutuData(serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                // 合算、電子
                if (!JoukenParam.syuturyokuKubun.Equals("2"))
                {
                    // データの取得
                    getDataTable = getDataDao.GetDenHaishutuData(serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                // ◆取得したデータを加工してreturnDataにセットする
                #region - カラムを設定 -
                returnData.Columns.Add("MANIKUBUN");
                returnData.Columns.Add("HAISHUTU_JIGYOUSHA_CD");
                returnData.Columns.Add("HAISHUTU_JIGYOUSHA_MEISHOU");
                returnData.Columns.Add("HAISHUTU_JIGYOUJOU_CD");
                returnData.Columns.Add("HAISHUTU_JIGYOUJOU_MEISHOU");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_CD");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_MEISHOU");
                returnData.Columns.Add("TANI");

                for (int i = 0; i < monthsBetween + 1; i++)
                {
                    string cName = "TUKIKEI_" + (dateFrom.AddMonths(i).Month).ToString();
                    returnData.Columns.Add(cName);
                }

                #endregion

                #region - 取得したデータを加工 -

                string gyoushaCD = string.Empty;
                string gyoushaName = string.Empty;
                string genbaCD = string.Empty;
                string genbaName = string.Empty;
                string haikiCD = string.Empty;
                string haikiName = string.Empty;
                string maniKubun = string.Empty;

                if (this.JoukenParam.ichijinijiKbn.Equals("1"))
                {
                    maniKubun = "一次マニフェスト";
                }
                else
                {
                    maniKubun = "二次マニフェスト";
                }

                DataRow retRow = returnData.NewRow();
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    gyoushaCD = temp.Rows[i]["HST_GYOUSHA_CD"].ToString();
                    gyoushaName = temp.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();
                    genbaCD = temp.Rows[i]["HST_GENBA_CD"].ToString();
                    genbaName = temp.Rows[i]["GENBA_NAME_RYAKU"].ToString();
                    haikiCD = temp.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    haikiName = temp.Rows[i]["HAIKI_SHURUI_NAME"].ToString();

                    DateTime targetDate;
                    DateTime.TryParse(temp.Rows[i]["KOUFU_YM"].ToString() + "/01", out targetDate);

                    string cName = "TUKIKEI_" + targetDate.Month.ToString();

                    retRow[cName] = this.GetDbValueDouble(temp.Rows[i]["KANSAN_SUU"]);

                    if ((i >= temp.Rows.Count - 1) ||
                        (
                            (!gyoushaCD.Equals(temp.Rows[i + 1]["HST_GYOUSHA_CD"].ToString())) ||
                            (!gyoushaName.Equals(temp.Rows[i + 1]["GYOUSHA_NAME_RYAKU"].ToString())) ||
                            (!genbaCD.Equals(temp.Rows[i + 1]["HST_GENBA_CD"].ToString())) ||
                            (!genbaName.Equals(temp.Rows[i + 1]["GENBA_NAME_RYAKU"].ToString())) ||
                            (!haikiCD.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_CD"].ToString())) ||
                            (!haikiName.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_NAME"].ToString()))
                        )
                       )
                    {
                        retRow["MANIKUBUN"] = maniKubun;
                        retRow["HAISHUTU_JIGYOUSHA_CD"] = gyoushaCD;
                        retRow["HAISHUTU_JIGYOUSHA_MEISHOU"] = gyoushaName;
                        retRow["HAISHUTU_JIGYOUJOU_CD"] = genbaCD;
                        retRow["HAISHUTU_JIGYOUJOU_MEISHOU"] = genbaName;
                        retRow["HAIKIBUTU_SHURUI_CD"] = haikiCD;
                        retRow["HAIKIBUTU_SHURUI_MEISHOU"] = haikiName;
                        retRow["TANI"] = this.unit;

                        returnData.Rows.Add(retRow);
                        retRow = returnData.NewRow();
                    }
                }
                #endregion
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

            return returnData;
        }

        /// <summary>
        /// 運搬データ取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetUnpanData()
        {
            LogUtility.DebugMethodStart();

            DataTable returnData = new DataTable();

            try
            {
                DataTable temp = new DataTable();

                temp.Columns.Add("HAIKI_KBN_CD");
                temp.Columns.Add("UPN_GYOUSHA_CD");
                temp.Columns.Add("GYOUSHA_NAME_RYAKU");
                temp.Columns.Add("HAIKI_SHURUI_CD");
                temp.Columns.Add("HAIKI_SHURUI_NAME");
                temp.Columns.Add("KOUFU_YM");
                temp.Columns.Add("KANSAN_SUU");

                DataTable getDataTable;

                if (!JoukenParam.syuturyokuKubun.Equals("3"))
                {
                    // データの取得
                    getDataTable = new DataTable();
                    getDataTable = getDataDao.GetKamiUnpanData(serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                if (!JoukenParam.syuturyokuKubun.Equals("2"))
                {
                    // データの取得
                    getDataTable = getDataDao.GetDenUnpanData(serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                // ◆取得したデータを加工してreturnDataにセットする
                #region - カラムを設定 -
                returnData.Columns.Add("MANIKUBUN");
                returnData.Columns.Add("HAIKIBUTU_KUBUN");
                returnData.Columns.Add("UNPAN_JUTAKUSHA_CD");
                returnData.Columns.Add("UNPAN_JUTAKUSHA_MEISHOU");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_CD");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_MEISHOU");
                returnData.Columns.Add("TANI");

                for (int i = 0; i < monthsBetween + 1; i++)
                {
                    string cName = "TUKIKEI_" + (dateFrom.AddMonths(i).Month).ToString();
                    returnData.Columns.Add(cName, typeof(Double));
                }

                #endregion

                #region - 取得したデータを加工 -

                string haikiKbn = string.Empty;
                string UnpanCD = string.Empty;
                string UnpanName = string.Empty;
                string haikiCD = string.Empty;
                string haikiName = string.Empty;
                string maniKubun = string.Empty;

                if (this.JoukenParam.ichijinijiKbn.Equals("1"))
                {
                    maniKubun = "一次マニフェスト";
                }
                else
                {
                    maniKubun = "二次マニフェスト";
                }

                DataRow retRow = returnData.NewRow();
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    haikiCD = temp.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    haikiName = temp.Rows[i]["HAIKI_SHURUI_NAME"].ToString();
                    haikiKbn = temp.Rows[i]["HAIKI_KBN_CD"].ToString();
                    UnpanCD = temp.Rows[i]["UPN_GYOUSHA_CD"].ToString();
                    UnpanName = temp.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();

                    DateTime targetDate;
                    DateTime.TryParse(temp.Rows[i]["KOUFU_YM"].ToString() + "/01", out targetDate);

                    string cName = "TUKIKEI_" + targetDate.Month.ToString();

                    retRow[cName] = this.GetDbValueDouble(temp.Rows[i]["KANSAN_SUU"]);

                    if ((i >= temp.Rows.Count - 1) ||
                        (
                            (!haikiCD.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_CD"].ToString())) ||
                            (!haikiName.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_NAME"].ToString())) ||
                            (!haikiKbn.Equals(temp.Rows[i + 1]["HAIKI_KBN_CD"].ToString())) ||
                            (!UnpanCD.Equals(temp.Rows[i + 1]["UPN_GYOUSHA_CD"].ToString())) ||
                            (!UnpanName.Equals(temp.Rows[i + 1]["GYOUSHA_NAME_RYAKU"].ToString()))
                        )
                       )
                    {
                        retRow["MANIKUBUN"] = maniKubun;
                        retRow["HAIKIBUTU_SHURUI_CD"] = haikiCD;
                        retRow["HAIKIBUTU_SHURUI_MEISHOU"] = haikiName;
                        retRow["HAIKIBUTU_KUBUN"] = haikiKbn;
                        retRow["UNPAN_JUTAKUSHA_CD"] = UnpanCD;
                        retRow["UNPAN_JUTAKUSHA_MEISHOU"] = UnpanName;
                        retRow["TANI"] = this.unit;

                        returnData.Rows.Add(retRow);
                        retRow = returnData.NewRow();
                    }
                }
                #endregion
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

            return returnData;
        }

        /// <summary>
        /// 処分データ取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetShobunData()
        {
            LogUtility.DebugMethodStart();

            DataTable returnData = new DataTable();

            try
            {
                DataTable temp = new DataTable();

                temp.Columns.Add("UPN_SAKI_GYOUSHA_CD");
                temp.Columns.Add("GYOUSHA_NAME_RYAKU");
                temp.Columns.Add("UPN_SAKI_GENBA_CD");
                temp.Columns.Add("GENBA_NAME_RYAKU");
                temp.Columns.Add("HAIKI_SHURUI_CD");
                temp.Columns.Add("HAIKI_SHURUI_NAME");
                temp.Columns.Add("KOUFU_YM");
                temp.Columns.Add("KANSAN_SUU");

                DataTable getDataTable;

                if (!JoukenParam.syuturyokuKubun.Equals("3"))
                {
                    // データの取得
                    getDataTable = new DataTable();
                    getDataTable = getDataDao.GetKamiShobunData(serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                if (!JoukenParam.syuturyokuKubun.Equals("2"))
                {
                    // データの取得
                    getDataTable = getDataDao.GetDenShobunData(serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                // ◆取得したデータを加工してreturnDataにセットする
                #region - カラムを設定 -
                returnData.Columns.Add("MANIKUBUN");
                returnData.Columns.Add("SHOBUN_JUTAKUSHA_CD");
                returnData.Columns.Add("SHOBUN_JUTAKUSHA_MEISHOU");
                returnData.Columns.Add("SHOBUN_JIGYOUJOU_CD");
                returnData.Columns.Add("SHOBUN_JIGYOUJOU_MEISHOU");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_CD");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_MEISHOU");
                returnData.Columns.Add("TANI");

                for (int i = 0; i < monthsBetween + 1; i++)
                {
                    string cName = "TUKIKEI_" + (dateFrom.AddMonths(i).Month).ToString();
                    returnData.Columns.Add(cName);
                }

                #endregion

                #region - 取得したデータを加工 -

                string gyoushaCD = string.Empty;
                string gyoushaName = string.Empty;
                string genbaCD = string.Empty;
                string genbaName = string.Empty;
                string haikiCD = string.Empty;
                string haikiName = string.Empty;
                string maniKubun = string.Empty;

                if (this.JoukenParam.ichijinijiKbn.Equals("1"))
                {
                    maniKubun = "一次マニフェスト";
                }
                else
                {
                    maniKubun = "二次マニフェスト";
                }

                DataRow retRow = returnData.NewRow();
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    gyoushaCD = temp.Rows[i]["UPN_SAKI_GYOUSHA_CD"].ToString();
                    gyoushaName = temp.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();
                    genbaCD = temp.Rows[i]["UPN_SAKI_GENBA_CD"].ToString();
                    genbaName = temp.Rows[i]["GENBA_NAME_RYAKU"].ToString();
                    haikiCD = temp.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    haikiName = temp.Rows[i]["HAIKI_SHURUI_NAME"].ToString();

                    DateTime targetDate;
                    DateTime.TryParse(temp.Rows[i]["KOUFU_YM"].ToString() + "/01", out targetDate);

                    string cName = "TUKIKEI_" + targetDate.Month.ToString();

                    retRow[cName] = this.GetDbValueDouble(temp.Rows[i]["KANSAN_SUU"]);

                    if ((i >= temp.Rows.Count - 1) ||
                        (
                            (!haikiCD.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_CD"].ToString())) ||
                            (!haikiName.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_NAME"].ToString())) ||
                            (!gyoushaCD.Equals(temp.Rows[i + 1]["UPN_SAKI_GYOUSHA_CD"].ToString())) ||
                            (!gyoushaName.Equals(temp.Rows[i + 1]["GYOUSHA_NAME_RYAKU"].ToString())) ||
                            (!genbaCD.Equals(temp.Rows[i + 1]["UPN_SAKI_GENBA_CD"].ToString())) ||
                            (!genbaName.Equals(temp.Rows[i + 1]["GENBA_NAME_RYAKU"].ToString()))
                        )
                       )
                    {
                        retRow["MANIKUBUN"] = maniKubun;
                        retRow["HAIKIBUTU_SHURUI_CD"] = haikiCD;
                        retRow["HAIKIBUTU_SHURUI_MEISHOU"] = haikiName;
                        retRow["SHOBUN_JUTAKUSHA_CD"] = gyoushaCD;
                        retRow["SHOBUN_JUTAKUSHA_MEISHOU"] = gyoushaName;
                        retRow["SHOBUN_JIGYOUJOU_CD"] = genbaCD;
                        retRow["SHOBUN_JIGYOUJOU_MEISHOU"] = genbaName;
                        retRow["TANI"] = this.unit;

                        returnData.Rows.Add(retRow);
                        retRow = returnData.NewRow();
                    }
                }
                #endregion
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

            return returnData;
        }

        /// <summary>
        /// 最終データ取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetSaishuuData()
        {
            LogUtility.DebugMethodStart();

            DataTable returnData = new DataTable();

            try
            {
                DataTable temp = new DataTable();

                temp.Columns.Add("LAST_SBN_GYOUSHA_CD");
                temp.Columns.Add("LAST_SBN_GENBA_CD");
                temp.Columns.Add("GENBA_NAME_RYAKU");
                temp.Columns.Add("HAIKI_SHURUI_CD");
                temp.Columns.Add("HAIKI_SHURUI_NAME");
                temp.Columns.Add("KOUFU_YM");
                temp.Columns.Add("KANSAN_SUU");

                DataTable getDataTable;

                if (!JoukenParam.syuturyokuKubun.Equals("3"))
                {
                    // データの取得
                    getDataTable = new DataTable();
                    getDataTable = getDataDao.GetKamiSaishuuData(serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                if (!JoukenParam.syuturyokuKubun.Equals("2"))
                {
                    // データの取得
                    getDataTable = getDataDao.GetDenSaishuuData(serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                // ◆取得したデータを加工してreturnDataにセットする
                #region - カラムを設定 -
                returnData.Columns.Add("MANIKUBUN");
                returnData.Columns.Add("SAISHUU_SHOBUNJOU_CD");
                returnData.Columns.Add("SAISHUU_SHOBUNJOU_MEISHOU");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_CD");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_MEISHOU");
                returnData.Columns.Add("TANI");

                for (int i = 0; i < monthsBetween + 1; i++)
                {
                    string cName = "TUKIKEI_" + (dateFrom.AddMonths(i).Month).ToString();
                    returnData.Columns.Add(cName);
                }

                #endregion

                #region - 取得したデータを加工 -

                string gyoushaCD = string.Empty;
                string genbaCD = string.Empty;
                string genbaName = string.Empty;
                string haikiCD = string.Empty;
                string haikiName = string.Empty;
                string maniKubun = string.Empty;

                if (this.JoukenParam.ichijinijiKbn.Equals("1"))
                {
                    maniKubun = "一次マニフェスト";
                }
                else
                {
                    maniKubun = "二次マニフェスト";
                }

                DataRow retRow = returnData.NewRow();
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    haikiCD = temp.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    haikiName = temp.Rows[i]["HAIKI_SHURUI_NAME"].ToString();
                    gyoushaCD = temp.Rows[i]["LAST_SBN_GYOUSHA_CD"].ToString();
                    genbaCD = temp.Rows[i]["LAST_SBN_GENBA_CD"].ToString();
                    genbaName = temp.Rows[i]["GENBA_NAME_RYAKU"].ToString();

                    DateTime targetDate;
                    DateTime.TryParse(temp.Rows[i]["KOUFU_YM"].ToString() + "/01", out targetDate);

                    string cName = "TUKIKEI_" + targetDate.Month.ToString();

                    retRow[cName] = this.GetDbValueDouble(temp.Rows[i]["KANSAN_SUU"]);

                    if ((i >= temp.Rows.Count - 1) ||
                        (
                            (!haikiCD.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_CD"].ToString())) ||
                            (!haikiName.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_NAME"].ToString())) ||
                            (!gyoushaCD.Equals(temp.Rows[i + 1]["LAST_SBN_GYOUSHA_CD"].ToString())) ||
                            (!genbaCD.Equals(temp.Rows[i + 1]["LAST_SBN_GENBA_CD"].ToString())) ||
                            (!genbaName.Equals(temp.Rows[i + 1]["GENBA_NAME_RYAKU"].ToString()))
                        )
                       )
                    {
                        retRow["MANIKUBUN"] = maniKubun;
                        retRow["HAIKIBUTU_SHURUI_CD"] = haikiCD;
                        retRow["HAIKIBUTU_SHURUI_MEISHOU"] = haikiName;
                        retRow["SAISHUU_SHOBUNJOU_CD"] = genbaCD;
                        retRow["SAISHUU_SHOBUNJOU_MEISHOU"] = genbaName;
                        retRow["TANI"] = this.unit;

                        returnData.Rows.Add(retRow);
                        retRow = returnData.NewRow();
                    }
                }
                #endregion
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

            return returnData;
        }

        /// <summary>
        /// 廃棄データ取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetHaikiData()
        {
            LogUtility.DebugMethodStart();

            DataTable returnData = new DataTable();

            try
            {
                DataTable temp = new DataTable();

                temp.Columns.Add("HAIKI_SHURUI_CD");
                temp.Columns.Add("HAIKI_SHURUI_NAME");
                temp.Columns.Add("KOUFU_YM");
                temp.Columns.Add("KANSAN_SUU");

                DataTable getDataTable;


                if (!JoukenParam.syuturyokuKubun.Equals("3"))
                {
                    // 廃棄データの取得
                    getDataTable = getDataDao.GetKamiHaikiData(serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                if (!JoukenParam.syuturyokuKubun.Equals("2"))
                {
                    // 廃棄データの取得
                    getDataTable = getDataDao.GetDenHaikiData(serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                // ◆取得したデータを加工してreturnDataにセットする
                #region - カラムを設定 -
                returnData.Columns.Add("MANIKUBUN");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_CD");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_MEISHOU");
                returnData.Columns.Add("TANI");

                for (int i = 0; i < monthsBetween + 1; i++)
                {
                    string cName = "TUKIKEI_" + (dateFrom.AddMonths(i).Month).ToString();
                    returnData.Columns.Add(cName);
                }

                #endregion

                #region - 取得したデータを加工 -

                string haikiCD = string.Empty;
                string haikiName = string.Empty;
                string maniKubun = string.Empty;

                if (this.JoukenParam.ichijinijiKbn.Equals("1"))
                {
                    maniKubun = "一次マニフェスト";
                }
                else
                {
                    maniKubun = "二次マニフェスト";
                }

                DataRow retRow = returnData.NewRow();
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    haikiCD = temp.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    haikiName = temp.Rows[i]["HAIKI_SHURUI_NAME"].ToString();

                    DateTime targetDate;
                    DateTime.TryParse(temp.Rows[i]["KOUFU_YM"].ToString() + "/01", out targetDate);

                    string cName = "TUKIKEI_" + targetDate.Month.ToString();

                    retRow[cName] = this.GetDbValueDouble(temp.Rows[i]["KANSAN_SUU"]);

                    if ((i >= temp.Rows.Count - 1) ||
                        ((!haikiCD.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_CD"].ToString())) ||
                        (!haikiName.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_NAME"].ToString()))))
                    {

                        retRow["MANIKUBUN"] = maniKubun;
                        retRow["HAIKIBUTU_SHURUI_CD"] = haikiCD;
                        retRow["HAIKIBUTU_SHURUI_MEISHOU"] = haikiName;
                        retRow["TANI"] = this.unit;

                        returnData.Rows.Add(retRow);
                        retRow = returnData.NewRow();

                    }
                }
                #endregion
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

            return returnData;
        }

        /// <summary> 取得したデータを一時テーブルに追加する </summary>
        /// <param name="getDataTable">getDataTable：取得したデータ</param>
        /// <param name="tempDataTable">tempDataTable：一時テーブル</param>
        /// <returns>一時テーブル</returns>
        private DataTable AddRow(DataTable getDataTable, DataTable tempDataTable)
        {
            LogUtility.DebugMethodStart(tempDataTable, tempDataTable);

            foreach (DataRow dr in getDataTable.Rows)
            {
                tempDataTable.ImportRow(dr);
            }

            LogUtility.DebugMethodEnd(tempDataTable, tempDataTable);

            return tempDataTable;
        }

        /// <summary>
        /// 検索条件の初期値を設定する
        /// </summary>
        /// <returns>初期値設定後の検索条件</returns>
        private JoukenParam CreateParams()
        {
            LogUtility.DebugMethodStart();

            var info = new JoukenParam();
            // 初期設定条件を設定
            info.ichijinijiKbn = "1";                       // 一時二次区分
            info.nengappiFrom = this.parentbaseform.sysDate.ToString();    // 年月日開始
            info.nengappiTo = this.parentbaseform.sysDate.ToString();      // 年月日終了
            info.kyoten = string.Empty;                     // 拠点CD
            info.haiJigyouShaFrom = string.Empty;           // 排出事業者CD開始
            info.haiJigyouShaTo = string.Empty;             // 排出事業者CD終了
            info.haiJigyouBaFrom = string.Empty;            // 排出事業場CD開始
            info.haiJigyouBaTo = string.Empty;              // 排出事業場CD終了
            info.unpanJutakuShaFrom = string.Empty;         // 搬受託者CD開始
            info.unpanJutakuShaTo = string.Empty;           // 運搬受託者CD終了
            info.shobunJutakuShaFrom = string.Empty;        // 処分受託者CD開始
            info.shobunJutakuShaTo = string.Empty;          // 処分受託者CD終了
            info.saisyuuShobunBashoFrom = string.Empty;     // 最終処分場所CD開始
            info.saisyuuShobunBashoTo = string.Empty;       // 最終処分場所CD終了
            info.chokkouHaikibutuSyurui = string.Empty;     // 産廃（直行）廃棄物種類CD
            info.tsumikaeHaikibutuSyurui = string.Empty;    // 産廃（積替）廃棄物種類CD
            info.kenpaiHaikibutuSyurui = string.Empty;      // 建廃廃棄物種類CD
            info.denshiHaikibutuSyurui = string.Empty;      // 電子廃棄物種類CD
            info.syuturyokuNaiyoiu = "1";                   // 出力内容
            info.syuturyokuKubun = "1";                     // 出力区分

            LogUtility.DebugMethodEnd();
            return info;
        }
    }

    /// <summary>
    /// 検索条件
    /// </summary>
    public class JoukenParam
    {
        public string ichijinijiKbn { get; set; }           // 一時二次区分
        public string nengappiFrom { get; set; }            // 年月日開始
        public string nengappiTo { get; set; }              // 年月日終了
        public string kyoten { get; set; }                  // 拠点CD
        public string haiJigyouShaFrom { get; set; }        // 排出事業者CD開始
        public string haiJigyouShaTo { get; set; }          // 排出事業者CD終了
        public string haiJigyouBaFrom { get; set; }         // 排出事業場CD開始
        public string haiJigyouBaTo { get; set; }           // 排出事業場CD終了
        public string unpanJutakuShaFrom { get; set; }      // 運搬受託者CD開始
        public string unpanJutakuShaTo { get; set; }        // 運搬受託者CD終了
        public string shobunJutakuShaFrom { get; set; }     // 処分受託者CD開始
        public string shobunJutakuShaTo { get; set; }       // 処分受託者CD終了
        public string saisyuuShobunBashoFrom { get; set; }  // 最終処分場所CD開始
        public string saisyuuShobunBashoTo { get; set; }    // 最終処分場所CD終了
        public string chokkouHaikibutuSyurui { get; set; }  // 産廃（直行）廃棄物種類CD
        public string tsumikaeHaikibutuSyurui { get; set; } // 産廃（積替）廃棄物種類CD
        public string kenpaiHaikibutuSyurui { get; set; }   // 建廃廃棄物種類CD
        public string denshiHaikibutuSyurui { get; set; }   // 電子廃棄物種類CD
        public string syuturyokuNaiyoiu { get; set; }       // 出力内容
        public string syuturyokuKubun { get; set; }         // 出力区分
    }
}
