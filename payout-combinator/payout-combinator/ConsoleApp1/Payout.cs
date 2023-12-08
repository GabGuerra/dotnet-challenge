namespace payout_combinator
{
    public class Payout
    {
        public Payout(decimal payoutAmount)
        {
            PayoutAmount = payoutAmount;
        }
        
        public decimal PayoutAmount { get; set; }
        public string PayoutDetails { get; set; }
    }
}
