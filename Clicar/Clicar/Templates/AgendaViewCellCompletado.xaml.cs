﻿using Clicar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaViewCellCompletado : ViewCell
    {
        public AgendaViewCellCompletado()
        {
            InitializeComponent();
            if (MainViewModel.GetInstance().Agenda.IsLastItem())
            {
                SeparatorFrame.IsVisible = false;
            }
        }
    }
}