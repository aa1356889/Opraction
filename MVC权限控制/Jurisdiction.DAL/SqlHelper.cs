using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jurisdiction.DAL
{
  public  class SqlHelper
    {
      public static void LoadSql(string ids, ref List<System.Data.SqlClient.SqlParameter> parameters, ref string str)
      {
          if (string.IsNullOrEmpty(ids))
          {
              return;
          }
          string[] arrids = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
          StringBuilder parStrs=new StringBuilder(20);
          for (int i = 0; i < arrids.Length; i++)
          {
              parStrs.Append("@p" + i);
              parStrs.Append(",");
              parameters.Add(new SqlParameter("@p" + i, System.Data.SqlDbType.VarChar) { Value = arrids[i] });
          }
          if (parStrs.Length > 0)
          {
              parStrs.Remove(parStrs.Length - 1, 1);
          }
          str = parStrs.ToString();
      }
    }
}
