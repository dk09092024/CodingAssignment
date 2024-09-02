namespace Infrastructure.Model.ENUM;

public enum TransactionState
{
    Recived = 0,
    Validated = 10,
    Processing = 20,
    Completed = 30,
    Invalid = -10,
    Failed = -20,
}