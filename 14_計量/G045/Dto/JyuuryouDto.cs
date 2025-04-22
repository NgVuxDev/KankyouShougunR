using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Utility;
using Shougun.Core.Scale.Keiryou.Utility;
using System.Data.SqlTypes;

namespace Shougun.Core.Scale.Keiryou.Dto
{
    /// <summary>
    /// 重量値用Dto
    /// Detailの行と重量値の行と関連性が無いため
    /// このDtoを利用し重量値計算を実施する
    /// 主に計量、出荷、売上/支払入力で使用してください
    /// </summary>
    public class JyuuryouDto
    {
        #region フィールド

        /// <summary>
        /// 総重量
        /// </summary>
        public decimal? stackJyuuryou { get; set; }

        /// <summary>
        /// 空車重量
        /// </summary>
        public decimal? emptyJyuuryou { get; set; }

        /// <summary>
        /// 割振重量
        /// </summary>
        public decimal? warifuriJyuuryou { get; set; }

        /// <summary>
        /// 割振(%)
        /// </summary>
        public decimal? warifuriPercent { get; set; }

        /// <summary>
        /// 調整重量
        /// </summary>
        public decimal? chouseiJyuuryou { get; set; }

        /// <summary>
        /// 調整(%)
        /// </summary>
        public decimal? chouseiPercent { get; set; }

        /// <summary>
        /// 容器重量
        /// </summary>
        public decimal? youkiJyuuryou { get; set; }

        /// <summary>
        /// 正味重量
        /// </summary>
        public decimal? netJyuuryou { get; set; }
        /// <summary>        /// 荷姿数量
        /// </summary>
        public decimal? nisugatasuryou { get; set; }
        /// <summary>
        /// 荷姿単位CD
        /// </summary>
        public decimal? nisugataunitcd { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public JyuuryouDto()
        {
            this.stackJyuuryou = null;
            this.emptyJyuuryou = null;
            this.warifuriJyuuryou = null;
            this.warifuriPercent = null;
            this.chouseiJyuuryou = null;
            this.chouseiPercent = null;
            this.youkiJyuuryou = null;
        }
        #endregion

        #region getter, setter
        /// <summary>
        /// 総重量をdecimal型で返す
        /// </summary>
        /// <returns></returns>
        public decimal StackJyuuryouToDecimal()
        {
            if (this.stackJyuuryou == null)
            {
                return 0;
            }

            return (decimal)this.stackJyuuryou;
        }

        /// <summary>
        /// 空車重量をDecimal型で返す
        /// </summary>
        /// <returns></returns>
        public decimal WarifuriJyuuryouToDecimal()
        {
            if (this.warifuriJyuuryou == null)
            {
                return 0;
            }

            return (decimal)this.warifuriJyuuryou;
        }

        /// <summary>
        /// 割振kgをDecimal型で返す
        /// </summary>
        /// <returns></returns>
        public decimal EmptyJyuuryouToDecimal()
        {
            if (this.emptyJyuuryou == null)
            {
                return 0;
            }

            return (decimal)this.emptyJyuuryou;
        }

        /// <summary>
        /// 割振(%)をDecimal型で返す
        /// </summary>
        /// <returns></returns>
        public decimal WarifuriPercentToDecimal()
        {
            if (this.warifuriPercent == null)
            {
                return 0;
            }

            return (decimal)this.warifuriPercent;
        }

        /// <summary>
        /// 調整kgをDecimal型で返す
        /// </summary>
        /// <returns></returns>
        public decimal ChouseiJyuuryouToDecimal()
        {
            if (this.chouseiJyuuryou == null)
            {
                return 0;
            }

            return (decimal)this.chouseiJyuuryou;
        }

        /// <summary>
        /// 調整%をDecimal型で返す
        /// </summary>
        /// <returns></returns>
        public decimal ChouseiPercentToToDecimal()
        {
            if (this.chouseiPercent == null)
            {
                return 0;
            }

            return (decimal)this.chouseiPercent;
        }

        /// <summary>
        /// 容器をDecimal型で返す
        /// </summary>
        /// <returns></returns>
        public decimal YoukiJyuuryouToDecimal()
        {
            if (this.youkiJyuuryou == null)
            {
                return 0;
            }

            return (decimal)this.youkiJyuuryou;
        }

        /// <summary>
        /// 正味重量をDecimal型で返す
        /// </summary>
        /// <returns></returns>
        public decimal NetJyuuryouToDecimal()
        {
            if (this.netJyuuryou == null)
            {
                return 0;
            }

            return (decimal)this.netJyuuryou;
        }
        #endregion

        #region ユーティリティ
        /// <summary>
        /// 引数内で割振重量と正味重量の再計算(追加時用)
        /// List内の構成は、index1のみにstackJyuuryou, emptyJyuuryouが設定されている
        /// ことを想定し処理します
        /// </summary>
        /// <param name="jyuuryouDtoList">計算対象のList</param>
        /// <param name="jyuuryouTargetFlag">計算方法の対象を決定するフラグ。true: 割振重量, false: 割振(%)</param>
        /// <param name="valueHasuCd">割振値端数CD</param>
        /// <param name="valueKetaKeta">割振値端数処理桁</param>
        /// <param name="percentHasuCd">割振割合端数CD</param>
        /// <param name="percentHasuKeta">割振割合端数処理桁</param>
        public static void CalcJyuuryouDtoForAdd(List<JyuuryouDto> jyuuryouDtoList, 
            bool jyuuryouTargetFlag, short valueHasuCd = 1, short valueKetaKeta = 1, short percentHasuCd = 1, short percentHasuKeta = 3)
        {
            LogUtility.DebugMethodStart(jyuuryouDtoList, jyuuryouTargetFlag, valueHasuCd, valueKetaKeta, percentHasuCd, percentHasuKeta);

            if (jyuuryouDtoList == null || jyuuryouDtoList.Count < 1)
            {
                return;
            }

            decimal stackJyuuryou = 0;    // 総重量(ループ中は加減しない)
            decimal emptyJyuuryou = 0;    // 空車重量(ループ中は加減しない)
            decimal netJyuuryou = 0;      // List全体を把握するための正味重量(ループ中に加減する)
            int i = 0;

            // 先頭indexから計算のため情報を取得
            JyuuryouDto firstJyuuryouDto = jyuuryouDtoList[0];

            // 計算可能な行かを確認
            if (firstJyuuryouDto.stackJyuuryou == null)
            {
                return;
            }

            // 先頭のみに総重量、空車重量が格納されているはず
            stackJyuuryou = firstJyuuryouDto.StackJyuuryouToDecimal();
            emptyJyuuryou = firstJyuuryouDto.EmptyJyuuryouToDecimal();
            netJyuuryou = stackJyuuryou - emptyJyuuryou;
            decimal percentSum = 0;   // 割振%の合計値を計算
            bool overCountFlag = false;     // 引数のリストを超えた場合にtrueにする

            // 正味重量がある限りListの上から順番に計算していく
            while (0 < netJyuuryou)
            {
                JyuuryouDto jyuuryouDto = new JyuuryouDto();

                if (i < jyuuryouDtoList.Count)
                {
                    // Listに情報がある場合は取得
                    jyuuryouDto = jyuuryouDtoList[i];
                    overCountFlag = false;
                }
                else
                {
                    // Listにない場合は新規作成(ループの最初にnewしているから、ここでnewする必要は無い)
                    overCountFlag = true;
                }

                if (netJyuuryou <= 0)
                {
                    // 正味重量が計算し終わった
                    break;
                }

                /**
                 * 割振kg, 割振(%)計算
                 */
                // 割振系データの計算
                if (jyuuryouTargetFlag)
                {
                    if (!jyuuryouDto.isValidWarifuriJyuuryou())
                    {
                        // 新規行のはずなので残りの割振kgを設定
                        jyuuryouDto.warifuriJyuuryou = netJyuuryou;
                    }
                    // 割振%計算
                    jyuuryouDto.warifuriPercent =
                        (decimal)CommonCalc.FractionCalc(
                        (decimal)(jyuuryouDto.warifuriJyuuryou / (stackJyuuryou - emptyJyuuryou)) * 100,
                        percentHasuCd,
                        percentHasuKeta);

                    // 画面では100%表示なので、掛ける100する
                    // jyuuryouDto.warifuriPercent = jyuuryouDto.warifuriPercent * 100;
                }
                else
                {
                    if (jyuuryouDto.isValidWarifuriPercent())
                    {
                        // 割振重量計算
                        jyuuryouDto.warifuriJyuuryou =
                            (decimal)CommonCalc.FractionCalc(
                            (decimal)((jyuuryouDto.warifuriPercent / 100) * (stackJyuuryou - emptyJyuuryou)),
                            valueHasuCd,
                            valueKetaKeta);
                    }
                    else
                    {
                        // 新規行のはずなので残りの割振kgと割振%を設定
                        jyuuryouDto.warifuriJyuuryou = netJyuuryou;
                        jyuuryouDto.warifuriPercent = 100 - percentSum;
                    }
                }

                percentSum += jyuuryouDto.WarifuriPercentToDecimal();

                /**
                 * 正味重量計算
                 */
                if (!jyuuryouDto.isValidWarifuriJyuuryou()
                    && !jyuuryouDto.isValidWarifuriPercent())
                {
                    // ブランク or 0の場合
                    // 割振系のデータがない場合はそのまま正味重量に値をセットする
                    jyuuryouDto.netJyuuryou = netJyuuryou 
                                                - jyuuryouDto.ChouseiJyuuryouToDecimal() 
                                                - jyuuryouDto.YoukiJyuuryouToDecimal();
                    netJyuuryou = 0;
                }
                else
                {
                    jyuuryouDto.netJyuuryou = jyuuryouDto.WarifuriJyuuryouToDecimal()
                                                - jyuuryouDto.ChouseiJyuuryouToDecimal()
                                                - jyuuryouDto.YoukiJyuuryouToDecimal();
                    netJyuuryou -= jyuuryouDto.WarifuriJyuuryouToDecimal();
                }

                // もし割振重量と正味重量が一致していれば割振系の情報は表示しない
                if ((stackJyuuryou - emptyJyuuryou) == jyuuryouDto.WarifuriJyuuryouToDecimal())
                {
                    jyuuryouDto.warifuriJyuuryou = null;
                    jyuuryouDto.warifuriPercent = null;
                }

                /**
                 * jyuuryouDtoオブジェクトの更新
                 */
                if (overCountFlag)
                {
                    // 一番下に新規追加
                    jyuuryouDtoList.Add(jyuuryouDto);
                }
                else
                {
                    // 同じindexのオブジェクトを更新
                    // 参照渡しで計算しているので恐らくここでは何もする必要は無いはず
                    jyuuryouDtoList[i] = jyuuryouDto;
                }

                i++;
                
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 有効な割振重量か返す
        /// </summary>
        /// <returns>true: 有効, false: 無効</returns>
        private bool isValidWarifuriJyuuryou()
        {
            return 0 < this.WarifuriJyuuryouToDecimal();
        }

        /// <summary>
        /// 有効な割振(%)か返す
        /// </summary>
        /// <returns>true: 有効, false: 無効</returns>
        private bool isValidWarifuriPercent()
        {
            return 0 < this.WarifuriPercentToDecimal();
        }
        #endregion
    }
}
