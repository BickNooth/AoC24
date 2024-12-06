namespace AoC24.Day5;

public class Day5
{
    private List<(int before, int after)> _pageOrders = new();
    private List<List<int>> _pageUpdates = new();
    public void Part1()
    {
        //ParseFile("Day5\\example.txt");
        ParseFile("Day5\\input.txt");
        
        var middleNumbersSum = 0;
        foreach (var pageUpdate in _pageUpdates)
        {
           var correctOrders = CheckPageUpdateOrder(pageUpdate);
           if (!correctOrders)
           {
               continue;
           }

           var middleIndex = pageUpdate.Count / 2;
           middleNumbersSum += pageUpdate[middleIndex];
        }

        Console.WriteLine("Middle numbers sum: "+middleNumbersSum);
    }

    public void Part2()
    {
        //ParseFile("Day5\\example.txt");
        ParseFile("Day5\\input.txt");

        var middleNumbersSum = 0;
        foreach (var pageUpdate in _pageUpdates)
        {
            var updatesList = pageUpdate;
            var correctOrders = CheckPageUpdateOrder(updatesList);
            if (correctOrders)
            {
                continue;
            }

            while (!correctOrders)
            {
                FixUpdatesListItem(updatesList);
                correctOrders = CheckPageUpdateOrder(updatesList);
            }

            var middleIndex = pageUpdate.Count / 2;
            middleNumbersSum += pageUpdate[middleIndex];
        }

        Console.WriteLine("Middle numbers sum: " + middleNumbersSum);
    }

    private void FixUpdatesListItem(List<int> updatesList)
    {
        foreach (var pageOrder in _pageOrders)
        {
            var firstPageLocation = updatesList.IndexOf(pageOrder.before);
            var secondPageLocation = updatesList.IndexOf(pageOrder.after);
            if (firstPageLocation == -1 || secondPageLocation == -1)
            {
                continue;
            }

            if (firstPageLocation <= secondPageLocation)
            {
                continue;
            }

            Swap(updatesList, firstPageLocation, secondPageLocation);
            break;
        }
    }

    private bool CheckPageUpdateOrder(List<int> pageUpdate)
    {
        foreach (var pageOrder in _pageOrders)
        {
            var firstPageLocation = pageUpdate.IndexOf(pageOrder.before);
            var secondPageLocation = pageUpdate.IndexOf(pageOrder.after);
            if (firstPageLocation == -1 || secondPageLocation == -1)
            {
                continue;
            }

            if (firstPageLocation < secondPageLocation)
            {
                continue;
            }

            return false;
        }

        return true;
    }

    private static void Swap(List<int> list, int firstPage, int secondPage)
    {
        (list[firstPage], list[secondPage]) = (list[secondPage], list[firstPage]);
    }

    private void ParseFile(string fileName)
    {
        var lines = File.ReadAllLines(fileName);

        var isTupleSection = true;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isTupleSection = false;
                continue;
            }

            if (isTupleSection)
            {
                var pageOrder = line.Split('|').Select(int.Parse).ToArray();
                _pageOrders.Add((pageOrder[0], pageOrder[1]));
                continue;
            }
            
            var update = line.Split(',').Select(int.Parse).ToList();
            _pageUpdates.Add(update);
        }
    }
}