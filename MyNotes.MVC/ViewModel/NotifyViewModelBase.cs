using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNotes.MVC.ViewModel
{
    public class NotifyViewModelBase<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public string Header { get; set; } = "Yönlendiriliyorsunuz";
        public string Title { get; set; } = "Geçersiz İşlem";
        public bool IsRedirecting { get; set; } = true;
        public string RedirectingUrl { get; set; } = "/Home/Index";
        public int RedirectingTimeout { get; set; } = 3000;
        //public NotifyViewModelBase()
        //{
        //    Header = "Yönlendiriliyorsunuz"; // ESKİ YÖNTEM
        //    Title = "Geçersiz İşlem";
        //    IsRedirecting = true;
        //    RedirectingUrl = "/Home/Index";
        //    RedirectingTimeout = 5000;
        //    Items = new List<T>();
        //}
    }
}