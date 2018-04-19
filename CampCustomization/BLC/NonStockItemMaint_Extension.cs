using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.GL;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.SM;
using PX.Web.UI;
using PX.Objects.RUTROT;
using CRLocation = PX.Objects.CR.Standalone.Location;
using ItemStats = PX.Objects.IN.Overrides.INDocumentRelease.ItemStats;
using PX.Objects;
using PX.Objects.IN;
using CAMPCustomization;

namespace PX.Objects.IN
{
    public class NonStockItemMaint_Extension : PXGraphExtension<NonStockItemMaint>
    {
        #region Event Handlers
        protected void InventoryItem_RowSelected(PXCache sender, PXRowSelectedEventArgs e, PXRowSelected del)
        {
            // Event handler body before the base BLC event handler collection 
            if (del != null)
                del(sender, e);
            // Event handler body after the base BLC event handler collection
            if (e.Row != null)
            {
                InventoryItemExt ext = PXCache<InventoryItem>.GetExtension<InventoryItemExt>((InventoryItem)e.Row);
                if (ext != null)
                {
                    if (ext.UsrIsAircraft != null)
                    {
                        PXUIFieldAttribute.SetEnabled<InventoryItemExt.usrTapeModelID>(sender, e.Row, (bool)ext.UsrIsAircraft);
                    }
                    else
                    {
                        PXUIFieldAttribute.SetEnabled<InventoryItemExt.usrTapeModelID>(sender, e.Row, false);
                    }
                }
            }
        }

        //Requires a model if IsAircraft is checked in the Inventory Items screen
        protected void InventoryItem_RowPersisting(PXCache sender, PXRowPersistingEventArgs e, PXRowPersisting del)
        {
            var row = (InventoryItem)e.Row;
            if (row != null)
            {
                InventoryItemExt ext = PXCache<InventoryItem>.GetExtension<InventoryItemExt>(row);
                if (ext != null)
                {
                    if ((bool)ext.UsrIsAircraft && ext.UsrTapeModelID == null)
                    {
                        throw new PXSetPropertyException<InventoryItemExt.usrTapeModelID>(
                                    "Enter a Model.", PXErrorLevel.Error);
                    }
                }
            }

            del(sender, e);
        }


        //Defaults the Manufacturer from the model selection in the Inventory Items screen
        protected virtual void InventoryItem_UsrTapeModelID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            var row = (InventoryItem)e.Row;
            if (row != null)
            {
                InventoryItemExt inventoryEX = PXCache<InventoryItem>.GetExtension<InventoryItemExt>(row);
                if (inventoryEX != null)
                {
                    PXCache hCache = Base.ItemSettings.Cache;
                    InventoryItem hdr = Base.ItemSettings.Current;
                    CAMPTapeModel model = (CAMPTapeModel)PXSelectorAttribute.Select<InventoryItemExt.usrTapeModelID>(hCache, hdr);
                    if (model != null)
                    {
                        inventoryEX.UsrManufacturerID = model.ManufacturerID;
                    }
                    else
                    {
                        inventoryEX.UsrManufacturerID = null;
                    }
                }
            }
        }

        //Validates the LOB to the first 3 digits of the InventoryID
        /* Must agree to first three characters of the inventory Id */
        protected virtual void InventoryItem_UsrLineofBusinessID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
        {
            var row = (InventoryItem)e.Row;
            if (row != null && e.NewValue != null)
            {
                CAMPLineofBusiness lob = PXSelect<CAMPLineofBusiness, Where<CAMPLineofBusiness.lineofBusinessID,
                                                    Equal<Required<CAMPLineofBusiness.lineofBusinessID>>>>.Select(Base, (int)e.NewValue);
                if (lob != null)
                {
                    if (String.Compare(lob.LineofBusinessCD, row.InventoryCD.Substring(0, 3), StringComparison.OrdinalIgnoreCase) != 0)
                        throw new PXSetPropertyException<InventoryItemExt.usrLineofBusinessID>(
                                          "Invalid Line of Business for Inventory ID.", PXErrorLevel.Error);
                }
            }
        }

        #endregion
    }
}