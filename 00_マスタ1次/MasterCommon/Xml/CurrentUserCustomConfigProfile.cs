using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace MasterCommon.Xml
{
    /// <summary>
    /// XMLファイルにアクセスするためのクラス
    /// </summary>
    [Serializable]
    public class CurrentUserCustomConfigProfile
    {
        /// <summary>
        /// ファイルから読み込みます
        /// </summary>
        /// <param name="file">ファイルのパス</param>
        /// <returns></returns>
        public static CurrentUserCustomConfigProfile Load(string file)
        {
            XmlSerializer x = new XmlSerializer(typeof(CurrentUserCustomConfigProfile));

            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {            
                return (CurrentUserCustomConfigProfile)x.Deserialize(fs);
            }
        }

        /// <summary>
        /// デフォルトファイルを読み込みます
        /// </summary>
        /// <returns></returns>
        public static CurrentUserCustomConfigProfile Load()
        {
            return Load(r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath);
        }

        /// <summary>
        /// ファイルへ書き込みます
        /// </summary>
        /// <param name="file">書き込むファイル</param>
        /// <returns></returns>
        public void Save(string file)
        {
            XmlSerializer x = new XmlSerializer(typeof(CurrentUserCustomConfigProfile));

            //TODO:バックアップして書き込んで成功したらバックアップ消す

            using (StreamWriter sr = new StreamWriter(file, false, Encoding.UTF8))
            {
                x.Serialize(sr, this);
            }
        }
        /// <summary>
        /// デフォルトのファイルへ書き込みます
        /// </summary>
        /// <returns></returns>
        public void Save()
        {
            Save(r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath);
        }
                       
        public SettingsCls Settings{ get; set; }

        //内部クラス

        [Serializable]
        public class SettingsCls
        {
            public LoginInfoCls LoginInfo { get; set; }
            public List<ItemSettings> DefaultValue { get; set; }
            public List<PrintSettings> PrintReport { get; set; }
            public TScaleSettingsCls TScaleSettings { get; set; }
            public CtiSettingsCls CtiSettings { get; set; }
            public InxsSettingsCls InxsSettings { get; set; }

            //内部クラス
            [Serializable]
            public class LoginInfoCls
            {
                public UserCls User { get; set; }

                //内部クラス（最下層）
                [Serializable]
                public class UserCls
                {
                    [XmlAttribute]
                    public string Code { get; set; }
                    [XmlAttribute]
                    public string Pwd { get; set; }
                    [XmlAttribute]
                    public string PwdSaved { get; set; }
                }

            }

            //内部クラス（最下層）
            [Serializable]
            public class ItemSettings
            {
                public string Name { get; set; }
                public string Value { get; set; }
            }

            [Serializable]
            public class PrintSettings
            {
                public NameCls Name { get; set; }
                public PaperCls Paper { get; set; }
                public MarginCls Margin { get; set; }

                //内部クラス
                [Serializable]
                public class NameCls
                {
                    [XmlAttribute]
                    public string Report { get; set; }
                    [XmlAttribute]
                    public string Printer { get; set; }
                }

                [Serializable]
                public class PaperCls
                {
                    [XmlAttribute]
                    public string Size { get; set; }
                    [XmlAttribute]
                    public string Source { get; set; }
                    [XmlAttribute]
                    public bool Landscape { get; set; }
                    [XmlAttribute]
                    public bool Color { get; set; }
                }

                [Serializable]
                public class MarginCls
                {
                    [XmlAttribute]
                    public decimal Top { get; set; }
                    [XmlAttribute]
                    public decimal Bottom { get; set; }
                    [XmlAttribute]
                    public decimal Left { get; set; }
                    [XmlAttribute]
                    public decimal Right { get; set; }
                }
            }

            //内部クラス（最下層）  
            [XmlElement]
            public string ManifestPrinter { get; set; }

            //内部クラス
            [Serializable]
            public class TScaleSettingsCls
            {
                public ScaleCls Scale { get; set; }

                //内部クラス（最下層）
                [Serializable]
                public class ScaleCls
                {
                    [XmlAttribute]
                    public string Use { get; set; }
                    [XmlAttribute]
                    public string FilePath { get; set; }
                    [XmlAttribute]
                    public string FileNoReactTime { get; set; }
                    [XmlAttribute]
                    public string FileDetectTime { get; set; }
                    [XmlAttribute]
                    public string DetectAllowWeight { get; set; }
                    [XmlAttribute]
                    public string STWeightCount { get; set; }
                }
            }

            //内部クラス
            [Serializable]
            public class CtiSettingsCls
            {
                public CtiScaleCls Values { get; set; }

                //内部クラス（最下層）
                [Serializable]
                public class CtiScaleCls
                {
                    [XmlAttribute]
                    public string Use { get; set; }
                    [XmlAttribute]
                    public string FilePath { get; set; }
                    [XmlAttribute]
                    public string FileDetectTime { get; set; }
                }
            }

            //内部クラス
            [Serializable]
            public class InxsSettingsCls
            {
                public InxsScanCls Values { get; set; }

                //内部クラス（最下層）
                [Serializable]
                public class InxsScanCls
                {
                    [XmlAttribute]
                    public string FilePath { get; set; }
                }
            }
        }
    }

}
