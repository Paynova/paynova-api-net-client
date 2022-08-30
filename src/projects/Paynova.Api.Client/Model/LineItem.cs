using System;
using System.Collections.Generic;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public class LineItem
    {
        /// <summary>
        /// The id for this line item. This value must be unique per collection of line items.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// The article/product number for the item being sold.
        /// </summary>
        public string ArticleNumber { get; private set; }

        /// <summary>
        /// The name/description of the item being sold.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The description of the item being sold.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The URL on your website to the item being sold.
        /// </summary>
        public string ProductUrl { get; set; }

        /// <summary>
        /// The number of items being sold at this price.
        /// </summary>
        public decimal Quantity { get; private set; }

        /// <summary>
        /// The unit of measure of the product/service being sold.
        /// Examples: meters, pieces, st., ea.
        /// </summary>
        public string UnitMeasure { get; private set; }

        /// <summary>
        /// The price of each unit, excluding tax.
        /// </summary>
        public decimal UnitAmountExcludingTax { get; private set; }

        /// <summary>
        /// The tax/VAT percentage for the item being sold.
        /// Example: 25
        /// </summary>
        public decimal TaxPercent { get; private set; }

        /// <summary>
        /// The total tax/VAT amount charged for this line.
        /// </summary>
        public decimal TotalLineTaxAmount { get; private set; }

        /// <summary>
        /// The total amount charged for this line, including tax/VAT (quantity * unitAmountExcludingTax + calculated tax).
        /// </summary>
        public decimal TotalLineAmount { get; private set; }

        /// <summary>
        /// The line item group that this line item belongs to.
        /// </summary>
        public string LineItemGroupKey { get; set; }

        /// <summary>
        /// Travel information, if this line item is related to a booking-of-travel.
        /// </summary>
        public TravelData TravelData { get; set; }

        /// <summary>
        /// List of Components affiliate with Line Item
        /// </summary>
        public List<LineItemComponent> Components { get; set; }

        /// <summary>
        /// Creates a <see cref="LineItem"/> with all necessary parameters.
        /// </summary>
        /// <param name="id">The id for this line item. This value must be unique per collection of line items.</param>
        /// <param name="articleNumber">The article/product number for the item being sold.</param>
        /// <param name="name">The name/description of the item being sold.</param>
        /// <param name="description"></param>
        /// <param name="quantity">The number of items being sold at this price.</param>
        /// <param name="unitMeasure">The unit of measure of the product/service being sold. Examples: meters, pieces, st., ea.</param>
        /// <param name="unitAmountExcludingTax">The price of each unit, excluding tax.</param>
        /// <param name="taxPercent">The tax/VAT percentage for the item being sold. Example: 25</param>
        /// <param name="totalLineTaxAmount">The total tax/VAT amount charged for this line.</param>
        /// <param name="totalLineAmount">The total amount charged for this line, including tax/VAT (quantity * unitAmountExcludingTax * calculated tax).</param>
        /// <param name="lineItemComponents">Components that are affiliate with Lint Item object</param>
        public LineItem(int id, string articleNumber, string name, string description, decimal quantity, string unitMeasure, decimal unitAmountExcludingTax, decimal taxPercent, decimal totalLineTaxAmount, decimal totalLineAmount, List<LineItemComponent> lineItemComponents)
            : this(id.ToString(Runtime.Instance.NumberFormatProvider), articleNumber, name, description, quantity, unitMeasure, unitAmountExcludingTax, taxPercent, totalLineTaxAmount, totalLineAmount, lineItemComponents)
        { }

        /// <summary>
        /// Creates a <see cref="LineItem"/> with all necessary parameters.
        /// </summary>
        /// <param name="id">The id for this line item. This value must be unique per collection of line items.</param>
        /// <param name="articleNumber">The article/product number for the item being sold.</param>
        /// <param name="name">The name/description of the item being sold.</param>
        /// <param name="description"></param>
        /// <param name="quantity">The number of items being sold at this price.</param>
        /// <param name="unitMeasure">The unit of measure of the product/service being sold. Examples: meters, pieces, st., ea.</param>
        /// <param name="unitAmountExcludingTax">The price of each unit, excluding tax.</param>
        /// <param name="taxPercent">The tax/VAT percentage for the item being sold. Example: 25 or null in case that has a list of components</param>
        /// <param name="totalLineTaxAmount">The total tax/VAT amount charged for this line.</param>
        /// <param name="totalLineAmount">The total amount charged for this line, including tax/VAT (quantity * unitAmountExcludingTax * calculated tax).</param>
        /// <param name="lineItemComponents">Components that are affiliate with Lint Item object</param>
        public LineItem(string id, string articleNumber, string name, string description, decimal quantity, string unitMeasure, decimal unitAmountExcludingTax, decimal taxPercent, decimal totalLineTaxAmount, decimal totalLineAmount, List<LineItemComponent> lineItemComponents)
        {
            Ensure.That(id, "id").IsNotNullOrEmpty();
            Ensure.That(articleNumber, "articleNumber").IsNotNullOrEmpty();
            Ensure.That(name, "name").IsNotNullOrEmpty();
            Ensure.That(description, "description").IsNotNullOrEmpty();
            Ensure.That(quantity, "quantity").IsGt(0);
            Ensure.That(unitMeasure, "unitMeasure").IsNotNullOrEmpty();
            if (unitAmountExcludingTax < 0)
            {
                Ensure.That(unitAmountExcludingTax, "unitAmountExcludingTax").IsLt(0);
            }
            else
            {
                Ensure.That(unitAmountExcludingTax, "unitAmountExcludingTax").IsGte(0);
            }

            Ensure.That(taxPercent, "taxPercent").IsGte(0);
            if (totalLineTaxAmount < 0)
            {
                Ensure.That(totalLineTaxAmount, "totalLineTaxAmount").IsLt(0);
            }
            else if (totalLineTaxAmount == 0)
            {
                Ensure.That(totalLineTaxAmount, "totalLineTaxAmount").IsGte(0);
            }
            else
            {
                Ensure.That(totalLineTaxAmount, "totalLineTaxAmount").IsGt(0);
            }

            if (totalLineAmount < 0)
            {
                Ensure.That(totalLineAmount, "totalLineAmount").IsLt(0);
            }
            else
            {
                Ensure.That(totalLineAmount, "totalLineAmount").IsGt(0);
            }

            Id = id;
            ArticleNumber = articleNumber;
            Name = name;
            Description = description;
            Quantity = quantity;
            UnitMeasure = unitMeasure;
            UnitAmountExcludingTax = unitAmountExcludingTax;
            TaxPercent = taxPercent > 0 && taxPercent < 1 ? taxPercent * 100 : taxPercent;
            TotalLineTaxAmount = totalLineTaxAmount;
            TotalLineAmount = totalLineAmount;
            Components = lineItemComponents;
        }

        /// <summary>
        /// Sets <see cref="LineItemGroupKey"/> and returns the
        /// current <see cref="LineItem"/> for fluent config.
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual LineItem WithLineItemGroupKey(Func<LineItemGroupKeys, string> selector)
        {
            LineItemGroupKey = selector(LineItemGroupKeys.Instance);

            return this;
        }
    }
}