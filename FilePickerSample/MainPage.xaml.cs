using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace FilePickerSample
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// このページがフレームに表示されるときに呼び出されます。
        /// </summary>
        /// <param name="e">このページにどのように到達したかを説明するイベント データ。Parameter 
        /// プロパティは、通常、ページを構成するために使用します。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


        }

        private async void pickButton_Click(object sender, RoutedEventArgs e)
        {
            // using Windows.Storage.Pickers;
            FileOpenPicker openPicker = new FileOpenPicker();

            // 表示モードはリスト形式
            //openPicker.ViewMode = PickerViewMode.List;

            // 表示モードはサムネイル形式
            openPicker.ViewMode = PickerViewMode.Thumbnail;

            // ピクチャーライブラリーが起動時の位置
            // その他候補はPickerLocationIdを参照
            // http://msdn.microsoft.com/en-us/library/windows/apps/windows.storage.pickers.pickerlocationid
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            // jpg, jpeg, pngのファイル形式から選択
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            // ファイルオープンピッカーを起動する
            StorageFile file = await openPicker.PickSingleFileAsync();

            if (null != file)
            {
                // FileNameText,FilePathTextはXASMLで定義されたTextBlockコントロール
                FileNameText.Text = file.Name;
                FilePathText.Text = file.Path;

                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                BitmapImage image = new BitmapImage();
                image.SetSource(stream);

                // FileImageはXAMLで定義されたコントロール
                FileImage.Source = image;
            }
            else
            {
                FileNameText.Text = "選択されませんでした";
                FilePathText.Text = "";

                FileImage.Source = null;
            }
        }
    }
}
