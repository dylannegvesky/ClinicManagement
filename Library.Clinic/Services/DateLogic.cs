namespace Library.Clinic.Services
{
    public class DateLogic
    {
        public static DateTime GetUserDate(string prompt)
        {
            var validDate = false;
            DateTime result;

            do
            {
                Console.Write(prompt);
                string? dateInput = Console.ReadLine();

                if (DateTime.TryParse(dateInput, out result))
                {
                    validDate = true;
                }
                else
                {
                    Console.WriteLine("That date is invalid, try again...");
                }
            } while (!validDate);
            return result;
        }
    }
}
