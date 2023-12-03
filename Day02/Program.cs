
using var reader = new StreamReader(new FileInfo("Input.txt").OpenRead());

const int maxR = 12;
const int maxG = 13;
const int maxB = 14;

int p1Answer = 0, p2Answer = 0;

while (!reader.EndOfStream)
{
    var nextLine = reader.ReadLine();
    
    if (nextLine is null) continue;

    var games = nextLine[(nextLine.IndexOf(':') + 2)..].Split(';');
    
    var gId = int.Parse(nextLine[nextLine.IndexOf(' ')..nextLine.IndexOf(':')]);

    var gD = new Dictionary<string, int>
    {
        { "red", 0 },
        { "green", 0 },
        { "blue", 0 }
    };

    var isImpossible = false;
    int minR = 0, minG = 0, minB = 0;
    
    foreach (var g in games)
    {
        var sets = g.Split(',');

        foreach (var s in sets)
        {
            var results = s.Trim().Split(' ');
            gD[results[1]] = int.Parse(results[0]);
        }
        
        if (gD["red"] > maxR ||
            gD["green"] > maxG ||
            gD["blue"] > maxB)
        {
            isImpossible = true;
        }

        if (gD["red"] > minR) minR = gD["red"];
        if (gD["green"] > minG) minG = gD["green"];
        if (gD["blue"] > minB) minB = gD["blue"];
    }
    
    if (!isImpossible)
    {
        p1Answer += gId;
    }

    p2Answer += minR * minG * minB;
}

Console.WriteLine($"Part 1: {p1Answer}");
Console.WriteLine($"Part 2: {p2Answer}");