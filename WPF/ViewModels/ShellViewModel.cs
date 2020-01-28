using Caliburn.Micro;
using DataAccess.EndPoints;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly ICurrenciesEnpoint _currencyEndpoint;

        public ShellViewModel(ICurrenciesEnpoint currencyEndpoint)
        {
            _currencyEndpoint = currencyEndpoint;
            List<CurrencyModel> list = _currencyEndpoint.GetAll();
            //CurrencyModel cm = new CurrencyModel()
            //{
            //    Name = "Euros",
            //    Symbol = "€",
            //    CreatedDate = DateTime.Now,
            //    UpdatedDate = DateTime.Now
            //};
            //_currencyEndpoint.Save(cm);
        }
    }
}
