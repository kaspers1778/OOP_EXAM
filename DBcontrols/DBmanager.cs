using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3
{
    class DBmanager
    {
        public DbSet table;
        public string conS = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public AppDBContext context;
        public DBmanager()
        {
            context = new AppDBContext();
        }
        public string SetTable()
        {
            Console.WriteLine("With which table you want to work?\n" +
                             "\t\nHc-HouseComplexs" +
                             "\t\nH-Houses" +
                             "\t\nF-Flats" +
                             "\t\nE-Entrances" +
                             "\t\nCo-Contracts" +
                             "\t\nCi-Citzens");
            string input = Console.ReadLine();
            DataSet ds = this.GetDataSet();
            switch (input)
            {
                case "Hc":
                    foreach (DataRow row in ds.Tables["HouseComplexs"].Rows)
                    {
                        Console.WriteLine("HouseComplexId:{0}; Name: {1}", row["HouseComplexId"], row["Name"]);
                    }
                    return "HouseComplex";
                case "H":
                    foreach (DataRow row in ds.Tables["Houses"].Rows)
                    {
                        Console.WriteLine("HouseId:{0}; Number: {1},Street: {2},Floors: {3},HouseComplex_HouseComplexId: {4}",
                            row["HouseId"], row["Number"], row["Street"], row["Floors"], row["HouseComplex_HouseComplexId"]);
                    }
                    return "House";
                case "F":
                    foreach (DataRow row in ds.Tables["Flats"].Rows)
                    {
                        Console.WriteLine("FlatId:{0}; Number: {1},Rooms: {2},Square: {3},UsefulSquare: {4},Entrance_EntranceId: {5},House_HouseId: {6}",
                            row["FlatId"], row["Number"], row["Rooms"], row["Square"], row["UsefulSquare"], row["Entrance_EntranceId"], row["House_HouseId"]);
                    }
                    return "Flat";
                case "E":
                    foreach (DataRow row in ds.Tables["Entrances"].Rows)
                    {
                        Console.WriteLine("EntranceId: {0}; Number: {1},House_HouseId: {2}",
                            row["EntranceId"], row["Number"], row["House_HouseId"]);
                    }
                    return "Flat";
                case "Co":
                    foreach (DataRow row in ds.Tables["Contracts"].Rows)
                    {
                        Console.WriteLine("ContractId:{0};Date: {1},isDebt: {2},Debt: {3},Flat_FlatId: {4},Rentor_CitzenId: {5}",
                            row["ContractId"], row["Date"], row["isDebt"], row["Debt"], row["Flat_FlatId"], row["Rentor_CitzenId"]);
                    }
                    return "Flat";
                case "Ci":
                    foreach (DataRow row in ds.Tables["Flats"].Rows)
                    {
                        Console.WriteLine("CitzenId:{0}; FirstName: {1},SecondName: {2},IsAdult: {3},Flat_FlatId: {4}",
                            row["CitzenId"], row["FirstName"], row["SecondName"], row["IsAdult"], row["Flat_FlatId"]);
                    }
                    return "Flat";
                default:
                    Console.WriteLine("No such option.Press any key to try again.");
                    Console.ReadKey();
                    Console.Clear();
                    return this.SetTable();
            }
        }

        public void ChangeData()
        {
            Console.WriteLine("What operation you want to execute?\n" +
                              "\t\nU-Update row" +
                              "\t\nA-Add row" +
                              "\t\nD-Delete row" +
                              "\t\nR-Return to the main menu");
            string input = Console.ReadLine();
            switch (input)
            {
                case "U":
                    Console.Clear();
                     UpdateRow();
                    break;
                case "A":
                    Console.Clear();
                     InsertRow();
                    break;
                case "D":
                    Console.Clear();
                      DeleteRow();
                    break;
                case "R":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("No such option.Press any key to try again.");
                    Console.ReadKey();
                    Console.Clear();
                    ChangeData();
                    break;
            }
  
        }
        //Method to let user update row in the DB
        public void UpdateRow()
        {

            string table = SetTable();
            Console.WriteLine("Which row you want to update?");
            string rowId = Console.ReadLine();
            Console.WriteLine("Which colummn you want to update?");
            string column = Console.ReadLine();
            Console.WriteLine("Which is new data?");
            string data = Console.ReadLine();

            try
            {
                using (var connection = new SqlConnection(conS))
                {
                    connection.Open();
                    string updating = String.Format("UPDATE {0}s SET {1} = {2} WHERE {0}Id ={3}", table, column, data, rowId);
                    var selectEstates = new SqlCommand(updating, connection);
                    selectEstates.ExecuteNonQuery();
                    connection.Close();
                }
                Console.WriteLine("Data correctly changed.Press any key to return to the main menu");
                Console.ReadKey();
                Console.Clear();

            }
            catch
            {
                Console.WriteLine("Invalid data.Press any key to try again.");
                Console.ReadKey();
                Console.Clear();
                ChangeData();
            }

        }
        //Method to let user insert new row into table in DB
        public void InsertRow()
        {

            string table = SetTable();
            Console.WriteLine("Write the new data separetede by comma,but without Id");
            string newData = Console.ReadLine();

            try
            {
                using (var connection = new SqlConnection(conS))
                {
                    connection.Open();
                    string inserting = String.Format("INSERT INTO {0}s VALUES({1})", table, newData);
                    var selectEstates = new SqlCommand(inserting, connection);
                    selectEstates.ExecuteNonQuery();
                    connection.Close();
                }
                Console.WriteLine("New row inserted.Press any key to return to the main menu");
                Console.ReadKey();
                Console.Clear();

            }
            catch
            {
                Console.WriteLine("Invalid data.Press any key to try again.");
                Console.ReadKey();
                Console.Clear();
                ChangeData();
            }

        }
        public void DeleteRow()
        {

            string table = SetTable();
            Console.WriteLine("Which row you want to delete?");
            string rowId = Console.ReadLine();

            try
            {
                using (var connection = new SqlConnection(conS))
                {
                    connection.Open();
                    string deleting = String.Format("DELETE FROM {0}s WHERE {0}Id = {1}", table, rowId);
                    var selectEstates = new SqlCommand(deleting, connection);
                    selectEstates.ExecuteNonQuery();
                    connection.Close();
                }
                Console.WriteLine("Row has been deleted.Press any key to return to the main menu");
                Console.ReadKey();
                Console.Clear();
            }
            catch
            {
                Console.WriteLine("Invalid data.Press any key to try again.");
                Console.ReadKey();
                Console.Clear();
                ChangeData();
            }

        }
        private DataSet GetDataSet()
        {
            SqlDataAdapter daHc = new SqlDataAdapter("SELECT * FROM HouseComplexs", conS);
            SqlDataAdapter daH = new SqlDataAdapter("SELECT * FROM Houses", conS);
            SqlDataAdapter daF = new SqlDataAdapter("SELECT * FROM Flats", conS);
            SqlDataAdapter daCon = new SqlDataAdapter("SELECT * FROM Contracts", conS);
            SqlDataAdapter daC = new SqlDataAdapter("SELECT * FROM Citzens", conS);
            SqlDataAdapter daEn = new SqlDataAdapter("SELECT * FROM Entrances", conS);

            DataSet ds = new DataSet();
            daHc.Fill(ds, "HouseComplexs");
            daH.Fill(ds, "Houses");
            daF.Fill(ds, "Flats");
            daCon.Fill(ds, "Contracts");
            daC.Fill(ds, "Citzens");
            daEn.Fill(ds, "Entrances");

            return ds;

        }
    }
}
