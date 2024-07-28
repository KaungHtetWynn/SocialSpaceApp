namespace API.Extensions;

// Extension method needs to be in static class
public static class CustomDateTimeExtensions
{
    public static int CalculateAge(this DateOnly dateOfBirth)
    {
        var todayDate = DateOnly.FromDateTime(DateTime.Now);

        var userAge = todayDate.Year - dateOfBirth.Year;

        return userAge;
    }
}
