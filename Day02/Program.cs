
using var reader = new StreamReader(new FileInfo("Input.txt").OpenRead());

var maxR = 12;
var maxG = 13;
var maxB = 14;

var answer = 0;
var impossibleGames = 0;

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
    
    foreach (var g in games)
    {
        if (isImpossible) continue;
        
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
    }
    
    if (!isImpossible)
    {
        answer += gId;
    }
}

Console.WriteLine(answer);