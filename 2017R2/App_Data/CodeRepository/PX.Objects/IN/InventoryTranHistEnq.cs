using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using PX.SM;
using PX.Data;
using PX.Objects.BQLConstants;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;

namespace PX.Objects.IN
{
    #region FilterDAC

    [Serializable]
    public partial class InventoryTranHistEnqFilter : PX.Data.IBqlTable
    {
        #region StartDate
        public abstract class startDate : PX.Data.IBqlField
        {
        }
        protected DateTime? _StartDate;
        [PXDBDate()]
        [PXUIField(DisplayName = "Start Date")]
        public virtual DateTime? StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                this._StartDate = value;
            }
        }
        #endregion
        #region EndDate
        public abstract class endDate : PX.Data.IBqlField
        {
        }
        protected DateTime? _EndDate;
        [PXDBDate()]
        //[PXDefault(typeof(AccessInfo.businessDate))]
        [PXUIField(DisplayName = "End Date")]
        public virtual DateTime? EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this._EndDate = value;
            }
        }
        #endregion
        #region InventoryID
        public abstract class inventoryID : PX.Data.IBqlField
        {
        }
        protected Int32? _InventoryID;
        [PXDefault()]
        [AnyInventory(typeof(Search<InventoryItem.inventoryID, Where<InventoryItem.stkItem, NotEqual<boolFalse>, And<Where<Match<Current<AccessInfo.userName>>>>>>), typeof(InventoryItem.inventoryCD), typeof(InventoryItem.descr))] // ??? zzz stock / nonstock ?
        public virtual Int32? InventoryID
        {
            get
            {
                return this._InventoryID;
            }
            set
            {
                this._InventoryID = value;
            }
        }
        #endregion

        #region SubItemCD
        public abstract class subItemCD : PX.Data.IBqlField
        {
        }
        protected String _SubItemCD;
        [SubItemRawExt(typeof(InventoryTranHistEnqFilter.inventoryID), DisplayName = "Subitem")]
        public virtual String SubItemCD
        {
            get
            {
                return this._SubItemCD;
            }
            set
            {
                this._SubItemCD = value;
            }
        }
        #endregion
        #region SubItemCD Wildcard
        public abstract class subItemCDWildcard : PX.Data.IBqlField { };
        [PXDBString(30, IsUnicode = true)]
        public virtual String SubItemCDWildcard
        {
            get
            {
                //return SubItemCDUtils.CreateSubItemCDWildcard(this._SubItemCD);
                return SubCDUtils.CreateSubCDWildcard(this._SubItemCD, SubItemAttribute.DimensionName);
            }
        }
        #endregion
        #region SiteID
        public abstract class siteID : PX.Data.IBqlField
        {
        }
        protected Int32? _SiteID;
        //        [Site(Visibility = PXUIVisibility.Visible)]
        [Site(DescriptionField = typeof(INSite.descr), Required = false, DisplayName = "Warehouse")]
        //        [PXDefault()]
        public virtual Int32? SiteID
        {
            get
            {
                return this._SiteID;
            }
            set
            {
                this._SiteID = value;
            }
        }
        #endregion
        #region LocationID
        public abstract class locationID : PX.Data.IBqlField
        {
        }
        protected Int32? _LocationID;
        [Location(typeof(InventoryTranHistEnqFilter.siteID), Visibility = PXUIVisibility.Visible, KeepEntry = false, DescriptionField = typeof(INLocation.descr), DisplayName = "Location")]
        public virtual Int32? LocationID
        {
            get
            {
                return this._LocationID;
            }
            set
            {
                this._LocationID = value;
            }
        }
        #endregion
        #region LotSerialNbr
        public abstract class lotSerialNbr : PX.Data.IBqlField
        {
        }
        protected String _LotSerialNbr;
		[LotSerialNbr]
		public virtual String LotSerialNbr
        {
            get
            {
                return this._LotSerialNbr;
            }
            set
            {
                this._LotSerialNbr = value;
            }
        }
        #endregion
        #region LotSerialNbrWildcard
        public abstract class lotSerialNbrWildcard : PX.Data.IBqlField { };
        [PXDBString(100, IsUnicode = true)]
        public virtual String LotSerialNbrWildcard
        {
            get
            {
                return PXDatabase.Provider.SqlDialect.WildcardAnything + this._LotSerialNbr + PXDatabase.Provider.SqlDialect.WildcardAnything;
            }
        }
        #endregion
        #region ByFinancialPeriod (commented)
        /*
        public abstract class byFinancialPeriod : PX.Data.IBqlField
        {
        }
        protected bool? _ByFinancialPeriod;
        [PXDBBool()]
        [PXDefault()]
        [PXUIField(DisplayName = "By Financial Period")]
        public virtual bool? ByFinancialPeriod
        {
            get
            {
                return this._ByFinancialPeriod;
            }
            set
            {
                this._ByFinancialPeriod = value;
            }
        }
*/
        #endregion
        #region SummaryByDay
        public abstract class summaryByDay : PX.Data.IBqlField
        {
        }
        protected bool? _SummaryByDay;
        [PXDBBool()]
        [PXDefault()]
        [PXUIField(DisplayName = "Summary By Day")]
        public virtual bool? SummaryByDay
        {
            get
            {
                return this._SummaryByDay;
            }
            set
            {
                this._SummaryByDay = value;
            }
        }
        #endregion
        #region IncludeUnreleased
        public abstract class includeUnreleased : PX.Data.IBqlField
        {
        }
        protected bool? _IncludeUnreleased;
        [PXDBBool()]
        [PXDefault()]
        [PXUIField(DisplayName = "Include Unreleased", Visibility = PXUIVisibility.Visible)]
        public virtual bool? IncludeUnreleased
        {
            get
            {
                return this._IncludeUnreleased;
            }
            set
            {
                this._IncludeUnreleased = value;
            }
        }
        #endregion
        #region ShowAdjUnitCost
        public abstract class showAdjUnitCost : PX.Data.IBqlField
        {
        }
        protected bool? _ShowAdjUnitCost;
        [PXDBBool()]
        [PXDefault()]
        [PXUIField(DisplayName = "Include Landed Cost in Unit Cost", Visibility = PXUIVisibility.Visible)]
        public virtual bool? ShowAdjUnitCost
        {
            get
            {
                return this._ShowAdjUnitCost;
            }
            set
            {
                this._ShowAdjUnitCost = value;
            }
        }
        #endregion
    }




#endregion

    #region ResultSet
    [Serializable]
    public partial class InventoryTranHistEnqResult : PX.Data.IBqlTable
    {

        #region GridLineNbr
        // just for sorting in gris
        public abstract class gridLineNbr : PX.Data.IBqlField { }
        protected Int32? _GridLineNbr;
        [PXDBInt(IsKey = true)]
        [PXUIField(DisplayName = "Grid Line Nbr.", Visibility = PXUIVisibility.SelectorVisible, Visible = false)]
        public virtual Int32? GridLineNbr
        {
            get
            {
                return this._GridLineNbr;
            }
            set
            {
                this._GridLineNbr = value;
            }
        }
        #endregion

        #region InventoryID
        public abstract class inventoryID : PX.Data.IBqlField { }
        protected Int32? _InventoryID;
        [Inventory(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Inventory ID")]
        public virtual Int32? InventoryID
        {
            get
            {
                return this._InventoryID;
            }
            set
            {
                this._InventoryID = value;
            }
        }
        #endregion
        #region TranDate
        public abstract class tranDate : PX.Data.IBqlField
        {
        }
        protected DateTime? _TranDate;
        [PXDBDate()]
        [PXUIField(DisplayName = "Date")]
        public virtual DateTime? TranDate
        {
            get
            {
                return this._TranDate;
            }
            set
            {
                this._TranDate = value;
            }
        }
        #endregion
        #region TranType
        public abstract class tranType : PX.Data.IBqlField { }
        protected String _TranType;
        [PXString(3)] // ???
        [INTranType.List()]
        [PXUIField(DisplayName = "Tran. Type", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual String TranType
        {
            get
            {
                return this._TranType;
            }
            set
            {
                this._TranType = value;
            }
        }
        #endregion
        #region DocType
        public abstract class docType : PX.Data.IBqlField
        {
        }
        protected String _DocType;
		//[PXDBString(1, IsKey = true, IsFixed = true)]
		[PXDBString(1, IsFixed = true)]
        [PXUIField(DisplayName = "Doc Type", Visibility = PXUIVisibility.Visible, Visible = false)]
        public virtual String DocType
        {
            get
            {
                return this._DocType;
            }
            set
            {
                this._DocType = value;
            }
        }
        #endregion
        #region DocRefNbr
        public abstract class docRefNbr : PX.Data.IBqlField
        {
        }
        protected String _DocRefNbr;
        [PXString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(Search<INRegister.refNbr, Where<INRegister.docType, Equal<Current<docType>>>>))]
        public virtual String DocRefNbr
        {
            get
            {
                return this._DocRefNbr;
            }
            set
            {
                this._DocRefNbr = value;
            }
        }
        #endregion

        #region SubItemID
        public abstract class subItemID : PX.Data.IBqlField
        {
        }
        protected Int32? _SubItemID;
        [SubItem(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Subitem")]
        public virtual Int32? SubItemID
        {
            get
            {
                return this._SubItemID;
            }
            set
            {
                this._SubItemID = value;
            }
        }
        #endregion
        #region SiteId
        public abstract class siteID : PX.Data.IBqlField { }
        protected Int32? _SiteID;
        //[PXDBInt(IsKey = true)] //???
        [Site(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Warehouse")]
        public virtual Int32? SiteID
        {
            get
            {
                return this._SiteID;
            }
            set
            {
                this._SiteID = value;
            }
        }
        #endregion
        #region LocationID
        public abstract class locationID : PX.Data.IBqlField { }
        protected Int32? _LocationID;
        //            [PXDBInt(IsKey = true)] //???
        [Location(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Location")]
        public virtual Int32? LocationID
        {
            get
            {
                return this._LocationID;
            }
            set
            {
                this._LocationID = value;
            }
        }
        #endregion
        #region LotSerialNbr
        public abstract class lotSerialNbr : PX.Data.IBqlField
        {
        }
        protected String _LotSerialNbr;
		//[PXDBString(100, IsUnicode = true, IsKey = true, InputMask = "")]
		[PXDBString(100, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Lot/Serial Number", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual String LotSerialNbr
        {
            get
            {
                return this._LotSerialNbr;
            }
            set
            {
                this._LotSerialNbr = value;
            }
        }
        #endregion
        #region FinPerNbr
        public abstract class finPerNbr : PX.Data.IBqlField { };
        protected String _FinPerNbr;
        [GL.FinPeriodID()]
        [PXUIField(DisplayName = "Fin. Period", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual String FinPerNbr
        {
            get
            {
                return this._FinPerNbr;
            }
            set
            {
                this._FinPerNbr = value;
            }
        }
        #endregion
        #region TranPerNbr
        public abstract class tranPerNbr : PX.Data.IBqlField { };
        protected String _TranPerNbr;
        [GL.FinPeriodID()]
        [PXUIField(DisplayName = "Tran. Period", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual String TranPerNbr
        {
            get
            {
                return this._TranPerNbr;
            }
            set
            {
                this._TranPerNbr = value;
            }
        }
        #endregion

        #region Released
        public abstract class released : PX.Data.IBqlField { }
        protected bool? _Released = false;
        [PXBool]
        [PXUIField(DisplayName = "Released", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual bool? Released
        {
            get
            {
                return this._Released;
            }
            set
            {
                this._Released = value;
            }
        }
        #endregion

        //  Qtys in stock UOM here
        #region BegQty
        public abstract class begQty : PX.Data.IBqlField
        {
        }
        protected Decimal? _BegQty;
        [PXDBQuantity()]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Beginning Qty.", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Decimal? BegQty
        {
            get
            {
                return this._BegQty;
            }
            set
            {
                this._BegQty = value;
            }
        }
        #endregion
        #region QtyIn
        public abstract class qtyIn : PX.Data.IBqlField
        {
        }
        protected Decimal? _QtyIn;
        [PXDBQuantity()]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Qty. In", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Decimal? QtyIn
        {
            get
            {
                return this._QtyIn;
            }
            set
            {
                this._QtyIn = value;
            }
        }
        #endregion
        #region QtyOut
        public abstract class qtyOut : PX.Data.IBqlField
        {
        }
        protected Decimal? _QtyOut;
        [PXDBQuantity()]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Qty. Out", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Decimal? QtyOut
        {
            get
            {
                return this._QtyOut;
            }
            set
            {
                this._QtyOut = value;
            }
        }
        #endregion
        #region EndQty
        public abstract class endQty : PX.Data.IBqlField
        {
        }
        protected Decimal? _EndQty;
        [PXDBQuantity()]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Ending Qty.", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Decimal? EndQty
        {
            get
            {
                return this._EndQty;
            }
            set
            {
                this._EndQty = value;
            }
        }
        #endregion

        #region UOM (commented)
        /*
        public abstract class uOM : PX.Data.IBqlField
        {
        }
        protected String _UOM;
        //[INUnit(typeof(INTranSplit.inventoryID))]
        //[PXDefault(typeof(INTran.uOM))]
        [PXUIField(DisplayName = "UOM", Enabled = false)]
        public virtual String UOM
        {
            get
            {
                return this._UOM;
            }
            set
            {
                this._UOM = value;
            }
        }
*/
        #endregion
        #region BaseQty (commented)
        /*
        public abstract class baseQty : PX.Data.IBqlField
        {
        }
        protected Decimal? _BaseQty;
        [PXDBQuantity()]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "BaseQty", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Decimal? BaseQty
        {
            get
            {
                return this._BaseQty;
            }
            set
            {
                this._BaseQty = value;
            }
        }
*/
        #endregion

        #region UnitCost
        public abstract class unitCost : PX.Data.IBqlField
        {
        }
        protected Decimal? _UnitCost;
		[PXDBPriceCost()]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Unit Cost", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Decimal? UnitCost
        {
            get
            {
                return this._UnitCost;
            }
            set
            {
                this._UnitCost = value;
            }
        }
        #endregion

        // not used currently :
        #region ExtCost
        public abstract class extCost : PX.Data.IBqlField
        {
        }
        protected Decimal? _ExtCost;
        [PXDBBaseCury()]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Extended Cost", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Decimal? ExtCost
        {
            get
            {
                return this._ExtCost;
            }
            set
            {
                this._ExtCost = value;
            }
        }
        #endregion
        #region BegBalance
        public abstract class begBalance : PX.Data.IBqlField
        {
        }
        protected Decimal? _BegBalance;
        [PXDBBaseCury()]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Beginning Balance", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Decimal? BegBalance
        {
            get
            {
                return this._BegBalance;
            }
            set
            {
                this._BegBalance = value;
            }
        }
        #endregion
        #region EndBalance
        public abstract class endBalance : PX.Data.IBqlField
        {
        }
        protected Decimal? _EndBalance;
        [PXDBBaseCury()]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Ending Balance", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Decimal? EndBalance
        {
            get
            {
                return this._EndBalance;
            }
            set
            {
                this._EndBalance = value;
            }
        }
        #endregion

    }
    #endregion

    [PX.Objects.GL.TableAndChartDashboardType]
    public class InventoryTranHistEnq : PXGraph<InventoryTranHistEnq>
    {

        public PXFilter<InventoryTranHistEnqFilter> Filter;

        [PXFilterable]
        public PXSelectJoin<InventoryTranHistEnqResult,
            CrossJoin<INTran>,
            Where<True, Equal<True>>,
            OrderBy<Asc<InventoryTranHistEnqResult.gridLineNbr>>> ResultRecords;
        public PXSelectJoin<InventoryTranHistEnqResult,
            CrossJoin<INTran>,
            Where<True, Equal<True>>,
            OrderBy<Asc<InventoryTranHistEnqResult.gridLineNbr>>> InternalResultRecords;
        public PXCancel<InventoryTranHistEnqFilter> Cancel;
        public PXAction<InventoryTranHistEnqFilter> viewSummary;
        public PXAction<InventoryTranHistEnqFilter> viewAllocDet;

        #region Cache Attached
        public PXSelect<INTran> Tran;
        [PXDBString(2, IsFixed = true)]
        [PXUIField(DisplayName = "SO Order Type", Visible = false, Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<SO.SOOrderType.orderType>))]
        protected virtual void INTran_SOOrderType_CacheAttached(PXCache sender)
        {
        }
        protected String _SOOrderNbr;
        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "SO Order Nbr.", Visible = false, Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<SO.SOOrder.orderNbr>))]
        protected virtual void INTran_SOOrderNbr_CacheAttached(PXCache sender)
        {
        }
        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "PO Receipt Nbr.", Visible = false, Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<PO.POReceipt.receiptNbr>))]
        protected virtual void INTran_POReceiptNbr_CacheAttached(PXCache sender)
        {
        }
        #endregion

        protected virtual void InventoryTranHistEnqFilter_StartDate_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
        {
            if (true)
            {
                DateTime businessDate = (DateTime)this.Accessinfo.BusinessDate;
                e.NewValue = new DateTime(businessDate.Year, businessDate.Month, 01);
                e.Cancel = true;
            }
        }

        public InventoryTranHistEnq()
        {
            ResultRecords.Cache.AllowInsert = false;
            ResultRecords.Cache.AllowDelete = false;
            ResultRecords.Cache.AllowUpdate = false;
        }

        protected virtual IEnumerable resultRecords()
        {
			int startRow = PXView.StartRow;
            int totalRows = 0;
			decimal? beginQty = null;

            if (PXView.MaximumRows == 1 && PXView.Searches != null && PXView.Searches.Length == 1)
            {
                InventoryTranHistEnqResult ither = new InventoryTranHistEnqResult();
                ither.GridLineNbr = (int?)PXView.Searches[0];
                ither = (InventoryTranHistEnqResult)ResultRecords.Cache.Locate(ither);
                if(ither!=null && ither.InventoryID!=null)
                    return new List<object>() { ither };
            }

            ResultRecords.Cache.Clear();
			List<object> list = InternalResultRecords.View.Select(PXView.Currents, PXView.Parameters, new object[PXView.SortColumns.Length], PXView.SortColumns, PXView.Descendings, PXView.Filters, ref startRow, PXView.MaximumRows, ref totalRows);
			PXView.StartRow = 0;

            foreach (PXResult<InventoryTranHistEnqResult> item in list)
            {
                InventoryTranHistEnqResult it = (InventoryTranHistEnqResult)item;
				it.BegQty = beginQty = (beginQty ?? it.BegQty);
                decimal? QtyIn = it.QtyIn;
                decimal? QtyOut = it.QtyOut;
                beginQty += (QtyIn ?? 0m) - (QtyOut ?? 0m);
                it.EndQty = beginQty;
                ResultRecords.Cache.SetStatus(it, PXEntryStatus.Held);
            }
            return list;
        }

		protected virtual void AlterSorts(out string[] newSorts, out bool[] newDescs)
		{
			bool byTranDate = false;
			List<string> newSortColumns = new List<string>();
			List<bool> newDescendings = new List<bool>();
			for (int i=0; i< PXView.SortColumns.Length; i++)
			{
				string field = PXView.SortColumns[i];
				bool desc = PXView.Descendings[i];
				switch (field.ToLower())
				{
					case "trandate":
					case "gridlinenbr":
						if (!byTranDate)
						{
							newSortColumns.Add("TranDate");
							newSortColumns.Add("CreatedDateTime");
							newSortColumns.Add("LastModifiedDateTime");

							newDescendings.Add(desc);
							newDescendings.Add(desc);
							newDescendings.Add(desc);

							byTranDate = true;
						}
						break;
					case "docrefnbr":
						newSortColumns.Add("RefNbr");
						newDescendings.Add(desc);
						break;
					case "finpernbr":
						newSortColumns.Add("INTran__FinPeriodID");
						newDescendings.Add(desc);
						break;
					case "tranpernbr":
						newSortColumns.Add("INTran__TranPeriodID");
						newDescendings.Add(desc);
						break;
					case "begqty":
					case "endgty:":
					case "qtyin":
					case "qtyout":
						break;
					default:
						newSortColumns.Add(field);
						newDescendings.Add(desc);
						break;
				}
			}
			newSorts = newSortColumns.ToArray();
			newDescs = newDescendings.ToArray();
		}

		protected virtual PXFilterRow[] AlterFilters()
		{
			bool summaryByDay = Filter.Current?.SummaryByDay ?? false;

			List<PXFilterRow> newFilters = new List<PXFilterRow>();
			foreach (PXFilterRow field in PXView.Filters)
			{
				switch (field.DataField.ToLower())
				{
					case "docrefnbr":
						newFilters.Add(new PXFilterRow(field) { DataField = "RefNbr" });
						break;
					case "finpernbr":
						newFilters.Add(new PXFilterRow(field) { DataField = "INTran__FinPeriodID" });
						break;
					case "tranpernbr":
						newFilters.Add(new PXFilterRow(field) { DataField = "INTran__TranPeriodID" });
						break;
					case "qtyin":
						if (!summaryByDay)
						{
							newFilters.Add(new PXFilterRow(field) { DataField = "INTranSplit__QtyIn" });
						}
						break;
					case "qtyout":
						if (!summaryByDay)
						{
							newFilters.Add(new PXFilterRow(field) { DataField = "INTranSplit__QtyOut" });
						}
						break;
					case "begqty":
					case "endgty:":
					case "gridlinenbr":
						break;
					default:
						newFilters.Add(field);
						break;
				}
			}
			return newFilters.ToArray();
		}


        protected virtual IEnumerable internalResultRecords()
        {
            InventoryTranHistEnqFilter filter = Filter.Current;

            bool summaryByDay = filter.SummaryByDay ?? false;
            bool includeUnreleased = filter.IncludeUnreleased ?? false;

            PXUIFieldAttribute.SetVisible<InventoryTranHistEnqResult.inventoryID>(ResultRecords.Cache, null, false);
            PXUIFieldAttribute.SetVisible<InventoryTranHistEnqResult.finPerNbr>(ResultRecords.Cache, null, false);  //???
            PXUIFieldAttribute.SetVisible<InventoryTranHistEnqResult.tranPerNbr>(ResultRecords.Cache, null, false);  //???


            PXUIFieldAttribute.SetVisible<InventoryTranHistEnqResult.tranType>(ResultRecords.Cache, null, !summaryByDay);
            PXUIFieldAttribute.SetVisible<InventoryTranHistEnqResult.docRefNbr>(ResultRecords.Cache, null, !summaryByDay);
            PXUIFieldAttribute.SetVisible<InventoryTranHistEnqResult.subItemID>(ResultRecords.Cache, null, !summaryByDay);
            PXUIFieldAttribute.SetVisible<InventoryTranHistEnqResult.siteID>(ResultRecords.Cache, null, !summaryByDay);
            PXUIFieldAttribute.SetVisible<InventoryTranHistEnqResult.locationID>(ResultRecords.Cache, null, !summaryByDay);
            PXUIFieldAttribute.SetVisible<InventoryTranHistEnqResult.lotSerialNbr>(ResultRecords.Cache, null, !summaryByDay);
            PXUIFieldAttribute.SetVisible(Tran.Cache, null, !summaryByDay);

            var resultList = new List<PXResult<InventoryTranHistEnqResult, INTran>>();

            if (filter.InventoryID == null)
            {
                return resultList;  //empty
            }

            PXSelectBase<INTranSplit> cmd = new PXSelectReadonly2<INTranSplit,
                    InnerJoin<INTran,
                                    On<INTran.docType, Equal<INTranSplit.docType>,
                                            And<INTran.refNbr, Equal<INTranSplit.refNbr>,
                                            And<INTran.lineNbr, Equal<INTranSplit.lineNbr>>>>,
                    InnerJoin<INSubItem,
                                    On<INSubItem.subItemID, Equal<INTranSplit.subItemID>>,
                    InnerJoin<INSite, On<INSite.siteID, Equal<INTran.siteID>>>>>,
					Where<INTranSplit.inventoryID, Equal<Current<InventoryTranHistEnqFilter.inventoryID>>, And<Match<INSite, Current<AccessInfo.userName>>>>,
                    OrderBy<Asc<INTranSplit.docType, Asc<INTranSplit.refNbr, Asc<INTranSplit.lineNbr, Asc<INTranSplit.splitLineNbr>>>>>>(this);

			PXSelectBase<INItemSiteHistByPeriod> cmdBegBalance = new PXSelectReadonly2<INItemSiteHistByPeriod,
				InnerJoin<INItemSiteHist,
					On<INItemSiteHist.inventoryID, Equal<INItemSiteHistByPeriod.inventoryID>,
					And<INItemSiteHist.siteID, Equal<INItemSiteHistByPeriod.siteID>,
					And<INItemSiteHist.subItemID, Equal<INItemSiteHistByPeriod.subItemID>,
					And<INItemSiteHist.locationID, Equal<INItemSiteHistByPeriod.locationID>,
					And<INItemSiteHist.finPeriodID, Equal<INItemSiteHistByPeriod.lastActivityPeriod>>>>>>,
				InnerJoin<INSubItem,
									On<INSubItem.subItemID, Equal<INItemSiteHistByPeriod.subItemID>>,
				InnerJoin<INSite, On<INSite.siteID, Equal<INItemSiteHistByPeriod.siteID>>>>>,
				Where<INItemSiteHistByPeriod.inventoryID, Equal<Current<InventoryTranHistEnqFilter.inventoryID>>,
				And<INItemSiteHistByPeriod.finPeriodID, Equal<Required<INItemSiteHistByPeriod.finPeriodID>>,
				And<Match<INSite, Current<AccessInfo.userName>>>>>>(this);

            if (!SubCDUtils.IsSubCDEmpty(filter.SubItemCD) && PXAccess.FeatureInstalled<FeaturesSet.subItem>())
            {
				cmdBegBalance.WhereAnd<Where<INSubItem.subItemCD, Like<Current<InventoryTranHistEnqFilter.subItemCDWildcard>>>>();
                cmd.WhereAnd<Where<INSubItem.subItemCD, Like<Current<InventoryTranHistEnqFilter.subItemCDWildcard>>>>();
            }

            if (filter.SiteID != null && PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
            {
				cmdBegBalance.WhereAnd<Where<INItemSiteHistByPeriod.siteID, Equal<Current<InventoryTranHistEnqFilter.siteID>>>>();
                cmd.WhereAnd<Where<INTranSplit.siteID, Equal<Current<InventoryTranHistEnqFilter.siteID>>>>();
            }

            if ((filter.LocationID ?? -1) != -1 && PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>()) // there are cases when filter.LocationID = -1
            {
				cmdBegBalance.WhereAnd<Where<INItemSiteHistByPeriod.locationID, Equal<Current<InventoryTranHistEnqFilter.locationID>>>>();
                cmd.WhereAnd<Where<INTranSplit.locationID, Equal<Current<InventoryTranHistEnqFilter.locationID>>>>();
            }

            if ((filter.LotSerialNbr ?? "") != "" && PXAccess.FeatureInstalled<FeaturesSet.lotSerialTracking>())
            {
                cmd.WhereAnd<Where<INTranSplit.lotSerialNbr, Like<Current<InventoryTranHistEnqFilter.lotSerialNbrWildcard>>>>();
            }

            if (!includeUnreleased)
            {
                cmd.WhereAnd<Where<INTranSplit.released, Equal<True>>>();
            }

			decimal cumulativeQty = 0m;
			string TranPeriodID;
			DateTime? PeriodStartDate;
			bool AnyPeriod = false;
			try
			{
				TranPeriodID = FinPeriodIDAttribute.PeriodFromDate(this, filter.StartDate);
				PeriodStartDate = FinPeriodIDAttribute.PeriodStartDate(this, TranPeriodID);
			}
			catch (PXFinPeriodException)
			{
				TranPeriodID = null;
				PeriodStartDate = filter.StartDate;
			}

			int startRow = 0;
			int totalRows = 0;
			int maximumRows = 0;

			if (includeUnreleased)
			{
				FinPeriod firstOpenOrCurrentClosedPeriod = PXSelectReadonly<FinPeriod, 
					Where<FinPeriod.finPeriodID, LessEqual<Required<FinPeriod.finPeriodID>>, And<FinPeriod.iNClosed, Equal<False>, 
					Or<FinPeriod.finPeriodID, Equal<Required<FinPeriod.finPeriodID>>>>>, OrderBy<Asc<FinPeriod.finPeriodID>>>.SelectWindowed(this, 0, 1, TranPeriodID, TranPeriodID);
				if (firstOpenOrCurrentClosedPeriod != null )
				{
					TranPeriodID = firstOpenOrCurrentClosedPeriod.FinPeriodID;
					PeriodStartDate = FinPeriodIDAttribute.PeriodStartDate(this, firstOpenOrCurrentClosedPeriod.FinPeriodID);
            }

				foreach (PXResult<INItemSiteHistByPeriod, INItemSiteHist> res in cmdBegBalance.Select(TranPeriodID))
				{
					INItemSiteHistByPeriod byperiod = res;
					INItemSiteHist hist = res;

					cumulativeQty += string.Equals(byperiod.FinPeriodID, byperiod.LastActivityPeriod) ? (decimal)hist.TranBegQty : (decimal)hist.TranYtdQty;
					AnyPeriod = true;
				}

				if (AnyPeriod)
				{
					//if StartDate is not on the Period border, make additional select with grouping
					if (PeriodStartDate != filter.StartDate)
					{
						PXView v2 = new PXView(this, true, cmd.View.BqlSelect
							.WhereAnd<Where<INTranSplit.tranDate, GreaterEqual<Required<INTranSplit.tranDate>>>>()
							.WhereAnd<Where<INTranSplit.tranDate, Less<Required<INTranSplit.tranDate>>>>()
							.AggregateNew<Aggregate<GroupBy<INTranSplit.inventoryID, GroupBy<INTranSplit.invtMult, Sum<INTranSplit.baseQty>>>>>());

						foreach (PXResult<INTranSplit> res in v2.Select(new object[0], new object[] { PeriodStartDate, filter.StartDate }, new object[0], new string[0], new bool[0], new PXFilterRow[0], ref startRow, 0, ref totalRows))
						{
							INTranSplit ts_rec = res;
							cumulativeQty += (ts_rec.InvtMult * ts_rec.BaseQty) ?? 0m;
						}
					}
				}
				else
				{
					PXView v2 = new PXView(this, true, cmd.View.BqlSelect
						.WhereAnd<Where<INTranSplit.tranDate, Less<Required<INTranSplit.tranDate>>>>()
						.AggregateNew<Aggregate<GroupBy<INTranSplit.inventoryID, GroupBy<INTranSplit.invtMult, Sum<INTranSplit.baseQty>>>>>());

					foreach (PXResult<INTranSplit> res in v2.Select(new object[0], new object[] { filter.StartDate }, new object[0], new string[0], new bool[0], new PXFilterRow[0], ref startRow, 0, ref totalRows))
					{
						INTranSplit ts_rec = res;
						cumulativeQty += (ts_rec.InvtMult * ts_rec.BaseQty) ?? 0m;
					}
				}
			}
			else
			{
				foreach (PXResult<INItemSiteHistByPeriod, INItemSiteHist> res in cmdBegBalance.Select(TranPeriodID))
				{
					INItemSiteHistByPeriod byperiod = res;
					INItemSiteHist hist = res;

					cumulativeQty += string.Equals(byperiod.FinPeriodID, byperiod.LastActivityPeriod) ? (decimal)hist.TranBegQty : (decimal)hist.TranYtdQty;
					AnyPeriod = true;
				}

				if (AnyPeriod)
				{
					//if StartDate is not on the Period border, make additional select with grouping
					if (PeriodStartDate != filter.StartDate)
					{
						PXView v2 = new PXView(this, true, cmd.View.BqlSelect
							.WhereAnd<Where<INTranSplit.tranDate, GreaterEqual<Required<INTranSplit.tranDate>>>>()
							.WhereAnd<Where<INTranSplit.tranDate, Less<Required<INTranSplit.tranDate>>>>()
							.AggregateNew<Aggregate<GroupBy<INTranSplit.inventoryID, GroupBy<INTranSplit.invtMult, Sum<INTranSplit.baseQty>>>>>());

						foreach (PXResult<INTranSplit> res in v2.Select(new object[0], new object[] { PeriodStartDate, filter.StartDate }, new object[0], new string[0], new bool[0], new PXFilterRow[0], ref startRow, 0, ref totalRows))
						{
							INTranSplit ts_rec = res;
							cumulativeQty += (ts_rec.InvtMult * ts_rec.BaseQty) ?? 0m;
						}
					}
				}
			}

			if (filter.StartDate != null)
			{
				cmd.WhereAnd<Where<INTranSplit.tranDate, GreaterEqual<Current<InventoryTranHistEnqFilter.startDate>>>>();
			}

            if (filter.EndDate != null)
            {
                cmd.WhereAnd<Where<INTranSplit.tranDate, LessEqual<Current<InventoryTranHistEnqFilter.endDate>>>>();
            }

			string[] newSortColumns;
			bool[] newDescendings;
			AlterSorts(out newSortColumns, out newDescendings);
			PXFilterRow[] newFilters = AlterFilters();

			//if user clicks last, sorts will be inverted
			//as it is not possible to calculate beginning balance from the end
			//we will select without top from the start and then apply reverse order and select top n records
			//for next page we will ommit startrow to set beginning balance correctly
			//top (n, m) will be set in the outer search results since we do not reset PXView.StartRow to 0
			startRow = 0;
			maximumRows = !PXView.ReverseOrder ? PXView.StartRow + PXView.MaximumRows : 0;
			totalRows = 0;

			PXView selectView = !summaryByDay ? cmd.View
				: new PXView(this, true, cmd.View.BqlSelect.AggregateNew<Aggregate<GroupBy<INTranSplit.tranDate, Sum<INTranSplit.qtyIn, Sum<INTranSplit.qtyOut>>>>>());

			List<object> intermediateResult = selectView.Select(PXView.Currents, new object[] { filter.StartDate }, new string[newSortColumns.Length], newSortColumns, PXView.Descendings, newFilters, ref startRow, maximumRows, ref totalRows);

            int gridLineNbr = 0;

            foreach (PXResult<INTranSplit, INTran, INSubItem> it in intermediateResult)
            {
                INTranSplit ts_rec = (INTranSplit)it;
                INTran t_rec = (INTran)it;

                if (ts_rec.TranDate < filter.StartDate)
                {
                    cumulativeQty += (ts_rec.InvtMult * ts_rec.BaseQty) ?? 0m;
                }
                else
                {
                    if (summaryByDay)
                    {
                            InventoryTranHistEnqResult item = new InventoryTranHistEnqResult();
                            item.BegQty = cumulativeQty;
                            item.TranDate = ts_rec.TranDate;
						item.QtyIn = ts_rec.QtyIn;
						item.QtyOut = ts_rec.QtyOut;
						item.EndQty = item.BegQty + ts_rec.QtyIn - ts_rec.QtyOut;
                            item.GridLineNbr = ++gridLineNbr;
                            resultList.Add(new PXResult<InventoryTranHistEnqResult, INTran>(item, null));
                        cumulativeQty += (ts_rec.QtyIn - ts_rec.QtyOut) ?? 0m;
                    }
                    else
                    {
                        InventoryTranHistEnqResult item = new InventoryTranHistEnqResult();
                        item.BegQty = cumulativeQty;
                        item.TranDate = ts_rec.TranDate;
						item.QtyIn = ts_rec.QtyIn;
						item.QtyOut = ts_rec.QtyOut;
						item.EndQty = item.BegQty + ts_rec.QtyIn - ts_rec.QtyOut;

                        item.InventoryID = ts_rec.InventoryID;
                        item.TranType = ts_rec.TranType;
                        item.DocType = ts_rec.DocType;
                        item.DocRefNbr = ts_rec.RefNbr;
                        item.SubItemID = ts_rec.SubItemID;
                        item.SiteID = ts_rec.SiteID;
                        item.LocationID = ts_rec.LocationID;
                        item.LotSerialNbr = ts_rec.LotSerialNbr;
                        item.FinPerNbr = t_rec.FinPeriodID;
                        item.TranPerNbr = t_rec.TranPeriodID;
                        item.Released = t_rec.Released;
                        item.GridLineNbr = ++gridLineNbr;

                        decimal? unitcost;
                        if(filter.ShowAdjUnitCost ?? false)
                        {
                            unitcost = ts_rec.TotalQty != null && ts_rec.TotalQty != 0m ? (ts_rec.TotalCost + ts_rec.AdditionalCost) / ts_rec.TotalQty : 0m;
                        }
                        else
                        {
                            unitcost = ts_rec.TotalQty != null && ts_rec.TotalQty != 0m ? ts_rec.TotalCost / ts_rec.TotalQty : 0m;
                        }

                        item.UnitCost = unitcost;
                        resultList.Add(new PXResult<InventoryTranHistEnqResult, INTran>(item, t_rec));
                        cumulativeQty += (ts_rec.InvtMult * ts_rec.BaseQty) ?? 0m;
                    }
                }
            }
			if (!PXView.ReverseOrder)
            return resultList;
			return PXView.Sort(resultList);
        }

		public override IEnumerable ExecuteSelect(string viewName, object[] parameters, object[] searches, string[] sortcolumns, bool[] descendings, PXFilterRow[] filters, ref int startRow, int maximumRows, ref int totalRows)
		{
			if (string.Equals(viewName, nameof(ResultRecords), StringComparison.OrdinalIgnoreCase))
			{
				bool summaryByDay = Filter.Current?.SummaryByDay ?? false;
				if (summaryByDay)
				{
					filters = filters?.Where(f =>
						!string.Equals(f.DataField, nameof(InventoryTranHistEnqResult.QtyIn), StringComparison.OrdinalIgnoreCase)
						&& !string.Equals(f.DataField, nameof(InventoryTranHistEnqResult.QtyOut), StringComparison.OrdinalIgnoreCase))
						.ToArray();
				}
			}
			return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
		}

        public override bool IsDirty
        {
            get
            {
                return false;
            }
        }

        #region View Actions

        [PXButton()]
        [PXUIField(DisplayName = Messages.InventorySummary)]
        protected virtual IEnumerable ViewSummary(PXAdapter a)
        {
            if (this.ResultRecords.Current != null)
            {
                PXSegmentedState subItem =
                    this.ResultRecords.Cache.GetValueExt<InventoryTranHistEnqResult.subItemID>
                    (this.ResultRecords.Current) as PXSegmentedState;
                InventorySummaryEnq.Redirect(
                    this.ResultRecords.Current.InventoryID,
                    subItem != null ? (string)subItem.Value : null,
                    this.ResultRecords.Current.SiteID,
                    this.ResultRecords.Current.LocationID, false);
            }
            return a.Get();
        }


        [PXButton()]
        [PXUIField(DisplayName = Messages.InventoryAllocDet)]
        protected virtual IEnumerable ViewAllocDet(PXAdapter a)
        {
            if (this.ResultRecords.Current != null)
            {
                PXSegmentedState subItem =
                    this.ResultRecords.Cache.GetValueExt<InventoryTranHistEnqResult.subItemID>
                    (this.ResultRecords.Current) as PXSegmentedState;
                InventoryAllocDetEnq.Redirect(
                    this.ResultRecords.Current.InventoryID,
                    subItem != null ? (string)subItem.Value : null,
                    null,
                    this.ResultRecords.Current.SiteID,
                    this.ResultRecords.Current.LocationID);
            }
            return a.Get();
        }

        #endregion

        public static void Redirect(string finPeriodID, int? inventoryID, string subItemCD, string lotSerNum, int? siteID, int? locationID)
        {
            InventoryTranHistEnq graph = PXGraph.CreateInstance<InventoryTranHistEnq>();

            graph.Filter.Current.InventoryID = inventoryID;
            graph.Filter.Current.SubItemCD = subItemCD;
            graph.Filter.Current.SiteID = siteID;
            graph.Filter.Current.LocationID = locationID;
            graph.Filter.Current.LotSerialNbr = lotSerNum;

            throw new PXRedirectRequiredException(graph, Messages.InventoryTranHist);
        }
    }
}


