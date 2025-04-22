using System;
using System.IO.Packaging;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;


namespace Shougun.Printing.Viewer
{
    /// <summary>
    /// XpsDocumentクラスをラップするクラス。他のアセンブリがWPFなしでも利用可能にするため。
    /// </summary>
    public class XpsFile : IDisposable
    {
        private bool disposed = false;
        
        public string FullPath 
        { 
            get; 
            set; 
        }

        public string FileName 
        {
            get
            {
                return System.IO.Path.GetFileName(this.FullPath);
            }
        }

        public string Title
        {
            get;
            private set;
        }

        public string Subject
        {
            get;
            private set;
        }
        public string Description
        {
            get;
            private set;
        }

        public int PageCount
        {
            get;
            private set;
        }

        public XpsFile()
        {
            this.disposed = false;
        }

        public XpsFile(string xpsPath)
        {
            this.disposed = false;
            Open(xpsPath);
        }

        ~XpsFile()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (this)
            {
                if (this.disposed)
                {
                    return;
                }

                this.disposed = true;

                if (disposing)
                {
                    this.Close();
                }
            }
        }
        
        public void Open(string xpsPath = null)
        {
            if (!string.IsNullOrEmpty(xpsPath))
            {
                this.FullPath = xpsPath;
            }

            Exception exception = null;
            for (int i = 0; i < 20 ; i++)
            {
                try
                {
                    using (var xpsDocument = new XpsDocument(this.FullPath, System.IO.FileAccess.Read))
                    {
                        var coreDocumentProperties = xpsDocument.CoreDocumentProperties;
                        var fixedDocumentSequence = xpsDocument.GetFixedDocumentSequence();
                        var documentPaginator = fixedDocumentSequence.DocumentPaginator;
                        if (documentPaginator.PageCount > 0)
                        {
                            this.PageCount = documentPaginator.PageCount;
                            this.Title = coreDocumentProperties.Title;
                            this.Subject = coreDocumentProperties.Subject;
                            this.Description = coreDocumentProperties.Description;

                            // XpsDocumentのメモリリーク対策、なぜかこれをやらないとリソースが解放されない
                            for (int p = 0; p < documentPaginator.PageCount; p++)
                            {
                                var fixedPage = documentPaginator.GetPage(p).Visual as FixedPage;
                                if (fixedPage != null) fixedPage.UpdateLayout();
                            }
                            exception = null;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 書き込み側がファイルクローズ間際の可能性があるので、300ms周期でリトライする
                    System.Threading.Thread.Sleep(300);
                    exception = ex;
                }
            }


            if (exception != null)
            {
                throw exception;
            }
        }

        public void Close()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public System.Drawing.Bitmap GetBitmap(int pageNo)
        {
            try
            {
                using (var xpsDocument = new XpsDocument(this.FullPath, System.IO.FileAccess.Read))
                {
                    var fixedDocumentSequence = xpsDocument.GetFixedDocumentSequence();
                    var documentPaginator = fixedDocumentSequence.DocumentPaginator;
                    using (var page = documentPaginator.GetPage(pageNo))
                    {
                        var renderer = new System.Windows.Media.Imaging.RenderTargetBitmap(
                                    (int)page.Size.Width, (int)page.Size.Height, 96, 96,
                                    System.Windows.Media.PixelFormats.Pbgra32);

                        renderer.Render(page.Visual);

                        var encoder = new System.Windows.Media.Imaging.BmpBitmapEncoder();
                        encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(renderer));

                        using (var stream = new System.IO.MemoryStream())
                        {
                            encoder.Save(stream);
                            return new System.Drawing.Bitmap(stream);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
