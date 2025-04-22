using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Menu;
using r_framework.Utility;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Configuration;

namespace r_framework.APP.Base
{
    #region - Class -

    /// <summary>メイン画面を表すクラス・コントロール</summary>
    public partial class RibbonMainMenu : SuperForm
    {
        #region - Fields -

        /// <summary>XMLファイル名を保持するフィールド(menu.xmlファイルのフルパス)</summary>
        private string xmlName = "menu.xml";

        /// <summary>選択ＴＡＢインデックスを保持するフィールド</summary>
        private int indexSelectTab = 0;

        /// <summary>マスターＴＡＢの位置インデックスを保持するフィールド</summary>
        private int indexMasterTab = 0;

        /// <summary>FormType=Modalを示す文字列</summary>
        private string modal = "modal";

        /// <summary>FormType=Dialogを示す文字列</summary>
        private string dialog = "dialog";

        /// <summary>
        /// 選択状態となっているボタンを保持するフィールド
        /// </summary>
        private RibbonButton selectedButton = null;

        /// <summary>
        /// マスタメニューが展開されているかどうか
        /// </summary>
        private bool orbMenuOpened = false;

        /// <summary>
        /// 検索結果一覧のフォームID
        /// </summary>
        private string kensakukekkaIchiran = "G176";

        /// <summary>
        /// 参照権限エラーのエラーコード
        /// </summary>
        private string kengenErrorCd = "E134";

        /// <summary>
        /// アセンブリ無エラーのエラーコード
        /// </summary>
        private string notFoundErrorCd = "E135";

        /// <summary>
        /// 機能の利用区分:2.使用しない となっている場合のエラーコード
        /// </summary>
        private string useKbnErrorCd = "E139";

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="RibbonMainMenu"/> class.</summary>
        public RibbonMainMenu()
        {
            this.InitializeComponent();
            this.xmlName = @"setting\menu.xml";
        }

        /// <summary>
        /// menu.xmlファイルパスと共有情報(DB)を使用したコンストラクタ
        /// </summary>
        /// <param name="menuXmlFile">menu.xmlファイルフルパス</param>
        /// <param name="commInfo"></param>
        public RibbonMainMenu(string menuXmlFile, CommonInformation commInfo)
        {
            this.InitializeComponent();

            this.xmlName = menuXmlFile;
            this.GlobalCommonInformation = commInfo;
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>メニューアイテムを保持するプロパティ</summary>
        public List<MenuItemComm> menuItems = new List<MenuItemComm>();

        /// <summary>アプリケーション共有情報クラス</summary>
        public CommonInformation GlobalCommonInformation { get; private set; }

        /// <summary>menu.xmlファイルのフルパス</summary>
        public string MenuConfigXML
        {
            get
            {
                return this.xmlName;
            }
        }

        /// <summary>
        /// 環境将軍シリーズ名を保持するプロパティ（例 ： A1, C5等）
        /// </summary>
        public string Series { get; private set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>メニューアイテムのロード処理を実行する</summary>
        /// <remarks>メニューアイテム構成用XMLファイルの読み込み処理</remarks>
        private void LoadMenuItem()
        {
            // XMLファイル一括読み込み
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(this.xmlName);

            var shainCD = SystemProperty.Shain.CD;
            int parseResult = 0;

            // グループアイテム処理
            foreach (XmlNode groupItemNode in xmlDocument.DocumentElement.ChildNodes)
            {
                GroupItem groupItem = new GroupItem();

                if (groupItemNode.NodeType == XmlNodeType.Element)
                {
                    if (groupItemNode.LocalName.ToLower() == "series")
                    {
                        // シリーズ名を取得
                        this.Series = groupItemNode.Attributes["name"].Value;
                    }

                    if (groupItemNode.LocalName.ToLower() != "group")
                    {   // グループタグでない
                        continue;
                    }

                    if (groupItemNode.Attributes.Count != 0)
                    {
                        foreach (XmlAttribute xmlAttribute in groupItemNode.Attributes)
                        {
                            switch (xmlAttribute.LocalName.ToLower())
                            {
                                case "index-no":    // インデックス番号
                                    int.TryParse(xmlAttribute.Value, out parseResult);
                                    groupItem.IndexNo = parseResult;
                                    break;
                                case "name":        // 名前
                                    groupItem.Name = xmlAttribute.Value;
                                    groupItem.IsMasterItem = groupItem.Name.Equals("マスタ");
                                    break;
                                case "icon-name":   // アイコン名
                                    groupItem.IconName = xmlAttribute.Value;
                                    break;
                                case "icon-size":   // アイコンサイズ
                                    groupItem.IconSize = xmlAttribute.Value.ToLower();
                                    break;
                                case "tool-tip":    // ツールチップ
                                    groupItem.ToolTip = xmlAttribute.Value;
                                    break;
                                case "visible":       // 表示フラグ "0" : 表示しない（0以外は表示する）
                                    groupItem.Disabled = xmlAttribute.Value == "0";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                if (groupItem.IndexNo < 1 || string.IsNullOrWhiteSpace(groupItem.Name))
                {
                    // インデックス番号が0以下の場合は追加しない
                    continue;
                }

                // サブアイテム処理
                foreach (XmlNode subItemNode in groupItemNode.ChildNodes)
                {
                    SubItem subItem = new SubItem();

                    if (subItemNode.NodeType == XmlNodeType.Element)
                    {
                        if (subItemNode.LocalName.ToLower() != "sub")
                        {   // サブタグでない
                            continue;
                        }

                        if (subItemNode.Attributes.Count != 0)
                        {
                            foreach (XmlAttribute xmlAttribute in subItemNode.Attributes)
                            {
                                switch (xmlAttribute.LocalName.ToLower())
                                {
                                    case "index-no":        // インデックス番号
                                        int.TryParse(xmlAttribute.Value, out parseResult);
                                        subItem.IndexNo = parseResult;
                                        break;
                                    case "name":            // 名前
                                        subItem.Name = xmlAttribute.Value;
                                        break;
                                    case "tool-tip":        // ツールチップ
                                        subItem.ToolTip = xmlAttribute.Value;
                                        break;
                                    case "icon-size":       // アイコンサイズ
                                        subItem.IconSize = xmlAttribute.Value.ToLower();
                                        break;
                                    case "icon-name":       // アイコン名
                                        subItem.IconName = xmlAttribute.Value;
                                        break;
                                    case "visible":       // 表示フラグ "0" : 表示しない（0以外は表示する）
                                        subItem.Disabled = xmlAttribute.Value == "0";
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    if (subItem.IndexNo < 1)
                    {
                        // インデックス番号が0以下の場合は追加しない
                        continue;
                    }

                    bool isExistsSeparator = false;

                    // アセンブリアイテム処理
                    foreach (XmlNode assemblyItemNode in subItemNode.ChildNodes)
                    {
                        AssemblyItem assemblyItem = new AssemblyItem();

                        if (assemblyItemNode.NodeType == XmlNodeType.Element)
                        {
                            if (assemblyItemNode.LocalName.ToLower() != "assembly")
                            {   // アセンブリタグでない
                                continue;
                            }

                            if (assemblyItemNode.Attributes.Count != 0)
                            {
                                foreach (XmlAttribute xmlAttribute in assemblyItemNode.Attributes)
                                {
                                    switch (xmlAttribute.LocalName.ToLower())
                                    {
                                        case "index-no":        // インデックス番号
                                            int.TryParse(xmlAttribute.Value, out parseResult);
                                            assemblyItem.IndexNo = parseResult;
                                            break;
                                        case "name":            // 名前
                                            assemblyItem.Name = xmlAttribute.Value;
                                            break;
                                        case "tool-tip":        // ツールチップ
                                            assemblyItem.ToolTip = xmlAttribute.Value;
                                            break;
                                        case "icon-size":       // アイコンサイズ
                                            assemblyItem.IconSize = xmlAttribute.Value.ToLower();
                                            break;
                                        case "icon-name":       // アイコン名
                                            assemblyItem.IconName = xmlAttribute.Value;
                                            break;
                                        case "form-id":         // フォームID
                                            assemblyItem.FormID = xmlAttribute.Value;
                                            break;
                                        case "window-id":       // 画面ID
                                            WINDOW_ID wid;
                                            if (Enum.TryParse<WINDOW_ID>(xmlAttribute.Value, out wid))
                                            {
                                                assemblyItem.WindowID = (int)wid;
                                            }
                                            break;
                                        case "namespace":       // 名前空間
                                            assemblyItem.NameSpace = xmlAttribute.Value;
                                            break;
                                        case "class-name":      // クラス名
                                            assemblyItem.ClassName = xmlAttribute.Value;
                                            break;
                                        case "assembly-name":   // アセンブリ名
                                            assemblyItem.AssemblyName = xmlAttribute.Value.ToLower();
                                            break;
                                        case "group-name":      // グループ名
                                            assemblyItem.GroupName = xmlAttribute.Value;
                                            break;
                                        case "group-icon-name": // グループアイコン名
                                            assemblyItem.GroupIconName = xmlAttribute.Value;
                                            break;
                                        case "group-icon-size": // グループアイコンサイズ
                                            assemblyItem.GroupIconSize = xmlAttribute.Value.ToLower();
                                            break;
                                        case "group-tool-tip":  // グループツールチップ
                                            assemblyItem.GroupToolTip = xmlAttribute.Value;
                                            break;
                                        case "form-type":       // モーダル指定
                                            assemblyItem.FormType = xmlAttribute.Value.ToLower();
                                            break;
                                        case "visible":         // 表示フラグ "0" : 表示しない（0以外は表示する）
                                            assemblyItem.Disabled = xmlAttribute.Value == "0";
                                            break;
                                        case "window-type":     // 起動モード（権限チェック用）
                                            WINDOW_TYPE winType;
                                            if (Enum.TryParse<WINDOW_TYPE>(xmlAttribute.Value, out winType))
                                            {
                                                assemblyItem.WindowType = winType;
                                            }
                                            break;
                                        case "use-auth-add":    // 新規権限 "0" : 設定不可（0以外は設定可能）
                                            assemblyItem.UseAuthAdd = xmlAttribute.Value != "0";
                                            break;
                                        case "use-auth-edit":   // 修正権限 "0" : 設定不可（0以外は設定可能）
                                            assemblyItem.UseAuthEdit = xmlAttribute.Value != "0";
                                            break;
                                        case "use-auth-delete": // 削除権限 "0" : 設定不可（0以外は設定可能）
                                            assemblyItem.UseAuthDelete = xmlAttribute.Value != "0";
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }

                        if (assemblyItem.IndexNo < 1)
                        {
                            // インデックス番号が0以下の場合は追加しない
                            continue;
                        }

                        if (assemblyItem.Name != "separator")
                        {
                            // 拡張子の確認
                            if (!assemblyItem.AssemblyName.ToLower().EndsWith(".dll"))
                            {   // 末尾に拡張子が存在しない
                                assemblyItem.AssemblyName += ".dll";
                            }
                            // 権限情報をセット
                            assemblyItem.UserAuth = SystemProperty.Shain.GetAuth(assemblyItem.FormID, assemblyItem.WindowID > 0 ? (WINDOW_ID)assemblyItem.WindowID : WINDOW_ID.NONE);
                        }

                        //20151020 hoanghm #4027 start
                        //システム設定- 伝票確定メニュー-伝票確定入力を利用を「2.しない」とした時、環境将軍Rを再起動したタイミングで、伝票確定画面をメニューから非表示にする。
                        if (this.GlobalCommonInformation.SysInfo.URIAGE_KAKUTEI_USE_KBN == 2 && assemblyItem.FormID.Equals("G576"))
                        {
                            assemblyItem.Disabled = true;
                        }
                        if (this.GlobalCommonInformation.SysInfo.UKEIRESHUKA_GAMEN_SIZE == 1)
                        {
                            if (assemblyItem.FormID.Equals("G051") || assemblyItem.FormID.Equals("G053"))
                            {
                                assemblyItem.Disabled = true;
                            }
                        }
                        else if (this.GlobalCommonInformation.SysInfo.UKEIRESHUKA_GAMEN_SIZE == 2)
                        {
                            if (assemblyItem.FormID.Equals("G721") || assemblyItem.FormID.Equals("G722"))
                            {
                                assemblyItem.Disabled = true;
                            }
                        }

                        if (this.Series == "C2" ||
                            this.Series == "C3" ||
                            this.Series == "C5" ||
                            this.Series == "C7" ||
                            this.Series == "C8" ||
                            this.Series == "C9" ||
                            this.Series == "D3" ||
                            this.Series == "D4")
                        {
                            if (assemblyItem.FormID.Equals("M738"))
                            {
                                // このシリーズではオプションがONでも表示したくない画面
                                continue;
                            }
                        }

                        //If previous item is "separator" and current item is "separator" too, I will set current item is disable.
                        //Because I don't want to show two "separator" next to each other.
                        if (assemblyItem.Name.ToLower().Equals("separator"))
                        {
                            if (isExistsSeparator)
                            {
                                assemblyItem.Disabled = true;
                            }
                            else
                            {
                                isExistsSeparator = true;
                            }
                        }
                        else
                        {
                            if (!assemblyItem.Disabled)
                            {
                                isExistsSeparator = false;
                            }
                        }
                        //20151020 hoanghm #4027 end

                        subItem.AssemblyItems.Add(assemblyItem);
                        subItem.AssemblyItems.Sort(new MenuIndexNoComparer());
                        // インデックス番号を採番し直す
                        for (int i = 0; i < subItem.AssemblyItems.Count; i++)
                        {
                            subItem.AssemblyItems[i].IndexNo = i;
                        }
                    }

                    groupItem.SubItems.Add(subItem);
                    groupItem.SubItems.Sort(new MenuIndexNoComparer());
                    // インデックス番号を採番し直す
                    for (int i = 0; i < groupItem.SubItems.Count; i++)
                    {
                        groupItem.SubItems[i].IndexNo = i;
                    }
                }

                /// ProtectMode(true:通常、false:ログアウトのみ)
                if ((AppConfig.ProtectMode) || ((!AppConfig.ProtectMode) && (groupItem.Name == "ログアウト")))
                {
                    this.menuItems.Add(groupItem);
                    this.menuItems.Sort(new MenuIndexNoComparer());
                    // インデックス番号を採番し直す
                    for (int i = 0; i < this.menuItems.Count; i++)
                    {
                        this.menuItems[i].IndexNo = i;
                    }
                }
            }

            /// ProtectMode(true:右上の検索表示あり、false:右上の検索表示なし)
            if (AppConfig.ProtectMode)
            {
                this.txb_quickSearch.Visible = true;
                this.seachButton.Visible = true;
            }
            else
            {
                this.txb_quickSearch.Visible = false;
                this.seachButton.Visible = false;
            }
            
            LogUtility.DebugMethodEnd();
        }

        /// <summary>ショートカット文字列からキー変換を実行する・取得する</summary>
        /// <param name="keys">ショートカットキーを表す文字列</param>
        /// <returns>ショートカット文字列が変換された値</returns>
        private Keys ConvertShortcutKeys(string keys)
        {
            if (keys == null || keys == string.Empty)
            {
                return Keys.None;
            }

            string[] keyList = keys.Split('+');

            Keys keysEnum = Keys.None;

            foreach (string key in keyList)
            {
                string tmp = key;

                int result;
                if (int.TryParse(tmp, out result))
                {   // 数値変換可能
                    tmp = string.Format("D{0}", result);
                }
                else
                {
                    if (tmp == "Ctrl")
                    {
                        tmp = "Control";
                    }
                }

                keysEnum |= (Keys)Enum.Parse(typeof(Keys), tmp);
            }
            return keysEnum;
        }

        /// <summary>メニューコントロールの作成処理を実行する</summary>
        private void MakeMenuCtrl()
        {
            foreach (GroupItem groupItem in this.menuItems.Where(n => !n.Disabled))
            {
                // Tabの追加
                RibbonTab ribbonTab = new RibbonTab(groupItem.Name);
                ribbonTab.MouseLeave += new MouseEventHandler(this.ribbonTab_MouseLeave);
                ribbonTab.ActiveChanged += new EventHandler(this.TabRibbon_Select);
                // ツールチップの設定
                if (string.IsNullOrWhiteSpace(groupItem.ToolTip))
                {
                    ribbonTab.ToolTip = groupItem.Name;
                }
                else
                {
                    ribbonTab.ToolTip = groupItem.ToolTip;
                    ribbonTab.ToolTipTitle = groupItem.Name;
                }
                ribbonTab.Tag = groupItem.IndexNo;

                if (groupItem.IsMasterItem)
                {
                    #region マスターアイテム

                    // マスタタブのインデックスを登録
                    this.indexMasterTab = groupItem.IndexNo;

                    foreach (SubItem subItem in groupItem.SubItems.Where(n => !n.Disabled))
                    {
                        RibbonOrbMenuItem ribbonOrbMenuItem = new RibbonOrbMenuItem(subItem.Name);
                        ribbonOrbMenuItem.MouseEnter += new MouseEventHandler(this.ribbonItem_MouseEnter);
                        ribbonOrbMenuItem.MouseLeave += new MouseEventHandler(this.ribbonItem_MouseLeave);
                        ribbonOrbMenuItem.DropDownArrowDirection = RibbonArrowDirection.Left;
                        ribbonOrbMenuItem.Style = RibbonButtonStyle.SplitDropDown;
                        ribbonOrbMenuItem.Tag = subItem.IndexNo;
                        ribbonOrbMenuItem.Value = this.ribbonMenu.OrbDropDown.MenuItems.Count.ToString();

                        // ツールチップの設定
                        if (string.IsNullOrWhiteSpace(subItem.ToolTip))
                        {
                            ribbonOrbMenuItem.ToolTip = subItem.Name;
                        }
                        else
                        {
                            ribbonOrbMenuItem.ToolTip = subItem.ToolTip;
                            ribbonOrbMenuItem.ToolTipTitle = subItem.Name;
                        }

                        // リソースの存在確認とロード
                        if (subItem.IconSize == "small")
                        {
                            ribbonOrbMenuItem.Image = this.CheckResource(subItem.IconName, true);
                            ribbonOrbMenuItem.MaxSizeMode = RibbonElementSizeMode.Medium;
                        }
                        else
                        {
                            ribbonOrbMenuItem.Image = this.CheckResource(subItem.IconName, false);
                        }

                        foreach (AssemblyItem assemblyItem in subItem.AssemblyItems.Where(n => !n.Disabled))
                        {
                            // Buttonの追加
                            RibbonDescriptionMenuItem ribbonDescriptionMenuItem = new RibbonDescriptionMenuItem(assemblyItem.Name);
                            ribbonDescriptionMenuItem.Click += new EventHandler(RibbonDescriptionMenuItem_Click);
                            ribbonDescriptionMenuItem.Description = assemblyItem.ToolTip; // 全角26文字迄
                            ribbonDescriptionMenuItem.Tag = string.Format("{0}-{1}", subItem.IndexNo, assemblyItem.IndexNo);
                            ribbonDescriptionMenuItem.Value = ribbonOrbMenuItem.DropDownItems.Count.ToString();

                            // リソースの存在確認とロード
                            if (assemblyItem.IconSize == "small")
                            {
                                ribbonDescriptionMenuItem.MaxSizeMode = RibbonElementSizeMode.Medium;
                                ribbonDescriptionMenuItem.SmallImage = this.CheckResource(assemblyItem.IconName, true);
                            }
                            else
                            {
                                ribbonDescriptionMenuItem.Image = this.CheckResource(assemblyItem.IconName, false);
                            }

                            ribbonDescriptionMenuItem.DropDownArrowDirection = RibbonArrowDirection.Left;
                            ribbonOrbMenuItem.DropDownItems.Add(ribbonDescriptionMenuItem);

                        }

                        this.ribbonMenu.OrbDropDown.MenuItems.Add(ribbonOrbMenuItem);
                    }

                    // ボタン数に応じてサイズ調整
                    this.ribbonMenu.OrbDropDown.Height = this.ribbonMenu.OrbDropDown.MenuItems.Count * 48 + 60;

                    // タブ追加
                    this.ribbonMenu.Tabs.Add(ribbonTab);

                    // マスタタブは非表示（ドロップダウンメニューがあるため）
                    this.ribbonMenu.Tabs[this.indexMasterTab].Visible = false;
                    #endregion
                }
                else
                {
                    #region 業務系アイテム
                    int itemNum = 0;
                    foreach (SubItem subItem in groupItem.SubItems.Where(n => !n.Disabled))
                    {
                        // パネルの追加
                        RibbonPanel ribbonPanel = new RibbonPanel(subItem.Name);
                        // 右下のマークを消す
                        ribbonPanel.ButtonMoreVisible = false;
                        // アイコン指定
                        ribbonPanel.Image = this.CheckResource(subItem.IconName, true);

                        // グループボタンのインスタンスを生成
                        RibbonButton splitButton = new RibbonButton();

                        foreach (AssemblyItem assemblyItem in subItem.AssemblyItems.Where(n => !n.Disabled))
                        {
                            if (!string.IsNullOrWhiteSpace(assemblyItem.GroupName))
                            {
                                // グループボタン
                                // ボタン名とグループ名が異なる場合初期化
                                if (splitButton.Text != assemblyItem.GroupName)
                                {
                                    splitButton = new RibbonButton(assemblyItem.GroupName);
                                    splitButton.Style = RibbonButtonStyle.SplitDropDown;
                                    splitButton.Click += new EventHandler(this.splitButton_Click);
                                    splitButton.MouseLeave += new MouseEventHandler(this.ribbonItem_MouseLeave);

                                    // アイコン指定
                                    splitButton.SmallImage = this.CheckResource(assemblyItem.GroupIconName, true);
                                    splitButton.Image = this.CheckResource(assemblyItem.GroupIconName, false);

                                    // ツールチップの設定
                                    if (string.IsNullOrWhiteSpace(assemblyItem.GroupToolTip))
                                    {
                                        splitButton.ToolTip = assemblyItem.GroupName;
                                    }
                                    else
                                    {
                                        splitButton.ToolTip = assemblyItem.GroupToolTip;
                                        splitButton.ToolTipTitle = assemblyItem.GroupName;
                                    }

                                    // 最大サイズ指定（画像無だとエラーで落ちるので画像もチェック）
                                    if (assemblyItem.GroupIconSize == "small" || splitButton.Image == null)
                                    {
                                        splitButton.MaxSizeMode = RibbonElementSizeMode.Medium;
                                    }
                                    else
                                    {
                                        splitButton.MaxSizeMode = RibbonElementSizeMode.Large;
                                    }
                                }

                                if (assemblyItem.Name.ToLower() == "separator")
                                {   // Separatorの追加
                                    RibbonSeparator ribbonSeparator = new RibbonSeparator();
                                    ribbonSeparator.Text = "separator";
                                    splitButton.DropDownItems.Add(ribbonSeparator);
                                }
                                else
                                {
                                    RibbonButton ribbonButton = this.MakeTransactionButton(assemblyItem, subItem.IndexNo, assemblyItem.IndexNo);
                                    ribbonButton.Value = splitButton.DropDownItems.Count.ToString();
                                    // グループボタンにボタンを追加
                                    splitButton.DropDownItems.Add(ribbonButton);
                                }

                                // グループアイテム数に達したらパネルに追加
                                if (splitButton.DropDownItems.Count == subItem.AssemblyItems.Cast<AssemblyItem>().Count(n => n.GroupName == splitButton.Text && !n.Disabled))
                                {
                                    splitButton.Value = itemNum.ToString();
                                    ribbonPanel.Items.Add(splitButton);
                                    itemNum++;
                                }
                            }
                            else
                            {
                                if (assemblyItem.Name.ToLower() == "separator")
                                {   // Separatorの追加
                                    RibbonSeparator ribbonSeparator = new RibbonSeparator();
                                    ribbonSeparator.Text = "separator";
                                    ribbonPanel.Items.Add(ribbonSeparator);
                                }
                                else
                                {
                                    // Buttonの追加
                                    RibbonButton ribbonButton = this.MakeTransactionButton(assemblyItem, subItem.IndexNo, assemblyItem.IndexNo);
                                    ribbonButton.Value = itemNum.ToString();
                                    ribbonPanel.Items.Add(ribbonButton);
                                    itemNum++;
                                }
                            }
                        }

                        // パネル追加
                        ribbonTab.Panels.Add(ribbonPanel);
                    }

                    // タブ追加
                    ribbonTab.Value = itemNum.ToString();
                    this.ribbonMenu.Tabs.Add(ribbonTab);
                    #endregion 業務系アイテム
                }
            }

            // マスタタブの設定
            if (this.ribbonMenu.OrbDropDown.MenuItems.Count <= 0)
            {
                // マスターアイテムが存在しない場合は表示しない
                this.ribbonMenu.OrbVisible = false;
            }
            else
            {
                // マスターアイテムが存在する場合は表示
                this.ribbonMenu.OrbVisible = true;
                this.ribbonMenu.OrbDropDown.Opening += new CancelEventHandler(OrbDropDown_Opening);
                this.ribbonMenu.OrbDropDown.Closed += new EventHandler(this.OrbDropDown_Closed);
                this.ribbonMenu.OrbDropDown.MinimumSize = new Size(400, 420);
            }

            // 表示されている最も若いタブをアクティベート
            var firstTab = this.ribbonMenu.Tabs.FirstOrDefault(t => t.Visible);
            if (firstTab != null)
            {
                this.ribbonMenu.ActiveTab = firstTab;
            }
            else
            {
                // 表示されるタブがない場合、レイアウトが崩れる為空のタブを追加する。
                this.ribbonMenu.Tabs.Add(new RibbonTab(" "));
            }
        }

        /// <summary>オプションメニューの非表示⇒表示対応</summary>
        private void DisplayOptionMenu()
        {
            // 非表示処理の初期化(親が非表示ならそれに紐付く子も非表示)
            foreach (GroupItem groupItem in this.menuItems)
            {
                if (groupItem.Disabled)
                {
                    foreach (SubItem subItem in groupItem.SubItems)
                    {
                        subItem.Disabled = true;
                        foreach (AssemblyItem assemblyItem in subItem.AssemblyItems)
                        {
                            // セパレーターは初期しない
                            if (assemblyItem.Name.ToLower() != "separator")
                            {
                                assemblyItem.Disabled = true;
                            }
                        }
                    }
                }
                else
                {
                    foreach (SubItem subItem in groupItem.SubItems)
                    {
                        if (subItem.Disabled)
                        {
                            foreach (AssemblyItem assemblyItem in subItem.AssemblyItems)
                            {
                                // セパレーターは初期しない
                                if (assemblyItem.Name.ToLower() != "separator")
                                {
                                    assemblyItem.Disabled = true;
                                }
                            }
                        }
                    }
                }
            }

            // オプションメニューの表示化
            foreach (GroupItem groupItem in this.menuItems)
            {
                int subItemCount = 0;
                foreach (SubItem subItem in groupItem.SubItems)
                {
                    int assemblyItemCount = 0;
                    foreach (AssemblyItem assemblyItem in subItem.AssemblyItems)
                    {
                        if (!IsOptionMenu(assemblyItem))
                        {
                            continue;
                        }

                        // オプションメニューの場合、表示
                        assemblyItemCount += 1;
                        assemblyItem.Disabled = false;
                    }

                    if (0 < assemblyItemCount)
                    {
                        subItemCount += 1;
                        subItem.Disabled = false;
                    }
                }

                if (0 < subItemCount)
                {
                    groupItem.Disabled = false;
                }
            }
        }

        /// <summary>指定されたAssemblyItemがオプション対象メニューか判定</summary>
        /// <param name="assemblyItem">アセンブリアイテム</param>
        /// <returns></returns>
        private bool IsOptionMenu(AssemblyItem assemblyItem)
        {
            if (assemblyItem == null
                || !assemblyItem.Disabled
                || assemblyItem.Name.ToLower() == "separator")
            {
                return false;
            }

            if (assemblyItem.Disabled
                && r_framework.FormManager.FormManager.IsEnabledForm(assemblyItem.FormID))
            {
                // 非表示指定かつ、オプション対象の場合は表示対象とする
                return true;
            }

            return false;
        }

        /// <summary>
        /// グループボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void splitButton_Click(object sender, EventArgs e)
        {
            // ドロップダウンメニューの開閉
            var button = (RibbonButton)sender;
            if (button.DropDownVisible)
            {
                button.CloseDropDown();
            }
            else
            {
                button.ShowDropDown();
            }
        }

        /// <summary>
        /// 業務系メニューのボタンを作成します。
        /// </summary>
        /// <param name="assemblyItem">AssemblyItem</param>
        /// <param name="subIndex">sub要素のインデックス</param>
        /// <param name="assemblyIndex">assembly要素のインデックス</param>
        /// <returns>作成したボタン</returns>
        private RibbonButton MakeTransactionButton(AssemblyItem assemblyItem, int subIndex, int assemblyIndex)
        {
            RibbonButton ribbonButton = new RibbonButton(assemblyItem.Name);
            ribbonButton.Tag = string.Format("{0}-{1}", subIndex, assemblyIndex);

            // ツールチップの設定
            if (string.IsNullOrWhiteSpace(assemblyItem.ToolTip))
            {
                ribbonButton.ToolTip = assemblyItem.Name;
            }
            else
            {
                ribbonButton.ToolTipTitle = assemblyItem.Name;
                ribbonButton.ToolTip = assemblyItem.ToolTip;
            }

            // No3953-->
            if (assemblyItem.Name == "ログアウト")
            {
                // ログアウトボタン
                ribbonButton.Click += this.LogOutButton_Click;
                ribbonButton.Text = "";
            }
            else if (assemblyItem.Name == "終了")
            {
                // 終了ボタン
                ribbonButton.Click += this.ExitButton_Click;
                ribbonButton.Text = "";
            }
            else if (assemblyItem.Name == "DownLoad")
            {
                // 終了ボタン
                ribbonButton.Click += this.DownloadButton_Click;
                ribbonButton.Text = "";
            }
            else
            {
                // ボタンクリックイベント追加
                ribbonButton.Click += new EventHandler(this.ButtonRibbonAssemblyItem_Click);
            }
            // No3953<--

            // リソースの存在確認とロード
            if (!string.IsNullOrWhiteSpace(assemblyItem.IconName))
            {
                ribbonButton.Image = this.CheckResource(assemblyItem.IconName, false);
                ribbonButton.SmallImage = this.CheckResource(assemblyItem.IconName, true);
            }

            // 最大サイズ指定
            if (assemblyItem.IconSize.ToLower() == "small")
            {
                ribbonButton.MaxSizeMode = RibbonElementSizeMode.Medium;
            }

            ribbonButton.MouseLeave += new MouseEventHandler(this.ribbonItem_MouseLeave);
            return ribbonButton;
        }

        /// <summary>
        /// リボンメニュー最小化時にタブメニューの1つ目のボタンを無効化
        /// （最小化時にOrbMenuを展開するとタブメニューのボタンクリックが反応するため）
        /// （RibbonのMinimizedプロパティをtrueにするとボタンがOrbタブに重なっているのが分かる）
        /// 暫定対応
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ribbonMenu_ExpandedChanged(object sender, EventArgs e)
        {
            var expanded = this.ribbonMenu.Expanded;
            foreach (var tab in this.ribbonMenu.Tabs)
            {
                if (tab.Panels.Count > 0)
                {
                    if (tab.Panels[0].Items.Count > 0)
                    {
                        tab.Panels[0].Items[0].Enabled = expanded;
                    }
                }
            }
            var button = new RibbonButton();
        }

        #region マスタメニューが閉じれない現象を防止

        // クローズイベントの後に数回連続でOpenイベントが発生するため、クローズ後、短い間に発生したオープンイベントをキャンセルします。
        private DateTime orbClosedTime = DateTime.Now; // OrbMenuを閉じた時間を保持
        /// <summary>
        /// OrbMenuクローズ後、短い間に発生したオープンイベントをキャンセルします。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrbDropDown_Opening(object sender, CancelEventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, this.orbClosedTime.AddMilliseconds(100)) <= 0)
            {
                e.Cancel = true;
                this.ribbonMenu.OrbPressed = false;
                this.ribbonMenu.OrbSelected = false;
                this.orbMenuOpened = false;
                return;
            }
            this.orbMenuOpened = true;
        }

        #endregion マスタメニューが閉じれない現象を防止

        /// <summary>
        /// ログアウトボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOutButton_Click(object sender, EventArgs e)
        {
            // 終了手続き
            FormManager.FormManager.CloseAllForm((Form)this.Parent);
        }

        // No3953-->
        /// <summary>
        /// 終了ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            // 終了手続き
            FormManager.FormManager.ExitShougunR((Form)this.Parent);
        }
        // No3953<--

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            // 終了手続き
            FormManager.FormManager.DownLoadShougunR((Form)this.Parent);
        }

        /// <summary>リボンメニューアイコンビットマップのキャッシュ用辞書構造。静的データメンバ
        /// 一度作成したBitmapはプロセス終了までキャッシュに保持して使いまわす。
        /// </summary>
        static private Dictionary<string, Bitmap> iconBitmapCache = new Dictionary<string, Bitmap>();

        /// <summary>埋め込みリソースから指定された名前のアイコン用Bitmapを取得する</summary>
        /// <param name="iconName">アイコン名(リソース名の元になる）</param>
        /// <param name="isSmallIcon">小アイコンフラグ(小:16px,大:32px)</param>
        /// <returns>取得したBitmapを返却する。存在しない場合はnullを返却。
        private Bitmap CheckResource(string iconName, bool isSmallIcon)
        {
            Bitmap iconBitmap = null;
            if (!string.IsNullOrEmpty(iconName))
            {
                // リソース名は例えばアイコン名が"M03kaisya_07"で小アイコンなら
                // "r_framework.MenuIcon.M03kaisya.M03kaisya_07_16.png"
                iconName = string.Format("{0}_{1}.png", iconName, (isSmallIcon ? 16 : 32));

                // キャッシュに既にあればキャッシュから取り出す
                if (!RibbonMainMenu.iconBitmapCache.TryGetValue(iconName, out iconBitmap))
                {
                    // キャッシュになければ埋め込みリソースから読み込みキャッシュに追加する
                    var resourceName = string.Format("r_framework.MenuIcon.{0}.{1}",
                        iconName.Remove(iconName.IndexOf('_')), iconName);

                    var assembly = Assembly.GetExecutingAssembly();
                    using (var stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        if (stream != null)
                        {
                            iconBitmap = new Bitmap(stream);
                        }
                    }
                    RibbonMainMenu.iconBitmapCache.Add(iconName, iconBitmap);
                }
            }
            return iconBitmap;
        }

        /// <summary>フォームがロードされる場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // メニュー構成用XMLファイルのロード処理を実行する
            this.LoadMenuItem();

            // 20150610 マイメニュー追加 Start
            this.SetBookmarkMenuItem();
            // 20150610 マイメニュー追加 End

            // オプションメニューの表示化
            this.DisplayOptionMenu();

            // メニューコントロールの作成処理を実行する
            this.MakeMenuCtrl();

            // マスターアセンブリボタン項目表示
            //this.DisplayMasterAssemblyItems(0);

            // フォーマット情報を登録
            SystemProperty.SetCurrentFormat(this.GlobalCommonInformation.SysInfo);

            this.Parent.SizeChanged += new System.EventHandler(this.Parent_SizeChanged);
        }

        /// <summary>
        /// フォーカスを受け取ったらフォーカスボックスにフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonMainMenu_GotFocus(object sender, EventArgs e)
        {
            this.FocusBox.Focus();
        }

        /// <summary>ＴＡＢが選択された場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void TabRibbon_Select(object sender, EventArgs e)
        {
            RibbonTab ribbonTab = (RibbonTab)sender;
            this.indexSelectTab = (int)ribbonTab.Tag;
        }

        /// <summary>リボンボタンクリック</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void ButtonRibbonAssemblyItem_Click(object sender, EventArgs e)
        {
            this.RibbonButton_Click(this.indexSelectTab, (RibbonButton)sender);
        }

        /// <summary>
        /// OrbMenu（マスタメニュー）のボタンをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonDescriptionMenuItem_Click(object sender, EventArgs e)
        {
            this.RibbonButton_Click(this.indexMasterTab, (RibbonButton)sender);
        }

        /// <summary>
        /// リボンボタンクリック処理
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <param name="ribbonButton"></param>
        private void RibbonButton_Click(int tabIndex, RibbonButton ribbonButton)
        {
            LogUtility.DebugMethodStart(tabIndex, ribbonButton);

            // カーソルを待機状態に変更
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string tmp = (string)ribbonButton.Tag;
                string[] split = tmp.Split('-');
                int groupNo = int.Parse(split[0]);
                int assemblyNo = int.Parse(split[1]);

                GroupItem groupItem = (GroupItem)this.menuItems[tabIndex];
                SubItem subItem = (SubItem)groupItem.SubItems[groupNo];
                AssemblyItem assemblyItem = (AssemblyItem)subItem.AssemblyItems[assemblyNo];

                // 利用区分チェック
                if (!this.CheckUseKbn(assemblyItem.FormID))
                {
                    var messageBoxShowLogic = new MessageBoxShowLogic();
                    messageBoxShowLogic.MessageBoxShow(useKbnErrorCd, assemblyItem.Name);
                    return;
                }

                //Cammunicate InxsSubApplication Start
                if (assemblyItem.FormID.StartsWith("S"))
                {
                    FormManager.FormManager.OpenFormSubApp(assemblyItem.FormID, assemblyItem.WindowType, FormManager.FormManager.CALLED_MENU);
                    return;
                }
                //Cammunicate InxsSubApplication End

                // 起動時権限チェックはFormManagerへ
                // メニュー権限チェック
                //if (!assemblyItem.UserAuth.HasFlag(AuthMethodFlag.Read))
                //{
                //    LogUtility.Info(string.Format("社員CD:[{0}]にFormID:[{1}]への参照権限がありませんでした。",
                //        SystemProperty.Shain.CD, assemblyItem.FormID));
                //    var messageBoxShowLogic = new MessageBoxShowLogic();
                //    messageBoxShowLogic.MessageBoxShow(kengenErrorCd, assemblyItem.Name);
                //    return;
                //}

                // 存在チェック
                var assemblyPath = Path.GetFullPath(assemblyItem.AssemblyName);
                if (!File.Exists(assemblyPath))
                {
                    LogUtility.Fatal(string.Format("ファイル '{0}' が見つかりませんでした。", assemblyPath));
                    var messageBoxShowLogic = new MessageBoxShowLogic();
                    messageBoxShowLogic.MessageBoxShow(notFoundErrorCd, assemblyItem.Name);
                    return;
                }

                if (assemblyItem.FormID.StartsWith("R"))
                {
                    // 汎用帳票呼出
                    WINDOW_ID windowId = WINDOW_ID.NONE;
                    windowId = (WINDOW_ID)assemblyItem.WindowID;
                    object[] assemblyArgs = new object[] { windowId, FormManager.FormManager.CALLED_MENU };

                    if (WINDOW_TYPE.NONE == assemblyItem.WindowType)
                    {
                        FormManager.FormManager.OpenDialog(assemblyItem.FormID, assemblyArgs);
                    }
                    else
                    {
                        // 権限チェック有
                        // 汎用帳票時のみ「FormID + WindowID」で権限確認
                        FormManager.FormManager.OpenDialogWithAuth(assemblyItem.FormID, windowId, assemblyItem.WindowType, assemblyArgs);
                    }
                }
                #region 休動メニューの暫定対応箇所
                /*// 不具合管理表No.3156対応
                // TODO:休動関係の製造完了次第、下記条件は廃止
                else if (assemblyItem.FormID.Equals("G202") || assemblyItem.FormID.Equals("G203") || assemblyItem.FormID.Equals("G208"))
                {
                    MessageBox.Show("本メニューはオプション機能です。");
                }*/
                #endregion
                else if (assemblyItem.FormType == this.dialog)
                {
                    // ダイアログ画面呼出

                    if (WINDOW_TYPE.NONE == assemblyItem.WindowType)
                    {
                        FormManager.FormManager.OpenDialog(assemblyItem.FormID, FormManager.FormManager.CALLED_MENU);
                    }
                    else
                    {
                        // 権限チェック有
                        FormManager.FormManager.OpenDialogWithAuth(assemblyItem.FormID, assemblyItem.WindowType, FormManager.FormManager.CALLED_MENU);
                    }
                }
                else if (assemblyItem.FormType == this.modal)
                {
                    // モーダル画面呼出

                    if (WINDOW_TYPE.NONE == assemblyItem.WindowType)
                    {
                        FormManager.FormManager.OpenFormModal(assemblyItem.FormID, FormManager.FormManager.CALLED_MENU);
                    }
                    else
                    {
                        // 権限チェック有
                        FormManager.FormManager.OpenFormModalWithAuth(assemblyItem.FormID, assemblyItem.WindowType, FormManager.FormManager.CALLED_MENU);
                    }
                }
                else
                {
                    // 通常の画面呼出

                    if (WINDOW_TYPE.NONE == assemblyItem.WindowType)
                    {
                        FormManager.FormManager.OpenForm(assemblyItem.FormID, FormManager.FormManager.CALLED_MENU);
                    }
                    else
                    {
                        // 権限チェック有
                        FormManager.FormManager.OpenFormWithAuth(assemblyItem.FormID, assemblyItem.WindowType, FormManager.FormManager.CALLED_MENU);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                // カーソルを元に戻す
                Cursor.Current = Cursors.Default;
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// システム設定の利用区分を調べます
        /// </summary>
        /// <param name="formID">フォームID</param>
        /// <returns>Treu:使用する, false:使用しない</returns>
        private bool CheckUseKbn(string formID)
        {
            bool result = true;

            switch (formID)
            {
                case "G059":
                    result = (this.GlobalCommonInformation.SysInfo.URIAGE_KAKUTEI_USE_KBN != 2).IsTrue;
                    break;
                case "G068":
                    result = (this.GlobalCommonInformation.SysInfo.SHIHARAI_KAKUTEI_USE_KBN != 2).IsTrue;
                    break;
                // 実績売上支払確定は関係なし（売上支払確定画面はなし）
                //case "G330":
                //    result = (this.GlobalCommonInformation.SysInfo.UR_SH_KAKUTEI_USE_KBN != 2).IsTrue;
                //    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// 親フォームのサイズ変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parent_SizeChanged(object sender, EventArgs e)
        {
            this.ResizeRibbon();
        }

        /// <summary>
        /// 再描画要求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Invalidated(object sender, InvalidateEventArgs e)
        {
            this.ResizeRibbon();
        }

        /// <summary>
        /// 親フォームのサイズに合わせてサイズを変更
        /// </summary>
        private void ResizeRibbon()
        {
            int frameWidth = 0;
            int ribbonHeight = this.ribbonMenu.Expanded ? 96 : 25;

            // 枠のスタイルによって枠幅が異なる
            switch (((Form)this.Parent).FormBorderStyle)
            {
                case System.Windows.Forms.FormBorderStyle.Sizable:
                case System.Windows.Forms.FormBorderStyle.SizableToolWindow:
                    frameWidth = SystemInformation.FrameBorderSize.Width * 2;
                    break;
                case System.Windows.Forms.FormBorderStyle.FixedDialog:
                case System.Windows.Forms.FormBorderStyle.FixedSingle:
                case System.Windows.Forms.FormBorderStyle.FixedToolWindow:
                    frameWidth = SystemInformation.FixedFrameBorderSize.Width * 2;
                    break;
                case System.Windows.Forms.FormBorderStyle.Fixed3D:
                    frameWidth = (SystemInformation.FixedFrameBorderSize.Width + SystemInformation.Border3DSize.Width) * 2;
                    break;
                default:
                    break;
            }

            this.Width = this.Parent.Width;
            this.ClientSize = new System.Drawing.Size(this.Parent.Width - frameWidth, ribbonHeight);
            this.ribbonMenu.Size = this.ClientSize;
        }

        /// <summary>
        /// 検索ボタンがクリックされた場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void seachButton_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        /// <summary>
        /// キー押下イベント
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (this.FocusBox.Focused && e.KeyCode == Keys.Enter)
            {
                this.FocusBox_KeyDown(null, e);
                e.Handled = true;
            }
            else if (this.txb_quickSearch.Focused && e.KeyCode == Keys.Enter)
            {
                // テキストボックスにフォーカスがある状態でエンターキーを押下
                this.Search();
                e.Handled = true;
            }
            else
            {
                // それ以外
                base.OnKeyDown(e);
            }
        }

        /// <summary>
        /// 検索結果一覧を呼び出す
        /// </summary>
        private void Search()
        {
            LogUtility.DebugMethodStart();

            try
            {
                string searchString = this.txb_quickSearch.Text.Trim();
                if (!string.IsNullOrEmpty(searchString))
                {
                    // カーソルを待機状態に変更
                    Cursor.Current = Cursors.WaitCursor;

                    object[] args = new object[] { searchString, FormManager.FormManager.CALLED_MENU };
                    // 検索結果一覧を呼び出し
                    FormManager.FormManager.OpenForm(this.kensakukekkaIchiran, args);
                }
                else
                {
                    this.txb_quickSearch.Focus();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                // カーソルを元に戻す
                Cursor.Current = Cursors.Default;

                LogUtility.DebugMethodEnd();
            }
        }

        #endregion - Methods -

        #region - キー操作対応 -
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FocusBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.selectedButton == null)
            {
                #region タブに疑似フォーカス

                // マウスによってマスタメニューが展開されていた場合は閉じる
                if (this.orbMenuOpened)
                {
                    this.ribbonMenu.OrbDropDown.Close();
                    return;
                }

                switch (e.KeyCode)
                {
                    case Keys.Right:
                        this.ChangeSelectTab(true);
                        break;
                    case Keys.Left:
                        this.ChangeSelectTab(false);
                        break;
                    case Keys.Down:
                    case Keys.Enter:
                        var button = (RibbonButton)this.ribbonMenu.ActiveTab.Panels.SelectMany(n => n.Items).FirstOrDefault(n => n is RibbonButton);
                        this.SetSelectedButton(button, true);
                        this.ribbonMenu.ActiveTab.SetSelected(false);
                        this.ribbonMenu.Refresh();
                        break;
                    case Keys.Z:
                        this.OpenOrbDropDown();
                        break;
                    default:
                        var index = this.ConvertKeyValueToIndex(e.KeyValue);
                        var tab = this.ribbonMenu.Tabs.Where(n => n.Visible).ElementAtOrDefault(index);
                        if (tab != null)
                        {
                            this.ribbonMenu.ActiveTab = tab;
                        }
                        break;
                }

                #endregion
            }
            else if (this.selectedButton is RibbonOrbMenuItem && !this.selectedButton.DropDownVisible)
            {
                #region マスタメニューに疑似フォーカス

                switch (e.KeyCode)
                {
                    case Keys.Right:
                        this.OpenDropDown(this.selectedButton);
                        break;
                    case Keys.Up:
                        this.ChangeSelectOrbDropDown(this.selectedButton, false);
                        break;
                    case Keys.Down:
                        this.ChangeSelectOrbDropDown(this.selectedButton, true);
                        break;
                    case Keys.Space:
                    case Keys.Enter:
                        this.OpenDropDown(this.selectedButton);
                        break;
                    case Keys.Z:
                        // マスタメニュークローズ
                        this.selectedButton.CloseDropDown();
                        this.ribbonMenu.OrbDropDown.Close();
                        break;
                    default:
                        this.SelectForKey(e.KeyValue, this.ribbonMenu.OrbDropDown.MenuItems.Where(n => n is RibbonButton));
                        break;
                }

                #endregion
            }
            else if (this.selectedButton is RibbonOrbMenuItem && this.selectedButton.DropDownVisible)
            {
                #region マスタメニューアイテムに疑似フォーカス

                switch (e.KeyCode)
                {
                    case Keys.Left:
                        this.SetSelectedButton(this.selectedButton, true);
                        return;
                    case Keys.Up:
                        this.ChangeSelectDropDown(this.selectedButton, false);
                        return;
                    case Keys.Down:
                        this.ChangeSelectDropDown(this.selectedButton, true);
                        return;
                    case Keys.Space:
                    case Keys.Enter:
                        // エンターキー押下でボタンクリック処理
                        if (this.selectedButton.DropDownItems.FirstOrDefault(n => n.Selected) != null)
                        {
                            // TODO：画面呼出先でKeyUpイベントが発生する
                            ((RibbonButton)this.selectedButton.DropDownItems.FirstOrDefault(n => n.Selected)).PerformClick();
                        }
                        this.ResetSelectedButton();
                        return;
                    case Keys.Z:
                        // マスタメニュークローズ
                        this.selectedButton.CloseDropDown();
                        this.ribbonMenu.OrbDropDown.Close();
                        return;
                    default:
                        this.SelectForKey(e.KeyValue, this.selectedButton.DropDownItems.Where(n => n is RibbonButton));
                        break;
                }

                #endregion
            }
            else if (this.selectedButton.DropDownVisible)
            {
                #region グループボタンに疑似フォーカス

                switch (e.KeyCode)
                {
                    case Keys.Left:
                        this.selectedButton.CloseDropDown();
                        this.ChangeSelectButton(this.selectedButton, false);
                        break;
                    case Keys.Right:
                        this.selectedButton.CloseDropDown();
                        this.ChangeSelectButton(this.selectedButton, true);
                        break;
                    case Keys.Up:
                        this.ChangeSelectDropDown(this.selectedButton, false);
                        break;
                    case Keys.Down:
                        this.ChangeSelectDropDown(this.selectedButton, true);
                        break;
                    case Keys.Space:
                    case Keys.Enter:
                        // エンターキー押下でボタンクリック処理
                        if (this.selectedButton.DropDownItems.FirstOrDefault(n => n.Selected) != null)
                        {
                            // TODO：画面呼出先でKeyUpイベントが発生する
                            ((RibbonButton)this.selectedButton.DropDownItems.FirstOrDefault(n => n.Selected)).PerformClick();
                        }
                        this.ResetSelectedButton();
                        break;
                    case Keys.Z:
                        this.selectedButton.CloseDropDown();
                        this.OpenOrbDropDown();
                        break;
                    default:
                        // 選択グループボタン中に入力に対応するボタンがあるか調べる
                        this.SelectForKey(e.KeyValue, this.selectedButton.DropDownItems.Where(n => n is RibbonButton));
                        break;
                }

                #endregion
            }
            else if (!this.selectedButton.DropDownVisible)
            {
                #region パネル（ボタン）に疑似フォーカス

                switch (e.KeyCode)
                {
                    case Keys.Right:
                        this.selectedButton.CloseDropDown();
                        this.ChangeSelectButton(this.selectedButton, true);
                        break;
                    case Keys.Left:
                        this.selectedButton.CloseDropDown();
                        this.ChangeSelectButton(this.selectedButton, false);
                        break;
                    case Keys.Up:
                        if (this.selectedButton.DropDownVisible)
                        {
                            this.ChangeSelectDropDown(this.selectedButton, false);
                        }
                        else
                        {
                            this.ResetSelectedButton();
                        }
                        break;
                    case Keys.Down:
                        this.OpenDropDown(this.selectedButton);
                        break;
                    case Keys.Space:
                    case Keys.Enter:
                        // エンターキー押下でボタンクリック処理
                        if (this.selectedButton.DropDownItems.Count(n => n is RibbonButton) > 0)
                        {
                            // ドロップダウンアイテムを持ってる場合はドロップダウン展開
                            this.OpenDropDown(this.selectedButton);
                        }
                        else
                        {
                            // TODO：画面呼出先でKeyUpイベントが発生する
                            this.selectedButton.PerformClick();
                        }
                        break;
                    case Keys.Z:
                        this.selectedButton.CloseDropDown();
                        this.OpenOrbDropDown();
                        break;
                    default:
                        // 選択タブ中に入力に対応するボタンがあるか調べる
                        this.SelectForKey(e.KeyValue, this.ribbonMenu.ActiveTab.Panels.SelectMany(n => n.Items));
                        break;
                }

                #endregion
            }
        }

        /// <summary>
        /// タブの選択を遷移する
        /// </summary>
        /// <param name="isNext">true:次, false:前</param>
        private void ChangeSelectTab(bool isNext)
        {
            int nextIndex = this.ribbonMenu.Tabs.IndexOf(this.ribbonMenu.ActiveTab);
            int increment = isNext ? 1 : -1;
            int cnt = this.ribbonMenu.Tabs.Count;

            for (int i = 0; i < cnt; i++)
            {
                nextIndex += increment;
                if (nextIndex < 0)
                {
                    // 0より小さくなった場合は最後のインデックス
                    nextIndex = cnt - 1;
                }
                else if (nextIndex > cnt - 1)
                {
                    // 最後のインデックスより大きくなった場合は0
                    nextIndex = 0;
                }

                if (this.ribbonMenu.Tabs[nextIndex].Visible)
                {
                    // 表示されているタブが見つかれば終了
                    break;
                }
            }

            this.ribbonMenu.ActiveTab = this.ribbonMenu.Tabs[nextIndex];
        }

        /// <summary>
        /// タブ下のボタン選択を遷移する
        /// </summary>
        /// <param name="button">現在選択中のボタン</param>
        /// <param name="isNext">true:次, false:前</param>
        private void ChangeSelectButton(RibbonButton button, bool isNext)
        {
            int nextIndex = int.Parse(button.Value);
            int cnt = int.Parse(button.OwnerPanel.OwnerTab.Value); // タブのバリュー値には保有するボタンの数が格納されている
            int increment = isNext ? 1 : -1;

            nextIndex += increment;
            if (nextIndex < 0)
            {
                // 0より小さくなった場合は最後のインデックス
                nextIndex = cnt - 1;
            }
            else if (nextIndex > cnt - 1)
            {
                // 最後のインデックスより大きくなった場合は0
                nextIndex = 0;
            }

            // ※Valueプロパティはボタンにしか登録されていないので、チェックは不要
            var nextButton = (RibbonButton)this.ribbonMenu.ActiveTab.Panels.SelectMany(n => n.Items).FirstOrDefault(n => n.Value == nextIndex.ToString());

            this.SetSelectedButton(this.selectedButton, false);
            this.SetSelectedButton(nextButton, true);
        }

        /// <summary>
        /// グループボタン下のボタン選択を遷移する
        /// 未展開の場合は展開して最初のボタンを選択する
        /// </summary>
        /// <param name="button">選択中のボタン</param>
        /// <param name="isNext">true:次, false:前</param>
        private void ChangeSelectDropDown(RibbonButton button, bool isNext)
        {
            int cnt = button.DropDownItems.Count(n => n is RibbonButton);
            int increment = isNext ? 1 : -1;
            int selectedIndex = button.DropDownItems.FindIndex(n => n.Selected && n is RibbonButton);
            selectedIndex = selectedIndex < 0 ? 0 : selectedIndex; // 負の場合は0にする

            int nextIndex = selectedIndex;

            for (int i = 0; i < cnt; i++)
            {
                nextIndex += increment;
                if (nextIndex < 0)
                {
                    // 0より小さくなった場合は最後のインデックス
                    nextIndex = cnt - 1;
                }
                else if (nextIndex > cnt - 1)
                {
                    // 最後のインデックスより大きくなった場合は0
                    nextIndex = 0;
                }

                if (button.DropDownItems[nextIndex] is RibbonButton)
                {
                    // ボタンなら終了
                    break;
                }

                if (i == cnt - 1)
                {
                    // ボタンなし
                    return;
                }
            }

            if (button.OwnerTab != null)
            {
                // 業務タブの場合は親ボタンの左上隅にカーソル移動
                var buttonTopLeft = new Point(button.ButtonFaceBounds.X - 1, button.ButtonFaceBounds.Y - 1);
                Cursor.Position = this.PointToScreen(buttonTopLeft);
            }
            else
            {
                // マスタタブの場合はマスタタブの左上隅にカーソル移動
                var buttonTopLeft = new Point(this.ribbonMenu.OrbBounds.X - 1, this.ribbonMenu.OrbBounds.Y - 1);
                Cursor.Position = this.PointToScreen(buttonTopLeft);

                // ポイント移動だとLeaveイベントが発生しないので、直接ドロップダウンを閉じる
                foreach (var item in this.ribbonMenu.OrbDropDown.MenuItems.Where(n => !n.Equals(this.selectedButton)))
                {
                    ((RibbonButton)item).CloseDropDown();
                    item.SetSelected(false);
                    item.RedrawItem();
                }
            }

            // 古いボタンの選択解除
            button.DropDownItems[selectedIndex].SetSelected(false);
            button.DropDownItems[selectedIndex].RedrawItem();

            var nextButton = (RibbonButton)button.DropDownItems[nextIndex];

            // 新しいボタンを選択
            nextButton.SetSelected(true);
            nextButton.RedrawItem();

            for (int i = 0; !button.DropDown.Bounds.Contains(nextButton.Bounds) && i < button.DropDownItems.Count; i++)
            {
                if (selectedIndex < nextIndex)
                {
                    button.DropDown.ScrollDown();
                }
                else
                {
                    button.DropDown.ScrollUp();
                }
            }
        }

        /// <summary>
        /// マスタメニューのボタン選択を遷移させます
        /// </summary>
        /// <param name="button">現在の選択ボタン</param>
        /// <param name="isNext">true:次, false:前</param>
        private void ChangeSelectOrbDropDown(RibbonItem button, bool isNext)
        {
            int increment = isNext ? 1 : -1;
            int nextIndex = this.ribbonMenu.OrbDropDown.MenuItems.FindIndex(n => n.Equals(button));
            int cnt = this.ribbonMenu.OrbDropDown.MenuItems.Count;

            nextIndex += increment;
            if (nextIndex < 0)
            {
                // 0より小さくなった場合は最後のインデックス
                nextIndex = cnt - 1;
            }
            else if (nextIndex > cnt - 1)
            {
                // 最後のインデックスより大きくなった場合は0
                nextIndex = 0;
            }

            // マスタタブの左上隅にカーソル移動
            var buttonTopLeft = new Point(this.ribbonMenu.OrbBounds.X - 1, this.ribbonMenu.OrbBounds.Y - 1);
            Cursor.Position = this.PointToScreen(buttonTopLeft);

            // 一旦全てのドロップダウンを閉じ、選択を解除する
            foreach (var item in this.ribbonMenu.OrbDropDown.MenuItems)
            {
                ((RibbonButton)item).CloseDropDown();
                item.SetSelected(false);
                item.RedrawItem();
            }

            this.SetSelectedButton((RibbonButton)this.ribbonMenu.OrbDropDown.MenuItems[nextIndex], true);
        }

        /// <summary>
        /// 指定したボタンが持つドロップダウンリストを展開します
        /// ドロップダウンアイテムを持っていない場合は何もせず返します
        /// </summary>
        /// <param name="button">ボタン</param>
        /// 
        private void OpenDropDown(RibbonButton button)
        {
            int cnt = button.DropDownItems.Count(n => n is RibbonButton);
            if (cnt == 0)
            {
                // ボタンを持っていない場合は何もせず返す
                return;
            }

            if (button.OwnerTab != null)
            {
                // 業務タブの場合は親ボタンの左上隅にカーソル移動
                var buttonTopLeft = new Point(button.ButtonFaceBounds.X - 1, button.ButtonFaceBounds.Y - 1);
                Cursor.Position = this.PointToScreen(buttonTopLeft);
            }
            else
            {
                // マスタタブの場合はマスタタブの左上隅にカーソル移動
                var buttonTopLeft = new Point(this.ribbonMenu.OrbBounds.X - 1, this.ribbonMenu.OrbBounds.Y - 1);
                Cursor.Position = this.PointToScreen(buttonTopLeft);
            }

            // 展開して最初のボタンを選択状態にする
            button.ShowDropDown();
            RibbonItem item = button.DropDownItems.FirstOrDefault(n => !(n is RibbonSeparator));
            item.SetSelected(true);
            item.RedrawItem();
            return;
        }

        /// <summary>
        /// リボンメニューのボタンの選択状態をセットします。
        /// 単なる選択状態の操作ははRibbonItem.SetSelected(bool)とRibbonItem.RedrawItem()を使用する。
        /// </summary>
        /// <param name="button">対象のボタン</param>
        /// <param name="select">true:選択, fasle:選択解除</param>
        private void SetSelectedButton(RibbonButton button, bool select)
        {
            if (button == null)
            {
                // 何もしない
                return;
            }

            // 新規選択時はドロップダウンを一度閉じる
            button.CloseDropDown();

            if (select)
            {
                if (this.selectedButton != null)
                {
                    // 前回選択ボタンのドロップダウンを閉じる
                    this.selectedButton.SetSelected(false);
                    this.selectedButton.RedrawItem();
                }

                // 選択ボタンに登録
                this.selectedButton = button;
            }

            // 選択状態にする
            if (button.SizeMode == RibbonElementSizeMode.DropDown)
            {
                // ドロップダウンアイテムは選択モード(押下モードがないため)
                button.SetSelected(select);
            }
            else
            {
                // 通常のボタンは押下モード
                button.SetPressed(select);
            }
            button.RedrawItem();
        }

        /// <summary>
        /// RibbonItem.Valueをキー文字に置き換えます
        /// ※未完成
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertButtonValueToKeyString(string value)
        {
            int num = 0;
            if (!int.TryParse(value, out num))
            {
                return string.Empty;
            }

            if (num < 10)
            {
                num += 48;
            }
            else if (num >= 10 && num < 22)
            {
                num += 55;
            }
            else if (num >= 22 && num < 35)
            {
                num += 56;
            }
            else
            {
                return string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// キーバリューをボタンバリューに変換します
        /// </summary>
        /// <param name="keyValue">キーバリュー</param>
        /// <returns>ボタンバリュー</returns>
        private int ConvertKeyValueToIndex(int keyValue)
        {
            LogUtility.DebugMethodStart(keyValue);

            int result = -1;

            if (keyValue >= 48 && keyValue < 58)
            {
                // 0 ～ 9 を 0 ～ 9 に変換する
                result = keyValue - 48;
            }
            else if (keyValue >= 65 && keyValue < 89)
            {
                // A ～ Y を 10 ～ 34 に変換する
                result = keyValue - 55;
            }
            else if (keyValue >= 96 && keyValue < 106)
            {
                // T0 ～ T9 を 0 ～ 9 に変換する
                result = keyValue - 96;
            }

            LogUtility.DebugMethodEnd(result);

            return result;
        }

        /// <summary>
        /// キー入力による対応ボタンの呼出
        /// </summary>
        /// <param name="keyValue">キーバリュー</param>
        /// <param name="target">対象のリボンアイテムコレクション</param>
        /// <returns>true:対応するボタン呼出成功, false:対応するボタン無</returns>
        private bool SelectForKey(int keyValue, IEnumerable<RibbonItem> target)
        {
            if (target == null)
            {
                return false;
            }

            var commandButton = (RibbonButton)target.FirstOrDefault(n => n.Value == this.ConvertKeyValueToIndex(keyValue).ToString());
            if (commandButton != null)
            {
                this.SetSelectedButton(commandButton, true);
                if (commandButton.DropDownItems.Count == 0)
                {
                    // 単なるボタンの場合はクリック処理
                    commandButton.OnClick(EventArgs.Empty);
                }
                else
                {
                    // ドロップダウンアイテムを持っている場合はドロップダウンを開く
                    this.OpenDropDown(commandButton);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// ボタン選択状態を解除します
        /// </summary>
        protected void ResetSelectedButton()
        {
            this.SetSelectedButton(this.selectedButton, false);
            this.selectedButton = null;
            foreach (var tab in this.ribbonMenu.Tabs)
            {
                tab.SetSelected(false);
            }

            // リボンにフォーカスがある場合はアクティブタブを選択状態にする
            if (this.FocusBox.Focused)
            {
                this.ribbonMenu.ActiveTab.SetSelected(true);
            }

            // リボンの表示を更新
            this.ribbonMenu.Refresh();
        }

        /// <summary>
        /// タブの選択状態をリセットします
        /// </summary>
        private void TabFocusReset()
        {
            if (this.FocusBox.Focused && this.selectedButton == null && !this.ribbonMenu.ActiveTab.Selected)
            {
                this.ribbonMenu.ActiveTab.SetSelected(true);
                this.ribbonMenu.Refresh();
            }
        }

        /// <summary>
        /// リボンがフォーカスを遺失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FocusBox_LostFocus(object sender, EventArgs e)
        {
            // 選択状態を解除して初期化
            this.ResetSelectedButton();

            // タブフォントを元に戻す
            this.ribbonMenu.RibbonTabFont = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ribbonMenu.TabsPadding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ribbonMenu.Refresh();
        }

        /// <summary>
        /// リボンがフォーカスを取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FocusBox_GotFocus(object sender, EventArgs e)
        {
            // 選択状態を解除して初期化
            this.ResetSelectedButton();

            // タブフォントを太文字にする
            this.ribbonMenu.RibbonTabFont = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ribbonMenu.TabsPadding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ribbonMenu.Refresh();
        }

        /// <summary>
        /// マウスがボタンの上に
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonItem_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = (RibbonButton)sender;
            if (button.Bounds.Contains(e.Location) && button.DropDown != null)
            {
                // 既に開いているドロップダウンがあった場合に、そのドロップダウンの所有者がnullになり削除できなくなってしまうので、その前に一旦閉じる
                ((RibbonButton)sender).CloseDropDown();
            }
        }

        /// <summary>
        /// マウスがボタンから離れた場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender.Equals(this.selectedButton))
            {
                // ボタンが離れると自動的に選択が解除されてしまうので、再度選択状態にする
                this.SetSelectedButton(this.selectedButton, true);
            }

            return;
        }

        /// <summary>
        /// マスタメニューが閉じた場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OrbDropDown_Closed(object sender, EventArgs e)
        {
            // マスタメニューを閉じた時間を登録
            this.orbClosedTime = DateTime.Now;

            // 選択ボタンをnullで初期化する
            this.ResetSelectedButton();
            this.orbMenuOpened = false;
            foreach (var item in this.ribbonMenu.OrbDropDown.MenuItems.Where(n => n is RibbonOrbMenuItem))
            {
                // ドロップダウンを閉じる
                ((RibbonButton)item).CloseDropDown();
            }
        }

        /// <summary>
        /// タブチェンジ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ribbonMenu_ActiveTabChanged(object sender, EventArgs e)
        {
            // タブチェンジでボタンの選択状態を解除する
            this.ResetSelectedButton();
        }

        /// <summary>
        /// タブからマウスが離れた場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonTab_MouseLeave(object sender, MouseEventArgs e)
        {
            this.TabFocusReset();
        }
        #endregion

        #region マスタメニュー
        /// <summary>
        /// マスタメニュー展開
        /// </summary>
        private void OpenOrbDropDown()
        {
            var buttonTopLeft = new Point(this.ribbonMenu.OrbBounds.X - 1, this.ribbonMenu.OrbBounds.Y - 1);
            Cursor.Position = this.PointToScreen(buttonTopLeft);
            this.ribbonMenu.ShowOrbDropDown();
            this.SetSelectedButton((RibbonButton)this.ribbonMenu.OrbDropDown.MenuItems.FirstOrDefault(), true);
            this.ribbonMenu.ActiveTab.SetSelected(false);
            this.ribbonMenu.Refresh();
        }

        /// <summary>
        /// マスタメニュー閉じる
        /// </summary>
        public void CloseOrbDropDown()
        {
            if (this.orbMenuOpened)
            {
                this.ribbonMenu.OrbDropDown.Close();
            }
        }
        #endregion

        #region マイメニュー選択の設定
        // 20150610 マイメニュー選択を設定する Start
        /// <summary>
        /// お気に入り選択のサブタグをメニューに反映する
        /// </summary>
        /// <param name="bookmarkSubItem"></param>
        private void SetBookmarkMenuItem()
        {
            var bookmarkSubItem = this.menuItems.
                SelectMany(s => ((GroupItem)s).SubItems).
                Where(s => s.Name.Equals("マイメニュー")).
                FirstOrDefault();
            if (bookmarkSubItem == null || bookmarkSubItem.Disabled)
            {
                return;
            }

            // マイメニューに定義したメニューアイテムを取得
            var bookmarkAssemblyItems = new List<AssemblyItem>();
            foreach (GroupItem groupItem in this.menuItems)
            {
                foreach (SubItem subItem in groupItem.SubItems)
                {
                    foreach (AssemblyItem assemblyItem in subItem.AssemblyItems)
                    {
                        if (!assemblyItem.Disabled || this.IsOptionMenu(assemblyItem))
                        {
                            int dispIndexNo = SystemProperty.Shain.GetBookmark(
                                groupItem.IndexNo, subItem.IndexNo, assemblyItem.IndexNo, assemblyItem.FormID
                                );
                            if (dispIndexNo > 0)
                            {
                                var bookmarkAssemblyItem = this.AssemblyItemCopy(assemblyItem);
                                bookmarkAssemblyItem.IndexNo = dispIndexNo;

                                bookmarkAssemblyItems.Add(bookmarkAssemblyItem);
                            }
                        }
                    }
                }
            }
            // マイメニューアイテムをソート
            bookmarkAssemblyItems.Sort((s1, s2) => s1.IndexNo.CompareTo(s2.IndexNo));

            // マイメニューアイテムを設定
            var bookmarkAssemblyIconItem = bookmarkSubItem.AssemblyItems.
                Where(s => s.Name.Equals("マイメニュー")).
                FirstOrDefault();
            var bookmarkAssemblyIconIndex = bookmarkAssemblyIconItem == null ?
                0 : bookmarkSubItem.AssemblyItems.IndexOf(bookmarkAssemblyIconItem);

            bookmarkSubItem.AssemblyItems.InsertRange(bookmarkAssemblyIconIndex, bookmarkAssemblyItems);
            for (int i = 0; i < bookmarkSubItem.AssemblyItems.Count; i++)
            {
                bookmarkSubItem.AssemblyItems[i].IndexNo = i;

                if (i >= bookmarkAssemblyIconIndex && i < bookmarkAssemblyIconIndex + bookmarkAssemblyItems.Count)
                {
                    bookmarkSubItem.AssemblyItems[i].GroupName = "";
                    bookmarkSubItem.AssemblyItems[i].IconSize = "large";
                }
            }
        }
        // 20150610 マイメニュー選択を設定する End

        // 20141211 ブン マイメニュー選択を設定する End
        /// <summary>
        /// アセンブリアイテムをコピーする
        /// </summary>
        /// <param name="assemblyItem"></param>
        /// <returns></returns>
        private AssemblyItem AssemblyItemCopy(AssemblyItem assemblyItem)
        {
            AssemblyItem result = new AssemblyItem();

            if (assemblyItem != null)
            {
                result.Name = assemblyItem.Name;
                result.IndexNo = assemblyItem.IndexNo;
                result.IconName = assemblyItem.IconName;
                result.IconSize = assemblyItem.IconSize;
                result.ToolTip = assemblyItem.ToolTip;
                result.FormID = assemblyItem.FormID;
                result.WindowID = assemblyItem.WindowID;
                result.NameSpace = assemblyItem.NameSpace;
                result.AssemblyName = assemblyItem.AssemblyName;
                result.ClassName = assemblyItem.ClassName;
                result.GroupName = assemblyItem.GroupName;
                result.GroupIconName = assemblyItem.GroupIconName;
                result.GroupIconSize = assemblyItem.GroupIconSize;
                result.GroupToolTip = assemblyItem.GroupToolTip;
                result.FormType = assemblyItem.FormType;
                result.UserAuth = assemblyItem.UserAuth;
                result.WindowType = assemblyItem.WindowType;
            }

            return result;
        }
        // 20141211 マイメニュー選択を設定する end
        #endregion
    }

    #endregion - Class -
}
