/*********************************************************************************
 *  概要 : UR情報XMLの署名や検証処理
 *         UR情報XML特有の機能を追加した
 *         System.Xml.XmlDocument派生クラス。
 *         
 *  機能 : 署名作成                                  Sign
 *         署名検証結果取得                          Verify
 *         groupからID文字列の配列取得               EnumItems
 *         IDからUR情報取得                          GetURItem
 *         IDからUR値(value)を取得                   GetURValue
 *         IDとUR値(value)を設定                     SetURValue
 *
 **********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Shougun.UserRestrict.URXmlDocument
{
    /// <summary>
    /// XmlDocument継承した署名/検証機能クラス
    /// </summary>
    public class URXmlDocument : XmlDocument
    {
        // UserRestrectItemのList
        private List<UserRestrictItem> URItemList = new List<UserRestrictItem>();

        // 署名時に作成されるXMLタグ名
        // 検証処理で署名ブロックを読み込む時に必要
        private const string SIGN_XML_TAG_NAME = "Signature";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public URXmlDocument()
        {
        }

        /// <summary>
        /// 構成情報ファイルを読込み、URItemListに値を設定する
        /// </summary>
        /// <param name="filename">ファイル名</param>
        public override void Load(string filename)
        {
            try
            {
                base.Load(filename);
                this.URItemList = CreateItemList(this.DocumentElement);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 構成情報テンプレートを読込み、URItemListに値を設定する
        /// </summary>
        /// <param name="xml">xml文字列</param>
        public override void LoadXml(string xml)
        {
            try
            {
                base.LoadXml(xml);
                this.URItemList = CreateItemList(this.DocumentElement);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// URXmlDocumentの署名部分を削除し、ファイルに保存する
        /// </summary>
        /// <param name="filename">保存ファイル名</param>
        public override void  Save(string filename)
        {
            try
            {
                DelSignNode();
                base.Save(filename);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// URXmlDocumentに署名を追加する
        /// </summary>
        /// <param name="secretKeyFilePath">
        /// URKey.CreateKeyで出力した秘密鍵ファイルパス
        /// </param>
        /// <exception cref="ArgumentException">
        /// [署名済みのXmlDocument削除(RemoveChild)]
        ///   oldChild がこのノードの子ではありません。 または、このノードが読み取り専用です。
        /// [秘密鍵ファイル読込み(ReadAllText)]
        ///   path が、長さが 0 の文字列であるか、空白しか含んでいないか、
        ///   または 
        ///   InvalidPathChars で定義されている無効な文字を 1 つ以上含んでいます。
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// [鍵情報設定(FromXmlString)]
        ///   secretKeyFilePathのファイル内容がnullです。
        /// [SignedXmlコンストラクタ]
        ///   document パラメーターが null です。
        ///   または
        ///   document パラメーターに null の DocumentElement プロパティが格納されています。
        /// [秘密鍵ファイル読込み(ReadAllText)]
        ///   path が null です。
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// [URXmlDocumentへの署名XmlElement追加(AppendChild)]
        ///   このノードは、newChild ノードの型の子ノードが許可されない型です。
        ///   newChild がこのノードの先祖です。
        /// [署名XmlElementのインポート(ImportNode)]
        ///   インポートできないノード型でこのメソッドを呼び出しています。
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// [秘密鍵ファイル読込み(ReadAllText)]
        ///   path の形式が無効です。
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// [秘密鍵ファイル読込み(ReadAllText)]
        ///   path によって、読み取り専用のファイルが指定されました。
        ///   または
        ///   この操作は、現在のプラットフォームではサポートされていません。
        ///   または
        ///   path によってディレクトリが指定されました。
        ///   または
        ///   呼び出し元に、必要なアクセス許可がありません。
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// [秘密鍵ファイル読込み(ReadAllText)]
        ///   指定したパス、ファイル名、またはその両方がシステム定義の最大長を超えています。 
        ///   たとえば、Windows ベースのプラットフォームの場合、パスの長さは 248 文字未満、ファイル名の長さは 260 文字未満である必要があります。
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// [秘密鍵ファイル読込み(ReadAllText)]
        ///   指定したパスが無効です (割り当てられていないドライブであるなど)。
        /// </exception>
        /// <exception cref="IOException">
        /// [秘密鍵ファイル読込み(ReadAllText)]
        ///   ファイルを開くときに、I/O エラーが発生しました。
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// [秘密鍵ファイル読込み(ReadAllText)]
        ///   path で指定されたファイルが見つかりませんでした。
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// [秘密鍵ファイル読込み(ReadAllText)]
        ///   呼び出し元に、必要なアクセス許可がありません。
        /// </exception>
        /// <exception cref="System.Security.Cryptography.CryptographicException">
        /// [RSACryptoServiceProviderコンストラクタ]
        ///   暗号化サービス プロバイダー (CSP) を取得できません。
        /// [鍵情報設定(FromXmlString)]
        ///   secretKeyFilePathのXML文字列の書式が有効でない。
        /// [デジタル署名の計算(ComputeSignature)]
        ///   SigningKey プロパティが null である。
        ///   または
        ///   SigningKey プロパティが DSA オブジェクトまたは RSA オブジェクトではありません。
        ///   または
        ///   キーを読み込むことができませんでした。
        /// [SignedXmlオブジェクトのXMLを取得(GetXml)]
        ///   SignedInfo プロパティが null である。
        ///   または
        ///   SignatureValue プロパティが null である。
        /// [reference要素へのTransform オブジェクト追加(AddReference)]
        ///   transform パラメーターが null です。
        /// [URXmlDocumentへの署名XmlElement追加(AppendChild)]
        ///   newChild は、このノードを作成したドキュメントとは異なるドキュメントから作成されました。
        ///   このノードは読み取り専用です。
        /// </exception>
        public void Sign(string secretKeyFilePath)
        {
            try
            {
                // 秘密鍵ファイルを文字列に読み込む
                string secretKeyContents = File.ReadAllText(secretKeyFilePath, Encoding.UTF8);

                // 鍵情報設定
                RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
                rsaProvider.FromXmlString(secretKeyContents);

                // Signタグ削除
                DelSignNode();

                // SignedXmlにURXmlDocumentを読み込む
                SignedXml signedXml = new SignedXml(this);

                // SignedXmlに鍵を追加
                signedXml.SigningKey = rsaProvider;

                // XML デジタル署名のreference要素作成
                Reference reference = new Reference();
                reference.Uri = "";

                // W3C によって定義された、XML デジタル署名のエンベロープ署名変換を表します。
                XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();

                // データ上で実行される変換を、ダイジェストアルゴリズムに渡す前に、
                // Transform オブジェクトを変換のリストに追加します。
                reference.AddTransform(env);

                // referenceオブジェクトをSignedXmlに追加
                signedXml.AddReference(reference);

                // デジタル署名の計算
                signedXml.ComputeSignature();

                // SignedXmlオブジェクトのXMLをXmlElementで取得
                XmlElement digitalSignXmlElement = signedXml.GetXml();

                // URXmlDocumentに上記の署名XmlElementをインポートし、URXmlDocumentを平文 + 署名の形式にする
                this.DocumentElement.AppendChild(this.ImportNode(digitalSignXmlElement, true));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// URXmlDocumentの署名を検証し、結果を取得する
        /// </summary>
        /// <param name="publicKeyContents">
        /// DBに登録した公開鍵文字列
        /// </param>
        /// <returns>
        ///  ture:検証成功
        ///  false:検証失敗
        /// </returns>
        /// <exception cref="ArgumentException">
        /// [公開鍵ファイル読込み(ReadAllText)]
        ///   path が、長さが 0 の文字列であるか、空白しか含んでいないか、
        ///   または 
        ///   InvalidPathChars で定義されている無効な文字を 1 つ以上含んでいます。
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// [鍵情報設定(FromXmlString)]
        ///   publicKeyFilePathのファイル内容がnullです。
        /// [SignedXmlコンストラクタ]
        ///   document パラメーターが null です。
        ///   または
        ///   document パラメーターに null の DocumentElement プロパティが格納されています。
        /// [signatureノード読込み(LoadXml)]
        ///   value パラメーターが null です。
        /// [署名検証(CheckSignature)]
        ///   key パラメーターが null です。
        /// [公開鍵ファイル読込み(ReadAllText)]
        ///   path が null です。
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// [公開鍵ファイル読込み(ReadAllText)]
        ///   path の形式が無効です。
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// [公開鍵ファイル読込み(ReadAllText)]
        ///   path によって、読み取り専用のファイルが指定されました。
        ///   または
        ///   この操作は、現在のプラットフォームではサポートされていません。
        ///   または
        ///   path によってディレクトリが指定されました。
        ///   または
        ///   呼び出し元に、必要なアクセス許可がありません。
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// [公開鍵ファイル読込み(ReadAllText)]
        ///   指定したパス、ファイル名、またはその両方がシステム定義の最大長を超えています。 
        ///   たとえば、Windows ベースのプラットフォームの場合、パスの長さは 248 文字未満、ファイル名の長さは 260 文字未満である必要があります。
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// [公開鍵ファイル読込み(ReadAllText)]
        ///   指定したパスが無効です (割り当てられていないドライブであるなど)。
        /// </exception>
        /// <exception cref="IOException">
        /// [公開鍵ファイル読込み(ReadAllText)]
        ///   ファイルを開くときに、I/O エラーが発生しました。
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// [公開鍵ファイル読込み(ReadAllText)]
        ///   path で指定されたファイルが見つかりませんでした。
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// [公開鍵ファイル読込み(ReadAllText)]
        ///   呼び出し元に、必要なアクセス許可がありません。
        /// </exception>
        /// <exception cref="System.Security.Cryptography.CryptographicException">
        /// [RSACryptoServiceProviderコンストラクタ]
        ///   暗号化サービス プロバイダー (CSP) を取得できません。
        /// [鍵情報設定(FromXmlString)]
        ///   publicKeyFilePathのXML文字列の書式が有効でない。
        /// [signatureノード読込み(LoadXml)]
        ///   value パラメーターが、有効な SignatureValue プロパティを格納していません。
        ///   または
        ///   value パラメーターが、有効な SignedInfo プロパティを格納していません。
        /// [署名検証(CheckSignature)]
        ///   key パラメーターの SignatureAlgorithm プロパティが、SignatureMethod プロパティと一致しません。
        ///   または
        ///   署名の説明を作成できませんでした。
        ///   または
        ///   ハッシュアルゴリズムを作成できませんでした。
        /// </exception>
        public bool Verify(string publicKeyContents)
        {
            bool result = false;

            try
            {
                // 公開鍵ファイルを文字列に読み込む
                //string publicKeyContents = File.ReadAllText(publicKeyFilePath, Encoding.UTF8);

                // rsaProviderに公開鍵を設定する
                RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
                rsaProvider.FromXmlString(publicKeyContents);

                // URXmlDocumentからSignedXmlを初期化
                SignedXml signedXml = new SignedXml(this);

                // Signatureノードを検索
                XmlNodeList nodeList = this.GetElementsByTagName(SIGN_XML_TAG_NAME);

                // 署名の無いxmlDocumntをはじく
                if (nodeList.Count == 0)
                {
                    // falseで返す
                    return result;
                }

                // Signatureノードを読込み
                signedXml.LoadXml((XmlElement)nodeList[0]);

                // 署名の検証
                result = signedXml.CheckSignature(rsaProvider);
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }


        /// <summary>
        /// 指定したグループのID文字列の配列を返す。
        ///（null指定の場合は全グループが対象）
        /// </summary>
        /// <param name="group">グループ名</param>
        /// <returns>IDの配列</returns>
        public string[] EnumItems(string group)
        {
            ArrayList idArrayList = new ArrayList();

            foreach (UserRestrictItem item in this.URItemList)
            {
                if (string.IsNullOrEmpty(group))
                {
                    // 全てのidを返す
                    idArrayList.Add(item.id);
                }
                else
                {
                    if (item.group.Equals(group))
                    {
                        idArrayList.Add(item.id);
                    }
                }
            }

            return (string[])idArrayList.ToArray(typeof(string));
        }

        /// <summary>
        /// 指定したID文字列のUR情報を返す。
        /// </summary>
        /// <param name="id">ID名</param>
        /// <returns>UserRestrictItem</returns>
        public UserRestrictItem GetItem(string id)
        {
            UserRestrictItem urItem = null;

            foreach (UserRestrictItem item in this.URItemList)
            {
                if (item.id.Equals(id))
                {
                    urItem = item;
                }
            }
            return urItem;
        }

        /// <summary>
        /// 指定したID文字列のUR値(value)を返す。
        /// 表示用にはToStringで文字列化する。
        /// </summary>
        /// <param name="id">ID名</param>
        /// <returns>設定値</returns>
        public object GetItemValue(string id)
        {
            object obj = null;

            XmlNode xmlNode = this.SelectSingleNode("/UserRestrictItems/item[id='" + id + "']");

            foreach (XmlNode xn in xmlNode.ChildNodes)
            {
                if (xn.LocalName.Equals("value"))
                {
                    obj = xn.InnerText;
                    break;
                }
            }

            return obj;
        }

        /// <summary>
        /// 指定したID文字列のUR値(value)を設定する。
        /// </summary>
        /// <param name="id">ID名</param>
        /// <param name="value">設定値</param>
        public void SetItemValue(string id, object value)
        {
            XmlNode xmlNode = this.SelectSingleNode("/UserRestrictItems/item[id='" + id + "']");

            foreach (XmlNode xn in xmlNode.ChildNodes)
            {
                if (xn.LocalName.Equals("value"))
                {
                    xn.InnerText = value.ToString();
                    return;
                }
            }
        }

        /// <summary>
        /// UserRestrictItemのListを作成する
        /// </summary>
        /// <param name="rootXmlNode">XmlDocumentのルート</param>
        private List<UserRestrictItem> CreateItemList(XmlNode rootXmlNode)
        {
            var itemList = new List<UserRestrictItem>();
            for (int i = 0; i < rootXmlNode.ChildNodes.Count; i++)
            {
                if (!rootXmlNode.ChildNodes[i].Name.Equals(SIGN_XML_TAG_NAME))
                {
                    XmlNodeList xmlNodeList = rootXmlNode.ChildNodes[i].ChildNodes;

                    string id = xmlNodeList[0].InnerText;
                    string caption = xmlNodeList[1].InnerText;
                    string description = xmlNodeList[2].InnerText;
                    string group = xmlNodeList[3].InnerText;
                    Type type = Type.GetType(xmlNodeList[4].InnerText);
                    string value = xmlNodeList[5].InnerText;

                    var item = new UserRestrictItem(id, caption, 
                                                    description, 
                                                    group, type);
                    itemList.Add(item);
                }
            }
            return itemList;
        }

        /// <summary>
        /// Signatureノードリストを削除する
        /// </summary>
        private void DelSignNode()
        {
            XmlNodeList nodeList = this.GetElementsByTagName(SIGN_XML_TAG_NAME);

            for (int i = 0; i < nodeList.Count; i++)
            {
                this.DocumentElement.RemoveChild(nodeList[i]);
            }
        }
    }
}
