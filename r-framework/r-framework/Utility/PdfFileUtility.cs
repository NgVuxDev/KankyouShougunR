using System;
using System.Collections.Generic;
using Container = System.Collections.Generic;

namespace r_framework.Utility
{
    /// <summary>
    /// 入力 PDF ファイルの各オブジェクトの，（開始位置, バイト数）の
    /// 情報を保持するためのクラス．Pdf.Reader の xref (cross
    /// </summary>
    public class PdfFileUtility
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PdfFileUtility()
        {
            this.offset_ = 0;
            this.length_ = 0;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PdfFileUtility(long off, long len)
        {
            this.offset_ = off;
            this.length_ = len;
        }

        /// <summary>
        /// オフセットへのアクセサ
        /// </summary>
        public long Offset
        {
            get { return this.offset_; }
            set { this.offset_ = value; }
        }

        /// <summary>
        /// PDFの長さへのアクセサ
        /// </summary>
        public long Length
        {
            get { return this.length_; }
            set { this.length_ = value; }
        }

        /// <summary>
        /// オフセット
        /// </summary>
        private long offset_;

        /// <summary>
        /// PDFのながさ
        /// </summary>
        private long length_;
    }

    /// <summary>
    /// 引数に指定された PDF ファイルを解析し，PDF ファイルを構成する
    /// 各オブジェクトにアクセスできるようにするためのクラス
    /// </summary>
    public class PDFReader : IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PDFReader()
        {
            this.Init();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PDFReader(System.String path)
        {
            this.Init();
            this.Open(path);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        ~PDFReader()
        {
            this.Dispose();
        }

        /// <summary>
        /// PDFファイルオープンメソッド
        /// </summary>
        /// <param name="path">ファイルパス</param>
        public void Open(System.String path)
        {
            this.path_ = path;
            if (!System.IO.File.Exists(this.path_))
            {
                throw new System.IO.FileNotFoundException(this.path_ + " is not found", this.path_);
            }
            if (!this.IsValid(this.path_)) throw new System.Exception("invalid PDF file format");

            this.ReadVersion(this.input_);
            this.ReadInfo(this.input_);
        }

        /// <summary>
        /// 破棄メソッド
        /// </summary>
        public void Dispose()
        {
            if (this.input_ != null)
            {
                this.input_.Dispose();
                this.input_ = null;
            }
        }

        /// <summary>
        /// PDF ファイルのフォーマットが正しいかどうかを判別する
        /// </summary>
        /// <param name="path">PDFファイルパス</param>
        /// <returns>チェックの成否</returns>
        public bool IsValid(string path)
        {
            this.input_ = new System.IO.FileStream(path, System.IO.FileMode.Open);
            // %PDF-1.X のチェック
            byte[] chk = new byte[5];
            this.input_.Seek(0, System.IO.SeekOrigin.Begin);
            this.input_.Read(chk, 0, 5);
            if (!(chk[0] == (byte)'%' && chk[1] == (byte)'P' && chk[2] == (byte)'D' && chk[3] == (byte)'F' && chk[4] == (byte)'-')) return false;

            // %%EOF のチェック
            byte[] feof = new byte[2];
            this.input_.Seek(-2, System.IO.SeekOrigin.End);
            this.input_.Read(feof, 0, 2);

            long n = (feof[0] == 0x0d && feof[1] == 0x0a) ? 7 : ((feof[1] == 0x0a) ? 6 : 5);
            this.input_.Seek(-n, System.IO.SeekOrigin.End);
            this.input_.Read(chk, 0, (int)5);
            if (!(chk[0] == (byte)'%' && chk[1] == (byte)'%' && chk[2] == (byte)'E' && chk[3] == (byte)'O' && chk[4] == (byte)'F')) return false;

            return true;
        }

        /// <summary>
        /// table の要素数を返す
        /// </summary>
        /// <returns>要素数</returns>
        public int Count()
        {
            return this.XrefTable.Count;
        }

        /// <summary>
        /// index に対応するオブジェクトを返す
        /// </summary>
        /// <param name="index">要素数</param>
        /// <returns>取得したオブジェクト</returns>
        public byte[] GetObject(uint index)
        {
            if (!this.xref_.ContainsKey(index)) return null;
            PdfFileUtility elem = this.xref_[index];
            byte[] dest = new byte[elem.Length];
            this.input_.Seek(elem.Offset, System.IO.SeekOrigin.Begin);
            this.input_.Read(dest, 0, (int)elem.Length);
            return dest;
        }

        /// <summary>
        /// バージョンアクセサ
        /// </summary>
        public double Version
        {
            get { return this.version_; }
        }

        /// <summary>
        /// 要素格納済みテーブル
        /// </summary>
        public Container.SortedDictionary<uint, PdfFileUtility> XrefTable
        {
            get { return this.xref_; }
        }

        /// <summary>
        /// トレーラーアクセサ
        /// </summary>
        public Container.Dictionary<System.String, System.String> Trailer
        {
            get { return this.trailer_; }
        }

        /// <summary>
        /// 初期化メソッド
        /// </summary>
        private void Init()
        {
            this.path_ = null;
            this.input_ = null;
            this.xref_ = new Container.SortedDictionary<uint, PdfFileUtility>();
            this.trailer_ = null;
        }

        /// <summary>
        ///  ReadLine (private)
        ///  指定された StreamReader から次の一行を読み込む．
        ///  ただし，コメント行（% で始まる行）は読み飛ばす．
        /// </summary>
        /// <param name="reader">読み込んだファイルストリーム</param>
        /// <returns>１行分のString</returns>
        private System.String ReadLine(System.IO.StreamReader reader)
        {
            System.String line = null;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Length > 0)
                {
                    if (line[0] != 0x0d && line[0] != 0x0a && line[0] != '%') return line.Trim();
                }
            }
            return null;
        }

        /// <summary>
        /// 読み込みバージョンの取得
        /// </summary>
        /// <param name="input">読み込むストリーム</param>
        private void ReadVersion(System.IO.Stream input)
        {
            byte[] version = new byte[3];
            this.input_.Seek(5, System.IO.SeekOrigin.Begin);
            this.input_.Read(version, 0, 3);
            this.version_ = System.Convert.ToDouble(System.Text.Encoding.ASCII.GetString(version));
        }

        /// <summary>
        /// xref (cross reference) table，および trailer を読み込む．
        /// </summary>
        /// <param name="input">ストリーム</param>
        private void ReadInfo(System.IO.Stream input)
        {
            Container.HashSet<long> pos = new Container.HashSet<long>();

            long startxref = this.ReadStartXref(input, 128);
            input.Seek(startxref, System.IO.SeekOrigin.Begin);
            System.IO.StreamReader reader = new System.IO.StreamReader(input);

            // 1. read xref (cross reference) table.
            System.String buffer = null;
            buffer = this.ReadLine(reader);
            if (buffer == null || buffer != "xref") throw new System.Exception("cannot find xref table");
            buffer = this.ReadLine(reader);
            if (buffer == null) throw new System.Exception("cannot find xref table");

            uint n = System.Convert.ToUInt32(buffer.Substring(buffer.IndexOf(' ') + 1));
            Container.SortedDictionary<long, uint> map = this.MakeXrefTable(reader, n);

            uint index = 0;
            long first = 0;
            foreach (Container.KeyValuePair<long, uint> elem in map)
            {
                if (first > 0)
                {
                    PdfFileUtility x = new PdfFileUtility(first, elem.Key - first);
                    this.xref_.Add(index, x);
                }
                index = elem.Value;
                first = elem.Key;
            }
            PdfFileUtility last = new PdfFileUtility(first, startxref - first);
            this.xref_.Add(index, last);

            // 2. read trailer.
            System.String trailer = "";
            bool target = false;
            while ((buffer = this.ReadLine(reader)) != null)
            {
                if (buffer == "startxref") break;
                if (buffer == "trailer") target = true;
                else if (target) trailer += buffer + " ";
            }

            if (trailer.Length > 0)
            {

                var dictionary = new Dictionary<string, string>();

                dictionary.Add(trailer, trailer);

                this.trailer_ = dictionary;
            }
        }

        /// <summary>
        /// ReadStartXref (private)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private long ReadStartXref(System.IO.Stream input, uint bytes)
        {
            long dest = -1;
            input.Seek(-bytes, System.IO.SeekOrigin.End);
            System.IO.StreamReader reader = new System.IO.StreamReader(input);
            System.String line = null;
            bool target = false;
            while ((line = this.ReadLine(reader)) != null)
            {
                if (line == "startxref") target = true;
                else if (target)
                {
                    dest = System.Convert.ToInt64(line);
                    break;
                }
            }
            return (dest > 0) ? dest : this.ReadStartXref(input, bytes * 2);
        }

        /// <summary>
        /// MakeXrefTable (private)
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private Container.SortedDictionary<long, uint> MakeXrefTable(System.IO.StreamReader reader, uint n)
        {
            Container.SortedDictionary<long, uint> dest = new Container.SortedDictionary<long, uint>();
            try
            {
                for (uint i = 0; i < n; i++)
                {
                    System.String buffer = this.ReadLine(reader);
                    System.String[] token = buffer.Split();
                    if (token[2] == "n") dest.Add(System.Convert.ToInt64(token[0]), i);
                }
            }
            catch
            {
                throw new System.Exception("invalid xref table");
            }

            return dest;
        }

        /// <summary>
        /// ファイルパス
        /// </summary>
        private System.String path_;

        /// <summary>
        /// inputストリーム
        /// </summary>
        private System.IO.FileStream input_;

        /// <summary>
        /// ヴァージョン
        /// </summary>
        private double version_;

        /// <summary>
        /// X軸読込内容
        /// </summary>
        private Container.SortedDictionary<uint, PdfFileUtility> xref_;

        /// <summary>
        /// トレーラー
        /// </summary>
        private Container.Dictionary<System.String, System.String> trailer_;
    }
}