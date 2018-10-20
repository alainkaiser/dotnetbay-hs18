using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using DotNetBay.Data.Entity;
using Xceed.Wpf.Toolkit;
using DotNetBay.Core;
using System.IO;
using System.Runtime.CompilerServices;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window, INotifyPropertyChanged
    {
        private readonly AuctionService auctionService;
        private readonly Auction newAuction;

        public event PropertyChangedEventHandler PropertyChanged;

        public Auction NewAuction
        {
            get
            {
                return newAuction;
            }
        }

        public string FilePath { get; set; }

        public SellView()
        {
            InitializeComponent();

            // Set Data-Context to this Window
            this.DataContext = this;

            // Set initial Data for Auction
            var app = Application.Current as App;
            if (app != null)
            {
                var simpleMemberService = new SimpleMemberService(app.MainRepository);
                this.auctionService = new AuctionService(app.MainRepository, simpleMemberService);
                this.newAuction = new Auction
                {
                    Seller = simpleMemberService.GetCurrentMember(),
                    StartDateTimeUtc = DateTime.UtcNow,
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(7)
                };
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true && File.Exists(openFileDialog.FileName))
            {
                var fileInfo = new FileInfo(openFileDialog.FileName);
                // allow only for jpg files
                if (fileInfo.Extension.EndsWith("jpg"))
                {
                    this.FilePath = openFileDialog.FileName;
                    this.newAuction.Image = File.ReadAllBytes(this.FilePath);
                    if (this.FilePath != null)
                    {
                        this.OnPropertyChanged(nameof(this.FilePath));
                    }
                }
            }
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NewAuctionBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.auctionService.Save(this.newAuction);
            this.Close();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
