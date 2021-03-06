﻿using Invoicer.Models;
using Invoicer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Invoicer.Models.Enums;

namespace Invoicer.Controllers
{
    public class FuelInvoiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FuelInvoiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {
            var vm = TempData.Peek("invoice");        
            return View("Create", vm);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(FuelInvoiceFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.FuelTypes = _unitOfWork.FuelTypes.GetFuelTypes();
                viewModel.CurrencyTypes = _unitOfWork.CurrencyTypes.GetCurrencyTypes();
                viewModel.GasStations = _unitOfWork.GasStations.GetGasStations();
                return View("Create", viewModel);
            }

            var fuelInvoice = new FuelInvoice
            {
                InvoiceTypeId = (int)InvoiceTypeEnum.FuelInvoice,
                InvoiceNumber = viewModel.InvoiceNumber,
                FuelTypeId = viewModel.FuelType,
                RefuelingDate = DateTime.Parse(viewModel.RefuelingDate),
                Amount = viewModel.Amount.Value,
                CurrencyId = viewModel.CurrencyType,
                UnitPrice = viewModel.UnitPrice.Value,
                Quantity = viewModel.Quantity.Value,
                GasStationId = viewModel.GasStation,
                MeterStatus = viewModel.MeterStatus.Value,
                UserId = User.Identity.GetUserId()                
            };

            _unitOfWork.FuelInvoices.Add(fuelInvoice);
            _unitOfWork.Complete();

            return RedirectToAction("Index", "Manage"); ;
        }
    }
}