namespace IPB2.AtmApp.Features.Account
{
    public class AccountUI
    {
        private readonly AccountService _service = new AccountService();

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n=== ATM App (Step 1) ===");
                Console.WriteLine("1) Create Account");
                Console.WriteLine("2) Exit");
                Console.Write("Choose: ");
                var choice = Console.ReadLine();

                
                if (choice == "1") CreateAccount();
                else if (choice == "2") return;
                else Console.WriteLine("Invalid option.");
            }
        }

        private void CreateAccount()
        {
            Console.WriteLine("\n=== Create New Account ===");

            Console.Write("Enter your name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter your mobile no: ");
            string mobileNo = Console.ReadLine() ?? "";

            string password;
            string confirmPassword;

            while (true)
            {
                Console.Write("Enter your password: ");
                password = Console.ReadLine() ?? "";

                Console.Write("Enter your confirm password: ");
                confirmPassword = Console.ReadLine() ?? "";

                if (password == confirmPassword) break;

                Console.WriteLine("Password and Confirm Password do not match.");
            }

            var req = new CreateAccountRequestDto(name, mobileNo, password, confirmPassword);
            var result = _service.CreateAccount(req);

            Console.WriteLine(result.Message);
        }
    }

 }
