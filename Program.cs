using System;
using System.Data.SqlClient;
namespace DemoDB
{
    class Program
    {

        //Constructor Program() which acts as a SIM CARD (it holds the telephone data)
        Program()
        {

        }
        string getMoMoUser(string tel)
        {
            string connectionString = null;
            string user = null;
            SqlConnection cnn;
            try
            {
       
                string sql = "select * from users where tel='"+tel+"' ";
                connectionString = @"Data Source=DALLINGTON\MSSQLSERVER01;Initial Catalog=MoMo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                cnn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    user = dr["fname"]+""+dr["lname"];
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return user;
        }



        void getAccountDetails(string tel)
        {
            string sql, connectionString;
            SqlConnection conn;
            SqlCommand command;
            Program obj;

            try

            {
                obj = new Program();
                Console.WriteLine("Enter your pin to see your A/C details");
                int pin = Convert.ToInt32(Console.ReadLine());
                bool truth = obj.authenticateMoMoUser(tel, pin);
                if(truth == true)
                {

                    sql = "select * from users where tel='" + tel + "'";
                    connectionString = @"Data Source=DALLINGTON\MSSQLSERVER01;Initial Catalog=MoMo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    conn = new SqlConnection(connectionString);
                    command = new SqlCommand(sql, conn);
                    conn.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Console.WriteLine("*******Your Details********");
                        Console.WriteLine("FirstName: " + dr["fname"]);
                        Console.WriteLine("LastName: " + dr["lname"]);
                        Console.WriteLine("Tel No: " + dr["tel"]);
                        Console.WriteLine("***************************");

                    }
                }
                else
                {
                    obj.doYouWantToContinue(tel);
                }
        
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        double getMoMoBalance(string tel)
        {
            string sql, connectionString;
            SqlConnection conn;
            SqlCommand command;
            Object balance_obj = null;

            try
            {
                sql = "select balance from users where tel='" + tel + "'";
                connectionString = @"Data Source=DALLINGTON\MSSQLSERVER01;Initial Catalog=MoMo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                conn = new SqlConnection(connectionString);
                command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader dr = command.ExecuteReader();
                
                while (dr.Read())
                {
                    balance_obj = dr["balance"];
                }

             
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Convert.ToDouble(balance_obj);
        }

        void checkBalance(string tel)
        {
            Program obj = new Program();
            Console.WriteLine("Enter your pin");
            int pin = Convert.ToInt32(Console.Read());
            bool is_valid = obj.authenticateMoMoUser(tel, pin);
            if (is_valid == true)
            {
                Console.WriteLine("Your current MoMo Balance is " + obj.getMoMoBalance(tel));
                obj.doYouWantToContinue(tel);
            }
            else
            {
                Console.WriteLine("Sorry, we couldn't complete transaction because of incorrect pin");
                obj.doYouWantToContinue(tel);
            }

        }


        void withdrawMoney(string tel, double amount)
        {
            string sql, connectionString;
            SqlConnection conn;
            SqlCommand command;
            SqlDataAdapter adapter;
            double oldBalance, newBalance;
            Program obj;

            try
            {
                obj = new Program();
                oldBalance = obj.getMoMoBalance(tel);
                Console.WriteLine("Enter your pin to confirm transaction");
                int pin = Convert.ToInt32(Console.ReadLine());
                bool is_valid = obj.authenticateMoMoUser(tel, pin);
                if (is_valid == true)
                {
                    newBalance = oldBalance - amount;
                    sql = "update users set balance='" + newBalance + "' where tel='" + tel + "'";
                    connectionString = @"Data Source=DALLINGTON\MSSQLSERVER01;Initial Catalog=MoMo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    conn = new SqlConnection(connectionString);
                    command = new SqlCommand(sql, conn);
                    adapter = new SqlDataAdapter();
                    conn.Open();
                    adapter.UpdateCommand = command;
                    adapter.UpdateCommand.ExecuteNonQuery();
                    Console.WriteLine("Yello,We have successfully deducted " + amount + " from your account in names of "+obj.getMoMoUser(tel)+".Your Current Balanace" +
                          " is " + obj.getMoMoBalance(tel) + "");

                    obj.doYouWantToContinue(tel);
                }
                else
                {
                    Console.WriteLine("Sorry! We couldnot complete transaction because of incorrect pin");
                    obj.doYouWantToContinue(tel);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
      
        }

      
        void depositMoney(string tel, double amount)
        {
            string sql, connectionString;
            SqlConnection conn;
            SqlCommand command;
            SqlDataAdapter adapter;
            double oldBalance, newBalance;
            Program obj;

            try
            {
                obj = new Program();
                oldBalance = obj.getMoMoBalance(tel);
                Console.WriteLine("Enter your pin to confirm transaction");
                int pin = Convert.ToInt32(Console.ReadLine());
                bool is_valid = obj.authenticateMoMoUser(tel, pin);
                if(is_valid == true)
                {

                    newBalance = oldBalance + amount;
                    sql = "update users set balance='" + newBalance + "' where tel='" + tel + "'";
                    connectionString = @"Data Source=DALLINGTON\MSSQLSERVER01;Initial Catalog=MoMo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    conn = new SqlConnection(connectionString);
                    command = new SqlCommand(sql, conn);
                    adapter = new SqlDataAdapter();
                    conn.Open();
                    adapter.UpdateCommand = command;
                    adapter.UpdateCommand.ExecuteNonQuery();
                    Console.WriteLine("Yello,You have successfully deposited " + amount + " to account of "+obj.getMoMoUser(tel)+".Your Current Balanace" +
                          " is " + obj.getMoMoBalance(tel) + "");

                    obj.doYouWantToContinue(tel);
                }
                else
                {
                    Console.WriteLine("Sorry! We couldnot complete transaction because of incorrect pin");
                    obj.doYouWantToContinue(tel);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        void sendMoney(string senderTel)
        {
            Program obj;

            try
            {
                obj = new Program();
                Console.WriteLine("Enter the telephone no you are sending to eg. 0774014727");
                string receiverTel = Convert.ToString(Console.ReadLine());
                if (!(senderTel.Equals(receiverTel)))
                {
                    Console.WriteLine("Enter the amount of money you are sending");
                    double amount = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the pin to confirm transaction");
                    int pin = Convert.ToInt32(Console.ReadLine());
                    bool is_validAccount = obj.authenticateMoMoUser(senderTel, pin);
    
                    obj.depositMoney(receiverTel, amount);
                    obj.withdrawMoney(senderTel, amount);

                }
                else
                {
                    Console.WriteLine("Sorry! You cant send money to yourself,enter a different tel number");
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



        bool authenticateMoMoUser(string tel, int pin)
        {
            string sql, connectionString;
            SqlConnection conn;
            SqlCommand command;
            bool is_validAccount = false;

            try { 
                sql = "select pin from users where tel='"+tel+"'";
                connectionString = @"Data Source=DALLINGTON\MSSQLSERVER01;Initial Catalog=MoMo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                conn = new SqlConnection(connectionString);
                command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader dr = command.ExecuteReader();
                
                while (dr.Read())
                {
                  int userPin =Convert.ToInt32(dr["pin"].ToString());

                    if (pin == userPin)
                    {
                        is_validAccount = true;
                    }
                    else
                    {
                        is_validAccount = false;

                    }

                }
                

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                
            }

            return is_validAccount;
            


        }

        void ConsoleHelp(string tel)
        {
            Console.WriteLine("***************");
            Console.WriteLine("\n1.Send Money\n2.Deposit Money\n3.Withdraw Money\n4.My Account\n5.Check Balance\n6.Exit\n");
            Console.Write("Enter number: ");
            int num = Convert.ToInt32(Console.ReadLine());
            Program obj = new Program();
            switch (num)
            {
                case 1:
                    obj.sendMoney(tel);
                    break;
                case 2:
                    Console.WriteLine("Enter the amount of money to deposit");
                    double amount = Convert.ToDouble(Console.ReadLine());
                    obj.depositMoney(tel,amount);
                    break;
                case 3:
                    Console.WriteLine("Enter the amount of money to withdraw");
                    double amt = Convert.ToDouble(Console.ReadLine());
                    obj.withdrawMoney(tel, amt);
                    break;
                case 4:
                    obj.getAccountDetails(tel);
                    break;
                case 5:
                    obj.checkBalance(tel);
                    break;
                

            }

        }

        void doYouWantToContinue(string tel)
        {
            Console.WriteLine("Do you want to continue (Yes or No): ");
            string value = Console.ReadLine();
            if(value.ToLower().Equals("yes"))
            {
                new Program().ConsoleHelp(tel);
            }

        }

        //entry point of the program
            static void Main(string[] args)
        {



            Program obj = new Program();

            Console.WriteLine("*************Welcome To DallingtonMoMo**************");


            string mobile;
            int pin;
            bool is_valid = false; 
            int count = 0;
            do
            {
                if(count >= 1)
                {
                    Console.WriteLine("Wrong details, please try again!!");
                }
                try
                {
                    Console.WriteLine("\nEnter your phone number");
                    mobile = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Enter your pin");
                    pin = Convert.ToInt16(Console.ReadLine());
                    is_valid = obj.authenticateMoMoUser(mobile, pin);
                    if (is_valid == true)
                    {
                        obj.ConsoleHelp(mobile);
                    }
                    else
                    {

                        count++;
                        if (count == 3)
                        {
                            Console.Clear();
                            Console.Beep();
                            Console.WriteLine("Opps,You have failed 3 times!!");

                        }

                    }
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
               
                
            } 
            while (is_valid == false);
          
         
            Console.ReadLine();

        }



        //Store data in an SQL database
        void storeData()
        {

            try
            {
                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=DALLINGTON\MSSQLSERVER01;Initial Catalog=storedb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                cnn = new SqlConnection(connetionString);

                SqlCommand cmd;
                string sql;
                SqlDataAdapter adapter = new SqlDataAdapter();
                cnn.Open();
                string a = "jm", b = "jimmy", c = "jam23";

                sql = "insert into products (product, price, code) values('" + b + "', 6700,'" + c + "')";
                cmd = new SqlCommand(sql, cnn);
                adapter.InsertCommand = new SqlCommand(sql, cnn);
                adapter.InsertCommand.ExecuteNonQuery();

                cmd.Dispose();
                cnn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        //updating data in an SQL database
        void updateData()
        {
            string connetionString, sql;
            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataAdapter adapter;
            connetionString = @"Data Source=DALLINGTON\MSSQLSERVER01;Initial Catalog=storedb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            try
            {
                string pdt = "Yorghut";
                cnn = new SqlConnection(connetionString);
                sql = "update products set product='" + pdt + "' where product_id=6";
                cmd = new SqlCommand(sql, cnn);
                adapter = new SqlDataAdapter();
                cnn.Open();
                adapter.UpdateCommand = cmd;
                adapter.UpdateCommand.ExecuteNonQuery();
                cmd.Dispose();
                cnn.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        //fetching data from an SQL database
        void fetchData()
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DALLINGTON\MSSQLSERVER01;Initial Catalog=storedb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cnn = new SqlConnection(connetionString);
            try
            {
                string sql = "select * from products";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine("" + dr["product_id"] + " is for " + dr["product"] + "at a price of " + dr["price"] + " ");
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //deleting data from an SQL database
        void deleteData()
        {
            string connetionString, sqlQuery;
            SqlConnection conn;
            connetionString = @"Data Source=DALLINGTON\MSSQLSERVER01;Initial Catalog=storedb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            conn = new SqlConnection(connetionString);
            try
            {
                sqlQuery = "Delete from products where product_id=8";
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                conn.Open();
                adapter.DeleteCommand = command;
                adapter.DeleteCommand.ExecuteNonQuery();
                command.Dispose();
                conn.Close();

            }
            catch (Exception de)
            {
                Console.WriteLine(de.Message);
            }


        }















    }
}
