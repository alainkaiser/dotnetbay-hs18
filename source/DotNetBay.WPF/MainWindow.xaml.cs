﻿using DotNetBay.Core;
using DotNetBay.Data.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Service Instanz der Auktionen, um die aktuellen Auktionen zu beziehen
        private readonly AuctionService auctionService;

        // Liste der aktuellen Auktionen in einer Observable Collection
        private ObservableCollection<Auction> auctions = new ObservableCollection<Auction>();

        // Public Property Auctions, welche Observable Collection zurückgibt
        public ObservableCollection<Auction> Auctions
        {
            get { return this.auctions; }
        }

        public MainWindow()
        {
            // Aktuelle Instanz der App lokaler Variable zuweisen
            App app = (App)Application.Current;

            InitializeComponent();

            // Auctiuon Daten beziehen
            this.auctionService = new AuctionService(app.MainRepository, new SimpleMemberService(app.MainRepository));
            this.auctions = new ObservableCollection<Auction>(this.auctionService.GetAll());

            // DataContext des Hauptfensters auf sich selbst setzen
            // Somit können direkt Properties des Hauptfensters für Data-Binding verwendet werden
            this.DataContext = this;
        }
    }
}
