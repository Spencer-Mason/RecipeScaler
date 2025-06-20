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

        /// <summary>
        /// Attempts to convert a given quantity and unit to a more appropriate unit.
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

                    // If conversion results in a clean value (like 1, 2.5, 3), return it
                    if (newQty >= 1 && newQty < factor)
                    {
                        return (Math.Round(newQty, 2), altUnit);
                    }
                }
            }

            return (Math.Round(quantity, 2), unit);
        }
    }
}
