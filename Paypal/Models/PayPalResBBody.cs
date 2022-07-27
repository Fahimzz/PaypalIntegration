using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.Models
{
    public class PayPalResBBody
    {
        public Amount Amounts { get; set; }
        public Breakdown Breakdowns { get; set; }
        public Item item { get; set; }
        public ItemTotal itemTotal { get; set; }
        public Link link { get; set; }
        public Payee payee { get; set; }
        public PurchaseUnit purchaseUnit { get; set; }
        public Root root { get; set; }

        public UnitAmount unitAmount { get; set; }
        public class Amount
        {
            public string currency_code { get; set; }
            public string value { get; set; }
            public Breakdown breakdown { get; set; }
        }

        public class Breakdown
        {
            public ItemTotal item_total { get; set; }
        }

        public class Item
        {
            public string name { get; set; }
            public UnitAmount unit_amount { get; set; }
            public string quantity { get; set; }
            public string description { get; set; }
        }

        public class ItemTotal
        {
            public string currency_code { get; set; }
            public string value { get; set; }
        }

        public class Link
        {
            public string href { get; set; }
            public string rel { get; set; }
            public string method { get; set; }
        }

        public class Payee
        {
            public string email_address { get; set; }
            public string merchant_id { get; set; }
        }

        public class PurchaseUnit
        {
            public string reference_id { get; set; }
            public Amount amount { get; set; }
            public Payee payee { get; set; }
            public List<Item> items { get; set; }
        }

        public class Root
        {
            public string id { get; set; }
            public string intent { get; set; }
            public string status { get; set; }
            public List<PurchaseUnit> purchase_units { get; set; }
            public DateTime create_time { get; set; }
            public List<Link> links { get; set; }
        }

        public class UnitAmount
        {
            public string currency_code { get; set; }
            public string value { get; set; }
        }
    }
}
