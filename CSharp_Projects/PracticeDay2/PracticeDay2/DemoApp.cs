namespace PracticeDay2;

public class DemoApp
{
    public void Increment(ref int a)
    {
        ++a;
    }

    static void Main()
    {
        int a = 12;
        DemoApp demoApp = new DemoApp();
        demoApp.Increment(ref a);
        Console.WriteLine(a);
    }
}