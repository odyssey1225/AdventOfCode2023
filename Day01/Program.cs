
using var reader = new StreamReader(new FileInfo("Input.txt").OpenRead());

var numbersDictionary = new Dictionary<string, char>
{
    { "one", '1' },
    { "two", '2' },
    { "three", '3' },
    { "four", '4' },
    { "five", '5' },
    { "six", '6' },
    { "seven", '7' },
    { "eight", '8' },
    { "nine", '9' },
};

var answer = 0;

while (!reader.EndOfStream)
{
    var nextLine = reader.ReadLine();
    
    if (nextLine is null) continue;

    var accumulator = string.Empty;
    var numbers = string.Empty;
    
    foreach (var c in nextLine)
    {
        if (char.IsDigit(c))
        {
            accumulator = string.Empty;
            numbers += c;
            continue;
        }
        
        accumulator += c;

        var match = numbersDictionary.Keys.FirstOrDefault(a => accumulator.Contains(a));

        if (match is not null)
        {
            numbers += numbersDictionary[match];
            
            var overlap = accumulator[^1..];
            
            accumulator = numbersDictionary.Keys.Any(a => a.StartsWith(overlap))
                ? overlap
                : string.Empty;
        }
    }
    
    answer += int.Parse($"{numbers[0]}{numbers[^1]}");
}

Console.WriteLine(answer);