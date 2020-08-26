using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex3
{
    class Program
    {

        static void Main(string[] args)
        {
            var cs = ConnectionSingleton.getInstance();
            var connection = cs.GetConnection();
            DBmanager db = new DBmanager();
            Menu(db,connection);
        }
            
        public static void Menu(DBmanager dbm,SqlConnection connection)
        {
            Console.WriteLine("Press C-to change data or E-to execute querys.");
            string output = Console.ReadLine();
            switch (output)
            {
                case "C":
                    Console.Clear();
                    dbm.ChangeData();
                    Console.WriteLine("Press any key to return to the main menu.");
                    Console.ReadKey();
                    Console.Clear();
                    Menu(dbm,connection);

                    break;
                case "E":
                    Console.Clear();
                    ExecuteQuerys(connection);
                    Console.WriteLine("Press any key to return to the main menu.");
                    Console.ReadKey();
                    Console.Clear();
                    Menu(dbm,connection);
                    break;
                default:
                    Console.WriteLine("No such option.Press any key to try again.");
                    Console.ReadKey();
                    Console.Clear();
                    Menu(dbm,connection);
                    break;
            }

        }

        public static void ExecuteQuerys(SqlConnection connection)
        {
            using (var conn = new SqlConnection(connection.ConnectionString))
            {
                conn.Open();
                
                Console.WriteLine("1.Знайти перелік ПІБ квартиронаймачів (через кому) заданої квартири за заданою адресов. : ");
                Console.WriteLine("Wrtite name of street :");
                string street = Console.ReadLine();
                Console.WriteLine("Wrtite number of house:");
                string house = Console.ReadLine();
                Console.WriteLine("Wrtite number of the flat :");
                string flat = Console.ReadLine();
                string q1 = String.Format("SELECT Citzens.FirstName,Citzens.SecondName FROM Citzens JOIN Flats ON Citzens.Flat_FlatId = Flats.FlatId JOIN Houses ON "+
                            "Flats.House_HouseId = Houses.HouseId WHERE Houses.Street = {0} and Houses.Number = {1} and Flats.Number = {2}",street,house,flat);
                var c1 = new SqlCommand(q1, conn);
                var out1 = c1.ExecuteReader();
                while (out1.Read())
                {
                    Console.Write(("{0} {1},", out1[0], out1[1]));
                }
                Console.WriteLine();
                out1.Close();
                
                Console.WriteLine("2.Вивести вулицю, на якій винаймають найбільшу кількість кварти. : ");
                string q2 = "SELECT TOP 1 COUNT(Contracts.ContractId),Houses.Street AS AMOUNT FROM Contracts JOIN Flats ON Contracts.Flat_FlatId = Flats.FlatId JOIN Houses ON Flats.House_HouseId = Houses.HouseId "+
                            "GROUP BY Houses.Street ORDER BY AMOUNT DESC";
                var c2 = new SqlCommand(q2, conn);
                var out2 = c2.ExecuteReader();
                while (out2.Read())
                {
                    Console.WriteLine("Street :{0}",out2[1]);
                }
                out2.Close();
                
                Console.WriteLine("3.Вивести будинки, в яких живуть сім’ї з найбільшою кількістю дітей.: ");
                string q3 = "SELECT COUNT(Houses.HouseId) ,Houses.HouseId FROM Citzens JOIN Flats ON Citzens.Flat_FlatId = Flats.FlatId JOIN Houses ON Flats.House_HouseId = Houses.HouseId "+
                            "WHERE Citzens.IsAdult = 0 "+
                            "GROUP BY Houses.HouseId";
                var c3 = new SqlCommand(q3, conn);
                var out3 = c3.ExecuteReader();
                while (out3.Read())
                {
                    Console.WriteLine("HouseId:{0};Amount of childrens: {1}", out3[1], out3[0]);
                }
                out3.Close();
            
                Console.WriteLine("5.Вивести будинки, в яких знаходяться квартири з найбільшою площею та найменшою кількістю мешканців : ");
                string q5 = "SELECT  Flats.House_HouseId FROM (SELECT COUNT(CitzenId) as AmountOfCitzens,Citzens.Flat_FlatId FROM Houses JOIN Flats ON Houses.HouseId = Flats.House_HouseId JOIN Citzens ON Citzens.Flat_FlatId = Flats.FlatId " +

                            "GROUP BY Citzens.Flat_FlatId " +
                            ") as res1 " +

                            "JOIN Flats ON res1.Flat_FlatId = Flats.FlatId " +

                            "ORDER BY Flats.[Square],res1.AmountOfCitzens DESC";

                var c5 = new SqlCommand(q5, conn);
                var out5 = c5.ExecuteReader();
                while (out5.Read())
                {
                    Console.WriteLine("HouseId:{0};", out5[0]);
                }
                out5.Close();

                conn.Close();
            }
        }
    }
}
