using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR.MassProcess;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.TX;
using PX.Objects;
using PX.SM;
using PX.TM;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace PX.Objects.CR
{
    public class BAccountExt : PXCacheExtension<PX.Objects.CR.BAccount>
    {
        #region UsrGPCustomerID
        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Customer Ref")]
        public virtual string UsrGPCustomerID { get; set; }
        public abstract class usrGPCustomerID : IBqlField { }
        #endregion

        #region UsrCollectorID
        [PXDBInt]
        [PXUIField(DisplayName = "Collector", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(Search<PX.Objects.EP.EPEmployee.bAccountID>),
               new Type[]{
                typeof(PX.Objects.EP.EPEmployee.acctCD),
                typeof(PX.Objects.EP.EPEmployee.acctName)},
               SubstituteKey = typeof(PX.Objects.EP.EPEmployee.acctCD),
               DescriptionField = typeof(PX.Objects.EP.EPEmployee.acctName))]
        public virtual int? UsrCollectorID { get; set; }
        public abstract class usrCollectorID : IBqlField { }
        #endregion
    }
}