using quizapp.Models;
using quizapp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace quizapp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Dashboard, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.StartAQuiz:
                        MenuPages.Add(id, new NavigationPage(new QuizOptions()));
                        break;
                    case (int)MenuItemType.RandomCategory:
                        var settingsRC = new QuizOptions();
                        var vmRC = settingsRC.BindingContext as QuizOptionsViewModel;
                        vmRC.QuizMode = "RC";
                        MenuPages.Add(id, new NavigationPage(settingsRC));
                        break;
                    case (int)MenuItemType.RandomQuestion:
                        var settingsRQ = new QuizOptions();
                        var vmRQ = settingsRQ.BindingContext as QuizOptionsViewModel;
                        vmRQ.QuizMode = "RQ";
                        MenuPages.Add(id, new NavigationPage(settingsRQ));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new About()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}