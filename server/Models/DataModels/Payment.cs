using System;

namespace Charity.Models.DataModels
{
    public class Payment
    {
        public Payment()
        {
        }

        public Payment(int id, int userId, int charityId, long amount, DateTime paymentDateTime, string desceiption)
            : this()
        {
            this.Id = id;
            this.UserId = userId;
            this.CharityId = charityId;
            this.Amount = amount;
            this.PaymentDateTime = paymentDateTime;
            this.Desceiption = desceiption;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public UserModels.User User { get; set; }

        public int CharityId { get; set; }

        public Charity Charity { get; set; }

        public long Amount { get; set; }

        public DateTime PaymentDateTime { get; set; }

        public string Desceiption { get; set; }
    }
}
