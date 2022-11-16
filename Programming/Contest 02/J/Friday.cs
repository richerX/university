using System;

partial class Program
{
    
    private static bool ValidateData(int day, int month, int year)
    {
        if (year < 1701 || year > 1800)
        {
            return false;
        }
        switch (month)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                return (day > 0 && day < 32);
            case 4:
            case 6:
            case 9:
            case 11:
                return (day > 0 && day < 31);
            case 2:
                if (year % 4 == 0 && year != 1800) // leap year
                {
                    return (day > 0 && day < 30);
                }
                return (day > 0 && day < 29);
            default:
                return false;
        }
    }
    
    private static int GetDayOfWeek(int day, int month, int year)
    {
        int answer;
        int year_shift, month_shift, day_shift;
        year_shift = EvaluateYearShiftAlternative(year);
        month_shift = EvaluateMonthShift(month);
        day_shift = day;
        answer = year_shift + month_shift + day_shift;
        if (year % 4 == 0 && year != 1800 && month > 2)
        {
            answer += 1;
        }
        // Console.WriteLine($"YS {year_shift} | MS {month_shift} | DS {day_shift}");
        // Console.WriteLine($"Answer = {answer}, {answer % 7}");
        if (answer % 7 == 0)
        {
            return 7;
        }
        return answer % 7;
    }

    private static string GetDateOfFriday(int dateOfWeek, int day, int month, int year)
    {
        int days_to_add = DaysToAdd(dateOfWeek);
        // Console.WriteLine($"Current day of week - {dateOfWeek}");
        // Console.WriteLine($"Days to add - {days_to_add}");
        // Console.WriteLine($"Days in month - {DaysInMonth(month, year)}");
        if (days_to_add + day > DaysInMonth(month, year))
        {
            if (month == 12)
            {
                return $"{day + days_to_add - DaysInMonth(month, year),2:d2}.{1,2:d2}.{year + 1}";
            }
            else
            {
                return $"{day + days_to_add - DaysInMonth(month, year),2:d2}.{month + 1,2:d2}.{year}";
            }
        }
        else
        {
            return $"{day + days_to_add,2:d2}.{month,2:d2}.{year}";
        }
    }

    private static int EvaluateYearShift(int year)
    {
        int Y = year % 100;
        int YB = NearestLeap(Y);
        return (100 + 2 * Y - 3 * YB) / 2;
    }

    private static int EvaluateYearShiftAlternative(int year)
    {
        switch (year)
        {
            case 1701:
                return 6;
            case 1702:
                return 7;
            case 1703:
                return 1;
            case 1704:
                return 2;
            case 1705:
                return 4;
            case 1706:
                return 5;
            case 1707:
                return 6;
            case 1708:
                return 7;
            case 1709:
                return 2;
            case 1710:
                return 3;
            case 1711:
                return 4;
            case 1712:
                return 5;
            case 1713:
                return 7;
            case 1714:
                return 1;
            case 1715:
                return 2;
            case 1716:
                return 3;
            case 1717:
                return 5;
            case 1718:
                return 6;
            case 1719:
                return 7;
            case 1720:
                return 1;
            case 1721:
                return 3;
            case 1722:
                return 4;
            case 1723:
                return 5;
            case 1724:
                return 6;
            case 1725:
                return 1;
            case 1726:
                return 2;
            case 1727:
                return 3;
            case 1728:
                return 4;
            case 1729:
                return 6;
            case 1730:
                return 7;
            case 1731:
                return 1;
            case 1732:
                return 2;
            case 1733:
                return 4;
            case 1734:
                return 5;
            case 1735:
                return 6;
            case 1736:
                return 7;
            case 1737:
                return 2;
            case 1738:
                return 3;
            case 1739:
                return 4;
            case 1740:
                return 5;
            case 1741:
                return 7;
            case 1742:
                return 1;
            case 1743:
                return 2;
            case 1744:
                return 3;
            case 1745:
                return 5;
            case 1746:
                return 6;
            case 1747:
                return 7;
            case 1748:
                return 1;
            case 1749:
                return 3;
            case 1750:
                return 4;
            case 1751:
                return 5;
            case 1752:
                return 6;
            case 1753:
                return 1;
            case 1754:
                return 2;
            case 1755:
                return 3;
            case 1756:
                return 4;
            case 1757:
                return 6;
            case 1758:
                return 7;
            case 1759:
                return 1;
            case 1760:
                return 2;
            case 1761:
                return 4;
            case 1762:
                return 5;
            case 1763:
                return 6;
            case 1764:
                return 7;
            case 1765:
                return 2;
            case 1766:
                return 3;
            case 1767:
                return 4;
            case 1768:
                return 5;
            case 1769:
                return 7;
            case 1770:
                return 1;
            case 1771:
                return 2;
            case 1772:
                return 3;
            case 1773:
                return 5;
            case 1774:
                return 6;
            case 1775:
                return 7;
            case 1776:
                return 1;
            case 1777:
                return 3;
            case 1778:
                return 4;
            case 1779:
                return 5;
            case 1780:
                return 6;
            case 1781:
                return 1;
            case 1782:
                return 2;
            case 1783:
                return 3;
            case 1784:
                return 4;
            case 1785:
                return 6;
            case 1786:
                return 7;
            case 1787:
                return 1;
            case 1788:
                return 2;
            case 1789:
                return 4;
            case 1790:
                return 5;
            case 1791:
                return 6;
            case 1792:
                return 7;
            case 1793:
                return 2;
            case 1794:
                return 3;
            case 1795:
                return 4;
            case 1796:
                return 5;
            case 1797:
                return 7;
            case 1798:
                return 1;
            case 1799:
                return 2;
            case 1800:
                return 3;
            default:
                return 0;
        }
    }

    private static int NearestLeap(int year)
    {
        while (year % 4 != 0)
        {
            year -= 1;
        }
        return year;
    }

    private static int EvaluateMonthShift(int month)
    {
        switch (month)
        {
            // 6 2 2 5 0 3 5 1 4 6 2 4
            case 1:
                return 6;
            case 2:
                return 2;
            case 3:
                return 2;
            case 4:
                return 5;
            case 5:
                return 0;
            case 6:
                return 3;
            case 7:
                return 5;
            case 8:
                return 1;
            case 9:
                return 4;
            case 10:
                return 6;
            case 11:
                return 2;
            case 12:
                return 4;
            default:
                return 0;
        }
    }

    private static int DaysToAdd(int dateOfWeek)
    {
        if (dateOfWeek > 5)
        {
            return 12 - dateOfWeek;
        }
        return 5 - dateOfWeek;
    }

    private static int DaysInMonth(int month, int year)
    {
        switch (month)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                return 31;
            case 4:
            case 6:
            case 9:
            case 11:
                return 30;
            case 2:
                if (year % 4 == 0 && year != 1800) // leap year
                {
                    return 29;
                }
                return 28;
            default:
                return 0;
        }
    }
}