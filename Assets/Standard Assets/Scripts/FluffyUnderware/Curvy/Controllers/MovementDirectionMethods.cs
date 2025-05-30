// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Controllers.MovementDirectionMethods
using System;

namespace FluffyUnderware.Curvy.Controllers
{
	public static class MovementDirectionMethods
	{
		public static MovementDirection FromInt(int value)
		{
			return (value < 0) ? MovementDirection.Backward : MovementDirection.Forward;
		}

		public static MovementDirection GetOpposite(this MovementDirection value)
		{
			MovementDirection result;
			if (value != MovementDirection.Forward)
			{
				if (value != MovementDirection.Backward)
				{
					throw new ArgumentOutOfRangeException();
				}
				result = MovementDirection.Forward;
			}
			else
			{
				result = MovementDirection.Backward;
			}
			return result;
		}

		public static int ToInt(this MovementDirection direction)
		{
			int result;
			if (direction != MovementDirection.Forward)
			{
				if (direction != MovementDirection.Backward)
				{
					throw new ArgumentOutOfRangeException();
				}
				result = -1;
			}
			else
			{
				result = 1;
			}
			return result;
		}
	}
}
