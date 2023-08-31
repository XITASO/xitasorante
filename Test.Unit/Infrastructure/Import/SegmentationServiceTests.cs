using System.Reflection;
using Infrastructure.Import;

namespace Test.Unit.Infrastructure.Import;

public class SegmentationServiceTest
{
    private string[] testData =
    {
        "id,first,second,third,fourth",
        "1,1,2,3,4",
        "2,5,6,7,8",
        "3,9,10,11,12",
        "4,13,14,15,16",
        "5,17,18,19,20"
    };

    public SegmentationServiceTest()
    {
        // cleanup left overs from previous test runs
        var dir = Directory.GetParent(Assembly.GetAssembly(typeof(SegmentationService))!.Location);
        dir?.GetFiles("*.csv").ToList().ForEach(f => f.Delete());
    }

    [Fact]
    public void getSegmentDataReturnsRequestedLines()
    {
        SegmentationService service = new SegmentationService();

        int fileId = 1;
        createFileWithTestData(fileId);

        var result = service.GetSegmentData(fileId, 2, 4);
        var lines = ReadAllLines(result).ToList();

        Assert.Equal(3, lines.Count);
        Assert.Equal(testData[0], lines[0]); // header
        Assert.Equal(testData[3], lines[1]);
        Assert.Equal(testData[4], lines[2]);
    }

    private IEnumerable<string> ReadAllLines(Stream content)
    {
        using var reader = new StreamReader(content);
        string? currentLine;
        while ((currentLine = reader.ReadLine()) != null)
        {
            yield return currentLine;
        }
    }

    [Fact]
    public void getSegmentDataLimitsResultsWhenExpectedLinesGiven()
    {
        SegmentationService service = new SegmentationService();

        int fileId = 1;
        createFileWithTestData(fileId);

        int expectedLines = 2;

        var result = service.GetSegmentData(fileId, 0, 5, expectedLines);
        var lines = ReadAllLines(result).ToList();

        Assert.Equal(expectedLines + 1, lines.Count);
        Assert.Equal(testData[0], lines[0]); // header
        Assert.Equal(testData[2], lines[1]);
        Assert.Equal(testData[4], lines[2]);
    }

    private void createFileWithTestData(int fileId)
    {
        var basePath = Directory.GetParent(Assembly.GetAssembly(typeof(SegmentationService))!.Location)!.FullName;
        var filePath = Path.Combine(basePath, fileId + ".csv");
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var line in testData)
            {
                writer.WriteLine(line);
            }

            writer.Close();
        }
    }
}