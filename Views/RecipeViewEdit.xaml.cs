﻿using System;
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

namespace recipe.Views
{
    /// <summary>
    /// Логика взаимодействия для RecipeViewEdit.xaml
    /// </summary>
    public partial class RecipeViewEdit : ContentControl
    {
        public RecipeViewEdit()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            listBox.SelectedIndex = -1;
        }
    }
}