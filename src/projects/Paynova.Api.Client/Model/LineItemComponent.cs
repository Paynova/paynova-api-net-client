using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public class LineItemComponent
    {
        /// <summary>
        /// The id for this component. This value must be unique per collection of components.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// The article/product number for the item being sold.
        /// </summary>
        public string ArticleNumber { get; private set; }

        /// <summary>
        /// The name/description of the component being sold.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The description of the component being sold.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The URL on your website to the component being sold.
        /// </summary>
        public string ProductUrl { get; private set; }

        /// <summary>
        /// The number of components being sold at this price.
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
        /// The tax/VAT percentage for the component being sold.
        /// Example: 25
        /// </summary>
        public decimal TaxPercent { get; private set; }

        /// <summary>
        /// The total tax/VAT amount charged for this component.
        /// </summary>
        public decimal TotalComponentTaxAmount { get; private set; }

        /// <summary>
        /// The total amount charged for this component, including tax/VAT (quantity * unitAmountExcludingTax * calculated tax).
        /// </summary>
        public decimal TotalComponentAmount { get; private set; }

        /// <summary>
        /// Creates a <see cref="LineItemComponent"/> with all necessary parameters.
        /// </summary>
        /// <param name="id">The id for this line item. This value must be unique per collection of line items.</param>
        /// <param name="articleNumber">The article/product number for the item being sold.</param>
        /// <param name="name">The name/description of the item being sold.</param>
        /// <param name="description">The description is set when an item is bought</param>
        /// <param name="quantity">The number of items being sold at this price.</param>
        /// <param name="unitMeasure">The unit of measure of the product/service being sold. Examples: meters, pieces, st., ea.</param>
        /// <param name="unitAmountExcludingTax">The price of each unit, excluding tax.</param>
        /// <param name="taxPercent">The tax/VAT percentage for the item being sold. Example: 25</param>
        /// <param name="totalComponentTaxAmount">The total tax/VAT amount charged for this line.</param>
        /// <param name="totalComponentAmount">The total amount charged for this line, including tax/VAT (quantity * unitAmountExcludingTax * calculated tax).</param>
        public LineItemComponent(int id, string articleNumber, string name, string description, decimal quantity, string unitMeasure, decimal unitAmountExcludingTax, decimal taxPercent, decimal totalComponentTaxAmount, decimal totalComponentAmount)
            : this(id.ToString(Runtime.Instance.NumberFormatProvider), articleNumber, name, description, quantity, unitMeasure, unitAmountExcludingTax, taxPercent, totalComponentTaxAmount, totalComponentAmount)
        {
        }

        /// <summary>
        /// Creates a <see cref="LineItemComponent"/> with all necessary parameters.
        /// </summary>
        /// <param name="id">The id for this line item. This value must be unique per collection of line items.</param>
        /// <param name="articleNumber">The article/product number for the item being sold.</param>
        /// <param name="name">The name/description of the item being sold.</param>
        /// <param name="description">The description is set when an item is bought</param>
        /// <param name="quantity">The number of items being sold at this price.</param>
        /// <param name="unitMeasure">The unit of measure of the product/service being sold. Examples: meters, pieces, st., ea.</param>
        /// <param name="unitAmountExcludingTax">The price of each unit, excluding tax.</param>
        /// <param name="taxPercent">The tax/VAT percentage for the item being sold. Example: 25</param>
        /// <param name="totalComponentTaxAmount">The total tax/VAT amount charged for this line.</param>
        /// <param name="totalComponentAmount">The total amount charged for this line, including tax/VAT (quantity * unitAmountExcludingTax * calculated tax).</param>
        public LineItemComponent(string id, string articleNumber, string name, string description, decimal quantity, string unitMeasure, decimal unitAmountExcludingTax,
            decimal taxPercent, decimal totalComponentTaxAmount, decimal totalComponentAmount)
        {
            Ensure.That(id, "id").IsNotNullOrEmpty();
            Ensure.That(articleNumber, "articleNumber").IsNotNullOrEmpty();
            Ensure.That(name, "name").IsNotNullOrEmpty();
            Ensure.That(description, "description").IsNotNullOrEmpty();
            Ensure.That(quantity, "quantity").IsGt(0);
            Ensure.That(unitMeasure, "unitMeasure").IsNotNullOrEmpty();
            Ensure.That(unitAmountExcludingTax, "unitAmountExcludingTax").IsGte(0);
            Ensure.That(taxPercent, "taxPercent").IsGte(0);
            Ensure.That(totalComponentTaxAmount, "totalComponentTaxAmount").IsGte(0);
            Ensure.That(totalComponentAmount, "totalComponentAmount").IsGte(0);

            Id = id;
            ArticleNumber = articleNumber;
            Name = name;
            Description = description;
            Quantity = quantity;
            UnitMeasure = unitMeasure;
            UnitAmountExcludingTax = unitAmountExcludingTax;
            TaxPercent = taxPercent > 0 && taxPercent < 1 ? taxPercent * 100 : taxPercent;
            TotalComponentTaxAmount = totalComponentTaxAmount;
            TotalComponentAmount = totalComponentAmount;
        }
    }
}