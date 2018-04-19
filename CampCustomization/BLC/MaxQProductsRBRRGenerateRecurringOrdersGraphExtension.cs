using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.SM;
using PX.Data;
using PX.Objects.AR;
using CAMPCustomization;

namespace MaxQ.Products.RBRR
{
    public class MaxQProductsRBRRGenerateRecurringOrdersGraphExtension : PXGraphExtension<GenerateRecurringOrders>
    {
        public override void Initialize()
        {
            var myDate = (DateTime)Base.Accessinfo.BusinessDate;
            bool oktoprocess = false;
            Base.FilteredProcessing.SetParametersDelegate(delegate (List<XRBOrdersToGen> documents)
            {
                string smessage = String.Format("Warning you are about to create multiple invoices. Do you want to proceed? The Invoice date is {0}/{1}/{2}", myDate.Month, myDate.Day, myDate.Year);

                return Base.FilteredProcessing.Ask("Generate Recurring Orders", smessage, MessageButtons.YesNo) == WebDialogResult.Yes;
            });

            Base.FilteredProcessing.SetProcessDelegate(delegate (XRBOrdersToGen doc)
            {
                if (Base.Filter != null)
                {
                    int daysDiff = ((TimeSpan)(Base.Filter.Current.BegDate - Base.Filter.Current.EndDate)).Days;
                    int begdaysDiff = ((TimeSpan)(Base.Filter.Current.BegDate - myDate)).Days;
                    int enddaysDiff = ((TimeSpan)(Base.Filter.Current.EndDate - myDate)).Days;

                    if (begdaysDiff < 240 && enddaysDiff < 240)
                    {
                        oktoprocess = true;
                    }
                }
                if (oktoprocess)
                {
                    PXGraph.InstanceCreated.AddHandler<ContractMaint>((graph) =>
                    {
                        graph.RowPersisting.AddHandler<MaxQ.Products.RBRR.XRBContrHdr>((sender, e) =>
                        {
                            XRBContrHdr contract = PXResult<XRBContrHdr>.Current;
                            XRBContrDet contractdet = PXResult<XRBContrDet>.Current;
                            XRBContrHdrExt contractEX = PXCache<XRBContrHdr>.GetExtension<XRBContrHdrExt>(contract);
                            CAMPContractType contracttype = (CAMPContractType)PXSelect<CAMPContractType,
                               Where<CAMPContractType.contractTypeID,
                                Equal<Required<XRBContrHdrExt.usrContractTypeID>>>>.Select(Base, contractEX.UsrContractTypeID);
                            if ((bool)contractdet.ManuallyBooked && contracttype != null && (bool)contracttype.UpdateContractType)
                            {
                                contractEX.UsrContractTypeID = contracttype.NextContractTypeID;
                            }
                        });
                    });

                    PXGraph.InstanceCreated.AddHandler<PX.Objects.AR.ARInvoiceEntry>((graph) =>
                    {
                        graph.RowInserting.AddHandler<PX.Objects.AR.ARInvoice>((sender, e) =>
                        {
                            XRBContrHdr contract = PXResult<XRBContrHdr>.Current;
                            XRBContrDet contractdet = PXResult<XRBContrDet>.Current;
                            XRBContrHdrExt contractEX = PXCache<XRBContrHdr>.GetExtension<XRBContrHdrExt>(contract);
                            PX.Objects.AR.ARInvoice invoice = (PX.Objects.AR.ARInvoice)e.Row;
                            ARRegisterExt ext = PXCache<PX.Objects.AR.ARRegister>.GetExtension<ARRegisterExt>(invoice);
                            CAMPLineofBusiness lob = (CAMPLineofBusiness)PXSelect<CAMPLineofBusiness,
                                Where<CAMPLineofBusiness.lineofBusinessID,
                                    Equal<Required<XRBContrHdrExt.usrLineofBusiness>>>>.Select(Base, contractEX.UsrLineofBusiness);
                            if (contract != null)
                            {
                                ext.UsrContractID = contract.ContractID;
                                ext.UsrContractCD = contract.ContractCD;
                                ext.UsrRevisionNbr = contract.RevisionNbr;
                                ext.UsrTotPeriods = contract.TotPeriods;
                                ext.UsrLineofBusiness = contractEX.UsrLineofBusiness;
                                ext.UsrContractTypeID = contractEX.UsrContractTypeID;
                                invoice.DocDesc = (lob != null) ? lob.LineofBusinessCD : "";
                            }
                            if (contractdet != null)
                            {
                                ext.UsrGenerateDate = contractdet.GenDate;
                                switch (contract.TotPeriods)
                                {
                                    case ContractMaintContractInterval_List.Every18Months_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(18);
                                        break;
                                    case ContractMaintContractInterval_List.FiveYears_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(60);
                                        break;
                                    case ContractMaintContractInterval_List.SixYears_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(72);
                                        break;
                                    case ContractMaintContractInterval_List.SevenYears_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(84);
                                        break;
                                    case ContractMaintContractInterval_List.ThreeYears_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(36);
                                        break;
                                    case ContractMaintContractInterval_List.Biennially_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(24);
                                        break;
                                    case ContractMaintContractInterval_List.Years_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(12);
                                        break;
                                    case ContractMaintContractInterval_List.SemiAnnually_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(6);
                                        break;
                                    case ContractMaintContractInterval_List.Quarters_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(3);
                                        break;
                                    case ContractMaintContractInterval_List.BiMonthly_key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(2);
                                        break;
                                    case ContractMaintContractInterval_List.Months_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(1);
                                        break;
                                    case ContractMaintContractInterval_List.UserDefined_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(60);
                                        break;
                                    case ContractMaintContractInterval_List.FourMonths_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddMonths(4);
                                        break;
                                    case ContractMaintContractInterval_List.BiWeekly_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddDays(14);
                                        break;
                                    case ContractMaintContractInterval_List.Days_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddDays(1);
                                        break;
                                    case ContractMaintContractInterval_List.Weeks_Key:
                                        ext.UsrContractEndDate = ((DateTime)contractdet.GenDate).AddDays(7);
                                        break;
                                }
                                ext.UsrContractEndDate = ((DateTime)ext.UsrContractEndDate).AddDays(-1);
                            }
                        });
                        graph.RowInserting.AddHandler<ARTran>((sender, e) =>
                        {
                            XRBGLDist contractdist = PXResult<XRBGLDist>.Current;
                            XRBGLDistExt contractdistEX = PXCache<XRBGLDist>.GetExtension<XRBGLDistExt>(contractdist);

                            if (contractdist != null)
                            {
                                ARTranExt ext = PXCache<ARTran>.GetExtension<ARTranExt>((ARTran)e.Row);
                                ext.UsrProfileNumber = contractdistEX.UsrProfileNumber;
                                ext.UsrDiscountReasonID = contractdistEX.UsrDiscountReasonID;
                                ext.UsrRegNbr = contractdistEX.UsrRegNbr;
                            }
                        });
                    });
                    GenerateRecurringOrders.ProcessOrdersAndInvoices(new List<XRBOrdersToGen>() { doc });
                }
                else
                {
                    throw new PXException("Invalid Date Range. Date range must be within 120 Days of business date.");
                }
            });

            base.Initialize();
        }
    }
}
