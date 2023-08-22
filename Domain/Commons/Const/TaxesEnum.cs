namespace Domain.Commons.Const;

public static class TaxesEnum
{
    public static readonly decimal CDI = DividedBy100(0.9m) ;
    public static readonly decimal TB = DividedBy100(108M);
    public static readonly decimal SixMonthsTax = DividedBy100(22.5M);
    public static readonly decimal TwelveMonthsTax = DividedBy100 (20M);
    public static readonly decimal TwentyFourMonthsTax = DividedBy100 (17.5M);
    public static readonly decimal AboveTwentyFourMonthsTax = DividedBy100(15M);
    
    private static decimal DividedBy100(decimal value) => value / 100;
}