namespace Domain.Model.ENUM;

public enum TransactionState
{
    Received = 0,
    Valid = 10,
    Processing = 20,
    Completed = 30,
    Invalid = -10,
    Failed = -20,
}