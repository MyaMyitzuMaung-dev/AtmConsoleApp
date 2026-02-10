using Microsoft.Data.SqlClient;
using System.Data;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("========== MAIN MENU ==========");
            Console.WriteLine("1. Account CRUD Operations");
            Console.WriteLine("2. Student CRUD Operations");
            Console.WriteLine("3. Exit");
            Console.WriteLine("===============================");
            Console.Write("Enter your choice (1-3): ");

            string mainChoice = Console.ReadLine();

            switch (mainChoice)
            {
                case "1":
                    AccountCRUD();
                    break;
                case "2":
                    StudentCRUD();
                    break;
                case "3":
                    Console.WriteLine("Exiting program. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice! Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static SqlConnection GetConnection()
    {
        SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
        sqlConnectionStringBuilder.DataSource = ".";
        sqlConnectionStringBuilder.InitialCatalog = "InPersonBatch2";
        sqlConnectionStringBuilder.UserID = "sa";
        sqlConnectionStringBuilder.Password = "12345";
        sqlConnectionStringBuilder.TrustServerCertificate = true;

        return new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
    }

    static void AccountCRUD()
    {
        using (SqlConnection connection = GetConnection())
        {
            connection.Open();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== ACCOUNT CRUD ==========");
                Console.WriteLine("1. View all accounts");
                Console.WriteLine("2. Create new account");
                Console.WriteLine("3. Update account");
                Console.WriteLine("4. Delete account");
                Console.WriteLine("5. Back to Main Menu");
                Console.WriteLine("==================================");
                Console.Write("Enter your choice (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": // Read
                        SqlCommand cmd = new SqlCommand(@"SELECT [AccountId]
                              ,[Name]
                              ,[MobileNo]
                              ,[Password]
                              ,[Balance]
                          FROM [dbo].[Tbl_Account] Where IsDeleted = 0", connection);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        Console.WriteLine("\nAccount List:");
                        Console.WriteLine("==========================================");
                        foreach (DataRow dr in dt.Rows)
                        {
                            Console.WriteLine($"AccountId: {dr["AccountId"]}");
                            Console.WriteLine($"Name: {dr["Name"]}");
                            Console.WriteLine($"MobileNo: {dr["MobileNo"]}");
                            Console.WriteLine($"Password: {dr["Password"]}");
                            Console.WriteLine($"Balance: {dr["Balance"]}");
                            Console.WriteLine("------------------------------------------");
                        }

                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "2": // Create
                        Console.Write("Enter name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter mobile no: ");
                        string mobileNo = Console.ReadLine();

                        Console.Write("Enter password: ");
                        string password = Console.ReadLine();

                        string createQuery = $@"INSERT INTO [dbo].[Tbl_Account]
                               ([AccountId]
                               ,[Name]
                               ,[MobileNo]
                               ,[Password]
                               ,[Balance]
                               ,[IsDeleted])
                         VALUES
                               ('{Guid.NewGuid().ToString()}'
                               ,'{name}'
                               ,'{mobileNo}'
                               ,'{password}'
                               ,0
                               ,0)";

                        SqlCommand createCmd = new SqlCommand(createQuery, connection);
                        int createResult = createCmd.ExecuteNonQuery();
                        string createMessage = createResult > 0 ?
                            "✓ Account created successfully" :
                            "✗ Failed to create account";
                        Console.WriteLine(createMessage);

                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "3": // Update
                        Console.Write("Enter account id: ");
                        string id = Console.ReadLine();

                        Console.Write("Enter name: ");
                        string updateName = Console.ReadLine();

                        Console.Write("Enter mobile no: ");
                        string updateMobileNo = Console.ReadLine();

                        Console.Write("Enter password: ");
                        string updatePassword = Console.ReadLine();

                        Console.Write("Enter balance: ");
                        string updateBalance = Console.ReadLine();

                        string updateQuery = $@"
                        UPDATE [dbo].[Tbl_Account]
                           SET [Name] = '{updateName}'
                              ,[MobileNo] = '{updateMobileNo}'
                              ,[Password] = '{updatePassword}'
                              ,[Balance] = '{updateBalance}'
                         WHERE AccountId = '{id}'";

                        SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                        int updateResult = updateCmd.ExecuteNonQuery();
                        string updateMessage = updateResult > 0 ?
                            "✓ Account updated successfully" :
                            "✗ Failed to update account";
                        Console.WriteLine(updateMessage);

                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "4": // Delete
                        Console.Write("Enter account id: ");
                        string deleteId = Console.ReadLine();

                        string deleteQuery = $@"
                        UPDATE [dbo].[Tbl_Account]
                           SET IsDeleted = 1
                         WHERE AccountId = '{deleteId}'";

                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
                        int deleteResult = deleteCmd.ExecuteNonQuery();
                        string deleteMessage = deleteResult > 0 ?
                            "✓ Account deleted successfully" :
                            "✗ Failed to delete account";
                        Console.WriteLine(deleteMessage);

                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid choice! Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }

    static void StudentCRUD()
    {
        using (SqlConnection connection = GetConnection())
        {
            connection.Open();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== STUDENT CRUD ==========");
                Console.WriteLine("1. View all students");
                Console.WriteLine("2. Create new student");
                Console.WriteLine("3. Update student");
                Console.WriteLine("4. Delete student");
                Console.WriteLine("5. Back to Main Menu");
                Console.WriteLine("===================================");
                Console.Write("Enter your choice (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": // Read
                        SqlCommand cmd = new SqlCommand(@"SELECT [StudentId]
                              ,[StudentName]
                              ,[Age]
                              ,[ClassNo]
                              ,[MobileNo]
                              ,[ParentName]
                              ,[Fees]
                          FROM [dbo].[Tbl_Student] Where IsDeleted = 0", connection);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        Console.WriteLine("\nStudent List:");
                        Console.WriteLine("==========================================");
                        foreach (DataRow dr in dt.Rows)
                        {
                            Console.WriteLine($"StudentId: {dr["StudentId"]}");
                            Console.WriteLine($"StudentName: {dr["StudentName"]}");
                            Console.WriteLine($"Age: {dr["Age"]}");
                            Console.WriteLine($"ClassNo: {dr["ClassNo"]}");
                            Console.WriteLine($"MobileNo: {dr["MobileNo"]}");
                            Console.WriteLine($"ParentName: {dr["ParentName"]}");
                            Console.WriteLine($"Fees: {dr["Fees"]}");
                            Console.WriteLine("------------------------------------------");
                        }

                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "2": // Create
                        Console.Write("Enter student name: ");
                        string studentName = Console.ReadLine();

                        Console.Write("Enter age: ");
                        string age = Console.ReadLine();

                        Console.Write("Enter class no: ");
                        string classNo = Console.ReadLine();

                        Console.Write("Enter mobile no: ");
                        string mobileNo = Console.ReadLine();

                        Console.Write("Enter parent name: ");
                        string parentName = Console.ReadLine();

                        Console.Write("Enter fees: ");
                        string fees = Console.ReadLine();

                        string createQuery = $@"INSERT INTO [dbo].[Tbl_Student]
                               ([StudentId]
                               ,[StudentName]
                               ,[Age]
                               ,[ClassNo]
                               ,[MobileNo]
                               ,[ParentName]
                               ,[IsDeleted]
                               ,[Fees])
                         VALUES
                               ('{Guid.NewGuid().ToString()}'
                               ,'{studentName}'
                               ,'{age}'
                               ,'{classNo}'
                               ,'{mobileNo}'
                               ,'{parentName}'
                               ,0
                               ,'{fees}')";

                        SqlCommand createCmd = new SqlCommand(createQuery, connection);
                        int createResult = createCmd.ExecuteNonQuery();
                        string createMessage = createResult > 0 ?
                            "✓ Student created successfully" :
                            "✗ Failed to create student";
                        Console.WriteLine(createMessage);

                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "3": // Update
                        Console.Write("Enter student id: ");
                        string id = Console.ReadLine();

                        Console.Write("Enter student name: ");
                        string updateStudentName = Console.ReadLine();

                        Console.Write("Enter age: ");
                        string updateAge = Console.ReadLine();

                        Console.Write("Enter class no: ");
                        string updateClassNo = Console.ReadLine();

                        Console.Write("Enter mobile no: ");
                        string updateMobileNo = Console.ReadLine();

                        Console.Write("Enter parent name: ");
                        string updateParentName = Console.ReadLine();

                        Console.Write("Enter fees: ");
                        string updateFees = Console.ReadLine();

                        string updateQuery = $@"
                        UPDATE [dbo].[Tbl_Student]
                           SET [StudentName] = '{updateStudentName}'
                              ,[Age] = '{updateAge}'
                              ,[ClassNo] = '{updateClassNo}'
                              ,[MobileNo] = '{updateMobileNo}'
                              ,[ParentName] = '{updateParentName}'
                              ,[Fees] = '{updateFees}'
                         WHERE StudentId = '{id}'";

                        SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                        int updateResult = updateCmd.ExecuteNonQuery();
                        string updateMessage = updateResult > 0 ?
                            "✓ Student updated successfully" :
                            "✗ Failed to update student";
                        Console.WriteLine(updateMessage);

                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "4": // Delete
                        Console.Write("Enter student id: ");
                        string deleteId = Console.ReadLine();

                        string deleteQuery = $@"
                        UPDATE [dbo].[Tbl_Student]
                           SET IsDeleted = 1
                         WHERE StudentId = '{deleteId}'";

                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
                        int deleteResult = deleteCmd.ExecuteNonQuery();
                        string deleteMessage = deleteResult > 0 ?
                            "✓ Student deleted successfully" :
                            "✗ Failed to delete student";
                        Console.WriteLine(deleteMessage);

                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid choice! Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}