using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Input.Inking;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HockeyPlaybook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IceRink : Page
    {
        private TranslateTransform dragPG;
        private TranslateTransform dragSG;
        private TranslateTransform dragSF;
        private TranslateTransform dragPF;
        private TranslateTransform dragC;
        private TranslateTransform OdragPG;
        private TranslateTransform OdragSG;
        private TranslateTransform OdragSF;
        private TranslateTransform OdragPF;
        private TranslateTransform OdragC;
        InkManager _inkKhaled = new Windows.UI.Input.Inking.InkManager();
        private uint _penID;
        private uint _touchID;
        private Point _previousContactPt;
        private Point currentContactPt;
        private double x1;
        private double y1;
        private double x2;
        private double y2; 
        public IceRink()
        {
            this.InitializeComponent();
            DrawPlayers.IsEnabled = false;
            OffensePG.ManipulationDelta += Drag_OffensePG;
            OffenseSG.ManipulationDelta += Drag_OffenseSG;
            OffenseSF.ManipulationDelta += Drag_OffenseSF;
            OffensePF.ManipulationDelta += Drag_OffensePF;
            OffenseC.ManipulationDelta += Drag_OffenseC;
            DefensePG.ManipulationDelta += Drag_DefensePG;
            DefenseSG.ManipulationDelta += Drag_DefenseSG;
            DefenseSF.ManipulationDelta += Drag_DefenseSF;
            DefensePF.ManipulationDelta += Drag_DefensePF;
            DefenseC.ManipulationDelta += Drag_DefenseC;
            dragPG = new TranslateTransform();
            dragSG = new TranslateTransform();
            dragSF = new TranslateTransform();
            dragPF = new TranslateTransform();
            dragC = new TranslateTransform();
            OdragC = new TranslateTransform();
            OdragPG = new TranslateTransform();
            OdragSG = new TranslateTransform();
            OdragSF = new TranslateTransform();
            OdragPF = new TranslateTransform();
            OffensePG.RenderTransform = this.OdragPG;
            OffenseSG.RenderTransform = this.OdragSG;
            OffenseSF.RenderTransform = this.OdragSF;
            OffensePF.RenderTransform = this.OdragPF;
            OffenseC.RenderTransform = this.OdragC;
            DefensePG.RenderTransform = this.dragPG;
            DefenseSG.RenderTransform = this.dragSG;
            DefenseSF.RenderTransform = this.dragSF;
            DefensePF.RenderTransform = this.dragPF;
            DefenseC.RenderTransform = this.dragC;
        }
        private void backn_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HubPage));
        }

        private void Drag_DefensePG(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            dragPG.X += e.Delta.Translation.X;
            dragPG.Y += e.Delta.Translation.Y;
        }
        private void Drag_DefenseSG(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            dragSG.X += e.Delta.Translation.X;
            dragSG.Y += e.Delta.Translation.Y;
        }
        private void Drag_DefenseSF(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            dragSF.X += e.Delta.Translation.X;
            dragSF.Y += e.Delta.Translation.Y;
        }
        private void Drag_DefensePF(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            dragPF.X += e.Delta.Translation.X;
            dragPF.Y += e.Delta.Translation.Y;
        }
        private void Drag_DefenseC(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            dragC.X += e.Delta.Translation.X;
            dragC.Y += e.Delta.Translation.Y;
        }
        private void Drag_OffensePG(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            OdragPG.X += e.Delta.Translation.X;
            OdragPG.Y += e.Delta.Translation.Y;
        }
        private void Drag_OffenseSG(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            OdragSG.X += e.Delta.Translation.X;
            OdragSG.Y += e.Delta.Translation.Y;
        }
        private void Drag_OffenseSF(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            OdragSF.X += e.Delta.Translation.X;
            OdragSF.Y += e.Delta.Translation.Y;
        }
        private void Drag_OffensePF(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            OdragPF.X += e.Delta.Translation.X;
            OdragPF.Y += e.Delta.Translation.Y;
        }
        private void Drag_OffenseC(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            OdragC.X += e.Delta.Translation.X;
            OdragC.Y += e.Delta.Translation.Y;
        }
        private void MyCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerId == _penID)
            {
                Windows.UI.Input.PointerPoint pt = e.GetCurrentPoint(drawCanvas1);

                // Pass the pointer information to the InkManager.  
                _inkKhaled.ProcessPointerUp(pt);
            }

            else if (e.Pointer.PointerId == _touchID)
            {
                // Process touch input 
            }

            _touchID = 0;
            _penID = 0;

            // Call an application-defined function to render the ink strokes. 


            e.Handled = true;
        }

        private void drawCanvas1_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerId == _penID)
            {
                PointerPoint pt = e.GetCurrentPoint(drawCanvas1);

                // Render a red line on the canvas as the pointer moves.  
                // Distance() is an application-defined function that tests 
                // whether the pointer has moved far enough to justify  
                // drawing a new line. 
                currentContactPt = pt.Position;
                x1 = _previousContactPt.X;
                y1 = _previousContactPt.Y;
                x2 = currentContactPt.X;
                y2 = currentContactPt.Y;

                if (Distance(x1, y1, x2, y2) > 2.0) // We need to developp this method now  
                {
                    Line line = new Line()
                    {
                        X1 = x1,
                        Y1 = y1,
                        X2 = x2,
                        Y2 = y2,
                        StrokeThickness = 4.0,
                        Stroke = new SolidColorBrush(Colors.Black)

                    };

                    _previousContactPt = currentContactPt;

                    // Draw the line on the canvas by adding the Line object as 
                    // a child of the Canvas object. 
                    drawCanvas1.Children.Add(line);

                    // Pass the pointer information to the InkManager. 
                    _inkKhaled.ProcessPointerUpdate(pt);
                }
            }

            else if (e.Pointer.PointerId == _touchID)
            {
                // Process touch input 
            }

        }

        private double Distance(double x1, double y1, double x2, double y2)
        {
            double d = 0;
            d = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
            return d;
        }

        private void drawCanvas1_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // Get information about the pointer location. 
            PointerPoint pt = e.GetCurrentPoint(drawCanvas1);
            _previousContactPt = pt.Position;

            // Accept input only from a pen or mouse with the left button pressed.  
            PointerDeviceType pointerDevType = e.Pointer.PointerDeviceType;
            if (pointerDevType == PointerDeviceType.Pen ||
                    pointerDevType == PointerDeviceType.Mouse &&
                    pt.Properties.IsLeftButtonPressed)
            {
                // Pass the pointer information to the InkManager. 
                _inkKhaled.ProcessPointerDown(pt);
                _penID = pt.PointerId;

                e.Handled = true;
            }

            else if (pointerDevType == PointerDeviceType.Touch)
            {
                // Process touch input 
            }
        }



        /// <summary> 
        /// Invoked when this page is about to be displayed in a Frame. 
        /// </summary> 
        /// <param name="e">Event data that describes how this page was reached.  The Parameter 
        /// property is typically used to configure the page.</param> 
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {


        }
        private async void ShowMessage(string content, string title)
        {
            MessageDialog msg = new MessageDialog(content, title);
            await msg.ShowAsync();
        }
        private async Task<StorageFile> CreateFile(string filename)
        {
            // Initialization  
            StorageFile file = null;
            try
            {
                // Setting file properties.  
                FileSavePicker save = new FileSavePicker();
                save.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
                save.DefaultFileExtension = ".png";
                save.SuggestedFileName = filename;
                save.FileTypeChoices.Add("PNG", new string[] { ".png" });
                // Creating File  
                file = await save.PickSaveFileAsync();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.ToString(), "Error");
            }
            return file;
        }
        private async Task SaveToPNG(RenderTargetBitmap bitmap, RenderTargetBitmap bitmap2, string filename)
        {
            // Verification.  
            if (bitmap == null)
            {
                if (bitmap2 == null)
                    // Message  
                    this.ShowMessage("try again later", "Error");
                // Info  
                return;
            }

            try
            {
                // Create file.  
                StorageFile file = await this.CreateFile(filename);
                // Verification.  
                if (bitmap == null)
                {
                    // Message  
                    this.ShowMessage(" try again later", "Error");
                    // Info  
                    return;
                }
                // Saving to file.  
                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    // Initialization.  
                    var pixelBuffer = await bitmap.GetPixelsAsync();
                    var logicalDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
                    // convert stream to IRandomAccessStream  
                    var randomAccessStream = stream.AsRandomAccessStream();
                    // encoding to PNG  
                    var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, randomAccessStream);
                    // Finish saving  
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)bitmap.PixelWidth,
                               (uint)bitmap.PixelHeight, logicalDpi, logicalDpi, pixelBuffer.ToArray());
                    // Flush encoder.  
                    await encoder.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.ToString(), "Error");
            }
        }
        private async void SaveCanvas()
        {
            // Initialization.  
            string filename = "Canvas File";
            try
            {
                // Save canvas.  
                RenderTargetBitmap bitmap = await this.CanvasToBMP();
                RenderTargetBitmap bitmap2 = await this.CanvasToBMP();

                await this.SaveToPNG(bitmap, bitmap2, filename);

            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.ToString(), "Error");
            }
        }
        private async Task<RenderTargetBitmap> CanvasToBMP()
        {
            // Initialization  
            RenderTargetBitmap bitmap = null;
            RenderTargetBitmap bitmap2 = null;
            try
            {
                // Initialization.  
                Size canvasSize = this.drawCanvas1.RenderSize;
                Point defaultPoint = this.drawCanvas1.RenderTransformOrigin;
                // Sezing to output image dimension.  
                this.drawCanvas1.Measure(canvasSize);
                this.drawCanvas1.UpdateLayout();
                this.drawCanvas1.Arrange(new Rect(defaultPoint, canvasSize));

                // Convert canvas to bmp.  
                var bmp = new RenderTargetBitmap();
                await bmp.RenderAsync(this.drawCanvas1);

                // Setting.  
                bitmap = bmp as RenderTargetBitmap;

            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.ToString(), "Error");
            }
            return bitmap;
        }

        private void Save_image_Click(object sender, RoutedEventArgs e)
        {
            SaveCanvas();
        }

        private void Clear_Lines_Click(object sender, RoutedEventArgs e)
        {
            DrawPlayers.IsEnabled = true;

            drawCanvas1.Children.Clear();


        }

        private void Draw_lines_Click(object sender, RoutedEventArgs e)
        {

            drawCanvas1.PointerPressed += new PointerEventHandler(drawCanvas1_PointerPressed);
            drawCanvas1.PointerMoved += new PointerEventHandler(drawCanvas1_PointerMoved);
            drawCanvas1.PointerReleased += new PointerEventHandler(MyCanvas_PointerReleased);
            drawCanvas1.PointerExited += new PointerEventHandler(MyCanvas_PointerReleased);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


        }

        private void DrawPlayers_Click(object sender, RoutedEventArgs e)
        {
            drawCanvas1.Children.Add(OffenseC);
            drawCanvas1.Children.Add(OffensePF);
            drawCanvas1.Children.Add(OffenseSF);
            drawCanvas1.Children.Add(OffenseSG);
            drawCanvas1.Children.Add(OffensePG);
            drawCanvas1.Children.Add(DefenseC);
            drawCanvas1.Children.Add(DefensePF);
            drawCanvas1.Children.Add(DefenseSF);
            drawCanvas1.Children.Add(DefenseSG);
            drawCanvas1.Children.Add(DefensePG);
        }


    }
}
