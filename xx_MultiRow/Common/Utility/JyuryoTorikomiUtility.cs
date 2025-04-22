using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Shougun.Function.ShougunCSCommon.Const;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Windows.Forms;

namespace Shougun.Function.ShougunCSCommon.Utility
{
    /// <summary>
    /// 重量値取込のユーティリティ
    /// </summary>
    public class JyuryoTorikomiUtility
    {
        /// <summary>
        /// 取込ファイルが存在しない場合は作成する。
        /// 存在する場合⇒「1：発音開始」の場合は「0：発音停止完了」に更新する。
        /// </summary>
        public void MakeTorikomiFile()
        {
            FileStream fs = null;
            StreamReader sr = null;
            StreamWriter sw = null;
            try
            {
                // CurrentUserCustomConfigProfileファイルからトラックスケール通信設定を読み込む。
                var configXml = XElement.Load(r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath);
                // 重量値取込み（Use）、重量ファイル名（FilePath）を取得する。
                var scaleElem = configXml.XPathSelectElement("./Settings/TScaleSettings/Scale");
                var scaleUse = scaleElem.Attribute("Use").Value;
                var scaleFilePath = scaleElem.Attribute("FilePath").Value;

                // 重量値取込みが「1：通信する」の場合のみ処理を行う。
                if (scaleUse.Equals("1"))
                {
                    // トラックスケール通信の重量値ファイルパスが設定されている場合
                    if (!string.IsNullOrEmpty(scaleFilePath))
                    {
                        string dir = Path.GetDirectoryName(scaleFilePath);

                        // ディレクトリの存在チェック
                        if (!Directory.Exists(dir))
                        {
                            // ディレクトリが存在しない場合、エラーとする。
                            MessageBox.Show("パス：" + dir +"の一部が見つかりませんでした。\n" + "フォルダが存在するか確認してください。"
                                , "アラート", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string filePath = dir + "/" + SalesPaymentConstans.TORIKOMI_FILE_NAME;
                        
                        // 取込ファイルが存在する場合
                        if (File.Exists(filePath))
                        {
                            // ReadOnlyでファイルを読み込む。
                            fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                            sr = new StreamReader(fs);
                            string text = sr.ReadToEnd();
                            sr.Close();
                            fs.Close();

                            // ファイルの中身が「1：発音開始」の場合
                            if (text.Equals("1"))
                            {
                                // 「0：発音停止完了」に更新する。
                                fs = new FileStream(filePath, FileMode.Create);
                                sw = new StreamWriter(fs);
                                sw.Write("0");
                            }
                        }
                        else
                        {
                            // 取込ファイルが存在しない場合、「0：発音停止完了」で新規にファイルを作成する。
                            fs = new FileStream(filePath, FileMode.Create);
                            sw = new StreamWriter(fs);
                            sw.Write("0");
                        }
                    }
                }   
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sw != null) sw.Close();
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();
            }   
        }
    }
}
