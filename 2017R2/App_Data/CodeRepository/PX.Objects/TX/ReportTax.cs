using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.CS;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.TX.Descriptor;
using PX.Objects.Common.Extensions;

namespace PX.Objects.TX
{
	public class RoundingManager
	{
		private readonly Vendor vendor;
		public RoundingManager(PXGraph graph, int? vendorId)
		{
			vendor = PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>.Select(graph, vendorId);
		}

		public RoundingManager(Vendor vendor)
		{
			this.vendor = vendor;
		}

		public bool IsRequireRounding
		{
			get { return this.vendor != null && this.vendor.TaxUseVendorCurPrecision != true && vendor.TaxReportRounding != null && vendor.TaxReportPrecision != null; }
		}

		public Vendor CurrentVendor
		{
			get { return vendor; }
		}

		public decimal? Round(decimal? value)
		{
            if (value == null || vendor == null || !IsRequireRounding)
            {
                return value;
            }

			short decimals = vendor.TaxReportPrecision ?? 2;
            decimal input = value.Value;

			switch (vendor.TaxReportRounding)
			{
				case RoundingTypes.Mathematical:
					return PXRound.Math(input, decimals);
				case RoundingTypes.Ceil:
					return PXRound.Ceil(input, decimals);
				case RoundingTypes.Floor:
					return PXRound.Floor(input, decimals);
				default:
					return value;
			}
		}
	}

	[System.SerializableAttribute()]
	public partial class TaxPeriodFilter : IBqlTable
	{
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[TaxMasterBranch(IsDBField = false)]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;

		[TaxAgencyActive]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region TaxPeriodID
		public abstract class taxPeriodID : IBqlField {}

		[PXDefault]
		[PXUIField(DisplayName = "Tax Period", Visibility = PXUIVisibility.Visible)]
		[FinPeriodID]
		[PXSelector(
			typeof(Search2<TaxPeriod.taxPeriodID,
				InnerJoin<Branch,
					On<TaxPeriod.branchID, Equal<Branch.parentBranchID>>>,
				Where<TaxPeriod.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>,
					And<Branch.branchID, Equal<Current<TaxPeriodFilter.branchID>>>>>), 
            typeof(TaxPeriod.taxPeriodID), typeof(TaxPeriod.startDateUI), typeof(TaxPeriod.endDateUI), typeof(TaxPeriod.status),
			SelectorMode = PXSelectorMode.NoAutocomplete,
			DirtyRead = true)]
		public virtual string TaxPeriodID { get; set; }
		#endregion
		#region RevisionID
		public abstract class revisionId : PX.Data.IBqlField
		{
		}
		protected Int32? _RevisionId;
		[PXDBInt()]
        [PXUIField(DisplayName = "Revision")]
		[PXSelector(typeof(Search5<TaxHistory.revisionID,
											InnerJoin<Branch,
												On<TaxHistory.branchID, Equal<Branch.branchID>>,
											InnerJoin<BranchAlias,
												On<Branch.parentBranchID, Equal<BranchAlias.parentBranchID>>>>,
											 Where<BranchAlias.branchID, Equal<Current<TaxPeriodFilter.branchID>>,
													And<TaxHistory.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>,
													And<TaxHistory.taxPeriodID, Equal<Current<TaxPeriodFilter.taxPeriodID>>>>>,
											 Aggregate<GroupBy<TaxHistory.revisionID>>,
											 OrderBy<Asc<TaxHistory.revisionID>>>))]
		public virtual Int32? RevisionId
		{
			get
			{
				return this._RevisionId;
			}
			set
			{
				this._RevisionId = value;
			}
		}
		#endregion
		#region StartDate
		public abstract class startDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _StartDate;
        [PXDate()]
        [PXUIField(DisplayName = "From", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
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
		[PXDate()]
		[PXUIField(DisplayName = "To", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
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
		#region ShowDifference
		public abstract class showDifference : PX.Data.IBqlField { }
		protected bool? _ShowDifference;
		[PXBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Show Difference", Visible = false)]
		public virtual bool? ShowDifference
		{
			get
			{
				return _ShowDifference;
			}
			set
			{
				this._ShowDifference = value;
			}
		}
		#endregion
		#region PreparedWarningMsg
		public abstract class preparedWarningMsg : PX.Data.IBqlField { }
		protected string _PreparedWarningMsg;
		[PXString()]
		public virtual string PreparedWarningMsg
		{
			get { return _PreparedWarningMsg; }
			set { this._PreparedWarningMsg = value; }
		}
		#endregion
	}

	[Serializable]
    [PXHidden]
	public partial class TaxPeriodMaster : TaxPeriod
	{
		#region BranchID
		public new abstract class branchID : PX.Data.IBqlField
		{
		}

		#endregion
		#region VendorID
		public new abstract class vendorID : PX.Data.IBqlField
		{
		}
		[Vendor(typeof(Search<Vendor.bAccountID, Where<Vendor.taxAgency, Equal<True>>>), IsKey = true, DisplayName = "Tax Agency", Required = true)]
		public override Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region TaxPeriodID
		public new abstract class taxPeriodID : PX.Data.IBqlField
		{
		}
		[GL.FinPeriodID(IsKey = true)]
		[PXDefault()]
		public override String TaxPeriodID
		{
			get
			{
				return this._TaxPeriodID;
			}
			set
			{
				this._TaxPeriodID = value;
			}
		}
		#endregion
		#region StartDate
		public new abstract class startDate : PX.Data.IBqlField
		{
		}
		[PXDBDate()]
		[PXDefault()]
		[PXUIField(DisplayName = "From", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public override DateTime? StartDate
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
		public new abstract class endDate : PX.Data.IBqlField
		{
		}
		[PXDBDate()]
		[PXDefault()]
		[PXUIField(DisplayName = "To", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public override DateTime? EndDate
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
		#region EndDateInclusive
		public abstract class endDateInclusive : PX.Data.IBqlField { }
		[PXDate()]
		[PXUIField(DisplayName = "To", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual DateTime? EndDateInclusive
		{
			[PXDependsOnFields(typeof(endDate))]
			get
			{
				return this._EndDate == null ? (DateTime?)null : ((DateTime)this._EndDate).AddDays(-1);
			}
			set
			{
				this._EndDate = value == null ? (DateTime?)null : ((DateTime)value).AddDays(+1);
			}
		}
		#endregion
		#region Status
		public new abstract class status : PX.Data.IBqlField
		{
		}
		#endregion
		#region RevisionID
		public abstract class revisionId : PX.Data.IBqlField
		{
		}
		protected Int32? _RevisionId;
		[PXInt()]
		[PXUIField(DisplayName = "Revision ID")]
		[PXSelector(typeof(Search5<TaxHistory.revisionID,
											InnerJoin<Branch,
												On<TaxHistory.branchID, Equal<Branch.branchID>>,
											InnerJoin<BranchAlias,
												On<Branch.parentBranchID, Equal<BranchAlias.parentBranchID>>>>,
											 Where<BranchAlias.branchID, Equal<Current<TaxPeriodMaster.branchID>>,
													And<TaxHistory.vendorID, Equal<Current<TaxPeriodMaster.vendorID>>,
													And<TaxHistory.taxPeriodID, Equal<Current<TaxPeriodMaster.taxPeriodID>>>>>,
											 Aggregate<GroupBy<TaxHistory.revisionID>>,
											 OrderBy<Asc<TaxHistory.revisionID>>>))]
		[PXDBScalar(typeof(Search5<TaxHistory.revisionID,
											InnerJoin<Branch, 
												On<TaxHistory.branchID, Equal<Branch.branchID>>,
											 InnerJoin<BranchAlias,
												On<Branch.parentBranchID, Equal<BranchAlias.parentBranchID>>>>,
											 Where<BranchAlias.branchID, Equal<TaxPeriod.branchID>,
												 And<TaxHistory.vendorID, Equal<TaxPeriod.vendorID>,
												 And<TaxHistory.taxPeriodID, Equal<TaxPeriod.taxPeriodID>>>>,
											 Aggregate<Max<TaxHistory.revisionID>>>))]
		public virtual Int32? RevisionId
		{
			get
			{
				return this._RevisionId;
			}
			set
			{
				this._RevisionId = value;
			}
		}
		#endregion				

		public static implicit operator TaxPeriodFilter(TaxPeriodMaster master)
		{
			return new TaxPeriodFilter()
							{
								VendorID = master.VendorID,
								TaxPeriodID = master.TaxPeriodID,
								RevisionId = master.RevisionId
							};
		}
	}

	[Serializable]
	public partial class TaxHistoryMaster : IBqlTable
	{
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[TaxMasterBranch(IsDBField = false)]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[Vendor(typeof(Search<Vendor.bAccountID, Where<Vendor.taxAgency, Equal<True>>>), DisplayName = "Tax Agency", Required = true)]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region TaxPeriodID
		public abstract class taxPeriodID : PX.Data.IBqlField
		{
		}
		protected String _TaxPeriodID;
		[GL.FinPeriodID()]
		[PXDefault()]
		[PXUIField(DisplayName = "Tax Period", Visibility = PXUIVisibility.Visible)]
		[PXSelector(typeof(Search2<TaxPeriod.taxPeriodID,
									InnerJoin<Branch,
										On<TaxPeriod.branchID, Equal<Branch.parentBranchID>>>,  
									Where<TaxPeriod.vendorID, Equal<Current<TaxHistoryMaster.vendorID>>,
											And<Branch.branchID, Equal<Current<TaxHistoryMaster.branchID>>,  
											And<Where<TaxPeriod.status, Equal<TaxPeriodStatus.prepared>, 
														Or<TaxPeriod.status, Equal<TaxPeriodStatus.closed>>>>>>>),
            typeof(TaxPeriod.taxPeriodID), typeof(TaxPeriod.startDateUI), typeof(TaxPeriod.endDateUI), typeof(TaxPeriod.status))]
		public virtual String TaxPeriodID
		{
			get
			{
				return this._TaxPeriodID;
			}
			set
			{
				this._TaxPeriodID = value;
			}
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineNbr;
		[PXDBInt()]
		[PXUIField(DisplayName = "Report Line", Visibility = PXUIVisibility.SelectorVisible)]
		[PXIntList(new int[] { 0 }, new string[] { "undefined" })]
		public virtual Int32? LineNbr
		{
			get
			{
				return this._LineNbr;
			}
			set
			{
				this._LineNbr = value;
			}
		}
		#endregion
		#region StartDate
		public abstract class startDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _StartDate;
		[PXDBDate()]
		[PXDefault()]
		[PXUIField(DisplayName = "From", Visibility = PXUIVisibility.Visible, Enabled = false)]
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
        #region StartDateUI
        public abstract class startDateUI : IBqlField { }

        /// <summary>
        /// The field used to display and edit the <see cref="StartDate"/> of the period (inclusive) in the UI.
        /// </summary>
        /// <value>
        /// Depends on and changes the value of the <see cref="StartDate"/> field, performing additional transformations.
        /// </value>
        [PXDate]
        [PXUIField(DisplayName = "From", Visibility = PXUIVisibility.Visible, Enabled = false)]
        public virtual DateTime? StartDateUI
        {
            [PXDependsOnFields(typeof(startDate), typeof(endDate))]
            get
            {
                return (_StartDate != null && _EndDate != null && _StartDate == _EndDate) ? _StartDate.Value.AddDays(-1) : _StartDate;
            }
            set
            {
                _StartDate = (value != null && _EndDate != null && value == EndDateInclusive) ? value.Value.AddDays(1) : value;
            }
        }
        #endregion
        #region EndDate
        public abstract class endDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _EndDate;
		[PXDBDate()]
		[PXDefault()]
		[PXUIField(DisplayName = "To", Visibility = PXUIVisibility.Visible, Enabled = false)]
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
		#region EndDateInclusive
		public abstract class endDateInclusive : PX.Data.IBqlField { }
		[PXDate()]
		[PXUIField(DisplayName = "To", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual DateTime? EndDateInclusive
		{
			get { return this._EndDate == null ? (DateTime?)null : ((DateTime)this._EndDate).AddDays(-1); }
			set { this._EndDate = value == null ? (DateTime?)null : ((DateTime)value).AddDays(+1); }
		}
		#endregion
	}

	[PXProjection(typeof(
		Select5<TaxPeriodMaster, 
				LeftJoin<TaxPeriod,
		On<TaxPeriod.vendorID, Equal<TaxPeriodMaster.vendorID>,
						And<TaxPeriod.branchID, Equal<TaxPeriodMaster.branchID>,
						And<TaxPeriod.status, Equal<TaxPeriodStatus.open>>>>>,
		Aggregate<
					GroupBy<TaxPeriodMaster.branchID,
		GroupBy<TaxPeriodMaster.vendorID,
		GroupBy<TaxPeriodMaster.taxPeriodID,
		Max<TaxPeriodMaster.endDate,
					Min<TaxPeriod.taxPeriodID>>>>>>>))]
    [PXHidden]
	public partial class TaxPeriodPlusOpen : PX.Data.IBqlTable
	{
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}

		[Branch(IsKey = true, BqlField = typeof(TaxPeriodMaster.branchID))]
		public virtual Int32? BranchID { get; set; }
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[PXDBInt(IsKey = true, BqlField = typeof(TaxPeriodMaster.vendorID))]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region TaxPeriodID
		public abstract class taxPeriodID : PX.Data.IBqlField
		{
		}
		protected String _TaxPeriodID;
		[GL.FinPeriodID(IsKey = true, BqlField = typeof(TaxPeriodMaster.taxPeriodID))]
		[PXUIField(DisplayName = "Tax Period", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String TaxPeriodID
		{
			get
			{
				return this._TaxPeriodID;
			}
			set
			{
				this._TaxPeriodID = value;
			}
		}
		#endregion
		#region Status
		public abstract class status : PX.Data.IBqlField
		{
		}
		protected String _Status;
		[PXDBString(1, IsFixed = true, BqlField = typeof(TaxPeriodMaster.status))]
		[TaxPeriodStatus.List()]
		[PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}
		#endregion
		#region EndDate
		public abstract class endDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _EndDate;
		[PXDBDate(BqlField = typeof(TaxPeriodMaster.endDate))]
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
		#region OpenPeriodID
		public abstract class openPeriodID : PX.Data.IBqlField
		{
		}
		protected String _OpenPeriodID;
		[PXDBString(6, IsFixed = true, BqlField = typeof(TaxPeriod.taxPeriodID))]
		public virtual String OpenPeriodID
		{
			get
			{
				return this._OpenPeriodID;
			}
			set
			{
				this._OpenPeriodID = value;
			}
		}
		#endregion
	}

	[PXProjection(typeof(Select<TaxPeriod, Where<TaxPeriod.status, NotEqual<TaxPeriodStatus.open>>>))]
	public partial class TaxPeriodForReportShowing : PX.Data.IBqlTable
	{
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}

		[TaxMasterBranch(IsKey = true, BqlField = typeof(TaxPeriod.branchID))]
		public virtual Int32? BranchID { get; set; }
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[Vendor(typeof(Search<Vendor.bAccountID, Where<Vendor.taxAgency, Equal<True>>>), IsKey = true, BqlField = typeof(TaxPeriod.vendorID), DisplayName = "Tax Agency")]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region TaxPeriodID
		public abstract class taxPeriodID : PX.Data.IBqlField
		{
		}
		protected string _TaxPeriodID;
		[GL.FinPeriodID(IsKey = true, BqlField = typeof(TaxPeriod.taxPeriodID))]
		[PXSelector(typeof(Search2<TaxPeriod.taxPeriodID, 
								InnerJoin<Branch,
									On<TaxPeriod.branchID, Equal<Branch.parentBranchID>>>, 
								Where<Branch.branchID, Equal<Optional<TaxPeriodForReportShowing.branchID>>,
										And<TaxPeriod.vendorID, Equal<Optional<TaxPeriodForReportShowing.vendorID>>,
										And<TaxPeriod.status, NotEqual<TaxPeriodStatus.open>>>>>),
            typeof(TaxPeriod.taxPeriodID), typeof(TaxPeriod.startDateUI), typeof(TaxPeriod.endDateUI), typeof(TaxPeriod.status))]
		[PXUIField(DisplayName = "Tax Period", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String TaxPeriodID
		{
			get
			{
				return this._TaxPeriodID;
			}
			set
			{
				this._TaxPeriodID = value;
			}
		}
		#endregion
		#region Status
		public abstract class status : PX.Data.IBqlField
		{
		}
		protected string _Status;
		[PXDBString(1, IsFixed = true, BqlField = typeof(TaxPeriod.status))]
		[PXDefault(TaxPeriodStatus.Open)]
		[TaxPeriodStatus.List()]
		[PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual String Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}
		#endregion
		#region EndDate
		public abstract class endDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _EndDate;
		[PXDBDate(BqlField = typeof(TaxPeriod.endDate))]
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

		#region RevisionID
		public abstract class revisionID : PX.Data.IBqlField
		{
		}
		protected Int32? _RevisionID;
		[PXSelector(typeof(Search5<TaxHistory.revisionID,
									InnerJoin<Branch, 
										On<TaxHistory.branchID, Equal<Branch.branchID>>,
									InnerJoin<BranchAlias,
										On<Branch.parentBranchID, Equal<BranchAlias.parentBranchID>>>>,
									Where<BranchAlias.branchID, Equal<Optional<TaxPeriodForReportShowing.branchID>>,
											And<TaxHistory.vendorID, Equal<Optional<TaxPeriodForReportShowing.vendorID>>, 
											And<Where<TaxHistory.taxPeriodID, Equal<Optional<TaxPeriodForReportShowing.taxPeriodID>>>>>>,
				Aggregate<GroupBy<TaxHistory.revisionID>>>))]
		[PXUIField(DisplayName = "Revision ID", Visibility = PXUIVisibility.SelectorVisible)]
		[PXInt()]
		public virtual Int32? RevisionID
		{
			get
			{
				return this._RevisionID;
			}
			set
			{
				this._RevisionID = value;
			}
		}
		#endregion
	}

	[PXProjection(typeof(Select<TaxPeriodPlusOpen, Where<TaxPeriodPlusOpen.taxPeriodID, Equal<TaxPeriodPlusOpen.openPeriodID>, Or<TaxPeriodPlusOpen.status, NotEqual<TaxPeriodStatus.open>>>>))]
	public partial class TaxPeriodEffective : PX.Data.IBqlTable
	{
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}

		[Branch(IsKey = true, BqlField = typeof(TaxPeriodPlusOpen.branchID))]
		public virtual Int32? BranchID { get; set; }
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[Vendor(typeof(Search<Vendor.bAccountID, Where<Vendor.taxAgency, Equal<True>>>), IsKey = true, BqlField = typeof(TaxPeriodPlusOpen.vendorID), DisplayName = "Tax Agency")]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region TaxPeriodID
		public abstract class taxPeriodID : PX.Data.IBqlField
		{
		}
		protected string _TaxPeriodID;
		[GL.FinPeriodID(IsKey = true, BqlField = typeof(TaxPeriodPlusOpen.taxPeriodID))]
		[PXSelector(typeof(Search2<TaxPeriodPlusOpen.taxPeriodID, 
								InnerJoin<Branch,
									On<TaxPeriodPlusOpen.branchID, Equal<Branch.parentBranchID>>>, 
									Where<Branch.branchID, Equal<Optional<TaxPeriodEffective.branchID>>, 
											And<TaxPeriodPlusOpen.vendorID, Equal<Optional<TaxPeriodEffective.vendorID>>, 
											And<Where<TaxPeriodPlusOpen.taxPeriodID, Equal<TaxPeriodPlusOpen.openPeriodID>, 
														Or<TaxPeriodPlusOpen.status, NotEqual<TaxPeriodStatus.open>>>>>>>))]
		[PXUIField(DisplayName = "Tax Period", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String TaxPeriodID
		{
			get
			{
				return this._TaxPeriodID;
			}
			set
			{
				this._TaxPeriodID = value;
			}
		}
		#endregion
		#region Status
		public abstract class status : PX.Data.IBqlField
		{
		}
		protected string _Status;
		[PXDBString(1, IsFixed = true, BqlField = typeof(TaxPeriodPlusOpen.status))]
		[PXDefault(TaxPeriodStatus.Open)]
		[TaxPeriodStatus.List()]
		[PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual String Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}
		#endregion
		#region EndDate
		public abstract class endDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _EndDate;
		[PXDBDate(BqlField = typeof(TaxPeriodPlusOpen.endDate))]
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

		#region RevisionID
		public abstract class revisionID : PX.Data.IBqlField
		{
		}
		protected Int32? _RevisionID;
		[PXSelector(typeof(Search5<TaxHistory.revisionID,
									InnerJoin<Branch,
										On<TaxHistory.branchID, Equal<Branch.branchID>>,
									InnerJoin<BranchAlias,
										On<Branch.parentBranchID, Equal<BranchAlias.parentBranchID>>>>,
									Where<BranchAlias.branchID, Equal<Optional<TaxPeriodEffective.branchID>>,
											And<TaxHistory.vendorID, Equal<Optional<TaxPeriodEffective.vendorID>>, 
											And<Where<TaxHistory.taxPeriodID, Equal<Optional<TaxPeriodEffective.taxPeriodID>>>>>>,
				Aggregate<GroupBy<TaxHistory.revisionID>>>))]
		[PXUIField(DisplayName = "Revision ID", Visibility = PXUIVisibility.SelectorVisible)]
		[PXInt()]
		public virtual Int32? RevisionID
		{
			get
			{
				return this._RevisionID;
			}
			set
			{
				this._RevisionID = value;
			}
		}
		#endregion
	}

	[PXProjection(typeof(Select<TaxTran>))]
	[PXCacheName(Messages.TaxTranReport)]
	public partial class TaxTranReport : TaxTran
	{	
		#region BranchID
		public new abstract class branchID : IBqlField { }
		#endregion
		#region RefNbr
		public new abstract class refNbr : IBqlField { }
		#endregion
		#region VendorID
		public new abstract class vendorID : IBqlField { }
		#endregion
		#region AccountID
		public new abstract class accountID : IBqlField { }
		#endregion
		#region SubID
		public new abstract class subID : IBqlField { }
		#endregion
		#region TaxPeriodID
		public new abstract class taxPeriodID : IBqlField { }
		#endregion
		#region FinPeriodID
		public new abstract class finPeriodID : IBqlField { }
		#endregion
		#region tstamp
		public new abstract class tstamp : IBqlField { }
		#endregion
		#region Module
		public new abstract class module : IBqlField { }
		#endregion
		#region TranType
		public new abstract class tranType : IBqlField { }
		#endregion
		#region TranTypeInvoiceDiscriminated
		public class tranTypeInvoiceDiscriminated : IBqlField, ILabelProvider
		{
			public const string APInvoice = "INP";
			public const string ARInvoice = "INR";

			public class apInvoice : Constant<string>
			{
				public apInvoice() : base(APInvoice) {}
			}

			public class arInvoice : Constant<string>
			{
				public arInvoice() : base(ARInvoice) {}
			}

			public IEnumerable<ValueLabelPair> ValueLabelPairs { get; } = new ValueLabelList
			{
				{ APInvoice, AP.Messages.Invoice },
				{ APDocType.DebitAdj, AP.Messages.DebitAdj },
				{ APDocType.CreditAdj, AP.Messages.CreditAdj },
				{ APDocType.QuickCheck, AP.Messages.QuickCheck },
				{ APDocType.VoidQuickCheck, AP.Messages.VoidQuickCheck },
				{ APDocType.Check, AP.Messages.Check },
				{ APDocType.VoidCheck, AP.Messages.VoidCheck },
				{ APDocType.Prepayment, AP.Messages.Prepayment },
				{ APDocType.Refund, AP.Messages.Refund },

				{ ARInvoice, AR.Messages.Invoice },
				{ ARDocType.DebitMemo, AR.Messages.DebitMemo },
				{ ARDocType.CreditMemo, AR.Messages.CreditMemo },
				{ ARDocType.CashSale, AR.Messages.CashSale },
				{ ARDocType.CashReturn, AR.Messages.CashReturn },
				{ ARDocType.Payment, AR.Messages.Payment },
				{ ARDocType.VoidPayment, AR.Messages.VoidPayment },
				{ ARDocType.Prepayment, AR.Messages.Prepayment },
				{ ARDocType.Refund, AR.Messages.Refund },
				{ ARDocType.FinCharge, AR.Messages.FinCharge },
				{ ARDocType.SmallBalanceWO, AR.Messages.SmallBalanceWO },
				{ ARDocType.SmallCreditWO, AR.Messages.SmallCreditWO },

				{ TaxAdjustmentType.AdjustOutput, TX.Messages.AdjustOutput },
				{ TaxAdjustmentType.AdjustInput, TX.Messages.AdjustInput },
				{ TaxAdjustmentType.InputVAT, TX.Messages.InputVAT },
				{ TaxAdjustmentType.OutputVAT, TX.Messages.OutputVAT },

				{ CATranType.CAAdjustment, CA.Messages.CAAdjustment },
				{ CATranType.CATransferExp, CA.Messages.CATransferExp },

				{ TaxTran.tranType.TranReversed, Messages.ReversingGLEntry },
				{ TaxTran.tranType.TranForward, Messages.GLEntry },
			};
		}

		protected string _TranTypeInvoiceDiscriminated;
		[PXString]
		[LabelList(typeof(tranTypeInvoiceDiscriminated))]
		[PXDBCalced(typeof(Switch<
			Case<
				Where<TaxTran.module, Equal<BatchModule.moduleAP>,
					And<TaxTran.tranType, Equal<APDocType.invoice>>>,
				tranTypeInvoiceDiscriminated.apInvoice,
			Case<
				Where<TaxTran.module, Equal<BatchModule.moduleAR>,
					And<TaxTran.tranType, Equal<ARDocType.invoice>>>,
				tranTypeInvoiceDiscriminated.arInvoice>>,
			TaxTran.tranType>),
			typeof(string))]
		[PXUIField(DisplayName = "Tran. Type")]
		public virtual string TranTypeInvoiceDiscriminated
		{
			get
			{
				return this._TranTypeInvoiceDiscriminated;
			}
			set
			{
				this._TranTypeInvoiceDiscriminated = value;
			}
		}

		#endregion
		#region Released
		public new abstract class released : IBqlField { }
		#endregion
		#region Voided
		public new abstract class voided : PX.Data.IBqlField
		{
		}
		#endregion
		#region TaxID
		public new abstract class taxID : IBqlField { }
		#endregion
		#region TaxRate
		public new abstract class taxRate : IBqlField { }
		#endregion
		#region CuryInfoID
		public new abstract class curyInfoID : IBqlField { }
		#endregion
		#region CuryTaxableAmt
		public new abstract class curyTaxableAmt : IBqlField { }
		#endregion
		#region TaxableAmt
		public new abstract class taxableAmt : IBqlField { }
		#endregion
		#region CuryTaxAmt
		public new abstract class curyTaxAmt : IBqlField { }
		#endregion
		#region TaxAmt
		public new abstract class taxAmt : IBqlField { }
		#endregion
		#region CuryID
		public new abstract class curyID : IBqlField { }
		#endregion
		#region ReportCuryID
		public new abstract class reportCuryID : IBqlField { }
		#endregion
		#region ReportCuryRateTypeID
		public new abstract class reportCuryRateTypeID : IBqlField { }
		#endregion
		#region ReportCuryEffDate
		public new abstract class reportCuryEffDate : IBqlField { }
		#endregion
		#region ReportCuryMultDiv
		public new abstract class reportCuryMultDiv : IBqlField { }
		#endregion
		#region ReportCuryRate
		public new abstract class reportCuryRate : IBqlField { }
		#endregion
		#region ReportTaxableAmt
		public new abstract class reportTaxableAmt : IBqlField { }
		#endregion
		#region ReportTaxAmt
		public new abstract class reportTaxAmt : IBqlField { }
		#endregion
		#region CreatedByID
		public new abstract class createdByID : IBqlField { }
		#endregion
		#region CreatedByScreenID
		public new abstract class createdByScreenID : IBqlField { }
		#endregion
		#region CreatedDateTime
		public new abstract class createdDateTime : IBqlField { }
		#endregion
		#region LastModifiedByID
		public new abstract class lastModifiedByID : IBqlField { }
		#endregion
		#region LastModifiedByScreenID
		public new abstract class lastModifiedByScreenID : IBqlField { }
		#endregion
		#region LastModifiedDateTime
		public new abstract class lastModifiedDateTime : IBqlField { }
		#endregion
		#region TranDate
		public new abstract class tranDate : IBqlField { }
		#endregion
		#region TaxBucketID
		public new abstract class taxBucketID : IBqlField { }
		#endregion
		#region TaxType
		public new abstract class taxType : IBqlField { }
		#endregion
		#region TaxZoneID
		public new abstract class taxZoneID : IBqlField { }
		#endregion
		#region TaxInvoiceNbr
		public new abstract class taxInvoiceNbr : IBqlField { }
		#endregion
		#region TaxInvoiceDate
		public new abstract class taxInvoiceDate : IBqlField { }
		#endregion
		#region OrigTranType
		public new abstract class origTranType : IBqlField { }
		#endregion
		#region OrigRefNbr
		public new abstract class origRefNbr : IBqlField { }
		#endregion
		#region RevisionID
		public new abstract class revisionID : IBqlField { }
		#endregion
		#region BAccountID
		public new abstract class bAccountID : IBqlField { }
		#endregion
		#region FinDate
		public new abstract class finDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _FinDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "FinDate", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
		public override DateTime? FinDate
		{
			get;
			set;
			}
		#endregion
		#region Sign
		public abstract class sign : PX.Data.IBqlField
		{
		}
		protected decimal? _Sign;

		/// <summary>
		/// Sign of TaxTran with which it will adjust net tax amount.
		/// Consists of following multipliers:
		/// - Tax type of TaxTran:
		///		- Sales (Output): 1
		///		- Purchase (Input): -1
		/// - Document type and module:
		///		- AP
		///			- Debit Adjustment, Voided Quick Check, Refund: -1  
		///			- Invoice, Credit Adjustment, Quik Check, Voided Check, any other not listed: 1  
		///		- AR
		///			- Credit Memo, Cash Return: -1  
		///			- Invoice, Debit Memo, Fin Charge, Cash Sale, any other not listed: 1 
		///		- GL
		///			- Reversing GL Entry: -1  
		///			- GL Entry, any other not listed: 1   
		///		- CA: 1 
		///		- Any other not listed combinations: -1
		/// </summary>
		[PXDecimal]
		public virtual decimal? Sign
		{
			[PXDependsOnFields(typeof(module), typeof(tranType), typeof(taxType))]
			get
			{
				return ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, 1);
			}
			set
			{
				this._Sign = value;
			}
		}
		#endregion
		#region Description
		public new abstract class description : PX.Data.IBqlField
		{
		}
		#endregion
		#region AdjdDocType
		public new abstract class adjdDocType : PX.Data.IBqlField
		{
		}
		#endregion
		#region AdjdRefNbr
		public new abstract class adjdRefNbr : PX.Data.IBqlField
		{
		}
		#endregion
		#region AdjNbr
		public new abstract class adjNbr : PX.Data.IBqlField
		{
		}
		#endregion
	}

	[PXProjection(typeof(Select2<TaxPeriodForReportShowing,
		InnerJoin<TaxReportLine,
			On<TaxReportLine.vendorID, Equal<TaxPeriodForReportShowing.vendorID>>,
		InnerJoin<TaxBucketLine,
			On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>, 
				And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>>>,
		InnerJoin<Branch,
			On<TaxPeriodForReportShowing.branchID, Equal<Branch.parentBranchID>>,
		LeftJoin<TaxTran,
			On<TaxTran.vendorID, Equal<TaxBucketLine.vendorID>,
				And<TaxTran.released, Equal<True>,
				And<TaxTran.voided, Equal<False>,
				And<TaxTran.taxType, NotEqual<TaxType.pendingSales>,
				And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>,
				And<TaxTran.taxBucketID, Equal<TaxBucketLine.bucketID>,
				And2<Where<TaxReportLine.taxZoneID, IsNull,
						And<TaxReportLine.tempLine, Equal<False>,
						Or<TaxReportLine.taxZoneID, Equal<TaxTran.taxZoneID>>>>,
				And<TaxTran.taxPeriodID, Equal<TaxPeriodForReportShowing.taxPeriodID>,
				And<TaxTran.branchID, Equal<Branch.branchID>>>>>>>>>>>>>>>))]
	[PXCacheName(Messages.TaxDetailReport)]
	public partial class TaxDetailReport : PX.Data.IBqlTable
	{
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineNbr;
		[PXDBInt(IsKey = true, BqlField = typeof(TaxReportLine.lineNbr))]
		public virtual Int32? LineNbr
		{
			get
			{
				return this._LineNbr;
			}
			set
			{
				this._LineNbr = value;
			}
		}
		#endregion
		#region LineMult
		public abstract class lineMult : PX.Data.IBqlField
		{
		}
		protected Int16? _LineMult;
		[PXDBShort(BqlField = typeof(TaxReportLine.lineMult))]
		public virtual Int16? LineMult
		{
			get
			{
				return this._LineMult;
			}
			set
			{
				this._LineMult = value;
			}
		}
		#endregion
		#region LineType
		public abstract class lineType : PX.Data.IBqlField
		{
		}
		protected string _LineType;
		[PXDBString(1, IsFixed = true, BqlField = typeof(TaxReportLine.lineType))]
		public virtual string LineType
		{
			get
			{
				return this._LineType;
			}
			set
			{
				this._LineType = value;
			}
		}
		#endregion
		#region Module
		public abstract class module : PX.Data.IBqlField
		{
		}
		protected String _Module;
		[PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof(TaxTran.module))]
		public virtual String Module
		{
			get
			{
				return this._Module;
			}
			set
			{
				this._Module = value;
			}
		}
		#endregion
		#region TranType
		public abstract class tranType : PX.Data.IBqlField
		{
		}
		protected String _TranType;
		[PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof(TaxTran.tranType))]
		[TaxAdjustmentType.List()]
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
		#region TranTypeInvoiceDiscriminated
		public abstract class tranTypeInvoiceDiscriminated : IBqlField {}
		protected string _TranTypeInvoiceDiscriminated;

		[PXString]
		[PXDBCalced(typeof(Switch<
			Case<
				Where<TaxTran.module, Equal<BatchModule.moduleAP>,
					And<TaxTran.tranType, Equal<APDocType.invoice>>>,
				TaxTranReport.tranTypeInvoiceDiscriminated.apInvoice,
			Case<
				Where<TaxTran.module, Equal<BatchModule.moduleAR>,
					And<TaxTran.tranType, Equal<ARDocType.invoice>>>,
				TaxTranReport.tranTypeInvoiceDiscriminated.arInvoice>>,
			TaxTran.tranType>),
			typeof(string))]
		[LabelList(typeof(TaxTranReport.tranTypeInvoiceDiscriminated))]
		public virtual string TranTypeInvoiceDiscriminated
		{
			get
			{
				return this._TranTypeInvoiceDiscriminated;
			}
			set
			{
				this._TranTypeInvoiceDiscriminated = value;
			}
		}
		#endregion
		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
		}
		protected String _RefNbr;
		[PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof(TaxTran.refNbr))]
		public virtual String RefNbr
		{
			get
			{
				return this._RefNbr;
			}
			set
			{
				this._RefNbr = value;
			}
		}
		#endregion
		#region RecordID
		public abstract class recordID : PX.Data.IBqlField
		{
		}
		protected Int32? _RecordID;
		[PXDBInt(IsKey = true, BqlField = typeof(TaxTran.recordID))]
		public virtual Int32? RecordID
		{
			get
			{
				return this._RecordID;
			}
			set
			{
				this._RecordID = value;
			}
		}
		#endregion
		#region Released
		public abstract class released : PX.Data.IBqlField
		{
		}
		protected Boolean? _Released;
		[PXDBBool(BqlField = typeof(TaxTran.released))]
		public virtual Boolean? Released
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
		#region Voided
		public abstract class voided : PX.Data.IBqlField
		{
		}
		protected Boolean? _Voided;
		[PXDBBool(BqlField = typeof(TaxTran.voided))]
		public virtual Boolean? Voided
		{
			get
			{
				return this._Voided;
			}
			set
			{
				this._Voided = value;
			}
		}
		#endregion
		#region TaxPeriodID
		public abstract class taxPeriodID : PX.Data.IBqlField
		{
		}
		protected String _TaxPeriodID;
		[GL.FinPeriodID(BqlField = typeof(TaxPeriodForReportShowing.taxPeriodID))]
		public virtual String TaxPeriodID
		{
			get
			{
				return this._TaxPeriodID;
			}
			set
			{
				this._TaxPeriodID = value;
			}
		}
		#endregion
		#region TaxID
		public abstract class taxID : PX.Data.IBqlField
		{
		}
		protected string _TaxID;
		[PXDBString(Tax.taxID.Length, IsUnicode = true, IsKey = true, BqlField = typeof(TaxTran.taxID))]
		public virtual String TaxID
		{
			get
			{
				return this._TaxID;
			}
			set
			{
				this._TaxID = value;
			}
		}
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[Vendor(BqlField = typeof(TaxTran.vendorID))]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[Branch(BqlField = typeof(TaxTran.branchID))]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region TaxZoneID
		public abstract class taxZoneID : PX.Data.IBqlField
		{
		}
		protected String _TaxZoneID;
		[PXDBString(10, IsUnicode = true, BqlField = typeof(TaxTran.taxZoneID))]
		public virtual String TaxZoneID
		{
			get
			{
				return this._TaxZoneID;
			}
			set
			{
				this._TaxZoneID = value;
			}
		}
		#endregion
		#region AccountID
		public abstract class accountID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountID;
		[Account(null, BqlField = typeof(TaxTran.accountID))]
		public virtual Int32? AccountID
		{
			get
			{
				return this._AccountID;
			}
			set
			{
				this._AccountID = value;
			}
		}
		#endregion
		#region SubID
		public abstract class subID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubID;
		[SubAccount(typeof(TaxTran.accountID), BqlField = typeof(TaxTran.subID))]
		public virtual Int32? SubID
		{
			get
			{
				return this._SubID;
			}
			set
			{
				this._SubID = value;
			}
		}
		#endregion
		#region TranDate
		public abstract class tranDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _TranDate;
		[PXDBDate(BqlField = typeof(TaxTran.tranDate))]
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
		#region TaxType
		public abstract class taxType : PX.Data.IBqlField
		{
		}
		protected String _TaxType;
		[PXDBString(1, IsFixed = true, BqlField = typeof(TaxTran.taxType))]
		public virtual String TaxType
		{
			get
			{
				return this._TaxType;
			}
			set
			{
				this._TaxType = value;
			}
		}
		#endregion
		#region TaxRate
		public abstract class taxRate : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxRate;
		[PXDBDecimal(6, BqlField = typeof(TaxTran.taxRate))]
		public virtual Decimal? TaxRate
		{
			get
			{
				return this._TaxRate;
			}
			set
			{
				this._TaxRate = value;
			}
		}
		#endregion
		#region ReportTaxableAmt
		public abstract class reportTaxableAmt : IBqlField
		{
		}

		protected Decimal? _ReportTaxableAmt;

		[PXDBDecimal(BqlField = typeof(TaxTran.reportTaxableAmt))]
		public virtual Decimal? ReportTaxableAmt
		{
			[PXDependsOnFields(typeof(module), typeof(tranType), typeof(taxType), typeof(lineMult))]
			get
			{
				decimal HistMult = ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, this._LineMult);
				return HistMult * this._ReportTaxableAmt;
			}
			set
			{
				this._ReportTaxableAmt = value;
			}
		}
		#endregion
		#region ReportTaxAmt
		public abstract class reportTaxAmt : IBqlField
		{
		}
		
		protected Decimal? _ReportTaxAmt;

		[PXDBDecimal(BqlField = typeof(TaxTran.reportTaxAmt))]
		public virtual Decimal? ReportTaxAmt
		{
			[PXDependsOnFields(typeof(module), typeof(tranType), typeof(taxType), typeof(lineMult))]
			get
			{
				decimal HistMult = ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, this._LineMult);
				return HistMult * this._ReportTaxAmt;
			}
			set
			{
				this._ReportTaxAmt = value;
			}
		}
		#endregion
	}

	[PXProjection(typeof(Select2<TaxPeriodForReportShowing,
			 InnerJoin<TaxReportLine,
							On<TaxReportLine.vendorID, Equal<TaxPeriodForReportShowing.vendorID>,
							And<TaxReportLine.tempLine, NotEqual<True>>>,
								InnerJoin<Branch,
											On<TaxPeriodForReportShowing.branchID, Equal<Branch.parentBranchID>>,
			 LeftJoin<TaxHistorySum,
									 On<TaxHistorySum.branchID, Equal<Branch.branchID>,
										And<TaxHistorySum.vendorID, Equal<TaxReportLine.vendorID>,
					 And<TaxHistorySum.lineNbr, Equal<TaxReportLine.lineNbr>,
										And<TaxHistorySum.taxPeriodID, Equal<TaxPeriodForReportShowing.taxPeriodID>>>>>>>>,
	 Where<TaxReportLine.tempLineNbr, IsNull, Or<TaxHistorySum.vendorID, IsNotNull>>>))]
	[PXCacheName(Messages.TaxReportSummary)]
	public partial class TaxReportSummary : PX.Data.IBqlTable
	{
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineNbr;
		[PXDBInt(IsKey = true, BqlField = typeof(TaxReportLine.lineNbr))]
		public virtual Int32? LineNbr
		{
			get
			{
				return this._LineNbr;
			}
			set
			{
				this._LineNbr = value;
			}
		}
		#endregion
		#region LineMult
		public abstract class lineMult : PX.Data.IBqlField
		{
		}
		protected Int16? _LineMult;
		[PXDBShort(BqlField = typeof(TaxReportLine.lineMult))]
		public virtual Int16? LineMult
		{
			get
			{
				return this._LineMult;
			}
			set
			{
				this._LineMult = value;
			}
		}
		#endregion
		#region LineType
		public abstract class lineType : PX.Data.IBqlField
		{
		}
		protected string _LineType;
		[PXDBString(1, IsFixed = true, BqlField = typeof(TaxReportLine.lineType))]
		public virtual string LineType
		{
			get
			{
				return this._LineType;
			}
			set
			{
				this._LineType = value;
			}
		}
		#endregion
		#region TaxPeriodID
		public abstract class taxPeriodID : PX.Data.IBqlField
		{
		}
		protected String _TaxPeriodID;
		[GL.FinPeriodID(BqlField = typeof(TaxPeriodForReportShowing.taxPeriodID))]
		public virtual String TaxPeriodID
		{
			get
			{
				return this._TaxPeriodID;
			}
			set
			{
				this._TaxPeriodID = value;
			}
		}
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[Vendor(BqlField = typeof(TaxReportLine.vendorID))]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region RevisionID
		public abstract class revisionID : PX.Data.IBqlField
		{
		}
		protected Int32? _RevisionID;
		[PXDBInt(IsKey = true, BqlTable = typeof(TaxHistorySum))]
		[PXDefault()]
		[PXUIField(DisplayName = "Revision ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual Int32? RevisionID
		{
			get
			{
				return this._RevisionID;
			}
			set
			{
				this._RevisionID = value;
			}
		}
		#endregion
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[Branch(BqlField = typeof(TaxHistorySum.branchID), IsKey = true)]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region FiledAmt
		public abstract class filedAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _FiledAmt;

		[PXDBBaseCury(BqlTable = typeof(TaxHistorySum))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual Decimal? FiledAmt
		{
			get
			{
				return this._FiledAmt;
			}
			set
			{
				this._FiledAmt = value;
			}
		}
		#endregion
		#region UnfiledAmt
		public abstract class unfiledAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _UnfiledAmt;
		[PXDBBaseCury(BqlTable = typeof(TaxHistorySum))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual Decimal? UnfiledAmt
		{
			get
			{
				return this._UnfiledAmt;
			}
			set
			{
				this._UnfiledAmt = value;
			}
		}
		#endregion
		#region ReportFiledAmt
		public abstract class reportFiledAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _ReportFiledAmt;

		[PXDBVendorCury(typeof(TaxReportSummary.vendorID), BqlTable = typeof(TaxHistorySum))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual Decimal? ReportFiledAmt
		{
			get
			{
				return this._ReportFiledAmt;
			}
			set
			{
				this._ReportFiledAmt = value;
			}
		}
		#endregion
		#region ReportUnfiledAmt
		public abstract class reportUnfiledAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _ReportUnfiledAmt;
		[PXDBVendorCury(typeof(TaxReportSummary.vendorID), BqlTable = typeof(TaxHistorySum))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual Decimal? ReportUnfiledAmt
		{
			get
			{
				return this._ReportUnfiledAmt;
			}
			set
			{
				this._ReportUnfiledAmt = value;
			}
		}
		#endregion
	}

	[PXProjection(typeof(Select2<TaxPeriodEffective,
	 InnerJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<TaxPeriodEffective.vendorID>>,
	 InnerJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>, And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>>>,
	 CrossJoin<Company,
	 InnerJoin<Vendor,
			 On<Vendor.bAccountID, Equal<TaxReportLine.vendorID>>,
	 LeftJoin<TaxTran,
			 On<TaxTran.vendorID, Equal<TaxBucketLine.vendorID>,
			 And<TaxTran.taxBucketID, Equal<TaxBucketLine.bucketID>,
			 And2<Where<TaxReportLine.taxZoneID, IsNull,
					 And<TaxReportLine.tempLine, Equal<False>,
							 Or<TaxReportLine.taxZoneID, Equal<TaxTran.taxZoneID>>>>,
			 And<Where<TaxTran.taxPeriodID, IsNull, And<TaxTran.origRefNbr, Equal<Empty>,
					 And<TaxTran.released, Equal<True>,
					 And<TaxTran.voided, Equal<False>,
			 And<TaxTran.taxType, NotEqual<TaxType.pendingSales>,
			 And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>,
			 And<TaxTran.tranDate, Less<TaxPeriodEffective.endDate>,
			 And<TaxPeriodEffective.status, Equal<TaxPeriodStatus.open>,
			 Or<TaxTran.taxPeriodID, Equal<TaxPeriodEffective.taxPeriodID>>>>>>>>>>>>>>,
	 LeftJoin<CurrencyInfo,
			 On<CurrencyInfo.curyInfoID, Equal<TaxTran.curyInfoID>>,
	 LeftJoin<CurrencyRateByDate,
			 On<CurrencyRateByDate.fromCuryID, Equal<CurrencyInfo.curyID>,
			 And<CurrencyRateByDate.toCuryID, Equal<Vendor.curyID>,
			 And<CurrencyRateByDate.curyRateType, Equal<Vendor.curyRateTypeID>,
			 And<CurrencyRateByDate.curyEffDate, LessEqual<TaxTran.tranDate>,
					 And<Where<CurrencyRateByDate.nextEffDate, Greater<TaxTran.tranDate>,
									 Or<CurrencyRateByDate.nextEffDate, IsNull>>>
>>>>>>>>>>>>))]

	[Obsolete("TaxDetailReportCurrency is obsolete and will be removed in Acumatica 8.0.")]
	[PXCacheName(Messages.TaxDetailReportCurrency)]
	public partial class TaxDetailReportCurrency : PX.Data.IBqlTable
	{
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineNbr;
		[PXDBInt(IsKey = true, BqlField = typeof(TaxReportLine.lineNbr))]
		public virtual Int32? LineNbr
		{
			get
			{
				return this._LineNbr;
			}
			set
			{
				this._LineNbr = value;
			}
		}
		#endregion
		#region LineMult
		public abstract class lineMult : PX.Data.IBqlField
		{
		}
		protected Int16? _LineMult;
		[PXDBShort(BqlField = typeof(TaxReportLine.lineMult))]
		public virtual Int16? LineMult
		{
			get
			{
				return this._LineMult;
			}
			set
			{
				this._LineMult = value;
			}
		}
		#endregion
		#region LineType
		public abstract class lineType : PX.Data.IBqlField
		{
		}
		protected string _LineType;
		[PXDBString(1, IsFixed = true, BqlField = typeof(TaxReportLine.lineType))]
		public virtual string LineType
		{
			get
			{
				return this._LineType;
			}
			set
			{
				this._LineType = value;
			}
		}
		#endregion
		#region Module
		public abstract class module : PX.Data.IBqlField
		{
		}
		protected String _Module;
		[PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof(TaxTran.module))]
		public virtual String Module
		{
			get
			{
				return this._Module;
			}
			set
			{
				this._Module = value;
			}
		}
		#endregion
		#region TranType
		public abstract class tranType : PX.Data.IBqlField
		{
		}
		protected String _TranType;
		[PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof(TaxTran.tranType))]
		[TaxAdjustmentType.List()]
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
		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
		}
		protected String _RefNbr;
		[PXDBString(15, IsKey = true, BqlField = typeof(TaxTran.refNbr))]
		public virtual String RefNbr
		{
			get
			{
				return this._RefNbr;
			}
			set
			{
				this._RefNbr = value;
			}
		}
		#endregion
		#region RecordID
		public abstract class recordID : PX.Data.IBqlField
		{
		}
		protected Int32? _RecordID;
		[PXDBInt(IsKey = true, BqlField = typeof(TaxTran.recordID))]
		public virtual Int32? RecordID
		{
			get
			{
				return this._RecordID;
			}
			set
			{
				this._RecordID = value;
			}
		}
		#endregion
		#region Released
		public abstract class released : PX.Data.IBqlField
		{
		}
		protected Boolean? _Released;
		[PXDBBool(BqlField = typeof(TaxTran.released))]
		public virtual Boolean? Released
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
		#region Voided
		public abstract class voided : PX.Data.IBqlField
		{
		}
		protected Boolean? _Voided;
		[PXDBBool(BqlField = typeof(TaxTran.voided))]
		public virtual Boolean? Voided
		{
			get
			{
				return this._Voided;
			}
			set
			{
				this._Voided = value;
			}
		}
		#endregion
		#region TaxPeriodID
		public abstract class taxPeriodID : PX.Data.IBqlField
		{
		}
		protected String _TaxPeriodID;
		[GL.FinPeriodID(BqlField = typeof(TaxPeriodEffective.taxPeriodID))]
		public virtual String TaxPeriodID
		{
			get
			{
				return this._TaxPeriodID;
			}
			set
			{
				this._TaxPeriodID = value;
			}
		}
		#endregion
		#region TaxID
		public abstract class taxID : PX.Data.IBqlField
		{
		}
		protected string _TaxID;
		[PXDBString(Tax.taxID.Length, IsKey = true, BqlField = typeof(TaxTran.taxID))]
		public virtual String TaxID
		{
			get
			{
				return this._TaxID;
			}
			set
			{
				this._TaxID = value;
			}
		}
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[Vendor(BqlField = typeof(TaxTran.vendorID))]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[Branch(BqlField = typeof(TaxTran.branchID))]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region TaxZoneID
		public abstract class taxZoneID : PX.Data.IBqlField
		{
		}
		protected String _TaxZoneID;
		[PXDBString(10, BqlField = typeof(TaxTran.taxZoneID))]
		public virtual String TaxZoneID
		{
			get
			{
				return this._TaxZoneID;
			}
			set
			{
				this._TaxZoneID = value;
			}
		}
		#endregion
		#region AccountID
		public abstract class accountID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountID;
		[Account(null, BqlField = typeof(TaxTran.accountID))]
		public virtual Int32? AccountID
		{
			get
			{
				return this._AccountID;
			}
			set
			{
				this._AccountID = value;
			}
		}
		#endregion
		#region SubID
		public abstract class subID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubID;
		[SubAccount(typeof(TaxTran.accountID), BqlField = typeof(TaxTran.subID))]
		public virtual Int32? SubID
		{
			get
			{
				return this._SubID;
			}
			set
			{
				this._SubID = value;
			}
		}
		#endregion
		#region TranDate
		public abstract class tranDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _TranDate;
		[PXDBDate(BqlField = typeof(TaxTran.tranDate))]
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
		#region TaxType
		public abstract class taxType : PX.Data.IBqlField
		{
		}
		protected String _TaxType;
		[PXDBString(1, IsFixed = true, BqlField = typeof(TaxTran.taxType))]
		public virtual String TaxType
		{
			get
			{
				return this._TaxType;
			}
			set
			{
				this._TaxType = value;
			}
		}
		#endregion
		#region TaxRate
		public abstract class taxRate : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxRate;
		[PXDBDecimal(6, BqlField = typeof(TaxTran.taxRate))]
		public virtual Decimal? TaxRate
		{
			get
			{
				return this._TaxRate;
			}
			set
			{
				this._TaxRate = value;
			}
		}
		#endregion
		#region TaxableAmt
		public abstract class taxableAmt : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxableAmt;
		[PXDBBaseCury(BqlField = typeof(TaxTran.taxableAmt))]
		public virtual decimal? TaxableAmt
		{
			[PXDependsOnFields(typeof(module), typeof(tranType), typeof(taxType), typeof(lineMult))]
			get
			{
				decimal HistMult = ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, this._LineMult);
				return HistMult * this._TaxableAmt;
			}
			set
			{
				this._TaxableAmt = value;
			}
		}
		#endregion
		#region TaxAmt
		public abstract class taxAmt : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxAmt;
		[PXDBBaseCury(BqlField = typeof(TaxTran.taxAmt))]
		public virtual decimal? TaxAmt
		{
			[PXDependsOnFields(typeof(module), typeof(tranType), typeof(taxType), typeof(lineMult))]
			get
			{
				decimal HistMult = ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, this._LineMult);
				return HistMult * this._TaxAmt;
			}
			set
			{
				this._TaxAmt = value;
			}
		}
		#endregion
		#region VendorCuryID
		public abstract class vendorCuryID : PX.Data.IBqlField
		{
		}
		protected String _VendorCuryID;
		[PXDBString(5, BqlField = typeof(Vendor.curyID))]
		public virtual String VendorCuryID
		{
			get
			{
				return this._VendorCuryID;
			}
			set
			{
				this._VendorCuryID = value;
			}
		}
		#endregion
		#region VendorCuryRateType
		public abstract class vendorCuryRateType : PX.Data.IBqlField
		{
		}
		protected String _VendorCuryRateType;
		[PXDBString(6, BqlField = typeof(Vendor.curyRateTypeID))]
		public virtual String VendorCuryRateType
		{
			get
			{
				return this._VendorCuryRateType;
			}
			set
			{
				this._VendorCuryRateType = value;
			}
		}
		#endregion
		#region CuryRate
		public abstract class curyRate : PX.Data.IBqlField
		{
		}
		protected decimal? _CuryRate;
		[PXDBDecimal(6, BqlField = typeof(CurrencyRateByDate.curyRate))]
		public virtual Decimal? CuryRate
		{
			get
			{
				return this._CuryRate;
			}
			set
			{
				this._CuryRate = value;
			}
		}
		#endregion
		#region CuryMultDiv
		public abstract class curyMultDiv : PX.Data.IBqlField
		{
		}
		protected String _CuryMultDiv;
		[PXDBString(1, BqlField = typeof(CurrencyRateByDate.curyMultDiv))]
		public virtual String CuryMultDiv
		{
			get
			{
				return this._CuryMultDiv;
			}
			set
			{
				this._CuryMultDiv = value;
			}
		}
		#endregion
	}

	[PXProjection(typeof(Select2<TaxTran,
	InnerJoin<TaxBucket, On<TaxBucket.vendorID, Equal<TaxTran.vendorID>, And<TaxBucket.bucketID, Equal<TaxTran.taxBucketID>>>,
	LeftJoin<TaxPeriodEffective,
			On<TaxPeriodEffective.vendorID, Equal<TaxTran.vendorID>,
					And<Where<TaxTran.taxPeriodID, IsNull, And<TaxTran.origRefNbr, Equal<Empty>,
							And<TaxTran.released, Equal<True>,
							And<TaxTran.voided, Equal<False>,
							And<TaxTran.taxType, NotEqual<TaxType.pendingSales>,
							And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>,
							And<TaxTran.tranDate, Less<TaxPeriodEffective.endDate>,
							And<TaxPeriodEffective.status, Equal<TaxPeriodStatus.open>,
									Or<TaxTran.taxPeriodID, Equal<TaxPeriodEffective.taxPeriodID>>>>>>>>>>>>,
	InnerJoin<Vendor,
			On<Vendor.bAccountID, Equal<TaxTran.vendorID>>,
	LeftJoin<CurrencyInfo,
			On<CurrencyInfo.curyInfoID, Equal<TaxTran.curyInfoID>>,
	LeftJoin<CurrencyRateByDate,
			On<CurrencyRateByDate.fromCuryID, Equal<CurrencyInfo.curyID>,
					And<CurrencyRateByDate.toCuryID, Equal<Vendor.curyID>,
					And<CurrencyRateByDate.curyRateType, Equal<Vendor.curyRateTypeID>,
					And<CurrencyRateByDate.curyEffDate, LessEqual<TaxTran.tranDate>,
							And<Where<CurrencyRateByDate.nextEffDate, Greater<TaxTran.tranDate>,
											Or<CurrencyRateByDate.nextEffDate, IsNull>>>>>>>
											>>>>>>))]
	[Obsolete("TaxDetailByGLReportCurrency is obsolete and will be removed in Acumatica 8.0.")]
    [PXHidden]
	public partial class TaxDetailByGLReportCurrency : PX.Data.IBqlTable
	{
		#region Module
		public abstract class module : PX.Data.IBqlField
		{
		}
		protected String _Module;
		[PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof(TaxTran.module))]
		public virtual String Module
		{
			get
			{
				return this._Module;
			}
			set
			{
				this._Module = value;
			}
		}
		#endregion
		#region TranType
		public abstract class tranType : PX.Data.IBqlField
		{
		}
		protected String _TranType;
		[PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof(TaxTran.tranType))]
		[TaxAdjustmentType.List()]
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
		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
		}
		protected String _RefNbr;
		[PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof(TaxTran.refNbr))]
		public virtual String RefNbr
		{
			get
			{
				return this._RefNbr;
			}
			set
			{
				this._RefNbr = value;
			}
		}
		#endregion
		#region RecordID
		public abstract class recordID : PX.Data.IBqlField
		{
		}
		protected Int32? _RecordID;
		[PXDBInt(IsKey = true, BqlField = typeof(TaxTran.recordID))]
		public virtual Int32? RecordID
		{
			get
			{
				return this._RecordID;
			}
			set
			{
				this._RecordID = value;
			}
		}
		#endregion
		#region Released
		public abstract class released : PX.Data.IBqlField
		{
		}
		protected Boolean? _Released;
		[PXDBBool(BqlField = typeof(TaxTran.released))]
		public virtual Boolean? Released
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
		#region Voided
		public abstract class voided : PX.Data.IBqlField
		{
		}
		protected Boolean? _Voided;
		[PXDBBool(BqlField = typeof(TaxTran.voided))]
		public virtual Boolean? Voided
		{
			get
			{
				return this._Voided;
			}
			set
			{
				this._Voided = value;
			}
		}
		#endregion
		#region TaxPeriodID
		public abstract class taxPeriodID : PX.Data.IBqlField
		{
		}
		protected String _TaxPeriodID;
		[GL.FinPeriodID(BqlField = typeof(TaxPeriodEffective.taxPeriodID))]
		public virtual String TaxPeriodID
		{
			get
			{
				return this._TaxPeriodID;
			}
			set
			{
				this._TaxPeriodID = value;
			}
		}
		#endregion
		#region TaxID
		public abstract class taxID : PX.Data.IBqlField
		{
		}
		protected string _TaxID;
		[PXDBString(Tax.taxID.Length, IsUnicode = true, IsKey = true, BqlField = typeof(TaxTran.taxID))]
		[PXSelector(typeof(Tax.taxID), DescriptionField = typeof(Tax.descr))]
		public virtual String TaxID
		{
			get
			{
				return this._TaxID;
			}
			set
			{
				this._TaxID = value;
			}
		}
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[Vendor(BqlField = typeof(TaxTran.vendorID))]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[Branch(BqlField = typeof(TaxTran.branchID))]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region TaxZoneID
		public abstract class taxZoneID : PX.Data.IBqlField
		{
		}
		protected String _TaxZoneID;
		[PXDBString(10, IsUnicode = true, BqlField = typeof(TaxTran.taxZoneID))]
		public virtual String TaxZoneID
		{
			get
			{
				return this._TaxZoneID;
			}
			set
			{
				this._TaxZoneID = value;
			}
		}
		#endregion
		#region AccountID
		public abstract class accountID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountID;
		[Account(null, BqlField = typeof(TaxTran.accountID))]
		public virtual Int32? AccountID
		{
			get
			{
				return this._AccountID;
			}
			set
			{
				this._AccountID = value;
			}
		}
		#endregion
		#region SubID
		public abstract class subID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubID;
		[SubAccount(typeof(TaxTran.accountID), BqlField = typeof(TaxTran.subID))]
		public virtual Int32? SubID
		{
			get
			{
				return this._SubID;
			}
			set
			{
				this._SubID = value;
			}
		}
		#endregion
		#region TranDate
		public abstract class tranDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _TranDate;
		[PXDBDate(BqlField = typeof(TaxTran.tranDate))]
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
		#region TaxType
		public abstract class taxType : PX.Data.IBqlField
		{
		}
		protected String _TaxType;
		[PXDBString(1, IsFixed = true, BqlField = typeof(TaxTran.taxType))]
		public virtual String TaxType
		{
			get
			{
				return this._TaxType;
			}
			set
			{
				this._TaxType = value;
			}
		}
		#endregion
		#region TaxRate
		public abstract class taxRate : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxRate;
		[PXDBDecimal(6, BqlField = typeof(TaxTran.taxRate))]
		public virtual Decimal? TaxRate
		{
			get
			{
				return this._TaxRate;
			}
			set
			{
				this._TaxRate = value;
			}
		}
		#endregion
		#region TaxableAmt
		public abstract class taxableAmt : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxableAmt;
		[PXDBBaseCury(BqlField = typeof(TaxTran.taxableAmt))]
		public virtual decimal? TaxableAmt
		{
			get
			{
				return this._TaxableAmt;
			}
			set
			{
				this._TaxableAmt = value;
			}
		}
		#endregion
		#region TaxAmt
		public abstract class taxAmt : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxAmt;
		[PXDBBaseCury(BqlField = typeof(TaxTran.taxAmt))]
		public virtual decimal? TaxAmt
		{
			get
			{
				return this._TaxAmt;
			}
			set
			{
				this._TaxAmt = value;
			}
		}
		#endregion
		#region TaxableAmtIO
		public abstract class taxableAmtIO : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxableAmtIO;
		[PXDBBaseCury(BqlField = typeof(TaxTran.taxableAmt))]
		public virtual decimal? TaxableAmtIO
		{
			[PXDependsOnFields(typeof(module), typeof(tranType), typeof(taxType), typeof(taxableAmt))]
			get
			{
				return ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, 1) * this._TaxableAmt;
			}
			set
			{
				this._TaxableAmtIO = value;
			}
		}
		#endregion
		#region TaxAmtIO
		public abstract class taxAmtIO : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxAmtIO;
		[PXDBBaseCury(BqlField = typeof(TaxTran.taxAmt))]
		public virtual decimal? TaxAmtIO
		{
			[PXDependsOnFields(typeof(module), typeof(tranType), typeof(taxType), typeof(taxAmt))]
			get
			{
				return ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, 1) * this._TaxAmt;
			}
			set
			{
				this._TaxAmtIO = value;
			}
		}
		#endregion
		#region VendorCuryID
		public abstract class vendorCuryID : PX.Data.IBqlField
		{
		}
		protected String _VendorCuryID;
		[PXDBString(5, BqlField = typeof(Vendor.curyID))]
		public virtual String VendorCuryID
		{
			get
			{
				return this._VendorCuryID;
			}
			set
			{
				this._VendorCuryID = value;
			}
		}
		#endregion
		#region VendorCuryRateType
		public abstract class vendorCuryRateType : PX.Data.IBqlField
		{
		}
		protected String _VendorCuryRateType;
		[PXDBString(6, BqlField = typeof(Vendor.curyRateTypeID))]
		public virtual String VendorCuryRateType
		{
			get
			{
				return this._VendorCuryRateType;
			}
			set
			{
				this._VendorCuryRateType = value;
			}
		}
		#endregion
		#region CuryRate
		public abstract class curyRate : PX.Data.IBqlField
		{
		}
		protected decimal? _CuryRate;
		[PXDBDecimal(6, BqlField = typeof(CurrencyRateByDate.curyRate))]
		public virtual Decimal? CuryRate
		{
			get
			{
				return this._CuryRate;
			}
			set
			{
				this._CuryRate = value;
			}
		}
		#endregion
		#region CuryMultDiv
		public abstract class curyMultDiv : PX.Data.IBqlField
		{
		}
		protected String _CuryMultDiv;
		[PXDBString(1, BqlField = typeof(CurrencyRateByDate.curyMultDiv))]
		public virtual String CuryMultDiv
		{
			get
			{
				return this._CuryMultDiv;
			}
			set
			{
				this._CuryMultDiv = value;
			}
		}
		#endregion
	}

	[PXProjection(typeof(Select2<TaxTran,
								InnerJoin<TaxBucket, 
									On<TaxBucket.vendorID, Equal<TaxTran.vendorID>, 
										And<TaxBucket.bucketID, Equal<TaxTran.taxBucketID>>>,
								InnerJoin<Branch,
									On<TaxTran.branchID, Equal<Branch.branchID>>,
		LeftJoin<TaxPeriodEffective,
									On<Branch.parentBranchID, Equal<TaxPeriodEffective.branchID>,
										And<TaxPeriodEffective.vendorID, Equal<TaxTran.vendorID>,
				And<Where<TaxTran.taxPeriodID, IsNull, 
								And<TaxTran.origRefNbr, Equal<Empty>,
								And<TaxTran.released, Equal<True>, 
								And<TaxTran.voided, Equal<False>,
								And<TaxTran.taxType, NotEqual<TaxType.pendingSales>, 
								And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>, 
								And<TaxTran.tranDate, Less<TaxPeriodEffective.endDate>, 
								And<TaxPeriodEffective.status, Equal<TaxPeriodStatus.open>, 
													Or<TaxTran.taxPeriodID, Equal<TaxPeriodEffective.taxPeriodID>>>>>>>>>>>>>>>>>))]
	[PXCacheName(Messages.TaxDetailReport)]
	public partial class TaxDetailByGLReport : PX.Data.IBqlTable
	{
		#region Module
		public abstract class module : PX.Data.IBqlField
		{
		}
		protected String _Module;
		[PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof(TaxTran.module))]
		public virtual String Module
		{
			get
			{
				return this._Module;
			}
			set
			{
				this._Module = value;
			}
		}
		#endregion
		#region TranType
		public abstract class tranType : PX.Data.IBqlField
		{
		}
		protected String _TranType;
		[PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof(TaxTran.tranType))]
		[TaxAdjustmentType.List()]
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
		#region TranTypeInvoiceDiscriminated
		public abstract class tranTypeInvoiceDiscriminated : IBqlField {}
		protected string _TranTypeInvoiceDiscriminated;

		[PXString]
		[PXDBCalced(typeof(Switch<
			Case<
				Where<TaxTran.module, Equal<BatchModule.moduleAP>,
					And<TaxTran.tranType, Equal<APDocType.invoice>>>,
				TaxTranReport.tranTypeInvoiceDiscriminated.apInvoice,
			Case<
				Where<TaxTran.module, Equal<BatchModule.moduleAR>,
					And<TaxTran.tranType, Equal<ARDocType.invoice>>>,
				TaxTranReport.tranTypeInvoiceDiscriminated.arInvoice>>,
			TaxTran.tranType>),
			typeof(string))]
		[LabelList(typeof(TaxTranReport.tranTypeInvoiceDiscriminated))]
		public virtual string TranTypeInvoiceDiscriminated
		{
			get
			{
				return this._TranTypeInvoiceDiscriminated;
			}
			set
			{
				this._TranTypeInvoiceDiscriminated = value;
			}
		}
		#endregion
		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
		}
		protected String _RefNbr;
		[PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof(TaxTran.refNbr))]
		public virtual String RefNbr
		{
			get
			{
				return this._RefNbr;
			}
			set
			{
				this._RefNbr = value;
			}
		}
		#endregion
		#region RecordID
		public abstract class recordID : PX.Data.IBqlField
		{
		}
		protected Int32? _RecordID;
		[PXDBInt(IsKey = true, BqlField = typeof(TaxTran.recordID))]
		public virtual Int32? RecordID
		{
			get
			{
				return this._RecordID;
			}
			set
			{
				this._RecordID = value;
			}
		}
		#endregion
		#region Released
		public abstract class released : PX.Data.IBqlField
		{
		}
		protected Boolean? _Released;
		[PXDBBool(BqlField = typeof(TaxTran.released))]
		public virtual Boolean? Released
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
		#region Voided
		public abstract class voided : PX.Data.IBqlField
		{
		}
		protected Boolean? _Voided;
		[PXDBBool(BqlField = typeof(TaxTran.voided))]
		public virtual Boolean? Voided
		{
			get
			{
				return this._Voided;
			}
			set
			{
				this._Voided = value;
			}
		}
		#endregion
		#region TaxPeriodID
		public abstract class taxPeriodID : PX.Data.IBqlField
		{
		}
		protected String _TaxPeriodID;
		[GL.FinPeriodID(BqlField = typeof(TaxPeriodEffective.taxPeriodID))]
		public virtual String TaxPeriodID
		{
			get
			{
				return this._TaxPeriodID;
			}
			set
			{
				this._TaxPeriodID = value;
			}
		}
		#endregion
		#region TaxID
		public abstract class taxID : PX.Data.IBqlField
		{
		}
		protected string _TaxID;
		[PXDBString(Tax.taxID.Length, IsUnicode = true, IsKey = true, BqlField = typeof(TaxTran.taxID))]
		[PXSelector(typeof(Tax.taxID), DescriptionField = typeof(Tax.descr))]
		public virtual String TaxID
		{
			get
			{
				return this._TaxID;
			}
			set
			{
				this._TaxID = value;
			}
		}
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[Vendor(BqlField = typeof(TaxTran.vendorID))]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}
		protected Int32? _BranchID;
		[Branch(BqlField = typeof(TaxTran.branchID))]
		public virtual Int32? BranchID
		{
			get
			{
				return this._BranchID;
			}
			set
			{
				this._BranchID = value;
			}
		}
		#endregion
		#region TaxZoneID
		public abstract class taxZoneID : PX.Data.IBqlField
		{
		}
		protected String _TaxZoneID;
		[PXDBString(10, IsUnicode = true, BqlField = typeof(TaxTran.taxZoneID))]
		public virtual String TaxZoneID
		{
			get
			{
				return this._TaxZoneID;
			}
			set
			{
				this._TaxZoneID = value;
			}
		}
		#endregion
		#region AccountID
		public abstract class accountID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountID;
		[Account(null, BqlField = typeof(TaxTran.accountID))]
		public virtual Int32? AccountID
		{
			get
			{
				return this._AccountID;
			}
			set
			{
				this._AccountID = value;
			}
		}
		#endregion
		#region SubID
		public abstract class subID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubID;
		[SubAccount(typeof(TaxTran.accountID), BqlField = typeof(TaxTran.subID))]
		public virtual Int32? SubID
		{
			get
			{
				return this._SubID;
			}
			set
			{
				this._SubID = value;
			}
		}
		#endregion
		#region TranDate
		public abstract class tranDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _TranDate;
		[PXDBDate(BqlField = typeof(TaxTran.tranDate))]
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
		#region TaxType
		public abstract class taxType : PX.Data.IBqlField
		{
		}
		protected String _TaxType;
		[PXDBString(1, IsFixed = true, BqlField = typeof(TaxTran.taxType))]
		public virtual String TaxType
		{
			get
			{
				return this._TaxType;
			}
			set
			{
				this._TaxType = value;
			}
		}
		#endregion
		#region TaxRate
		public abstract class taxRate : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxRate;
		[PXDBDecimal(6, BqlField = typeof(TaxTran.taxRate))]
		public virtual Decimal? TaxRate
		{
			get
			{
				return this._TaxRate;
			}
			set
			{
				this._TaxRate = value;
			}
		}
		#endregion
		#region TaxableAmt
		public abstract class taxableAmt : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxableAmt;
		[PXDBBaseCury(BqlField = typeof(TaxTran.taxableAmt))]
		public virtual decimal? TaxableAmt
		{
			get
			{
				return this._TaxableAmt;
			}
			set
			{
				this._TaxableAmt = value;
			}
		}
		#endregion
		#region TaxAmt
		public abstract class taxAmt : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxAmt;
		[PXDBBaseCury(BqlField = typeof(TaxTran.taxAmt))]
		public virtual decimal? TaxAmt
		{
			get
			{
				return this._TaxAmt;
			}
			set
			{
				this._TaxAmt = value;
			}
		}
		#endregion
		#region TaxableAmtIO
		public abstract class taxableAmtIO : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxableAmtIO;
		[PXDBBaseCury(BqlField = typeof(TaxTran.taxableAmt))]
		public virtual decimal? TaxableAmtIO
		{
			[PXDependsOnFields(typeof(module), typeof(tranType), typeof(taxType), typeof(taxableAmt))]
			get
			{
				return ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, 1) * this._TaxableAmt;
			}
			set
			{
				this._TaxableAmtIO = value;
			}
		}
		#endregion
		#region TaxAmtIO
		public abstract class taxAmtIO : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxAmtIO;
		[PXDBBaseCury(BqlField = typeof(TaxTran.taxAmt))]
		public virtual decimal? TaxAmtIO
		{
			[PXDependsOnFields(typeof(module), typeof(tranType), typeof(taxType), typeof(taxAmt))]
			get
			{
				return ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, 1) * this._TaxAmt;
			}
			set
			{
				this._TaxAmtIO = value;
			}
		}
		#endregion
	}


	[PX.Objects.GL.TableAndChartDashboardType]
	public class ReportTaxDetail : PXGraph<ReportTaxDetail>
	{
		public PXSelect<BAccount> dummy_baccount;
		public PXCancel<TaxHistoryMaster> Cancel;
		public PXFilter<TaxHistoryMaster> History_Header;

		public PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Current<TaxHistoryMaster.vendorID>>, And<TaxReportLine.lineNbr, Equal<Current<TaxHistoryMaster.lineNbr>>>>> TaxReportLine_Select;
		[PXFilterable]
		public PXSelectJoin<TaxTranReport,
			LeftJoin<TaxBucketLine,
				On<TaxBucketLine.vendorID, Equal<TaxTranReport.vendorID>, And<TaxBucketLine.bucketID, Equal<TaxTranReport.taxBucketID>>>>,
			Where<boolFalse, Equal<boolTrue>>> History_Detail;
		public PXSelectJoin<TaxReportLine,
			InnerJoin<TaxBucketLine,
						 On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>,
						And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>>>,
		  InnerJoin<TaxTranReport,
						 On<TaxTranReport.vendorID, Equal<TaxBucketLine.vendorID>,
						And<TaxTranReport.taxBucketID, Equal<TaxBucketLine.bucketID>,
						And<Where<TaxReportLine.taxZoneID, IsNull,
									And<TaxReportLine.tempLine, Equal<boolFalse>,
									 Or<TaxReportLine.taxZoneID, Equal<TaxTranReport.taxZoneID>>>>>>>,
			LeftJoin<BAccount,
						On<BAccount.bAccountID, Equal<TaxTranReport.bAccountID>>>>>,
			Where<TaxReportLine.vendorID, Equal<Current<TaxHistoryMaster.vendorID>>,
				And<TaxReportLine.lineNbr, Equal<Current<TaxHistoryMaster.lineNbr>>,
				And<TaxTranReport.taxPeriodID, Equal<Current<TaxHistoryMaster.taxPeriodID>>,
				And<TaxTranReport.released, Equal<boolTrue>,
				And<TaxTranReport.voided, Equal<boolFalse>>>>>>> History_Detail_Expanded;

		public PXAction<TaxHistoryMaster> viewBatch;
		public PXAction<TaxHistoryMaster> viewDocument;

		public TaxReportLine taxReportLine
		{
			get
			{
				return TaxReportLine_Select.Select();
			}
		}

		public ReportTaxDetail()
		{
			if (Company.Current.BAccountID.HasValue == false)
			{
				throw new PXSetupNotEnteredException(ErrorMessages.SetupNotEntered, typeof(Branch), PXMessages.LocalizeNoPrefix(CS.Messages.BranchMaint));
			}

			History_Detail.Cache.AllowInsert = false;
			History_Detail.Cache.AllowUpdate = false;
			History_Detail.Cache.AllowDelete = false;

			FieldDefaulting.AddHandler<BAccountR.type>((sender, e) => { if (e.Row != null) e.NewValue = BAccountType.VendorType; });

			History_Header.Cache.IsDirty = false;
		}

		public PXSetup<Branch> Company;

		public virtual IEnumerable history_Detail()
		{
			using (new PXReadBranchRestrictedScope(History_Header.Current.BranchID))
			foreach (PXResult<TaxReportLine, TaxBucketLine, TaxTranReport, BAccount> res in History_Detail_Expanded.Select(History_Header.Current.BranchID))
			{
				TaxBucketLine line = res;
				TaxTranReport tran = (TaxTranReport)this.Caches[typeof(TaxTranReport)].CreateCopy((TaxTranReport)res);
				BAccount baccount = res;

				decimal HistMult = 0m;

				if (TaxReportLine_Select.Current != null)
				{
					HistMult = ReportTaxProcess.GetMult(tran.Module, tran.TranType, tran.TaxType, TaxReportLine_Select.Current.LineMult);
				}

				tran.ReportTaxAmt = HistMult * tran.ReportTaxAmt;
				tran.ReportTaxableAmt = HistMult * tran.ReportTaxableAmt;

				yield return new PXResult<TaxTranReport, TaxBucketLine, BAccount>(tran, line, baccount);
			}
		}

		protected virtual void TaxHistoryMaster_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			int? firstLine = null;
			if (History_Header.Current != null && TaxReportLine_Select.Current != null && History_Header.Current.LineNbr != TaxReportLine_Select.Current.LineNbr)
			{
				TaxReportLine_Select.Current = null;
			}

			if (History_Header.Current != null && History_Header.Current.VendorID != null)
			{
				List<int> AllowedValues = new List<int>();
				List<string> AllowedLabels = new List<string>();

				foreach (TaxReportLine line in PXSelectReadonly<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Current<TaxHistoryMaster.vendorID>>>>.Select(this, null))
				{
					AllowedValues.Add(line.LineNbr.GetValueOrDefault());
					StringBuilder bld = new StringBuilder();
					bld.Append(line.LineNbr.GetValueOrDefault());
					bld.Append("-");
					bld.Append(line.Descr);
					AllowedLabels.Add(bld.ToString());
				}

				if (AllowedValues.Count > 0)
				{
					firstLine = AllowedValues[0];
					PXIntListAttribute.SetList<TaxHistoryMaster.lineNbr>(History_Header.Cache, null, AllowedValues.ToArray(), AllowedLabels.ToArray());
				}
			}

			if (History_Header.Current != null && History_Header.Current.VendorID != null && string.IsNullOrEmpty(History_Header.Current.TaxPeriodID) == false)
			{
				//TODO 111 Pank check
				TaxPeriod per = (TaxPeriod)PXSelectorAttribute.Select<TaxHistoryMaster.taxPeriodID>(History_Header.Cache, History_Header.Current);
				if (per != null)
				{
					History_Header.Current.StartDate = per.StartDate;
					History_Header.Current.EndDate = per.EndDate;
				}
			}
		}

		protected virtual void TaxHistoryMaster_VendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetDefaultExt<TaxHistoryMaster.taxPeriodID>(e.Row);
			sender.SetDefaultExt<TaxHistoryMaster.startDate>(e.Row);
			sender.SetDefaultExt<TaxHistoryMaster.endDate>(e.Row);
		}

		[PXUIField(DisplayName = GL.Messages.ViewBatch, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXLookupButton]
		public virtual IEnumerable ViewBatch(PXAdapter adapter)
		{
			TaxTranReport taxtran = History_Detail.Current;
			if (taxtran != null)
			{
				string batchNbr = null;
				switch (taxtran.Module)
				{
					case BatchModule.AP:
						{
							APRegister apreg = PXSelect<APRegister, Where<APRegister.docType, Equal<Current<TaxTranReport.tranType>>,
								And<APRegister.refNbr, Equal<Current<TaxTranReport.refNbr>>>>>.SelectSingleBound(this, new object[] { taxtran });
							batchNbr = apreg?.BatchNbr;
						}
						break;
					case BatchModule.AR:
						{
							ARRegister arreg = PXSelect<ARRegister, Where<ARRegister.docType, Equal<Current<TaxTranReport.tranType>>,
								And<ARRegister.refNbr, Equal<Current<TaxTranReport.refNbr>>>>>.SelectSingleBound(this, new object[] { taxtran });
							batchNbr = arreg?.BatchNbr;
						}
						break;
					case BatchModule.GL:
						{
							if (taxtran.TranType == TaxTran.tranType.TranForward ||
								taxtran.TranType == TaxTran.tranType.TranReversed)
							{
								batchNbr = taxtran.RefNbr;
							}
							else if ((taxtran.TranType == TaxAdjustmentType.OutputVAT ||
								taxtran.TranType == TaxAdjustmentType.InputVAT) &&
								!string.IsNullOrEmpty(taxtran.TaxInvoiceNbr) &&
								taxtran.TaxInvoiceDate != null)
							{
								SVATConversionHist docSVAT = PXSelect<SVATConversionHist,
									Where<SVATConversionHist.taxRecordID, Equal<Current<TaxTranReport.recordID>>,
										And<SVATConversionHist.processed, Equal<True>>>>.SelectSingleBound(this, new object[] { taxtran });
								batchNbr = docSVAT?.AdjBatchNbr;
							}
							else
							{
								TaxAdjustmentEntry graph = PXGraph.CreateInstance<TaxAdjustmentEntry>();
								TaxAdjustment taxadj = graph.Document.Search<TaxAdjustment.refNbr>(taxtran.RefNbr, taxtran.TranType);
								batchNbr = taxadj?.BatchNbr;
							}
						}
						break;
					case BatchModule.CA:
						{
							CATranEntry graph = PXGraph.CreateInstance<CATranEntry>();
							CAAdj caadj = graph.CAAdjRecords.Search<CAAdj.adjRefNbr>(taxtran.RefNbr);
							if (caadj != null)
							{
								batchNbr = (string)graph.CAAdjRecords.Cache.GetValue<CAAdj.tranID_CATran_batchNbr>(caadj);
							}
						}
						break;
				}

				if (!string.IsNullOrEmpty(batchNbr))
				{
					Batch batch = JournalEntry.FindBatch(this, taxtran.Module, batchNbr);
					if (batch != null)
					{
						JournalEntry.RedirectToBatch(batch);
					}
				}
			}

			return adapter.Get();
		}

		[PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXEditDetailButton]
		public virtual IEnumerable ViewDocument(PXAdapter adapter)
		{
			TaxTranReport taxtran = History_Detail.Current;
			if (taxtran != null)
			{
				switch (taxtran.Module)
				{
					case BatchModule.AP:
						{
							APDocGraphCreator apDocGraphCreator = new APDocGraphCreator();
							PXGraph apDocGraph = apDocGraphCreator.Create(taxtran.TranType, taxtran.RefNbr, null);
							throw new PXRedirectRequiredException(apDocGraph, true, Messages.Document){ Mode = PXBaseRedirectException.WindowMode.NewWindow };
						}
					case BatchModule.AR:
						{
							ARDocGraphCreator arDocGraphCreator = new ARDocGraphCreator();
							PXGraph arDocGraph = arDocGraphCreator.Create(taxtran.TranType, taxtran.RefNbr, null);
							throw new PXRedirectRequiredException(arDocGraph, true, Messages.Document) { Mode = PXBaseRedirectException.WindowMode.NewWindow };
							}
					case BatchModule.GL:
						{
							if (taxtran.TranType == TaxTran.tranType.TranForward ||
								taxtran.TranType == TaxTran.tranType.TranReversed)
							{
								Batch batch = JournalEntry.FindBatch(this, taxtran.Module, taxtran.RefNbr);
								if (batch != null)
								{
									JournalEntry.RedirectToBatch(batch);
								}
							}
							else if ((taxtran.TranType == TaxAdjustmentType.OutputVAT ||
								taxtran.TranType == TaxAdjustmentType.InputVAT) &&
								!string.IsNullOrEmpty(taxtran.TaxInvoiceNbr) &&
								taxtran.TaxInvoiceDate != null)
							{
								ProcessSVATBase graph = PXGraph.CreateInstance<ProcessSVATBase>();
								SVATConversionHist docSVAT = PXSelect<SVATConversionHist, 
									Where<SVATConversionHist.taxRecordID, Equal<Required<SVATConversionHist.taxRecordID>>,
										And<SVATConversionHist.processed, Equal<True>>>>.SelectSingleBound(graph, null, taxtran.RecordID);
								if (docSVAT != null)
								{
									PXRedirectHelper.TryRedirect(graph.SVATDocuments.Cache, docSVAT, Messages.Document, PXRedirectHelper.WindowMode.NewWindow);
								}
							}
							else
							{
								TaxAdjustmentEntry graph = PXGraph.CreateInstance<TaxAdjustmentEntry>();
								TaxAdjustment apdoc = graph.Document.Search<TaxAdjustment.refNbr>(taxtran.RefNbr, taxtran.TranType);
								if (apdoc != null)
								{
									graph.Document.Current = apdoc;
									throw new PXRedirectRequiredException(graph, true, Messages.Document){ Mode = PXBaseRedirectException.WindowMode.NewWindow };
								}
							}
						}
						break;
					case BatchModule.CA:
						{
							CATranEntry graph = PXGraph.CreateInstance<CATranEntry>();
							CAAdj apdoc = graph.CAAdjRecords.Search<CAAdj.adjRefNbr>(taxtran.RefNbr);
							if (apdoc != null)
							{
								graph.CAAdjRecords.Current = apdoc;
								throw new PXRedirectRequiredException(graph, true, Messages.Document) { Mode = PXBaseRedirectException.WindowMode.NewWindow };
							}
						}
						break;
				}
			}
			return History_Header.Select();
		}
	}
	
	[Obsolete("TaxRevisionManager is obsolete and will be removed in Acumatica 8.0.")]
	public static class TaxRevisionManager
	{
		public static int NewRevisionID(PXGraph graph, int? branchID, int? vendorId, String taxperiodID)
		{
			var result = ReportTaxProcess.CurrentRevisionId(graph, branchID, vendorId, taxperiodID);
			return result == null ? 1 : result.Value + 1;
		}

		public static bool HasRevisions(PXGraph graph, int? branchID, int? vendorId, String taxperiodID)
		{
			return ReportTaxProcess.CurrentRevisionId(graph, branchID, vendorId, taxperiodID) != null;
		}
	}

	[PX.Objects.GL.TableAndChartDashboardType]
	public class ReportTaxReview : PXGraph<ReportTax>
	{
		public PXFilter<TaxPeriodFilter> Period_Header;
		public PXCancel<TaxPeriodFilter> Cancel;

		public PXSelectJoin<TaxPeriod,
						InnerJoin<Branch,
							On<TaxPeriod.branchID, Equal<Branch.parentBranchID>>>,
						Where<Branch.branchID, Equal<Current<TaxPeriodFilter.branchID>>, 
								And<TaxPeriod.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>,
								And<TaxPeriod.taxPeriodID, Equal<Current<TaxPeriodFilter.taxPeriodID>>>>>> Period;

		public PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Current<TaxPeriodFilter.vendorID>>>> Vendor;
        public PXSelectJoin<TaxReportLine,
            LeftJoin<TaxHistory, On<TaxHistory.vendorID, Equal<TaxReportLine.vendorID>, And<TaxHistory.lineNbr, Equal<TaxReportLine.lineNbr>>>>,
            Where<boolFalse, Equal<boolTrue>>> Period_Details;

		public PXSelectJoinGroupBy<TaxReportLine,
						 LeftJoin<TaxHistory,
									 On<TaxHistory.vendorID, Equal<TaxReportLine.vendorID>,
									And<TaxHistory.lineNbr, Equal<TaxReportLine.lineNbr>,
									And<TaxHistory.taxPeriodID, Equal<Current<TaxPeriodFilter.taxPeriodID>>,
									And2<Where<Current<TaxPeriodFilter.showDifference>, NotEqual<boolTrue>,
												 Or<TaxHistory.revisionID, Equal<Current<TaxPeriodFilter.revisionId>>>>,
									And<Where<Current<TaxPeriodFilter.showDifference>, Equal<boolTrue>,
													Or<TaxHistory.revisionID, LessEqual<Current<TaxPeriodFilter.revisionId>>>>>>>>>>,
						Where<TaxReportLine.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>,
							And<TaxReportLine.tempLine, Equal<False>,
                            And2<Where<TaxReportLine.tempLineNbr, IsNull, Or<TaxHistory.vendorID, IsNotNull>>
                            , And<Where<TaxReportLine.hideReportLine, IsNull, Or<TaxReportLine.hideReportLine, Equal<False>>>>>>>,
						Aggregate<GroupBy<TaxReportLine.lineNbr, Sum<TaxHistory.filedAmt, Sum<TaxHistory.reportFiledAmt>>>>> Period_Details_Expanded;

		[PXFilterable]
		public PXSelect<APInvoice> APDocuments;
		
		public override bool IsDirty
		{
			get
			{
				return false;
			}
		}

		protected virtual void TaxPeriodFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			TaxPeriodFilter filter = e.Row as TaxPeriodFilter;

			if (filter?.BranchID == null || filter.VendorID == null || filter.TaxPeriodID == null)
				return;

			TaxPeriod taxPeriod = Period.Select();

			bool isOpenRevision = taxPeriod?.Status == TaxPeriodStatus.Prepared;
            int? maxRevision = ReportTaxProcess.CurrentRevisionId(sender.Graph, filter.BranchID, filter.VendorID, filter.TaxPeriodID);
			if (maxRevision != null)
			{
				if (maxRevision.Value != filter.RevisionId)
					isOpenRevision = false;
			}
			filter.StartDate = taxPeriod?.StartDateUI;
			filter.EndDate = taxPeriod?.EndDate != null
                ? (DateTime?)(((DateTime)taxPeriod.EndDate).AddDays(-1))
				: null;

			PXUIFieldAttribute.SetEnabled<TaxPeriodFilter.revisionId>(sender, null, maxRevision > 1);
			PXUIFieldAttribute.SetVisible<TaxPeriodFilter.showDifference>(sender, null, maxRevision > 1 && filter.RevisionId > 1);
			PXUIFieldAttribute.SetEnabled<TaxPeriodFilter.taxPeriodID>(sender, null, true);
			voidReport.SetEnabled(isOpenRevision);
			adjustTax.SetEnabled(isOpenRevision);
			closePeriod.SetEnabled(isOpenRevision);

			FinPeriod financialPeriod = FinPeriodIDAttribute.FindFinPeriodByDate(this, filter.EndDate);

			sender.ClearFieldErrors<TaxPeriodFilter.taxPeriodID>(filter);

			if (!string.IsNullOrEmpty(filter.PreparedWarningMsg))
			{
				sender.DisplayFieldWarning<TaxPeriodFilter.taxPeriodID>(
					filter,
					filter.TaxPeriodID,
					filter.PreparedWarningMsg);
			}
			else if (isOpenRevision && financialPeriod == null)
			{
				sender.DisplayFieldWarning<TaxPeriodFilter.taxPeriodID>(
					filter,
					filter.TaxPeriodID,
					Messages.CannotReleaseTaxReportNoFinancialPeriodForPeriodEndDate,
					filter.EndDate?.ToShortDateString(),
					FinPeriodIDAttribute.FormatForError(filter.TaxPeriodID));
			}
			else if (isOpenRevision && financialPeriod.APClosed == true)
			{
				sender.DisplayFieldWarning<TaxPeriodFilter.taxPeriodID>(
					filter,
					filter.TaxPeriodID,
					Messages.FinancialPeriodClosedInAPDocumentsWillBePostedToFirstOpenPeriod,
					filter.EndDate?.ToShortDateString(),
					FinPeriodIDAttribute.FormatForError(filter.TaxPeriodID));
			}
			else if (isOpenRevision && financialPeriod.Active != true)
			{
				sender.DisplayFieldWarning<TaxPeriodFilter.taxPeriodID>(
					filter,
					filter.TaxPeriodID,
					Messages.FinancialPeriodInactiveDocumentsWillBePostedToFirstOpenPeriod,
					filter.EndDate?.ToShortDateString(),
					FinPeriodIDAttribute.FormatForError(filter.TaxPeriodID));
			}

			if (ReportTaxProcess.CheckForUnprocessedSVAT(this, filter.BranchID, Vendor.Select(), taxPeriod?.EndDate))
			{
				sender.RaiseExceptionHandling<TaxPeriodFilter.vendorID>(e.Row, filter.VendorID,
					new PXSetPropertyException(Messages.TaxReportHasUnprocessedSVAT, PXErrorLevel.Warning));
			}
		}
		protected virtual void TaxPeriodFilter_VendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetValue<TaxPeriodFilter.taxPeriodID>(e.Row, null);
		}

		protected virtual void TaxPeriodFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			TaxPeriodFilter filter = (TaxPeriodFilter)e.Row;
			if (filter == null) return;

			if (!sender.ObjectsEqual<TaxPeriodFilter.branchID>(e.Row, e.OldRow))
			{
				List<PXView> views = this.Views.Select(view => view.Value).ToList();
				foreach (var view in views) view.Clear();
			}

			if (!sender.ObjectsEqual<TaxPeriodFilter.branchID>(e.Row, e.OldRow)
				|| !sender.ObjectsEqual<TaxPeriodFilter.vendorID>(e.Row, e.OldRow))
			{
				if (filter.BranchID != null && filter.VendorID != null)
			{
					Branch branch = BranchMaint.FindBranchByID(this, filter.BranchID);

					TaxPeriod taxper = TaxYearMaint.FindPreparedPeriod(this, branch.ParentBranchID, filter.VendorID);

				if (taxper != null)
				{
					filter.TaxPeriodID = taxper.TaxPeriodID;
				}
				else
				{
						taxper = TaxYearMaint.FindLastClosedPeriod(this, branch.ParentBranchID, filter.VendorID);
					filter.TaxPeriodID = taxper != null ? taxper.TaxPeriodID : null;
				}
			}
				else
				{
					filter.TaxPeriodID = null;
				}
			}

			if (!sender.ObjectsEqual<TaxPeriodFilter.branchID>(e.Row, e.OldRow)
				|| !sender.ObjectsEqual<TaxPeriodFilter.vendorID>(e.Row, e.OldRow)
				|| !sender.ObjectsEqual<TaxPeriodFilter.taxPeriodID>(e.Row, e.OldRow))
			{
				if (filter.BranchID != null && filter.VendorID != null && filter.TaxPeriodID != null)
				{
					filter.RevisionId = ReportTaxProcess.CurrentRevisionId(this, filter.BranchID, filter.VendorID, filter.TaxPeriodID);
				}
				else
			{
					filter.RevisionId = null;
				}
			}
		}

		protected virtual IEnumerable period_Details()
		{
			using (new PXReadBranchRestrictedScope(Period_Header.Current.BranchID))
			{
				return Period_Details_Expanded.Select().Where(line => ShowTaxReportLine(line.GetItem<TaxReportLine>(), Period_Header.Current.TaxPeriodID));				
			}
		}

		protected virtual IEnumerable apdocuments()
		{
			Branch selectedBranch = BranchMaint.FindBranchByID(this, Period_Header.Current.BranchID);

			if (selectedBranch == null)
				return new object[0];

			int[] branchesIDsToLoad = null;

			branchesIDsToLoad = selectedBranch.IsParentOrSeparate()
									? PXAccess.GetChildBranchIDs(selectedBranch.BranchCD)
									: new[] {selectedBranch.BranchID.Value};

			return PXSelect<APInvoice,
						Where<APInvoice.docDate, GreaterEqual<Current<TaxPeriodFilter.startDate>>,
							And<APInvoice.docDate, LessEqual<Current<TaxPeriodFilter.endDate>>,
							And<APInvoice.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>,
							And<APInvoice.branchID, In<Required<APInvoice.branchID>>,
							And<Where<APInvoice.docType, Equal<APDocType.invoice>,
										Or<APInvoice.docType, Equal<APDocType.debitAdj>>>>>>>>>
						.Select(this, branchesIDsToLoad);
	}

		public virtual bool ShowTaxReportLine(TaxReportLine taxReportLine, string taxPeriodID)
		{
			return true;
		}

		public PXAction<TaxPeriodFilter> adjustTax;
		[PXUIField(DisplayName = Messages.AdjustTax, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton]
		public virtual IEnumerable AdjustTax(PXAdapter adapter)
		{
			TaxAdjustmentEntry graph = PXGraph.CreateInstance<TaxAdjustmentEntry>();
			graph.Clear();

			TaxAdjustment newDoc = graph.Document.Insert(new TaxAdjustment());
			newDoc.VendorID = this.Period_Header.Current.VendorID;
			graph.Document.Cache.RaiseFieldUpdated<TaxAdjustment.vendorID>(newDoc, null);

			if (Period_Header.Current.BranchID != null)
			{
				Branch branch = BranchMaint.FindBranchByID(this, Period_Header.Current.BranchID);
				Ledger ledger = GeneralLedgerMaint.FindLedgerByID(this, branch.LedgerID);

				if (ledger == null)
				{
					newDoc.BranchID = null;
				}
				else if (ledger.DefBranchID != branch.BranchID)
				{
					newDoc.BranchID = branch.BranchID;
				}
				else
				{
					Branch currentBranch = BranchMaint.FindBranchByID(this, Accessinfo.BranchID);

					if (currentBranch != null && currentBranch.ParentBranchID == branch.BranchID)
					{
						newDoc.BranchID = currentBranch.BranchID;
					}
					else
					{
						newDoc.BranchID = null;
					}
				}

				graph.Document.Cache.RaiseFieldUpdated<TaxAdjustment.branchID>(newDoc, null);
			}

			throw new PXRedirectRequiredException(graph, Messages.NewAdjustment);
		}

		public PXAction<TaxPeriodFilter> voidReport;
		[PXUIField(DisplayName = Messages.VoidReport, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public virtual IEnumerable VoidReport(PXAdapter adapter)
		{
			TaxPeriodFilter tp = Period_Header.Current;
			PXLongOperation.StartOperation(this, () => ReportTaxReview.VoidReportProc(tp));
			return adapter.Get();
		}

		public PXAction<TaxPeriodFilter> closePeriod;
		[PXUIField(DisplayName = Messages.Release, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public virtual IEnumerable ClosePeriod(PXAdapter adapter)
		{
			TaxPeriodFilter tp = Period_Header.Current;
			PXLongOperation.StartOperation(this, () => ReportTaxReview.ClosePeriodProc(tp));
			return adapter.Get();
		}

		public PXAction<TaxPeriodFilter> viewDocument;
		[PXUIField(DisplayName = Messages.ViewDocuments, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXButton]
		public virtual IEnumerable ViewDocument(PXAdapter adapter)
		{
			if (this.Period_Details.Current != null)
			{
                ReportTaxDetail graph = PXGraph.CreateInstance<ReportTaxDetail>();
				TaxHistoryMaster filter = new TaxHistoryMaster();
				filter.BranchID = Period_Header.Current.BranchID;
				filter.VendorID = Period_Header.Current.VendorID;
				filter.TaxPeriodID = Period_Header.Current.TaxPeriodID;
				filter.LineNbr = Period_Details.Current.LineNbr;
				graph.History_Header.Insert(filter);
				throw new PXRedirectRequiredException(graph, Messages.ViewDocuments);
			}
			return Period_Header.Select();
		}

		public PXAction<TaxPeriodFilter> checkDocument;
		[PXUIField(DisplayName = Messages.Document, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
		[PXButton(ImageKey = PX.Web.UI.Sprite.Main.Inquiry)]
		public virtual IEnumerable CheckDocument(PXAdapter adapter)
		{
			if (APDocuments.Current != null)
			{
				APInvoiceEntry graph = PXGraph.CreateInstance<APInvoiceEntry>();
				APInvoice apdoc = graph.Document.Search<APInvoice.refNbr>(APDocuments.Current.RefNbr, APDocuments.Current.DocType);
				if (apdoc != null)
				{
					graph.Document.Current = apdoc;
					throw new PXRedirectRequiredException(graph, true, Messages.Document) { Mode = PXBaseRedirectException.WindowMode.NewWindow };
				}
			}
			return Period_Header.Select();
		}


		public static void VoidReportProc(TaxPeriodFilter p)
		{
			ReportTaxProcess fp = PXGraph.CreateInstance<ReportTaxProcess>();
			fp.VoidReportProc(p);
            ReportTax docgraph = PXGraph.CreateInstance<ReportTax>();
			docgraph.TimeStamp = fp.TimeStamp;
			docgraph.Period_Header.Insert();
			docgraph.Period_Header.Current.VendorID = p.VendorID;
			docgraph.Period_Header.Current.BranchID = p.BranchID;
			docgraph.Period_Header.Update(docgraph.Period_Header.Current);
			throw new PXRedirectRequiredException(docgraph, Messages.Report);
		}

		public static void ClosePeriodProc(TaxPeriodFilter p)
		{
			ReportTaxProcess fp = PXGraph.CreateInstance<ReportTaxProcess>();
			fp.ClosePeriodProc(p);
		}

		public static void ReleaseDoc(List<TaxAdjustment> list)
		{
			ReportTaxProcess rg = PXGraph.CreateInstance<ReportTaxProcess>();
			JournalEntry je = PXGraph.CreateInstance<JournalEntry>();
			PostGraph pg = PXGraph.CreateInstance<PostGraph>();
			List<Batch> batchlist = new List<Batch>();
			List<int> batchbind = new List<int>();

			list.Sort(new Comparison<TaxAdjustment>(delegate(TaxAdjustment a, TaxAdjustment b)
				{
					object aBranchID = a.BranchID;
					object bBranchID = b.BranchID;
					int ret = ((IComparable)aBranchID).CompareTo(bBranchID);

					if (ret != 0)
					{
						return ret;
					}
                    object aFinPeriodID = a.FinPeriodID;
                    object bFinPeriodID = b.FinPeriodID;
                    ret = ((IComparable)aFinPeriodID).CompareTo(bFinPeriodID);

					return ret;
				}
			));

			for (int i = 0; i < list.Count; i++)
			{
				TaxAdjustment doc = list[i];
				rg.ReleaseDocProc(je, doc);
				rg.Clear();

				if (je.BatchModule.Current != null && !batchlist.Contains(je.BatchModule.Current))
				{
					batchlist.Add(je.BatchModule.Current);
					batchbind.Add(i);
				}
			}

			foreach (Batch batch in batchlist)
			{
				pg.TimeStamp = batch.tstamp;
				pg.PostBatchProc(batch);
				pg.Clear();
			}
		}

		public ReportTaxReview()
		{
			APSetup setup = APSetup.Current;
			PXCache cache = Period_Details.Cache;
			cache.AllowDelete = false;
			cache.AllowInsert = false;
			cache.AllowUpdate = false;
			cache = APDocuments.Cache;
			cache.AllowDelete = false;
			cache.AllowInsert = false;
			cache.AllowUpdate = false;
			FieldDefaulting.AddHandler<BAccountR.type>((sender, e) => { if (e.Row != null) e.NewValue = BAccountType.VendorType; });
		}

		public PXSetup<APSetup> APSetup;
	}



	[PX.Objects.GL.TableAndChartDashboardType]
	public class ReportTax : PXGraph<ReportTax>
	{
		public PXFilter<TaxPeriodFilter> Period_Header;
		public PXCancel<TaxPeriodFilter> Cancel; 

		public PXSelect<TaxYear> TaxYear_Current;

		public PXSelectJoin<TaxPeriod,
						InnerJoin<Branch,
							On<TaxPeriod.branchID, Equal<Branch.parentBranchID>>>, 
						Where<Branch.branchID, Equal<Current<TaxPeriodFilter.branchID>>, 
							And<TaxPeriod.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>,
							And<TaxPeriod.taxPeriodID, Equal<Current<TaxPeriodFilter.taxPeriodID>>>>>> 
						TaxPeriod_Current;

		[Obsolete("The view is obsolete and will be removed in Acumatica 8.0.")]
		public PXSelect<TaxPeriod,
						Where<TaxPeriod.branchID, Equal<Required<TaxPeriodFilter.branchID>>,
								And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>,
				And<TaxPeriod.startDate, LessEqual<Required<TaxPeriod.startDate>>,
								And<TaxPeriod.endDate, Greater<Required<TaxPeriod.endDate>>>>>>> 
						TaxPeriod_ByDate;

		[PXFilterable]
        public PXSelectJoin<TaxReportLine,
            LeftJoin<TaxHistory, On<TaxHistory.vendorID, Equal<TaxReportLine.vendorID>, And<TaxHistory.lineNbr, Equal<TaxReportLine.lineNbr>>>>,
            Where<boolFalse, Equal<boolTrue>>> Period_Details;

		public PXSelectJoin<TaxReportLine,
		  LeftJoin<TaxBucketLine,
				On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>,
			   And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>>>,
		  LeftJoin<TaxRev,
				On<TaxRev.taxVendorID, Equal<TaxBucketLine.vendorID>,
				And<TaxRev.taxBucketID, Equal<TaxBucketLine.bucketID>,
				And<TaxRev.outdated, Equal<boolFalse>>>>,
		  LeftJoin<TaxTranReport,
				On<TaxTranReport.taxID, Equal<TaxRev.taxID>,
			   And<TaxTranReport.taxType, Equal<TaxRev.taxType>,
			   And<TaxTranReport.tranDate, Between<TaxRev.startDate, TaxRev.endDate>,
			   And<TaxTranReport.released, Equal<True>,
			   And<TaxTranReport.voided, Equal<False>,
			   And<TaxTranReport.taxPeriodID, IsNull,
			   And<TaxTranReport.origRefNbr, Equal<Empty>,
			   And<TaxTranReport.taxType, NotEqual<TaxType.pendingPurchase>,
			   And<TaxTranReport.taxType, NotEqual<TaxType.pendingSales>,
			   And2<Where<Current<Vendor.taxReportFinPeriod>, Equal<boolTrue>,
					  Or<TaxTranReport.tranDate, Less<Current<TaxPeriod.endDate>>>>,
			   And2<Where<Current<Vendor.taxReportFinPeriod>, Equal<boolFalse>,
					  Or<TaxTranReport.finDate, Less<Current<TaxPeriod.endDate>>>>,
			   And<Where<TaxReportLine.taxZoneID, IsNull,
					 And<TaxReportLine.tempLine, Equal<boolFalse>,
					  Or<TaxReportLine.taxZoneID, Equal<TaxTranReport.taxZoneID>>>>>>>>>>>>>>>>,
		  LeftJoin<Currency,
					   On<Currency.curyID, Equal<Current<Vendor.curyID>>>>>>>,
		  Where<TaxReportLine.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>>,
		  OrderBy<Asc<TaxReportLine.vendorID, Asc<TaxReportLine.lineNbr>>>> Period_Details_Expanded;

		public PXSetup<Vendor, Where<Vendor.bAccountID, Equal<Current<TaxPeriodFilter.vendorID>>>> Vendor;

		[Obsolete("The view is obsolete and will be removed in Acumatica 8.0.")]
		public PXSelectJoin<TaxTranReport,
				InnerJoin<Tax, On<Tax.taxID, Equal<TaxTranReport.taxID>>,
				InnerJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<Tax.taxVendorID>>,
				InnerJoin<TaxBucketLine,
					On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>,
				 And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>,
				 And<TaxBucketLine.bucketID, Equal<TaxTranReport.taxBucketID>>>>>>>,
				Where<Tax.taxVendorID, Equal<Required<TaxPeriodFilter.vendorID>>,
					And<TaxTranReport.released, Equal<True>, 
					And<TaxTranReport.voided, Equal<False>, 
					And<TaxTranReport.taxPeriodID, IsNull,
					And<TaxTranReport.taxType, NotEqual<TaxType.pendingPurchase>,
					And<TaxTranReport.taxType, NotEqual<TaxType.pendingSales>,
					And<TaxTranReport.origRefNbr, Equal<Empty>>>>>>>>,
				OrderBy<Asc<TaxTranReport.tranDate>>> OldestNotReportedTaxTran;

		public PXSelect<TaxHistory,
			Where<TaxHistory.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>,
				And<TaxHistory.taxPeriodID, Equal<Current<TaxPeriodFilter.taxPeriodID>>>>,
						OrderBy<Desc<TaxHistory.revisionID>>> 
						History_Last;

		public PXSetup<Company> company;

		protected TaxCalendar<TaxYear, TaxPeriod> TaxCalendar;

		public static TaxTran GetEarliestNotReportedTaxTran(PXGraph graph, Vendor taxAgency)
		{
			return taxAgency.TaxReportFinPeriod == true 
				? GetEarliestNotReportedTaxTranByField<TaxTran.finDate>(graph, taxAgency.BAccountID) 
				: GetEarliestNotReportedTaxTranByField<TaxTran.tranDate>(graph, taxAgency.BAccountID);
		}

		public static TaxTran GetEarliestNotReportedTaxTranByField<TOrderByField>(PXGraph graph, int? taxAgencyID)
			where TOrderByField: IBqlField
		{
			return PXSelectJoin<TaxTran,
								InnerJoin<Tax, 
										On<Tax.taxID, Equal<TaxTran.taxID>>,
								InnerJoin<TaxReportLine, 
										On<TaxReportLine.vendorID, Equal<Tax.taxVendorID>>,
								InnerJoin<TaxBucketLine,
										On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>,
											And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>,
											And<TaxBucketLine.bucketID, Equal<TaxTran.taxBucketID>>>>>>>,
								Where<Tax.taxVendorID, Equal<Required<TaxPeriodFilter.vendorID>>,
										And<TaxTran.released, Equal<True>,
										And<TaxTran.voided, Equal<False>,
										And<TaxTran.taxPeriodID, IsNull,
										And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>,
										And<TaxTran.taxType, NotEqual<TaxType.pendingSales>,
										And<TaxTran.origRefNbr, Equal<Empty>>>>>>>>,
								OrderBy<Asc<TOrderByField>>>
								.SelectWindowed(graph, 0, 1, taxAgencyID);
		}

		protected virtual void TaxPeriodFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			TaxPeriodFilter filter = (TaxPeriodFilter)e.Row;
			if (filter == null)
				return;

			filter.StartDate = null;
			filter.EndDate = null;
			fileTax.SetEnabled(false);
			sender.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>(e.Row, filter.TaxPeriodID, null);
			sender.RaiseExceptionHandling<TaxPeriodFilter.vendorID>(e.Row, filter.VendorID, null);
			PXUIFieldAttribute.SetEnabled<TaxPeriodFilter.taxPeriodID>(sender, null, Vendor.Current != null && Vendor.Current.UpdClosedTaxPeriods == true);

			TaxPeriod taxper = TaxPeriod_Current.SelectSingle();
			if (taxper?.TaxPeriodID == null)
				return;

			bool allowProcess = (e.Row != null && taxper.VendorID != null);
			filter.StartDate = taxper.StartDateUI;
			filter.EndDate = (((DateTime)taxper.EndDate).AddDays(-1));

			if (allowProcess && taxper.Status == TaxPeriodStatus.Prepared)
			{
				int? maxRevision = ReportTaxProcess.CurrentRevisionId(sender.Graph, filter.BranchID, filter.VendorID, filter.TaxPeriodID);

				using (new PXReadBranchRestrictedScope(filter.BranchID))
				{
					TaxHistory history = History_Last.SelectWindowed(0, 1);
					if (history?.RevisionID == maxRevision)
					{
						allowProcess = false;
				}
			}
			}

			Vendor vendor = Vendor.SelectWindowed(0, 1, taxper.VendorID);
			if (vendor == null)
				return;

			Branch branch = BranchMaint.FindBranchByID(this, filter.BranchID);

			string taxPeriodIDForMessage = (string)Period_Header.GetValueExt<TaxPeriodFilter.taxPeriodID>(filter);
			TaxPeriod preparedPeriod = TaxYearMaint.FindPreparedPeriod(this, branch.ParentBranchID, filter.VendorID);

			if (preparedPeriod != null && preparedPeriod.TaxPeriodID != taxper.TaxPeriodID)
			{
				sender.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>(e.Row, taxPeriodIDForMessage,
					new PXSetPropertyException<TaxPeriodFilter.taxPeriodID>(Messages.CannotPrepareReportExistPrepared, PXErrorLevel.Error));

				allowProcess = false;
			}

			if (allowProcess)
			{
				if (taxper.Status != TaxPeriodStatus.Dummy 
					&& ReportTaxProcess.CheckForUnprocessedSVAT(this, filter.BranchID, vendor, taxper?.EndDate))
					{
						sender.RaiseExceptionHandling<TaxPeriodFilter.vendorID>(e.Row, filter.VendorID,
							new PXSetPropertyException(Messages.TaxReportHasUnprocessedSVAT, PXErrorLevel.Warning));
					}

					switch (taxper.Status)
					{
						case TaxPeriodStatus.Dummy:
							sender.RaiseExceptionHandling<TaxPeriodFilter.vendorID>(e.Row, taxper.VendorID,
								new PXSetPropertyException<TaxPeriodFilter.vendorID>(Messages.TaxAgencyWithoutTran, PXErrorLevel.Warning));
							break;
						case TaxPeriodStatus.Closed:
							{
							TaxTran tran = null;
							using (new PXReadBranchRestrictedScope(filter.BranchID))
								{
								tran = GetEarliestNotReportedTaxTran(this, vendor);
								}
								if (tran != null && tran.TranDate != null && 
									(vendor.TaxReportFinPeriod != true && taxper.StartDate > tran.TranDate ||
									vendor.TaxReportFinPeriod == true && taxper.StartDate > tran.FinDate))
								{
								sender.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>(e.Row, taxPeriodIDForMessage,
										new PXSetPropertyException<TaxPeriodFilter.taxPeriodID>(
										Messages.OneOrMoreTaxTransactionsFromPreviousPeriodsWillBeReported, PXErrorLevel.Warning));
								}

								if (tran == null || tran.TranDate == null ||
									(vendor.TaxReportFinPeriod != true && taxper.EndDate <= tran.TranDate ||
										vendor.TaxReportFinPeriod == true && taxper.EndDate <= tran.FinDate))
								{
								sender.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>(e.Row, taxPeriodIDForMessage,
																																						 new PXSetPropertyException<TaxPeriodFilter.taxPeriodID>(
										Messages.NoAdjustmentToReportedTaxPeriodWillBeMade, PXErrorLevel.Warning));

									allowProcess = false;
								}
							}
							break;
						default:
							{
							TaxTran tran = null;
							using (new PXReadBranchRestrictedScope(filter.BranchID))
								{
								tran = GetEarliestNotReportedTaxTran(this, vendor);
								}

								if (tran == null)
									break;

								if (taxper.Status == TaxPeriodStatus.Open)
								{
									TaxPeriod period = PXSelect<TaxPeriod,
															Where<TaxPeriod.branchID, Equal<Required<TaxPeriod.branchID>>, 
																	And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>,
																	And<TaxPeriod.taxPeriodID, Less<Required<TaxPeriod.taxPeriodID>>,
																	And<TaxPeriod.status, Equal<TaxPeriodStatus.open>>>>>>
															.SelectWindowed(this, 0, 1, branch.ParentBranchID, filter.VendorID, filter.TaxPeriodID);

									if (period != null && vendor.UpdClosedTaxPeriods != true &&
										(vendor.TaxReportFinPeriod != true && taxper.StartDate > tran.TranDate ||
											vendor.TaxReportFinPeriod == true && taxper.StartDate > tran.FinDate))
									{
									sender.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>(e.Row, taxPeriodIDForMessage,
																																							 new PXSetPropertyException<TaxPeriodFilter.taxPeriodID>(
																																								Messages.CannotPrepareReportPreviousOpen,
																																								PXErrorLevel.Error));

										allowProcess = false;
									}
								}

								if (allowProcess)
								{
									if (tran.TranDate != null && 
										(vendor.TaxReportFinPeriod != true && taxper.StartDate > tran.TranDate ||
										vendor.TaxReportFinPeriod == true && taxper.StartDate > tran.FinDate))
									{
									sender.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>(e.Row, taxPeriodIDForMessage,
											new PXSetPropertyException<TaxPeriodFilter.taxPeriodID>(
																																								Messages.OneOrMoreTaxTransactionsFromPreviousPeriodsWillBeReported,
																																								PXErrorLevel.Warning));
									}
								}
							}
							break;
					}
				}

			fileTax.SetEnabled(allowProcess);
		}

		protected virtual void TaxPeriodFilter_VendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			sender.SetValue<TaxPeriodFilter.taxPeriodID>(e.Row, null);
		}
        
		protected virtual void TaxPeriodFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			TaxPeriodFilter filter = (TaxPeriodFilter)e.Row;

			if (filter == null)
				return;

			if (filter.BranchID == null || filter.VendorID == null)
					{
				filter.TaxPeriodID = null;
				filter.EndDate = null;
				filter.StartDate = null;
				filter.RevisionId = null;
				filter.ShowDifference = null;
				filter.PreparedWarningMsg = null;

				return;
					}

			TaxPeriod taxper = TaxPeriod_Current.SelectSingle();

			if (!sender.ObjectsEqual<TaxPeriodFilter.branchID, TaxPeriodFilter.vendorID>(e.Row, e.OldRow))
			{
				List<PXView> views = this.Views.Select(view => view.Value).ToList();
				foreach (var view in views) view.Clear();
				this.Caches[typeof(TaxPeriod)].Clear();
				this.Caches[typeof(TaxPeriod)].ClearQueryCache();
				this.Caches[typeof(TaxYear)].Clear();

				History_Last.View.Clear();
				
				Branch branch = BranchMaint.FindBranchByID(this, filter.BranchID);

				taxper = TaxCalendar.GetOrCreateCurrentTaxPeriod(TaxYear_Current.Cache, TaxPeriod_Current.Cache, branch.ParentBranchID, filter.VendorID);
					filter.TaxPeriodID = taxper?.TaxPeriodID ?? filter.TaxPeriodID;
				}
			}

		protected virtual void TaxPeriodFilter_TaxPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			e.Cancel = true;
		}

		public override bool IsDirty
		{
			get
			{
				return false;
			}
		}

		protected virtual IEnumerable period_Details()
		{
			TaxReportLine prev_line = null;
			TaxHistory hist = null;
			PXResultset<TaxReportLine, TaxHistory> ret = new PXResultset<TaxReportLine, TaxHistory>();
			TaxPeriod_Current.Current = TaxPeriod_Current.SelectSingle();

			if (Period_Header.Current.TaxPeriodID == null)
				return ret;

			Vendor vendor = Vendor.Current;
			using (new PXReadBranchRestrictedScope(Period_Header.Current.BranchID))
			{
				var results = Period_Details_Expanded
				  .Select()
				  .Where(line => ShowTaxReportLine(line.GetItem<TaxReportLine>(), Period_Header.Current.TaxPeriodID));

				var trans = results.RowCast<TaxTranReport>();
				var dates = trans.Where(t => t.TranDate.HasValue).Select(t => t.TranDate.Value).OrderBy(t => t);

				CurrencyRatesProvider ratesProvider = null;
				bool provideRates = Vendor.Current.CuryRateTypeID != null && Vendor.Current.CuryID != null;

				if (provideRates == true)
				{
					ratesProvider = new CurrencyRatesProvider(Vendor.Current.CuryRateTypeID, Vendor.Current.CuryID);
					ratesProvider.Fill(this, trans.Select(t => t.CuryID).Where(v => v != null).Distinct(), dates.FirstOrDefault(), dates.LastOrDefault());
				}

				foreach (PXResult<TaxReportLine, TaxBucketLine, TaxRev, TaxTranReport, Currency> res in results)
				{
					TaxReportLine line = res;
					TaxTranReport tran = res;
					Currency cury = res;
					CurrencyRate rate = tran.TranDate != null && provideRates == true ? ratesProvider.GetRate(tran.CuryID, tran.TranDate.Value) : null;

					if (object.Equals(prev_line, line) == false || hist == null)
					{
						if (hist != null)
						{
							ret.Add(new PXResult<TaxReportLine, TaxHistory>(prev_line, hist));
						}

						hist = new TaxHistory();
						hist.BranchID = null;
						hist.VendorID = line.VendorID;
						hist.LineNbr = line.LineNbr;
						hist.TaxPeriodID = Period_Header.Current.TaxPeriodID;
						hist.UnfiledAmt = 0m;
						hist.ReportUnfiledAmt = 0m;
					}

					if (tran.RefNbr != null && tran.TaxType != TaxType.PendingSales && tran.TaxType != TaxType.PendingPurchase)
					{
						decimal HistMult = ReportTaxProcess.GetMult(tran.Module, tran.TranType, tran.TaxType, line.LineMult);

						switch (line.LineType)
						{
							case TaxReportLineType.TaxAmount:
								hist.UnfiledAmt += HistMult * tran.TaxAmt.GetValueOrDefault();
								hist.ReportUnfiledAmt += HistMult *
								  (cury.CuryID == tran.CuryID ? tran.CuryTaxAmt.GetValueOrDefault() :
								   cury.CuryID == company.Current.BaseCuryID || cury.CuryID == null || rate == null ? tran.TaxAmt.GetValueOrDefault() :
								   TaxHistorySumManager.RecalcCurrency(cury, rate, tran.CuryTaxAmt.GetValueOrDefault()));
								break;
							case TaxReportLineType.TaxableAmount:
								hist.UnfiledAmt += HistMult * tran.TaxableAmt.GetValueOrDefault();
								hist.ReportUnfiledAmt += HistMult *
									(cury.CuryID == tran.CuryID ? tran.CuryTaxableAmt.GetValueOrDefault() :
								   cury.CuryID == company.Current.BaseCuryID || cury.CuryID == null || rate == null ? tran.TaxableAmt.GetValueOrDefault() :
									 TaxHistorySumManager.RecalcCurrency(cury, rate, tran.CuryTaxableAmt.GetValueOrDefault()));
								break;
						}
					}
					if (line.TempLine == true || line.TempLineNbr != null && hist.ReportUnfiledAmt.GetValueOrDefault() == 0)
						hist = null;
					else
						prev_line = line;
				}
			}

			if (hist != null)
			{
				ret.Add(new PXResult<TaxReportLine, TaxHistory>(prev_line, hist));
			}
			ret = (vendor != null && vendor.TaxUseVendorCurPrecision != true) ?
			  TaxHistorySumManager.GetPreviewReport(this, vendor, ret, (line) => ShowTaxReportLine(line, Period_Header.Current.TaxPeriodID)) : ret;
			PXResultset<TaxReportLine, TaxHistory> result = new PXResultset<TaxReportLine, TaxHistory>();
			foreach (PXResult<TaxReportLine, TaxHistory> pxResult in ret)
			{
				TaxReportLine line = pxResult;
				if (line.HideReportLine != true)
				{
					result.Add(pxResult);
				}
			}
			return result;
		}

		public virtual bool ShowTaxReportLine(TaxReportLine taxReportLine, string taxPeriodID)
		{
			return true;
		}

		public PXAction<TaxPeriodFilter> fileTax;
		[PXUIField(DisplayName = Messages.PrepareTaxReport, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public virtual IEnumerable FileTax(PXAdapter adapter)
		{
			Actions.PressSave();
			TaxPeriodFilter p = Period_Header.Current;
            TaxReportMaint.TaxBucketAnalizer.CheckTaxAgencySettings(this, p.VendorID == null ? 0 : (int)p.VendorID);
			PXLongOperation.StartOperation(this, () => ReportTax.FileTaxProc(p));
			return adapter.Get();
		}

       public static void FileTaxProc(TaxPeriodFilter p)
		{
			ReportTaxProcess fp = PXGraph.CreateInstance<ReportTaxProcess>();
			string warning = fp.FileTaxProc(p);
			ReportTaxReview docgraph = PXGraph.CreateInstance<ReportTaxReview>();
			docgraph.TimeStamp = fp.TimeStamp;
			docgraph.Period_Header.Insert();
			docgraph.Period_Header.Current.BranchID = p.BranchID;
			docgraph.Period_Header.Current.VendorID = p.VendorID;
			docgraph.Period_Header.Current.TaxPeriodID = p.TaxPeriodID;
			docgraph.Period_Header.Current.RevisionId = ReportTaxProcess.CurrentRevisionId(docgraph, p.BranchID, p.VendorID, p.TaxPeriodID);
			docgraph.Period_Header.Current.PreparedWarningMsg = warning;
			throw new PXRedirectRequiredException(docgraph, Messages.Review);			
		}

		public PXAction<TaxPeriodFilter> viewTaxPeriods;
		[PXUIField(DisplayName = Messages.ViewTaxPeriods, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
		[PXProcessButton]
		public virtual IEnumerable ViewTaxPeriods(PXAdapter adapter)
		{
			TaxPeriodFilter filter = Period_Header.Current;

			if (filter.BranchID != null
			    && filter.VendorID != null)
			{
				TaxYearMaint taxYearMaint = CreateInstance<TaxYearMaint>();

				TaxYearMaint.TaxYearFilter taxYearFilter =
					(TaxYearMaint.TaxYearFilter)
						taxYearMaint.TaxYearFilterSelectView.Cache.CreateCopy(taxYearMaint.TaxYearFilterSelectView.Current);

				Branch branch = BranchMaint.FindBranchByID(this, filter.BranchID);

				taxYearFilter.BranchID = branch.ParentBranchID;
				taxYearFilter.VendorID = filter.VendorID;

				if (TaxPeriod_Current.Current != null)
				{
					taxYearFilter.Year = TaxPeriod_Current.Current.TaxYear;
				}

				taxYearMaint.TaxYearFilterSelectView.Update(taxYearFilter);

				throw new PXRedirectRequiredException(taxYearMaint, true, string.Empty)
				{
					Mode = PXBaseRedirectException.WindowMode.Same
				};
			}

			return adapter.Get();
		}


		public ReportTax()
		{
			APSetup setup = APSetup.Current;

			Period_Details.Cache.AllowInsert = false;
			Period_Details.Cache.AllowUpdate = false;
			Period_Details.Cache.AllowDelete = false;
			FieldDefaulting.AddHandler<BAccountR.type>((sender, e) => { if (e.Row != null) e.NewValue = BAccountType.VendorType; });

			TaxCalendar = new TaxCalendar<TaxYear, TaxPeriod>(this);
		}


		public PXSetup<APSetup> APSetup;
	}

	[PXHidden()]
	public class ReportTaxProcess : PXGraph<ReportTaxProcess>
	{

		public PXSelect<TaxHistory, 
						Where<TaxHistory.vendorID, Equal<Required<TaxHistory.vendorID>>,
							And<TaxHistory.branchID, Equal<Required<TaxHistory.branchID>>, 
							And<TaxHistory.accountID, Equal<Required<TaxHistory.accountID>>,
							And<TaxHistory.subID, Equal<Required<TaxHistory.subID>>, 
							And<TaxHistory.taxID, Equal<Required<TaxHistory.taxID>>, 
							And<TaxHistory.taxPeriodID, Equal<Required<TaxHistory.taxPeriodID>>, 
							And<TaxHistory.lineNbr, Equal<Required<TaxHistory.lineNbr>>, 
							And<TaxHistory.revisionID, Equal<Required<TaxHistory.revisionID>>>>>>>>>>> 
						TaxHistory_Current;

		public PXSelectJoin<TaxReportLine,
			InnerJoin<TaxBucketLine,
						 On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>,
						And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>>>,
			InnerJoin<TaxTranReport, On<TaxTranReport.vendorID, Equal<TaxReportLine.vendorID>,
						And<TaxTranReport.taxBucketID, Equal<TaxBucketLine.bucketID>,
						And<TaxTranReport.revisionID, Equal<Required<TaxTranReport.revisionID>>,
						And<TaxTranReport.released, Equal<boolTrue>,
						And<TaxTranReport.voided, Equal<boolFalse>,
						And<TaxTranReport.taxPeriodID, Equal<Required<TaxTranReport.taxPeriodID>>,
						And<Where<TaxReportLine.taxZoneID, IsNull, And<TaxReportLine.tempLine, Equal<boolFalse>,
									 Or<TaxReportLine.taxZoneID, Equal<TaxTranReport.taxZoneID>>>>>>>>>>>>>,
			 Where<TaxReportLine.vendorID, Equal<Required<TaxReportLine.vendorID>>>,
			 OrderBy<Asc<TaxReportLine.vendorID,
							 Asc<TaxTranReport.branchID,
							 Asc<TaxReportLine.lineNbr,
							 Asc<TaxTranReport.accountID, Asc<TaxTranReport.subID,
							 Asc<TaxTranReport.taxID,
							 Asc<TaxTranReport.taxPeriodID,
							 Asc<TaxTranReport.module, Asc<TaxTranReport.tranType, Asc<TaxTranReport.refNbr>>>>>>>>>>>> Period_Details_Expanded;

		public PXSelect<TaxPeriod, 
							Where<TaxPeriod.branchID, Equal<Required<TaxPeriod.branchID>>, 
									And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, 
									And<TaxPeriod.taxPeriodID, Equal<Required<TaxPeriod.taxPeriodID>>>>>> 
							TaxPeriod_Current;

		public PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>> Vendor_Current;

		public PXSelectJoin<TaxAdjustment,
							LeftJoin<Branch,
								On<TaxAdjustment.branchID, Equal<Branch.branchID>>,
							LeftJoin<TaxPeriod, 
									On<TaxPeriod.branchID, Equal<Branch.parentBranchID>,
										And<TaxPeriod.vendorID, Equal<TaxAdjustment.vendorID>, 
										And<TaxPeriod.taxPeriodID, Equal<TaxAdjustment.taxPeriod>>>>,
			LeftJoin<TaxReportLine,
									On<TaxReportLine.vendorID, Equal<TaxAdjustment.vendorID>, 
										And<TaxReportLine.netTax, Equal<boolTrue>, 
										And<TaxReportLine.tempLine, Equal<boolTrue>>>>,
							InnerJoin<Vendor, 
									On<Vendor.bAccountID, Equal<TaxAdjustment.vendorID>>>>>>,
			Where<TaxAdjustment.docType, Equal<Required<TaxAdjustment.docType>>,
									And<TaxAdjustment.refNbr, Equal<Required<TaxAdjustment.refNbr>>>>>
							TaxAdjustment_Select;

		public PXSelectJoin<TaxTran,
			InnerJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxTran.vendorID>, And<TaxBucketLine.bucketID, Equal<TaxTran.taxBucketID>>>,
			InnerJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<TaxBucketLine.vendorID>, And<TaxReportLine.lineNbr, Equal<TaxBucketLine.lineNbr>>>>>,
			Where<TaxTran.tranType, Equal<Required<TaxTran.tranType>>,
				And<TaxTran.refNbr, Equal<Required<TaxTran.refNbr>>,
				And<Where<TaxReportLine.taxZoneID, IsNull, And<TaxReportLine.tempLine, Equal<boolFalse>,
											 Or<TaxReportLine.taxZoneID, Equal<TaxTran.taxZoneID>>>>>>>> TaxTran_Select;

		#region Repository mehods

		public static int? CurrentRevisionId(PXGraph graph, int? branchID, int? vendorId, string taxPeriodID)
		{
			int? parentBranchID = PXAccess.GetMasterBranchID(branchID).First();
			IEnumerable<Branch> childBranches = BranchMaint.GetChildBranches(graph, parentBranchID);

			int?[] childBranchIDs = childBranches.Select(b => b.BranchID).ToArray();

			PXResult<TaxPeriod, TaxHistory> result = (PXResult<TaxPeriod, TaxHistory>)
													PXSelectJoin<TaxPeriod,
																LeftJoin<TaxHistory,
																	On<TaxPeriod.branchID, Equal<Required<TaxPeriod.branchID>>, 
																		And<TaxHistory.vendorID, Equal<TaxPeriod.vendorID>,
																		And<TaxHistory.taxPeriodID, Equal<TaxPeriod.taxPeriodID>>>>>,
																Where<TaxHistory.branchID, In<Required<TaxHistory.branchID>>,
																		And<TaxPeriod.vendorID, Equal<Required<TaxHistory.vendorID>>,
																		And<TaxPeriod.taxPeriodID, Equal<Required<TaxHistory.taxPeriodID>>>>>,
																OrderBy<Desc<TaxHistory.revisionID>>>
																.Select(graph, parentBranchID, childBranchIDs, vendorId, taxPeriodID);

			if (result == null) return null;

			TaxPeriod period = result;
			TaxHistory search = result;
		

			return search.RevisionID ?? (period.Status != TaxPeriodStatus.Open ? (int?)1 : null);
		}

		#endregion

        [PXDate()]
        [PXDBScalar(typeof(Search<CurrencyRate.curyEffDate,

            Where<CurrencyRate.fromCuryID, Equal<TaxTran.curyID>,
            And<CurrencyRate.toCuryID, Equal<CurrentValue<AP.Vendor.curyID>>,
            And<CurrencyRate.curyRateType, Equal<CurrentValue<AP.Vendor.curyRateTypeID>>,
            And<CurrencyRate.curyEffDate, LessEqual<TaxTran.tranDate>>>>>,
            OrderBy<Desc<CurrencyRate.curyEffDate>>>))]
        protected virtual void TaxTran_CuryEffDate_CacheAttached(PXCache sender)
        { 
        }

		public static decimal GetMult(TaxTran tran)
		{
			return GetMult(tran.Module, tran.TranType, tran.TaxType, 1);
		}

		public static decimal GetMultByTranType(string module, string tranType)
		{
			return (module == BatchModule.AP && APDocType.TaxDrCr(tranType) == DrCr.Debit ||
					module == BatchModule.AR && ARDocType.TaxDrCr(tranType) == DrCr.Credit ||
					(module == BatchModule.GL && tranType != TaxTran.tranType.TranReversed) ||
					module == BatchModule.CA ? 1m : -1m);
		}

		public static decimal GetMult(string module, string tranType, string tranTaxType, short? reportLineMult)
		{
			decimal lineMult = (reportLineMult == 1 && tranTaxType == TaxType.Sales ||
				reportLineMult == 1 && tranTaxType == TaxType.PendingSales ||
				reportLineMult == -1 && tranTaxType == TaxType.Purchase ||
				reportLineMult == -1 && tranTaxType == TaxType.PendingPurchase ? 1m : -1m);

			return GetMultByTranType(module, tranType) * lineMult;
		}

		private void SegregateBatch(JournalEntry je, int? BranchID, string CuryID, DateTime? DocDate, string FinPeriodID, string description)
		{
			Batch apbatch = je.BatchModule.Current;

			if (apbatch == null ||
				!object.Equals(apbatch.BranchID, BranchID) ||
				!object.Equals(apbatch.CuryID, CuryID) ||
				!object.Equals(apbatch.FinPeriodID, FinPeriodID))
			{
				je.Clear();

				CurrencyInfo info = new CurrencyInfo();
				info.CuryID = CuryID;
				info.CuryEffDate = DocDate;
				info = je.currencyinfo.Insert(info);

				apbatch = new Batch();
				apbatch.BranchID = BranchID;
				apbatch.Module = "GL";
				apbatch.Status = "U";
				apbatch.Released = true;
				apbatch.Hold = false;
				apbatch.DateEntered = DocDate;
				apbatch.FinPeriodID = FinPeriodID;
				apbatch.TranPeriodID = FinPeriodID;
				apbatch.CuryID = CuryID;
				apbatch.CuryInfoID = info.CuryInfoID;
				apbatch.Description = description;
				apbatch = je.BatchModule.Insert(apbatch);

				CurrencyInfo b_info = (CurrencyInfo)PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Current<Batch.curyInfoID>>>>.Select(je, null);
				b_info.CuryID = CuryID;
				b_info.CuryEffDate = DocDate;
				je.currencyinfo.Update(b_info);
			}
		}

		private void UpdateHistory(TaxTran tran, TaxReportLine line)
		{
			TaxHistory hist = TaxHistory_Current.Select(tran.VendorID, tran.BranchID, tran.AccountID, tran.SubID, tran.TaxID, tran.TaxPeriodID, line.LineNbr, tran.RevisionID);

			if (hist == null)
			{
				hist = new TaxHistory();
				hist.RevisionID = tran.RevisionID;
				hist.VendorID = tran.VendorID;
				hist.BranchID = tran.BranchID;
				hist.AccountID = tran.AccountID;
				hist.SubID = tran.SubID;
				hist.TaxID = tran.TaxID;
				hist.TaxPeriodID = tran.TaxPeriodID;
				hist.LineNbr = line.LineNbr;

				hist = (TaxHistory)TaxHistory_Current.Cache.Insert(hist);
			}

			decimal HistMult = GetMult(tran.Module, tran.TranType, tran.TaxType, line.LineMult);

			switch (line.LineType)
			{
				case "P":
					hist.ReportFiledAmt += HistMult * tran.ReportTaxAmt.GetValueOrDefault();
					hist.FiledAmt += HistMult * tran.TaxAmt.GetValueOrDefault();
					break;
				case "A":
					hist.ReportFiledAmt += HistMult * tran.ReportTaxableAmt.GetValueOrDefault();
					hist.FiledAmt += HistMult * tran.TaxableAmt.GetValueOrDefault();
					break;
			}
			TaxHistory_Current.Cache.Update(hist);
		}

		public virtual void ReleaseDocProc(JournalEntry je, TaxAdjustment doc)
		{
			if (doc.Hold == true)
			{
				throw new PXException(AP.Messages.Document_OnHold_CannotRelease);
			}

			using (new PXConnectionScope())
			{
				using (PXTransactionScope ts = new PXTransactionScope())
				{
					RoundingManager rmanager = new RoundingManager(this, doc.VendorID);
					int? revisionId = CurrentRevisionId(this, doc.BranchID, doc.VendorID, doc.TaxPeriod) ?? 1;

					foreach (PXResult<TaxAdjustment, Branch, TaxPeriod, TaxReportLine, Vendor> res in TaxAdjustment_Select.Select(doc.DocType, doc.RefNbr))
					{
						TaxAdjustment taxdoc = res;
						TaxPeriod taxper = res;
						TaxReportLine taxline = res;
						Vendor vend = res;

						if (taxper.Status == null || taxper.Status == TaxPeriodStatus.Open || taxper.Status == TaxPeriodStatus.Closed && taxline.NetTax != null)
						{
							throw new PXException(Messages.Only_Prepared_CanBe_Adjusted);
						}

						//use timestamp to handle concurrent report releasing
						Caches[typeof (TaxPeriod)].SetStatus(taxper, PXEntryStatus.Updated);

                        SegregateBatch(je, taxdoc.BranchID, taxdoc.CuryID, taxdoc.DocDate, taxdoc.FinPeriodID, taxdoc.DocDesc);
                        bool debit = (doc.DocType == TaxAdjustmentType.AdjustOutput ? 1 : -1) * Math.Sign(taxdoc.OrigDocAmt ?? 0m) > 0;

						GLTran tran = new GLTran();
						tran.AccountID = taxdoc.AdjAccountID;
						tran.SubID = taxdoc.AdjSubID;
						tran.CuryDebitAmt = debit ? Math.Abs(taxdoc.CuryOrigDocAmt ?? 0m) : 0m;
                        tran.DebitAmt = debit ? Math.Abs(taxdoc.OrigDocAmt ?? 0m) : 0m;
                        tran.CuryCreditAmt = debit ? 0m : Math.Abs(taxdoc.CuryOrigDocAmt ?? 0m);
                        tran.CreditAmt = debit ? 0m : Math.Abs(taxdoc.OrigDocAmt ?? 0m);
						tran.TranType = taxdoc.DocType;
						tran.TranClass = GLTran.tranClass.Normal;
						tran.RefNbr = taxdoc.RefNbr;
						tran.TranDesc = taxdoc.DocDesc;
                        tran.TranPeriodID = taxdoc.FinPeriodID;
                        tran.FinPeriodID = taxdoc.FinPeriodID;
						tran.TranDate = taxdoc.DocDate;
						tran.CuryInfoID = je.BatchModule.Current.CuryInfoID;
						tran.Released = true;

						je.GLTranModuleBatNbr.Insert(tran);
					}

					TaxTran prev_n = null;

					foreach (PXResult<TaxTran, TaxBucketLine, TaxReportLine> res in TaxTran_Select.Select(doc.DocType, doc.RefNbr))
					{
						TaxTran n = res;
						TaxReportLine line = res;
                        bool debit = (doc.DocType == TaxAdjustmentType.AdjustOutput ? 1 : -1) * Math.Sign(n.TaxAmt ?? 0m) > 0;

						if (object.Equals(n, prev_n) == false)
						{
							GLTran tran = new GLTran();
							tran.AccountID = n.AccountID;
							tran.SubID = n.SubID;
							tran.CuryDebitAmt = debit ? 0m : Math.Abs(n.CuryTaxAmt ?? 0m);
                            tran.DebitAmt = debit ? 0m : Math.Abs(n.TaxAmt ?? 0m);
                            tran.CuryCreditAmt = debit ? Math.Abs(n.CuryTaxAmt ?? 0m) : 0m;
                            tran.CreditAmt = debit ? Math.Abs(n.TaxAmt ?? 0m) : 0m;
							tran.TranType = doc.DocType;
							tran.TranClass = GLTran.tranClass.Normal;
							tran.RefNbr = doc.RefNbr;
							tran.TranDesc = n.Description;
                            tran.TranPeriodID = doc.FinPeriodID;
                            tran.FinPeriodID = doc.FinPeriodID;
							tran.TranDate = doc.DocDate;
							tran.CuryInfoID = je.BatchModule.Current.CuryInfoID;
							tran.Released = true;

							je.GLTranModuleBatNbr.Insert(tran);

							n.Released = true;
							n.RevisionID = revisionId;
							TaxTran_Select.Cache.Update(n);
						}
						prev_n = n;
						UpdateHistory(n, line);
					}

					je.Save.Press();

					doc.Released = true;
					doc.BatchNbr = je.BatchModule.Current.BatchNbr;
					doc = TaxAdjustment_Select.Update(doc);

					foreach (TaxHistory rounding in PXSelect<TaxHistory,
						Where<TaxHistory.vendorID, Equal<Required<TaxHistory.vendorID>>,
							And<TaxHistory.taxPeriodID, Equal<Required<TaxHistory.taxPeriodID>>,
							And<TaxHistory.revisionID, Equal<Required<TaxHistory.revisionID>>,
							And<TaxHistory.taxID, Equal<StringEmpty>>>>>>.Select(this, doc.VendorID, doc.TaxPeriod, revisionId))
					{
						TaxHistory_Current.Cache.Delete(rounding);
					}

					this.Persist();
                    TaxHistorySumManager.UpdateTaxHistorySums(this, rmanager, doc.TaxPeriod, revisionId, doc.BranchID,
						(line) => ShowTaxReportLine(line, doc.TaxPeriod));
					ts.Complete(this);
				}

				TaxAdjustment_Select.Cache.Persisted(false);
				TaxTran_Select.Cache.Persisted(false);
				TaxHistory_Current.Cache.Persisted(false);
			}
		}

		public virtual void VoidReportProc(TaxPeriodFilter taxper)
		{
			Branch branch = BranchMaint.FindBranchByID(this, taxper.BranchID);

			using (new PXConnectionScope())
			{
				using (PXTransactionScope ts = new PXTransactionScope())
				{
					using (new PXReadBranchRestrictedScope(taxper.BranchID))
					{
						PXUpdate<
							Set<TaxTran.taxPeriodID, Null,
								Set<TaxTran.revisionID, Null>>,
							TaxTran,
							Where<TaxTran.vendorID, Equal<Required<TaxTran.vendorID>>,
								And<TaxTran.taxPeriodID, Equal<Required<TaxTran.taxPeriodID>>,
								And<TaxTran.revisionID, Equal<Required<TaxTran.revisionID>>,
								And<TaxTran.released, Equal<True>,
								And<TaxTran.voided, Equal<False>>>>>>>
							.Update(this, taxper.VendorID, taxper.TaxPeriodID, taxper.RevisionId);

						foreach (TaxHistory history in PXSelect<TaxHistory,
							Where<TaxHistory.vendorID, Equal<Required<TaxHistory.vendorID>>,
								And<TaxHistory.taxPeriodID, Equal<Required<TaxHistory.taxPeriodID>>,
									And<TaxHistory.revisionID, Equal<Required<TaxHistory.revisionID>>>>>>
							.Select(this, taxper.VendorID, taxper.TaxPeriodID, taxper.RevisionId))
						{
							TaxHistory_Current.Cache.Delete(history);
						}
					}

					TaxHistory_Current.Cache.Persist(PXDBOperation.Delete);
					TaxHistory_Current.Cache.Persisted(false);
					bool needUpdatePeriodState;
					using (new PXReadBranchRestrictedScope(branch.ParentBranchID))
					{
						TaxHistory history = PXSelect<TaxHistory,
							Where<TaxHistory.vendorID, Equal<Required<TaxHistory.vendorID>>,
								And<TaxHistory.taxPeriodID, Equal<Required<TaxHistory.taxPeriodID>>,
								And<TaxHistory.revisionID, Equal<Required<TaxHistory.revisionID>>>>>>
							.SelectWindowed(this, 0, 1, taxper.VendorID, taxper.TaxPeriodID, taxper.RevisionId);
						needUpdatePeriodState = history == null;
					}

                    if (needUpdatePeriodState)
					{
						foreach (TaxPeriod res in TaxPeriod_Current.Select(branch.ParentBranchID, taxper.VendorID, taxper.TaxPeriodID))
						{
							if (res.Status != TaxPeriodStatus.Prepared)
							{
								throw new PXException();
							}
							res.Status = taxper.RevisionId > 1 ? TaxPeriodStatus.Closed : TaxPeriodStatus.Open;
							TaxPeriod_Current.Cache.Update(res);
						}

						TaxPeriod_Current.Cache.Persist(PXDBOperation.Update);

						TaxPeriod_Current.Cache.Persisted(false);
						bool yearHasNoProcessedTransactions;
						using (new PXReadBranchRestrictedScope(branch.ParentBranchID))
						{
							TaxPeriod anyProcessedTransactions = PXSelectReadonly2<TaxPeriod, 
								InnerJoin<TaxHistory,
																						On<TaxPeriod.branchID, Equal<Required<TaxPeriod.branchID>>, 
																							And<TaxPeriod.vendorID, Equal<TaxHistory.vendorID>,
																							And<TaxPeriod.taxPeriodID, Equal<TaxHistory.taxPeriodID>>>>>,
								Where<TaxPeriod.vendorID,Equal<Required<TaxPeriod.vendorID>>,
																						And<TaxPeriod.taxYear, Equal<Required<TaxPeriod.taxYear>>>>>
																				.Select(this,
																						branch.ParentBranchID, 
																						taxper.VendorID, 
																						taxper.TaxPeriodID.Substring(0, 4));

							yearHasNoProcessedTransactions = anyProcessedTransactions == null;
						}

						if (yearHasNoProcessedTransactions)
						{
							PXDatabase.Delete<TaxPeriod>(
								new PXDataFieldRestrict<TaxPeriod.branchID>(branch.ParentBranchID),
								new PXDataFieldRestrict<TaxPeriod.vendorID>(taxper.VendorID),
								new PXDataFieldRestrict<TaxPeriod.taxYear>(taxper.TaxPeriodID.Substring(0, 4)));
						}
					}

					ts.Complete(this);
				}
			}
		}

		public virtual void ClosePeriodProc(TaxPeriodFilter p)
		{
			List<APRegister> doclist = new List<APRegister>();

			Branch branch = BranchMaint.FindBranchByID(this, p.BranchID);

			using (new PXConnectionScope())
			using (PXTransactionScope ts = new PXTransactionScope())
			{
				CheckNetTaxReportLinesInMigrationMode(p.VendorID);				
				CheckUnreleasedTaxAdjustmentsDoNotExist(p.TaxPeriodID, branch.ParentBranchID);

				bool arPPDExist;
				bool apPPDExist;
				CheckForUnprocessedPPD(this, branch.ParentBranchID, p.VendorID, p.EndDate, out arPPDExist, out apPPDExist);
				string error = String.Empty;
				error += arPPDExist ? PXMessages.Localize(AR.Messages.UnprocessedPPDExists) : String.Empty;
				error += apPPDExist ? " " + PXMessages.Localize(AP.Messages.UnprocessedPPDExists) : String.Empty;
				if (!String.IsNullOrEmpty(error))
				{
					throw new PXSetPropertyException(error, PXErrorLevel.Error);
				}

				TaxPeriod taxper = TaxPeriod_Current.Select(branch.ParentBranchID, p.VendorID, p.TaxPeriodID);

				if (taxper.Status != "P")
				{
					throw new PXException(Messages.CannotCloseReportForNotPreparedPeriod);
				}

				taxper.Status = "C";
				TaxPeriod_Current.Cache.Update(taxper);

				Vendor vendor = Vendor_Current.Select(p.VendorID);
				
				string FinPeriodID = null;
                DateTime docDate = ((DateTime)taxper.EndDate).AddDays(-1);

				VerifyTaxConfigurationErrors(p, taxper, vendor)
					.RaiseIfHasError();

				APInvoiceEntry docgraph = PXGraph.CreateInstance<APInvoiceEntry>();

				Dictionary<int?, KeyValuePair<APInvoice, List<APTran>>> tranlist = new Dictionary<int?, KeyValuePair<APInvoice, List<APTran>>>();

				PXResult<TaxHistory, TaxReportLine>[] taxHistories;

				using (new PXReadBranchRestrictedScope(branch.ParentBranchID))
				{
					taxHistories = PXSelectJoinGroupBy<TaxHistory,
										InnerJoin<TaxReportLine, 
											On<TaxReportLine.vendorID, Equal<TaxHistory.vendorID>,
							And<TaxReportLine.lineNbr, Equal<TaxHistory.lineNbr>>>>,
						Where<TaxHistory.vendorID, Equal<Required<TaxHistory.vendorID>>,
							And<TaxHistory.taxPeriodID, Equal<Required<TaxHistory.taxPeriodID>>,
							And<TaxHistory.revisionID, Equal<Required<TaxHistory.revisionID>>>>>,
						Aggregate<GroupBy<TaxHistory.vendorID,
							GroupBy<TaxHistory.branchID,
							GroupBy<TaxHistory.lineNbr,
							GroupBy<TaxHistory.accountID,
							GroupBy<TaxHistory.subID,
							GroupBy<TaxHistory.taxID,
							GroupBy<TaxReportLine.netTax,
											Sum<TaxHistory.filedAmt>>>>>>>>>>
										.Select(docgraph, p.VendorID, p.TaxPeriodID, p.RevisionId)
										.Cast<PXResult<TaxHistory, TaxReportLine>>()
										.ToArray();	
				}

				foreach (PXResult<TaxHistory, TaxReportLine> res in taxHistories)
				{
					TaxHistory hist = res;
					TaxReportLine line = res;

					if (line.NetTax == true 
						&& hist.BranchID != null 
						&& vendor.AutoGenerateTaxBill == true)
					{
						KeyValuePair<APInvoice, List<APTran>> pair;
						if (!tranlist.TryGetValue(hist.BranchID, out pair))
						{
							tranlist[hist.BranchID] = pair = new KeyValuePair<APInvoice, List<APTran>>(new APInvoice(), new List<APTran>());
							pair.Key.CuryLineTotal = 0m;
							pair.Key.LineTotal = 0m;
						}

						APTran new_aptran = new APTran();
						new_aptran.BranchID = hist.BranchID;
						new_aptran.AccountID = hist.AccountID;
						new_aptran.SubID = hist.SubID;
						new_aptran.LineAmt = hist.FiledAmt;
						new_aptran.CuryLineAmt = hist.ReportFiledAmt;
						new_aptran.TranAmt = hist.FiledAmt;
						new_aptran.CuryTranAmt = hist.ReportFiledAmt;

						new_aptran.TranDesc = string.IsNullOrEmpty(line.TaxZoneID)
						                      	? string.Format(Messages.TranDesc1, hist.TaxID)
						                      	: string.Format(Messages.TranDesc2, hist.TaxID, line.TaxZoneID);

						pair.Value.Add(new_aptran);
						pair.Key.CuryLineTotal += new_aptran.CuryLineAmt ?? 0m;
						pair.Key.LineTotal += new_aptran.LineAmt ?? 0m;
					}
				}

				if (tranlist.Count > 0)
				{
					foreach (KeyValuePair<int?, KeyValuePair<APInvoice, List<APTran>>> pair in tranlist)
					{
						int? BranchID = pair.Key;
						decimal? TranTotal = pair.Value.Key.LineTotal;
						decimal? CuryTranTotal = pair.Value.Key.CuryLineTotal;

						docgraph.Clear();
						docgraph.vendor.Current = PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>.Select(docgraph, taxper.VendorID);
                       
                        bool postingToClosed=(bool)((GLSetup)PXSelect<GLSetup>.Select(docgraph)).PostClosedPeriods;

						FinPeriod currentFinPeriodID = FinPeriodIDAttribute.FindFinPeriodByDate(this, docDate);

						if (currentFinPeriodID == null)
						{
							throw new PXException(
								Messages.CannotReleaseTaxReportNoFinancialPeriodForPeriodEndDate,
								docDate.ToShortDateString(),
								FinPeriodIDAttribute.FormatForError(taxper.TaxPeriodID));
						}

                        if (FinPeriodID == null && (!(bool)currentFinPeriodID.Active || ((bool)currentFinPeriodID.APClosed && !postingToClosed)))
						{
							FinPeriodID = docgraph.vendor.Current.TaxPeriodType == "F"
							              	? taxper.TaxPeriodID
                                            : FinPeriodSelectorAttribute.FindFinPeriodByDate(this, ((DateTime)taxper.EndDate).AddDays(-1))?.FinPeriodID;

							FinPeriod openPeriod =
								PXSelect<FinPeriod,
									Where<FinPeriod.finPeriodID, GreaterEqual<Required<FinPeriod.finPeriodID>>,
										And<FinPeriod.aPClosed, Equal<False>>>,
									OrderBy<Asc<FinPeriod.finPeriodID>>>.SelectWindowed(docgraph, 0, 1, FinPeriodID);

							if (openPeriod != null)
							{
								FinPeriodID = openPeriod.FinPeriodID;
                                docDate = ((DateTime)openPeriod.EndDate).AddDays(-1);
							}
						}
						CurrencyInfo new_info = new CurrencyInfo();
						if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
						{
							new_info.CuryID = docgraph.vendor.Current.CuryID;

							if (!string.IsNullOrEmpty(docgraph.vendor.Current.CuryRateTypeID))
							{
							new_info.CuryRateTypeID = docgraph.vendor.Current.CuryRateTypeID;
							}

							new_info.BaseCalc = false;
						}

						new_info = docgraph.currencyinfo.Insert(new_info);

						APInvoice new_apdoc = new APInvoice();

						decimal amountsSign = CuryTranTotal >= 0m ? 1m : -1m;
						new_apdoc.DocType = (CuryTranTotal >= 0m ? APDocType.Invoice : APDocType.DebitAdj);
						new_apdoc.TaxCalcMode = TaxCalculationMode.TaxSetting;
						new_apdoc.VendorID = taxper.VendorID;
						new_apdoc.DocDate = docDate;
						new_apdoc.Released = false;
						new_apdoc.Hold = false;
						new_apdoc.CuryID = new_info.CuryID;
						new_apdoc.CuryInfoID = new_info.CuryInfoID;

						new_apdoc = docgraph.Document.Insert(new_apdoc);

						new_apdoc.BranchID = BranchID;
						new_apdoc.TaxZoneID = null;
						new_apdoc.IsTaxDocument = true;

						new_apdoc = docgraph.Document.Update(new_apdoc);

						docgraph.APSetup.Current.RequireControlTotal = false;
						docgraph.APSetup.Current.RequireControlTaxTotal = false;

						foreach (APTran new_aptran in pair.Value.Value)
						{
							new_aptran.LineAmt = amountsSign * new_aptran.LineAmt;
							new_aptran.CuryLineAmt = amountsSign * new_aptran.CuryLineAmt;
							new_aptran.TranAmt = amountsSign * new_aptran.TranAmt;
							new_aptran.CuryTranAmt = amountsSign * new_aptran.CuryTranAmt;
							docgraph.Transactions.Insert(new_aptran);
						}

						/// Because with Multi-Currency enabled (in particular for foreign currency
						/// tag agencies) we disable automatic calculation of base amounts in the bill
						/// (<see cref="CurrencyInfo.BaseCalc"/>), we become fully responsible for
						/// updating document totals in base amounts properly.
						/// Normally they would be calculated from the corresponding currency amounts
						/// by the PXCurrency attributes.
						/// .
						new_apdoc.LineTotal = amountsSign * TranTotal;
						new_apdoc.CuryLineTotal = amountsSign * CuryTranTotal;
						new_apdoc.OrigDocAmt = amountsSign * TranTotal;
						new_apdoc.CuryOrigDocAmt = amountsSign * CuryTranTotal;
						new_apdoc.DocBal = amountsSign * TranTotal;
						new_apdoc.CuryDocBal = amountsSign * CuryTranTotal;
						new_apdoc.CuryOrigDiscAmt = 0m;
						new_apdoc.OrigDiscAmt = 0m;

						new_info.CuryMultDiv = CuryMultDivType.Mult;

						if (new_apdoc.CuryOrigDocAmt == 0 || new_apdoc.OrigDocAmt == 0)
						{
							new_info.CuryRate = 1;
							new_info.RecipRate = 1;
						}
						else
						{
							new_info.CuryRate = Math.Round(new_apdoc.OrigDocAmt.Value / new_apdoc.CuryOrigDocAmt.Value, 8);
							new_info.RecipRate = Math.Round(new_apdoc.CuryOrigDocAmt.Value / new_apdoc.OrigDocAmt.Value, 8);
						}

						docgraph.Save.Press();

						doclist.Add(docgraph.Document.Current);
					}
				}
				TaxPeriod_Current.Cache.Persist(PXDBOperation.Update);

				TaxPeriod_Current.Cache.Persisted(false);

				ts.Complete(this);
			}
			APDocumentRelease.ReleaseDoc(doclist, false);
		}

		private void CheckNetTaxReportLinesInMigrationMode(int? vendorID)
		{
			PXSelectBase<TaxReportLine> netTaxLines = new PXSelectJoin<
				TaxReportLine,
					CrossJoin<APSetup>,
				Where<
					TaxReportLine.vendorID, Equal<Required<TaxReportLine.vendorID>>,
					And<TaxReportLine.netTax, Equal<True>,
					And<APSetup.migrationMode, Equal<True>>>>>
				(this);

			if (netTaxLines.Any(vendorID))
			{
				throw new PXException(Messages.TaxReportCannotBeReleasedMigrationModeNetTax);
			}
		}

		private void CheckUnreleasedTaxAdjustmentsDoNotExist(string taxPeriodID, int? branchID)
		{
			IEnumerable<Branch> childBranches = BranchMaint.GetChildBranches(this, branchID);

			int?[] childBranchIDs = childBranches.Select(b => b.BranchID).ToArray();

			var unreleasedTaxAdjustment = (TaxAdjustment)PXSelect<TaxAdjustment,
															Where<TaxAdjustment.taxPeriod, Equal<Required<TaxAdjustment.taxPeriod>>,
																	And<TaxAdjustment.released, Equal<False>,
																	And<TaxAdjustment.branchID, In<Required<TaxAdjustment.branchID>>>>>>
																.Select(this, taxPeriodID, childBranchIDs);

			if (unreleasedTaxAdjustment != null)
			{
				throw new PXException(Messages.TaxReportCannotBeReleased);
			}
		}

		public class ErrorNotifications
		{
			private List<string> _errorMessages = new List<string> { };

			public void AddMessage(string message, params object[] args)
			{
				_errorMessages.Add(PXMessages.LocalizeFormatNoPrefix(message, args));
			}

			public string Message
			{
				get { return String.Join(" ", _errorMessages.ToArray()); }
			}

			public void RaiseIfAny()
			{
				if(_errorMessages.Any())
					throw new PXException(Message);
			}
		}

		protected virtual ProcessingResult VerifyTaxConfigurationErrors(TaxPeriodFilter filter, TaxPeriod taxPeriod, Vendor vendor)
		{
			Branch closingBranch = BranchMaint.FindBranchByID(this, filter.BranchID);

			using (new PXReadBranchRestrictedScope(closingBranch.ParentBranchID))
			{
			Dictionary<string, HashSet<string>> unreportedTaxes = new Dictionary<string, HashSet<string>>();
			HashSet<string> misconfiguredTaxes = new HashSet<string>();

			var validationResult = new ProcessingResult();

			foreach (PXResult<Branch, TaxHistory, TaxTranReport, TaxBucketLine> taxTrans in PXSelectJoin<
				Branch,
					LeftJoin<TaxHistory,
						On<TaxHistory.branchID, Equal<Branch.branchID>,
							And<TaxHistory.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>,
								And<TaxHistory.taxPeriodID, Equal<Current<TaxPeriodFilter.taxPeriodID>>,
									And<TaxHistory.revisionID, Equal<Current<TaxPeriodFilter.revisionId>>>>>>,
						LeftJoin<TaxTranReport,
							On<TaxTranReport.branchID, Equal<Branch.branchID>,
								And<TaxTranReport.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>,
								And<TaxTranReport.released, Equal<True>,
								And<TaxTranReport.voided, Equal<False>,
								And<TaxTranReport.taxPeriodID, IsNull,
								And<TaxTranReport.origRefNbr, Equal<Empty>,
								And<TaxTranReport.taxType, NotEqual<TaxType.pendingPurchase>,
								And<TaxTranReport.taxType, NotEqual<TaxType.pendingSales>,
						And2<Where<
							Current<Vendor.taxReportFinPeriod>, Equal<True>,
											  Or<TaxTranReport.tranDate, Less<Current<TaxPeriod.endDate>>>>,
						And<Where<
							Current<Vendor.taxReportFinPeriod>, Equal<False>,
												Or<TaxTranReport.finDate, Less<Current<TaxPeriod.endDate>>>>>>>>>>>>>>,
						LeftJoin<TaxBucketLine,
								On<TaxBucketLine.vendorID, Equal<TaxTranReport.vendorID>,
									And<TaxBucketLine.bucketID, Equal<TaxTranReport.taxBucketID>>>>>>,
				Where<
					TaxHistory.taxPeriodID, IsNull,
						And<TaxTranReport.refNbr, IsNotNull>>>
					.SelectMultiBound(this, new object[] { filter, taxPeriod, vendor }))
			{
				Branch branch = taxTrans;
				TaxTranReport tran = taxTrans;
				TaxBucketLine bucketLine = taxTrans;
				if (bucketLine.VendorID.HasValue)
				{
					HashSet<string> taxIds;
					if (!unreportedTaxes.TryGetValue(branch.BranchCD, out taxIds))
					{
						taxIds = new HashSet<string>();
						unreportedTaxes[branch.BranchCD] = taxIds;
					}
					taxIds.Add(tran.TaxID);
				}
				else
				{
					misconfiguredTaxes.Add(tran.TaxID);
				}
			}

			if (unreportedTaxes.Count != 0 || misconfiguredTaxes.Count != 0)
			{
				validationResult.AddErrorMessage(Messages.CannotClosePeriod);
				foreach (var kvp in unreportedTaxes)
				{
					var taxIDs = String.Join(", ", kvp.Value.ToArray());
					validationResult.AddErrorMessage(Messages.ThereAreUnreportedTrans, taxIDs, kvp.Key.Trim());
				}

				if (misconfiguredTaxes.Count != 0)
				{
					var taxIDs = String.Join(", ", misconfiguredTaxes.ToArray());
					validationResult.AddErrorMessage(Messages.WrongTaxConfig, taxIDs);
					validationResult.AddErrorMessage(Messages.CheckTaxConfig);
				}
			}

			return validationResult;
		}
		}

		public virtual string FileTaxProc(TaxPeriodFilter p)
		{
            ProcessingResult validationResult = new ProcessingResult();

				using (new PXConnectionScope())
				{
					using (PXTransactionScope ts = new PXTransactionScope())
					{
					bool arPPDExist;
					bool apPPDExist;
					CheckForUnprocessedPPD(this, p.BranchID, p.VendorID, p.EndDate, out arPPDExist, out apPPDExist);
					string error = String.Empty;
					error += arPPDExist ? PXMessages.Localize(AR.Messages.UnprocessedPPDExists) : String.Empty;
					error += apPPDExist ? " " + PXMessages.Localize(AP.Messages.UnprocessedPPDExists) : String.Empty;
					if (!String.IsNullOrEmpty(error))
					{
						throw new PXSetPropertyException(error, PXErrorLevel.Error);
					}

					Branch branch = BranchMaint.FindBranchByID(this, p.BranchID);

					TaxPeriod taxper = TaxPeriod_Current.SelectSingle(branch.ParentBranchID, p.VendorID, p.TaxPeriodID);

						RoundingManager rmanager = new RoundingManager(this, taxper.VendorID);
						Company company = new PXSetup<Company>(this).Current;

					int revisionId = ReportTaxProcess.CurrentRevisionId(this, taxper.BranchID, taxper.VendorID, taxper.TaxPeriodID) ?? 1;
						
						if (taxper.Status == TaxPeriodStatus.Closed)
                    {
							revisionId += 1;
                    }

						Vendor vendor = PXSelect<Vendor,
							Where<Vendor.bAccountID, Equal<Current<TaxPeriod.vendorID>>>>
							.SelectSingleBound(this, new object[] { taxper });

					using (new PXReadBranchRestrictedScope(p.BranchID))
					{
						this.Defaults[typeof(Vendor)] = () => { return vendor; };

						string reportCuryID = rmanager.CurrentVendor.CuryID ?? company.BaseCuryID;

						PXDatabase.Update<TaxPeriod>(
							new PXDataFieldAssign(typeof(TaxPeriod.status).Name, TaxPeriodStatus.Closed),
							new PXDataFieldRestrict(typeof(TaxPeriod.branchID).Name, PXDbType.Int, 4, taxper.BranchID, PXComp.EQ),
							new PXDataFieldRestrict(typeof(TaxPeriod.vendorID).Name, PXDbType.Int, 4, taxper.VendorID, PXComp.EQ),
							new PXDataFieldRestrict(typeof(TaxPeriod.taxYear).Name, PXDbType.Char, 4, taxper.TaxYear, PXComp.EQ),
							new PXDataFieldRestrict(typeof(TaxPeriod.taxPeriodID).Name, PXDbType.Char, 6, taxper.TaxPeriodID, PXComp.LT)
							);
					
							PXUpdateJoin<Set<TaxTran.taxBucketID, TaxRev.taxBucketID,
								Set<TaxTran.vendorID, TaxRev.taxVendorID>>,
								TaxTran,
								InnerJoin<TaxRev,
									On<TaxRev.taxID, Equal<TaxTran.taxID>,
										And<TaxRev.taxType, Equal<TaxTran.taxType>,
										And<TaxTran.tranDate, Between<TaxRev.startDate, TaxRev.endDate>,
										And<TaxRev.outdated, Equal<False>>>>>>,
								Where<TaxRev.taxVendorID, Equal<Required<TaxPeriod.vendorID>>,
									And<TaxTran.released, Equal<True>,
									And<TaxTran.voided, Equal<False>,
									And<TaxTran.taxPeriodID, IsNull,
									And<TaxTran.origRefNbr, Equal<StringEmpty>,
									And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>,
									And<TaxTran.taxType, NotEqual<TaxType.pendingSales>>>>>>>>>
								.Update(this, taxper.VendorID);

						PXResultset<TaxTran> taxTransWOTaxRevs = PXSelectJoin<TaxTran,
                            LeftJoin<TaxRev,
								On<TaxRev.taxID, Equal<TaxTran.taxID>,
									And<TaxRev.taxType, Equal<TaxTran.taxType>,
									And<TaxTran.tranDate, Between<TaxRev.startDate, TaxRev.endDate>,
									And<TaxRev.outdated, Equal<False>>>>>>,
								Where<TaxRev.revisionID, IsNull,
									And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>,
									And<TaxTran.taxType, NotEqual<TaxType.pendingSales>>>>>.Select(this);
 
							PXUpdateJoin<Set<TaxTran.taxPeriodID, Required<TaxPeriod.taxPeriodID>,
								Set<TaxTran.revisionID, Required<TaxTran.revisionID>,
								Set<TaxTran.reportTaxAmt, Switch<
										Case<Where<Currency.curyID, Equal<Required<TaxTran.curyID>>>, TaxTran.taxAmt,
										Case<Where<TaxTran.curyID, Equal<Currency.curyID>>, TaxTran.curyTaxAmt,
										Case<Where<CurrencyRateByDate.curyMultDiv, Equal<CuryMultDivType.mult>>,
													Round<Mult<TaxTran.curyTaxAmt, CurrencyRateByDate.curyRate>, Currency.decimalPlaces>,
										Case<Where<CurrencyRateByDate.curyMultDiv, Equal<CuryMultDivType.div>>,
													Round<Div<TaxTran.curyTaxAmt, CurrencyRateByDate.curyRate>, Currency.decimalPlaces>>>>>>,
								Set<TaxTran.reportTaxableAmt, Switch<
										Case<Where<Currency.curyID, Equal<Required<TaxTran.curyID>>>, TaxTran.taxableAmt,
										Case<Where<TaxTran.curyID, Equal<Currency.curyID>>, TaxTran.curyTaxableAmt,
										Case<Where<CurrencyRateByDate.curyMultDiv, Equal<CuryMultDivType.mult>>,
													Round<Mult<TaxTran.curyTaxableAmt, CurrencyRateByDate.curyRate>, Currency.decimalPlaces>,
										Case<Where<CurrencyRateByDate.curyMultDiv, Equal<CuryMultDivType.div>>,
													Round<Div<TaxTran.curyTaxableAmt, CurrencyRateByDate.curyRate>, Currency.decimalPlaces>>>>>>,
								Set<TaxTran.reportCuryID, Currency.curyID,
								Set<TaxTran.reportCuryRateTypeID, CurrencyRateByDate.curyRateType,
								Set<TaxTran.reportCuryEffDate, CurrencyRateByDate.curyEffDate,
									Set<TaxTran.reportCuryRate, Switch<Case<Where<CurrencyRateByDate.curyRate, IsNotNull>, CurrencyRateByDate.curyRate>,
																		decimal1>,
									Set<TaxTran.reportCuryMultDiv, Switch<Case<Where<CurrencyRateByDate.curyMultDiv, IsNotNull>, CurrencyRateByDate.curyMultDiv>,
																		CuryMultDivType.mult>>>>>>>>>>,
								TaxTran,
									InnerJoin<FinPeriod,
										On<FinPeriod.finPeriodID, Equal<TaxTran.finPeriodID>>,
								InnerJoin<TaxBucketLine,
										On<TaxTran.vendorID, Equal<TaxBucketLine.vendorID>,
											And<TaxTran.taxBucketID, Equal<TaxBucketLine.bucketID>>>,
										LeftJoin<Currency,
											On<Currency.curyID, Equal<Required<Currency.curyID>>>,
											LeftJoin<CurrencyRateByDate,
												On<CurrencyRateByDate.fromCuryID, Equal<TaxTran.curyID>,
                                                And<CurrencyRateByDate.toCuryID, Equal<Required<CurrencyRateByDate.toCuryID>>,
												And<CurrencyRateByDate.curyRateType, Equal<Required<CurrencyRateByDate.curyRateType>>,
												And<CurrencyRateByDate.curyEffDate, Equal<TaxTran.curyEffDate>>>>>>>>>,
								Where<TaxTran.vendorID, Equal<Required<TaxPeriod.vendorID>>,
									And<TaxTran.released, Equal<True>,
									And<TaxTran.voided, Equal<False>,
									And<TaxTran.taxPeriodID, IsNull,
									And<TaxTran.origRefNbr, Equal<Empty>,
									And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>,
									And<TaxTran.taxType, NotEqual<TaxType.pendingSales>,
                                    And2<Where<Required<Vendor.taxReportFinPeriod>, Equal<True>,
                                                    Or<TaxTran.tranDate, Less<Required<TaxPeriod.endDate>>>>,
                                                And<Where<Required<Vendor.taxReportFinPeriod>, Equal<False>,
                                                 Or<Sub<FinPeriod.endDate, int1>, Less<Required<TaxPeriod.endDate>>>>>>>>>>>>>>
								.Update(this, taxper.TaxPeriodID, revisionId,
											company.BaseCuryID, company.BaseCuryID, reportCuryID, reportCuryID, rmanager.CurrentVendor.CuryRateTypeID,
											taxper.VendorID,
												vendor.TaxReportFinPeriod, taxper.EndDate, vendor.TaxReportFinPeriod, taxper.EndDate);

						PXResultset<TaxTran> unreportedTaxTrans = PXSelectJoin<TaxTran,
																		InnerJoin<FinPeriod,
																			On<FinPeriod.finPeriodID, Equal<TaxTran.finPeriodID>>>,
								Where<TaxTran.vendorID, Equal<Required<TaxPeriod.vendorID>>,
								And<TaxTran.released, Equal<True>,
								And<TaxTran.voided, Equal<False>,
								And<TaxTran.taxPeriodID, IsNull,
								And<TaxTran.origRefNbr, Equal<StringEmpty>,
								And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>,
								And<TaxTran.taxType, NotEqual<TaxType.pendingSales>,
								And2<Where<Required<Vendor.taxReportFinPeriod>, Equal<True>,
									Or<TaxTran.tranDate, Less<Required<TaxPeriod.endDate>>>>,
									And2<Where<Required<Vendor.taxReportFinPeriod>, Equal<False>,
										Or<Sub<FinPeriod.endDate, int1>, Less<Required<TaxPeriod.endDate>>>>,
																							And<TaxTran.taxPeriodID, IsNull>>>>>>>>>>>
																		.Select(this,
																				taxper.VendorID,
																				vendor.TaxReportFinPeriod,
																				taxper.EndDate,
																				vendor.TaxReportFinPeriod,
																				taxper.EndDate);

							PXUpdateJoin<Set<TaxAdjustment.taxPeriod, TaxTran.taxPeriodID>,
										TaxAdjustment,
											InnerJoin<TaxTran,
												On<TaxAdjustment.docType, Equal<TaxTran.tranType>,
													And<TaxAdjustment.refNbr, Equal<TaxTran.refNbr>>>>,
										Where<TaxAdjustment.taxPeriod, NotEqual<TaxTran.taxPeriodID>,
												And<TaxAdjustment.released, Equal<True>>>>
										.Update(this);

						TaxTran rateNotFound = PXSelect<TaxTran,
									Where<TaxTran.taxPeriodID, Equal<Required<TaxTran.taxPeriodID>>,
										And<TaxTran.vendorID, Equal<Required<TaxTran.vendorID>>,
										And<TaxTran.revisionID, Equal<Required<TaxTran.revisionID>>,
										And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>,
										And<TaxTran.taxType, NotEqual<TaxType.pendingSales>,
										And<Where<TaxTran.reportTaxAmt, IsNull,
											Or<TaxTran.reportTaxableAmt, IsNull>>>>>>>>,
														OrderBy<Asc<TaxTran.tranDate>>>
														.SelectWindowed(this, 0, 1, taxper.TaxPeriodID, taxper.VendorID, revisionId);

							if (rateNotFound != null)
							throw new PXException(Messages.TaxReportRateNotFound, rateNotFound.CuryID, reportCuryID,
								rateNotFound.TranDate.Value.ToShortDateString());

							if (taxTransWOTaxRevs.Count != 0 || unreportedTaxTrans.Count != 0)
							{
								validationResult.AddErrorMessage(AP.Messages.Warning);
								validationResult.AddErrorMessage(":");

								if (taxTransWOTaxRevs.Count != 0)
								{
									Dictionary<String, HashSet<string>> misconfiguredTaxes = new Dictionary<string, HashSet<string>>();

									foreach (TaxTran tran in taxTransWOTaxRevs)
									{
										HashSet<string> taxTypes;

										if (!misconfiguredTaxes.TryGetValue(tran.TaxID, out taxTypes))
										{
											taxTypes = new HashSet<string>();
											misconfiguredTaxes[tran.TaxID] = taxTypes;
										}

										taxTypes.Add(tran.TaxType);
									}

									foreach (var kvp in misconfiguredTaxes)
									{
										foreach (var taxType in kvp.Value)
										{
											validationResult.AddErrorMessage(Messages.NoTaxRevForTaxType, kvp.Key,
												PXMessages.LocalizeNoPrefix(GetLabel.For<TaxType>(taxType)));
										}
									}
								}

								if (unreportedTaxTrans.Count != 0)
								{
								string taxesString = string.Join(",",
                                    unreportedTaxTrans
										.RowCast<TaxTran>()
										.Select(tran => tran.TaxID)
										.Distinct()
										.OrderBy(x => x).ToArray());

									validationResult.AddErrorMessage(Messages.WrongTaxConfig, taxesString);
								}

								validationResult.AddErrorMessage(Messages.CheckTaxConfig);
							}
					}
							TaxHistory prev_hist = null;

					foreach (TaxPeriod res in TaxPeriod_Current.Select(branch.ParentBranchID, taxper.VendorID, taxper.TaxPeriodID))
							{
								res.Status = TaxPeriodStatus.Prepared;
								TaxPeriod_Current.Cache.Update(res);
							}
					using (new PXReadBranchRestrictedScope(p.BranchID))
					{
						foreach (PXResult<TaxReportLine, TaxBucketLine, TaxTranReport> res
								in Period_Details_Expanded.Select(revisionId, taxper.TaxPeriodID, taxper.VendorID)
									.Where(line => ShowTaxReportLine(line.GetItem<TaxReportLine>(), taxper.TaxPeriodID)))
							{
								TaxReportLine line = res;
								TaxTranReport tran = res;


								if (prev_hist == null ||
								    object.Equals(prev_hist.BranchID, tran.BranchID) == false ||
								    object.Equals(prev_hist.AccountID, tran.AccountID) == false ||
								    object.Equals(prev_hist.SubID, tran.SubID) == false ||
								    object.Equals(prev_hist.TaxID, tran.TaxID) == false ||
								    object.Equals(prev_hist.LineNbr, line.LineNbr) == false)
								{
									if (prev_hist != null)
									{
										TaxHistory_Current.Cache.Update(prev_hist);
									}

                                prev_hist = TaxHistory_Current.Select(tran.VendorID, tran.BranchID, tran.AccountID, tran.SubID,
                                    tran.TaxID, tran.TaxPeriodID, line.LineNbr, revisionId);

									if (prev_hist == null)
									{
                                    prev_hist = new TaxHistory
                                    {
                                        VendorID = tran.VendorID,
                                        BranchID = tran.BranchID,
                                        TaxPeriodID = tran.TaxPeriodID,
                                        AccountID = tran.AccountID,
                                        SubID = tran.SubID,
                                        TaxID = tran.TaxID,
                                        LineNbr = line.LineNbr,
                                        CuryID = rmanager.CurrentVendor.CuryID ?? company.BaseCuryID,

                                        FiledAmt = 0m,
                                        UnfiledAmt = 0m,
                                        ReportFiledAmt = 0m,
                                        ReportUnfiledAmt = 0m,
                                        RevisionID = revisionId
                                    };

                                    prev_hist = (TaxHistory)TaxHistory_Current.Cache.Insert(prev_hist);
									}
								}

								decimal HistMult = GetMult(tran.Module, tran.TranType, tran.TaxType, line.LineMult);

								switch (line.LineType)
								{
									case "P":
                                    prev_hist.FiledAmt += HistMult * tran.TaxAmt.GetValueOrDefault();
                                    prev_hist.ReportFiledAmt += HistMult * tran.ReportTaxAmt.GetValueOrDefault();
										break;
									case "A":
                                    prev_hist.FiledAmt += HistMult * tran.TaxableAmt.GetValueOrDefault();
                                    prev_hist.ReportFiledAmt += HistMult * tran.ReportTaxableAmt.GetValueOrDefault();
										break;
								}

								if (line.TempLine == true || line.TempLineNbr != null && prev_hist.FiledAmt.GetValueOrDefault() == 0)
								{
									TaxHistory_Current.Cache.Delete(prev_hist);
									prev_hist = null;
								}
							}

							if (prev_hist != null)
							{
								TaxHistory_Current.Cache.Update(prev_hist);
							}
						}

						TaxPeriod_Current.Cache.Persist(PXDBOperation.Insert);
						TaxPeriod_Current.Cache.Persist(PXDBOperation.Update);

						TaxHistory_Current.Cache.Persist(PXDBOperation.Insert);
						TaxHistory_Current.Cache.Persist(PXDBOperation.Update);

						TaxPeriod_Current.Cache.Persisted(false);
						TaxHistory_Current.Cache.Persisted(false);

					TaxHistorySumManager.UpdateTaxHistorySums(this, rmanager, taxper.TaxPeriodID, revisionId, p.BranchID,
							(line) => ShowTaxReportLine(line, taxper.TaxPeriodID));

					ts.Complete(this);
					}
				}

				return validationResult.GetGeneralMessage();
		}

		public virtual bool ShowTaxReportLine(TaxReportLine taxReportLine, string taxPeriodID)
		{
			return true;
		}

		public static void CheckForUnprocessedPPD(PXGraph graph, int? branchID, int? vendorID, DateTime? endDate,
			out bool arPPDExist, out bool apPPDExist)
		{
			arPPDExist = CheckForUnprocessedPPD(graph, branchID, vendorID, endDate);
			apPPDExist = CheckAPInvoicesForUnprocessedPPD(graph, branchID, vendorID, endDate);
		}
		public static bool CheckForUnprocessedPPD(PXGraph graph, int? branchID, int? vendorID, DateTime? endDate)
		{
			bool exist = false;

			Tax tax = PXSelect<Tax, Where<Tax.taxType, Equal<CSTaxType.vat>,
				And<Tax.taxApplyTermsDisc, Equal<CSTaxTermsDiscount.toPromtPayment>,
					And<Tax.taxVendorID, Equal<Required<Tax.taxVendorID>>>>>>.SelectSingleBound(graph, null, vendorID);
			if (tax != null)
			{
				Branch branch = PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Select(graph, branchID);

				ARInvoice arInvoice = PXSelectJoin<ARInvoice,
						InnerJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARInvoice.docType>,
						And<ARAdjust.adjdRefNbr, Equal<ARInvoice.refNbr>,
						And<ARAdjust.released, Equal<True>,
						And<ARAdjust.voided, NotEqual<True>,
						And<ARAdjust.pendingPPD, Equal<True>,
						And<ARAdjust.adjgDocDate, LessEqual<Required<ARAdjust.adjgDocDate>>>>>>>>>,
					Where<ARInvoice.pendingPPD, Equal<True>,
						And<ARInvoice.released, Equal<True>,
								And<ARInvoice.openDoc, Equal<True>,
									And<ARInvoice.branchID, In<Required<ARInvoice.branchID>>>>>>>
					.SelectSingleBound(graph, null, endDate, PXAccess.GetChildBranchIDs(branch.BranchCD));

				exist = (arInvoice != null);
			}

			return exist;
		}

		public static bool CheckAPInvoicesForUnprocessedPPD(PXGraph graph, int? branchID, int? vendorID, DateTime? endDate)
		{
			bool exist = false;

			Tax tax = PXSelect<Tax, Where<Tax.taxType, Equal<CSTaxType.vat>,
				And<Tax.taxApplyTermsDisc, Equal<CSTaxTermsDiscount.toPromtPayment>,
					And<Tax.taxVendorID, Equal<Required<Tax.taxVendorID>>>>>>.SelectSingleBound(graph, null, vendorID);
			if (tax != null)
			{
				Branch branch = PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Select(graph, branchID);

				APInvoice doc = PXSelectJoin<APInvoice,
						InnerJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APInvoice.docType>,
						And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>,
						And<APAdjust.released, Equal<True>,
						And<APAdjust.voided, NotEqual<True>,
						And<APAdjust.pendingPPD, Equal<True>,
						And<APAdjust.adjgDocDate, LessEqual<Required<APAdjust.adjgDocDate>>>>>>>>>,
					Where<APInvoice.pendingPPD, Equal<True>,
						And<APInvoice.released, Equal<True>,
								And<APInvoice.openDoc, Equal<True>,
									And<APInvoice.branchID, In<Required<APInvoice.branchID>>>>>>>
					.SelectSingleBound(graph, null, endDate, PXAccess.GetChildBranchIDs(branch.BranchCD));

				exist = doc != null;
			}

			return exist;
		}
		public static bool CheckForUnprocessedSVAT(PXGraph graph, int? branchID, Vendor vendor, DateTime? endDate)
		{
			bool result = false;
			if (PXAccess.FeatureInstalled<FeaturesSet.vATReporting>() &&
				branchID != null && vendor?.BAccountID != null && endDate != null)
			{
				PXSelectBase<SVATConversionHist> select = vendor.TaxReportFinPeriod == true
				? new PXSelectJoin<SVATConversionHist,
					InnerJoin<FinPeriod, On<FinPeriod.startDate, LessEqual<SVATConversionHist.adjdDocDate>,
						And<FinPeriod.endDate, Greater<SVATConversionHist.adjdDocDate>,
						And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>>>>>,
					Where<FinPeriod.endDate, LessEqual<Required<FinPeriod.endDate>>>>(graph)
				: (PXSelectBase<SVATConversionHist>)new PXSelect<SVATConversionHist,
					Where<SVATConversionHist.adjdDocDate, LessEqual<Required<FinPeriod.endDate>>>>(graph);

				select.WhereAnd<Where<SVATConversionHist.processed, NotEqual<True>,
					And<SVATConversionHist.adjdBranchID, In<Required<SVATConversionHist.adjdBranchID>>,
					And<SVATConversionHist.vendorID, Equal<Required<SVATConversionHist.vendorID>>,
					And<SVATConversionHist.reversalMethod, Equal<SVATTaxReversalMethods.onPayments>,
					And<Where<SVATConversionHist.adjdDocType, NotEqual<SVATConversionHist.adjgDocType>,
						Or<SVATConversionHist.adjdRefNbr, NotEqual<SVATConversionHist.adjgRefNbr>>>>>>>>>();

				int?[] childBranches = BranchMaint.GetChildBranches(graph, branchID)
													.Select(b => b.BranchID)
													.ToArray();

				if (!childBranches.Any())
				{
					childBranches = new[] {branchID};
				}

				SVATConversionHist hist = select.SelectSingle(endDate.Value.AddDays(-1), childBranches, vendor.BAccountID);
				result = hist != null;
			}

			return result;
		}
	}
}

