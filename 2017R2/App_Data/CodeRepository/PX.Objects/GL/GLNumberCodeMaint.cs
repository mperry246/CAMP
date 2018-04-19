using System;
using PX.Data;

namespace PX.Objects.GL
{
	public class GLNumberCodeMaint : PXGraph<GLNumberCodeMaint, GLNumberCode>
	{        
		[PXImport(typeof(GLNumberCode))]
		public PXSelect<GLNumberCode> NumberCodes;
	}
}
