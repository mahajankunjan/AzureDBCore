using System.Data.SqlClient;

namespace AzureDBCore
{
    public class ProductService
    {
        private static string db_source = "productdbserver.database.windows.net";
        private static string db_user = "kunjanmahajan";
        private static string db_password = "mahajan__186";
        private static string db_database = "ProductDB";

        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID = db_user;
            builder.Password = db_password;
            builder.InitialCatalog = db_database;


            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> products = new List<Product>();

            string statement = "SELECT * FROM PRODUCTS";

            conn.Open();

            SqlCommand cmd = new SqlCommand(statement, conn);

            using(SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        MRP = reader.GetDecimal(2)
                    };
                    products.Add(product);
                }    
            }
            conn.Close();

            return products;            
        }
    }
}
