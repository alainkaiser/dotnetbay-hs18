using DotNetBay.Core;
using DotNetBay.Data.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // Service Instanz der Auktionen, um die aktuellen Auktionen zu beziehen
        private readonly AuctionService auctionService;

        // Liste der aktuellen Auktionen in einer Observable Collection
        // Änderungen an dieser Collection werden automatisch dem UI weitergeleitet
        private ObservableCollection<Auction> auctions = new ObservableCollection<Auction>();

        public event PropertyChangedEventHandler PropertyChanged;

        // Public Property Auctions, welche Observable Collection zurückgibt
        public ObservableCollection<Auction> Auctions
        {
            get { return this.auctions; }
            private set
            {
                this.auctions = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            // Initialisierungsarbeiten§
            InitializeComponent();

            // Aktuelle Instanz der App einer lokalen Variable zuweisen
            // App-Instanz wird verwendet, um auf das Main-Repository zuzugreiffen
            // Main-Repository wird in App-Klasse als public Property angeboten
            App app = (App)Application.Current;

            // Auction Daten beziehen
            this.auctionService = new AuctionService(app.MainRepository, new SimpleMemberService(app.MainRepository));
            this.auctions = new ObservableCollection<Auction>(this.auctionService.GetAll());

            // DataContext des Hauptfensters auf sich selbst setzen
            // Somit können direkt Properties des Hauptfensters für Data-Binding verwendet werden
            // Konkret soll z.B. die Liste der Auctions (Observable-Collection) verwendet werden
            this.DataContext = this;
        }

        // Place-Bid Button Click Event Handler
        // Hier soll Place-Bid View geöffnet werden
        private void BtnPlaceBid_OnClick(object sender, RoutedEventArgs e)
        {
            var placeBidView = new BidView();
            // Öffent Fenster in Dialog-Modus, sodass zurest dieses Fenster geschlossen werden muss, bevor auf Main Window zugegriffen werden kann.
            placeBidView.ShowDialog();
        }

        // Add-New-Auction Click Event Handler
        // Hier soll Add-New-Auction View geöffent werden
        private void NewAuctionBtn_Click(object sender, RoutedEventArgs e)
        {
            var addAuctionView = new SellView();
            addAuctionView.ShowDialog();

            // Update View
            var allAuctionsFromService = this.auctionService.GetAll();
            this.Auctions = new ObservableCollection<Auction>(allAuctionsFromService);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
