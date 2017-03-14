namespace Disqus.Core.Services.Api
{
	public struct DsqLimit
	{
		const int _max = 100;
		const int _min = 1;

		public DsqLimit(int limit = 25)
		{
			if (limit > _max)
				Value = _max;
			else if (limit < _min)
				Value = _min;
			else
				Value = limit;
		}

		public int Value;

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
