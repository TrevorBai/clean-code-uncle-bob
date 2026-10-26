// Pay attention to these method names, it's an art.

public void Pay()
{
    foreach (Employee e in employees)
        PayIfNecessary(e);
}

private void PayIfNecessary(Employee e)
{
    if (e.IsPayDay())
        CalculateAndDeliverPay(e);
}

private void CalculateAndDeliverPay(Employee e)
{
    Money pay = e.CalculatePay();
    e.DeliverPay(pay);
}
