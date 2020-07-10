using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DXApplicationImageMemory
{
    public partial class FormException : DevExpress.XtraEditors.XtraForm
    {
        string localPictureFilePath = null;
        string cachPictureFilePath = null;

        int count1 = 0;
        int count2 = 0;

        public FormException()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (GalleryItem eachGalleryItem in galleryControl1.Gallery.Groups[0].Items)
                {
                    eachGalleryItem.Image = null;
                }
                ////pictureEdit1.BackgroundImage = null;
                ////pictureEdit1.Image = null;
            }
            catch (OutOfMemoryException catchOutOfMemoryException)
            {
                Debug.WriteLine("catchOutOfMemoryException");
                //MessageBox.Show("catchOutOfMemoryException");
            }
            catch (Exception catchImageException)
            {
                ////MessageBox.Show("catchImageException");
            }
            finally
            {
                ////GC.Collect();
                ////GC.WaitForPendingFinalizers();
            }
            localPictureFilePath = Path.Combine(Application.StartupPath,
                                   "24Color.jpg");
            textEdit1.Text = localPictureFilePath;
            cachPictureFilePath = Path.Combine(Application.StartupPath,
                                               "24ColorCache.jpg");
            loadImage1();
            timer1.Start();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectImageOpenFileDialog = new OpenFileDialog();
            selectImageOpenFileDialog.InitialDirectory = Application.StartupPath;
            selectImageOpenFileDialog.Title = "请选择文件";
            selectImageOpenFileDialog.Filter = "图像文件(*.jpg)|*.jpg|所有文件|*.*";
            selectImageOpenFileDialog.ValidateNames = true;
            selectImageOpenFileDialog.Multiselect = false;
            selectImageOpenFileDialog.CheckPathExists = true;
            selectImageOpenFileDialog.CheckFileExists = true;
            if (selectImageOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((selectImageOpenFileDialog.OpenFile()) != null)
                {
                    if (selectImageOpenFileDialog.FileNames.Length > 1)
                    {
                    }
                    for (int fi = 0; fi < selectImageOpenFileDialog.FileNames.Length; fi++)
                    {
                        localPictureFilePath = selectImageOpenFileDialog.FileNames[fi].ToString();
                        FileInfo sourceFileInfo = new FileInfo(localPictureFilePath);

                        if (sourceFileInfo.Exists)
                        {
                            textEdit1.Text = localPictureFilePath;
                        }
                    }
                }
            }

            cachPictureFilePath = Path.Combine(Path.GetDirectoryName(localPictureFilePath),
                                   Path.GetFileNameWithoutExtension(localPictureFilePath) +
                                   "cach" +
                                   Path.GetExtension(localPictureFilePath));
            loadImage1();
        }

        private void loadImage1()
        {
            if (!String.IsNullOrEmpty(localPictureFilePath) &&
                File.Exists(localPictureFilePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(localPictureFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        try
                        {
                            Image imageFromStream = Image.FromStream(fs, true);
                            if (imageFromStream != null)
                            {
                                Image fromStreamImage = (Image)imageFromStream.Clone();
                                ////ResizeImage(fromStreamImage, Properties.Settings.Default.ThumbnailSize).Save(localTempPictureFilePath, ImageFormat.Jpeg);
                                Image localTempPictureImage = fromStreamImage.GetThumbnailImage(fromStreamImage.Width,
                                                                      fromStreamImage.Height, null, IntPtr.Zero);

                                if (localTempPictureImage != null)
                                {
                                    localTempPictureImage.Save(cachPictureFilePath, ImageFormat.Jpeg);
                                }

                                try
                                {
                                    foreach (GalleryItem eachGalleryItem in galleryControl1.Gallery.Groups[0].Items)
                                    {
                                        eachGalleryItem.Image = (Image)localTempPictureImage.Clone();
                                    }
                                    ////pictureEdit1.Image = (Image)localTempPictureImage.Clone();
                                }
                                catch (OutOfMemoryException catchOutOfMemoryException)
                                {
                                    Debug.WriteLine("catchOutOfMemoryException");
                                    ////MessageBox.Show("catchOutOfMemoryException");
                                }
                                catch (Exception catchImageException)
                                {
                                    ////MessageBox.Show("catchImageException");
                                }
                                finally
                                {
                                    if (fs != null)
                                    {
                                        fs.Close();
                                        fs.Dispose();
                                    }

                                    ////GC.Collect();
                                    ////GC.WaitForPendingFinalizers();
                                }

                                if (fromStreamImage != null) fromStreamImage.Dispose();
                            }
                            if (imageFromStream != null) imageFromStream.Dispose();
                        }
                        catch (OutOfMemoryException catchOutOfMemoryException)
                        {
                            Debug.WriteLine("catchOutOfMemoryException");
                            //MessageBox.Show("catchOutOfMemoryException");
                        }
                        catch (Exception catchImageException)
                        {
                            ////MessageBox.Show("catchImageException");
                        }
                        finally
                        {
                            if (fs != null)
                            {
                                fs.Close();
                                fs.Dispose();
                            }

                            ////GC.Collect();
                            ////GC.WaitForPendingFinalizers();
                        }
                    }
                }
                catch (OutOfMemoryException catchOutOfMemoryException)
                {
                    Debug.WriteLine("catchOutOfMemoryException");
                    // MessageBox.Show("catchOutOfMemoryException");
                }
                catch (Exception catchException)
                {
                    ////MessageBox.Show("catchException");
                }
                finally
                {

                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
 
        private void timer1_Tick(object sender, EventArgs e)
        {
            test1();
        }

        private void test1()
        {
            if (!String.IsNullOrEmpty(cachPictureFilePath) &&
                File.Exists(cachPictureFilePath))
            {
                FileInfo fi = new FileInfo(cachPictureFilePath);
                if (fi.Length > 0)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(cachPictureFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            try
                            {
                                Image imageFromStream = Image.FromStream(fs);
                                if (imageFromStream != null)
                                {
                                    new Thread(new ParameterizedThreadStart(delegate (object threadObject)
                                    {
                                        Thread.CurrentThread.IsBackground = true;
                                        if (!this.Disposing && !this.IsDisposed)
                                        {
                                            this.BeginInvoke(new MethodInvoker(delegate
                                            {
                                                GalleryControl threadObjectGalleryControl = null;
                                                PictureEdit threadObjectPictureEdit = null;
                                                Image threadObjectStreamImage = null;
                                                if (threadObject is Object[])
                                                {
                                                    foreach (object eachObject in threadObject as Object[])
                                                    {
                                                        if (eachObject is GalleryControl)
                                                        {
                                                            threadObjectGalleryControl = eachObject as GalleryControl;
                                                        }
                                                        if (eachObject is PictureEdit)
                                                        {
                                                            threadObjectPictureEdit = eachObject as PictureEdit;
                                                        }
                                                        if (eachObject is Image)
                                                        {
                                                            threadObjectStreamImage = eachObject as Image;
                                                        }
                                                    }
                                                }
                                                if (threadObjectGalleryControl == null)
                                                    threadObjectGalleryControl = galleryControl1;
                                                ////if (threadObjectPictureEdit == null)
                                                ////    threadObjectPictureEdit = pictureEdit1;

                                                try
                                                {
                                                    if (threadObjectGalleryControl != null)
                                                    {
                                                        foreach (GalleryItem eachGalleryItem in threadObjectGalleryControl.Gallery.Groups[0].Items)
                                                        {
                                                            eachGalleryItem.Image = (Image)threadObjectStreamImage.Clone();

                                                            {
                                                                count1++;
                                                            }
                                                        }
                                                    }
                                                    if (threadObjectPictureEdit != null)
                                                    {
                                                        threadObjectPictureEdit.Image = (Image)threadObjectStreamImage.Clone();

                                                        {
                                                            count2++;
                                                        }
                                                    }
                                                }
                                                catch (OutOfMemoryException catchOutOfMemoryException)
                                                {
                                                    timer1.Stop();
                                                    Debug.WriteLine("catchOutOfMemoryException");
                                                    //MessageBox.Show("catchOutOfMemoryException");
                                                }
                                                catch (Exception catchImageException)
                                                {
                                                    ////MessageBox.Show("catchImageException");
                                                }
                                                finally
                                                {
                                                    if (fs != null)
                                                    {
                                                        ////fs.Close();
                                                        ////fs.Dispose();
                                                    }

                                                    ////GC.Collect();
                                                    ////GC.WaitForPendingFinalizers();
                                                }
                                            }
                                            ));
                                        }
                                    }
                                    )).Start(new Object[] {imageFromStream});
                                }
                            }
                            catch (OutOfMemoryException catchOutOfMemoryException)
                            {
                                timer1.Stop();
                                Debug.WriteLine("catchOutOfMemoryException");
                                //MessageBox.Show("catchOutOfMemoryException");
                            }
                            catch (Exception catchImageException)
                            {
                                ////MessageBox.Show("catchImageException");
                            }
                            finally
                            {
                                if (fs != null)
                                {
                                    fs.Close();
                                    fs.Dispose();
                                }

                                ////GC.Collect();
                                ////GC.WaitForPendingFinalizers();
                            }
                        }
                    }
                    catch (OutOfMemoryException catchOutOfMemoryException)
                    {
                        Debug.WriteLine("catchOutOfMemoryException");
                        //MessageBox.Show("catchOutOfMemoryException");
                    }
                    catch (Exception catchException)
                    {
                        ////MessageBox.Show("catchException");
                    }
                    finally
                    {

                    }
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            loadImage1();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            foreach (GalleryItem eachGalleryItem in galleryControl1.Gallery.Groups[0].Items)
            {
                eachGalleryItem.Image = null;
            }
            ////pictureEdit1.Image = null;
        }
    }
}
