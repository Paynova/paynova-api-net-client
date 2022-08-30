using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public class FinalizedLineItemUpdateOperation
    {
        /// <summary>
        /// The operationtype defines an valid operation. RemoveLineItem or AddLineItem, components will be supported in the future
        /// </summary>
        public FinalizedLineItemUpdateOperationsType OperationType { get; set; }

        /// <summary>
        /// The id for this line item. This value must be unique per collection of line items.
        /// </summary>
        public string LineItemId { get; set; }

        /// <summary>
        /// In this case the object contains LineItem object.
        /// </summary>
        public LineItem LineItem { get; set; }
    }
}