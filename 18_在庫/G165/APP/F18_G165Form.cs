// $Id: F18_G165Form.cs 11341 2013-12-16 12:04:22Z sys_dev_18 $

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
using r_framework.Entity;
using Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.Logic;

namespace Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.APP
{
    /// <summary>
    /// 在庫伝票入力画面
    /// </summary>
    public partial class F18_G165Form : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private F18Logic logic;

        /// <summary>
        /// 在庫明細_受入リスト（返却）
        /// </summary>
        public List<T_ZAIKO_UKEIRE_DETAIL> RetZaikoUkeireDetail { get; set; }

        /// <summary>
        /// 在庫明細_出荷リスト（返却）
        /// </summary>
        public List<T_ZAIKO_SHUKKA_DETAIL> RetZaikoShukkaDetail { get; set; }

        /// <summary>
        /// 在庫明細_受入リスト（受入）
        /// </summary>
        internal List<T_ZAIKO_UKEIRE_DETAIL> recZaikoUkeireDetail;

        /// <summary>
        /// 在庫明細_出荷リスト（受入）
        /// </summary>
        internal List<T_ZAIKO_SHUKKA_DETAIL> recZaikoShukkaDetail;

        /// <summary>
        /// 伝種区分
        /// </summary>
        internal DENSHU_KBN denshuKBN;

        #endregion

        #region 初期処理

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="hinmeiCd">品名CD</param>
        /// <param name="hinmeiName">品名</param>
        /// <param name="suuryou">数量</param>
        /// <param name="syoumiJyuuryou">正味重量</param>
        /// <param name="nisugataSuuryou">荷姿数量</param>
        /// <param name="nisugataUnitName">荷姿単位名</param>
        /// <param name="kingaku">金額</param>
        /// <param name="lstZaikoUkeireDetail">在庫明細_受入Entityリスト</param>
        public F18_G165Form(string hinmeiCd, string hinmeiName, decimal suuryou, decimal syoumiJyuuryou,
                            decimal nisugataSuuryou, string nisugataUnitName, decimal kingaku,
                            List<T_ZAIKO_UKEIRE_DETAIL> lstZaikoUkeireDetail)
            : base(WINDOW_ID.T_ZAIKO_DENPYOU_NYUURYOKU, WINDOW_TYPE.NONE)
        {
            try
            {

                LogUtility.DebugMethodStart(hinmeiCd, hinmeiName, suuryou, syoumiJyuuryou
                                    , nisugataSuuryou, nisugataUnitName, kingaku, lstZaikoUkeireDetail);

                this.InitializeComponent();

                // パラメータ設定
                Properties.Settings.Default.HinmeiCd = hinmeiCd;
                Properties.Settings.Default.HinmeiName = hinmeiName;
                Properties.Settings.Default.Suuryou = suuryou;
                Properties.Settings.Default.SyoumiJyuuryou = syoumiJyuuryou;
                Properties.Settings.Default.NisugataSuuryou = nisugataSuuryou;
                Properties.Settings.Default.NisugataUnitName = nisugataUnitName;
                Properties.Settings.Default.Kingaku = kingaku;
                Properties.Settings.Default.Save();
                // 在庫受入明細パラメータ
                this.recZaikoUkeireDetail = lstZaikoUkeireDetail;

                // 伝種区分(受入)
                this.denshuKBN = DENSHU_KBN.UKEIRE;

                // 行のヘッダを表示
                this.ZaikoMeisaiIchiran.RowHeadersVisible = true;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new F18Logic(this);

                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="hinmeiCd">品名CD</param>
        /// <param name="hinmeiName">品名</param>
        /// <param name="suuryou">数量</param>
        /// <param name="syoumiJyuuryou">正味重量</param>
        /// <param name="nisugataSuuryou">荷姿数量</param>
        /// <param name="nisugataUnitName">荷姿単位名</param>
        /// <param name="kingaku">金額</param>
        /// <param name="lstZaikoShukkaDetail">在庫明細_出荷Entityリスト</param>
        public F18_G165Form(string hinmeiCd, string hinmeiName, decimal suuryou, decimal syoumiJyuuryou,
                            decimal nisugataSuuryou, string nisugataUnitName, decimal kingaku,
                            List<T_ZAIKO_SHUKKA_DETAIL> lstZaikoShukkaDetail)
            : base(WINDOW_ID.T_ZAIKO_DENPYOU_NYUURYOKU, WINDOW_TYPE.NONE)
        {
            try
            {

                LogUtility.DebugMethodStart(hinmeiCd, hinmeiName, suuryou, syoumiJyuuryou
                                    , nisugataSuuryou, nisugataUnitName, kingaku, lstZaikoShukkaDetail);

                this.InitializeComponent();

                // パラメータ設定
                Properties.Settings.Default.HinmeiCd = hinmeiCd;
                Properties.Settings.Default.HinmeiName = hinmeiName;
                Properties.Settings.Default.Suuryou = suuryou;
                Properties.Settings.Default.SyoumiJyuuryou = syoumiJyuuryou;
                Properties.Settings.Default.NisugataSuuryou = nisugataSuuryou;
                Properties.Settings.Default.NisugataUnitName = nisugataUnitName;
                Properties.Settings.Default.Kingaku = kingaku;
                Properties.Settings.Default.Save();
                // 在庫出荷明細パラメータ
                this.recZaikoShukkaDetail = lstZaikoShukkaDetail;

                // 伝種区分(出荷)
                this.denshuKBN = DENSHU_KBN.SHUKKA;

                // 行のヘッダを表示
                this.ZaikoMeisaiIchiran.RowHeadersVisible = true;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new F18Logic(this);

                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 画面ロード処理
        /// <summary>
        /// 画面ロード処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                this.logic.WindowInit();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion

        #region UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        internal Control[] GetAllControl()
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<Control> allControl = new List<Control>();
                allControl.AddRange(this.allControl);
                allControl.AddRange(controlUtil.GetAllControls(this.logic.headerForm));
                allControl.AddRange(controlUtil.GetAllControls(this.logic.parentForm));

                return allControl.ToArray();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region イベント処理

        #region CellValidatingイベント
        /// <summary>
        /// CellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ZaikoMeisaiIchiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.Disposing)
                {
                    return;
                }

                // CellValidating処理
                if (!logic.DataGridViewCellValidating(e))
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region CellValidatedイベント
        /// <summary>
        /// CellValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ZaikoMeisaiIchiran_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.Disposing)
                {
                    return;
                }

                // CellValidated処理
                logic.DataGridViewCellValidated(e);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region CellEnterイベント
        /// <summary>
        /// CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZaikoMeisaiIchiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // CellEnter処理
                logic.DataGridViewCellEnter(e);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion
    }
}
