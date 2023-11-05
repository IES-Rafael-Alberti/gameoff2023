namespace Utilities.Enum
{
    public enum RotationEnum
    {
        None,   // 0 degrees
        CW90,   // 90 degrees clockwise
        CW180,  // 180 degrees clockwise
        CW270   // 270 degrees clockwise
    }

    public static class RotateEnum
    {
        public static (float, RotationEnum) RotateClockwise(RotationEnum current_rotation)
        {
            switch (current_rotation)
            {
                case RotationEnum.None:
                    return (90, RotationEnum.CW90);
                case RotationEnum.CW90:
                    return (180, RotationEnum.CW180);
                case RotationEnum.CW180:
                    return (270, RotationEnum.CW270);
                case RotationEnum.CW270:
                    return (0, RotationEnum.None);
            }
            return (0, RotationEnum.None);
        }
        public static (float, RotationEnum) RotateCounterclockwise(RotationEnum current_rotation)
        {
            switch (current_rotation)
            {
                case RotationEnum.None:
                    return (270, RotationEnum.CW270);
                case RotationEnum.CW90:
                    return (0, RotationEnum.None);
                case RotationEnum.CW180:
                    return (90, RotationEnum.CW90);
                case RotationEnum.CW270:
                    return (180, RotationEnum.CW180);
            }
            return (0, RotationEnum.None);

        }
    }
}
