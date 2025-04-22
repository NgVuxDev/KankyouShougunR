using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.SMS;
using Shougun.Core.ExternalConnection.ExternalCommon.Utility;
using System.Data.SqlTypes;

namespace Shougun.Core.ExternalConnection.ExternalCommon.Logic
{
    /// <summary>
    /// 各受付メッセージの内容を設定するロジック
    /// </summary>
    public class SMSLogic
    {
        #region フィールド

        /// <summary>メッセージ</summary>
        private MessageBoxShowLogic msgLogic;
        
        /// <summary>システム設定Dao</summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>現場Dao</summary>
        private IM_GENBADao genbaDao;

        /// <summary>SMSを送信するメッセージリスト</summary>
        internal string[] smsList;

        /// <summary>最大文字数</summary>
        int maxCount = 0;

        /// <summary>アラート文字数</summary>
        int alertCount = 0;

        /// <summary>ショートメッセージ合計文字数</summary>
        private int sumAll = 0;

        /// <summary>メッセージテキスト</summary>
        private string msg = string.Empty;

        /// <summary>メッセージ文字数計算結果1</summary>
        private string sum1 = string.Empty;

        /// <summary>メッセージ文字数計算結果2</summary>
        private string sum2 = string.Empty;

        /// <summary>メッセージ文字数計算結果3（本文2（作業日）＋本文3（現場名）＋本文4（その他））</summary>
        private string sum3 = string.Empty;

        /// <summary>メッセージ文字数計算結果4（本文3（現場名）＋本文4（その他））</summary>
        private string sum4 = string.Empty;

        #endregion

        #region コンストラクタ
        /// <summary>コンストラクタ</summary>
        public SMSLogic()
        {
            this.msgLogic = new MessageBoxShowLogic();
            // DAO
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
        }
        #endregion

        /// <summary>件名</summary>
        /// <param name="denpyouKbn">伝票種類</param>
        /// <param name="haisyaJokyo">配車状況</param>
        public string SubjectSetting(string denpyouKbn, string haisyaJokyo)
        {
            string subject = string.Empty;

            // 収集受付入力
            if(denpyouKbn == "1")
            {
                if (haisyaJokyo == "受注" || haisyaJokyo == "配車")
                {
                    subject = "【収集受付のご連絡】\r\n";
                }
                else if (haisyaJokyo == "計上" || haisyaJokyo == "回収なし")
                {
                    subject = "【収集完了のご連絡】\r\n";
                }
                else if (haisyaJokyo == "キャンセル")
                {
                    subject = "【収集キャンセルのご連絡】\r\n";
                }
            }
            else if(denpyouKbn == "2")
            {
                if (haisyaJokyo == "受注" || haisyaJokyo == "配車")
                {
                    subject = "【出荷受付のご連絡】\r\n";
                }
                else if (haisyaJokyo == "計上" || haisyaJokyo == "回収なし")
                {
                    subject = "【出荷完了のご連絡】\r\n";
                }
                else if (haisyaJokyo == "キャンセル")
                {
                    subject = "【出荷キャンセルのご連絡】\r\n";
                }
            }
            else if(denpyouKbn == "3")
            {
                subject = "【持込受付のご連絡】\r\n";
            }
            else if(denpyouKbn == "4")
            {
                subject = "【定期回収のご連絡】\r\n";
            }
            return subject;
        }

        /// <summary>受信者</summary>
        /// <param name="smsReceiverName">ｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタの携帯番号に紐づく受信者名</param>
        public string ReceiverSetting(string smsReceiverName)
        {
            string receiver = string.Empty;

            if (!string.IsNullOrEmpty(smsReceiverName))
            {
                if (!smsReceiverName.Contains("様") && !smsReceiverName.Contains("\n"))
                {
                    receiver = string.Format("{0}様\n", smsReceiverName);
                }
            }

            return receiver;
        }

        /// <summary>挨拶文</summary>
        /// <param name="greetingS">システム設定もしくは個別設定で設定されている挨拶文</param>
        public string GreetingsSetting(string greetingS)
        {
            string greetings = string.Empty;

            if (string.IsNullOrEmpty(greetingS))
            {
                greetings = greetingS;
            }

            return greetings;
        }

        /// <summary>本文初期設定</summary>
        /// <param name="denpyouShurui">伝票種類</param>
        /// <param name="haisyaJokyo">配車状況</param>
        /// <param name="sagyouDate">作業日付</param>
        /// <param name="genchakuTime">現着時間（定期時は希望時間として扱う）</param>
        /// <param name="genbaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="genbaName">現場名</param>
        /// <param name="genbaAddress1">現場住所1</param>
        /// <param name="genbaAddress2">現場住所2</param>
        public string[] TextInitSetting(string denpyouShurui, string haisyaJokyo, string sagyouDate, 
                                        string genchakuTime, string gyoushaCd, string genbaCd, string genbaName)
        {
            // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面にセットする本文
            string text1 = string.Empty;
            string text2 = string.Empty;
            string text3 = string.Empty;
            string text4 = string.Empty;

            // 諸口フラグ検索SQL
        　　string sql = "SELECT SHOKUCHI_KBN,GENBA_NAME1,GENBA_NAME2 FROM M_GENBA WHERE GYOUSHA_CD = '{0}' AND GENBA_CD = '{1}'";
            sql = string.Format(sql, gyoushaCd, genbaCd);
            DataTable dt = this.genbaDao.GetDateForStringSql(sql);

            // 収集
            if(denpyouShurui == "1")
            {
                if (haisyaJokyo == "受注" || haisyaJokyo == "配車")
                {
                    if (!string.IsNullOrEmpty(sagyouDate))
                    {
                        // 仮本文1
                        text1 += "収集作業日は、以下の通りとなります。\r\n";

                        // 仮本文2
                        text2 += string.Format("作業日：{0}", sagyouDate);

                        if (genchakuTime != null)
                        {
                            text2 += string.Format("{0}", genchakuTime);
                        }
                    }

                    // 仮本文3
                    if (dt.Rows.Count > 0 && (bool)dt.Rows[0]["SHOKUCHI_KBN"])
                    {
                        text3 += string.Format("現場名：{0}", genbaName);
                    }
                    else if (dt.Rows.Count > 0)
                    {
                        string genName = dt.Rows[0]["GENBA_NAME1"].ToString();
                        if (dt.Rows[0]["GENBA_NAME2"] != null)
                        {
                            genName += dt.Rows[0]["GENBA_NAME2"].ToString();
                        }
                        if (genName.Length > 66)
                        {
                            text3 += string.Format("現場名：{0}", dt.Rows[0]["GENBA_NAME1"].ToString());
                            text4 += string.Format("{0}\r\n", dt.Rows[0]["GENBA_NAME2"].ToString());
                        }
                        else
                        {
                            text3 += string.Format("現場名：{0}\r\n", genName);
                        }
                    }
                }

                else if (haisyaJokyo == "計上")
                {
                    // 仮本文1
                    text1 += "収集作業が完了しました。\r\n";

                    // 仮本文2
                    if (dt.Rows.Count > 0 && (bool)dt.Rows[0]["SHOKUCHI_KBN"])
                    {
                        text2 += string.Format("現場名：{0}", genbaName);
                    }
                    else if (dt.Rows.Count > 0)
                    {
                        string genName = dt.Rows[0]["GENBA_NAME1"].ToString();
                        if (dt.Rows[0]["GENBA_NAME2"] != null)
                        {
                            genName += dt.Rows[0]["GENBA_NAME2"].ToString();
                        }
                        if (genName.Length > 66)
                        {
                            text2 += string.Format("現場名：{0}", dt.Rows[0]["GENBA_NAME1"].ToString());
                            text3 += string.Format("{0}\r\n", dt.Rows[0]["GENBA_NAME2"].ToString());
                        }
                        else
                        {
                            text2 += string.Format("現場名：{0}\r\n", genName);
                        }
                    }
                }

                else if (haisyaJokyo == "キャンセル")
                {
                    // 仮本文1
                    text1 += "収集作業のキャンセルを承りました。\r\n";

                    // 仮本文2
                    if (dt.Rows.Count > 0 && (bool)dt.Rows[0]["SHOKUCHI_KBN"])
                    {
                        text2 += string.Format("現場名：{0}", genbaName);
                    }
                    else if (dt.Rows.Count > 0)
                    {
                        string genName = dt.Rows[0]["GENBA_NAME1"].ToString();
                        if (dt.Rows[0]["GENBA_NAME2"] != null)
                        {
                            genName += dt.Rows[0]["GENBA_NAME2"].ToString();
                        }
                        if (genName.Length > 66)
                        {
                            text2 += string.Format("現場名：{0}", dt.Rows[0]["GENBA_NAME1"].ToString());
                            text3 += string.Format("{0}\r\n", dt.Rows[0]["GENBA_NAME2"].ToString());
                        }
                        else
                        {
                            text2 += string.Format("現場名：{0}\r\n", genName);
                        }
                    }
                }

                else if (haisyaJokyo == "回収なし")
                {
                    // 仮本文1
                    text1 += "収集予定の品物がございませんでした。\r\n";

                    // 仮本文2
                    if (dt.Rows.Count > 0 && (bool)dt.Rows[0]["SHOKUCHI_KBN"])
                    {
                        text2 += string.Format("現場名：{0}", genbaName);
                    }
                    else if (dt.Rows.Count > 0)
                    {
                        string genName = dt.Rows[0]["GENBA_NAME1"].ToString();
                        if (dt.Rows[0]["GENBA_NAME2"] != null)
                        {
                            genName += dt.Rows[0]["GENBA_NAME2"].ToString();
                        }
                        if (genName.Length > 66)
                        {
                            text2 += string.Format("現場名：{0}", dt.Rows[0]["GENBA_NAME1"].ToString());
                            text3 += string.Format("{0}\r\n", dt.Rows[0]["GENBA_NAME2"].ToString());
                        }
                        else
                        {
                            text2 += string.Format("現場名：{0}\r\n", genName);
                        }
                    }
                }
            }
            // 出荷
            else if(denpyouShurui == "2")
            {
                if (haisyaJokyo == "受注" || haisyaJokyo == "配車")
                {
                    if (!string.IsNullOrEmpty(sagyouDate))
                    {
                        // 仮本文1
                        text1 += "出荷予定日は、以下の通りとなります。\r\n";

                        // 仮本文2
                        text2 += string.Format("予定日：{0}", sagyouDate);

                        if (genchakuTime != null)
                        {
                            text2 += string.Format("{0}", genchakuTime);
                        }
                    }

                    // 仮本文3
                    if (dt.Rows.Count > 0 && (bool)dt.Rows[0]["SHOKUCHI_KBN"])
                    {
                        text3 += string.Format("現場名：{0}", genbaName);
                    }
                    else if (dt.Rows.Count > 0)
                    {
                        string genName = dt.Rows[0]["GENBA_NAME1"].ToString();
                        if (dt.Rows[0]["GENBA_NAME2"] != null)
                        {
                            genName += dt.Rows[0]["GENBA_NAME2"].ToString();
                        }
                        if (genName.Length > 66)
                        {
                            text3 += string.Format("現場名：{0}", dt.Rows[0]["GENBA_NAME1"].ToString());
                            text4 += string.Format("{0}\r\n", dt.Rows[0]["GENBA_NAME2"].ToString());
                        }
                        else
                        {
                            text3 += string.Format("現場名：{0}\r\n", genName);
                        }
                    }
                }

                else if (haisyaJokyo == "計上")
                {
                    // 仮本文1
                    text1 += "出荷作業が完了しました。\r\n";

                    // 仮本文2
                    if (dt.Rows.Count > 0 && (bool)dt.Rows[0]["SHOKUCHI_KBN"])
                    {
                        text2 += string.Format("現場名：{0}", genbaName);
                    }
                    else if (dt.Rows.Count > 0)
                    {
                        string genName = dt.Rows[0]["GENBA_NAME1"].ToString();
                        if (dt.Rows[0]["GENBA_NAME2"] != null)
                        {
                            genName += dt.Rows[0]["GENBA_NAME2"].ToString();
                        }
                        if (genName.Length > 66)
                        {
                            text2 += string.Format("現場名：{0}", dt.Rows[0]["GENBA_NAME1"].ToString());
                            text3 += string.Format("{0}\r\n", dt.Rows[0]["GENBA_NAME2"].ToString());
                        }
                        else
                        {
                            text2 += string.Format("現場名：{0}\r\n", genName);
                        }
                    }
                }

                else if (haisyaJokyo == "キャンセル")
                {
                    // 仮本文1
                    text1 += "出荷作業のキャンセルを承りました。\r\n";

                    // 仮本文2
                    if (dt.Rows.Count > 0 && (bool)dt.Rows[0]["SHOKUCHI_KBN"])
                    {
                        text2 += string.Format("現場名：{0}", genbaName);
                    }
                    else if (dt.Rows.Count > 0)
                    {
                        string genName = dt.Rows[0]["GENBA_NAME1"].ToString();
                        if (dt.Rows[0]["GENBA_NAME2"] != null)
                        {
                            genName += dt.Rows[0]["GENBA_NAME2"].ToString();
                        }
                        if (genName.Length > 66)
                        {
                            text2 += string.Format("現場名：{0}", dt.Rows[0]["GENBA_NAME1"].ToString());
                            text3 += string.Format("{0}\r\n", dt.Rows[0]["GENBA_NAME2"].ToString());
                        }
                        else
                        {
                            text2 += string.Format("現場名：{0}\r\n", genName);
                        }
                    }
                }

                else if (haisyaJokyo == "回収なし")
                {
                    // 仮本文1
                    text1 += "出荷予定の品物がございませんでした。\r\n";

                    // 仮本文2
                    if (dt.Rows.Count > 0 && (bool)dt.Rows[0]["SHOKUCHI_KBN"])
                    {
                        text2 += string.Format("現場名：{0}", genbaName);
                    }
                    else if (dt.Rows.Count > 0)
                    {
                        string genName = dt.Rows[0]["GENBA_NAME1"].ToString();
                        if (dt.Rows[0]["GENBA_NAME2"] != null)
                        {
                            genName += dt.Rows[0]["GENBA_NAME2"].ToString();
                        }
                        if (genName.Length > 66)
                        {
                            text2 += string.Format("現場名：{0}", dt.Rows[0]["GENBA_NAME1"].ToString());
                            text3 += string.Format("{0}\r\n", dt.Rows[0]["GENBA_NAME2"].ToString());
                        }
                        else
                        {
                            text2 += string.Format("現場名：{0}\r\n", genName);
                        }
                    }
                }
            }
            // 持込
            else if(denpyouShurui == "3")
            {
                if (!string.IsNullOrEmpty(sagyouDate))
                {
                    // 仮本文1
                    text1 += "持込日は、以下の通りとなります。\r\n";

                    // 仮本文2
                    text2 += string.Format("持込日：{0}", sagyouDate);

                    if (genchakuTime != null)
                    {
                        text2 += string.Format("{0}", genchakuTime);
                    }
                }
            }
            // 定期
            else if(denpyouShurui == "4")
            {
                if (!string.IsNullOrEmpty(sagyouDate))
                {
                    // 仮本文1
                    text1 += "定期回収日は、以下の通りとなります。\r\n";

                    // 仮本文2
                    text2 += string.Format("予定日：{0}", sagyouDate);

                    if (genchakuTime != null)
                    {
                        text2 += string.Format("{0}", genchakuTime);
                    }
                }

                // 仮本文3（定期時、現場略称名は使用しない）
                if (dt.Rows.Count > 0)
                {
                    string genName = dt.Rows[0]["GENBA_NAME1"].ToString();
                    if (dt.Rows[0]["GENBA_NAME2"] != null)
                    {
                        genName += dt.Rows[0]["GENBA_NAME2"].ToString();
                    }
                    if (genName.Length > 66)
                    {
                        text3 += string.Format("現場名：{0}", dt.Rows[0]["GENBA_NAME1"].ToString());
                        text4 += string.Format("{0}\r\n", dt.Rows[0]["GENBA_NAME2"].ToString());
                    }
                    else
                    {
                        text3 += string.Format("現場名：{0}\r\n", genName);
                    }
                }
            }

            return new string[4]{ text1, text2, text3, text4 };
        }

        /// <summary>署名</summary>
        /// <param name="greetingS">システム設定もしくは個別設定で設定されている署名</param>
        public string SignatureSetting(string signaturE)
        {
            string signature = string.Empty;

            if (string.IsNullOrEmpty(signaturE))
            {
                signature = signaturE;
            }

            return signature;
        }

        /// <summary>ショートメッセージ文字数算出（param=項目の文字数）</summary>
        /// <param name="subject">件名</param>
        /// <param name="receiver">受信者</param>
        /// <param name="greetings">挨拶文</param>
        /// <param name="text1">本文1（状況説明）</param>
        /// <param name="text2">本文2（作業日）</param>
        /// <param name="text3">本文3（現場名）</param>
        /// <param name="text4">本文4（その他）</param>
        /// <param name="signature">署名</param>
        public int SMSWordCountCalc(string subject, string receiver, string greetings, 
                                      string text1, string text2, string text3, string text4, string signature)
        {
            // 各項目のLengthに改行文字が含まれている回数を足す
            int subCount = subject.Length + subject.Count(f => f == '\n');
            int recCount = receiver.Length + receiver.Count(f => f == '\n');
            int greCount = greetings.Length + greetings.Count(f => f == '\n');
            int te1Count = text1.Length + text1.Count(f => f == '\n');
            int te2Count = text2.Length + text2.Count(f => f == '\n');
            int te3Count = text3.Length + text3.Count(f => f == '\n');
            int te4Count = text4.Length + text4.Count(f => f == '\n');
            int sigCount = signature.Length + signature.Count(f => f == '\n');

            // 全ての項目の合計文字数を算出
            sumAll = subCount + recCount + greCount + te1Count + te2Count + te3Count + te4Count + sigCount;

            // 最大文字数、アラート文字数を設定（システム設定参照）
            M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");
            maxCount = sysInfo.KARADEN_MAX_WORD_COUNT;
            alertCount = sysInfo.SMS_ALERT_CHARACTER_LIMIT;

            return sumAll;
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ送信内容設定（改行文字の変換）
        /// </summary>
        public void SendMessageSetting(List<string> sendPrame)
        {
            // 件名
            if (!string.IsNullOrEmpty(sendPrame[0]))
            {
                if (sendPrame[0].Contains("\r\n"))
                {
                    // 改行文字を変換
                    var sub = Regex.Unescape(sendPrame[0]);
                    sendPrame[0] = Regex.Replace(sub, "\r\n", "\n");
                }
            }

            // 挨拶文
            if (!string.IsNullOrEmpty(sendPrame[1]))
            {
                if (sendPrame[1].Contains("\r\n"))
                {
                    // 改行文字を変換
                    var gre = Regex.Unescape(sendPrame[1]);
                    sendPrame[1] = Regex.Replace(gre, "\r\n", "\n");
                }
            }

            // 本文1
            if (!string.IsNullOrEmpty(sendPrame[2]))
            {
                if (sendPrame[2].Contains("\r\n"))
                {
                    // 改行文字を変換
                    var te1 = Regex.Unescape(sendPrame[2]);
                    sendPrame[2] = Regex.Replace(te1, "\r\n", "\n");
                }
            }

            // 本文2
            if (!string.IsNullOrEmpty(sendPrame[3]))
            {
                if (sendPrame[3].Contains("\r\n"))
                {
                    // 改行文字を変換
                    var te2 = Regex.Unescape(sendPrame[3]);
                    sendPrame[3] = Regex.Replace(te2, "\r\n", "\n");
                }
            }

            // 本文3
            if (!string.IsNullOrEmpty(sendPrame[4]))
            {
                if (sendPrame[4].Contains("\r\n"))
                {
                    // 改行文字を変換
                    var te3 = Regex.Unescape(sendPrame[4]);
                    sendPrame[4] = Regex.Replace(te3, "\r\n", "\n");
                }
            }

            // 本文4
            if (!string.IsNullOrEmpty(sendPrame[5]))
            {
                if (sendPrame[5].Contains("\r\n"))
                {
                    // 改行文字を変換
                    var te4 = Regex.Unescape(sendPrame[5]);
                    sendPrame[5] = Regex.Replace(te4, "\r\n", "\n");
                }
            }

            // 署名
            if (!string.IsNullOrEmpty(sendPrame[6]))
            {
                if (sendPrame[6].Contains("\r\n"))
                {
                    // 改行文字を変換
                    var sig = Regex.Unescape(sendPrame[6]);
                    sendPrame[6] = Regex.Replace(sig, "\r\n", "\n");
                }
            }
        }

        /// <summary>APIへのリクエスト作成</summary>
        /// <param name="subject">件名</param>
        /// <param name="receiverName">受信者名</param>
        /// <param name="greetings">挨拶文</param>
        /// <param name="text1">本文1（状況説明）</param>
        /// <param name="text2">本文2（作業日）</param>
        /// <param name="text3">本文3（現場名）</param>
        /// <param name="text4">本文4（その他）</param>
        /// <param name="signature">署名</param>
        public void APIRequestSetting(string subject, string receiverName, string greetings, 
                                      string text1, string text2, string text3, string text4, string signature)
        {
            // リクエスト用のメッセージ（最大70文字）
            string message1 = string.Empty;
            string message2 = string.Empty;
            string message3 = string.Empty;
            string message4 = string.Empty;
            string message5 = string.Empty;
            string message6 = string.Empty;
            string message7 = string.Empty;

            #region 文字数算出
            
            // SMS送信をする時に、文章1つをそのまま送信するのではなく、
            // 70文字以内に納めた複数のメッセージをリクエストする仕様であるため、文字数の算出を行う

            // 件名・受信者・挨拶文を結合、合計文字数と改行文字出現回数を算出
            sum1 = subject + receiverName + greetings;
            int count1 = sum1.Count(f => f == '\n');

            // 本文1（状況説明）＋本文2（作業日）を結合、合計文字数と改行文字出現回数を算出
            sum2 = text1 + text2;
            int count2 = sum2.Count(f => f == '\n');

            // 本文2（作業日）＋本文3（現場名）＋本文4（その他）を結合、合計文字数と改行文字出現回数を算出
            sum3 = text2 + text3 + text4;
            int count3 = sum3.Count(f => f == '\n');

            // 本文3（現場名）＋本文4（その他）を結合、合計文字数と改行文字出現回数を算出
            sum4 = text3 + text4;
            int count4 = sum4.Count(f => f == '\n');

            #endregion

            // 件名＋受信者＋挨拶文の文字数が70文字に収まる場合
            if (sum1.Length + (count1 * 2) < 71)
            {
                message1 = sum1;

                // 本文1（状況説明）＋本文2（作業日）の文字数が70文字に収まる場合
                if (sum2.Length + (count2 * 2) < 71)
                {
                    message2 = sum2;

                    // 本文3（現場名）＋本文4（その他）の文字数が70文字に収まる場合
                    if (sum4.Length + (count4 * 2) < 71)
                    {
                        if (message1.Length + (count1 * 2) + message2.Length + (count2 * 2) < 71)
                        {
                            message1 = message1 + message2;
                            message2 = sum4;
                            message3 = signature;
                        }
                        else
                        {
                            // 本文3（現場名）、本文4（その他）が未入力である場合
                            if (sum4.Length == 0)
                            {
                                message3 = signature;
                            }
                            else
                            {
                                message3 = sum4;
                                message4 = signature;
                            }
                        }
                    }
                    // 本文3（現場名）＋本文4（その他）の文字数が70文字に収まらない場合
                    else
                    {
                        if (message1.Length + (count1 * 2) + message2.Length + (count2 * 2) < 71)
                        {
                            message1 = message1 + message2;
                            message2 = text3;
                            message3 = text4;
                            message4 = signature;
                        }
                        else
                        {
                            message3 = text3;
                            message4 = text4;
                            message5 = signature;
                        }
                    }
                }
                // 本文1（状況説明）＋本文2（作業日）の文字数が70文字に収まらない場合
                else
                {
                    message2 = text1;

                    // 本文2（作業日）＋本文3（現場名）＋本文4（その他）の文字数が70文字に収まる場合
                    if (sum3.Length + (count3 * 2) < 71)
                    {
                        message3 = sum3;
                        message4 = signature;
                    }
                    // 本文2（作業日）＋本文3（現場名）＋本文4（その他）の文字数が70文字に収まらない場合
                    else
                    {
                        message3 = text2;

                        // 本文3（現場名）＋本文4（その他）の文字数が70文字に収まる場合
                        if (sum4.Length + (count4 * 2) < 71)
                        {
                            // 本文3（現場名）、本文4（その他）が未入力である場合
                            if (sum4.Length == 0)
                            {
                                message4 = signature;
                            }
                            else
                            {
                                message4 = sum4;
                                message5 = signature;
                            }
                        }
                        // 本文3（現場名）＋本文4（その他）の文字数が70文字に収まらない場合
                        else
                        {
                            message4 = text3;
                            message5 = text4;
                            message6 = signature;
                        }
                    }
                }
            }
            // 件名＋受信者＋挨拶文の文字数が70文字に収まらない場合
            else
            {
                message1 = subject + receiverName;
                message2 = greetings;

                // 本文1（状況説明）＋本文2（作業日）の文字数が70文字に収まる場合
                if (sum2.Length + (count2 * 2) < 71)
                {
                    message3 = sum2;

                    // 本文3（現場名）＋本文4（その他）の文字数が70文字に収まる場合
                    if (sum4.Length + (count4 * 2) < 71)
                    {
                        // 本文3（現場名）、本文4（その他）が未入力である場合
                        if (sum4.Length == 0)
                        {
                            message4 = signature;
                        }
                        else
                        {
                            message4 = sum4;
                            message5 = signature;
                        }
                    }
                    // 本文3（現場名）＋本文4（その他）の文字数が70文字に収まらない場合
                    else
                    {
                        message4 = text3;
                        message5 = text4;
                        message6 = signature;
                    }
                }
                // 本文1（状況説明）＋本文2（作業日）の文字数が70文字に収まらない場合
                else
                {
                    message3 = text1;

                    // 本文2（作業日）＋本文3（現場名）＋本文4（その他）の文字数が70文字に収まる場合
                    if (sum3.Length + (count3 * 2) < 71)
                    {
                        message4 = sum3;
                        message5 = signature;
                    }
                    // 本文2（作業日）＋本文3（現場名）＋本文4（その他）の文字数が70文字に収まらない場合
                    else
                    {
                        message4 = text2;

                        // 本文3（現場名）＋本文4（その他）の文字数が70文字に収まる場合
                        if (sum4.Length + (count4 * 2) < 71)
                        {
                            // 本文3（現場名）、本文4（その他）が未入力である場合
                            if (sum4.Length == 0)
                            {
                                message5 = signature;
                            }
                            else
                            {
                                message5 = sum4;
                                message6 = signature;
                            }
                        }
                        // 本文3（現場名）＋本文4（その他）の文字数が70文字に収まらない場合
                        else
                        {
                            message5 = text3;
                            message6 = text4;
                            message7 = signature;
                        }
                    }
                }
            }

            smsList = new string[7] { message1, message2, message3, message4, message5, message6, message7 };
        }

        /// <summary>メッセージサイズチェック（最大送信文字数）</summary>
        /// <param name="msgcount">メッセージ合計文字数</param>
        /// <param name="sysInfo">システム設定</param>
        public bool SMSMaxWordCountCheck(int msgcount)
        {
            if (msgcount > maxCount)
            {
                // 最大文字数よりメッセージ文字数が大きい場合は、処理中断
                msg = string.Format("メッセージの文字数が多すぎるため送信できません。\r\n（最大文字数={0},送信メッセージ文字数={1}）", maxCount, msgcount);
                this.msgLogic.MessageBoxShowError(msg);
                return false;
            }

            return true;
        }

        /// <summary>メッセージサイズチェック（アラート文字数）</summary>
        /// <param name="msgcount">メッセージ合計文字数</param>
        /// <param name="sysInfo">システム設定</param>
        public bool SMSAlertCountCheck(int msgcount)
        {
            // アラート文字数取得
            if (msgcount > alertCount)
            {
                // アラート文字数よりメッセージ文字数が大きい場合は、
                return false;
            }

            return true;
        }

        /// <summary>
        /// 長文分割送信API
        /// </summary>
        /// <param name="entity"></param>
        public string[] LongSmsSplitSendAPI(T_SMS entity)
        {
            // レスポンス配列
            string[] resArray = new string[2];
            // APIリクエスト後に表示するメッセージテキスト
            string smsMsg = string.Empty;

            try
            {
                // TLS1.2を指定（指定しないとAPI連携不可）
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);

                var url = "https://push-se.karaden.jp/v2/sepakaradenqueue.json";

                // パラメータ項目チェック
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");

                string token = sysInfo.KARADEN_ACCESS_KEY;
                string to = entity.MOBILE_PHONE_NUMBER;
                string message1 = smsList[0];
                string message2 = smsList[1];
                string message3 = smsList[2];
                string message4 = smsList[3];
                string message5 = smsList[4];
                string message6 = smsList[5];
                string message7 = smsList[6];
                //string message8 = smsMessageList[7];
                //string message9 = smsMessageList[8];
                //string message10 = smsMessageList[9];
                string trackingcode = this.sysDate();
                string securitycode = sysInfo.KARADEN_SECURITY_CODE;

                string param = string.Empty;

                var dic = new Dictionary<string, string>();
                dic["Token"] = token;
                dic["To"] = to;
                dic["message1"] = message1;
                dic["message2"] = message2;
                dic["message3"] = message3;
                dic["message4"] = message4;
                dic["message5"] = message5;
                dic["message6"] = message6;
                dic["message7"] = message7;
                //dic["message8"] = message8;
                //dic["message9"] = message9;
                //dic["message10"] = message10;
                dic["TrackingCode"] = trackingcode;
                dic["SecurityCode"] = securitycode;

                // POSTメソッドのパラメータ作成
                foreach (string key in dic.Keys)
                {
                    param += String.Format("{0}={1}&", key, dic[key]);
                }

                // 余計な文字列が入らないように一部文字を取り除く
                param.Remove(param.Length - 1, 1);

                // paramをUTF-8文字列にエンコードする
                byte[] bd = Encoding.UTF8.GetBytes(param);

                // リクエスト作成
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = bd.Length;

                // ポストデータをリクエストに書き込む
                using (Stream reqStream = req.GetRequestStream())
                    reqStream.Write(bd, 0, bd.Length);

                // レスポンスの取得
                WebResponse response = (HttpWebResponse)req.GetResponse();
                RES_LONG_SMS_SPLIT_SEND_API result = null;

                // 結果の読み込み
                using (Stream resStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(resStream, Encoding.GetEncoding("Shift_JIS")))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(RES_LONG_SMS_SPLIT_SEND_API));
                        result = (RES_LONG_SMS_SPLIT_SEND_API)serializer.ReadObject(resStream);
                        if (result.Status.Contains("100"))
                        {
                            resArray[0] = result.MessageId;
                            resArray[1] = result.Status;
                        }
                        else
                        {
                            resArray[1] = result.Status;
                        }
                    }
                }

                return resArray;
            }
            catch (WebException webEx)
            {
                // WebExceptionだけ一括でエラー処理をする
                LogUtility.Error("LongSmsSplitSendAPI", webEx);

                if (webEx.Status == WebExceptionStatus.ProtocolError)
                {
                    // API連携時のエラー
                    this.WebExceptionSetting(webEx);
                }
                else
                {
                    this.msgLogic.MessageBoxShowError("エラーが発生しました。");
                }
                return resArray;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LongSmsSplitSendAPI", ex);
                this.msgLogic.MessageBoxShowError("エラーが発生しました。");
                return resArray;
            }
        }

        /// <summary>
        /// SMS送信結果取得API
        /// </summary>
        /// <param name="msgId">結果を取得するメッセージID</param>
        public string[] SmsSendResultGetAPI(string msgId)
        {
            // レスポンス配列
            string[] resArray = new string[5];
            // APIリクエスト後に表示するメッセージテキスト
            string smsMsg = string.Empty;

            try
            {
                // TLS1.2を指定（指定しないとAPI連携不可）
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);

                var url = "https://push-se.karaden.jp/v2/karadeninquiry.json";

                // パラメータ項目チェック
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");

                string token = sysInfo.KARADEN_ACCESS_KEY;
                string messageId = msgId;
                string securitycode = sysInfo.KARADEN_SECURITY_CODE;

                string param = string.Empty;

                url += "?Token=" + token;
                url += "&messageId=" + messageId;
                url += "&SecurityCode=" + securitycode;

                // リクエスト作成
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";

                // レスポンスの取得
                WebResponse response = (HttpWebResponse)req.GetResponse();
                RES_SMS_SEND_RESULT_GET_API result = null;

                // 結果の読み込み
                using (Stream resStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(resStream))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(RES_SMS_SEND_RESULT_GET_API));
                        result = (RES_SMS_SEND_RESULT_GET_API)serializer.ReadObject(resStream);
                        if (result.Status.Contains("100"))
                        {
                            resArray[1] = result.Messagestatus;
                            resArray[2] = result.Resultstatus;
                            resArray[3] = result.Carrier;
                            resArray[4] = result.Senddate;
                        }
                        resArray[0] = result.Status;
                    }
                }

                return resArray;
            }
            catch (WebException webEx)
            {
                // WebExceptionだけ一括でエラー処理をする
                LogUtility.Error("SmsSendResultGetAPI", webEx);

                if (webEx.Status == WebExceptionStatus.ProtocolError)
                {
                    // API連携時のエラー
                    this.WebExceptionSetting(webEx);
                }
                else
                {
                    this.msgLogic.MessageBoxShowError("エラーが発生しました。");
                }
                return resArray;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SmsSendResultGetAPI", ex);
                this.msgLogic.MessageBoxShowError("エラーが発生しました。");
                return resArray;
            }
        }

        /// <summary>
        /// SMS送信結果取得（トラッキングコード）API
        /// </summary>
        public RES_SMS_SEND_TRACKING_RESULT_GET_API SmsSendTrackingResultGetAPI(string trackingCode)
        {
            RES_SMS_SEND_TRACKING_RESULT_GET_API result = null;

            // APIリクエスト後に表示するメッセージテキスト
            string smsMsg = string.Empty;

            try
            {
                // TLS1.2を指定（指定しないとAPI連携不可）
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);

                var url = "https://push-se.karaden.jp/v2/karadeninquiry_tr.json";

                // パラメータ項目チェック
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");

                string token = sysInfo.KARADEN_ACCESS_KEY;
                string securitycode = sysInfo.KARADEN_SECURITY_CODE;

                string param = string.Empty;

                url += "?Token=" + token;
                url += "&TrackingCode=" + trackingCode;
                url += "&Startcount=1";
                url += "&Startdate=" + trackingCode + "0000";
                url += "&Enddate=" + trackingCode + "2359";
                url += "&SecurityCode=" + securitycode;

                // リクエスト作成
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";

                // レスポンスの取得
                WebResponse response = (HttpWebResponse)req.GetResponse();

                // 結果の読み込み
                using (Stream resStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(resStream))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(RES_SMS_SEND_TRACKING_RESULT_GET_API));
                        result = (RES_SMS_SEND_TRACKING_RESULT_GET_API)serializer.ReadObject(resStream);
                    }
                }

                return result;
            }
            catch (WebException webEx)
            {
                // WebExceptionだけ一括でエラー処理をする
                LogUtility.Error("SmsSendTrackingResultGetAPI", webEx);

                if (webEx.Status == WebExceptionStatus.ProtocolError)
                {
                    // API連携時のエラー
                    this.WebExceptionSetting(webEx);
                }
                else
                {
                    this.msgLogic.MessageBoxShowError("エラーが発生しました。");
                }
                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SmsSendTrackingResultGetAPI", ex);
                this.msgLogic.MessageBoxShowError("エラーが発生しました。");
                return result;
            }
        }

        #region API連携時のエラー

        private void WebExceptionSetting(WebException webEx)
        {
            HttpWebResponse errRes = (HttpWebResponse)webEx.Response;
            var title = string.Empty;

            switch (errRes.StatusCode)
            {
                case HttpStatusCode.BadRequest:         // 400
                    // リクエスト不正
                    title = "HTTP STATUS 400 Bad Request";
                    break;
                case HttpStatusCode.Unauthorized:       // 401
                    // アクセストークン無効
                    title = "HTTP STATUS 401 Unauthorized";
                    break;
                case HttpStatusCode.PaymentRequired:    // 402
                    // 
                    title = "HTTP STATUS 402 Payment Required";
                    break;
                case HttpStatusCode.Forbidden:          // 403
                    // アクセス拒否
                    title = "HTTP STATUS 403 Forbidden";
                    break;
                case HttpStatusCode.NotFound:           // 404
                    // 指定されたページが存在しない。権限が無い。
                    title = "HTTP STATUS 404 Not Found";
                    break;
                case HttpStatusCode.MethodNotAllowed:   // 405
                    // 未許可のメソッド
                    title = "HTTP STATUS 405 Method Not Allowed";
                    break;
                case HttpStatusCode.InternalServerError:// 500
                    // サーバ内部エラー
                    title = "HTTP STATUS 500 Internal Server Error";
                    break;
                default:
                    title = "その他エラー";
                    break;
            }
            this.msgLogic.MessageBoxShowError(string.Format("API連携において、エラーが発生しました。\r\nエラー内容：{0}", title));
        }

        #endregion

        #region システム日付の取得

        /// <summary>
        /// システム日付の取得
        /// </summary>
        /// <returns></returns>
        private string sysDate()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now.ToString("yyyyMMdd");
        }

        #endregion

        #region エラー概要設定
        /// <summary>エラー概要設定</summary>
        /// <param name="msgcount">メッセージ合計文字数</param>
        public string SMSErrorSummarySetting(string ErrorCode)
        {
            string errDetail = string.Empty;

            if (ErrorCode == "201")
            {
                errDetail = "認証エラー";
            }
            else if (ErrorCode == "202")
            {
                errDetail = "電話番号不正値または未指定";
            }
            else if (ErrorCode == "203")
            {
                errDetail = "電話番号制限による送信不可または国制限";
            }
            else if (ErrorCode == "205")
            {
                errDetail = "トラッキングコード不正値";
            }
            else if (ErrorCode == "206")
            {
                errDetail = "messageId不正値";
            }
            else if (ErrorCode == "209")
            {
                errDetail = "長文分割が利用不可プロファイル";
            }
            else if (ErrorCode == "301")
            {
                errDetail = "メッセージ指定なしもしくは制限長超え";
            }
            else if (ErrorCode == "999")
            {
                errDetail = "内部システムエラー";
            }

            return errDetail;
        }
        #endregion
    }
}
