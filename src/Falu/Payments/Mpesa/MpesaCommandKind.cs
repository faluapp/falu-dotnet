namespace Falu.Payments.Mpesa
{
    /// <summary>
    /// The kind of command to be executed
    /// </summary>
    public enum MpesaCommandKind
    {
        /// <summary>
        /// Reversal for an erroneous C2B transaction.
        /// </summary>
        TransactionReversal,

        /// <summary>
        /// Used to send money from an employer to employees e.g. salaries
        /// </summary>
        SalaryPayment,

        /// <summary>
        /// Used to send money from business to customer e.g. refunds
        /// </summary>
        BusinessPayment,

        /// <summary>
        /// Used to send money when promotions take place e.g. raffle winners
        /// </summary>
        PromotionPayment,

        /// <summary>
        /// Used to check the balance in a paybill/buy goods account (includes utility, MMF, Merchant, Charges paid account).
        /// </summary>
        AccountBalance,

        /// <summary>
        /// Used to simulate a transaction taking place in the case of C2B Simulate Transaction
        /// or to initiate a transaction on behalf of the customer (STK Push).
        /// </summary>
        CustomerPayBillOnline,

        /// <summary>
        /// Used to query the details of a transaction.
        /// </summary>
        TransactionStatusQuery,

        /// <summary>
        /// Similar to STK push, uses M-Pesa PIN as a service.
        /// </summary>
        CheckIdentity,

        /// <summary>
        /// Sending funds from one paybill to another paybill
        /// </summary>
        BusinessPayBill,

        /// <summary>
        /// Sending funds from buy goods to another buy goods.
        /// </summary>
        BusinessBuyGoods,

        /// <summary>
        /// Transfer of funds from utility to MMF account.
        /// </summary>
        DisburseFundsToBusiness,

        /// <summary>
        /// Transferring funds from one paybills MMF to another paybills MMF account.
        /// </summary>
        BusinessToBusinessTransfer,

        /// <summary>
        /// Transferring funds from paybills MMF to another paybills utility account.
        /// </summary>
        BusinessTransferFromMMFToUtility,
    }
}
