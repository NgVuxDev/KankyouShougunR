
using System.Collections.Generic;
using System.Linq;
using r_framework.CustomControl;
using r_framework.Dto;
namespace r_framework.Logic
{
    /// <summary>
    /// MultiRowのインデックスを生成する処理
    /// </summary>
    public class MultiRowIndexCreateLogic
    {
        /// <summary>
        /// MultiRowの設定プロパティ
        /// </summary>
        public GcCustomMultiRow multiRow { get; set; }

        /// <summary>
        /// MultiRowのロケーションを格納しているディクショナリークラス
        /// </summary>
        private Dictionary<int, List<MultiRowLocationDto>> list = new Dictionary<int, List<MultiRowLocationDto>>();

        /// <summary>
        /// ソート処理用の情報格納
        /// </summary>
        public List<MultiRowLocationDto> sortEndList { get; set; }

        /// <summary>
        /// ローケーション情報を含むMultiRowLocationDtoのリストを作成
        /// </summary>
        public void CreateLocations()
        {
            for (int i = 0; i < this.multiRow.ColumnHeaders[0].Cells.Count; i++)
            {
                MultiRowLocationDto test = new MultiRowLocationDto();

                test.LocationX = this.multiRow.ColumnHeaders[0].Cells[i].Location.X;
                test.LocationY = this.multiRow.ColumnHeaders[0].Cells[i].Location.Y;
                test.Cells = this.multiRow.ColumnHeaders[0].Cells[i];

                if (list.ContainsKey(test.LocationY))
                {
                    list[test.LocationY].Add(test);
                }
                else
                {
                    List<MultiRowLocationDto> inputList = new List<MultiRowLocationDto>();
                    inputList.Add(test);
                    list.Add(test.LocationY, inputList);
                }

            }

            this.DoSort();

        }
        /// <summary>
        /// ソート処理の実施
        /// </summary>
        private void DoSort()
        {
            IOrderedEnumerable<KeyValuePair<int, List<MultiRowLocationDto>>> sortedList = list.OrderBy(pair => pair.Key);
            sortEndList = new List<MultiRowLocationDto>();
            foreach (KeyValuePair<int, List<MultiRowLocationDto>> param in sortedList)
            {
                param.Value.Sort(MultiRowLocationDto.ComparisonX);

                foreach (var multiRow in param.Value)
                {
                    sortEndList.Add(multiRow);
                }
            }
        }
    }
}
