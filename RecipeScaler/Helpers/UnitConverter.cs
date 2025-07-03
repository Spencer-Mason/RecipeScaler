using System.Text;

namespace RecipeScaler.Helpers
{
    public static class UnitConverter
    {
        private static readonly Dictionary<string, (string altUnit, double conversionFactor)> conversions = new()
        {
            // Cup -> Tbsp, Tsp
            { "cup", ("tbsp", 16) },
            { "tbsp", ("tsp", 3) },

            // Metric
            { "l", ("ml", 1000) },
            { "kg", ("g", 1000) },

            // Imperial weight
            { "lb", ("oz", 16) }
        };

        // Common fractional representations for cups
        private static readonly Dictionary<double, string> cupFractions = new()
        {
            { 0.75, "¾" },
            { 0.66, "⅔" },
            { 0.5,  "½" },
            { 0.33, "⅓" },
            { 0.25, "¼" }
        };

        /// <summary>
        /// Normalizes a unit/quantity by converting to a smaller unit if possible.
        /// Still useful for metric/weight units or raw output.
        /// </summary>
        public static (double quantity, string unit) Normalize(double quantity, string unit)
        {
            unit = unit.ToLower();

            if (conversions.ContainsKey(unit))
            {
                var (altUnit, factor) = conversions[unit];

                if (quantity < 1)
                {
                    double newQty = quantity * factor;

                    // If conversion results in a clean value, return it in the smaller unit
                    if (newQty >= 1 && newQty < factor)
                    {
                        return (Math.Round(newQty, 2), altUnit);
                    }
                }
            }

            return (Math.Round(quantity, 2), unit);
        }

        /// <summary>
        /// Converts a given quantity and unit into a friendly string for display (e.g. "1 cup and 2 tbsp").
        /// </summary>
        public static string ToFriendlyString(double quantity, string unit)
        {
            unit = unit.ToLower();

            if (unit == "cup")
            {
                return ConvertCups(quantity);
            }

            // Use Normalize fallback for other units
            var (newQty, newUnit) = Normalize(quantity, unit);
            return $"{newQty} {newUnit}";
        }

        /// <summary>
        /// Converts decimal cups into fractions or smaller units like tbsp.
        /// </summary>
        private static string ConvertCups(double quantity)
        {
            int whole = (int)Math.Floor(quantity);
            double remainder = quantity - whole;

            string result = "";

            if (whole > 0)
            {
                result += whole == 1 ? "1 cup" : $"{whole} cups";
            }

            // Check if remainder matches a known friendly fraction
            string? fractionLabel = cupFractions
                .OrderByDescending(f => f.Key)
                .FirstOrDefault(f => Math.Abs(remainder - f.Key) < 0.04).Value;

            if (fractionLabel != null)
            {
                if (!string.IsNullOrEmpty(result)) result += " and ";
                result += fractionLabel;
                return result;
            }

            // If remainder isn't a clean fraction, convert to tablespoons
            if (remainder > 0.01)
            {
                var (altUnit, factor) = conversions["cup"];
                double tbsp = remainder * factor;

                if (!string.IsNullOrEmpty(result)) result += " and ";
                result += $"{Math.Round(tbsp)} {altUnit}";
            }

            return string.IsNullOrEmpty(result) ? "0 cups" : result;
        }
    }
}
