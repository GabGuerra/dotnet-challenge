using System.Text;

namespace payout_combinator
{
    public class ATM
    {

        public decimal[] Cartridges = new decimal[] { 10M, 50M, 100M };

        public ATM()
        {

        }

        public Payout CalculatePossiblePayouts(decimal payoutAmount)
        {
            var payout = new Payout(payoutAmount);
            var noPayout = payoutAmount == 0;

            if (noPayout)
            {
                payout.PayoutDetails = Constants.ErrorMessages.PAYOUT_AMOUNT_MANDATORY;
                return payout;
            }

            payout.PayoutDetails = GetPossiblePayouts(payoutAmount);
            return payout;

        }

        private string GetPossiblePayouts(decimal payoutAmount)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{payoutAmount} EUR Payout options:");
            var possibleCombinations = GetPayoutCombinations(payoutAmount);

            foreach (var combinations in possibleCombinations)
            {                
                sb.AppendLine(string.Join(" + ", combinations.Select(c => $"{c.Key} X {c.Value}")));
            }

            sb.AppendLine("---------------------------------------------------------");

            return sb.ToString();
        }

        private List<Dictionary<decimal, int>> GetPayoutCombinations(decimal payoutAmount, int index = 0)
        {
            var combinations = new List<Dictionary<decimal, int>>();
            var hasPassedTheLastAvailableCartridge = index >= Cartridges.Length;

            if (hasPassedTheLastAvailableCartridge)
                return combinations;

            var cartridge = Cartridges[index];
            var cartridgesNeeded = payoutAmount / cartridge;

            if (cartridgesNeeded < 1)
                return combinations;

            for (int i = 0; i <= cartridgesNeeded; i++)
            {
                var remaining = payoutAmount - cartridge * i;

                if (remaining == 0 && i > 0)
                    combinations.Add(new Dictionary<decimal, int> { { cartridge, i } }); // add the amount of times the cartridge was used

                var subCombinations = GetPayoutCombinations(remaining, index + 1);
                foreach (var subCombination in subCombinations)
                {
                    if (i > 0)
                        subCombination[cartridge] = i;           

                    combinations.Add(subCombination);
                }
            }

            return combinations;
        }
    }
}


