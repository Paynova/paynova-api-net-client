namespace Paynova.Api.Client.Model
{
    public class TravelTicket
    {
        /// <summary>
        /// The carrier's service id for this ticket.
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// The carrier's ticket number.
        /// </summary>
        public string TicketNumber { get; set; }

        /// <summary>
        /// Indicates whether or not this ticket is refundable.
        /// </summary>
        public bool IsRefundable { get; set; }

        /// <summary>
        /// Indicates whether or not this ticket is re-bookable.
        /// </summary>
        public bool IsRebookable { get; set; }

        /// <summary>
        /// The passenger travelling on this ticket.
        /// </summary>
        public TravelPassenger Passenger { get; set; }

        public TravelTicket(bool isRefundable, bool isRebookable)
        {
            IsRefundable = isRefundable;
            IsRebookable = isRebookable;
        }
    }
}