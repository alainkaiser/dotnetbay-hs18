using DotNetBay.Core.Execution;
using DotNetBay.Data.Provider.FileStorage;
using DotNetBay.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Global verfügbare Properties
        public readonly IMainRepository MainRepository;
        public readonly IAuctionRunner AuctionRunner;

        public App()
        {
            // Instanziierung der global verfügbaren Properties
            this.MainRepository = new FileSystemMainRepository("appdata.json");
            this.MainRepository.SaveChanges();

            this.FillDemoData();

            this.AuctionRunner = new AuctionRunner(this.MainRepository);
            // Starten vom AuctionRunner
            this.AuctionRunner.Start();
        }

        private void FillDemoData()
        {
            var memberService = new SimpleMemberService(this.MainRepository);
            var service = new AuctionService(this.MainRepository, memberService);

            if (!service.GetAll().Any())
            {
                var me = memberService.GetCurrentMember();

                service.Save(new Auction
                {
                    Title = "My First Auction",
                    StartDateTimeUtc = DateTime.UtcNow.AddSeconds(10),
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(14),
                    StartPrice = 72,
                    Seller = me
                });
            }
        }
    }
}
