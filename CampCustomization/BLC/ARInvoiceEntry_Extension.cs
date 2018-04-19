using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using PX.Common;
using PX.Objects.Common;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.CR;
using PX.Objects.TX;
using PX.Objects.IN;
using PX.Objects.BQLConstants;
using PX.Objects.EP;
using PX.Objects.SO;
using PX.Objects.DR;
using Avalara.AvaTax.Adapter;
using Avalara.AvaTax.Adapter.TaxService;
using SOInvoice = PX.Objects.SO.SOInvoice;
using SOInvoiceEntry = PX.Objects.SO.SOInvoiceEntry;
using AvaAddress = Avalara.AvaTax.Adapter.AddressService;
using AvaMessage = Avalara.AvaTax.Adapter.Message;
using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Objects.GL.Reclassification.UI;
using PX.Objects.AR.BQL;
using PX.Objects.Common.Extensions;
using PX.Objects.PM;
using System.Text;
using PX.Objects;
using PX.Objects.AR;


namespace PX.Objects.AR
{
    public class ARInvoiceEntry_Extension : PXGraphExtension<ARInvoiceEntry>
    {
        #region Event Handlers

        protected void ARTran_DRTermStartDate_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e, PXFieldDefaulting InvokeBaseHandler)
        {
            if (InvokeBaseHandler != null)
                InvokeBaseHandler(cache, e);
            var line = e.Row as ARTran;
            var currentInvoice = Base.Document.Current.GetExtension<ARRegisterExt>();


            if (line != null && line.RequiresTerms == true)
            {
                e.NewValue = currentInvoice.UsrGenerateDate;
            }

        }
        protected void ARTran_DRTermEndDate_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
        {

            var line = e.Row as ARTran;
            var currentInvoice = Base.Document.Current.GetExtension<ARRegisterExt>();


            if (line != null && line.RequiresTerms == true)
            {
                e.NewValue = currentInvoice.UsrContractEndDate;
            }

        }




        #endregion
    }
}