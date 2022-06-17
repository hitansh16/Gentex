string path = "C:/Users/hitan/source/repos/Gentex/Gentex/data.csv";
int dataCount = System.IO.File.ReadAllLines(path).Length;
string[,] output = new string[dataCount, 6];
string[] lines = System.IO.File.ReadAllLines(path);
int count = 0;
double area = 0;
double perimeter = 0;
int points = 0;
double CenterX = 0;
double CenterY = 0;
foreach (string line in lines)
{
    var result = line.Split(',')[1];
    if(result == "Circle")
    {
        area = Math.PI * Math.Pow(double.Parse(line.Split(',')[7]), 2);
        perimeter = 2 * Math.PI * double.Parse(line.Split(',')[7]);
        CenterX = double.Parse(line.Split(',')[3]);
        CenterY = double.Parse(line.Split(',')[5]);
    }
    else if (result == "Square")
    {
        area = Math.Pow(double.Parse(line.Split(',')[7]), 2);
        perimeter = 4 * double.Parse(line.Split(',')[7]);
        CenterX = double.Parse(line.Split(',')[3]);
        CenterY = double.Parse(line.Split(',')[5]);
    }
    else if (result == "Equilateral Triangle")
    {
        area = Math.Sqrt(3)/4 * Math.Pow(double.Parse(line.Split(',')[7]), 2);
        perimeter = 3 * double.Parse(line.Split(',')[7]);
        CenterX = double.Parse(line.Split(',')[3]);
        CenterY = double.Parse(line.Split(',')[5]);
    }
    else if (result == "Ellipse")
    {
        area = Math.PI * double.Parse(line.Split(',')[7]) * double.Parse(line.Split(',')[9]);
        perimeter = Math.PI * (3 * (double.Parse(line.Split(',')[7]) + double.Parse(line.Split(',')[9])) - Math.Sqrt((3 * double.Parse(line.Split(',')[7]) + double.Parse(line.Split(',')[9])) * (double.Parse(line.Split(',')[7]) + 3 * double.Parse(line.Split(',')[9]))));
        CenterX = double.Parse(line.Split(',')[3]);
        CenterY = double.Parse(line.Split(',')[5]);
    }
    else if (result == "Polygon")
    {
        points = line.Split(',').Length;
        area = 0;
        perimeter = 0;
        for (int i = 3; i < points - 4; i = i + 4)
        {
            area += (double.Parse(line.Split(',')[i + 4]) - double.Parse(line.Split(',')[i])) * (double.Parse(line.Split(',')[i + 6]) + double.Parse(line.Split(',')[i + 2])) / 2;
            perimeter += Math.Sqrt(Math.Pow((double.Parse(line.Split(',')[i + 4]) - double.Parse(line.Split(',')[i])), 2) + Math.Pow((double.Parse(line.Split(',')[i + 6]) - double.Parse(line.Split(',')[i + 2])), 2));
        }
        area = Math.Abs(area);
        perimeter = Math.Abs(perimeter);
        CenterX = 0;
        CenterY = 0;
    }
    output[count, 0] = (count + 1).ToString();
    output[count, 1] = result;
    output[count, 2] = area.ToString();
    output[count, 3] = perimeter.ToString();
    output[count, 4] = CenterX.ToString();
    output[count, 5] = CenterY.ToString();
    count++;
}
using (StreamWriter outfile = new StreamWriter(@"C:/Users/hitan/source/repos/Gentex/Gentex/Output.csv"))
{
    for (int x = 0; x < output.GetLength(0); x++)
    {
        string content = "";
        for (int y = 0; y < output.GetLength(1); y++)
        {
            content += output[x, y] + ",";
        }
        outfile.WriteLine(content);
    }
    Console.WriteLine("Check the Output.csv file!");
}