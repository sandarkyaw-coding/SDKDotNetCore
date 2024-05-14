using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SDKDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query,params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            if(parameters != null && parameters.Length > 0 ) {

                //foreach(var item in parameters){
                //cmd.Parameters.AddWithValue(item.Name, item.Value);
                //}
                //another way to add parameters

                //cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
                var parametersArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(parametersArray);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            if(dt.Rows.Count == 0)
            {
                return null;
            }

            string json = JsonConvert.SerializeObject(dt); // C# => JSON
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!; // JSON => C#
            return lst;
        }

        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters != null && parameters.Length > 0)
            {
                var parametersArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(parametersArray);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            if(dt.Rows.Count == 0)
            {
                return default(T);
            }

            string json = JsonConvert.SerializeObject(dt); // C# => JSON
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!; // JSON => C#
            return lst[0];
        }

        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
            }
            var result =  cmd.ExecuteNonQuery();
            connection.Close();

            return result;
        }
    }

public class AdoDotNetParameter
    {
        public AdoDotNetParameter() { }

        public AdoDotNetParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
