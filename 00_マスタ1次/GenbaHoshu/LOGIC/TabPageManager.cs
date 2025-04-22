using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Utility;

namespace GenbaHoshu.Logic
{
    /// <summary>
    /// タブコントロール制御クラス
    /// </summary>
    public class TabPageManager
    {
        private class TabPageInfo
        {
            public TabPage TabPage;
            public bool Visible;
            public string Name;
            public int Index;
            public TabPageInfo(TabPage page,string name,int index, bool v)
            {
                TabPage = page;
                Name = name;
                Index = index;
                Visible = v;

            }
        }
        private TabPageInfo[] _tabPageInfos = null;
        private TabControl _tabControl = null;

        /// <summary>
        /// TabPageManagerクラスのインスタンスを作成する
        /// </summary>
        /// <param name="crl">基になるTabControlオブジェクト</param>
        public TabPageManager(TabControl crl)
        {
            _tabControl = crl;
            _tabPageInfos = new TabPageInfo[_tabControl.TabPages.Count];
            for (int i = 0; i < _tabControl.TabPages.Count; i++)
                _tabPageInfos[i] =
                    new TabPageInfo(_tabControl.TabPages[i],_tabControl.TabPages[i].Name, i , true);
        }

        /// <summary>
        /// TabPageの表示・非表示を変更する
        /// </summary>
        /// <param name="index">変更するTabPageのIndex番号</param>
        /// <param name="v">表示するときはTrue。
        /// 非表示にするときはFalse。</param>
        public void ChangeTabPageVisible(int index, bool v)
        {
            if (_tabPageInfos[index].Visible == v)
                return;

            _tabControl.SuspendLayout();
            //_tabControl.TabPages.Clear();
            //for (int i = 0; i < _tabPageInfos.Length; i++)
            //{
            //    if (_tabPageInfos[i].Visible)
            //        _tabControl.TabPages.Add(_tabPageInfos[i].TabPage);
            //}
            if (!IsVisible(index) && v)
            {
                _tabControl.TabPages.Insert(index, _tabPageInfos[index].TabPage);
            }
            if (IsVisible(index) && !v)
            {
                _tabControl.TabPages.Remove(_tabPageInfos[index].TabPage);
            }
            _tabPageInfos[index].Visible = v;
            _tabControl.ResumeLayout();
        }

        /// <summary>
        /// TabPageの表示・非表示を変更する
        /// </summary>
        /// <param name="index">変更するTabPageのIndex番号</param>
        /// <param name="v">表示するときはTrue。
        /// 非表示にするときはFalse。</param>
        public void ChangeTabPageVisible(string tabPageName, bool v)
        {
            int index = GetIndexTagPage(tabPageName);
            if (index >= 0)
            {
                if (_tabPageInfos[index].Visible == v)
                    return;

                _tabControl.SuspendLayout();
                //_tabControl.TabPages.Clear();
                //for (int i = 0; i < _tabPageInfos.Length; i++)
                //{
                //    if (_tabPageInfos[i].Visible)
                //        _tabControl.TabPages.Add(_tabPageInfos[i].TabPage);
                //}
                if (!IsVisible(index) && v)
                {
                    _tabControl.TabPages.Insert(index, _tabPageInfos[index].TabPage);
                }
                if (IsVisible(index) && !v)
                {
                    _tabControl.TabPages.Remove(_tabPageInfos[index].TabPage);
                }
                _tabPageInfos[index].Visible = v;
                _tabControl.ResumeLayout();
            }
        }

        /// <summary>
        /// TabPageの表示状態をチェックする
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsVisible(int index)
        {
            LogUtility.DebugMethodStart(index);

            bool result = false;

            if (_tabPageInfos.Length > index)
            {
                result = _tabPageInfos[index].Visible;
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }
        /// <summary>
        /// TabPageの表示状態をチェックする
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsVisible(string tabPageName)
        {
            LogUtility.DebugMethodStart(tabPageName);

            bool result = false;

            int index = GetIndexTagPage(tabPageName);
            if (index >= 0)
            {
                if (_tabPageInfos.Length > index)
                {
                    result = _tabPageInfos[index].Visible;
                }
            }
            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabPageName"></param>
        /// <returns></returns>
        public int GetIndexTagPage(string tabPageName)
        {
            int index = -1;
            var tabPageInfo = _tabPageInfos.Where(w => w.Name.Equals(tabPageName)).FirstOrDefault();
            if (tabPageInfo != null)
            {
                index = tabPageInfo.Index;
            }
            return index;
        }
    }
}
