using PX.SM;
using System;
using PX.Data;
using PX.Objects.GL;

namespace PX.Objects.CR
{
    [Serializable]
    public class CROpportunityClassProbability : IBqlTable
    {
        #region ClassID
        public abstract class classID : IBqlField { }

        [PXDBString(10, IsKey = true, IsUnicode = true)]
        [PXDBDefault(typeof(CROpportunityClass.cROpportunityClassID))]
        [PXParent(typeof(Select<CROpportunityClass,
            Where<CROpportunityClass.cROpportunityClassID, Equal<Current<classID>>>>))]
        public virtual string ClassID { get; set; }
        #endregion

        #region StageID

        public abstract class stageID : IBqlField { }

        [PXDBString(2, IsKey = true)]
        [PXDBDefault(typeof(CROpportunityProbability.stageCode))]
        public virtual string StageID { get; set; }

        #endregion

        #region tstamp

        public abstract class Tstamp : IBqlField { }

        [PXDBTimestamp]
        public virtual Byte[] tstamp { get; set; }

        #endregion

        #region CreatedByScreenID

        public abstract class createdByScreenID : IBqlField { }

        [PXDBCreatedByScreenID]
        public virtual String CreatedByScreenID { get; set; }

        #endregion

        #region CreatedByID

        public abstract class createdByID : IBqlField { }

        [PXDBCreatedByID]
        public virtual Guid? CreatedByID { get; set; }

        #endregion

        #region CreatedDateTime

        public abstract class createdDateTime : PX.Data.IBqlField { }

        [PXDBCreatedDateTimeUtc]
        public virtual DateTime? CreatedDateTime { get; set; }

        #endregion

        #region LastModifiedByID

        public abstract class lastModifiedByID : PX.Data.IBqlField { }

        [PXDBLastModifiedByID]
        public virtual Guid? LastModifiedByID { get; set; }

        #endregion

        #region LastModifiedByScreenID

        public abstract class lastModifiedByScreenID : IBqlField { }

        [PXDBLastModifiedByScreenID]
        public virtual String LastModifiedByScreenID { get; set; }

        #endregion

        #region LastModifiedDateTime

        public abstract class lastModifiedDateTime : IBqlField { }

        [PXDBLastModifiedDateTimeUtc]
        public virtual DateTime? LastModifiedDateTime { get; set; }

        #endregion
    }
}