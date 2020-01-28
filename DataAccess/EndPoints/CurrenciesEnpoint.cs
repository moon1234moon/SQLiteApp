using DataAccess.Connections;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EndPoints
{
    public class CurrenciesEnpoint : ICurrenciesEnpoint
    {
        private readonly string _table = "Currencies";

        public void Save(CurrencyModel cm)
        {
            SqlLiteDataAccess data_access = new SqlLiteDataAccess();
            data_access.SaveData(_table, ToListOfKeyValuePairs(cm));
        }

        public List<CurrencyModel> GetAll()
        {
            SqlLiteDataAccess data_access = new SqlLiteDataAccess();
            var result = data_access.GetAll(_table);

            return MapVarToList(result);
        }

        private List<CurrencyModel> MapVarToList(dynamic data)
        {
            List<CurrencyModel> output = new List<CurrencyModel>();

            foreach (var item in data)
            {
                var row = (IDictionary<string, object>)item;

                CurrencyModel cm = new CurrencyModel()
                {
                    Id = System.Convert.ToInt32(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    Symbol = row["Symbol"].ToString()
                };

                if(row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    cm.CreatedDate = System.Convert.ToDateTime(row["CreatedDate"].ToString());
                }

                if (row["UpdatedDate"] != null && row["UpdatedDate"].ToString() != "")
                {
                    cm.CreatedDate = System.Convert.ToDateTime(row["UpdatedDate"].ToString());
                }

                output.Add(cm);
            }

            return output;
        }

        private List<KeyValuePair<string, string[]>> ToListOfKeyValuePairs(CurrencyModel cm)
        {
            List<KeyValuePair<string, string[]>> output = new List<KeyValuePair<string, string[]>>();
            string[] value;

            value = new string[2] { "", cm.Name };
            output.Add(new KeyValuePair<string, string[]>("Name", value));

            value = new string[2] { "", cm.Symbol };
            output.Add(new KeyValuePair<string, string[]>("Symbol", value));

            value = new string[2] { "", cm.CreatedDate.ToString() };
            output.Add(new KeyValuePair<string, string[]>("CreatedDate", value));

            value = new string[2] { "", cm.UpdatedDate.ToString() };
            output.Add(new KeyValuePair<string, string[]>("UpdatedDate", value));

            return output;
        }
    }
}
