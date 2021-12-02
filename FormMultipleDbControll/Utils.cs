using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace FormMultipleDbControll
{
    internal class Utils
    {
        private static readonly string SERVER_NAME = "DESKTOP-5J4NPKL";
        private static readonly string DATABASE_NAME = "Person";
        private static readonly string ConnectionString = $"server={SERVER_NAME}; database={DATABASE_NAME} ;integrated security=true;";
        public static SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        public static SqlDataAdapter sqlDataAdapter  = new SqlDataAdapter();
        private static Boolean isConnected = false;
        private static Boolean isSetup = false; 
        public  List<String> idList = new List<String>();
        public static DataSet dataSet ; 
        public static DataTable dataTable ;

        public  static void createConnection() {
            if (isConnected) return;
            
            isConnected = true;
        }


        private static void Setup()
        {
            if (isSetup) return;
            sqlDataAdapter.SelectCommand = new SqlCommand("select * from persons", sqlConnection);
            sqlDataAdapter.UpdateCommand = new SqlCommand("update  persons set person_name=@nom,person_address=@add  where id=@id ;", sqlConnection);
            sqlDataAdapter.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 255, "id");
            sqlDataAdapter.UpdateCommand.Parameters.Add("@nom", SqlDbType.VarChar, 255, "person_name");
            sqlDataAdapter.UpdateCommand.Parameters.Add("@add", SqlDbType.VarChar, 255, "person_address");
            sqlDataAdapter.DeleteCommand = new SqlCommand("delete from persons where id =@id; ", sqlConnection);
            sqlDataAdapter.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 255, "id");
            sqlDataAdapter.InsertCommand = new SqlCommand("INSERT INTO persons(id, person_name, person_address) values(@id,@nom,@add)", sqlConnection);
            sqlDataAdapter.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 255, "id");
            sqlDataAdapter.InsertCommand.Parameters.Add("@nom", SqlDbType.VarChar, 255, "person_name");
            sqlDataAdapter.InsertCommand.Parameters.Add("@add", SqlDbType.VarChar, 255, "person_address");
            isSetup = true; 
        }
        public static void openConnection()
        {
            Setup();
            if (isConnected) return;
            isConnected = true; 
            sqlConnection.Open();  
        }
      public static void closeConnection()
        {
            sqlConnection.Close(); 
            isConnected = false;
        }
        public static Boolean updateCommand(int id ,  string nom , string address) {
          
            int len = dataTable.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                if (Int32.Parse(dataTable.Rows[i][0].ToString()) == id)
                {
                    Utils.openConnection();
                    dataTable.Rows[i][1] = nom;
                    dataTable.Rows[i][2] =address;
                    sqlDataAdapter.Update(dataTable);
                    Utils.closeConnection();
                    return true;
                }
            }
            return false;
        }
        public static Boolean insertCommand(int id, string nom, string address)
        {


            foreach (DataRow l in Utils.dataTable.Rows)
            {
                if (Int16.Parse(l[0].ToString()) == id)
                {

                    return false;
                }
            }
            DataRow row = Utils.dataTable.NewRow();
            row["id"] = id;
            row["person_name"] = nom;
            row["person_address"] = address;
            Utils.openConnection();
            Utils.dataTable.Rows.Add(row);
            sqlDataAdapter.Update(Utils.dataTable);
            Utils.closeConnection(); 
            return true;
        }
        public static void  deleteItem(int index)
        {
            Utils.openConnection();
            sqlDataAdapter.DeleteCommand =
                new SqlCommand("delete from persons where id =@id", sqlConnection);
            sqlDataAdapter.DeleteCommand.Parameters.Add("@id", SqlDbType.Int).Value = index ; 
            sqlDataAdapter.DeleteCommand.ExecuteNonQuery();
            Utils.closeConnection(); 
        }
        public static Boolean deleteCommand (int id )
        {
      
            int len = dataTable.Rows.Count; 
             for (int i = 0; i < len; i++)
            {
                if ( Int32.Parse(dataTable.Rows[i][0].ToString()) == id  )
                {
                  
                    int temp = Int32.Parse(dataTable.Rows[i][0].ToString());
                    dataTable.Rows.RemoveAt(i);
                    deleteItem(temp);
                    //sqlDataAdapter.Update(dataTable);  ma5dmaatch 
                    return true; 
                }
            }
            return false; 
        }
        public static void loadData()
        {
            Utils.openConnection();
            dataTable =  new DataTable();
            dataSet = new DataSet(); 
            sqlDataAdapter.SelectCommand = new SqlCommand("select * from persons", sqlConnection);
            sqlDataAdapter.Fill(dataSet,"Persons");
            sqlDataAdapter.Fill(dataTable);
            Utils.closeConnection();
        }



    }
}
