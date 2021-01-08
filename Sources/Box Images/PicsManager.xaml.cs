using DxTBoxCore.Languages;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DxTBoxCore.Cont;
using DxTBoxCore.Common;

namespace DxTBoxCore.Images
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class PicsManager : Window
    {
        private string DllLocation;
        const int valString = 100;

        /// <summary>
        /// Liste des images à trier
        /// </summary>
        //public List<string> ListImages { get; set; } = new List<string>();

        public Queue<DxImage> QImages { get; set; } = new Queue<DxImage>();

        private DxImage iLeftPic;
        private DxImage iRightPic;

        public PicsManager()
        {
            InitializeComponent();
            DllLocation = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (QImages == null) throw new Exception("Queue of images is null !");

            tbLeftTitle.Text = /*tbRightTitle.Text =*/ "";
            
            iLeftPic = QImages.Dequeue();
            SetLeftImage(iLeftPic);
            //QImages.Enqueue(sLeftPic);

            if (QImages.Count() > 0)
            {
                iRightPic = QImages.Dequeue();
                SetRightImage(iRightPic);
            }
            //QImages.Enqueue(sRightPic);
            /*
            SetLeftImage(ListImages[0]);
            LeftPos = 0;
            SetRightImage(ListImages[1]);
            RightPos = 1;*/
        }

        /// <summary>
        /// Set Main title on the top of window
        /// </summary>
        /// <param name="title"></param>
        public void SetMainTitle(string title)
        {
            tbTitre.Text = title;
        }

        public void SetHash(string hash)
        {
            /////////   tbHash.Text = hash;
        }

        /// <summary>
        /// Set image, path, resolution to left side
        /// </summary>
        /// <param name="img"></param>
        public void SetLeftImage(DxImage img)
        {
            BitmapImage biImage = new BitmapImage();
            // Obligatoire pour pouvoir effacer ensuite
            biImage.BeginInit();
            biImage.CacheOption = BitmapCacheOption.OnLoad;
            biImage.UriSource = new Uri(img.FullPath);
            biImage.EndInit();
            LeftPic.Source = biImage;
            //
            /////////         tbLeftImagePath.ToolTip = img;
            /////////         tbLeftImagePath.Text = SizeString(img.FullPath);

            tbLeftTitle.Text = img.Title;
            tbLeftDims.Text = $"{biImage.PixelWidth} x {biImage.PixelHeight}";
        }

        /// <summary>
        /// Set image, path, resolution to right side
        /// </summary>
        /// <param name="img"></param>
        public void SetRightImage(DxImage img)
        {

            BitmapImage biImage = new BitmapImage();
            // Obligatoire pour pouvoir effacer ensuite
            biImage.BeginInit();
            biImage.CacheOption = BitmapCacheOption.OnLoad;
            biImage.UriSource = new Uri(img.FullPath);
            biImage.EndInit();
            //

            //BitmapImage biImage = new BitmapImage(new Uri(imgLink));
            RightPic.Source = biImage;

            /////////     tbRightImagePath.ToolTip = img;
            /////////    tbRightImagePath.Text = SizeString(img.FullPath);

            ///////// tbRightTitle.Text = img.Title;
            tbRightDims.Text = $"{biImage.PixelWidth} x {biImage.PixelHeight}";
        }

        /// <summary>
        /// Resize string to fit
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string SizeString(string value)
        {
            if (value.Length > valString) return "[...]" + value.Substring(value.Length - valString);

            return value;
        }

        /// <summary>
        /// Click on a pic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic_Clicked(object sender, MouseButtonEventArgs e)
        {
            Image imgBox = (Image)sender;
            BigPic showBig = new BigPic();
            showBig.imageBox.Source = imgBox.Source;
            showBig.ShowDialog();
        }

        /// <summary>
        /// External Launch left image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftLab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /////////  Process.Start(tbLeftImagePath.Text);
        }

        /// <summary>
        /// External Launch right image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightLab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ///////// Process.Start(tbRightImagePath.Text);
        }

        /*
        private void LeftTrash2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (QImages.Count > 1)
            {
                QImages.Enqueue(sLeftPic);
                sLeftPic = QImages.Dequeue();
                SetLeftImage(sLeftPic);
                //    if (tmp == sLeftPic || tmp == sRightPic) tmp
            }
        }*/

        /// <summary>
        /// Left Forward
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftForward_Click(object sender, RoutedEventArgs e)
        {
            if (QImages.Count > 0)
            {
                QImages.Enqueue(iLeftPic);
                iLeftPic = QImages.Dequeue();
                SetLeftImage(iLeftPic);
            }
        }

        /// <summary>
        /// Right Forward
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightForward_Click(object sender, RoutedEventArgs e)
        {
            if (QImages.Count > 0)
            {
                QImages.Enqueue(iRightPic);
                iRightPic = QImages.Dequeue();
                SetRightImage(iRightPic);
            }
        }

        /// <summary>
        /// Delete left file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftTrash_Click(object sender, RoutedEventArgs e)
        {
            DeleteFile(iLeftPic.FullPath);

            if (QImages.Count > 0)
            {
                iLeftPic = QImages.Dequeue();
                SetLeftImage(iLeftPic);
            }
            else
            {
                LeftPic.Source = new BitmapImage(new Uri(@"/DxTBoxCore;component/Resources/no-photo.png", UriKind.Relative));
                iLeftPic = null;

                /////////   tbLeftTitle.Text = tbLeftImagePath.Text = "No More Pics";
                tbLeftDims.Text = "";

                LeftForward.IsEnabled = false;
                RightForward.IsEnabled = false;
                LeftTrash.IsEnabled = false;
            }
        }

        /// <summary>
        /// Delete right file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightTrash_Click(object sender, RoutedEventArgs e)
        {
            DeleteFile(iRightPic.FullPath);

            if (QImages.Count > 0)
            {
                iRightPic = QImages.Dequeue();
                SetRightImage(iRightPic);
            }
            else
            {
                this.RightPic.Source = new BitmapImage(new Uri(@"/DxTBoxCore;component/Resources/no-photo.png", UriKind.Relative));
                iRightPic = null;

                /////////    tbRightTitle.Text =tbRightImagePath.Text= "No More Pics";
                tbRightDims.Text = "";


                LeftForward.IsEnabled = false;
                RightForward.IsEnabled = false;
                RightTrash.IsEnabled = false;
            }
        }
        
        /// <summary>
        /// Delete function
        /// </summary>
        /// <param name="toDel"></param>
        private void DeleteFile(string toDel)
        {
            bool? res = MBox.DxMBox.ShowDial(DxTBLang.Trash_Question + $"\n{toDel}", DxTBLang.Trash_FileTitle, DxButtons.YesNo);
            if (res == true)
            {
                try
                {
                    FileSystem.DeleteFile(toDel, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                }
            }
        }

        /// <summary>
        /// Button to leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtLeave_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Fin de duplicate");
            DialogResult = true;
            this.Close();
        }

        private void StopAll_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Arrêt total demandé");
            DialogResult = false;
            this.Close();
        }
    }
}
