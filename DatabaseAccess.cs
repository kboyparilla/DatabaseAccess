using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Database
{
    public class DatabaseAccess
    {
        SqlConnection con = new SqlConnection();

        private SqlCommand m_cmd = new SqlCommand();

        public DataTable dataTable = new DataTable();
        public SqlCommand Command
        {
            get { return m_cmd; }
        }

        public DatabaseAccess()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        }

        public void Close()
        {
            if (this.m_cmd != null)
            {
                this.m_cmd.Dispose();
            }
            con.Close();
        }

        public DataTable ExecDataTable(string sp)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (this.m_cmd.Connection == null)
                {
                    this.m_cmd.Connection = con;
                }

                this.m_cmd.CommandText = sp;
                this.m_cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(this.m_cmd);

                da.Fill(dataTable);
                da.Dispose();
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.m_cmd != null)
                    this.m_cmd.Dispose();
                con.Close();
            }
            return dataTable;
        }

        public DataSet ExecDataSet(string sp)
        {
            DataSet dataSet = new DataSet();

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (this.m_cmd.Connection == null)
                {
                    this.m_cmd.Connection = con;
                }

                this.m_cmd.CommandText = sp;
                this.m_cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(this.m_cmd);

                da.Fill(dataSet);
                da.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.m_cmd != null)
                {
                    this.m_cmd.Dispose();
                }
                con.Close();
            }
            return dataSet;
        }

        public SqlDataReader ExecReader(string sp)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (this.m_cmd.Connection == null)
                {
                    this.m_cmd.Connection = con;
                }

                this.m_cmd.CommandText = sp;
                this.m_cmd.CommandType = CommandType.StoredProcedure;
                return this.m_cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.m_cmd != null)
                {
                    this.m_cmd.Dispose();
                }
            }

        }

        public Boolean ExecNonQuery(string sp)
        {
            Boolean isValid = false;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (this.m_cmd.Connection == null)
                {
                    this.m_cmd.Connection = con;
                }

                this.m_cmd.CommandText = sp;
                this.m_cmd.CommandType = CommandType.StoredProcedure;
                int rowInfected = this.m_cmd.ExecuteNonQuery();
                if (rowInfected > 0)
                {
                    isValid = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.m_cmd != null)
                {
                    this.m_cmd.Dispose();
                }
                con.Close();
            }

            return isValid;
        }

        public int ExecuteScalar(string sp)
        {
            int num = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (this.m_cmd.Connection == null)
                {
                    this.m_cmd.Connection = con;
                }

                this.m_cmd.CommandText = sp;
                this.m_cmd.CommandType = CommandType.StoredProcedure;
                num = Convert.ToInt32(this.m_cmd.ExecuteScalar());

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.m_cmd != null)
                {
                    this.m_cmd.Dispose();
                }
                con.Close();
            }
            return num;
        }
    }
}

