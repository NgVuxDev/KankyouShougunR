using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.FormManager;
using r_framework.Utility;

namespace Shougun.Core.Common.Menu
{
    /// <summary>
    /// 通知情報を表示する枠組を提供するユーザーコントロール
    /// </summary>
    public partial class TsuuchiJouhou : UserControl
    {
        #region - Fields -

        /// <summary>
        /// TOPへの情報公開アセンブリパス
        /// </summary>
        private string assemblyPath = "TopHeNoJouhouKoukai.dll";

        /// <summary>
        /// TOPへの情報公開のクラス名
        /// </summary>
        private string className = "Shougun.Core.ElectronicManifest.TopHeNoJouhouKoukai.Logic.LogicClass";

        /// <summary>
        /// 通知件数取得メソッドの名称
        /// </summary>
        private string methodName = "SelectKensuuData";

        /// <summary>
        /// 通知最短間隔（秒）
        /// </summary>
        private int tsuuchiSpan = 300;

        /// <summary>
        /// TOPへの情報公開のインスタンスを保持する
        /// </summary>
        private object classInstance;

        /// <summary>
        /// 通知情報件数取得メソッドの情報を保持する
        /// </summary>
        private MethodInfo methodInfo;

        /// <summary>
        /// 前回通知情報更新時間
        /// </summary>
        private DateTime lastTsuuchiTime = DateTime.Now.AddDays(-1);

        /// <summary>
        /// 本文のフォント
        /// </summary>
        private Font messageFont = new Font("ＭＳ ゴシック", 9.75F);

        /// <summary>
        /// 本文のサイズ
        /// </summary>
        private Size messageSize = new Size(410, 20);

        /// <summary>
        /// 本文同士の間隔
        /// </summary>
        private int messageSpan = 3;

        /// <summary>
        /// 本文の色
        /// </summary>
        private Color messageColor = Color.Black;

        /// <summary>
        /// 本文の開始位置
        /// </summary>
        private Point messageStartLocation = new Point(10, 18);

        #endregion - Fields -

        #region - Ctor -

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TsuuchiJouhou()
        {
            InitializeComponent();

            // G148:TOPへの情報公開を呼ぶように初期化（メッセージは画面側で設定）
            this.AssemblyPath = this.assemblyPath;
            this.ClassName = this.className;
            this.MethodName = this.methodName;
            this.TsuuchiSpan = this.tsuuchiSpan;
            this.MessageFont = this.messageFont;
            this.MessageSize = this.messageSize;
            this.MessageSpan = this.messageSpan;
            this.MessageStartLocation = this.messageStartLocation;
            this.MessageColor = this.messageColor;
            this.MessageSetting = new System.Collections.Generic.List<Shougun.Core.Common.Menu.TsuuchiJouhou.MessageSettingDto>();

            // 背景透過
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            
            // 枠のサイズをタイトルラベルに合わせて動的に変更
            this.titleLabel.SizeChanged += new EventHandler(this.titleLabel_SizeChanged);
            this.SizeChanged += new EventHandler(this.titleLabel_SizeChanged);
        }

        #endregion - Ctor -

        #region - Props -

        #region - 動作定義 - 

        /// <summary>
        /// 通知最短間隔（秒）
        /// 0をセットすると
        /// </summary>
        [Category("動作定義")]
        [Description("通知時間の最短間隔を指定します。（単位：秒）")]
        public int TsuuchiSpan { get; set; }

        /// <summary>
        /// TOPへの情報公開のアセンブリパス
        /// </summary>
        [Category("動作定義")]
        [DefaultValue("TopHeNoJouhouKoukai.dll")]
        [Description("読み込むアセンブリのパスを指定します。")]
        public string AssemblyPath { get; set; }

        /// <summary>
        /// TOPへの情報公開のクラス名
        /// </summary>
        [Category("動作定義")]
        [DefaultValue("Shougun.Core.ElectronicManifest.TopHeNoJouhouKoukai.Logic.LogicClass")]
        [Description("通知情報件数を取得するメソッドのクラス名を名前空間込みで指定します。")]
        public string ClassName { get; set; }

        /// <summary>
        /// 通知件数取得メソッドの名称
        /// </summary>
        [Category("動作定義")]
        [DefaultValue("SelectKensuuData")]
        [Description("通知情報件数を取得するメソッドの名称を指定します。")]
        public string MethodName { get; set; }

        #endregion - 動作定義 -

        #region - デザイン -

        /// <summary>
        /// 枠線のスタイル
        /// </summary>
        [Category("デザイン")]
        [DefaultValue("True")]
        [Description("枠線のスタイルを指定します。")]
        public BorderStyle FrameBorderStyle
        {
            get
            {
                return this.framePanel.BorderStyle;
            }

            set
            {
                this.framePanel.BorderStyle = value;
            }
        }

        #endregion - デザイン -

        #region - タイトル -

        /// <summary>
        /// タイトルラベルを表示するかどうか
        /// </summary>
        [Category("タイトル")]
        [DefaultValue("True")]
        [Description("タイトルラベルを表示するかどうかを指定します。")]
        public bool TitleVisible
        {
            get
            {
                return this.titleLabel.Visible;
            }

            set
            {
                this.titleLabel.Visible = value;
            }
        }

        /// <summary>
        /// タイトルラベルのテキスト
        /// </summary>
        [Category("タイトル")]
        [DefaultValue("お知らせ")]
        [Description("タイトルラベルに表示される文字列を指定します。")]
        public string TitleText
        {
            get
            {
                return this.titleLabel.Text;
            }

            set
            {
                this.titleLabel.Text = value;
            }
        }

        /// <summary>
        /// タイトルラベルのフォント
        /// </summary>
        [Category("タイトル")]
        [Description("タイトルラベルのフォントを指定します。")]
        public Font TitleFont
        {
            get
            {
                return this.titleLabel.Font;
            }

            set
            {
                this.titleLabel.Font = value;
            }
        }

        /// <summary>
        /// タイトルラベルのサイズ
        /// </summary>
        [Category("タイトル")]
        [Description("タイトルラベルの表示サイズを指定します。")]
        public Size TitleSize
        {
            get
            {
                return this.titleLabel.Size;
            }

            set
            {
                this.titleLabel.Size = value;
            }
        }

        /// <summary>
        /// タイトルラベルのテキスト表示位置
        /// </summary>
        [Category("タイトル")]
        [Description("タイトルラベルのテキスト表示位置を指定します。")]
        public ContentAlignment TitleAlign
        {
            get
            {
                return this.titleLabel.TextAlign;
            }

            set
            {
                this.titleLabel.TextAlign = value;
            }
        }

        /// <summary>
        /// タイトルラベルの背景色
        /// </summary>
        [Category("タイトル")]
        [Description("タイトルラベルの背景色を指定します。")]
        public Color TitleBackColor
        {
            get
            {
                return this.titleLabel.BackColor;
            }

            set
            {
                this.titleLabel.BackColor = value;
            }
        }

        /// <summary>
        /// タイトルラベルの前景色
        /// </summary>
        [Category("タイトル")]
        [Description("タイトルラベルの前景色を指定します。")]
        public Color TitleForColor
        {
            get
            {
                return this.titleLabel.ForeColor;
            }

            set
            {
                this.titleLabel.ForeColor = value;
            }
        }

        /// <summary>
        /// タイトルラベルのY軸方向の位置
        /// </summary>
        [Category("タイトル")]
        [Description("タイトルラベルの横方向の位置を指定します。")]
        public int TitleLocationX
        {
            get
            {
                return this.titleLabel.Location.X;
            }

            set
            {
                this.titleLabel.Location = new Point(value, this.titleLabel.Location.Y);
            }
        }

        #endregion - タイトル -

        #region - 本文 -

        /// <summary>
        /// 本文のフォント
        /// </summary>
        [Category("本文")]
        [Description("本文のフォントを指定します。")]
        public Font MessageFont { get; set; }

        /// <summary>
        /// 本文のサイズ
        /// </summary>
        [Category("本文")]
        [Description("本文の１文当たりの表示サイズを指定します。")]
        public Size MessageSize { get; set; }

        /// <summary>
        /// 本文の行間隔
        /// </summary>
        [Category("本文")]
        [Description("本文の行間隔を指定します。")]
        public int MessageSpan { get; set; }

        /// <summary>
        /// 本文の開始位置
        /// </summary>
        [Category("本文")]
        [Description("本文の枠内の開始位置を指定します。")]
        public Point MessageStartLocation { get; set; }

        /// <summary>
        /// 本文の文字色
        /// </summary>
        [Category("本文")]
        [Description("本文の文字色を指定します。")]
        public Color MessageColor { get; set; }

        /// <summary>
        /// 本文の設定
        /// </summary>
        [Category("本文")]
        [Description("枠内に表示するメッセージの設定を行います。")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<MessageSettingDto> MessageSetting { get; set; }

        #endregion - 本文 -

        #region - DisBroesable -

        /// <summary>
        /// TOPへの情報公開アセンブリの読込チェック
        /// </summary>
        [Browsable(false)]
        public bool Loaded
        {
            get
            {
                return this.methodInfo != null && this.classInstance != null;
            }
        }

        /// <summary>
        /// 設定されているメッセージの数を返します。
        /// </summary>
        [Browsable(false)]
        public int MessageCount
        {
            get
            {
                return this.framePanel.Controls.Count;
            }
        }

        #endregion - DisBrowsable -

        #endregion - Props -

        #region - Events -

        /// <summary>
        /// リンククリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var formId = ((LinkLabel)sender).Links[0].LinkData.ToString();
            FormManager.OpenForm(formId);
        }

        /// <summary>
        /// ラベルの中心を枠が通るようにパネルサイズを動的に変更します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void titleLabel_SizeChanged(object sender, EventArgs e)
        {
            this.framePanel.Location = new Point(this.framePanel.Location.X, this.titleLabel.Height / 2);
            this.framePanel.Size = new Size(this.Width, this.Height - this.titleLabel.Height / 2);
        }

        #endregion

        #region - Methods -

        /// <summary>
        /// TOPへの情報公開のアセンブリをロードします。
        /// </summary>
        /// <returns>True:成功, False:失敗</returns>
        public void LoadTopHeNoJouhouKoukai()
        {
            LogUtility.DebugMethodStart();

            try
            {
                if (!this.Loaded)
                {
                    // TOPへの情報公開アセンブリを取得
                    Assembly assembly = Assembly.LoadFrom(this.AssemblyPath);

                    // インスタンスとメソッド情報を取得する
                    Type type = assembly.GetType(this.ClassName);
                    ConstructorInfo ci = type.GetConstructor(Type.EmptyTypes);
                    this.methodInfo = type.GetMethod(this.MethodName, BindingFlags.Public | BindingFlags.Instance);
                    this.classInstance = ci.Invoke(null);
                }
            }
            catch
            {
                // 例外はスロー
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// G148のSelectKensuuData()呼出
        /// LoadTopHeNoJouhouKoukai()の後に呼ぶ
        /// </summary>
        /// <returns>通知情報件数の配列（TopHeNoJouhouKoukai.SelectKensuuData()の戻り値）</returns>
        public string[] SelectKensuuData()
        {
            // アセンブリロードの確認
            if (!this.Loaded)
            {
                return null;
            }

            // メソッド呼出
            return (string[])this.methodInfo.Invoke(this.classInstance, null);
        }

        /// <summary>
        /// お知らせ欄を更新します
        /// 基本的にこのメソッドの呼び出しのみでOKです。
        /// TsuuchiJouhouそのものの表示/非表示は画面側の制御になります。
        /// </summary>
        /// <returns>True:成功, False:失敗（アセンブリ無）</returns>
        public bool Reload()
        {
            LogUtility.DebugMethodStart();

            bool result = false;

            try
            {
                // 通知間隔チェック
                if (DateTime.Now < this.lastTsuuchiTime.AddSeconds(this.TsuuchiSpan))
                {
                    result = true;
                    return result;
                }

                // お知らせ欄を空にする
                this.framePanel.Controls.Clear();

                // アセンブリの存在確認
                if (!File.Exists(this.AssemblyPath))
                {
                    result = false;
                    return result;
                }

                // アセンブリロード
                this.LoadTopHeNoJouhouKoukai();

                // メソッド呼出
                string[] kensuu = this.SelectKensuuData();

                // 件数チェック
                if (kensuu != null && kensuu.Count(n => n != null) > 0)
                {
                    int rowNum = 0;
                    for (int i = 0; i < this.MessageSetting.Count; i++)
                    {
                        if (kensuu.Length <= i)
                        {
                            // 取得してきた件数データが設定しているメッセージ数より少ない場合はその時点で終了
                            break;
                        }

                        if (string.IsNullOrWhiteSpace(kensuu[i]))
                        {
                            continue;
                        }
                        else if (kensuu[i] == "0")
                        {
                            // 件数が0の場合は通常のラベル
                            var label = new Label();
                            label.Text = this.MessageSetting[i].ZeroMessage;
                            label.Location = new Point(this.MessageStartLocation.X, this.MessageStartLocation.Y + (rowNum * (this.MessageSize.Height + this.MessageSpan)));
                            label.Size = this.MessageSize;
                            label.Font = this.MessageFont;
                            this.framePanel.Controls.Add(label);
                            rowNum++;
                        }
                        else
                        {
                            // 件数が1以上の場合はリンクラベル
                            var linkLabel = new LinkLabel();
                            linkLabel.Text = string.Format(this.MessageSetting[i].ExistMessage, kensuu[i] + this.MessageSetting[i].Unit);
                            linkLabel.Location = new Point(this.MessageStartLocation.X, this.MessageStartLocation.Y + (rowNum * (this.MessageSize.Height + this.MessageSpan)));
                            linkLabel.Size = this.MessageSize;
                            linkLabel.Font = this.MessageFont;
                            linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
                            linkLabel.Links.Add(this.MessageSetting[i].ExistMessage.IndexOf("{"), kensuu[i].Length + this.MessageSetting[i].Unit.Length, this.MessageSetting[i].LinkFormID);
                            linkLabel.TabIndex = rowNum;
                            this.framePanel.Controls.Add(linkLabel);
                            rowNum++;
                        }
                    }
                }

                // 現在時刻を最終更新時刻にセット
                this.lastTsuuchiTime = DateTime.Now;

                result = true;
                return result;
            }
            catch
            {
                // 例外はスロー
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd(this.framePanel.Controls.Count);
                LogUtility.DebugMethodEnd(result);
            }
        }

        #endregion - Methods -

        #region - Class -

        /// <summary>
        /// 本文のメッセージを設定します。
        /// </summary>
        [DesignTimeVisible(true)]
        public class MessageSettingDto : Component
        {
            /// <summary>
            /// 件数0の時に表示するメッセージ
            /// </summary>
            [Description("件数0の時に表示するメッセージを指定します。")]
            public string ZeroMessage { get; set; }

            /// <summary>
            /// 件数1以上の時に表示するメッセージ
            /// </summary>
            [Description("件数1以上の時に表示するメッセージを指定します。{0}に件数と単位が挿入されます。")]
            public string ExistMessage { get; set; }

            /// <summary>
            /// 件数1件以上の時にリンクする画面のID
            /// </summary>
            [Description("件数1以上の時に件数クリックで遷移する画面のFormIDを指定します。")]
            public string LinkFormID { get; set; }

            /// <summary>
            /// 単位
            /// </summary>
            [Description("単位を指定します。（例：件）")]
            public string Unit { get; set; } 

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public MessageSettingDto()
            {
                this.ZeroMessage = string.Empty;
                this.ExistMessage = string.Empty;
                this.LinkFormID = string.Empty;
                this.Unit = string.Empty;
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="zeroMessage">件数0の場合のメッセージ</param>
            /// <param name="existMessage">件数1以上の場合のメッセージ</param>
            /// <param name="linkFormID">リンク先FormID</param>
            public MessageSettingDto(string zeroMessage, string existMessage, string linkFormID, string unit)
            {
                this.ZeroMessage = zeroMessage;
                this.ExistMessage = existMessage;
                this.LinkFormID = linkFormID;
                this.Unit = unit;
            }
        }

        #endregion
    }
}
