using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.DAL
{
    public class DataAccess
    {

        public DataTable Execute(string cmdText, CommandType cmdType = CommandType.StoredProcedure, List<ParmStruct> parms = null)
        {
            SqlCommand cmd = CreateCommand(cmdText, cmdType, parms);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            return dt;
        }

        public int ExecuteNonQuery(string cmdText, List<ParmStruct> parms = null, CommandType cmdType = CommandType.StoredProcedure)
        {
            SqlCommand cmd = CreateCommand(cmdText, cmdType, parms);
            int retVal;

            using (cmd.Connection)
            {
                cmd.Connection.Open();
                retVal = cmd.ExecuteNonQuery();
                UnloadParms(parms, cmd);
            }

            return retVal;
        }

        public object ExecuteScaler(string sql, CommandType cmdType, List<ParmStruct> parms = null)
        {
            SqlCommand cmd = CreateCommand(sql, cmdType, parms);
            object retVal;

            using (cmd.Connection)
            {
                cmd.Connection.Open();
                retVal = cmd.ExecuteScalar();
            }

            return retVal;
        }

        private SqlCommand CreateCommand(string cmdText, CommandType cmdType, List<ParmStruct> parms)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cryptech"].ConnectionString);

            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = cmdType;

            if (parms != null)
            {
                foreach (ParmStruct p in parms)
                {
                    cmd.Parameters.Add(p.Name, p.DataType, p.Size).Value = p.Value;
                    cmd.Parameters[p.Name].Direction = p.Direction;
                }
            }

            return cmd;
        }

        private static void UnloadParms(List<ParmStruct> parms, SqlCommand cmd)
        {
            if (parms != null)
            {
                for (int i = 0; i <= parms.Count - 1; i++)
                {
                    ParmStruct p = parms[i];
                    p.Value = cmd.Parameters[i].Value;
                    parms[i] = p;
                }
            }
        }

        public bool HasRows(DataTable table)
        {
            DataTableReader reader = new DataTableReader(table);

            if (reader.HasRows)
            {
                return true;
            }
            return false;
        }
    }
}
