using BankAccount.Tests;
using MvcStarterProject.Business;
using MvcStarterProject.Tests.TestDataBuilders;
using NUnit.Framework;
using Should;

namespace MvcStarterProject.Tests.UnitTests.Business
{
    public class When_calculating_the_tax_for_Ohio : Specification
    {
        private decimal _taxAmount;
        private Order _order;

        protected override void Establish_context()
        {
            _order = OrderBuilder.CreateOrderWithProductsForState("OH");
        }

        protected override void Because_of()
        {
            _taxAmount = new TaxCalculator().CalculateTax(_order);
        }

        [Test]
        public void Should_multiply_the_cost_of_the_products_by_7_percent_and_round_to_two_decimal_places()
        {
            _taxAmount.ShouldEqual(0.32m); // 4.5 * .07
        }
    }

    public class When_calculating_the_tax_for_Michigan : Specification
    {
        private decimal _taxAmount;
        private Order _order;

        protected override void Establish_context()
        {
            _order = OrderBuilder.CreateOrderWithProductsForState("MI");
        }

        protected override void Because_of()
        {
            _taxAmount = new TaxCalculator().CalculateTax(_order);
        }

        [Test]
        public void Should_multiply_the_cost_of_the_products_by_6_point_5_percent_and_round_to_two_decimal_places()
        {
            _taxAmount.ShouldEqual(0.29m); // 4.5 * .065
        }
    }

    public class When_calculating_the_tax_for_states_that_have_no_tax : Specification
    {
        [Test]
        public void Should_return_0_for_tax()
        {
            // Including all US Territories just in case.  Also including null and an empty string.
            var otherStateCodes = new[]
                                      {
                                          "AL", "AK", "AS", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FM", "FL", "GA",
                                          "GU", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MH", "MD", "MA",
                                          "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND",
                                          "MP", "OK", "OR", "PW", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT",
                                          "VT", "VI", "VA", "WA", "WV", "WI", "WY", "AE", "AA", "AP", "", null
                                      };

            foreach (var stateCode in otherStateCodes)
            {
                var order = OrderBuilder.CreateOrderWithProductsForState(stateCode);

                var taxAmount = new TaxCalculator().CalculateTax(order);

                taxAmount.ShouldEqual(0);
            }
        }
    }
}