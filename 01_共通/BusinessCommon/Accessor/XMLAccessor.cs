using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Shougun.Core.Common.BusinessCommon.Accessor
{
    /// <summary>
    /// XMLファイルにアクセスするためのクラス
    /// </summary>
    public class XMLAccessor
    {
        /// <summary>
        /// メソッド
        /// </summary>
        /// <returns>CurrentUserCustomConfigProfile</returns>
        public CurrentUserCustomConfigProfile XMLReader_CurrentUserCustomConfigProfile()
        {
            string xmlpath = r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlpath);
            
            XmlNode Root = xmldoc.SelectSingleNode("CurrentUserCustomConfigProfile");
            XmlNodeList AllXN1 = Root.ChildNodes;

            // ユーザカスタマ設定ファイル
            CurrentUserCustomConfigProfile configProfile = new CurrentUserCustomConfigProfile(); 

            // ノードに進入（ループ処理）
            foreach (XmlNode xn1 in AllXN1) { // Node : Setting
                
                XmlNodeList AllXN2 = xn1.ChildNodes; // Node : DefaultValue or PrinterDriver

                  foreach (XmlNode xn in AllXN2) // Node : DefaultValue or PrinterDriver
                  {
                      XmlNodeList defValList = xn.ChildNodes;
                      if ("DefaultValue".Equals(xn.Name)) // Node : DefaultValue
                      {
                          // 実行回数計算用
                          int i =  0;
                          foreach (XmlNode xn3 in defValList) // Node : ItemName
                          {
                              // 【注意】Ver1.19→Ver2.0にて部門CDを削除。
                              // そのためVer1.19から移行したユーザー環境の場合、インデンックス指定で取得すると差異が発生します
                              // 値を取得する場合は、名称指定で取得すること
                              i++;
                              if ("ItemSettings".Equals(xn3.Name))
                              {
                                  foreach (XmlNode item in xn3.ChildNodes)
                                  {
                                      if ("Name".Equals(item.Name))
                                      {
                                          switch (i)
                                          {
                                              case 1:
                                                  configProfile.ItemName1 = item.InnerText;
                                                  break;
                                          }
                                      }
                                      else if ("Value".Equals(item.Name))
                                      {
                                          switch (i)
                                          {
                                              case 1:
                                                  configProfile.ItemSetVal1 = item.InnerText;
                                                  break;
                                          }
                                      }
                                  }
                              }
                          }
                      }

                      if ("PrinterDriver".Equals(xn.Name))// Node:PrinterDriver
                      {
                          // 実行回数計算用
                          int j = 0;
                          foreach (XmlNode xn3 in defValList) 
                          {
                              j++;
                              if ("PrinterName".Equals(xn3.Name))// Node:PrinterName
                              {
                                  switch (j)
                                  {
                                      case 1:
                                          configProfile.PrinterName1 = xn3.InnerText;
                                          break;
                                      case 3:
                                          configProfile.PrinterName2 = xn3.InnerText;
                                          break;
                                  }
                              }

                              if ("PrintSettings".Equals(xn3.Name))// Node:PrintSettings
                              {
                                  XmlNodeList ValList = xn3.ChildNodes;
                                  switch (j)
                                  {
                                      case 2:
                                          configProfile.PrtSet_PaperSize1 = ((XmlNode)ValList[0]).InnerText;
                                          configProfile.PrtSet_Blank_Top1 = ((XmlNode)ValList[1]).InnerText;
                                          configProfile.PrtSet_Blank_Bottom1 = ((XmlNode)ValList[2]).InnerText;
                                          configProfile.PrtSet_Blank_Left1 = ((XmlNode)ValList[3]).InnerText;
                                          configProfile.PrtSet_Blank_Right1 = ((XmlNode)ValList[4]).InnerText;
                                          break;
                                      case 4:
                                          configProfile.PrtSet_PaperSize2 = ((XmlNode)ValList[0]).InnerText;
                                          configProfile.PrtSet_Blank_Top2 = ((XmlNode)ValList[1]).InnerText;
                                          configProfile.PrtSet_Blank_Bottom2 = ((XmlNode)ValList[2]).InnerText;
                                          configProfile.PrtSet_Blank_Left2 = ((XmlNode)ValList[3]).InnerText;
                                          configProfile.PrtSet_Blank_Right2 = ((XmlNode)ValList[4]).InnerText;
                                          break;
                                  }
                              }
                          }


                      }
                }
            }

            return configProfile;
        }
    }

    /// <summary>
    /// FileAccessするためのDTOクラス
    /// </summary>
    public class CurrentUserCustomConfigProfile
    {
        /// <summary>
        /// カスタム設定 :ItemName1
        /// </summary>
        public String ItemName1 { get; set; }
        /// <summary>
        /// カスタム設定 : ItemSetVal1
        /// </summary>
        public String ItemSetVal1 { get; set; }

        /// <summary>
        /// カスタム設定 : PrinterName1
        /// </summary>
        public String PrinterName1 { get; set; }
        /// <summary>
        /// カスタム設定 : PrtSet_PaperSize1
        /// </summary>
        public String PrtSet_PaperSize1 { get; set; }
        /// <summary>
        /// カスタム設定 : PrtSet_Blank_Top1
        /// </summary>
        public String PrtSet_Blank_Top1 { get; set; }
        /// <summary>
        /// カスタム設定 : PrtSet_Blank_Bottom1
        /// </summary>
        public String PrtSet_Blank_Bottom1 { get; set; }
        /// <summary>
        /// カスタム設定 : PrtSet_Blank_Left1
        /// </summary>
        public String PrtSet_Blank_Left1 { get; set; }
        /// <summary>
        /// カスタム設定 : PrtSet_Blank_Right1
        /// </summary>
        public String PrtSet_Blank_Right1 { get; set; }

        /// <summary>
        /// カスタム設定 : PrinterName2
        /// </summary>
        public String PrinterName2 { get; set; }
        /// <summary>
        /// カスタム設定 : PrtSet_PaperSize2
        /// </summary>
        public String PrtSet_PaperSize2 { get; set; }
        /// <summary>
        /// カスタム設定 : PrtSet_Blank_Top2
        /// </summary>
        public String PrtSet_Blank_Top2 { get; set; }
        /// <summary>
        /// カスタム設定 : PrtSet_Blank_Bottom2
        /// </summary>
        public String PrtSet_Blank_Bottom2 { get; set; }
        /// <summary>
        /// カスタム設定 : PrtSet_Blank_Left2
        /// </summary>
        public String PrtSet_Blank_Left2 { get; set; }
        /// <summary>
        /// カスタム設定 : PrtSet_Blank_Right1
        /// </summary>
        public String PrtSet_Blank_Right2 { get; set; }
    }
}
