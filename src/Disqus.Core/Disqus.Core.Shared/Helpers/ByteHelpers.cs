namespace Disqus.Core.Helpers
{
	internal static class ByteHelpers
	{
		internal static string ToHexString(this byte[] buff)
		{
			string sbinary = "";

			for (int i = 0; i < buff.Length; i++)
			{
				sbinary += buff[i].ToString("X2"); // hex format
			}

			return (sbinary);
		}

		internal static byte[] ToAsciiBytes(this string str)
		{
			byte[] retval = new byte[str.Length];

			for (int ix = 0; ix < str.Length; ++ix)
			{
				char ch = str[ix];
				if (ch <= 0x7f) retval[ix] = (byte)ch;
				else retval[ix] = (byte)'?';
			}

			return retval;
		}
	}
}
