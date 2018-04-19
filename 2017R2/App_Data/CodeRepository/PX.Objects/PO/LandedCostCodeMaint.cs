﻿using System;
using System.Collections.Generic;
using System.Text;
using PX.Data;
using PX.Objects.AP;

namespace PX.Objects.PO
{
	public class LandedCostCodeMaint : PXGraph<LandedCostCodeMaint, LandedCostCode>
	{
		public PXSelect<LandedCostCode> LandedCostCode;						
		
		public LandedCostCodeMaint()
		{

		}

		protected virtual void LandedCostCode_VendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e) 
		{
			LandedCostCode row = (LandedCostCode)e.Row;
			sender.SetDefaultExt<LandedCostCode.vendorLocationID>(e.Row);
			sender.SetDefaultExt<LandedCostCode.termsID>(e.Row);
			doCancel = true;
		}

		protected virtual void LandedCostCode_VendorLocationID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if(doCancel)
			{
				e.NewValue = ((LandedCostCode)e.Row).VendorLocationID;
				e.Cancel = true;
				doCancel = false;
			}
			
		}

		public virtual void ValidateAllocationApplication(PXCache sender, LandedCostCode row)
		{
			if ((row.ApplicationMethod == LandedCostApplicationMethod.FromAP || row.ApplicationMethod == LandedCostApplicationMethod.FromBoth) && row.AllocationMethod == LandedCostAllocationMethod.None)
				sender.RaiseExceptionHandling<LandedCostCode.allocationMethod>(row, row.AllocationMethod, new PXSetPropertyException(Messages.InvalidApplicationAllocationCombination, PXErrorLevel.Warning));
			else
				sender.RaiseExceptionHandling<LandedCostCode.allocationMethod>(row, row.AllocationMethod, null);
		}

		protected virtual void LandedCostCode_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			LandedCostCode row = (LandedCostCode)e.Row;
			if (row != null)
			{
				bool hasVendor = row.VendorID.HasValue;
				PXDefaultAttribute.SetPersistingCheck<LandedCostCode.vendorLocationID>(sender, e.Row, hasVendor ? PXPersistingCheck.Null : PXPersistingCheck.Nothing);
				PXUIFieldAttribute.SetRequired<LandedCostCode.vendorLocationID>(sender, hasVendor);
				PXUIFieldAttribute.SetEnabled<LandedCostCode.vendorLocationID>(sender,e.Row,hasVendor);
				sender.RaiseExceptionHandling<LandedCostCode.vendorID>(row, row.VendorID, null);
				if (hasVendor) 
				{
					Vendor vnd = PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>.Select(this, row.VendorID);
					if (vnd != null && vnd.LandedCostVendor == false)
					{
						sender.RaiseExceptionHandling<LandedCostCode.vendorID>(row, row.VendorID, new PXSetPropertyException(Messages.LCCodeUsesNonLCVendor, PXErrorLevel.Warning));
					}					
				}

				ValidateAllocationApplication(sender, row);
			}				
		}

		protected virtual void LandedCostCode_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
		{
			LandedCostTran costTran = PXSelect<LandedCostTran, Where<LandedCostTran.landedCostCodeID, Equal<Current<LandedCostCode.landedCostCodeID>>>>.SelectWindowed(this, 0, 1);
			if (costTran != null)
			{
				throw new PXException(Messages.ThisEntityNotBeDeletedBecauseItIsUsedIn, Messages.LandedCostCode, Messages.LandedCostTran);
			}

			APTran apTran = PXSelect<APTran, Where<APTran.landedCostCodeID, Equal<Current<LandedCostCode.landedCostCodeID>>>>.SelectWindowed(this, 0, 1);
			if (costTran != null)
			{
				throw new PXException(Messages.ThisEntityNotBeDeletedBecauseItIsUsedIn, Messages.LandedCostCode, AP.Messages.APTran);
			}
		}



		private bool doCancel = false;

	}

}
