using PX.Data;

namespace PX.Objects.CR
{
	public class CRActivityClass : PXIntListAttribute
	{
		//NOTE: don't use 5 and 3. These numbers were used in old version. Or look at sql update script carefully.
		public const int Task = 0;
		public const int Event = 1;
		public const int Activity = 2;
		public const int Email = 4;
		public const int EmailRouting = -2;
		public const int OldEmails = -3;

		public CRActivityClass()
			: base(
				new[]
				{
					Task,
					Event,
					Activity,
					Email,
					EmailRouting,
					OldEmails
				},
				new[]
				{
					Data.EP.Messages.TaskClassInfo,
					Data.EP.Messages.EventClassInfo,
					Messages.ActivityClassInfo,
					Messages.EmailClassInfo,
					Messages.EmailResponse,
					Messages.EmailClassInfo
				})
		{
		}

		public class task : Constant<int>
		{
			public task() : base(Task) { }
		}

		public class events : Constant<int>
		{
			public events() : base(Event) { }
		}

		public class activity : Constant<int>
		{
			public activity() : base(Activity) { }
		}

		public class email : Constant<int>
		{
			public email() : base(Email) { }
		}

		public class emailRouting : Constant<int>
		{
			public emailRouting() : base(EmailRouting) { }
		}

		public class oldEmails : Constant<int>
		{
			public oldEmails() : base(OldEmails) { }
		}
	}

	public class PMActivityClass : CRActivityClass
	{
		public const int TimeActivity = 8;

		public PMActivityClass()
		{
			var values = new int[_AllowedValues.Length + 1];
			_AllowedValues.CopyTo(values, 0);
			values[_AllowedValues.Length] = TimeActivity;
			_AllowedValues = values;
			
			var labels = new string[_AllowedLabels.Length + 1];
			_AllowedLabels.CopyTo(labels, 0);
			labels[_AllowedLabels.Length] = Messages.TimeActivity;
			_AllowedLabels = labels;
		}

		public class UI
		{
			public class timeActivity : Constant<string>
			{
				public timeActivity() : base(Messages.TimeActivity) { }
			}
		}

		public class timeActivity : Constant<int>
		{
			public timeActivity() : base(TimeActivity) { }
		}
	}
}
