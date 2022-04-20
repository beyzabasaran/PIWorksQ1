using System;
using System.Collections.Generic;
class Program
{
    static void Main(string[] args)
    {


        //get example input
        var input = getGivenInput();

        string[] rowArray = input.Split('\n');
        //P.S. Dynamic Approach has been used in the algorithm
        var dynamicTable = ConvertTriangleToTable(rowArray);

        var result = traverseNodes(rowArray, dynamicTable);

        Console.WriteLine($"Maximum Total Sum Of the Non-Prime Numbers is {result[0,0]}");
  
        Console.ReadKey();
    }

    private static string getGivenInput()
    {
        const string input =      @" 1
                                   8 4
                                  2 6 9
                                8 5 9 3";
        return input;
    }
  private static string getGivenSecondInput()
    {
        const string secondInput =   @" 215
                                   193 124
                                  117 237 442
                                218 935 347 235
                              320 804 522 417 345
                            229 601 723 835 133 124
                          248 202 277 433 207 263 257
                        359 464 504 528 516 716 871 182
                      461 441 426 656 863 560 380 171 923
                     381 348 573 533 447 632 387 176 975 449
                   223 711 445 645 245 543 931 532 937 541 444
                 330 131 333 928 377 733 017 778 839 168 197 197
                131 171 522 137 217 224 291 413 528 520 227 229 928
              223 626 034 683 839 053 627 310 713 999 629 817 410 121
            924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";
    
        return secondInput;
    }

    private static int[,] traverseNodes(string[] rowArray, int[,] dynamicTable)
    {

       var resetPrimeNumsResult= ResetPrimeNumbers(rowArray, dynamicTable); 

        // walking through the non-prime node
        for (int i = rowArray.Length - 2; i >= 0; i--)
        {
            for (int j = 0; j < rowArray.Length; j++)
            {
                var c = resetPrimeNumsResult[i, j];
                var a = resetPrimeNumsResult[i + 1, j];
                var b = resetPrimeNumsResult[i + 1, j + 1];
                //sum through the non-prime nodes
                if ((!IsNumberPrime(c) && !IsNumberPrime(a)) || (!IsNumberPrime(c) && !IsNumberPrime(b)))
                    dynamicTable[i, j] = c + Math.Max(a, b);

            }
        }
        return dynamicTable;
    }

    private static int[,] ResetPrimeNumbers(string[] rowArray, int[,] tableHolder)
    {
        for (int i = 0; i < rowArray.Length; i++)
        {
            for (int j = 0; j < rowArray.Length; j++)
            {
                if (IsNumberPrime(tableHolder[i, j]))
                    tableHolder[i, j] = 0;
            }
        }
        return tableHolder;
    }

    public static  Dictionary<int,bool> PrimeList= new Dictionary<int, bool>();
    private static int[,] ConvertTriangleToTable(string[] rowArray)
    {
        int[,] dynamicTable = new int[rowArray.Length, rowArray.Length + 1];

        for (int row = 0; row < rowArray.Length; row++)
        {
            var RowCharacters = rowArray[row].Trim().Split(' ');

            for (int column = 0; column < RowCharacters.Length; column++)
            {
                int number;
                int.TryParse(RowCharacters[column], out number);
                dynamicTable[row, column] = number;
            }
        }
        return dynamicTable;
    }

    public static bool IsNumberPrime(int number)
    {
        // Test whether the parameter is a prime number.
        if (PrimeList.ContainsKey(number))
        {
            bool value;
            PrimeList.TryGetValue(number, out value);
            return value;
        }
        if ((number & 1) == 0)
        {
            if (number == 2)
            {
                if (!PrimeList.ContainsKey(number)) PrimeList.Add(number, true);
                return true;
            }
            if (!PrimeList.ContainsKey(number)) PrimeList.Add(number, false);
            return false;
        }

        for (int i = 3; (i * i) <= number; i += 2)
        {
            if ((number % i) == 0)
            {
                if (!PrimeList.ContainsKey(number)) PrimeList.Add(number, false);
                return false;
            }
        }
        var check= number != 1;
        if (!PrimeList.ContainsKey(number)) PrimeList.Add(number, check);
        return check;
    }


}
