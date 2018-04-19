using System;
using System.Collections.Generic;
using System.Text;
using PX.CCProcessingBase;
using PX.Data;

namespace PX.Objects.CA
{
	public static class ControlTypeDefintion
	{
		public const int Text = 1;
		public const int Combo = 2;
		public const int CheckBox = 3;
		public const int Password = 4;

		public class List : PXIntListAttribute
		{
			public List() : base(new int[] { Text, Combo, CheckBox }, new string[] { "Text", "Combo", "Checkbox" })
			{
			}
		}
	}

	[Serializable]
	[PXCacheName(Messages.CCProcessingCenterDetail)]
	public partial class CCProcessingCenterDetail : IBqlTable, ISettingsDetail
	{
		#region ProcessingCenterID
		public abstract class processingCenterID : IBqlField
		{
		}

		[PXDBString(10, IsUnicode = true, IsKey = true)]
		[PXDefault(typeof(CCProcessingCenter.processingCenterID))]
		[PXParent(typeof(Select<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Current<CCProcessingCenterDetail.processingCenterID>>>>))]
		public virtual string ProcessingCenterID
		{
			get;
			set;
		}
		#endregion
		#region DetailID
		public abstract class detailID : IBqlField
		{
		}

		[PXDBString(10, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "ID")]
		public virtual string DetailID
		{
			get;
			set;
		}
		#endregion
		#region Descr
		public abstract class descr : IBqlField
		{
		}

		[PXDBString(255, IsUnicode = true)]
		[PXDefault("")]
		[PXUIField(DisplayName = "Description")]
		public virtual string Descr
		{
			get;
			set;
		}
		#endregion
		#region IsEncryptionRequired
		public abstract class isEncryptionRequired : IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(false)]
		public virtual bool? IsEncryptionRequired
		{
			get;
			set;
		}
		#endregion
		#region IsEncrypted
		public abstract class isEncrypted : IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(false)]
		public virtual bool? IsEncrypted
		{
			get;
			set;
		}
		#endregion
		#region Value
		public abstract class value : IBqlField
		{
		}

		[PXRSACryptStringWithConditional(1024, typeof(CCProcessingCenterDetail.isEncryptionRequired), typeof(CCProcessingCenterDetail.isEncrypted))]
		[PXDBDefault]
		[PXUIField(DisplayName = "Value")]
		public virtual string Value
		{
			get;
			set;
		}
		#endregion
		#region ControlType
		public abstract class controlType : IBqlField
		{
		}

		[PXDBInt]
		[PXDefault(ControlTypeDefintion.Text)]
		[PXUIField(DisplayName = "Control Type", Visibility = PXUIVisibility.SelectorVisible)]
		[ControlTypeDefintion.List]
		public virtual int? ControlType
		{
			get;
			set;
		}
		#endregion
		#region ComboValues
		public abstract class comboValues : IBqlField
		{
		}

		[PXDBString(4000, IsUnicode = true)]
		public virtual string ComboValues
		{
			get;
			set;
		}
		#endregion


		#region CreatedByID
		public abstract class createdByID : IBqlField
		{
		}

		[PXDBCreatedByID]
		public virtual Guid? CreatedByID
		{
			get;
			set;
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : IBqlField
		{
		}

		[PXDBCreatedByScreenID]
		public virtual string CreatedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : IBqlField
		{
		}

		[PXDBCreatedDateTime]
		public virtual DateTime? CreatedDateTime
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : IBqlField
		{
		}

		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : IBqlField
		{
		}

		[PXDBLastModifiedByScreenID]
		public virtual string LastModifiedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : IBqlField
		{
		}

		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime
		{
			get;
			set;
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : IBqlField
		{
		}

		[PXDBTimestamp]
		public virtual byte[] tstamp
		{
			get;
			set;
		}
		#endregion

		#region IPCDetail Members

		public const int ValueFieldLength = 1024;

		public IList<KeyValuePair<string, string>> GetComboValues()
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

			string[] parts = ComboValues.Split(';');
			foreach (string part in parts)
			{
				if (!string.IsNullOrEmpty(part))
				{
					string[] keyval = part.Split('|');

					if (keyval.Length == 2)
					{
						list.Add(new KeyValuePair<string, string>(keyval[0], keyval[1]));
					}
				}
			}

			return list;
		}

		public virtual void SetComboValues(IList<KeyValuePair<string, string>> list)
		{
			if (list != null)
			{
				StringBuilder sb = new StringBuilder();
				foreach (KeyValuePair<string, string> kv in list)
				{
					sb.AppendFormat("{0}|{1};", kv.Key, kv.Value);
				}

				ComboValues = sb.ToString();
			}
			else
			{
				ComboValues = null;
			}
		}

		#endregion
		public virtual void Copy(ISettingsDetail aFrom)
		{
			this.DetailID = aFrom.DetailID;
			this.Value = aFrom.Value;
			this.Descr = aFrom.Descr;
			this.ControlType = aFrom.ControlType;
			this.IsEncryptionRequired = aFrom.IsEncryptionRequired;
			this.SetComboValues(aFrom.GetComboValues());
		}
	}

}
