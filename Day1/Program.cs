using System.Text.RegularExpressions;

//read all lines into a variable lines
var lines = File.ReadLines("input.txt");

int sum = 0;

//create a dictionary to map words to numbers
Dictionary<string, int> map = new Dictionary<string, int>();
map.Add("n", 1);
map.Add("w", 2);
map.Add("hre", 3);
map.Add("four", 4);
map.Add("fiv", 5);
map.Add("six", 6);
map.Add("seve", 7);
map.Add("igh", 8);
map.Add("in", 9);

Part1(lines);
Part2(lines);

void Part1(IEnumerable<string?> lines)
{
    sum = 0;

    //match all digits
    string regexPart1 = @"\d+";

    //repeat this for every line in lines
    foreach (var line in lines)
    {
        //find every string that matches the regex expression
        MatchCollection numbers = Regex.Matches(line, regexPart1);

        //firstAndLast will equal first digit of first number + last digit of last number
        string firstAndLast = string.Concat(numbers.First().Value.First(), numbers.Last().Value.Last());

        sum += int.Parse(firstAndLast);
    }

    Console.WriteLine(sum);
}

void Part2(IEnumerable<string?> lines)
{
    sum = 0;

    //bit more complicated: match all digits, and find all words
    //due to overlaps, we don't want to remove any characters from the line when found
    //therefore we use regex lookaround to avoid looking at any characters that could possibly overlap other numbers
    string regexPart2 = @"\d+|(?<=o)n(?=e)|(?<=t)w(?=o)|(?<=t)hre(?=e)|four|fiv(?=e)|six|seve(?=n)|(?<=e)igh(?=t)|(?<=n)in(?=e)";

    foreach (var line in lines)
    {
        MatchCollection numbers = Regex.Matches(line, regexPart2);
        string first, last;

        //if first substring found is a key in the map, convert to number
        if (map.ContainsKey(numbers.First().ToString()))
        {
            first = map[numbers.First().ToString()].ToString();
        }
        //else, we know we found a numeric digit, not a word
        else
        {
            first = numbers.First().Value.First().ToString();
        }
    
        //same as above but for the last word / number found
        if (map.ContainsKey(numbers.Last().ToString()))
        {
            last = map[numbers.Last().ToString()].ToString();
        }
        else
        {
            last = numbers.Last().Value.Last().ToString();
        }

        string firstAndLast = string.Concat(first, last);
        sum += int.Parse(firstAndLast);
    }

    Console.WriteLine(sum);
}