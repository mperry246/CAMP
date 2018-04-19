using System;

using PX.Data;

namespace PX.Objects.CR.Standalone
{
	[Serializable]
	[PXHidden]
	public class LocationAlias : CR.Location
	{
		public new abstract class bAccountID : IBqlField { }
		public new abstract class locationID : IBqlField { }
		public new abstract class locationCD : IBqlField { }
		public new abstract class locType : IBqlField { }
		public new abstract class descr : IBqlField { }
		public new abstract class taxRegistrationID : IBqlField { }
		public new abstract class defAddressID : IBqlField { }
		public new abstract class defContactID : IBqlField { }
		public new abstract class noteID : IBqlField { }
		public new abstract class isActive : IBqlField { }
		public new abstract class isDefault : IBqlField { }
		public new abstract class isAPAccountSameAsMain : IBqlField { }
		public new abstract class isAPPaymentInfoSameAsMain : IBqlField { }
		public new abstract class isARAccountSameAsMain : IBqlField { }
		public new abstract class isRemitAddressSameAsMain : IBqlField { }
		public new abstract class isRemitContactSameAsMain : IBqlField { }
		public new abstract class cTaxZoneID : IBqlField { }
		public new abstract class cAvalaraExemptionNumber : IBqlField { }
		public new abstract class cAvalaraCustomerUsageType : IBqlField { }
		public new abstract class cCarrierID : IBqlField { }
		public new abstract class cShipTermsID : IBqlField { }
		public new abstract class cShipZoneID : IBqlField { }
		public new abstract class cFOBPointID : IBqlField { }
		public new abstract class cResedential : IBqlField { }
		public new abstract class cSaturdayDelivery : IBqlField { }
		public new abstract class cGroundCollect : IBqlField { }
		public new abstract class cInsurance : IBqlField { }
		public new abstract class cLeadTime : IBqlField { }
		public new abstract class cBranchID : IBqlField { }
		public new abstract class cSalesAcctID : IBqlField { }
		public new abstract class cSalesSubID : IBqlField { }
		public new abstract class cPriceClassID : IBqlField { }
		public new abstract class cSiteID : IBqlField { }
		public new abstract class cDiscountAcctID : IBqlField { }
		public new abstract class cDiscountSubID : IBqlField { }
		public new abstract class cFreightAcctID : IBqlField { }
		public new abstract class cFreightSubID : IBqlField { }
		public new abstract class cShipComplete : IBqlField { }
		public new abstract class cOrderPriority : IBqlField { }
		public new abstract class cCalendarID : IBqlField { }
		public new abstract class cDefProjectID : IBqlField { }
		public new abstract class cARAccountLocationID : IBqlField { }
		public new abstract class cARAccountID : IBqlField { }
		public new abstract class cARSubID : IBqlField { }
		public new abstract class vTaxZoneID : IBqlField { }
		public new abstract class vTaxCalcMode : IBqlField { }
		public new abstract class vCarrierID : IBqlField { }
		public new abstract class vShipTermsID : IBqlField { }
		public new abstract class vFOBPointID : IBqlField { }
		public new abstract class vLeadTime : IBqlField { }
		public new abstract class vBranchID : IBqlField { }
		public new abstract class vExpenseAcctID : IBqlField { }
		public new abstract class vExpenseSubID : IBqlField { }
		public new abstract class vFreightAcctID : IBqlField { }
		public new abstract class vFreightSubID : IBqlField { }
		public new abstract class vDiscountAcctID : IBqlField { }
		public new abstract class vDiscountSubID : IBqlField { }
		public new abstract class vRcptQtyMin : IBqlField { }
		public new abstract class vRcptQtyMax : IBqlField { }
		public new abstract class vRcptQtyThreshold : IBqlField { }
		public new abstract class vRcptQtyAction : IBqlField { }
		public new abstract class vSiteID : IBqlField { }
		public new abstract class vPrintOrder : IBqlField { }
		public new abstract class vEmailOrder : IBqlField { }
		public new abstract class vDefProjectID : IBqlField { }
		public new abstract class vAPAccountLocationID : IBqlField { }
		public new abstract class vAPAccountID : IBqlField { }
		public new abstract class vAPSubID : IBqlField { }
		public new abstract class vPaymentInfoLocationID : IBqlField { }
		public new abstract class vRemitAddressID : IBqlField { }
		public new abstract class vRemitContactID : IBqlField { }
		public new abstract class vPaymentMethodID : IBqlField { }
		public new abstract class vCashAccountID : IBqlField { }
		public new abstract class vPaymentLeadTime : IBqlField { }
		public new abstract class vPaymentByType : IBqlField { }
		public new abstract class vSeparateCheck : IBqlField { }
		public new abstract class locationAPAccountSubBAccountID : IBqlField { }
		public new abstract class aPAccountID : IBqlField { }
		public new abstract class aPSubID : IBqlField { }
		public new abstract class locationARAccountSubBAccountID : IBqlField { }
		public new abstract class aRAccountID : IBqlField { }
		public new abstract class aRSubID : IBqlField { }
		public new abstract class locationAPPaymentInfoBAccountID : IBqlField { }
		public new abstract class remitAddressID : IBqlField { }
		public new abstract class remitContactID : IBqlField { }
		public new abstract class paymentMethodID : IBqlField { }
		public new abstract class cashAccountID : IBqlField { }
		public new abstract class paymentLeadTime : IBqlField { }
		public new abstract class separateCheck : IBqlField { }
		public new abstract class paymentByType : IBqlField { }
		public new abstract class bAccountBAccountID : IBqlField { }
		public new abstract class vDefAddressID : IBqlField { }
		public new abstract class vDefContactID : IBqlField { }
		public new abstract class cMPSalesSubID : IBqlField { }
		public new abstract class cMPExpenseSubID : IBqlField { }
		public new abstract class cMPFreightSubID : IBqlField { }
		public new abstract class cMPDiscountSubID : IBqlField { }
		public new abstract class cMPGainLossSubID : IBqlField { }
		public new abstract class cMPSiteID : IBqlField { }
		public new abstract class Tstamp : IBqlField { }
		public new abstract class createdByID : IBqlField { }
		public new abstract class createdByScreenID : IBqlField { }
		public new abstract class createdDateTime : IBqlField { }
		public new abstract class lastModifiedByID : IBqlField { }
		public new abstract class lastModifiedByScreenID : IBqlField { }
		public new abstract class lastModifiedDateTime : IBqlField { }
		public new abstract class vSiteIDIsNull : IBqlField { }
		public new abstract class isAddressSameAsMain : IBqlField { }
		public new abstract class isContactSameAsMain : IBqlField { }
	}
}
