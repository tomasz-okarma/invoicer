﻿using System;

namespace Invoicer.Models
{
    public class FuelInvoice : Invoice
    {
        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }
        
        public int MeterStatus { get; set; }

        public int FuelTypeId { get; set; }

        public DateTime RefuelingDate { get; set; }

        public int GasStationId { get; set; }

        public GasStation GasStation { get; set; }

        public FuelType FuelType { get; set; }
    }
}