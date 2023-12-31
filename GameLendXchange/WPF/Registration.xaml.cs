﻿using GameLendXchange.Classes;
using System;
using System.Collections.Generic;
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

namespace GameLendXchange.WPF
{
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Password) || string.IsNullOrWhiteSpace(pseudoTextBox.Text) || !dateOfBirthSelect.SelectedDate.HasValue)
            {
                errorMessage.Text = "Merci de remplir tous les champs";
                return;
            }
            else
            {
                Player p = new Player();
                p.Username = usernameTextBox.Text;
                p.Password = passwordTextBox.Password;
                p.DateOfBirth = dateOfBirthSelect.SelectedDate.Value;
                p.Pseudo = pseudoTextBox.Text;
                p.RegistrationDate = DateTime.Now;

                bool success = p.Insert();

                if (success)
                {
                    NavigationService.Navigate(new Login());

                    ClearFields();

                    errorMessage.Text = string.Empty;
                }
                else
                {
                    errorMessage.Text = "Erreur lors de la création du joueur. Veuillez vérifier vos informations.";
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void ClearFields()
        {
            usernameTextBox.Text = string.Empty;
            passwordTextBox.Password = string.Empty;
            dateOfBirthSelect.SelectedDate = null;
            pseudoTextBox.Text = string.Empty;
        }

    }
}
