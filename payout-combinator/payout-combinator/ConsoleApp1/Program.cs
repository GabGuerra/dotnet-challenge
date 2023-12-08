// See https://aka.ms/new-console-template for more information

using payout_combinator;

var atm = new ATM();

var payoutAmounts = new decimal[] { 30, 50, 60, 80, 140, 230, 370, 610, 980, };

foreach (var payoutAmount in payoutAmounts)
{
    var payout = atm.CalculatePossiblePayouts(payoutAmount);
    Console.WriteLine(payout.PayoutDetails);
}

Console.WriteLine("Press any key to exit");
Console.ReadKey();
