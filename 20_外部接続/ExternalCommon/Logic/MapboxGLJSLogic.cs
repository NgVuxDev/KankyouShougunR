using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.DirectionsAPI;
using Shougun.Printing.Common;
using System.Data.SqlTypes;
using System.Threading.Tasks;


namespace Shougun.Core.ExternalConnection.ExternalCommon.Logic
{
    /// <summary>
    /// MapboxのGLJS(地図表示)に関する処理を定義
    /// </summary>
    public class MapboxGLJSLogic
    {
        #region フィールド

        /// <summary>システム設定Dao</summary>
        private IM_SYS_INFODao sysInfoDao;
        /// <summary>mapboxアクセスカウントDao</summary>
        private IS_MAPBOX_ACCESS_COUNTDao mapboxDao;

        /// <summary>メッセージ</summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>ソース</summary>
        List<mapboxArrayList> allList;
        /// <summary>マーカー用のDtoのリスト</summary>
        List<mapboxMarkerDto> markerDtoList;
        /// <summary>順番用のDtoのリスト</summary>
        List<mapboxRowNoDto> rowNoDtoList;
        /// <summary>ルート用のDtoのリスト</summary>
        List<mapboxRootDto> rootDtoList;

        // 配車割当用
        /// <summary>割当済み</summary>
        List<mapboxArrayList> wariateList;
        /// <summary>未配車</summary>
        List<mapboxArrayList> mihaishaList;
        /// <summary>コンテナ</summary>
        List<mapboxArrayList> contenaList;


        /// <summary>システム設定のEntity</summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>画面ID</summary>
        WINDOW_ID windowId;

        string firstLayerId;

        /// <summary>
        /// 業者Dao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;
        /// <summary>
        /// 現場Dao
        /// </summary>
        private IM_GENBADao genbaDao;
        /// <summary>
        /// 都道府県Dao
        /// </summary>
        private IM_TODOUFUKENDao todoufukenDao;

        private IM_CONTENA_KEIKA_DATEDao contenaKeikaDao;

        #endregion

        #region プロパティ

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MapboxGLJSLogic()
        {
            // DAO
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mapboxDao = DaoInitUtility.GetComponent<IS_MAPBOX_ACCESS_COUNTDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.todoufukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.contenaKeikaDao = DaoInitUtility.GetComponent<IM_CONTENA_KEIKA_DATEDao>();

            // メッセージ用
            this.msgLogic = new MessageBoxShowLogic();
            // ソース
            this.allList = new List<mapboxArrayList>();
            // マーカー用Dtoのリスト
            this.markerDtoList = new List<mapboxMarkerDto>();
            // 順番用Dtoのリスト
            this.rowNoDtoList = new List<mapboxRowNoDto>();
            // ルート用Dtoのリスト
            this.rootDtoList = new List<mapboxRootDto>();
            // 配車割当用
            this.wariateList = new List<mapboxArrayList>();
            this.mihaishaList = new List<mapboxArrayList>();
            this.contenaList = new List<mapboxArrayList>();
        }
        #endregion

        #region 共通入り口

        /// <summary>
        /// mapbox地図展開
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="windowId"></param>
        public void mapbox_HTML_Open(List<mapDtoList> dtoLists, WINDOW_ID winId)
        {
            // M_SYS_INFOの読み込み
            this.LoadSysInfo();

            this.windowId = winId;

            string html = string.Empty;

            var firstLayer = (from a in dtoLists orderby a.layerId select a).First();
            this.firstLayerId = (firstLayer.layerId - 1).ToString();

            // 出力前にHTMLファイルを削除
            this.htmlFileDelete();

            // S_MAPBOX_ACCESS_TOKENに登録
            this.accessCountRegist();

            // 並び替えを行う
            if (this.windowId == WINDOW_ID.T_TEIKI_HAISHA)
            {
                // 並び替え
                foreach (mapDtoList item in dtoLists)
                {
                    item.dtos.Sort(CompareAccount);
                }
            }

            // 受け取ったDTOから出力データを作成する
            this.createDtos(dtoLists);

            // 出力用データからHTML作成
            html = this.createHTML();

            // ファイルパスの設定
            string filepath = getFilePath();

            // HTMLファイル出力
            if (!string.IsNullOrEmpty(filepath))
            {
                using (var writer = new StreamWriter(filepath + @"\map.html", false))
                {
                    writer.WriteLine(html);
                }
            }
            else
            {
                this.msgLogic.MessageBoxShowError("地図表示の設定が未完了です。印刷設定画面で出力先フォルダの登録を行ってください");
                return;
            }
        }

        #endregion

        #region DTO作成

        /// <summary>
        /// 受け取ったDTOから出力データを作成する
        /// </summary>
        /// <param name="dto"></param>
        private void createDtos(List<mapDtoList> dtoLists)
        {
            foreach (mapDtoList dtoList in dtoLists)
            {
                // マーカー
                this.markerDtoList.Add(this.createMarkerDto(dtoList));

                if (this.No_Output())
                {
                    // 順番
                    this.rowNoDtoList.Add(this.createRowNoDto(dtoList));
                }

                if (this.Root_Output())
                {
                    // ルート
                    // 緯度経度の情報が2件以上
                    // かつ「組込」データでない場合のみルート作成
                    if (dtoList.dtos.Where(a => a.latitude != "").Count() > 1 &&
                        dtoList.dtos.Where(a => a.NoCount == false).Count() > 0)
                    {
                        this.rootDtoList.Add(this.createRootDto(dtoList));
                    }
                }
            }

            // マーカー用のfeatureから取り出してリスト用のDTOを作成
            foreach (mapboxMarkerDto dto in this.markerDtoList)
            {
                foreach (mapboxArrayList arrayList in dto.source.data.features)
                {
                    this.allList.Add(arrayList);
                    if (this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY)
                    {
                        // マーカー用dtoは配車割当の定期データが、余分に1件作られているので省く
                        if (arrayList.properties.dataKBN == "3" && this.NullToSpace(arrayList.properties.rowNo) == string.Empty) { continue; }

                        if (arrayList.properties.dataShurui == "0")
                        {
                            this.wariateList.Add(arrayList);
                        }
                        else if (arrayList.properties.dataShurui == "1")
                        {
                            this.mihaishaList.Add(arrayList);
                        }
                        else
                        {
                            this.contenaList.Add(arrayList);
                        }
                    }
                }
            }

            // マーカー用のDTOから緯度経度なしの情報を削除する
            foreach (mapboxMarkerDto dto in this.markerDtoList.ToArray())
            {
                foreach (mapboxArrayList arrayList in dto.source.data.features.ToArray())
                {
                    if (arrayList.geometry.Latitude == "")
                    {
                        dto.source.data.features.Remove(arrayList);
                    }
                }
            }
        }

        #endregion

        #region HTMLファイル作成

        /// <summary>
        /// テンプレートを利用してHTMLを出力する
        /// </summary>
        private string createHTML()
        {
            string tmp = string.Empty;

            // メニューIDからHTMLテンプレートファイルを返す
            string template = MapboxConst.ReturnTemplateString(this.windowId);

            // HTMLテンプレートファイルを読み込んで文字列に変換
            string html = this.readHTMLConvertToString(template);

            // テンプレートファイルの@Titleを書き換える
            string title = MapboxConst.ReturnMenuName(this.windowId) + " - 地図表示";
            html = html.Replace("@Title", title);

            // テンプレートファイルの@accessTokenを書き換える
            html = html.Replace("@accessToken", this.sysInfoEntity.MAPBOX_ACCESS_TOKEN);

            // テンプレートファイルの@styleを書き換える
            html = html.Replace("@style", this.sysInfoEntity.MAPBOX_MAP_STYLE);

            // テンプレートファイルの@allSourceを書き換える
            html = html.Replace("@allSource", convertDtoToString(this.allList));

            // テンプレートファイルの@kanriHououを書き換える
            html = html.Replace("@kanriHouou", this.sysInfoEntity.CONTENA_KANRI_HOUHOU.ToString());

            // テンプレートファイルの@startLayerIdを書き換える
            html = html.Replace("@StartLayerId", this.firstLayerId);

            // 配車割当専用
            if (windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY)
            {
                // テンプレートファイルのSourceを書き換える
                html = html.Replace("@wariateSource", convertDtoToString(this.wariateList));
                html = html.Replace("@mihaishaSource", convertDtoToString(this.mihaishaList));
                html = html.Replace("@contenaSource", convertDtoToString(this.contenaList));
                // ラベル
                string mapHeader = string.Empty;
                string mapHeader2 = string.Empty;
                string wariateCount = string.Empty;
                string contenaCount = string.Empty;
                string mihaishaCount = string.Empty;
                this.wariateInfo(out mapHeader, out mapHeader2, out wariateCount, out contenaCount, out mihaishaCount);
                html = html.Replace("@header", mapHeader);
                html = html.Replace("@secondHeader", mapHeader2);
                html = html.Replace("@wariateCount", wariateCount);
                html = html.Replace("@contenaCount", contenaCount);
                html = html.Replace("@mihaishaCount", mihaishaCount);
            }

            // MAPの中心点を算出する
            string centerLat = string.Empty;
            string centerLng = string.Empty;
            string zoom = string.Empty;
            this.MapCenterCalculation(this.allList, out centerLat, out centerLng, out zoom);

            // allListに緯度経度なしデータのみ格納されているケースもある
            if (centerLat == string.Empty || centerLat == "0")
            {
                // 初期値として日本の中心をセット
                centerLat = "34.78549065";
                centerLng = "134.37559305";
                zoom = "4";
            }

            // テンプレートファイルの@Centerを書き換える
            html = html.Replace("@Center", centerLng + ", " + centerLat);
            // テンプレートファイルの@Zoomを書き換える
            html = html.Replace("@Zoom", zoom);

            // 設置コンテナ一覧だけ表示非表示の切り替えが特殊になる
            if (this.windowId == WINDOW_ID.T_CONTENA_ICHIRAN)
            {
                string visibleChange = string.Empty;
                this.VisibleChange(this.allList, out visibleChange);
                html = html.Replace("@VisibleChange", visibleChange);
            }


            // 一斉表示ポップアップ関連の値差し替え
            string popupSource = string.Empty;
            string popupAdd = string.Empty;
            string popupRemove = string.Empty;
            this.MapPopup(this.allList, out popupSource, out popupAdd, out popupRemove);
            html = html.Replace("@PopupSource", popupSource);
            html = html.Replace("@PopupAdd", popupAdd);
            html = html.Replace("@PopupRemove", popupRemove);

            if (this.windowId == WINDOW_ID.T_UKETSUKE_ICHIRAN)
            {
                html = html.Replace("@Ichiran", "1");
            }
            else
            {
                html = html.Replace("@Ichiran", "0");
            }

            #region map.on('load', function() {

            // マーカー用のレイヤーを追加する
            tmp = string.Empty;
            foreach (mapboxMarkerDto dto in this.markerDtoList)
            {
                tmp += "map.addLayer(";
                tmp += convertDtoToString(dto);
                tmp += ");" + Environment.NewLine;
            }
            html = html.Replace("@Cource", tmp);

            // 順番表示部分の値差し替え
            tmp = string.Empty;
            if (this.No_Output())
            {
                foreach (mapboxRowNoDto rowNoDto in this.rowNoDtoList)
                {
                    tmp += "map.addLayer(";
                    tmp += convertDtoToString(rowNoDto);
                    tmp += ");" + Environment.NewLine;
                }
            }
            html = html.Replace("@NumberLayer", tmp);

            // ルート情報を書き換え
            tmp = string.Empty;
            if (this.Root_Output())
            {
                foreach (mapboxRootDto rootDto in this.rootDtoList)
                {
                    tmp += "map.addLayer(";
                    tmp += convertDtoToString(rootDto);
                    tmp += ");" + Environment.NewLine;
                }
            }
            html = html.Replace("@Root", tmp);

            // mouseoverイベントの差し替え
            string mouseOver = string.Empty;
            this.mouseOverEvent(this.markerDtoList, out mouseOver);
            html = html.Replace("@mouseover", mouseOver);

            // mouseleaveイベントの差し替え
            string mouseLeave = string.Empty;
            this.mouseLeaveEvent(this.markerDtoList, out mouseLeave);
            html = html.Replace("@mouseleave", mouseLeave);

            #endregion

            // 変数名を変換(DTOの変数名に「-」が使えないためここで対応)
            html = convertDtoName(html);

            return html;
        }

        #endregion

        /// <summary>
        /// テンプレートを利用してHTMLを出力する(一覧からの表示用)
        /// </summary>
        public void createHTML2Kadoujyoukyou(List<ArrayListKadoujyoukyou> dto, WINDOW_ID windowId)
        {
            // M_SYS_INFOの読み込み
            this.LoadSysInfo();

            // ファイルパス
            ProcessMode pMode = ProcessMode.NotSet;
            if (SystemInformation.TerminalServerSession)
            {
                pMode = ProcessMode.CloudServerSideProcess;
            }
            else
            {
                pMode = ProcessMode.CloudClientSideProcess;
            }
            string filepath = LocalDirectories.GetMapDirectory(pMode) + @"\map.html";

            // メニューIDからHTMLテンプレートファイルを返す
            string template = MapboxConst.ReturnTemplateString(windowId);
            // HTMLテンプレートファイルを読み込んで文字列に変換
            string html = this.readHTMLConvertToString(template);

            // テンプレートファイルの@Titleを書き換える
            string title = MapboxConst.ReturnMenuName(windowId) + " - 地図表示";
            html = html.Replace("@Title", title);

            // テンプレートファイルの@Sourceを書き換える
            html = html.Replace("@Source", convertDtoToString(dto));

            // テンプレートファイルの@accessTokenを書き換える
            html = html.Replace("@accessToken", this.sysInfoEntity.MAPBOX_ACCESS_TOKEN);

            // テンプレートファイルの@styleを書き換える
            html = html.Replace("@style", this.sysInfoEntity.MAPBOX_MAP_STYLE);

            // MAPの中心点を算出する
            string centerLat = "34.78549065";
            string centerLng = "134.37559305";
            string zoom = "4";
            this.MapCenterCalculationKadujyoukyou(dto, out centerLat, out centerLng, out zoom);
            // テンプレートファイルの@Centerを書き換える
            html = html.Replace("@Center", centerLng + ", " + centerLat);
            // テンプレートファイルの@Zoomを書き換える
            html = html.Replace("@Zoom", zoom);

            // 一斉表示ポップアップ関連の値差し替え
            string popupSource = string.Empty;
            string popupAdd = string.Empty;
            string popupRemove = string.Empty;
            this.MapPopupKadoujyoukyou(dto, out popupSource, out popupAdd, out popupRemove);
            html = html.Replace("@PopupSource", popupSource);
            html = html.Replace("@PopupAdd", popupAdd);
            html = html.Replace("@PopupRemove", popupRemove);

            // HTMLファイル出力
            using (var writer = new StreamWriter(filepath, false))
            {
                writer.WriteLine(html);
            }
        }

        /// <summary>
        /// DTO(リスト型)を文字列に変換する モバイル状況一覧
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string convertDtoToString(List<ArrayListKadoujyoukyou> dto)
        {
            string jsonString = new JavaScriptSerializer().Serialize(dto);
            return jsonString;
        }


        #region map.on('load', function() {

        #region 地図に表示するマーカー生成

        /// <summary>
        /// マーカー用のDtoを作成する
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private mapboxMarkerDto createMarkerDto(mapDtoList dtoList)
        {
            // アイコンの名称を先に取得
            string iconImage = "";
            if (this.windowId == WINDOW_ID.T_UKETSUKE_ICHIRAN)
            {
                // 収集受付一覧のみ固定で割り振る
                if (dtoList.layerId == 0)
                {
                    iconImage = "Marker-SteelBlue";
                }
                else
                {
                    iconImage = "Marker-Red";
                }
            }
            else
            {
                if (dtoList.layerId == 0)
                {
                    // ID=0は赤とする
                    iconImage = "Marker-Red";
                }
                else
                {
                    iconImage = this.iconName(dtoList.layerId);
                }
            }

            List<mapboxArrayList> markerFeaturesList = new List<mapboxArrayList>();

            bool bolNoCount = false;

            foreach (mapDto dto in dtoList.dtos)
            {
                mapboxArrayListProperties markerProperties = new mapboxArrayListProperties();
                markerProperties.tabName = this.SetTabName(dto);
                markerProperties.torihikisakiCd = dto.torihikisakiCd;
                markerProperties.torihikisakiName = dto.torihikisakiName;
                markerProperties.gyoushaCd = dto.gyoushaCd;
                markerProperties.gyoushaName = dto.gyoushaName;
                markerProperties.genbaCd = dto.genbaCd;
                markerProperties.genbaName = dto.genbaName;
                markerProperties.address = dto.address;
                markerProperties.description = this.createDiscription(dto);
                markerProperties.rowNo = Convert.ToString(dto.rowNo);
                markerProperties.roundNo = Convert.ToString(dto.roundNo);
                markerProperties.hinmei = dto.hinmei;
                markerProperties.shuppatsuFlag = dto.shuppatsuFlag;
                if (this.windowId == WINDOW_ID.T_COURSE_HAISHA_IRAI)
                    markerProperties.windowId = 1;
                else
                    markerProperties.windowId = 0;

                // 受付一覧用
                markerProperties.denpyouNo = dto.teikiHaishaNo;
                markerProperties.sagyouDate = dto.sagyouDate;
                markerProperties.genbaChaku = dto.genbaChaku;
                markerProperties.shasyu = dto.shasyu;
                markerProperties.sharyou = dto.sharyou;
                markerProperties.driver = dto.driver;
                markerProperties.sijijikou1 = dto.sijijikou1;
                markerProperties.sijijikou2 = dto.sijijikou2;
                markerProperties.sijijikou3 = dto.sijijikou3;

                // 設置コンテナ一覧用
                markerProperties.contenaShuruiName = dto.contenaShuruiName;
                markerProperties.contenaName = dto.contenaName;
                markerProperties.secchiDate = dto.secchiDate;
                markerProperties.daisuu = dto.daisuu;
                markerProperties.daysCount = dto.daysCount;
                if (this.windowId == WINDOW_ID.T_CONTENA_ICHIRAN)
                {
                    markerProperties.layerColor = searchHTMLContenaColor(iconImage);
                }
                else
                {
                    markerProperties.layerColor = searchHTMLColor(iconImage);
                }

                // 配車割当用
                markerProperties.header = dto.header;
                markerProperties.header2 = dto.header2;
                markerProperties.dataShurui = dto.dataShurui;
                markerProperties.dataKBN = dto.dataKBN;
                markerProperties.NNGyoushaName = dto.NNGyoushaName;
                markerProperties.NNGenbaName = dto.NNGenbaName;
                markerProperties.NNAddress = dto.NNAddress;
                markerProperties.secchiChouhuku = dto.secchiChouhuku;

                // 配車割当一日定期表示用
                markerProperties.gyoushaName_2 = dto.gyoushaName_2;
                markerProperties.genbaName_2 = dto.genbaName_2;
                markerProperties.address_2 = dto.address_2;
                markerProperties.NNGyoushaName_2 = dto.NNGyoushaName_2;
                markerProperties.NNGenbaName_2 = dto.NNGenbaName_2;
                markerProperties.NNAddress_2 = dto.NNAddress_2;
                markerProperties.hinmei_2 = dto.hinmei_2;
                markerProperties.description_2 = this.createDiscription(dto, 1);

                List<double> coordinatesList = new List<double>();
                if (dto.latitude == "")
                {
                    coordinatesList.Add(0);
                    coordinatesList.Add(0);
                }
                else
                {
                    coordinatesList.Add(Convert.ToDouble(dto.longitude));
                    coordinatesList.Add(Convert.ToDouble(dto.latitude));
                }
                // 配車割当一日定期表示用
                List<double> coordinatesList_2 = new List<double>();
                if (this.NullToSpace(dto.latitude_2) == "")
                {
                    coordinatesList_2.Add(0);
                    coordinatesList_2.Add(0);
                }
                else
                {
                    coordinatesList_2.Add(Convert.ToDouble(dto.longitude_2));
                    coordinatesList_2.Add(Convert.ToDouble(dto.latitude_2));
                }

                mapboxArrayListGeometry markerGeometry = new mapboxArrayListGeometry();
                markerGeometry.type = "Point";
                markerGeometry.zoom = "4";
                markerGeometry.Latitude = dto.latitude;
                markerGeometry.Longitude = dto.longitude;
                markerGeometry.coordinates = coordinatesList;
                // 配車割当一日定期表示用
                markerGeometry.Latitude_2 = this.NullToSpace(dto.latitude_2);
                markerGeometry.Longitude_2 = this.NullToSpace(dto.longitude_2);
                markerGeometry.coordinates_2 = coordinatesList_2;

                mapboxArrayList markerFeatures = new mapboxArrayList();
                markerFeatures.type = "Feature";
                markerFeatures.properties = markerProperties;
                markerFeatures.geometry = markerGeometry;

                markerFeaturesList.Add(markerFeatures);

                // 配車割当の定期データ用
                #region 配車割当定期用

                if (this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY && dto.dataKBN == "3")
                {
                    markerProperties = new mapboxArrayListProperties();
                    markerProperties.tabName = this.SetTabName(dto);
                    markerProperties.gyoushaName = dto.gyoushaName_2;
                    markerProperties.genbaName = dto.genbaName_2;
                    markerProperties.address = dto.address_2;
                    markerProperties.NNGyoushaName = dto.NNGyoushaName_2;
                    markerProperties.NNGenbaName = dto.NNGenbaName_2;
                    markerProperties.NNAddress = dto.NNAddress_2;
                    markerProperties.hinmei = dto.hinmei_2;
                    markerProperties.description = this.createDiscription(dto, 1);

                    // 配車割当用
                    markerProperties.header = dto.header;
                    markerProperties.dataShurui = dto.dataShurui;
                    markerProperties.dataKBN = dto.dataKBN;

                    // 配車割当一日定期表示用
                    markerProperties.gyoushaName_2 = dto.gyoushaName;
                    markerProperties.genbaName_2 = dto.genbaName;
                    markerProperties.address_2 = dto.address;
                    markerProperties.description_2 = this.createDiscription(dto);
                    markerProperties.NNGyoushaName_2 = dto.NNGyoushaName;
                    markerProperties.NNGenbaName_2 = dto.NNGenbaName;
                    markerProperties.NNAddress_2 = dto.NNAddress;
                    markerProperties.hinmei_2 = dto.hinmei;

                    coordinatesList = new List<double>();
                    if (dto.latitude == "")
                    {
                        coordinatesList.Add(0);
                        coordinatesList.Add(0);
                    }
                    else
                    {
                        coordinatesList.Add(Convert.ToDouble(dto.longitude));
                        coordinatesList.Add(Convert.ToDouble(dto.latitude));
                    }
                    // 配車割当一日定期表示用
                    coordinatesList_2 = new List<double>();
                    if (this.NullToSpace(dto.latitude_2) == "")
                    {
                        coordinatesList_2.Add(0);
                        coordinatesList_2.Add(0);
                    }
                    else
                    {
                        coordinatesList_2.Add(Convert.ToDouble(dto.longitude_2));
                        coordinatesList_2.Add(Convert.ToDouble(dto.latitude_2));
                    }

                    markerGeometry = new mapboxArrayListGeometry();
                    markerGeometry.type = "Point";
                    markerGeometry.zoom = "4";
                    markerGeometry.Latitude = this.NullToSpace(dto.latitude_2);
                    markerGeometry.Longitude = this.NullToSpace(dto.longitude_2);
                    markerGeometry.coordinates = coordinatesList_2;
                    // 配車割当一日定期表示用
                    markerGeometry.Latitude_2 = dto.latitude;
                    markerGeometry.Longitude_2 = dto.longitude;
                    markerGeometry.coordinates_2 = coordinatesList;

                    markerFeatures = new mapboxArrayList();
                    markerFeatures.type = "Feature";
                    markerFeatures.properties = markerProperties;
                    markerFeatures.geometry = markerGeometry;

                    markerFeaturesList.Add(markerFeatures);
                }

                #endregion
                if (dto.NoCount)
                {
                    bolNoCount = true;
                }
                // 配車割当、設置コンテナの場合はアイコンをここで設定
                if ((this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY && dto.dataShurui == "2") ||
                    this.windowId == WINDOW_ID.T_CONTENA_ICHIRAN)
                {
                    iconImage = searchHTMLContenaColor(dto.daysCount);
                }
            }

            mapboxMarkerData markerData = new mapboxMarkerData();
            markerData.type = "FeatureCollection";
            markerData.features = markerFeaturesList;

            mapboxMarkerSource markerSource = new mapboxMarkerSource();
            markerSource.type = "geojson";
            markerSource.data = markerData;

            mapboxMarkerLayout markerLayout = new mapboxMarkerLayout();
            if (bolNoCount)
            {
                // コース入力、コース配車依頼入力の「組込」は赤
                markerLayout.iconImage = "Marker-Red";
            }
            else
            {
                markerLayout.iconImage = iconImage;
            }
            markerLayout.iconSize = 0.7;
            markerLayout.iconPadding = 0;
            markerLayout.iconAllowOverlap = true;
            markerLayout.visibility = "visible";

            mapboxMarkerDto markerDto = new mapboxMarkerDto();
            markerDto.id = "meisai" + dtoList.layerId;
            markerDto.type = "symbol";
            markerDto.source = markerSource;
            markerDto.layout = markerLayout;

            return markerDto;
        }

        #endregion

        #region ルート表示する場合

        #region 地図に表示する順番生成

        /// <summary>
        /// 順番用のDtoを作成する
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private mapboxRowNoDto createRowNoDto(mapDtoList dtoList)
        {
            // アイコンの名称を先に取得
            string iconImage = "";
            if (dtoList.layerId == 0)
            {
                // ID=0は赤とする
                iconImage = "Marker-Red";
            }
            else
            {
                iconImage = this.iconName(dtoList.layerId);
            }

            List<mapboxRowNoFeatures> rowNoFeaturesList = new List<mapboxRowNoFeatures>();

            foreach (mapDto dto in dtoList.dtos)
            {
                // 配車割当（一日）の処理、且つ開始地点の位置情報が空の場合、開始マーカーの編集をスキップする。
                bool startSkip = false;
                if (this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY && dto.latitude == "")
                {
                    startSkip = true;
                }

                List<double> coordinatesList = new List<double>();
                mapboxRowNoGeometry rowNoGeometry = new mapboxRowNoGeometry();
                mapboxRowNoProperties rowNoProperties = new mapboxRowNoProperties();
                mapboxRowNoFeatures rowNoFeatures = new mapboxRowNoFeatures();
                if (!startSkip)
                {
                    if (dto.latitude == "" || dto.NoCount)
                    {
                        continue;
                    }

                    coordinatesList.Add(Convert.ToDouble(dto.longitude));
                    coordinatesList.Add(Convert.ToDouble(dto.latitude));

                    rowNoGeometry.type = "Point";
                    rowNoGeometry.coordinates = coordinatesList;

                    // 出発地点がある場合、「出発」とする。
                    if (dto.shuppatsuFlag)
                    {
                        rowNoProperties.name = "出発";
                    }
                    else
                    {
                        if (this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY && dto.dataKBN == "3")
                        {
                            rowNoProperties.name = Convert.ToString(dto.rowNo) + "-始";
                        }
                        else
                        {
                            rowNoProperties.name = Convert.ToString(dto.rowNo);
                        }
                    }

                    if ((this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY && dto.dataShurui == "2") ||
                        this.windowId == WINDOW_ID.T_CONTENA_ICHIRAN)
                    {
                        // ブランクで渡せば黒を返してくれる
                        rowNoProperties.color = searchHTMLColor("");
                    }
                    else
                    {
                        rowNoProperties.color = searchHTMLColor(iconImage);
                    }
                    rowNoProperties.image = "circle";

                    rowNoFeatures.type = "Feature";
                    rowNoFeatures.properties = rowNoProperties;
                    rowNoFeatures.geometry = rowNoGeometry;

                    rowNoFeaturesList.Add(rowNoFeatures);
                }

                // 配車割当の定期データ用
                #region 配車割当定期用

                if (this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY && dto.dataKBN == "3")
                {
                    if (this.NullToSpace(dto.latitude_2) == "" || dto.NoCount)
                    {
                        continue;
                    }

                    coordinatesList = new List<double>();
                    coordinatesList.Add(Convert.ToDouble(dto.longitude_2));
                    coordinatesList.Add(Convert.ToDouble(dto.latitude_2));

                    rowNoGeometry = new mapboxRowNoGeometry();
                    rowNoGeometry.type = "Point";
                    rowNoGeometry.coordinates = coordinatesList;

                    rowNoProperties = new mapboxRowNoProperties();
                    rowNoProperties.name = Convert.ToString(dto.rowNo) + "-終";

                    if (this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY && dto.dataShurui == "2")
                    {
                        // ブランクで渡せば黒を返してくれる
                        rowNoProperties.color = searchHTMLColor("");
                    }
                    else
                    {
                        rowNoProperties.color = searchHTMLColor(iconImage);
                    }
                    rowNoProperties.image = "circle";

                    rowNoFeatures = new mapboxRowNoFeatures();
                    rowNoFeatures.type = "Feature";
                    rowNoFeatures.properties = rowNoProperties;
                    rowNoFeatures.geometry = rowNoGeometry;

                    rowNoFeaturesList.Add(rowNoFeatures);
                }

                #endregion
            }

            mapboxRowNoData rowNoData = new mapboxRowNoData();
            rowNoData.type = "FeatureCollection";
            rowNoData.features = rowNoFeaturesList;

            mapboxRowNoSource rowNoSource = new mapboxRowNoSource();
            rowNoSource.type = "geojson";
            rowNoSource.data = rowNoData;

            mapboxRowNoLayout rowNoLayout = new mapboxRowNoLayout();
            // iconは自由に差し替えできるようにしないといけない
            rowNoLayout.iconImage = "{image}-15";
            rowNoLayout.iconSize = 0;
            rowNoLayout.textField = "{name}";
            rowNoLayout.visibility = "visible";

            List<double> textOffsetList = new List<double>();
            textOffsetList.Add(0);
            textOffsetList.Add(0.8);
            rowNoLayout.textOffset = textOffsetList;

            rowNoLayout.textSize = 20;
            rowNoLayout.textAnchor = "top";
            rowNoLayout.iconAllowOverlap = true;
            rowNoLayout.textAllowOverlap = true;

            List<string> ColorList = new List<string>();
            ColorList.Add("get");
            ColorList.Add("color");
            mapboxRowNoPaint rowNoPaint = new mapboxRowNoPaint();
            rowNoPaint.textColor = ColorList;
            rowNoPaint.iconColor = ColorList;

            mapboxRowNoDto rowNoDto = new mapboxRowNoDto();
            rowNoDto.id = "rowNumber" + dtoList.layerId;
            rowNoDto.type = "symbol";
            rowNoDto.source = rowNoSource;
            rowNoDto.layout = rowNoLayout;
            rowNoDto.paint = rowNoPaint;

            return rowNoDto;
        }

        #endregion

        #region 地図に表示するルート作成

        /// <summary>
        /// ルート用のDtoを作成する
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private mapboxRootDto createRootDto(mapDtoList dtoList)
        {
            // アイコンの名称を先に取得
            string iconImage = "";
            if (dtoList.layerId == 0)
            {
                // ID=0は赤とする
                iconImage = "Marker-Red";
            }
            else
            {
                iconImage = this.iconName(dtoList.layerId);
            }

            List<List<double>> coodinatesList = new List<List<double>>();

            mapboxRootGeometry rootGeometry = new mapboxRootGeometry();
            rootGeometry.type = "LineString";
            // RESTAPIを利用してルート情報を取得
            coodinatesList = this.rootAPI(dtoList.dtos);
            rootGeometry.coordinates = coodinatesList;

            mapboxRootData rootData = new mapboxRootData();
            rootData.type = "Feature";
            rootData.geometry = rootGeometry;

            mapboxRootSource rootSource = new mapboxRootSource();
            rootSource.type = "geojson";
            rootSource.data = rootData;

            mapboxRootLayout rootLayout = new mapboxRootLayout();
            rootLayout.visibility = "visible";
            rootLayout.lineJoin = "round";
            rootLayout.lineCap = "round";

            mapboxRootPaint rootPaint = new mapboxRootPaint();
            rootPaint.lineColor = searchHTMLColor(iconImage);
            rootPaint.lineWidth = 7;

            mapboxRootDto rootDto = new mapboxRootDto();
            rootDto.id = "rootLayer" + dtoList.layerId;
            rootDto.type = "line";
            rootDto.source = rootSource;
            rootDto.layout = rootLayout;
            rootDto.paint = rootPaint;

            return rootDto;
        }

        #endregion

        #region ルート情報取得(API利用)

        /// <summary>
        /// RESTAPIを使ってルートの情報を取得する
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private List<List<double>> rootAPI(List<mapDto> dto)
        {
            MapboxAPILogic apiLogic = new MapboxAPILogic();
            DirectionsAPI result = null;
            List<mapboxArrayListGeometry> geometryList = new List<mapboxArrayListGeometry>();
            List<List<List<string>>> coordinatesList = new List<List<List<string>>>();

            // 地点情報文字列化
            foreach (mapDto rootDto in dto)
            {
                mapboxArrayListGeometry geo = new mapboxArrayListGeometry();
                if (string.IsNullOrEmpty(rootDto.latitude))
                {
                    continue;
                }
                else
                {
                    geo.Latitude = rootDto.latitude;
                    geo.Longitude = rootDto.longitude;
                    geometryList.Add(geo);
                }
                // 25件を超えると一括でAPIを飛ばすとエラーになる
                // 上限に達した時点で一旦APIを飛ばす
                if (geometryList.Count == 25)
                {
                    // REST APIでルート情報を取得する
                    if (!apiLogic.HttpGET<DirectionsAPI>(geometryList, out result))
                    {
                        return null;
                    }

                    foreach (Route routes in result.routes)
                    {
                        coordinatesList.Add(routes.geometry.coordinates);
                    }

                    // 25件目の地点と次の地点のルートを補填するため
                    // 初期化して、最後の地点情報を設定し、次のループへ向かう
                    geometryList = new List<mapboxArrayListGeometry>();
                    geometryList.Add(geo);
                }
            }
            if (geometryList.Count != 0)
            {
                // 地点情報が１件の場合、ルート情報のAPIでエラーとなるため、
                // 必ず２件以上の地点がある場合にAPIを実行する。
                if (geometryList.Count >= 2)
                {
                    // REST APIでルート情報を取得する
                    if (!apiLogic.HttpGET<DirectionsAPI>(geometryList, out result))
                    {
                        return null;
                    }

                    foreach (Route routes in result.routes)
                    {
                        coordinatesList.Add(routes.geometry.coordinates);
                    }
                }
            }

            List<List<double>> ret = new List<List<double>>();
            foreach (List<List<string>> item in coordinatesList)
            {
                foreach (List<string> item2 in item)
                {
                    List<double> retList = new List<double>();
                    retList = item2.ConvertAll(x => double.Parse(x));
                    ret.Add(retList);
                }
            }

            return ret;
        }



        #endregion

        #endregion

        #region mouseoverイベント作成

        /// <summary>
        /// HTMLのmousemoveイベントを動的に作成する
        /// </summary>
        /// <param name="dtoList"></param>
        /// <param name="mouseOver"></param>
        private void mouseOverEvent(List<mapboxMarkerDto> dtoList, out string mouseOver)
        {
            mouseOver = string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (mapboxMarkerDto dto in dtoList)
            {
                sb.AppendLine("map.on('mouseover', '" + dto.id + "', function(e) {");
                sb.AppendLine("map.getCanvas().style.cursor = 'pointer';");
                sb.AppendLine("var feature = e.features[0];");
                sb.AppendLine("popup");
                sb.AppendLine(".setLngLat(feature.geometry.coordinates)");
                sb.AppendLine(".setHTML(feature.properties.description)");
                sb.AppendLine(".addTo(map);");
                sb.AppendLine("});");
            }
            mouseOver = sb.ToString();
        }

        #endregion

        #region mouseleaveイベント作成

        /// <summary>
        /// HTMLのmouseleaveイベントを動的に作成する
        /// </summary>
        /// <param name="dtoList"></param>
        /// <param name="mouseLeave"></param>
        private void mouseLeaveEvent(List<mapboxMarkerDto> dtoList, out string mouseLeave)
        {
            mouseLeave = string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (mapboxMarkerDto dto in dtoList)
            {
                sb.AppendLine("map.on('mouseleave', '" + dto.id + "', function() {");
                sb.AppendLine("map.getCanvas().style.cursor = '';");
                sb.AppendLine("popup.remove();");
                sb.AppendLine("});");
            }
            mouseLeave = sb.ToString();
        }

        #endregion

        #endregion

        #region その他共通処理

        #region 色の設定

        #region マーカー

        /// <summary>
        /// IconImageの色設定
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string iconName(int id)
        {
            string iconImage = string.Empty;
            int cnt = id - ((id / 38) * 38);
            if (cnt == 0) cnt = 38;
            switch (cnt)
            {
                case 1:
                    iconImage = "Marker-SteelBlue";
                    break;
                case 2:
                    iconImage = "Marker-BlueViolet";
                    break;
                case 3:
                    iconImage = "Marker-LimeGreen";
                    break;
                case 4:
                    iconImage = "Marker-Blue";
                    break;
                case 5:
                    iconImage = "Marker-Brown";
                    break;
                case 6:
                    iconImage = "Marker-Coral";
                    break;
                case 7:
                    iconImage = "Marker-Crimson";
                    break;
                case 8:
                    iconImage = "Marker-DarkGreen";
                    break;
                case 9:
                    iconImage = "Marker-DarkBlue";
                    break;
                case 10:
                    iconImage = "Marker-DarkCyan";
                    break;
                case 11:
                    iconImage = "Marker-DarkGoldenrod";
                    break;
                case 12:
                    iconImage = "Marker-DarkGray";
                    break;
                case 13:
                    iconImage = "Marker-DarkRed";
                    break;
                case 14:
                    iconImage = "Marker-DarkOrange";
                    break;
                case 15:
                    iconImage = "Marker-DeepPink";
                    break;
                case 16:
                    iconImage = "Marker-DeepSkyBlue";
                    break;
                case 17:
                    iconImage = "Marker-DimGray";
                    break;
                case 18:
                    iconImage = "Marker-ForestGreen";
                    break;
                case 19:
                    iconImage = "Marker-Gold";
                    break;
                case 20:
                    iconImage = "Marker-HotPink";
                    break;
                case 21:
                    iconImage = "Marker-Magenta";
                    break;
                case 22:
                    iconImage = "Marker-Maroon";
                    break;
                case 23:
                    iconImage = "Marker-MidnightBlue";
                    break;
                case 24:
                    iconImage = "Marker-Orange";
                    break;
                case 25:
                    iconImage = "Marker-RoyalBlue";
                    break;
                case 26:
                    iconImage = "Marker-Purple";
                    break;
                case 27:
                    iconImage = "Marker-Salmon";
                    break;
                case 28:
                    iconImage = "Marker-SeaGreen";
                    break;
                case 29:
                    iconImage = "Marker-Silver";
                    break;
                case 30:
                    iconImage = "Marker-Teal";
                    break;
                case 31:
                    iconImage = "Marker-Tan";
                    break;
                case 32:
                    iconImage = "Marker-Violet";
                    break;
                case 33:
                    iconImage = "Marker-Tomato";
                    break;
                case 34:
                    iconImage = "Marker-Turquoise";
                    break;
                case 35:
                    iconImage = "Marker-YellowGreen";
                    break;
                case 36:
                    iconImage = "Marker-Yellow";
                    break;
                case 37:
                    iconImage = "Marker-Peru";
                    break;
                case 38:
                    iconImage = "Marker-Black";
                    break;
                default:
                    break;
            }

            return iconImage;
        }

        /// <summary>
        /// iconImageからHTMLカラーコードを取得する
        /// </summary>
        /// <param name="iconImage"></param>
        /// <returns></returns>
        private string searchHTMLColor(string iconImage)
        {
            try
            {
                // iconImageの「Marker-」以降を切り取る
                string colorName = iconImage.Substring(7);
                // 色に変換
                Color color1 = Color.FromName(colorName);
                // HTML形式の文字列に変換
                string ret = ColorTranslator.ToHtml(color1);

                return ret;
            }
            catch
            {
                // うまく取得できなかったら黒で返しておく
                return "#000000";
            }
        }

        #endregion

        #region コンテナ

        /// <summary>
        /// コンテナ経過日数からHTMLカラーコードを取得する
        /// </summary>
        /// <param name="iconImage"></param>
        /// <returns></returns>
        private string searchHTMLContenaColor(string keikaDate)
        {
            string strColor = string.Empty;
            try
            {
                Color color = new Color();
                // 経過日数以内で一番大きい値を抽出M_CONTENA_KEIKA_DATEを抽出
                string sql = "SELECT TOP 1 CONTENA_KEIKA_BACK_COLOR FROM M_CONTENA_KEIKA_DATE WHERE DELETE_FLG = 0 AND CONTENA_KEIKA_DATE >= " + keikaDate + " ORDER BY CONTENA_KEIKA_DATE";
                DataTable dt = this.contenaKeikaDao.GetDateForStringSql(sql);
                if (dt.Rows.Count == 0)
                {
                    color = Color.SteelBlue;
                }
                else
                {
                    // 更新
                    foreach (DataRow row in dt.Rows)
                    {
                        color = Color.FromKnownColor((KnownColor)int.Parse(Convert.ToString(row["CONTENA_KEIKA_BACK_COLOR"])));
                    }
                }
                strColor = ColorTranslator.ToHtml(color);
            }
            catch
            {
                // うまく取得できなかったら黒で返しておく
                strColor = ColorTranslator.ToHtml(Color.SteelBlue);
            }
            //return strColor;

            //return "Marker-" + strColor;
            // コンテナ画像入れたらこちらに変更すること
            return "Contena-" + strColor;
        }


        #endregion

        #endregion

        #region ポップアップ表示内容作成

        /// <summary>
        /// 画面から渡されたmapDtoから、ポップアップに表示する内容を生成
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string createDiscription(mapDto dto, int wariateFlg = 0)
        {
            StringBuilder sb = new StringBuilder();

            if (this.windowId == WINDOW_ID.T_CONTENA_ICHIRAN)
            {
                #region コンテナ一覧

                // 重複
                sb.Append("<p>");
                sb.AppendFormat("重複：{0}", dto.secchiChouhuku);
                sb.Append("</p>");

                // コンテナ種類名
                sb.Append("<p>");
                sb.AppendFormat("コンテナ種類名：{0}", dto.contenaShuruiName);
                sb.Append("</p>");

                if (this.sysInfoEntity.CONTENA_KANRI_HOUHOU == 2)
                {
                    // コンテナ名
                    sb.Append("<p>");
                    sb.AppendFormat("コンテナ名：{0}", dto.contenaName);
                    sb.Append("</p>");
                }

                // 業者名
                sb.Append("<p>");
                sb.AppendFormat("業者名：{0}", dto.gyoushaName);
                sb.Append("</p>");

                // 現場名
                sb.Append("<p>");
                sb.AppendFormat("現場名：{0}", dto.genbaName);
                sb.Append("</p>");

                if (this.sysInfoEntity.CONTENA_KANRI_HOUHOU == 1)
                {
                    // 設置台数
                    sb.Append("<p>");
                    sb.AppendFormat("設置台数：{0}台", dto.daisuu);
                    sb.Append("</p>");

                    // 最終更新日/設置日
                    sb.Append("<p>");
                    sb.AppendFormat("最終更新日：{0}", dto.secchiDate);
                    sb.Append("</p>");

                    // 無回転日数/経過日数
                    sb.Append("<p>");
                    sb.AppendFormat("無回転日数：{0}日", dto.daysCount);
                    sb.Append("</p>");
                }
                else
                {
                    // 設置日
                    sb.Append("<p>");
                    sb.AppendFormat("設置日：{0}", dto.secchiDate);
                    sb.Append("</p>");

                    // 経過日数
                    sb.Append("<p>");
                    sb.AppendFormat("経過日数：{0}日", dto.daysCount);
                    sb.Append("</p>");
                }

                #endregion
            }
            else if (this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY)
            {
                #region 配車割当一日

                if (dto.dataShurui == "2")
                {
                    // 設置コンテナのデータのみ表示内容が異なる
                    #region 設置コンテナ

                    sb.Append("<p>");
                    sb.AppendFormat("重複：{0}", dto.secchiChouhuku).Append("<br>");
                    sb.AppendFormat("種類：{0}", dto.contenaShuruiName).Append("<br>");
                    sb.AppendFormat("名称：{0}", dto.contenaName).Append("<br>");
                    sb.AppendFormat("業者：{0}", dto.gyoushaName).Append("<br>");
                    sb.AppendFormat("現場：{0}", dto.genbaName).Append("<br>");
                    sb.AppendFormat("住所：{0}", dto.address).Append("<br>");
                    sb.AppendFormat("日数：{0}日", dto.daysCount).Append("<br>");
                    sb.Append("</p>");

                    #endregion
                }
                else if (dto.dataShurui != "2" && wariateFlg == 0)
                {
                    #region 割当済み、未配車

                    string header = string.Empty;
                    if (dto.dataKBN == "1")
                        header = "【" + dto.rowNo + "】" + dto.teikiHaishaNo + " － 収集受付";
                    else if (dto.dataKBN == "2")
                        header = "【" + dto.rowNo + "】" + dto.teikiHaishaNo + " － 出荷受付";
                    else if (dto.dataKBN == "3")
                        header = "【" + dto.rowNo + "】" + dto.teikiHaishaNo + " － 定期配車";

                    if (dto.dataKBN == "1" || dto.dataKBN == "2")
                    {
                        sb.Append("<p>");
                        sb.AppendFormat("{0}", header).Append("<br>");
                        sb.AppendFormat("現　　着：{0}", dto.genbaChaku).Append("<br>");
                        sb.AppendFormat("業　　者：{0}", dto.gyoushaName).Append("<br>");
                        sb.AppendFormat("現　　場：{0}", dto.genbaName).Append("<br>");
                        sb.AppendFormat("住　　所：{0}", dto.address).Append("<br>");
                        if (dto.dataKBN == "1")
                        {
                            sb.AppendFormat("荷降業者：{0}", dto.NNGyoushaName).Append("<br>");
                            sb.AppendFormat("荷降現場：{0}", dto.NNGenbaName).Append("<br>");
                        }
                        else if (dto.dataKBN == "2")
                        {
                            sb.AppendFormat("荷積業者：{0}", dto.NNGyoushaName).Append("<br>");
                            sb.AppendFormat("荷積現場：{0}", dto.NNGenbaName).Append("<br>");
                        }
                        sb.AppendFormat("住　　所：{0}", dto.NNAddress).Append("<br>");
                        sb.AppendFormat("コンテナ：{0}", dto.contenaName).Append("<br>");
                        sb.AppendFormat("品　　名：{0}", dto.hinmei).Append("<br>");
                        sb.Append("</p>");
                    }
                    else
                    {
                        sb.Append("<p>");
                        sb.AppendFormat("{0}", header).Append("<br>");
                        sb.AppendFormat("コース：{0}", dto.courseName).Append("<br>");
                        sb.AppendFormat("時　間：{0}", dto.genbaChaku).Append("<br>");
                        sb.AppendFormat("＜開始＞").Append("<br>");
                        sb.AppendFormat("業　者：{0}", dto.gyoushaName).Append("<br>");
                        sb.AppendFormat("現　場：{0}", dto.genbaName).Append("<br>");
                        sb.AppendFormat("住　所：{0}", dto.address).Append("<br>");
                        sb.AppendFormat("品　名：{0}", dto.hinmei).Append("<br>");
                        sb.Append("</p>");
                    }
                    #endregion
                }
                else
                {
                    #region 割当済み、未配車(定期のToデータ)

                    string header = string.Empty;
                    header = "【" + dto.rowNo + "】" + dto.teikiHaishaNo + " － 定期配車";

                    sb.Append("<p>");
                    sb.AppendFormat("{0}", header).Append("<br>");
                    sb.AppendFormat("コース：{0}", dto.courseName).Append("<br>");
                    sb.AppendFormat("時　間：{0}", dto.genbaChaku).Append("<br>");
                    sb.AppendFormat("＜終了＞").Append("<br>");
                    sb.AppendFormat("業　者：{0}", dto.gyoushaName_2).Append("<br>");
                    sb.AppendFormat("現　場：{0}", dto.genbaName_2).Append("<br>");
                    sb.AppendFormat("住　所：{0}", dto.address_2).Append("<br>");
                    sb.AppendFormat("品　名：{0}", dto.hinmei_2).Append("<br>");
                    sb.Append("</p>");

                    #endregion
                }
                #endregion
            }
            else if (this.windowId == WINDOW_ID.M_COURSE ||
                     this.windowId == WINDOW_ID.M_COURSE_ICHIRAN)
            {
                #region コース入力、コース一覧

                sb.Append("<strong>");
                sb.AppendFormat("コース：{0}", this.SetTabName(dto));
                sb.Append("</strong>");
                sb.Append("<p>");

                if (dto.shuppatsuFlag)
                {
                    sb.AppendFormat("順　番：出発").Append("<br>");
                }
                else
                {
                    if (dto.rowNo == 0)
                        sb.AppendFormat("順　番：組込").Append("<br>");
                    else
                        sb.AppendFormat("順　番：{0}（回数：{1}）", dto.rowNo, dto.roundNo).Append("<br>");
                }

                sb.AppendFormat("時　間：{0}", dto.genbaChaku).Append("<br>");
                sb.AppendFormat("業　者：{0}", dto.gyoushaName).Append("<br>");
                sb.AppendFormat("現　場：{0}", dto.genbaName).Append("<br>");
                sb.AppendFormat("住　所：{0}", dto.address).Append("<br>");
                sb.AppendFormat("品　名：{0}", dto.hinmei).Append("<br>");
                sb.Append("</p>");

                #endregion
            }
            else if (this.windowId == WINDOW_ID.T_UKETSUKE_ICHIRAN)
            {
                #region 収集出荷受付一覧

                // 番　号
                sb.Append("<strong>");
                sb.AppendFormat("番　号：{0}－{1}", dto.teikiHaishaNo, dto.courseName);
                sb.Append("</strong>");
                sb.Append("<p>");
                sb.AppendFormat("作業日：{0}", dto.sagyouDate).Append("<br>");
                sb.AppendFormat("現　着：{0}", dto.genbaChaku).Append("<br>");
                sb.AppendFormat("業　者：{0}", dto.gyoushaName).Append("<br>");
                sb.AppendFormat("現　場：{0}", dto.genbaName).Append("<br>");
                sb.AppendFormat("住　所：{0}", dto.address).Append("<br>");
                sb.AppendFormat("車　種：{0}", dto.shasyu).Append("<br>");
                sb.AppendFormat("車　輛：{0}", dto.sharyou).Append("<br>");
                sb.AppendFormat("運転者：{0}", dto.driver).Append("<br>");
                sb.AppendFormat("運転者指示事項：{0}", dto.sijijikou1).Append("<br>");
                if (!string.IsNullOrEmpty(dto.sijijikou2))
                    sb.AppendFormat("{0}", dto.sijijikou2).Append("<br>");
                if (!string.IsNullOrEmpty(dto.sijijikou3))
                    sb.AppendFormat("{0}", dto.sijijikou3).Append("<br>");
                sb.AppendFormat("品　名：{0}", dto.hinmei).Append("<br>");
                sb.Append("</p>");

                #endregion
            }
            else if (this.windowId == WINDOW_ID.T_TEIKI_HAISHA ||
                     this.windowId == WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN)
            {
                #region 定期配車入力、定期配車一覧

                // 番号
                sb.Append("<strong>");
                sb.AppendFormat("番号：{0}", this.SetTabName(dto));
                sb.Append("</strong>");
                sb.Append("<p>");

                if (dto.shuppatsuFlag)
                {
                    sb.AppendFormat("順番：出発").Append("<br>");
                }
                else
                {
                    sb.AppendFormat("順番：{0}（回数：{1}）", dto.rowNo, dto.roundNo).Append("<br>");
                }
                
                sb.AppendFormat("時間：{0}", dto.genbaChaku).Append("<br>");
                sb.AppendFormat("業者：{0}", dto.gyoushaName).Append("<br>");
                sb.AppendFormat("現場：{0}", dto.genbaName).Append("<br>");
                sb.AppendFormat("住所：{0}", dto.address).Append("<br>");
                sb.AppendFormat("品名：{0}", dto.hinmei).Append("<br>");
                sb.Append("</p>");

                #endregion
            }
            else if (this.windowId == WINDOW_ID.T_COURSE_HAISHA_IRAI)
            {
                #region コース配車依頼入力

                // 番号
                sb.Append("<strong>");
                sb.AppendFormat("番号：{0}", this.SetTabName(dto));
                sb.Append("</strong>");
                sb.Append("<p>");

                if (dto.shuppatsuFlag)
                {
                    sb.AppendFormat("順番：出発").Append("<br>");
                    sb.AppendFormat("時間：{0}", dto.genbaChaku).Append("<br>");
                }
                else
                {
                    if (dto.rowNo == 0)
                    {
                        sb.AppendFormat("順番：組込").Append("<br>");
                        sb.AppendFormat("現着：{0}", dto.genbaChaku).Append("<br>");
                    }
                    else
                    {
                        sb.AppendFormat("順番：{0}（回数：{1}）", dto.rowNo, dto.roundNo).Append("<br>");
                        sb.AppendFormat("時間：{0}", dto.genbaChaku).Append("<br>");
                    }
                }
                
                sb.AppendFormat("業者：{0}", dto.gyoushaName).Append("<br>");
                sb.AppendFormat("現場：{0}", dto.genbaName).Append("<br>");
                sb.AppendFormat("住所：{0}", dto.address).Append("<br>");
                sb.AppendFormat("品名：{0}", dto.hinmei).Append("<br>");
                sb.Append("</p>");
                #endregion
            }
            else
            {
                #region その他

                sb.Append("<strong>");
                if (!string.IsNullOrEmpty(dto.torihikisakiCd))
                {
                    sb.AppendFormat("<strong>{0}</strong>", dto.torihikisakiName);
                }
                else
                {
                    if (string.IsNullOrEmpty(dto.genbaName))
                    {
                        sb.AppendFormat("<strong>{0}</strong>", dto.gyoushaName);
                    }
                    else
                    {
                        sb.AppendFormat("<strong>{0}</strong>", dto.genbaName);
                    }
                }
                sb.Append("<p>");
                // 郵便番号
                if (!string.IsNullOrEmpty(dto.post))
                {
                    sb.AppendFormat("〒{0}", dto.post);
                }
                // 住所
                sb.AppendFormat("<br>{0}", dto.address);
                sb.Append("</p>");

                // 必要なら以下も
                // TEL
                if (!string.IsNullOrEmpty(dto.tel))
                {
                    sb.Append("<p>");
                    sb.AppendFormat("{0}", dto.tel);
                    sb.Append("</p>");
                }
                // 備考
                if (!string.IsNullOrEmpty(dto.bikou1))
                {
                    sb.Append("<p>");
                    sb.AppendFormat("{0}", dto.bikou1);
                    if (!string.IsNullOrEmpty(dto.bikou2))
                    {
                        sb.AppendFormat("<br>{0}", dto.bikou2);
                    }
                    sb.Append("</p>");
                }

                #endregion
            }
            return sb.ToString();
        }

        #endregion

        #region MAPの中心点、拡大率算出(private)

        /// <summary>
        /// 中心点算出
        /// </summary>
        /// <param name="dto">dto</param>
        /// <param name="centerLat">中心点緯度</param>
        /// <param name="centerLng">中心点経度</param>
        /// <param name="zoom">拡大率</param>
        /// <returns></returns>
        private void MapCenterCalculation(List<mapboxArrayList> dto, out string centerLat, out string centerLng, out string zoom)
        {
            centerLat = string.Empty;
            centerLng = string.Empty;
            zoom = string.Empty;

            double maxLat = 0;
            double minLat = 0;
            double maxLng = 0;
            double minLng = 0;

            try
            {
                if (maxLat == 0 || maxLat < dto.Where(x => x.geometry.Latitude != "").Max(x => Convert.ToDouble(x.geometry.Latitude)))
                {
                    maxLat = dto.Where(x => x.geometry.Latitude != "").Max(x => Convert.ToDouble(x.geometry.Latitude));
                }
                if (minLat == 0 || minLat > dto.Where(x => x.geometry.Latitude != "").Min(x => Convert.ToDouble(x.geometry.Latitude)))
                {
                    minLat = dto.Where(x => x.geometry.Latitude != "").Min(x => Convert.ToDouble(x.geometry.Latitude));
                }
                if (maxLng == 0 || maxLng < dto.Where(x => x.geometry.Longitude != "").Max(x => Convert.ToDouble(x.geometry.Longitude)))
                {
                    maxLng = dto.Where(x => x.geometry.Longitude != "").Max(x => Convert.ToDouble(x.geometry.Longitude));
                }
                if (minLng == 0 || minLng > dto.Where(x => x.geometry.Longitude != "").Min(x => Convert.ToDouble(x.geometry.Longitude)))
                {
                    minLng = dto.Where(x => x.geometry.Longitude != "").Min(x => Convert.ToDouble(x.geometry.Longitude));
                }

                // 中心点
                centerLat = Convert.ToString((maxLat + minLat) / 2);
                centerLng = Convert.ToString((maxLng + minLng) / 2);

                double difLat = maxLat - minLat;
                double difLng = maxLng - minLng;
                double difValue = difLat;

                // 緯度経度の差分で大きい方を利用
                if (difLat < difLng)
                {
                    difValue = difLng;
                }
                if (difValue < 0.015)
                {
                    zoom = "15";
                }
                else if (difValue < 0.025)
                {
                    zoom = "14.5";
                }
                else if (difValue < 0.038)
                {
                    zoom = "14";
                }
                else if (difValue < 0.045)
                {
                    zoom = "13.5";
                }
                else if (difValue < 0.075)
                {
                    zoom = "13";
                }
                else if (difValue < 0.1)
                {
                    zoom = "12";
                }
                else if (difValue < 0.3)
                {
                    zoom = "11";
                }
                else if (difValue < 0.53)
                {
                    zoom = "10";
                }
                else if (difValue < 1.1)
                {
                    zoom = "9";
                }
                else if (difValue < 2.45)
                {
                    zoom = "8";
                }
                else if (difValue < 4.65)
                {
                    zoom = "7";
                }
                else if (difValue < 9.4)
                {
                    zoom = "6";
                }
                else if (difValue < 10.6)
                {
                    zoom = "5.5";
                }
                else if (difValue < 11.4)
                {
                    zoom = "5";
                }
                else if (difValue < 16.5)
                {
                    zoom = "4.5";
                }
                else if (difValue < 23)
                {
                    zoom = "4";
                }
                else
                {
                    zoom = "4";
                }

                // -1して拡大率を下げる
                zoom = Convert.ToString(double.Parse(zoom) - 1);
            }
            catch
            {
                return;
            }
            return;
        }

        /// <summary>
        /// こっちを使っている
        /// </summary>
        /// <param name="dto">dto</param>
        /// <param name="centerLat">中心点緯度</param>
        /// <param name="centerLng">中心点経度</param>
        /// <param name="zoom">拡大率</param>
        /// <returns></returns>
        private void MapCenterCalculationKadujyoukyou(List<ArrayListKadoujyoukyou> dto, out string centerLat, out string centerLng, out string zoom)
        {
            centerLat = string.Empty;
            centerLng = string.Empty;
            zoom = string.Empty;

            double maxLat = 0;
            double minLat = 0;
            double maxLng = 0;
            double minLng = 0;

            try
            {

                if (maxLat == 0 || maxLat < dto.Max(x => Convert.ToDouble(x.geometry.Latitude)))
                {
                    maxLat = dto.Max(x => Convert.ToDouble(x.geometry.Latitude));
                }
                if (minLat == 0 || minLat > dto.Min(x => Convert.ToDouble(x.geometry.Latitude)))
                {
                    minLat = dto.Min(x => Convert.ToDouble(x.geometry.Latitude));
                }
                if (maxLng == 0 || maxLng < dto.Max(x => Convert.ToDouble(x.geometry.Longitude)))
                {
                    maxLng = dto.Max(x => Convert.ToDouble(x.geometry.Longitude));
                }
                if (minLng == 0 || minLng > dto.Min(x => Convert.ToDouble(x.geometry.Longitude)))
                {
                    minLng = dto.Min(x => Convert.ToDouble(x.geometry.Longitude));
                }

                // 中心点
                centerLat = Convert.ToString((maxLat + minLat) / 2);
                centerLng = Convert.ToString((maxLng + minLng) / 2);

                double difLat = maxLat - minLat;
                double difLng = maxLng - minLng;
                double difValue = difLat;

                // 緯度経度の差分で大きい方を利用
                if (difLat < difLng)
                {
                    difValue = difLng;
                }
                if (difValue < 0.015)
                {
                    zoom = "15";
                }
                else if (difValue < 0.025)
                {
                    zoom = "14.5";
                }
                else if (difValue < 0.038)
                {
                    zoom = "14";
                }
                else if (difValue < 0.045)
                {
                    zoom = "13.5";
                }
                else if (difValue < 0.075)
                {
                    zoom = "13";
                }
                else if (difValue < 0.1)
                {
                    zoom = "12";
                }
                else if (difValue < 0.3)
                {
                    zoom = "11";
                }
                else if (difValue < 0.53)
                {
                    zoom = "10";
                }
                else if (difValue < 1.1)
                {
                    zoom = "9";
                }
                else if (difValue < 2.45)
                {
                    zoom = "8";
                }
                else if (difValue < 4.65)
                {
                    zoom = "7";
                }
                else if (difValue < 9.4)
                {
                    zoom = "6";
                }
                else if (difValue < 10.6)
                {
                    zoom = "5.5";
                }
                else if (difValue < 11.4)
                {
                    zoom = "5";
                }
                else if (difValue < 16.5)
                {
                    zoom = "4.5";
                }
                else if (difValue < 23)
                {
                    zoom = "4";
                }
                else
                {
                    zoom = "4";
                }
            }
            catch
            {
                return;
            }
            return;
        }

        #endregion

        #region HTMLファイルの削除(public)

        /// <summary>
        /// ユーザーのローカルフォルダ(printing)にあるHTMLファイルを削除する
        /// </summary>
        public void htmlFileDelete()
        {
            // ファイルパスの設定
            string filepath = getFilePath();

            if (!Directory.Exists(filepath) || string.IsNullOrEmpty(filepath))
            {
                return;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(filepath);
            List<FileInfo> filesList = new List<FileInfo>(dirInfo.GetFiles("*.html"));
            foreach (var path in filesList)
            {
                if (File.Exists(path.FullName))
                {
                    try
                    {
                        File.Delete(path.FullName);
                    }
                    catch { }
                    continue;
                }
            }
        }

        #endregion

        #region S_MAPBOX_ACCESS_COUNTの更新(private)

        /// <summary>
        /// S_MAPBOX_ACCESS_COUNTを更新
        /// </summary>
        /// <param name="menuName">メニュー名</param>
        /// <returns></returns>
        public bool accessCountRegist(WINDOW_ID winId)
        {
            string sql = string.Empty;

            try
            {
                string menuName = MapboxConst.ReturnMenuName(winId);
                sql = string.Format("SELECT * FROM S_MAPBOX_ACCESS_COUNT WHERE USER_NAME='{0}' AND PC_NAME='{1}' AND MENU_NAME='{2}'",
                    SystemProperty.Shain.Name, AppConfig.LoginComputerName, menuName);
                DataTable dt = mapboxDao.GetDateForStringSql(sql);

                S_MAPBOX_ACCESS_COUNT dto = new S_MAPBOX_ACCESS_COUNT();
                if (dt.Rows.Count == 0)
                {
                    // 新規
                    dto.USER_NAME = SystemProperty.Shain.Name;
                    dto.PC_NAME = AppConfig.LoginComputerName;
                    dto.MENU_NAME = menuName;
                    dto.ACCESS_COUNT = 1;
                    dto.UPDATE_DATE = DateTime.Now;
                    mapboxDao.Insert(dto);
                }
                else
                {
                    // 更新
                    foreach (DataRow row in dt.Rows)
                    {
                        dto.USER_NAME = row["USER_NAME"].ToString();
                        dto.PC_NAME = row["PC_NAME"].ToString();
                        dto.MENU_NAME = row["MENU_NAME"].ToString();
                        dto.ACCESS_COUNT = (int)row["ACCESS_COUNT"] + 1;
                        dto.UPDATE_DATE = DateTime.Now;
                        dto.TIME_STAMP = (byte[])row["TIME_STAMP"];
                    }
                    mapboxDao.Update(dto);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("accessCountRegist", ex);
                this.msgLogic.MessageBoxShow("E245");
                return false;
            }
        }

        /// <summary>
        /// S_MAPBOX_ACCESS_COUNTを更新
        /// </summary>
        /// <param name="menuName">メニュー名</param>
        /// <returns></returns>
        private bool accessCountRegist()
        {
            string sql = string.Empty;

            try
            {
                string menuName = MapboxConst.ReturnMenuName(this.windowId);
                sql = string.Format("SELECT * FROM S_MAPBOX_ACCESS_COUNT WHERE USER_NAME='{0}' AND PC_NAME='{1}' AND MENU_NAME='{2}'",
                    SystemProperty.Shain.Name, AppConfig.LoginComputerName, menuName);
                DataTable dt = mapboxDao.GetDateForStringSql(sql);

                S_MAPBOX_ACCESS_COUNT dto = new S_MAPBOX_ACCESS_COUNT();
                if (dt.Rows.Count == 0)
                {
                    // 新規
                    dto.USER_NAME = SystemProperty.Shain.Name;
                    dto.PC_NAME = AppConfig.LoginComputerName;
                    dto.MENU_NAME = menuName;
                    dto.ACCESS_COUNT = 1;
                    dto.UPDATE_DATE = DateTime.Now;
                    mapboxDao.Insert(dto);
                }
                else
                {
                    // 更新
                    foreach (DataRow row in dt.Rows)
                    {
                        dto.USER_NAME = row["USER_NAME"].ToString();
                        dto.PC_NAME = row["PC_NAME"].ToString();
                        dto.MENU_NAME = row["MENU_NAME"].ToString();
                        dto.ACCESS_COUNT = (int)row["ACCESS_COUNT"] + 1;
                        dto.UPDATE_DATE = DateTime.Now;
                        dto.TIME_STAMP = (byte[])row["TIME_STAMP"];
                    }
                    mapboxDao.Update(dto);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("accessCountRegist", ex);
                this.msgLogic.MessageBoxShow("E245");
                return false;
            }
        }

        #endregion

        #region 設置コンテナ一覧用の表示非表示切替ロジック生成

        /// <summary>
        /// 設置コンテナ一覧用の表示非表示切替ロジック生成
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="visibleChange">表示非表示切替用のコード</param>
        private void VisibleChange(List<mapboxArrayList> dto, out string visibleChange)
        {
            visibleChange = string.Empty;

            StringBuilder sb = new StringBuilder();

            int i = 1;
            int j = 1;
            string tabName = string.Empty;
            foreach (mapboxArrayList item in dto)
            {
                bool elseFlg = false;
                if (i != 1 && tabName != item.properties.tabName)
                {
                    j++;
                    elseFlg = true;
                }

                if (i == 1)
                {
                    sb.AppendFormat("if (this.value == {0}) {{", j).AppendLine();
                }
                else
                {
                    if (elseFlg)
                    {
                        sb.AppendFormat("}} else if (this.value == {0}) {{", j).AppendLine();
                    }
                }
                sb.AppendFormat("    if (map.getLayoutProperty('meisai{0}', 'visibility') == 'visible' ||", i).AppendLine();
                sb.AppendFormat("	     map.getLayoutProperty('rowNumber{0}', 'visibility') == 'visible') {{ ", i).AppendLine();
                sb.AppendFormat("	     map.setLayoutProperty('meisai{0}', 'visibility', 'none');", i).AppendLine();
                sb.AppendFormat("	     map.setLayoutProperty('rowNumber{0}', 'visibility', 'none');", i).AppendLine();
                sb.AppendFormat("	     ChangeStatus.textContent = '非表示';").AppendLine();
                sb.AppendFormat("	     ChangeStatus.style.cssText = 'color:black; font-size:15px;';").AppendLine();
                sb.AppendFormat("    }} else {{").AppendLine();
                sb.AppendFormat("	     map.setLayoutProperty('meisai{0}', 'visibility', 'visible');", i).AppendLine();
                sb.AppendFormat("	     map.setLayoutProperty('rowNumber{0}', 'visibility', 'visible');", i).AppendLine();
                sb.AppendFormat("	     ChangeStatus.textContent = '表示';").AppendLine();
                sb.AppendFormat("	     ChangeStatus.style.cssText = 'color:red; font-weight:bold; font-size:15px;';").AppendLine();
                sb.AppendFormat("    }}", i).AppendLine();

                i++;
                tabName = item.properties.tabName;
            }
            sb.AppendLine("}");

            visibleChange = sb.ToString();
        }

        #endregion

        #region ポップアップ表示一斉切替(private)

        /// <summary>
        /// ポップアップ表示一斉切替用
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="PopupSource">ポップアップ用のコード</param>
        /// <param name="popupAdd">ポップアップを地図に追加</param>
        /// <param name="popupRemove">ポップアップを地図から削除</param>
        private void MapPopup(List<mapboxArrayList> dto, out string popupSource, out string popupAdd, out string popupRemove)
        {
            popupSource = string.Empty;
            popupAdd = string.Empty;
            popupRemove = string.Empty;

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();

            int i = 0;
            string beforeDenpyouNo = string.Empty;
            foreach (mapboxArrayList mapArrayList in dto)
            {
                if (mapArrayList.geometry.Latitude == "" &&
                    mapArrayList.geometry.Latitude_2 == "")
                {
                    continue;
                }

                // 配車割当の定期配車の場合、かつ伝票番号が空または前回値と同じ場合は処理をスキップする。
                if ((this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY && mapArrayList.properties.dataKBN == "3")
                    && (string.IsNullOrEmpty(mapArrayList.properties.denpyouNo) || beforeDenpyouNo.Equals(mapArrayList.properties.denpyouNo)))
                {
                    continue;
                }

                i++;

                if (mapArrayList.geometry.Latitude != "")
                {
                    // @PopupSource
                    sb1.AppendLine("var popup_" + i + " = new mapboxgl.Popup({ closeOnClick: false })");
                    sb1.AppendFormat(".setLngLat([{0}, {1}])", mapArrayList.geometry.Longitude, mapArrayList.geometry.Latitude).AppendLine();
                    sb1.AppendFormat(".setHTML('{0}')", mapArrayList.properties.description).AppendLine();
                    sb1.AppendLine(".addTo(map);");

                    // @PopupAdd
                    sb2.AppendFormat("popup_{0}.addTo(map);", i).AppendLine();

                    // @PopupRemove
                    sb3.AppendFormat("popup_{0}.remove();", i).AppendLine();
                }
                // 配車割当の定期の場合、始点終点が必要になるので終点の分はここで設定する
                if (this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY && mapArrayList.properties.dataKBN == "3")
                {
                    if (mapArrayList.geometry.Latitude_2 == "")
                    {
                        continue;
                    }

                    // @PopupSource
                    sb1.AppendLine("var popup_" + i + "_2 = new mapboxgl.Popup({ closeOnClick: false })");
                    sb1.AppendFormat(".setLngLat([{0}, {1}])", mapArrayList.geometry.Longitude_2, mapArrayList.geometry.Latitude_2).AppendLine();
                    sb1.AppendFormat(".setHTML('{0}')", mapArrayList.properties.description_2).AppendLine();
                    sb1.AppendLine(".addTo(map);");

                    // @PopupAdd
                    sb2.AppendFormat("popup_{0}_2.addTo(map);", i).AppendLine();

                    // @PopupRemove
                    sb3.AppendFormat("popup_{0}_2.remove();", i).AppendLine();
                }

                // 前回の伝票番号を保持する。
                beforeDenpyouNo = mapArrayList.properties.denpyouNo;
            }
            popupSource = sb1.ToString();
            popupAdd = sb2.ToString();
            popupRemove = sb3.ToString();
        }

        /// <summary>
        /// ポップアップ表示一斉切替用(モバイル状況一覧用)
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="PopupSource">ポップアップ用のコード</param>
        /// <param name="popupAdd">ポップアップを地図に追加</param>
        /// <param name="popupRemove">ポップアップを地図から削除</param>
        private void MapPopupKadoujyoukyou(List<ArrayListKadoujyoukyou> dto, out string popupSource, out string popupAdd, out string popupRemove)
        {
            popupSource = string.Empty;
            popupAdd = string.Empty;
            popupRemove = string.Empty;

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();

            int i = 0;
            foreach (ArrayListKadoujyoukyou arrayList in dto)
            {
                i++;
                // @PopupSource
                sb1.AppendLine("var popup" + i + " = new mapboxgl.Popup({ closeOnClick: false })");
                sb1.AppendFormat(".setLngLat([{0}, {1}])", arrayList.geometry.Longitude, arrayList.geometry.Latitude).AppendLine();
                sb1.AppendFormat(".setHTML('{0}')", arrayList.properties.description).AppendLine();
                sb1.AppendLine(".addTo(map);");

                // @PopupAdd
                sb2.AppendFormat("popup{0}.addTo(map);", i).AppendLine();

                // @PopupRemove
                sb3.AppendFormat("popup{0}.remove();", i).AppendLine();
            }
            popupSource = sb1.ToString();
            popupAdd = sb2.ToString();
            popupRemove = sb3.ToString();
        }

        #endregion

        #region 変換メソッド(private)

        /// <summary>
        /// HTMLファイルを読み込んで文字列に変換して返す
        /// </summary>
        /// <returns></returns>
        private string readHTMLConvertToString(string template)
        {
            string html = string.Empty;

            try
            {
                // テンプレートファイル読み込み
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                using (var reader = new StreamReader(myAssembly.GetManifestResourceStream(template)))
                {
                    html = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("readHTMLConvertToString", ex);
                this.msgLogic.MessageBoxShow("E245");
            }

            return html;
        }

        /// <summary>
        /// DTO(リスト型)を文字列に変換する
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string convertDtoToString(List<mapboxArrayList> dto)
        {
            string jsonString = new JavaScriptSerializer().Serialize(dto);
            return jsonString;
        }

        /// <summary>
        /// DTOを文字列に変換する
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string convertDtoToString(mapboxMarkerDto dto)
        {
            string jsonString = new JavaScriptSerializer().Serialize(dto);
            return jsonString;
        }

        /// <summary>
        /// DTOを文字列に変換する
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string convertDtoToString(mapboxRowNoDto dto)
        {
            string jsonString = new JavaScriptSerializer().Serialize(dto);
            return jsonString;
        }

        /// <summary>
        /// DTOを文字列に変換する
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string convertDtoToString(mapboxRootDto dto)
        {
            string jsonString = new JavaScriptSerializer().Serialize(dto);
            return jsonString;
        }

        /// <summary>
        /// DTOをJSONに変換する際の変数名を差し替え(DTOの変数名に「-」が使えないため)
        /// </summary>
        private string convertDtoName(string html)
        {
            html = html.Replace("iconImage", "icon-image");
            html = html.Replace("iconSize", "icon-size");
            html = html.Replace("iconPadding", "icon-padding");
            html = html.Replace("textField", "text-field");
            html = html.Replace("textOffset", "text-offset");
            html = html.Replace("textSize", "text-size");
            html = html.Replace("textAnchor", "text-anchor");
            html = html.Replace("iconAllowOverlap", "icon-allow-overlap");
            html = html.Replace("textAllowOverlap", "text-allow-overlap");
            html = html.Replace("lineJoin", "line-join");
            html = html.Replace("lineCap", "line-cap");
            html = html.Replace("textColor", "text-color");
            html = html.Replace("iconColor", "icon-color");
            html = html.Replace("lineColor", "line-color");
            html = html.Replace("lineWidth", "line-width");

            return html;
        }

        #endregion

        #region システム設定マスタ読込(private)

        private void LoadSysInfo()
        {
            M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
            }
        }

        #endregion

        #region WINDOW_IDごとの判定(private)

        /// <summary>
        /// ルートが必要である場合Trueを返す
        /// </summary>
        /// <returns></returns>
        private bool Root_Output()
        {
            if (this.windowId == WINDOW_ID.M_COURSE ||                  // コース入力
                this.windowId == WINDOW_ID.M_COURSE_ICHIRAN ||          // コース一覧
                this.windowId == WINDOW_ID.T_TEIKI_HAISHA ||            // 定期配車
                this.windowId == WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN ||    // 定期配車一覧
                this.windowId == WINDOW_ID.T_COURSE_HAISHA_IRAI)        // コース配車依頼入力
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 順番が必要である場合Trueを返す
        /// </summary>
        /// <returns></returns>
        private bool No_Output()
        {
            if (this.windowId == WINDOW_ID.M_COURSE ||                  // コース入力
                this.windowId == WINDOW_ID.M_COURSE_ICHIRAN ||          // コース一覧
                this.windowId == WINDOW_ID.T_TEIKI_HAISHA ||            // 定期配車
                this.windowId == WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN ||    // 定期配車一覧
                this.windowId == WINDOW_ID.T_CONTENA_ICHIRAN ||         // コンテナ一覧
                this.windowId == WINDOW_ID.T_COURSE_HAISHA_IRAI ||      // コース配車依頼入力
                this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY)        // 配車割当一日
            {
                return true;
            }
            return false;
        }

        #endregion

        #region DTOの並び替え

        /// <summary>
        /// 並び替えロジック
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static int CompareAccount(mapDto x, mapDto y)
        {
            // 第一のキー(rowNoフィールド)で比較
            if (x.rowNo > y.rowNo)
            {
                return 1;
            }
            else if (x.rowNo < y.rowNo)
            {
                return -1;
            }
            else /* if (x.rowNo == y.rowNo) */
            {
                // 第一のキーが同じだった場合は、第二のキー(roundNoフィールド)で比較
                if (x.roundNo > y.roundNo)
                {
                    return 1;
                }
                else if (x.roundNo < y.roundNo)
                {
                    return -1;
                }
                else // if (x.roundNo == y.roundNo)
                {
                    // 第二のキーが同じだった場合は、第三のキー(numberフィールド)で比較
                    if (x.number > y.number)
                        return 1;
                    else if (x.number < y.number)
                        return -1;
                    else // if (x.number == y.number)
                        return 0;
                }
            }
        }

        #endregion

        #region 曜日名を返す

        /// <summary>
        /// 日付から曜日名を返す
        /// </summary>
        /// <param name="DayCd"></param>
        /// <returns></returns>
        public string SetDayNameByDate(string Date)
        {
            DateTime dt = Convert.ToDateTime(Date);
            DayOfWeek dow = dt.DayOfWeek;

            switch (dow)
            {
                case DayOfWeek.Monday:
                    return "月";
                case DayOfWeek.Tuesday:
                    return "火";
                case DayOfWeek.Wednesday:
                    return "水";
                case DayOfWeek.Thursday:
                    return "木";
                case DayOfWeek.Friday:
                    return "金";
                case DayOfWeek.Saturday:
                    return "土";
                case DayOfWeek.Sunday:
                    return "日";
                default:
                    return "";
            }
        }

        /// <summary>
        /// CDから曜日名を返す
        /// </summary>
        /// <param name="DayCd"></param>
        /// <returns></returns>
        public string SetDayNameByCd(string DayCd)
        {
            switch (DayCd)
            {
                case "1":
                    return "月";
                case "2":
                    return "火";
                case "3":
                    return "水";
                case "4":
                    return "木";
                case "5":
                    return "金";
                case "6":
                    return "土";
                case "7":
                    return "日";
                default:
                    return "";
            }
        }

        #endregion

        #region タブ名を返す

        private string SetTabName(mapDto dto)
        {
            string tabName = string.Empty;

            if (this.windowId == WINDOW_ID.T_TEIKI_HAISHA ||            // 定期配車
                this.windowId == WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN)      // 定期配車一覧
            {
                // 定期配車番号+コース名+曜日
                tabName = dto.teikiHaishaNo + " － " + dto.courseName + "(" + dto.dayName + ")";
            }
            else if (this.windowId == WINDOW_ID.M_COURSE)               // コース入力
            {
                if (dto.courseName == string.Empty)
                {
                    // 組込データ
                    tabName = "組込";
                }
                else
                {
                    // コース名+曜日
                    tabName = dto.courseName + "(" + dto.dayName + ")";
                }
            }
            else if (this.windowId == WINDOW_ID.M_COURSE_ICHIRAN)       // コース一覧
            {
                // コース名+曜日
                tabName = dto.courseName + "(" + dto.dayName + ")";
            }
            else if (this.windowId == WINDOW_ID.T_COURSE_HAISHA_IRAI)   // コース配車依頼入力
            {
                if (dto.teikiHaishaNo == string.Empty)
                {
                    // 組込データ
                    tabName = "組込";
                }
                else
                {
                    // 定期配車番号+コース名+曜日
                    tabName = dto.teikiHaishaNo + " － " + dto.courseName + "(" + dto.dayName + ")";
                }
            }
            else if (this.windowId == WINDOW_ID.T_UKETSUKE_ICHIRAN)
            {
                tabName = dto.courseName;
            }
            else if (this.windowId == WINDOW_ID.T_HAISHA_WARIATE_DAY)
            {
                tabName = dto.courseName;
            }
            else if (this.windowId == WINDOW_ID.T_CONTENA_ICHIRAN)
            {
                tabName = dto.courseName;
            }
            else
            {
                // その他
                tabName = "データなし";
            }
            return tabName;
        }

        #endregion

        #region 配車割当用情報抽出
        private void wariateInfo(out string mapHeader, out string mapHeader2, out string wariateCount, out string contenaCount, out string mihaishaCount)
        {
            // 地図のヘッダー情報を取得
            var header = (from a in this.allList select a).First();
            mapHeader = header.properties.header;
            mapHeader2 = header.properties.header2;

            wariateCount = (from a in this.wariateList select a).Count().ToString();
            contenaCount = (from a in this.contenaList select a).Count().ToString();
            mihaishaCount = (from a in this.mihaishaList select a).Count().ToString();
        }
        #endregion

        #region 業者・現場の情報を抽出

        /// <summary>
        /// 業者または現場の情報を返す
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public MAP_GENBA_DTO mapGenbaInfo(string gyoushaCd, string genbaCd)
        {
            MAP_GENBA_DTO dto = new MAP_GENBA_DTO();

            if (gyoushaCd == null)
            {
                gyoushaCd = string.Empty;
            }
            if (genbaCd == null)
            {
                genbaCd = string.Empty;
            }

            M_TODOUFUKEN todoufukenEntity;
            string address = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(genbaCd))
                {
                    // 現場CDがない場合は業者の情報を抽出
                    // 現場がなければ業者の情報を取得する
                    var gyoushaEntity = new M_GYOUSHA();
                    gyoushaEntity.GYOUSHA_CD = gyoushaCd;
                    var gyoushaEntitys = this.gyoushaDao.GetAllValidData(gyoushaEntity);
                    foreach (M_GYOUSHA entity in gyoushaEntitys)
                    {
                        dto.GYOUSHA_CD = entity.GYOUSHA_CD;
                        dto.GYOUSHA_NAME = entity.GYOUSHA_NAME_RYAKU;
                        dto.GENBA_CD = string.Empty;
                        dto.GENBA_NAME = string.Empty;
                        dto.POST = this.NullToSpace(entity.GYOUSHA_POST);
                        todoufukenEntity = this.todoufukenDao.GetDataByCd(entity.GYOUSHA_TODOUFUKEN_CD.ToString());
                        if (todoufukenEntity != null)
                        {
                            address += this.NullToSpace(todoufukenEntity.TODOUFUKEN_NAME_RYAKU);
                        }
                        address += this.NullToSpace(entity.GYOUSHA_ADDRESS1);
                        address += this.NullToSpace(entity.GYOUSHA_ADDRESS2);
                        dto.ADDRESS = address;
                        dto.TEL = this.NullToSpace(entity.GYOUSHA_TEL);
                        dto.BIKOU1 = this.NullToSpace(entity.BIKOU1);
                        dto.BIKOU2 = this.NullToSpace(entity.BIKOU2);
                        dto.LATITUDE = this.NullToSpace(entity.GYOUSHA_LATITUDE);
                        dto.LONGITUDE = this.NullToSpace(entity.GYOUSHA_LONGITUDE);
                    }
                }
                else
                {
                    // 現場の情報を抽出
                    var genbaEntity = new M_GENBA();
                    genbaEntity.GYOUSHA_CD = gyoushaCd;
                    genbaEntity.GENBA_CD = genbaCd;
                    var genbaEntitys = this.genbaDao.GetAllValidData(genbaEntity);
                    // 現場の情報を取得
                    foreach (M_GENBA entity in genbaEntitys)
                    {
                        var gyoushaEntity = new M_GYOUSHA();
                        gyoushaEntity.GYOUSHA_CD = gyoushaCd;
                        var gyoushaEntitys = this.gyoushaDao.GetAllValidData(gyoushaEntity);
                        foreach (M_GYOUSHA gyousha in gyoushaEntitys)
                        {
                            dto.GYOUSHA_NAME = this.NullToSpace(gyousha.GYOUSHA_NAME_RYAKU);
                        }
                        dto.GENBA_NAME = this.NullToSpace(entity.GENBA_NAME_RYAKU);
                        dto.POST = this.NullToSpace(entity.GENBA_POST);
                        todoufukenEntity = this.todoufukenDao.GetDataByCd(this.NullToSpace(entity.GENBA_TODOUFUKEN_CD));
                        if (todoufukenEntity != null)
                        {
                            address += this.NullToSpace(todoufukenEntity.TODOUFUKEN_NAME_RYAKU);
                        }
                        address += this.NullToSpace(entity.GENBA_ADDRESS1);
                        address += this.NullToSpace(entity.GENBA_ADDRESS2);
                        dto.ADDRESS = address;
                        dto.TEL = this.NullToSpace(entity.GENBA_TEL);
                        dto.BIKOU1 = this.NullToSpace(entity.BIKOU1);
                        dto.BIKOU2 = this.NullToSpace(entity.BIKOU2);
                        dto.LATITUDE = this.NullToSpace(entity.GENBA_LATITUDE);
                        dto.LONGITUDE = this.NullToSpace(entity.GENBA_LONGITUDE);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("mapGenbaInfo", ex);
            }
            return dto;
        }

        #endregion

        #region nullをブランクに変換

        private string NullToSpace(SqlInt16 value)
        {
            string ret = string.Empty;

            if (value.IsNull)
            {
                ret = string.Empty;
            }
            else
            {
                ret = Convert.ToString(value);
            }

            return ret;
        }

        private string NullToSpace(string value)
        {
            string ret = string.Empty;

            if (value != null)
            {
                ret = value;
            }

            return ret;
        }

        #endregion

        #region ファイルパスを返す

        private string getFilePath()
        {
            ProcessMode pMode = ProcessMode.NotSet;
            if (SystemInformation.TerminalServerSession)
            {
                pMode = ProcessMode.CloudServerSideProcess;
            }
            else
            {
                pMode = ProcessMode.CloudClientSideProcess;
            }
            return LocalDirectories.GetMapDirectory(pMode);
        }

        #endregion

        #endregion
    }
}
