using System;
using System.Collections.Generic;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;

namespace PX.Objects.CA
{
	/// <summary>
	/// Selector. Allows to select either vendors or customers depending <br/>
	/// upon the value in the BatchModule field. Will return a list of Customers for AR<br/>
	/// and a list of Vendors for AP, other types are not supported. If a currency ID is provided, <br/>
	/// list of Vendors/Customers will be restricted by those, having this CuryID set <br/>
	/// or having AllowOverrideCury set on. For example this allows to select only Customers/Vendors <br/>
	/// which may pay to/from a specific cash account<br/>
	/// <example> 
	/// [PXVendorCustomerSelector(typeof(CABankStatementDetail.origModule))]
	/// </example>
	/// </summary>    
	[Serializable]
	public class PXVendorCustomerSelectorAttribute : PXCustomSelectorAttribute
	{
		protected Type BatchModule;
		[Serializable]
        [PXHidden]
		public partial class VendorR : Vendor
		{
			#region BAccountID
			public new abstract class bAccountID : PX.Data.IBqlField
			{
			}
			#endregion
            public new abstract class curyID : IBqlField {}

            public new abstract class allowOverrideCury : IBqlField { }
		}
		[Serializable]
        [PXHidden]
		public partial class CustomerR : Customer
		{
			#region BAccountID
			public new abstract class bAccountID : PX.Data.IBqlField
			{
			}
			#endregion
            public new abstract class curyID : IBqlField { }

            public new abstract class allowOverrideCury : IBqlField { }

		}

		protected Type _CuryID;

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="batchModule">Must be IBqlField. Refers to the BatchModule field of the row.</param>
		public PXVendorCustomerSelectorAttribute(Type batchModule)
			: base(typeof(Search<BAccountR.bAccountID, Where<True, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>),
				   typeof(BAccountR.acctCD), 
				   typeof(BAccountR.acctName))
		{
			this.SubstituteKey    = typeof(BAccountR.acctCD);
			this.DescriptionField = typeof(BAccountR.acctName);
			this.BatchModule      = batchModule;
			this._CuryID = null;
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="batchModule">Must be IBqlField. Refers to the BatchModule field of the row.</param>		
		/// <param name="curyID">Must be IBqlField.Refers to the CuryID field of the row.</param>
		public PXVendorCustomerSelectorAttribute(Type batchModule, Type curyID)
			: base(typeof(Search<BAccountR.bAccountID, Where<True, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>),
				   typeof(BAccountR.acctCD),
				   typeof(BAccountR.acctName))
		{
			this.SubstituteKey = typeof(BAccountR.acctCD);
			this.DescriptionField = typeof(BAccountR.acctName);
			this.BatchModule = batchModule;
			this._CuryID = curyID;
		}
				
		protected virtual void GetRecords()
		{			
			PXCache cache = this._Graph.Caches[BqlCommand.GetItemType(this.BatchModule)];
			object current = null;
			foreach (object item in PXView.Currents)
			{
				if (item != null && (item.GetType() == BqlCommand.GetItemType(this.BatchModule) || item.GetType().IsSubclassOf(BqlCommand.GetItemType(this.BatchModule))))
				{
					current = item;
					break;
				}
			}

            if (current == null)
            {
                current = cache.Current;
            }

            PXView view = PXView.View;
            if (view != null)
            {
                if (_CuryID != null)
                {
                    object curyID = cache.GetValue(current, this._CuryID.Name) ?? cache.GetValuePending(current, _CuryID.Name);
                    PX.Common.PXContext.SetSlot(this._CuryID.FullName, curyID);
                }
                switch ((string)(cache.GetValue(current, this.BatchModule.Name) ?? cache.GetValuePending(current, this.BatchModule.Name)))
                {
                    case GL.BatchModule.AP:
						view.WhereNew<Where2<Match<VendorR, Current<AccessInfo.userName>>, And<Where<BAccountR.type, Equal<BAccountType.vendorType>, Or<BAccountR.type, Equal<BAccountType.employeeType>, Or<BAccountR.type, Equal<BAccountType.combinedType>, Or<BAccountR.type, Equal<BAccountType.empCombinedType>>>>>>>>();

                        if (_CuryID != null)
                        {
                            Type join = BqlCommand.Compose(
                                typeof(InnerJoin<,>),
                                typeof(VendorR),
                                typeof(On<,,>),
                                typeof(VendorR.bAccountID),
                                typeof(Equal<>),
                                typeof(BAccountR.bAccountID),
                                typeof(And<>),
                                typeof(Where<,,>),
                                typeof(VendorR.curyID),
                                typeof(Equal<>),
                                typeof(SavedStringValue<>),
                                _CuryID,
                                typeof(Or<,,>),
                                typeof(VendorR.allowOverrideCury),
                                typeof(Equal<>),
                                typeof(True),
								typeof(Or<,>),
								typeof(VendorR.curyID),
								typeof(IsNull));

                            view.JoinNew(join);
                        }
						else
						{
							Type join = BqlCommand.Compose(
								typeof(InnerJoin<,>),
								typeof(VendorR),
								typeof(On<,>),
								typeof(VendorR.bAccountID),
								typeof(Equal<>),
								typeof(BAccountR.bAccountID));

							view.JoinNew(join);
						}

                        break;
                    case GL.BatchModule.AR:
						view.WhereNew<Where2<Match<CustomerR, Current<AccessInfo.userName>>, And<Where<BAccountR.type, Equal<BAccountType.customerType>, Or<BAccountR.type, Equal<BAccountType.combinedType>, Or<BAccountR.type, Equal<BAccountType.empCombinedType>>>>>>>();

                        if (_CuryID != null)
                        {
                            Type join = BqlCommand.Compose(
                                typeof(InnerJoin<,>),
                                typeof(CustomerR),
                                typeof(On<,,>),
                                typeof(CustomerR.bAccountID),
                                typeof(Equal<>),
                                typeof(BAccountR.bAccountID),
                                typeof(And<>),
                                typeof(Where<,,>),
                                typeof(CustomerR.curyID),
                                typeof(Equal<>),
                                typeof(SavedStringValue<>),
                                _CuryID,
                                typeof(Or<,,>),
                                typeof(CustomerR.allowOverrideCury),
                                typeof(Equal<>),
                                typeof(True),
								typeof(Or<,>),
								typeof(CustomerR.curyID),
								typeof(IsNull));

                            view.JoinNew(join);
                        }
						else
						{
							Type join = BqlCommand.Compose(
								typeof(InnerJoin<,>),
								typeof(CustomerR),
								typeof(On<,>),
								typeof(CustomerR.bAccountID),
								typeof(Equal<>),
								typeof(BAccountR.bAccountID));

							view.JoinNew(join);
						}
                        break;
                }
            }
            //return null;
		}

		private static object SelectSingleBound(PXView view, object[] currents, params object[] pars)
		{
			List<object> ret = view.SelectMultiBound(currents, pars);
			if (ret.Count > 0)
			{
				if (ret[0] is PXResult)
				{
					return ((PXResult)ret[0])[0];
				}
				return ret[0];
			}

			return null;
		}

		public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (e.NewValue == null || !ValidateValue)
			{
				return;
			}

			if (sender.Keys.Count == 0 || _FieldName != sender.Keys[sender.Keys.Count - 1])
			{
				var view = GetViewWithParameters(sender, e.NewValue);
				object item = null;
				try
				{
					item = view.SelectSingleBound(e.Row);
				}
				catch (FormatException) { } // thrown by SqlServer
				catch (InvalidCastException) { } // thrown by MySql

				if (item == null)
				{
					throwNoItem(hasRestrictedAccess(sender, _PrimarySimpleSelect, e.Row), e.ExternalCall, e.NewValue);
				}
			}
		}

	}
}
