using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNotes.MVC.ViewModel
{
    public class OkViewModel : NotifyViewModelBase<string>
    {
        public OkViewModel()
        {
            Title = "Islem Basarili";
        }
    }
}