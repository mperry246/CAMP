using System;
using PX.Data;
using PX.Objects.CR;

namespace PX.Objects.AR
{
   	[PXPrimaryGraph(typeof(CustomerLocationMaint))]
	[PXSubstitute(GraphType = typeof(CustomerLocationMaint))]
    [Serializable]
	public partial class SelectedCustomerLocation : SelectedLocation
	{
        #region BAccountID
        public new abstract class bAccountID : PX.Data.IBqlField
        {
        }
        [PXDefault(typeof(Location.bAccountID))]
        [Customer(typeof(Search<Customer.bAccountID,
            Where<Where<Customer.type, Equal<BAccountType.customerType>,
                    Or<Customer.type, Equal<BAccountType.prospectType>,
                     Or<Customer.type, Equal<BAccountType.combinedType>>>>>>), IsKey = true, TabOrder = 0)]
        [PXParent(typeof(Select<BAccount,
            Where<BAccount.bAccountID,
            Equal<Current<Location.bAccountID>>>>)
            )]
        public override Int32? BAccountID
        {
            get
            {
                return base._BAccountID;
            }
            set
            {
                base._BAccountID = value;
            }
        }
        #endregion
      
	}

    public class CustomerLocationMaint : LocationMaint
    {
		protected override void Location_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
		{
			base.Location_RowPersisted(sender, e);

			if (e.TranStatus == PXTranStatus.Completed)
				DiscountEngine.RemoveFromCachedCustomerPriceClasses(((Location)e.Row).BAccountID);
		}
	}

}
