using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.EndPoints
{
    public interface ICurrenciesEnpoint
    {
        List<CurrencyModel> GetAll();
        void Save(CurrencyModel cm);
    }
}