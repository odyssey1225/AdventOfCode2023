using var reader = new StreamReader(new FileInfo("Input.txt").OpenRead());

var p1Answer = 0;

var table = new List<char[]>();

var nums = new List<RowNumber>();

var rIndex = 0;

while (!reader.EndOfStream)
{
    var nextLine = reader.ReadLine();

    if (nextLine is null) continue;

    table.Add(nextLine.ToCharArray());

    var num = string.Empty;
    for (var i  = 0; i < nextLine.Length; i++)
    {
        if (char.IsDigit(nextLine[i]))
        {
            num += nextLine[i];
            if (i + 1 < nextLine.Length) continue;
        }

        if (int.TryParse(num, out var n))
        {
            var startIndex = i - num.Length;
            if (i + 1 == nextLine.Length && char.IsDigit(nextLine[i])) ++startIndex;
            
            nums.Add(new RowNumber(n, rIndex, startIndex, num.Length));
        }

        num = string.Empty;
    }

    rIndex++;
}

foreach (var n in nums)
{
    var startIndex = n.startIndex;
    if (startIndex > 0) --startIndex;
    
    var endIndex = n.startIndex + n.length - 1;
    if (endIndex < table[n.row].Length - 1) ++endIndex;

    var topMatch = n.row > 0 && table[n.row - 1][startIndex..(endIndex + 1)].Any(IsMatchingChar);
    var bottomMatch = n.row < table.Count - 1 && table[n.row + 1][startIndex..(endIndex + 1)].Any(IsMatchingChar);
    var leftMatch = IsMatchingChar(table[n.row][startIndex]);
    var rightMatch = IsMatchingChar(table[n.row][endIndex]);

    if (topMatch || bottomMatch || leftMatch || rightMatch)
    {
        p1Answer += n.number;
    }
}

Console.WriteLine($"Part 1: {p1Answer}");

bool IsMatchingChar(char c) => c != '.' && !char.IsDigit(c);

record RowNumber(int number, int row, int startIndex, int length);