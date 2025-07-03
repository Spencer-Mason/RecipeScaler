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

        // Common fractional representations for tsp
        private static readonly Dictionary<double, string> tspFractions = new()
        {
            { 0.5, "½ tsp" },
            { 0.25, "¼ tsp" },
            { 0.125, "⅛ tsp" }
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

            return unit switch
            {
                "cup" => ConvertCups(quantity),
                "tbsp" => ConvertTablespoons(quantity),
                _ => ConvertDefault(quantity, unit)
            };
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

        private static string ConvertTablespoons(double quantity)
        {
            int whole = (int)Math.Floor(quantity);
            double remainder = quantity - whole;

            string result = "";

            if (whole > 0)
            {
                result += whole == 1 ? "1 tbsp" : $"{whole} tbsp";
            }

            if (remainder >= 0.01)
            {
                // Convert remainder to tsp
                var (altUnit, factor) = conversions["tbsp"];
                double tspQty = remainder * factor;

                // Try to match to a friendly tsp fraction
                string? tspFraction = tspFractions
                    .OrderByDescending(f => f.Key)
                    .FirstOrDefault(f => Math.Abs(tspQty - f.Key) < 0.05).Value;

                if (tspFraction != null)
                {
                    if (!string.IsNullOrEmpty(result)) result += " and ";
                    result += tspFraction;
                }
                else if (tspQty > 0.1)
                {
                    if (!string.IsNullOrEmpty(result)) result += " and ";
                    result += $"{Math.Round(tspQty)} tsp";
                }
                else
                {
                    if (!string.IsNullOrEmpty(result)) result += " and ";
                    result += "less than ⅛ tsp";
                }
            }

            return string.IsNullOrEmpty(result) ? "0 tbsp" : result;
        }

        private static string ConvertDefault(double quantity, string unit)
        {
            var (qty, normalizedUnit) = Normalize(quantity, unit);
            return $"{qty} {normalizedUnit}";
        }
    }
}
