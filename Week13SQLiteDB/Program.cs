using System.Data.SQLite;


ReadData(CreateConnection());
InsertCustomer(CreateConnection());
RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; version = 3; New = True; Compress = True;");

try
    {
        connection.Open();
        Console.WriteLine("DB found.");
    }
    catch
    {
        Console.WriteLine("DB not found.");
    }

    return connection; 
}

static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerStringRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"{readerStringRowId}. Full name: {readerStringFirstName} {readerStringLastName}; DoB: {readerStringDoB} ");
    }
    myConnection.Close();
}

static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fname, lname, dob;

    Console.WriteLine("Enter first name:");
    fname = Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lname = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy):");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateofBirth) " +
        $"VALUES ('{fname}', '{lname}', '{dob}')";

    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInserted}");


    ReadData(myConnection);
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an id to delete a customer:");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowDeleted = command.ExecuteNonQuery();
    Console.WriteLine($"{rowDeleted} was removed from the customer table.");

    ReadData(myConnection);
}

