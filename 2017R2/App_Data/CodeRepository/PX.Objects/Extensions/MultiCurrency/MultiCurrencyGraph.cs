using System;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.CS;

namespace PX.Objects.Extensions.MultiCurrency
{
    /// <summary>The generic graph extension that defines the multi-currency functionality.</summary>
    /// <typeparam name="TGraph">A <see cref="PX.Data.PXGraph" /> type.</typeparam>
    /// <typeparam name="TPrimary">A DAC (a <see cref="PX.Data.IBqlTable" /> type).</typeparam>
    public abstract class MultiCurrencyGraph<TGraph, TPrimary> : PXGraphExtension<TGraph>
            where TGraph : PXGraph
            where TPrimary : class, IBqlTable, new()
    {
        /// <summary>A class that defines the default mapping of the <see cref="Document" /> class to a DAC.</summary>
        protected class DocumentMapping : IBqlMapping
        {
            /// <exclude />
            public Type Extension => typeof(Document);
            /// <exclude />
            protected Type _table;
            /// <exclude />
            public Type Table => _table;

            /// <summary>Creates the default mapping of the <see cref="Document" /> mapped cache extension to the specified table.</summary>
            /// <param name="table">A DAC.</param>
            public DocumentMapping(Type table)
            {
                _table = table;
            }
            /// <exclude />
            public Type BAccountID = typeof(Document.bAccountID);
            /// <exclude />
            public Type CuryInfoID = typeof(Document.curyInfoID);
            /// <exclude />
            public Type CuryID = typeof(Document.curyID);
            /// <exclude />
            public Type DocumentDate = typeof(Document.documentDate);
        }

        /// <summary>A class that defines the default mapping of the <see cref="CurySource" /> mapped cache extension to a DAC.</summary>
        protected class CurySourceMapping : IBqlMapping
        {
            /// <exclude />
            public Type Extension => typeof(CurySource);
            /// <exclude />
            protected Type _table;
            /// <exclude />
            public Type Table => _table;

            /// <summary>Creates the default mapping of the <see cref="CurySource" /> mapped cache extension to the specified table.</summary>
            /// <param name="table">A DAC.</param>
            public CurySourceMapping(Type table)
            {
                _table = table;
            }
            /// <exclude />
            public Type CuryID = typeof(CurySource.curyID);
            /// <exclude />
            public Type CuryRateTypeID = typeof(CurySource.curyRateTypeID);
            /// <exclude />
            public Type AllowOverrideCury = typeof(CurySource.allowOverrideCury);
            /// <exclude />
            public Type AllowOverrideRate = typeof(CurySource.allowOverrideRate);
        }

        /// <summary>Returns the mapping of the <see cref="Document" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
        /// <remarks>In the implementation graph for a particular graph, you  can either return the default mapping or override the default
        /// mapping in this method.</remarks>
        /// <example>
        ///   <code title="Example" description="The following code shows the method that overrides the GetDocumentMapping() method in the implementation class. The  method overrides the default mapping of the %Document% mapped cache extension to a DAC: For the CROpportunity DAC, the DocumentDate field of the mapped cache extension is mapped to the closeDate field of the DAC; other fields of the extension are mapped by default." lang="CS">
        /// protected override DocumentMapping GetDocumentMapping()
        ///  {
        ///          return new DocumentMapping(typeof(CROpportunity)) {DocumentDate =  typeof(CROpportunity.closeDate)};
        ///  }</code>
        /// </example>
        protected abstract DocumentMapping GetDocumentMapping();
        /// <summary>Returns the mapping of the <see cref="CurySource" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
        /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
        /// <example>
        ///   <code title="Example" description="The following code shows the method that overrides the GetCurySourceMapping() method in the implementation class. The method returns the defaul mapping of the %CurySource% mapped cache extension to the Customer DAC." lang="CS">
        /// protected override CurySourceMapping GetCurySourceMapping()
        ///  {
        ///      return new CurySourceMapping(typeof(Customer));
        ///  }</code>
        /// </example>
        protected abstract CurySourceMapping GetCurySourceMapping();

        /// <summary>A mapping-based view of the <see cref="Document" /> data.</summary>
        public PXSelectExtension<Document> Documents;
        /// <summary>A mapping-based view of the <see cref="CurySource" /> data.</summary>
        public PXSelectExtension<CurySource> CurySource;

        /// <summary>Returns the current currency source.</summary>
        /// <returns>The default implementation returns the <see cref="CurySource" /> data view.</returns>
        /// <example>
        ///   <code title="Example" description="The following code shows sample implementation of the method in the implementation class." lang="CS">
        /// public PXSelect&lt;CRSetup&gt; crCurrency;
        /// protected PXSelectExtension&lt;CurySource&gt; SourceSetup =&gt; new PXSelectExtension&lt;CurySource&gt;(crCurrency);
        ///  
        /// protected virtual CurySourceMapping GetSourceSetupMapping()
        /// { 
        ///       return new CurySourceMapping(typeof(CRSetup)) {CuryID = typeof(CRSetup.defaultCuryID), CuryRateTypeID = typeof(CRSetup.defaultRateTypeID)};                        
        ///  }
        ///  
        /// protected override CurySource CurrentSourceSelect()
        /// {
        ///        CurySource settings = base.CurrentSourceSelect();
        ///        if (settings == null)
        ///              return SourceSetup.Select();
        ///        if (settings.CuryID == null || settings.CuryRateTypeID == null)
        ///        {
        ///              CurySource setup = SourceSetup.Select();
        ///              settings = (CurySource)CurySource.Cache.CreateCopy(settings);
        ///              settings.CuryID = settings.CuryID ?? setup.CuryID;
        ///              settings.CuryRateTypeID = settings.CuryRateTypeID ?? setup.CuryRateTypeID;
        ///        }                                    
        ///        return settings;
        /// }</code>
        /// </example>
        protected virtual CurySource CurrentSourceSelect()
        {
            return CurySource.Select();
        }
        /// <summary>The current <see cref="CurrencyInfo" /> object of the document.</summary>
        public PXSelect<CurrencyInfo,
                    Where<CurrencyInfo.curyInfoID, Equal<Current<Document.curyInfoID>>>>
                    currencyinfo;

        /// <summary>The <strong>Currency Toggle</strong> action.</summary>
        public ToggleCurrency<TPrimary>
                    CurrencyView;

        /// <summary>The FieldUpdated2 event handler for the <see cref="Document.BAccountID" /> field. When the BAccountID field value is changed, <see cref="Document.CuryID" /> is assigned the default
        /// value.</summary>
        /// <param name="e">Parameters of the event.</param>
        protected virtual void _(Events.FieldUpdated<Document, Document.bAccountID> e)
        {
            if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
            {
                if (e.Row == null) return;

                if (e.ExternalCall || e.Row.CuryID == null)
                {
                    CurrencyInfo info = CurrencyInfoAttribute.SetDefaults<Document.curyInfoID>(e.Cache, e.Row, e.Cache.GetMain(e.Row).GetType());
                    string message = PXUIFieldAttribute.GetError<CurrencyInfo.curyEffDate>(e.Cache.Graph.Caches[typeof(CurrencyInfo)], info);
                    if (string.IsNullOrEmpty(message) == false)
                    {
                        Documents.Cache.RaiseExceptionHandling<Document.documentDate>(e.Row,
                            e.Row.DocumentDate,
                            new PXSetPropertyException(message, PXErrorLevel.Warning));
                    }

                    if (info != null)
                    {
                        Documents.Cache.SetValue<Document.curyID>(e.Row, info.CuryID);
                    }
                }
            }
        }
		/// <summary>The FieldDefaulting2 event handler for the <see cref="Document.DocumentDate" /> field. When the DocumentDate field value is changed, <see cref="CurrencyInfo.curyEffDate"/> is changed to DocumentDate value.</summary>
		/// <param name="e">Parameters of the event.</param>
		protected virtual void _(Events.FieldUpdated<Document, Document.documentDate> e)
        {
            if (e.Row == null) return;
            CurrencyInfoAttribute.SetEffectiveDate<Document.documentDate>(Documents.Cache,
                new PXFieldUpdatedEventArgs(e.Row, e.OldValue, e.ExternalCall));
        }

		/// <summary>The FieldDefaulting2 event handler for the <see cref="CurrencyInfo.CuryID" /> field. The CuryID field takes the current value of <see cref="CurySource.CuryID"/>.</summary>
		/// <param name="e">Parameters of the event.</param>
		protected virtual void _(Events.FieldDefaulting<CurrencyInfo, CurrencyInfo.curyID> e)
        {
            if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
            {
                CurySource source = CurrentSourceSelect();
                if (!string.IsNullOrEmpty(source?.CuryID))
                {
                    e.NewValue = source.CuryID;
                    e.Cancel = true;
                }
            }
        }
		/// <summary>The FieldDefaulting2 event handler for the <see cref="CurrencyInfo.CuryRateTypeID" /> field. The CuryRateTypeID field takes the current value of <see cref="CurySource.CuryRateTypeID"/>.</summary>
		/// <param name="e">Parameters of the event.</param>
		protected virtual void _(Events.FieldDefaulting<CurrencyInfo, CurrencyInfo.curyRateTypeID> e)
        {
            if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
            {
                CurySource source = CurrentSourceSelect();
                if (!string.IsNullOrEmpty(source?.CuryRateTypeID))
                {
                    e.NewValue = source.CuryRateTypeID;
                    e.Cancel = true;
                }
            }
        }
		/// <summary>The FieldDefaulting2 event handler for the <see cref="CurrencyInfo.CuryEffDate" /> field. The CuryEffDate field takes the current value of <see cref="Document.DocumentDate"/>.</summary>
		/// <param name="e">Parameters of the event.</param>
		protected virtual void _(Events.FieldDefaulting<CurrencyInfo, CurrencyInfo.curyEffDate> e)
        {
            if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
            {
                e.NewValue = Documents.Cache.Current != null && Documents.Current.DocumentDate != null
                    ? Documents.Current.DocumentDate
                    : e.Cache.Graph.Accessinfo.BusinessDate;
            }
        }

		/// <summary>The RowSelected event handler for the <see cref="CurrencyInfo" /> DAC. The handler sets the values of the Enabled property of the UI fields of <see cref="CurrencyInfo"/> according to the values of this property of the corresponding fields of <see cref="CurySource"/>.</summary>
		/// <param name="e">Parameters of the event.</param>
		protected virtual void _(Events.RowSelected<CurrencyInfo> e)
        {
            if (e.Row != null)
            {
                bool curyenabled = true;
                CurySource source = CurrentSourceSelect();

                if (source != null && source.AllowOverrideRate != true)
                {
                    curyenabled = false;
                }

                PXUIFieldAttribute.SetEnabled<CurrencyInfo.curyRateTypeID>(e.Cache, e.Row, curyenabled);
                PXUIFieldAttribute.SetEnabled<CurrencyInfo.curyEffDate>(e.Cache, e.Row, curyenabled);
                PXUIFieldAttribute.SetEnabled<CurrencyInfo.sampleCuryRate>(e.Cache, e.Row, curyenabled);
                PXUIFieldAttribute.SetEnabled<CurrencyInfo.sampleRecipRate>(e.Cache, e.Row, curyenabled);
            }
        }
        /// <summary>The RowSelected event handler for the <see cref="Document" /> DAC. The handler sets the value of the Enabled property of <see cref="Document.CuryID"/> according to the value of this property of <see cref="CurySource.AllowOverrideCury"/>.</summary>
        /// <param name="e">Parameters of the event.</param>
        protected virtual void _(Events.RowSelected<Document> e)
        {
            if (e.Row != null)
            {
                CurySource source = CurrentSourceSelect();
                PXUIFieldAttribute.SetEnabled<Document.curyID>(e.Cache, e.Row,
                    source == null || source.AllowOverrideCury == true);
            }
        }

    }
}
