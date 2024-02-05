using System;


var account = new Account(50);

account.AddNotifyer(new EmailBalanceChanged("readgme@gmail.com"));
account.AddNotifyer(new EmailBalanceChanged("gopotron@gmail.com"));
account.AddNotifyer(new SMSLowBalance("+7(993) 380 72-99", 20));
account.AddNotifyer(new SMSLowBalance("+7(924) 394 72-99", 40));

account.ChangeBalance(100);
account.ChangeBalance(5);
account.ChangeBalance(50);
account.ChangeBalance(30);

Console.WriteLine();

public class Account
{
    private int _balance;
    private List<INotifyer> _notifiers = new();

    public Account() { }

    public Account(int balance)
    {
        _balance = balance;
    }

    public void AddNotifyer(INotifyer notifyer)
    {
        _notifiers.Add(notifyer);
    }

    public void ChangeBalance(int newBalance)
    {
        _balance = newBalance;
        Notification();
    }

    public int Balance
    {
        get { return _balance; }
    }

    private void Notification()
    {
        Console.WriteLine();
        _notifiers.ForEach(
            (item) =>
            {
                item.Notify(_balance);
            }
        );
    }
}

public class EmailBalanceChanged : INotifyer
{
    private string _email;

    public EmailBalanceChanged(string email)
    {
        _email = email;
    }

    public void Notify(int balance)
    {
        Console.WriteLine(_email + ": баланс изменен, текущий баланс = " + balance);
    }
}

public interface INotifyer
{
    public void Notify(int balance);
}


public class SMSLowBalance : INotifyer
{
    private string _phone;
    private int _lowBalanceValue;

    public SMSLowBalance(string phone, int lowBalanceValue)
    {
        _phone = phone;
        _lowBalanceValue = lowBalanceValue;
    }

    public void Notify(int balance)
    {
        if (balance < _lowBalanceValue)
            Console.WriteLine(_phone + ": баланс меньше чем " + _lowBalanceValue);
    }
}
