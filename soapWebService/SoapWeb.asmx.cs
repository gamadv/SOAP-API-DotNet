using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace soapWebService
{
    /// <summary>
    /// Summary description for SoapWeb
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SoapWeb : System.Web.Services.WebService
    {

        [WebMethod]
        public DataTable ListarProdutos()
        {
            SqlConnection conection = new SqlConnection();


            conection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            conection.Open();

            SqlCommand writeSql = new SqlCommand();
            writeSql.Connection = conection;


            SqlDataAdapter adap = new SqlDataAdapter();
                writeSql.CommandText = "select * from produtos";

                adap.SelectCommand = writeSql;

                DataTable table = new DataTable();

                table.TableName = "produto";

                adap.Fill(table);

            return table; 
        }

        [WebMethod]
        public void InserirProdutos(String nome, String descricao, String preco, String estoque)
        {
            SqlConnection conection = new SqlConnection();

            conection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            conection.Open();

            SqlCommand writeSql = new SqlCommand();
            writeSql.Connection = conection;

            SqlDataAdapter adap = new SqlDataAdapter();
            writeSql.CommandText = "insert into produtos values (@nome, @descricao, @preco, @estoque)";

            writeSql.Parameters.Add("@nome", SqlDbType.NVarChar, 100).Value = nome;
            writeSql.Parameters.Add("@descricao", SqlDbType.NVarChar, 300).Value = descricao;
            writeSql.Parameters.Add("@preco", SqlDbType.Money, 8).Value = preco;
            writeSql.Parameters.Add("@estoque", SqlDbType.NVarChar, 4).Value = estoque;

            adap.InsertCommand = writeSql;

            adap.InsertCommand.ExecuteNonQuery();


        }



    }
}
