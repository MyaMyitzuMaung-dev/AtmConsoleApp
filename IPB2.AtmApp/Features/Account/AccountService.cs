namespace IPB2.AtmApp.Features.Account
{
    #region Dtos
    public class CreateAccountRequestDto
    {
        public CreateAccountRequestDto(string name, string mobileNo, string password, string confirmPassword)
        {
            Name = name;
            MobileNo = mobileNo;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class BasicResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    // Table
    public class AccountDto
    {
        public AccountDto(string id, string name, string mobileNo, string password, decimal balance = 0) //constructor
        {
            AccountId = id;
            Name = name;
            MobileNo = mobileNo;
            Password = password;
            Balance = balance;
        }
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
    }

    public class DepositRequestDto
    {
        public DepositRequestDto(string mobileNo, decimal amount)
        {
            MobileNo = mobileNo;
            Amount = amount;
        }

        public string MobileNo { get; set; }
        public decimal Amount { get; set; }
    }

    public class BalanceRequestDto
    {
        public BalanceRequestDto(string mobileNo)
        {
            MobileNo = mobileNo;
        }

        public string MobileNo { get; set; }
    }

    public class WithdrawRequestDto
    {
        public WithdrawRequestDto(string mobileNo, decimal amount)
        {
            MobileNo = mobileNo;
            Amount = amount;
        }

        public string MobileNo { get; set; }
        public decimal Amount { get; set; }
    }

    public class LoginRequestDto
    {
        public LoginRequestDto(string mobileNo, string password)
        {
            MobileNo = mobileNo;
            Password = password;
        }

        public string MobileNo { get; set; }
        public string Password { get; set; }
    }

    #endregion

    // Business Logic
    public class AccountService
    {
        // fake database
        private static List<AccountDto> _accounts = new List<AccountDto>();

        public BasicResponseDto CreateAccount(CreateAccountRequestDto request)
        {
            // Validation
            if (string.IsNullOrEmpty(request.Name))
            {
                return new BasicResponseDto
                {
                    IsSuccess = false,
                    Message = "Please enter your name."
                };
            }
            if (string.IsNullOrEmpty(request.MobileNo))
            {
                return new BasicResponseDto
                {
                    IsSuccess = false,
                    Message = "Please enter your mobile no."
                };
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                return new BasicResponseDto
                {
                    IsSuccess = false,
                    Message = "Please enter your password."
                };
            }
            if (string.IsNullOrEmpty(request.ConfirmPassword))
            {
                return new BasicResponseDto
                {
                    IsSuccess = false,
                    Message = "Please enter your confirm password."
                };
            }

            // Password check
            if (request.Password != request.ConfirmPassword)
            {
                return new BasicResponseDto
                {
                    IsSuccess = false,
                    Message = "Password and Confirm Password do not match."
                };
            }

            // Exist Mobile No check
            bool isExistMobileNo = _accounts.Any(x => x.MobileNo == request.MobileNo);
            if (isExistMobileNo)
            {
                return new BasicResponseDto
                {
                    IsSuccess = false,
                    Message = "Mobile No already exists."
                };
            }

            AccountDto accountDto = new AccountDto(
                    Guid.NewGuid().ToString(),
                    request.Name,
                    request.MobileNo,
                    request.Password
                );
            _accounts.Add(accountDto);
            return new BasicResponseDto
            {
                IsSuccess = true,
                Message = "Account created successfully."
            };
        }

        public BasicResponseDto CreateDeposit(DepositRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.MobileNo))
                return new BasicResponseDto { IsSuccess = false, Message = "Please enter your mobile no." };

            if (request.Amount <= 0)
                return new BasicResponseDto { IsSuccess = false, Message = "Amount must be greater than 0." };

            var account = _accounts.FirstOrDefault(x => x.MobileNo == request.MobileNo.Trim()); //Trim to avoid spaces
            if (account is null)
                return new BasicResponseDto { IsSuccess = false, Message = "Account not found." };

            account.Balance += request.Amount;

            return new BasicResponseDto
            {
                IsSuccess = true,
                Message = $"Deposit successful. Current balance: {account.Balance}"
            };
        }

        public BasicResponseDto GetBalance(BalanceRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.MobileNo))
                return new BasicResponseDto { IsSuccess = false, Message = "Please enter your mobile no." };

            var account = _accounts.FirstOrDefault(x => x.MobileNo == request.MobileNo.Trim());
            if (account is null)
                return new BasicResponseDto { IsSuccess = false, Message = "Account not found." };

            return new BasicResponseDto
            {
                IsSuccess = true,
                Message = $"Current balance: {account.Balance}"
            };
        }

        public BasicResponseDto Withdraw(WithdrawRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.MobileNo))
                return new BasicResponseDto { IsSuccess = false, Message = "Please enter your mobile no." };

            if (request.Amount <= 0)
                return new BasicResponseDto { IsSuccess = false, Message = "Amount must be greater than 0." };

            var account = _accounts.FirstOrDefault(x => x.MobileNo == request.MobileNo.Trim());
            if (account is null)
                return new BasicResponseDto { IsSuccess = false, Message = "Account not found." };

            if (request.Amount > account.Balance)
                return new BasicResponseDto { IsSuccess = false, Message = "Insufficient balance." };

            account.Balance -= request.Amount;

            return new BasicResponseDto
            {
                IsSuccess = true,
                Message = $"Withdraw successful. Current balance: {account.Balance}"
            };
        }

        public BasicResponseDto Login(LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.MobileNo))
                return new BasicResponseDto { IsSuccess = false, Message = "Please enter your mobile no." };

            if (string.IsNullOrWhiteSpace(request.Password))
                return new BasicResponseDto { IsSuccess = false, Message = "Please enter your password." };

            var account = _accounts.FirstOrDefault(x => x.MobileNo == request.MobileNo.Trim());
            if (account is null)
                return new BasicResponseDto { IsSuccess = false, Message = "Account not found." };

            if (account.Password != request.Password)
                return new BasicResponseDto { IsSuccess = false, Message = "Invalid password." };

            return new BasicResponseDto { IsSuccess = true, Message = $"Hello, {account.Name}!" };
        }

    }
}
