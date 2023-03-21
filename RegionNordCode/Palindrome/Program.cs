using System.Text.RegularExpressions;

public class Palindrome
{
    static void Main(string[] args)
    {
        string userInput;
        bool running = true;

        Console.WriteLine("Welcome to the Palindrome checker");
        Console.WriteLine("Type 'exit' to stop)");

        while (running)
        {
            Console.WriteLine("Enter a word or integer to be checked:");

            userInput= Console.ReadLine();
            
            //If the word exit is used, stop the program
            if(userInput.ToLower() == "exit")
            {
                Console.WriteLine("Bye!");
                running= false;
            }
            else if (userInput != null)
            {
                try
                {
                //Do the check and write the result
                Console.WriteLine("is '" + userInput + "' a palindrome: " + PalindromeCheck(userInput).ToString());
                }
                catch(Exception ex) 
                {
                    Console.WriteLine("Something went wrong");
                    //Should do some logging here
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    private static bool PalindromeCheck(int input)
    {
        // The below method can check string sequences as well.
        // But i seem to remember that there is a mathematical quick way of checking integers
        return PalindromeCheck(input.ToString());
    }
    private static bool PalindromeCheck(string input)
    {
        bool result = false;
        //TODO remove special characters
        //Trim all the whitespaces and make it lower
        string trim = Regex.Replace(input, " ", string.Empty).ToLower();

        char[] trimCharArray = trim.ToCharArray();
        int length = trimCharArray.Length;

        for (int i = 0; i < length; i++)
        {
            //Reverse checking the indexes, if one does not match, we stop and return false
            if(trimCharArray[i] == trimCharArray[length - (i+1)])
            {
                result= true;
            }
            else
            {
                return false;
            }
        }
        return result;
    } 
}
