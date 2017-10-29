using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using ClickerSDK.Keyboard;
using ClickerSDK.Screen;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ClickerSDK;
using ClickerSDK.Wind;
using LiteDB;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace ForTest
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region StateMonitor
        public class RectPoint
        {
            public int x1 { get; set; }
            public int y1 { get; set; }
            public int x2 { get; set; }
            public int y2 { get; set; }
            public string Hash { get; set; }
            public bool Contrast { get; set; }

            public bool IsSuccess()
            {
                return Hash.Equals(GetHash(x1, y1, x2, y2, Contrast));
            }

            public string ComputeHash()
            {
                return GetHash(x1, y1, x2, y2, Contrast);
            }

            public string ComputeHash(out string pathOfFile)
            {
                var x = GetHash(x1, y1, x2, y2, Contrast, false, out pathOfFile);

                return x;
            }
        }

        private RectPoint FastAnaliz = new RectPoint()
        {
            x1 = 428,
            y1 = 596,
            x2 = 440,
            y2 = 610,
            Contrast = false,
            Hash = "971232CFAA594B3EDA24EF905221D375"
        };

        private RectPoint FastAnaliz2 = new RectPoint()
        {
            x1 = 428,
            y1 = 596,
            x2 = 440,
            y2 = 610,
            Contrast = true,
            Hash = "3C654AB05B5B9CBF7279E9FEAB01E99D"
        };


        private RectPoint BadConnection = new RectPoint()
        {
            x1 = 437,
            y1 = 215,
            x2 = 446,
            y2 = 220,
            Contrast = true,
            Hash = "2022EDD21EFAC332627EFFAFBF8A5857"
        };

        private RectPoint SlideIdenty = new RectPoint()
        {
            x1 = 53,
            y1 = 76,
            x2 = 73,
            y2 = 224,
            Contrast = true
        };

        private RectPoint D1Point = new RectPoint()
        {
            x1 = 44,
            y1 = 30,
            x2 = 50,
            y2 = 40,
            Hash = "77E81C6911749880EC67EA448D37E56C",
            Contrast = true
        };

        private RectPoint SuccesAnalizPoint2 = new RectPoint()
        {
            x1 = 517,
            y1 = 97,
            x2 = 525,
            y2 = 100,
            Hash = "8FDC01C1B6195D1B7B96E472498DB282",
            Contrast = false
        };

        private RectPoint SuccesAnalizPoint = new RectPoint()
        {
            x1 = 482,
            y1 = 94,
            x2 = 490,
            y2 = 100,
            Hash = "AD88C83AB8E8CB560212B3C10486E7B2",
            Contrast = false
        };

        private RectPoint SuccesAnalizPoint3 = new RectPoint()
        {
            x1 = 459,
            y1 = 98,
            x2 = 463,
            y2 = 102,
            Hash = "A5038DBF8E43BB1C7A7C56421F9D5CAD",
            Contrast = false
        };

        private RectPoint SuccesAnalizPoint4 = new RectPoint()
        {
            x1 = 540,
            y1 = 95,
            x2 = 542,
            y2 = 97,
            Hash = "F0975A85BED013CFB12FE6764AAAC871",
            Contrast = false
        };

        private RectPoint SuccesAnalizPoint5 = new RectPoint()
        {
            x1 = 422,
            y1 = 94,
            x2 = 424,
            y2 = 96,
            Hash = "32C009AEA94A9CFE81D8E91BECC62894",
            Contrast = false
        };

        private RectPoint BadAnalizPoint = new RectPoint()
        {
            x1 = 463,
            y1 = 96,
            x2 = 465,
            y2 = 98,
            Hash = "4E5E0A5035C331946C7C6AB62514A337",
            Contrast = false
        };


        private RectPoint BadAnalizPoint2 = new RectPoint()
        {
            x1 = 477,
            y1 = 89,
            x2 = 480,
            y2 = 92,
            Hash = "D93660DA5C9383729DECF4024E0CCB46",
            Contrast = false
        };

        private RectPoint BadAnalizPoint3 = new RectPoint()
        {
            x1 = 479,
            y1 = 101,
            x2 = 481,
            y2 = 103,
            Hash = "D458C5A014D30E7C08B9A25CA48DA04E",
            Contrast = false
        };

        private RectPoint PrizePoint = new RectPoint()
        {
            x1 = 494,
            y1 = 180,
            x2 = 500,
            y2 = 190,
            Hash = "412229D5BB0E80521A760211F01B918D",
            Contrast = true
        };

        private RectPoint PrizePoint2 = new RectPoint()
        {
            x1 = 496,
            y1 = 165,
            x2 = 500,
            y2 = 170,
            Hash = "6A4A3F8FAD5446AFBED3D6CD8EBB0B3E",
            Contrast = true
        };

        private RectPoint PrizePoint3 = new RectPoint()
        {
            x1 = 553,
            y1 = 144,
            x2 = 556,
            y2 = 151,
            Hash = "50B532A2DEA6B058935C69819B0F82FD",
            Contrast = true
        };

        private RectPoint PrizePoint4 = new RectPoint()
        {
            x1 = 471,
            y1 = 194,
            x2 = 473,
            y2 = 197,
            Hash = "66C8FC8820A48F52622347D87355CC85",
            Contrast = true
        };

        private RectPoint PrizePoint5 = new RectPoint()
        {
            x1 = 544,
            y1 = 256,
            x2 = 548,
            y2 = 260,
            Hash = "5AA9CAA6160FC8299BE755E720DFB89A",
            Contrast = true
        };

        private RectPoint PrizePoint6 = new RectPoint()
        {
            x1 = 409,
            y1 = 112,
            x2 = 411,
            y2 = 114,
            Hash = "A0B7AFEEF8412FCFC9176452102234B5",
            Contrast = false
        };

        private RectPoint per990 = new RectPoint()
        {
            x1 = 476,
            y1 = 51,
            x2 = 503,
            y2 = 59,
            Hash = "10EAA2D7757ACF1F9A1B81BD54039E65",
            Contrast = true
        };

        private RectPoint per989 = new RectPoint()
        {
            x1 = 476,
            y1 = 51,
            x2 = 503,
            y2 = 59,
            Hash = "3B8C29A0F9C9808E236685F98487AC08",
            Contrast = true
        };

        private RectPoint AnalizPointPoint = new RectPoint()
        {
            x1 = 801,
            y1 = 481,
            x2 = 809,
            y2 = 490,
            Hash = "115A072C5DD6A69A0C6BD0D9A8DA2DC1",
            Contrast = true
        };

        private RectPoint AnalizPointPoint2 = new RectPoint()
        {
            x1 = 73,
            y1 = 261,
            x2 = 77,
            y2 = 269,
            Hash = "B1E104B984F83DC02C1E7DF96EC4F8ED",
            Contrast = true
        };

        private RectPoint AnalizPointPoint3 = new RectPoint()
        {
            x1 = 747,
            y1 = 418,
            x2 = 753,
            y2 = 422,
            Hash = "F8A5AA9D4D001CD5940ADBD9B455EC45",
            Contrast = true
        };

        private RectPoint ConcensusPoint = new RectPoint()
        {
            x1 = 255,
            y1 = 89,
            x2 = 259,
            y2 = 91,
            Hash = "DCF69082C55B79CDE48187EED0628026",
            Contrast = false
        };

        private RectPoint ConcensusPoint2 = new RectPoint()
        {
            x1 = 330,
            y1 = 96,
            x2 = 335,
            y2 = 100,
            Hash = "BD5FFE462DC921D11AAF5CAE8FF5B310",
            Contrast = false
        };

        private RectPoint ConcensusPoint3 = new RectPoint()
        {
            x1 = 698,
            y1 = 97,
            x2 = 703,
            y2 = 102,
            Hash = "8B4CC2F58B049BB163EFCE4904DADADF",
            Contrast = false
        };

        private RectPoint ConcensusPoint4 = new RectPoint()
        {
            x1 = 495,
            y1 = 75,
            x2 = 497,
            y2 = 97,
            Hash = "82CE9454EEC439A6320EE3C154F9C4B4",
            Contrast = false
        };

        private RectPoint ConcensusPoint5 = new RectPoint()
        {
            x1 = 407,
            y1 = 92,
            x2 = 410,
            y2 = 98,
            Hash = "4BD513E97008950D74E64EE328FC0357",
            Contrast = false
        };

        private static string ScrrenShotTempFolder
        {
            get
            {
                var x = Path.Combine(_screnShotFolder, "Temp");

                if (!Directory.Exists(x))
                {
                    Directory.CreateDirectory(x);
                }

                return x;
            }
        }

        private static string _screnShotFolder =
            Path.Combine(
                new System.IO.FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).Directory.FullName,
                "screenshots");

        enum Stage
        {
            Uncknow,
            BadAnaliz,
            ConcesusAnaliz,
            Prize,
            SuccessAnaliz,
            Analiz,
            BadConnection
        }

        private Stage CurrentStage = Stage.Uncknow;
        private Stage PrevStage = Stage.Uncknow;

        private void CheckState()
        {
            PrevStage = CurrentStage;

            if (BadConnection.IsSuccess())
            {
                CurrentStage = Stage.BadConnection;
            }
            else
            if (BadAnalizPoint.IsSuccess() || BadAnalizPoint2.IsSuccess() || BadAnalizPoint3.IsSuccess())
            {

                CurrentStage = Stage.BadAnaliz;
            }
            else if (ConcensusPoint.IsSuccess() || ConcensusPoint2.IsSuccess() || ConcensusPoint3.IsSuccess() || ConcensusPoint4.IsSuccess() || ConcensusPoint5.IsSuccess())
            {

                CurrentStage = Stage.ConcesusAnaliz;
            }
            else if (PrizePoint6.IsSuccess())
            {
                CurrentStage = Stage.Prize;
            }
            else if (SuccesAnalizPoint.IsSuccess() || SuccesAnalizPoint2.IsSuccess() || SuccesAnalizPoint3.IsSuccess() || SuccesAnalizPoint4.IsSuccess() || SuccesAnalizPoint5.IsSuccess())
            {

                CurrentStage = Stage.SuccessAnaliz;
            }
            else
          if (AnalizPointPoint.IsSuccess() || AnalizPointPoint2.IsSuccess() || AnalizPointPoint3.IsSuccess())
            {

                CurrentStage = Stage.Analiz;
            }
            else
            {

                CurrentStage = Stage.Uncknow;
            }
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                StagePositionTextBlock.Text = CurrentStage.ToString();
            }));
        }

        private bool CheckPosition()
        {
            if (D1Point.IsSuccess())
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                 {
                     CheckPositionTextBlock.Text = "Success position";
                 }));

                return true;
            }
            else
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    CheckPositionTextBlock.Text = "Cancel position";
                }));

                return false;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            keyboard.Dispose();
        }

        Image CaptureScreen(int sourceX, int sourceY, int destX, int destY,
            System.Drawing.Size regionSize)
        {
            Bitmap bmp = new Bitmap(regionSize.Width, regionSize.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(sourceX, sourceY, destX, destY, regionSize);
            return bmp;
        }

        public static Bitmap GrayScale(Bitmap Bmp)
        {
            int rgb;
            Color c;

            for (int y = 0; y < Bmp.Height; y++)
                for (int x = 0; x < Bmp.Width; x++)
                {
                    c = Bmp.GetPixel(x, y);
                    rgb = (int)((c.G + c.B) / 2);
                    Bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            return Bmp;
        }

        public static void GetScreen(string fileName, int x1, int y1, int x2, int y2)
        {
            Rectangle rect = new Rectangle(0, 0, x2 - x1, y2 - y1);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(x1, y1, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            // bmp = AdjustContrast(GrayScale(bmp), (float)500);

            bmp.Save(fileName, ImageFormat.Png);
        }
        public static void GetContrastScreen(string fileName, int x1, int y1, int x2, int y2)
        {
            Rectangle rect = new Rectangle(0, 0, x2 - x1, y2 - y1);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(x1, y1, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            bmp = AdjustContrast(GrayScale(bmp), (float)500);

            bmp.Save(fileName, ImageFormat.Png);
        }

        private static string GetTempImagePath()
        {
            return Path.Combine(ScrrenShotTempFolder, Guid.NewGuid() + ".png");
        }

        public static string GetHash(int x1, int y1, int x2, int y2, bool contrast, bool clean, out string fileName)
        {
            fileName = GetTempImagePath();

            try
            {
                if (contrast)
                {
                    GetContrastScreen(fileName, x1, y1, x2, y2);
                }
                else
                {
                    GetScreen(fileName, x1, y1, x2, y2);
                }

                return GetMD5HashFromFile(fileName);
            }
            finally
            {
                if (clean && File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
        }

        public static string GetHash(int x1, int y1, int x2, int y2, bool contrast, bool clean = true)
        {
            string skip;
            return GetHash(x1, y1, x2, y2, contrast, clean, out skip);
        }

        public static string GetMD5HashFromFile(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

        public static Bitmap AdjustContrast(Bitmap Image, float Value)
        {
            Value = (100.0f + Value) / 100.0f;
            Value *= Value;
            Bitmap NewBitmap = (Bitmap)Image.Clone();
            BitmapData data = NewBitmap.LockBits(
                new Rectangle(0, 0, NewBitmap.Width, NewBitmap.Height),
                ImageLockMode.ReadWrite,
                NewBitmap.PixelFormat);
            int Height = NewBitmap.Height;
            int Width = NewBitmap.Width;

            unsafe
            {
                for (int y = 0; y < Height; ++y)
                {
                    byte* row = (byte*)data.Scan0 + (y * data.Stride);
                    int columnOffset = 0;
                    for (int x = 0; x < Width; ++x)
                    {
                        byte B = row[columnOffset];
                        byte G = row[columnOffset + 1];
                        byte R = row[columnOffset + 2];

                        float Red = R / 255.0f;
                        float Green = G / 255.0f;
                        float Blue = B / 255.0f;
                        Red = (((Red - 0.5f) * Value) + 0.5f) * 255.0f;
                        Green = (((Green - 0.5f) * Value) + 0.5f) * 255.0f;
                        Blue = (((Blue - 0.5f) * Value) + 0.5f) * 255.0f;

                        int iR = (int)Red;
                        iR = iR > 255 ? 255 : iR;
                        iR = iR < 0 ? 0 : iR;
                        int iG = (int)Green;
                        iG = iG > 255 ? 255 : iG;
                        iG = iG < 0 ? 0 : iG;
                        int iB = (int)Blue;
                        iB = iB > 255 ? 255 : iB;
                        iB = iB < 0 ? 0 : iB;

                        row[columnOffset] = (byte)iB;
                        row[columnOffset + 1] = (byte)iG;
                        row[columnOffset + 2] = (byte)iR;

                        columnOffset += 4;
                    }
                }
            }

            NewBitmap.UnlockBits(data);

            return NewBitmap;
        }

        private bool IsClosed = false;

        private void GetHash(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 40; i++)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        ToDiscaveryLog(GetHash(int.Parse(X1.Text), int.Parse(Y1.Text), int.Parse(X2.Text),
                            int.Parse(Y2.Text), Contrast.IsChecked != null && Contrast.IsChecked.Value, false));
                        Thread.Sleep(100);
                    }));
                }
            });
            
        }

        #endregion

        private void ToLog(string message)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                var logText = String.Concat(message, Environment.NewLine, Log.Text);

                if (logText.Length > 1000)
                {
                    logText = logText.Remove(900);
                }

                Log.Text = logText;
            }));
        }

        private void ToDiscaveryLog(string message)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                var logText = String.Concat(message, Environment.NewLine, DiscaveryLog.Text);

                if (logText.Length > 1000)
                {
                    logText = logText.Remove(900);
                }

                DiscaveryLog.Text = logText;
            }));
        }

        private ClickerSDK.Mouse mouse;
        private ClickerSDK.Keyboard.Keyboard keyboard;

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            IsClosed = true;
            keyboard.Dispose();
        }

        private string CurSlideHash;
        public List<MouseClickEventArgs> recMouseClick = new List<MouseClickEventArgs>();
        private string pHash = string.Empty;
        private bool _isPause;

        private void PlayAction()
        {
            ClearTemp();

            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                Stat.Text = string.Concat(statSuccess, "/", statFail);
                StagePositionTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                MainGrid.Background = new SolidColorBrush(Colors.Transparent);
            }));

            CheckPause();
            switch (CurrentStage)
            {
                case Stage.BadConnection:
                    Thread.Sleep(500);
                    mouse.MouseClick(MouseButton.Left, 650, 472);
                    break;

                case Stage.Analiz:
                    string pathOfCurAnalizImage;

                    pathOfCurAnalizImage = GetSlideHash();
                    if (CurSlideHash == "2F0584083F44B78ACE619C898F442496")
                    {
                        CurrentStage = Stage.Uncknow;
                        return;
                    }

                    Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        CurrentAnalizImage.Source = new BitmapImage(new Uri(pathOfCurAnalizImage));
                    }));

                    if (CurSlideHash.Equals(pHash))
                    {
                        break;
                    }
                    ToDiscaveryLog("New analiz slide.");
                    recMouseClick = new List<MouseClickEventArgs>();
                    var knowSlide = GetKnowSlide(CurSlideHash);
                    if (knowSlide == null)
                    {
                        var speedTraining = IsTraining;

                        if (speedTraining)
                        {
                            CurrentStage = Stage.Uncknow;
                            ClickNext(IsFast);
                        }
                        //recMouseClick = new List<MouseClickEventArgs>();
                    }
                    else
                    {
                        if (knowSlide.IsSuccess)
                        {
                            if (string.IsNullOrEmpty(knowSlide.ClickEventArgses))
                            {
                                SaveSlide(CurSlideHash, false, true);
                            }
                            else
                            {
                                PlaySlide(knowSlide);
                            }

                            if (IsFast)
                            {
                                CurrentStage = Stage.Uncknow;
                                ClickNext(true);
                            }
                        }
                        else
                        {
                            //recMouseClick = new List<MouseClickEventArgs>();
                            if (IsBot)
                            {
                                ClickNext();

                                if (IsFast)
                                {
                                    CurrentStage = Stage.Uncknow;
                                    ClickNext(true);
                                }
                            }
                            else
                            {
                                StopBotAndShowSavedScreen(knowSlide);
                            }


                        }
                    }

                    pHash = CurSlideHash;
                    break;
                case Stage.SuccessAnaliz:
                    statSuccess++;

                    if (!IsFast)
                    {
                        SaveSlide(CurSlideHash, true);
                    }
                    recMouseClick = new List<MouseClickEventArgs>();
                    ClickNext(true);
                    break;
                case Stage.ConcesusAnaliz:
                    //SaveSlide(CurSlideHash, true);
                    ToDiscaveryLog("Consensus.");
                    recMouseClick = new List<MouseClickEventArgs>();
                    ClickNext(true);
                    break;
                case Stage.BadAnaliz:
                    statFail++;
                    if (!IsFast)
                    {
                        SaveBadScreen(CurSlideHash);
                        SaveSlide(CurSlideHash, false);
                    }
                    recMouseClick = new List<MouseClickEventArgs>();
                    ClickNext(true);
                    break;
                case Stage.Prize:
                    ClickNext(true);
                    recMouseClick = new List<MouseClickEventArgs>();
                    break;
                case Stage.Uncknow:

                    break;
            }
        }

        private void ClearTemp()
        {
            foreach (var x in new DirectoryInfo(ScrrenShotTempFolder).GetFiles())
            {
                try
                {
                    File.Delete(x.FullName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            };
        }

        private string GetSlideHash()
        {
            string pathOfCurAnalizImage;
            CurSlideHash = SlideIdenty.ComputeHash(out pathOfCurAnalizImage);

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(200);
                if (GetKnowSlide(CurSlideHash) != null)
                {
                    break;
                }
            }

            return pathOfCurAnalizImage;
        }

        private bool IsBot
        {
            get
            {
                var speedTraining = false;
                Dispatcher.Invoke(DispatcherPriority.Background,
                    new Action(() => { speedTraining = Bot.IsChecked ?? false; }));
                return speedTraining;
            }
        }

        private bool IsTraining
        {
            get
            {
                var speedTraining = false;
                Dispatcher.Invoke(DispatcherPriority.Background,
                    new Action(() => { speedTraining = SpeedTraining.IsChecked ?? false; }));
                return speedTraining;
            }
        }

        private bool IsFast
        {
            get
            {
                var x = false;
                Dispatcher.Invoke(DispatcherPriority.Background,
                    new Action(() => { x = Fast.IsChecked ?? false; }));
                return x;
            }
        }

        private void SaveBadScreen(string curSlideHash)
        {
            if (!string.IsNullOrWhiteSpace(CurSlideHash))
            {
                ToDiscaveryLog($"Saved bad screen: {curSlideHash}");

                var dir = Path.Combine(_screnShotFolder, curSlideHash);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var newFileName = Path.Combine(dir, Guid.NewGuid().ToString());

                GetScreen(newFileName, 43, 52, 913, 248);
            }
        }

        private string DbName => string.Concat(Environment.MachineName, Environment.MachineName, ".db");

        private void SaveSlide(string hash, bool isSuccess, bool force = false)
        {
            if (!string.IsNullOrEmpty(hash))
            {
                using (var db = new LiteDatabase(Path.Combine(_screnShotFolder, DbName)))
                {
                    var knowSlides = db.GetCollection<KnowSlide>("KnowSlide");
                    knowSlides.EnsureIndex(x => x.Hash);

                    var oldSlide = knowSlides.FindOne(slide => slide.Hash == hash);

                    if (oldSlide != null)
                    {
                        if ((!oldSlide.IsSuccess && isSuccess) || force)
                        {
                            ToDiscaveryLog($"Update slide. Success: {isSuccess}. ClicCount {recMouseClick.Count}");
                            oldSlide.ClickEventArgses = ToStr(recMouseClick.ToArray());
                            oldSlide.IsSuccess = isSuccess;
                            knowSlides.Update(oldSlide);
                        }
                        else
                        {
                            ToDiscaveryLog("Ignore save.");
                        }
                    }
                    else
                    {
                        ToDiscaveryLog($"Save new slide. Success: {isSuccess}. ClicCount {recMouseClick.Count}");
                        knowSlides.Insert(new KnowSlide()
                        {
                            IsSuccess = isSuccess,
                            ClickEventArgses = ToStr(recMouseClick.ToArray()),
                            Hash = hash
                        });
                    }
                }
            }
        }

        public void CheckPause()
        {
            while (IsPause)
            {
                Thread.Sleep(500);
            }
        }

        private bool IsPause
        {
            get { return _isPause; }
            set
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    StateTextBlock.Text = value ? "Paused" : "Rec";
                    ToDiscaveryLog(value ? "Stoped..." : "Play...");
                }));

                _isPause = value;
            }
        }

        private void StopBotAndShowSavedScreen(KnowSlide knowSlide)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                MainGrid.Background = new SolidColorBrush(Colors.YellowGreen);
                StagePositionTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }));

            //IsPause = true;
            var dirHash = new DirectoryInfo(Path.Combine(_screnShotFolder, knowSlide.Hash));

            if (dirHash.Exists)
            {
                var files = dirHash.GetFiles();

                if (files.Any())
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        BadAnalizImage.Source = new BitmapImage(new Uri(files.First().FullName));
                    }));
                }
            }

            ToDiscaveryLog("Auto pause. Checked sliede! Prev bad status. See screen. Hash: " + knowSlide.Hash);
            recMouseClick = new List<MouseClickEventArgs>();
        }

        public static int indexOfActiveWindow = 0;

        private static Process[] _eveProcess = Process.GetProcessesByName("exefile");

        private static void ChangeEve()
        {
            prevIndexWindow = indexOfActiveWindow;

            if (++indexOfActiveWindow >= _eveProcess.Length)
            {
                indexOfActiveWindow = 0;
            }

            WindHandle.ActiveWindow(_eveProcess[indexOfActiveWindow]);
        }

        private bool IsPlayed { get; set; }

        private void PlaySlide(KnowSlide knowSlide)
        {
            try
            {
                IsPlayed = true;

                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    MainGrid.Background = new SolidColorBrush(Colors.YellowGreen);
                }));

                Thread.Sleep(500);

                ToDiscaveryLog("Checked slide");
                foreach (var a in ToEventArgs(knowSlide.ClickEventArgses))
                {
                    if (a.X > 38 && a.X < 922 && a.Y > 46 && a.Y < 618)
                    {
                        CheckPause();

                        mouse.MouseClick(a.MouseButton, a.X, a.Y);

                        if (a.X > 52 && a.X < 914 && a.Y > 81 && a.Y < 247)
                        {
                            Thread.Sleep(700);
                        }
                        else
                        {
                            Thread.Sleep(3500);
                        }
                    }
                }

                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    MainGrid.Background = new SolidColorBrush(Colors.Transparent);
                }));
            }
            finally
            {
                IsPlayed = false;
            }
        }

        private int statSuccess = 0;
        private int statFail = 0;

        public class KnowSlide
        {
            public int Id { get; set; }

            public string Hash { get; set; }

            public bool IsSuccess { get; set; }

            public string ClickEventArgses { get; set; }
        }

        public string ToStr(MouseClickEventArgs[] argses)
        {
            var r = string.Empty;

            foreach (var x in argses)
            {
                r = string.Concat(r, ";", (byte)x.MouseButton, "|", x.X, "|", x.Y);
            }

            return r;
        }

        public MouseClickEventArgs[] ToEventArgs(string argses)
        {
            var r = new List<MouseClickEventArgs>();

            foreach (var x in argses.Split(';').Where(s => !string.IsNullOrWhiteSpace(s)))
            {
                var xArr = x.Split('|');

                r.Add(new MouseClickEventArgs((MouseButton)byte.Parse(xArr[0]), int.Parse(xArr[1]), int.Parse(xArr[2])));
            }

            return r.ToArray();
        }

        private KnowSlide GetKnowSlide(string hash)
        {
            using (var db = new LiteDatabase(Path.Combine(_screnShotFolder, DbName)))
            {
                var knowSlides = db.GetCollection<KnowSlide>("KnowSlide");
                //knowSlides.EnsureIndex(x => x.Hash);
                return knowSlides.FindOne(slide => slide.Hash == hash);
            }
        }

        //508 603
        private void ClickNext(bool changeWindow = false)
        {
            mouse.MouseClick(MouseButton.Left, 508, 603);

            if (changeWindow)
            {
                Thread.Sleep(50);
                ChangeEve();
                Thread.Sleep(200);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            //SynkDb();



            mouse = Mouse.Instanse;


            var eveProvess = Process.GetProcessesByName("exefile");

            if (eveProvess.Any())
            {
                var z = WindHandle.GetWindowRect(eveProvess[0]);
                ToDiscaveryLog($"{z.Top} {z.Left} {z.Right} {z.Bottom}");
            }

            //if (eveProvess.Any())
            //{
            //    ToDiscaveryLog("Finded exe file");
            //}

            mouse.OnClick += (sender, args) =>
            {
                if (args.X > 0 && args.X < 935 && args.Y > 0 && args.Y < 628 && !IsPause)
                {
                    if (recMouseClick != null)
                    {
                        ToDiscaveryLog($"Stored click");
                        recMouseClick?.Add(args);
                    }

                }

                ToLog($"Mouse click. Mouse button:{args.MouseButton}, X: {args.X}, Y: {args.Y}");
            };

            keyboard = ClickerSDK.Keyboard.Keyboard.Instanse;
            keyboard.OnKeyPress += (sender, args) =>
            {
                if (args.ConsoleKeys == ConsoleKey.S && args.KeyModifer == KeyModifers.Ctrl)
                {
                    IsPause = true;
                }

                if (args.ConsoleKeys == ConsoleKey.R && args.KeyModifer == KeyModifers.Ctrl)
                {
                    IsPause = false;
                }

                ToLog($"Press key {args.ConsoleKeys}. Key modifer {args.KeyModifer}");
            };

            WhileTrue();
        }

        private void WhileTrue()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (IsClosed)
                    {
                        break;
                    }

                    Thread.Sleep(100);

                    if (CheckPosition())
                    {
                        var a = !IsBot || !IsFast || ((!per989.IsSuccess() && !per990.IsSuccess() && (FastAnaliz.IsSuccess() || FastAnaliz2.IsSuccess())) || BadConnection.IsSuccess());

                        if ( a)
                        {
                            CheckState();
                            if (!(CurrentStage == PrevStage && PrevStage == Stage.Analiz))
                            {
                                PlayAction();
                            }

                            if (CurrentStage != PrevStage)
                            {
                                timeoutStart = DateTime.UtcNow;
                            }

                            if (IsBot && !IsPlayed && (DateTime.UtcNow - timeoutStart).TotalSeconds > 90)
                            {
                                mouse.MouseClick(MouseButton.Left, 459, 379);
                                Thread.Sleep(100);
                                mouse.MouseClick(MouseButton.Left, 644, 467);
                                ClickNext();
                            }
                        }
                        else
                        {
                            CurrentStage = Stage.Uncknow;
                            //mouse.MouseClick(MouseButton.Left, 459, 379);
                            
                            //mouse.MouseClick(MouseButton.Left, 644, 467);
                            ClickNext(true);
                        }
                    }
                }
            }).ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    WhileTrue();
                }
            });
        }

        private static int prevIndexWindow = 0;

        private void SynkDb()
        {
            using (var db = new LiteDatabase(Path.Combine(_screnShotFolder, DbName)))
            {
                var knowSlides = db.GetCollection<KnowSlide>("KnowSlide");
                knowSlides.EnsureIndex(x => x.Hash);

                foreach (
                    var dbDName in
                    new DirectoryInfo(_screnShotFolder).GetFiles()
                        .Where(info => info.Name.Contains(".db"))
                        .Select(info => info.Name))
                {
                    if (!DbName.Equals(dbDName))
                    {
                        using (var importeDb = new LiteDatabase(Path.Combine(_screnShotFolder, dbDName)))
                        {
                            var importedKnowSlides = importeDb.GetCollection<KnowSlide>("KnowSlide");
                            //importedKnowSlides.EnsureIndex(x => x.Hash);

                            foreach (var importedSlide in importedKnowSlides.FindAll())
                            {
                                var existSlide = knowSlides.FindOne(slide => slide.Hash == importedSlide.Hash);

                                if (existSlide == null)
                                {
                                    importedSlide.Id = 0;
                                    knowSlides.Insert(importedSlide);
                                }
                                else
                                {
                                    if (importedSlide.IsSuccess && !existSlide.IsSuccess)
                                    {
                                        existSlide.IsSuccess = importedSlide.IsSuccess;
                                        existSlide.ClickEventArgses = importedSlide.ClickEventArgses;
                                        knowSlides.Update(existSlide);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private DateTime timeoutStart;

        private void SynkDbOnClick(object sender, RoutedEventArgs e)
        {
            SynkDb();
        }

        private void MainWindow_OnDeactivated(object sender, EventArgs e)
        {
            //Window window = (Window)sender;
            //window.Topmost = true;
        }
    }
}
