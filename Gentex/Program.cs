var start = DateTime.Now; //store start time of script
string path = "C:/Users/hitan/source/repos/Gentex/Gentex/data.csv";
int dataCount = System.IO.File.ReadAllLines(path).Length;
string[,] output = new string[dataCount, 6]; //create an array the size of the number of shapes in the csv file
string[] lines = System.IO.File.ReadAllLines(path);
int count = 0; //keep count of the amount of shapes processed
double area = 0;
double perimeter = 0;
double CenterX = 0;
double CenterY = 0;
foreach (string line in lines)
{
    var result = line.Split(',')[1]; //split each part of the lines where a , is present and pick the second data
    if(result == "Circle")
    {
        area = Math.PI * Math.Pow(double.Parse(line.Split(',')[7]), 2); //find area of circle (pi * r^2)
        perimeter = 2 * Math.PI * double.Parse(line.Split(',')[7]); //find perimeter of circle (2 * pi * r)
        CenterX = double.Parse(line.Split(',')[3]); //get the x center point of the shape from the line
        CenterY = double.Parse(line.Split(',')[5]); //get the y center point of the shape from the line
    }
    else if (result == "Square")
    {
        area = Math.Pow(double.Parse(line.Split(',')[7]), 2); //find area of square (side^2)
        perimeter = 4 * double.Parse(line.Split(',')[7]); //find perimeter of square (4 * side)
        CenterX = double.Parse(line.Split(',')[3]); //get the x center point of the shape from the line
        CenterY = double.Parse(line.Split(',')[5]); //get the y center point of the shape from the line
    }
    else if (result == "Equilateral Triangle")
    {
        area = Math.Sqrt(3)/4 * Math.Pow(double.Parse(line.Split(',')[7]), 2); //find area of equilateral triangle (sqrt(3)/4 * side^2)
        perimeter = 3 * double.Parse(line.Split(',')[7]); //find perimeter of equilateral triangle (3 * side)
        CenterX = double.Parse(line.Split(',')[3]); //get the y center point of the shape from the line
        CenterY = double.Parse(line.Split(',')[5]); //get the y center point of the shape from the line
    }
    else if (result == "Ellipse")
    {
        area = Math.PI * double.Parse(line.Split(',')[7]) * double.Parse(line.Split(',')[9]); //find area of ellipse (pi * r1 * r2)
        perimeter = Math.PI * (3 * (double.Parse(line.Split(',')[7]) + double.Parse(line.Split(',')[9])) - Math.Sqrt((3 * double.Parse(line.Split(',')[7]) + double.Parse(line.Split(',')[9])) * (double.Parse(line.Split(',')[7]) + 3 * double.Parse(line.Split(',')[9])))); //find perimeter of ellipse (pi * (3 * (r1 + r2) - sqrt((3 * r1 + r2) * (r1 + 3 * r2))))
        CenterX = double.Parse(line.Split(',')[3]); //get the y center point of the shape from the line
        CenterY = double.Parse(line.Split(',')[5]); //get the y center point of the shape from the line
    }
    else if (result == "Polygon")
    {
        area = 0; //reset area
        perimeter = 0; //reset perimeter
        for (int i = 3; i < line.Split(',').Length - 4; i = i + 4) //run loop for the amount of data in the line
        {
            area += (double.Parse(line.Split(',')[i + 4]) - double.Parse(line.Split(',')[i])) * (double.Parse(line.Split(',')[i + 6]) + double.Parse(line.Split(',')[i + 2])) / 2; //area of polygon
            perimeter += Math.Sqrt(Math.Pow((double.Parse(line.Split(',')[i + 4]) - double.Parse(line.Split(',')[i])), 2) + Math.Pow((double.Parse(line.Split(',')[i + 6]) - double.Parse(line.Split(',')[i + 2])), 2)); //perimeter of ellipse (sum of all distances from one point to another)
        }
        area = Math.Abs(area); //make sure the answer is positive
        perimeter = Math.Abs(perimeter); //make sure the answer is positive
        CenterX = 0; //placeholder
        CenterY = 0; //placeholder
    }
    output[count, 0] = (count + 1).ToString(); //store the shape ID
    output[count, 1] = result; //store the shape type
    output[count, 2] = area.ToString(); //store the shape area
    output[count, 3] = perimeter.ToString(); //store the shape perimeter
    output[count, 4] = CenterX.ToString(); //store the shape's center X
    output[count, 5] = CenterY.ToString(); //store the shape's center Y
    count++; //increment to next shape
}
using (StreamWriter outfile = new StreamWriter(@"C:/Users/hitan/source/repos/Gentex/Gentex/Output.csv")) //define path for output file
{
    for (int x = 0; x < output.GetLength(0); x++) //run loop for all shapes
    {
        string content = ""; //reset string
        for (int y = 0; y < output.GetLength(1); y++) //run loop for data of each shape
        {
            content += output[x, y] + ","; //create string with , seperating all data
        }
        outfile.WriteLine(content); //push to csv file
    }
    Console.WriteLine("Check the Output.csv file!"); //print to let user know that script is complete
}
var end = DateTime.Now; //store end time of script
Console.WriteLine(end - start); //print out amount of time taken to run script