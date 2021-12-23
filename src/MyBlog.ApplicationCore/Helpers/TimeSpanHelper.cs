public class TimeSpanHelper
{
    private int? Days;
    public int? Weeks 
    { 
        get
        {
            return Days.Value < 7 ? 0 : (int)Math.Floor((decimal)Days.Value / 7);
        }
    }

    public TimeSpanHelper(int? days) => Days = days;
}
