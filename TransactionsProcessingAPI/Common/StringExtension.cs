using System.Globalization;

namespace TransactionsProcessingAPI.Common;

public static class StringExtension
{
    public static bool TryParse(this string line, out Transaction parsedLine)
    {
        var splitLine = line.Split(',');

        var category = splitLine[4];
        var description = splitLine[5];
        var merchant = splitLine[6];

        if (Guid.TryParse(splitLine[0], out Guid transactionId) &&
            Guid.TryParse(splitLine[1], out Guid userId) &&
            DateTime.TryParse(splitLine[2], out DateTime date) &&
            decimal.TryParse(splitLine[3], CultureInfo.InvariantCulture, out decimal amount))
        {
            parsedLine = new Transaction(
                transactionId,
                userId,
                date.ToUniversalTime(),
                amount,
                category,
                description,
                merchant);
            return true;
        }
        else
        {
            parsedLine = null;
            return false;
        }
    }
}
