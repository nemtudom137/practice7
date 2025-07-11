namespace Business.Data;

public static class TestData
{
    public const string CompanyOverviewFileName = "EPAM_Corporate_Overview_Q4FY-2024.pdf";

    public static readonly object[][] CarrierSearch =
   [
       ["C", "All Locations", string.Empty],
        ["Java", "Japan", "Tokyo"]
   ];

    public static readonly object[] GlobalSearch =
    [
       "BLOCKCHAIN",
       "RPA"
    ];

    public static readonly object[] SwipeTimes = [0, 2, 7];
}