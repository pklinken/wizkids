using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WizKids
{
    class Program
    {
        // https://stackoverflow.com/questions/16167983/best-regular-expression-for-email-validation-in-c-sharp/16168118
        static string EMAIL_REGEX = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

        static void Main(string[] args)
        {
            // 1
            // Console.WriteLine(ReverseString("Not a palindrome"));
            // Console.WriteLine($"This is a palindrome: {IsPalindrome("abc is a l a si cba")}");

            // 2
            // FooBar();

            // 3
            // Console.WriteLine(ReplaceEmailAddresses(@"Christian has the email address christian+123@gmail.com.
            // Christian's friend, John Cave-Brown, has the email address john.cave-brown@gmail.com.
            // John's daughter Kira studies at Oxford University and has the email adress Kira123@oxford.co.uk.
            // Her Twitter handle is @kira.cavebrown.", "kregan@battle.net"));

            // 4a
            var result = GenerateDLAlternatives("test");
            foreach(var word in result)
            {
                Console.WriteLine(word);
            }

            // 4b
            Console.WriteLine($"Calculated amount of alternative words: {CalculateAlternatives(4, 26)}");
            Console.WriteLine($"Actual amount of alternative words: {result.Count}");
        }

        public static string ReverseString(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        
        static Boolean IsPalindrome(string s)
        {
            return ReverseString(s).Equals(s);
        }

        static Boolean IsFoo(int n)
        {
            return n % 3 == 0;
        }

        static Boolean IsBar(int n)
        {
            return n % 5 == 0;
        }
        static void FooBar()
        {
            for(int index = 1; index <= 100; index++)
            {
                if(IsFoo(index) && IsBar(index))
                {
                    Console.WriteLine("FooBar");
                } else if(IsFoo(index)) {
                    Console.WriteLine("Foo");
                } else if(IsBar(index))
                {
                    Console.WriteLine("Bar");
                } else
                {
                    Console.WriteLine(index.ToString());
                }
            }
        }

        static string ReplaceEmailAddresses(string content, string email)
        {
            return Regex.Replace(content, EMAIL_REGEX, email);
        }

        static List<string> GenerateDLAlternatives(string word)
        {
            List<string> result = new List<String>();
            char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            // Insert
            for(int index = 0; index <= word.Length; index++)
            {
                foreach(char c in alphabet)
                {
                    result.Add(word.Insert(index, c.ToString()));
                }
            }

            // Delete

            for (int index = 0; index < word.Length; index++)
            {
               result.Add(word.Remove(index, 1));
            }

            // Replace

            for (int index = 0; index < word.Length; index++)
            {
                var prefix = word.Substring(0, index);
                var postfix = word.Substring(index + 1, word.Length - (index + 1));
                foreach (char c in alphabet)
                {
                    if(word[index] != c)
                    {
                        result.Add(prefix + c.ToString() + postfix);    
                    }
                }
            }

            // Swap
            for (int index = 0; index < word.Length - 1; index++)
            {
                var prefix = word.Substring(0, index);
                var postfix = word.Substring(index + 2, word.Length - (index + 2));
                result.Add(prefix + word[index + 1] + word[index] + postfix);
            }

            return result;
        }

        static int CalculateAlternatives(int wordLength, int alphabetLength)
        {
            int resultSum = 0;

            // Insertion
            resultSum += alphabetLength * (wordLength + 1);

            // Replacement
            resultSum += (alphabetLength - 1) * wordLength;

            // Deletion
            resultSum += wordLength;

            // Swapping
            resultSum += wordLength - 1;

            return resultSum;
        }
    }
}
