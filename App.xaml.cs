﻿using Koolitused.Model;
using Koolitused.Views;

namespace Koolitused
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "koolitused.db";
        public static KoolitusRepository database;
        public static KoolitusRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new KoolitusRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new DBKoolitusedPage());
        }
    }
}