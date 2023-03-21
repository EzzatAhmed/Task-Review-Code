
//for Single Responsibility Principle make class for actions database 
public static class DatBaseAccess
{
    // Make Function that return connection Don't Repeat YourSelf & WHEN you nedd to change connection it will change in one method not all methods 

    public static SqlConnection GetSQLConnection()
    {
        SqlConnection conn= new SqlConnection("Data Source=server;Initial Catalog =  db;User ID=user;Password=password");
        retun conn;
    }
        // make function for Insert Generic
    public  static bool ExecuteSQLNONQuery(string query, SqlConnection conn)
    {
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        conn.Close();

        retun true;
    }
        // make function for SELECT Generic
    public static SqlDataReader ExecuteSelectSQLQuery(string query,SqlConnection conn)
    {
            conn.Open();
            SqlCommand cmd = new SqlCommand(query,GetSQLConnection());
            SqlDataReader reader=cmd.ExecuteReader();
            retun reader;
    }
}

public class User 
{
    public string Name{get; set;}
    public int age{get; set;}


    public bool SaveToDatabase(User user)
    {
        try
        {
            DatBaseAccess.GetSQLConnection().Open();
            string query= $"INSERT INTO Users (Name,Age) VALUES ('{user.Name}','{user.Age}')";
            retun DatBaseAccess.ExecuteSQLNONQuery(query,DatBaseAccess.GetSQLConnection());
        }
        catch(Exception e)
        {
            Console.WriteLine("Error saving user to database : " + ex.Message);
            retun false;
        }
    }


    public void GetUserList()
    {
        // add try & Catch Here
        try
        {
            string query = $"SELECT * FROM Users";
            SqlDataReader reader =  DatBaseAccess.ExecuteSelectSQLQuery(query,DatBaseAccess.GetSQLConnection())
            while(reader.Read())
            {
                Console.WriteLine(reader["Name"] + ", " + reader["Age"]);

            } 
            reader.Close();
            DatBaseAccess.GetSQLConnection().Close();  
        }
        catch(Exception e)
        {
            Console.WriteLine("Error :" + e);
        } 
    }  
    public void DoSomething()
    {
        Console.WriteLine("Doing Something....");
    }
}
