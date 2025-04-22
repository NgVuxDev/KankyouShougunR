using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using r_framework.Dto;

namespace r_framework.Setting
{
    /// <summary>
    /// リボンメニューの生成クラス
    /// </summary>
    public class MenuSetting
    {
        /// <summary>
        /// XMLよりメニュー構成を取得する
        /// </summary>
        public RibbonTabSettingDto[] LoadMenuSetting()
        {
            List<RibbonTabSettingDto> loadClasses = new List<RibbonTabSettingDto>();

            try
            {
                // r-framework設定ファイルの読込
                XmlDocument config = new XmlDocument();
                config.Load("r-frameworkConfig.xml");
                DataSet ds = new DataSet();
                ds.ReadXml(new MemoryStream(ASCIIEncoding.UTF8.GetBytes(config.OuterXml)));
                string menuConfigPath = ds.Tables[0].Rows[0]["menuConfigPath"].ToString();

                // メニュー情報を読み込む
                XmlDocument menu = new XmlDocument();
                menu.Load(menuConfigPath);

                // グループアイテム処理
                foreach (XmlNode groupItemNode in menu.DocumentElement.ChildNodes)
                {
                    RibbonTabSettingDto dto = new RibbonTabSettingDto();
                    if (groupItemNode.NodeType == XmlNodeType.Element)
                    {
                        if (groupItemNode.LocalName.ToLower() != "group")
                        {   // グループタグでない
                            continue;
                        }

                        // タブ名を設定
                        dto.RibbonTabName = groupItemNode.Attributes["name"].Value;

                        // タブ内情報の設定
                        List<MenuButtonDto> list = new List<MenuButtonDto>();
                        foreach (XmlNode subItemNode in groupItemNode.ChildNodes)
                        {
                            foreach (XmlNode assemblyItemNode in subItemNode.ChildNodes)
                            {
                                if (assemblyItemNode.NodeType == XmlNodeType.Element)
                                {
                                    if (assemblyItemNode.LocalName.ToLower() != "assembly")
                                    {   // アセンブリタグでない
                                        continue;
                                    }

                                    if (assemblyItemNode.Attributes.Count != 0)
                                    {
                                        MenuButtonDto assemblyItem = new MenuButtonDto();
                                        foreach (XmlAttribute xmlAttribute in assemblyItemNode.Attributes)
                                        {
                                            switch (xmlAttribute.LocalName.ToLower())
                                            {
                                                case "index-no":    // インデックス番号
                                                    assemblyItem.windowId = int.Parse(string.Format("{0:D2}{1:D2}{2:D2}", int.Parse(groupItemNode.Attributes["index-no"].Value), int.Parse(subItemNode.Attributes["index-no"].Value), int.Parse(xmlAttribute.Value)));

                                                    break;
                                                case "name":        // ボタンテキスト
                                                    assemblyItem.buttonName = xmlAttribute.Value;

                                                    // テキストがseparatorの場合はスキップ
                                                    if (xmlAttribute.Value.Equals("separator"))
                                                    {
                                                        assemblyItem = null;
                                                    }

                                                    break;
                                                case "asemmbly-name":        // アセンブリー名
                                                    assemblyItem.assemblyName = xmlAttribute.Value;

                                                    break;
                                                default:

                                                    break;
                                            }

                                            if (assemblyItem == null)
                                            {
                                                break;
                                            }
                                        }
                                        if (assemblyItem != null)
                                        {
                                            list.Add(assemblyItem);
                                        }
                                    }
                                }
                            }
                        }
                        dto.MenuSettingDto = list.ToArray();

                    }
                    loadClasses.Add(dto);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(r_framework.Properties.Resources.MENU_FILE_NOT_FOUND, r_framework.Properties.Resources.MENU_FILE_NOT_FOUND_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return loadClasses.ToArray();
        }
    }
}
