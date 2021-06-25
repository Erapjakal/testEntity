using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Test.Commands;
using Test.ViewModels.Base;

namespace Test.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Title property
        private string _title = "Главное окно";

        public string Title { get => _title; set => Set(ref _title, value); }
        #endregion
        #region Products property
        private List<Продукты> _продукты;
        public List<Продукты> Продукты { get => _продукты; set => Set(ref _продукты, value); }
        #endregion
        #region PriceCount property
        private string _PriceCount;
        public string PriceCount { get => _PriceCount; set => Set(ref _PriceCount, value); }
        private double GetFullPrice()
        {
            var fullPrice = 0.0;
            foreach (var item in Продукты)
            {
                fullPrice += item.Price * item.Weight;
            }
            return fullPrice;
        }

        #endregion     
        #region ProgressBarCounter property
        private int _ProgressBarCounter = 0;
        public int ProgressBarCounter { get => _ProgressBarCounter; set => Set(ref _ProgressBarCounter, value); }

        #endregion 
        #region Commands
        #region CloseCurrentWindow Command
        private ICommand _CloseCurrentWindow;
        public ICommand CloseCurrentWindow => _CloseCurrentWindow ??= new LambdaCommand(OnCloseCurrentWindowExecuted, CanCloseCurrentWindowExecute);
        private bool CanCloseCurrentWindowExecute(object p) => true;
        private void OnCloseCurrentWindowExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion
        #region GetProducts Command
        private ICommand _GetProducts;
        public ICommand GetProducts => _GetProducts ??= new LambdaCommand(OnGetProductsExecuted, CanGetProductsExecute);
        private bool CanGetProductsExecute(object p) => true;
        private void OnGetProductsExecuted(object p)
        {

        }
        #endregion
        #region SaveDataBase Command
        private ICommand _SaveDataBase;
        public ICommand SaveDataBase => _SaveDataBase ??= new LambdaCommand(OnSaveDataBaseExecuted, CanSaveDataBaseExecute);
        private bool CanSaveDataBaseExecute(object p) => true;
        private void OnSaveDataBaseExecuted(object p)
        {
            using (helloappdbContext db = new helloappdbContext())
            {
                foreach (var item in db.Продуктыs)
                {
                    ProgressBarCounter += 1;
                    if (db.Продуктыs != null)
                        db.Продуктыs.Remove(item);
                }
                foreach (var item in Продукты)
                {
                    ProgressBarCounter += 1;
                    db.Продуктыs.Add(item);
                }
                db.SaveChanges();
                ProgressBarCounter = 100;
            }
            ProgressBarCounter = 0;
            var fullprice = GetFullPrice();
            PriceCount = $"Товара на сумму {fullprice} грн.";
        }
        #endregion
        #endregion
        #region Constructors
        public MainWindowViewModel()
        {
            _продукты = new List<Продукты>();
            using (helloappdbContext db = new helloappdbContext())
            {
                var users = db.Продуктыs.ToList();
                foreach (var item in users)
                {
                    Продукты.Add(item);
                }
            }
            var fullprice = GetFullPrice();
            PriceCount = $"Товара на сумму {fullprice} грн.";
        }
        #endregion
    }
}
