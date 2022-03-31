using System;
using System.Collections.Generic;
class Program
{
    static void Main(string[] args)
    {


        //get input
        var input = GetInput();

        string[] arrayOfRowsByNewlines = input.Split('\n');

        var tableHolder = FlattenTheTriangleIntoTable(arrayOfRowsByNewlines);

        var result = WalkThroughTheNode(arrayOfRowsByNewlines, tableHolder);

        Console.WriteLine($"The Maximum Total Sum Of Non-Prime Numbers From Top To Bottom Is:  {result[0,0]}");

        Console.ReadKey();
    }

    private static string GetInput()
    {


        const string input =      @" 1
                                   8 4
                                  2 6 9
                                8 5 9 3";
        return input;
    }

    private static int[,] WalkThroughTheNode(string[] arrayOfRowsByNewlines, int[,] tableHolder)
    {

       var resetResult= ResetAllPrimeNumbers(arrayOfRowsByNewlines, tableHolder); 

        // walking through the non-prime node
        for (int i = arrayOfRowsByNewlines.Length - 2; i >= 0; i--)
        {
            for (int j = 0; j < arrayOfRowsByNewlines.Length; j++)
            {
                var c = resetResult[i, j];
                var a = resetResult[i + 1, j];
                var b = resetResult[i + 1, j + 1];
                //only sum through the non - prime node
                if ((!IsPrime(c) && !IsPrime(a)) || (!IsPrime(c) && !IsPrime(b)))
                    tableHolder[i, j] = c + Math.Max(a, b);

            }
        }
        return tableHolder;
    }

    private static int[,] ResetAllPrimeNumbers(string[] arrayOfRowsByNewlines, int[,] tableHolder)
    {
        for (int i = 0; i < arrayOfRowsByNewlines.Length; i++)
        {
            for (int j = 0; j < arrayOfRowsByNewlines.Length; j++)
            {
                if (IsPrime(tableHolder[i, j]))
                    tableHolder[i, j] = 0;
            }
        }
        return tableHolder;
    }

    public static  Dictionary<int,bool> PrimeCache= new Dictionary<int, bool>();
    private static int[,] FlattenTheTriangleIntoTable(string[] arrayOfRowsByNewlines)
    {
        int[,] tableHolder = new int[arrayOfRowsByNewlines.Length, arrayOfRowsByNewlines.Length + 1];

        for (int row = 0; row < arrayOfRowsByNewlines.Length; row++)
        {
            var eachCharactersInRow = arrayOfRowsByNewlines[row].Trim().Split(' ');

            for (int column = 0; column < eachCharactersInRow.Length; column++)
            {
                int number;
                int.TryParse(eachCharactersInRow[column], out number);
                tableHolder[row, column] = number;
            }
        }
        return tableHolder;
    }

    public static bool IsPrime(int number)
    {
        // Test whether the parameter is a prime number.
        if (PrimeCache.ContainsKey(number))
        {
            bool value;
            PrimeCache.TryGetValue(number, out value);
            return value;
        }
        if ((number & 1) == 0)
        {
            if (number == 2)
            {
                if (!PrimeCache.ContainsKey(number)) PrimeCache.Add(number, true);
                return true;
            }
            if (!PrimeCache.ContainsKey(number)) PrimeCache.Add(number, false);
            return false;
        }

        for (int i = 3; (i * i) <= number; i += 2)
        {
            if ((number % i) == 0)
            {
                if (!PrimeCache.ContainsKey(number)) PrimeCache.Add(number, false);
                return false;
            }
        }
        var check= number != 1;
        if (!PrimeCache.ContainsKey(number)) PrimeCache.Add(number, check);
        return check;
    }


}