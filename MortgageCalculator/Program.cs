namespace MortgageCalculator
{
    public class MortgageCalculator
    {
        public static double CalculateMonthlyRepayment(double loanAmount, double annualInterestRate, int loanTermYears)
        {
            // Calculate the monthly repayment amount
            double monthlyInterestRate = annualInterestRate / 12 / 100;
            int numberOfPayments = loanTermYears * 12;
            double monthlyRepayment = loanAmount * monthlyInterestRate / (1 - Math.Pow(1 + monthlyInterestRate, -numberOfPayments));
            return monthlyRepayment;
        }

        public static double CalculateTotalInterestPaid(double loanAmount, double annualInterestRate, int loanTermYears)
        {
            // Calculate the total amount of interest paid over the life of the loan
            double monthlyPayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
            int numberOfPayments = loanTermYears * 12;
            double totalInterestPaid = monthlyPayment * numberOfPayments - loanAmount;
            return totalInterestPaid;
        }

        public static double CalculateTotalAmountPaid(double loanAmount, double annualInterestRate, int loanTermYears)
        {
            // Calculate the total amount paid over the life of the loan
            double monthlyPayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
            int numberOfPayments = loanTermYears * 12;
            double totalAmountPaid = monthlyPayment * numberOfPayments;
            return totalAmountPaid;
        }

        public static List<string> GenerateAmortizationSchedule(double loanAmount, double annualInterestRate, int loanTermYears)
        {
            // Generate the amortization schedule
            List<string> amortizationSchedule = new List<string>();
            double monthlyPayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
            double remainingBalance = loanAmount;
            double monthlyInterestRate = annualInterestRate / 12 / 100;

            // Creating the amortization data
            for (int i = 1; i <= loanTermYears * 12; i++)
            {
                double interestPaid = remainingBalance * monthlyInterestRate;
                double principalPaid = monthlyPayment - interestPaid;
                remainingBalance -= principalPaid;

                string entry = $"{i}\t\t{monthlyPayment:C}\t\t{interestPaid:C}\t\t{principalPaid:C}\t\t{remainingBalance:C}";
                amortizationSchedule.Add(entry);
            }

            return amortizationSchedule;
        }

        public static void InvalidAmounts()
        {
            Console.WriteLine("\n----------------------------\nPlease enter valid amounts.\n----------------------------");
        }

        public static double GetLoanAmount()
        {
            // Get amount and checks for FormatException and negative amounts
            double loanAmount = 0;
            bool isValid = false;
            while (!isValid)
            {
                try
                {
                    Console.Write("\nEnter the loan amount: ");
                    loanAmount = Convert.ToDouble(Console.ReadLine());
                }
                catch (FormatException)
                {
                    InvalidAmounts();
                    continue;
                }
                if (loanAmount > 0)
                {
                    isValid = true;
                }
                else
                {
                    InvalidAmounts();
                }
            }
            return loanAmount;
        }

        public static double GetAnnualInterestRate()
        {
            // Get amount and checks for FormatException and negative amounts
            double annualInterestRate = 0;
            bool isValid = false;
            while (!isValid)
            {
                try
                {
                    Console.Write("\nEnter the annual interest rate (in percentage): ");
                    annualInterestRate = Convert.ToDouble(Console.ReadLine());
                }
                catch (FormatException)
                {
                    InvalidAmounts();
                    continue;
                }
                if (annualInterestRate > 0)
                {
                    isValid = true;
                }
                else
                {
                    InvalidAmounts();
                }
            }
            return annualInterestRate;
        }

        public static int GetLongTermYears()
        {
            // Get amount and checks for FormatException and negative amounts
            int loanTermYears = 0;
            bool isValid = false;
            while (!isValid)
            {
                try
                {
                    Console.Write("\nEnter the loan term in years: ");
                    loanTermYears = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    InvalidAmounts();
                    continue;
                }

                if (loanTermYears > 0)
                {
                    isValid = true;
                }
                else
                {
                    InvalidAmounts();
                }
            }
            return loanTermYears;
        }

        public static void Main()
        {
            Console.WriteLine("------------------------------------\nWelcome to the Mortgage Calculator!\nCreated by Salomon Jansen Pretorius\nStudent Number: 20231348\n------------------------------------");
            while (true)
            {
                // Get loan details
                double loanAmount = GetLoanAmount();
                double annualInterestRate = GetAnnualInterestRate();
                int loanTermYears = GetLongTermYears();

                // Calculate mortgage details
                double monthlyRepayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
                double totalInterestPaid = CalculateTotalInterestPaid(loanAmount, annualInterestRate, loanTermYears);
                double totalAmountPaid = CalculateTotalAmountPaid(loanAmount, annualInterestRate, loanTermYears);
                List<string> amortizationSchedule = GenerateAmortizationSchedule(loanAmount, annualInterestRate, loanTermYears);

                // Output results
                Console.WriteLine($"\nMonthly Repayment Amount: {monthlyRepayment:C}");
                Console.WriteLine($"Total Interest Paid: {totalInterestPaid:C}");
                Console.WriteLine($"Total Amount Paid: {totalAmountPaid:C}");

                Console.WriteLine("\nAmortization Schedule:");
                Console.WriteLine("Payment #\tPayment Amount\tInterest Paid\tPrincipal Paid\tRemaining Balance");
                foreach (var entry in amortizationSchedule)
                {
                    Console.WriteLine(entry);
                }

            // Repeat action
            repeat:
                Console.Write("\nDo you want to do another calculation? (Y or n): ");
                string repeat = Console.ReadLine();
                if (repeat == "n")
                {
                    break;
                }
                else if (repeat == "y" || repeat == "")
                {
                    continue;
                }
                else
                {
                    goto repeat;
                }
            }
        }
    }
}