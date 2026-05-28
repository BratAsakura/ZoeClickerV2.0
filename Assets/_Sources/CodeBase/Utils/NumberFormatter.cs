public static class NumberFormatter
{
    public static string Format(double value)
    {
        const int Threshold = 1000;
        int index = 0;

        string[] suffixes =
            {
            "", "K", "M", "B", "T", "q", "Q", "s", "S",
            "Sp", "Oc", "No", "Dc", "Un", "Du", "Tr", "Qt", "Qi", "Se",
            "SpT", "OcT", "NoT", "DcT", "UnT", "DuT", "TrT"
        };

        while (value >= Threshold && index < suffixes.Length - 1)
        {
            value /= Threshold;
            index++;
        }

        string formatted = value % 1 == 0 ? value.ToString("F0") : value.ToString("F1");

        return formatted + suffixes[index];
    }
}
