public static class Masks
{
    private static int _landBirthMask;
    private static int _landSurviveMask;
    private static int _gunPlaceMask;
    private static int _movablePlaceMask;
    public static int LandBirthMask
    {
        get { return _landBirthMask; }
        private set { _landBirthMask = value; }
    }
    public static int LandSurviveMask
    {
        get { return _landSurviveMask; }
        private set { _landSurviveMask = value; }
    }
    public static int GunPlaceMask
    {
        get { return _gunPlaceMask; }
        private set { _gunPlaceMask = value; }
    }
    public static int MovablePlaceMask
    {
        get { return _movablePlaceMask; }
        private set { _movablePlaceMask = value; }
    }

    public static void InitializeMasks()
    {
        LandBirthMask = CreateAndGetMask(landBirthMaskRules);
        LandSurviveMask = CreateAndGetMask(landSurviveMaskRules);
        GunPlaceMask = CreateAndGetMask(gunPlaceMaskRules);
        MovablePlaceMask = CreateAndGetMask(movableMaskRules);
    }

    private static int[] landBirthMaskRules = new int[] { 4, 5, 6, 7, 8 };

    private static int[] landSurviveMaskRules = new int[] { 5, 6, 7, 8 };

    private static int[] gunPlaceMaskRules = new int[] { 4, 5, 7 };
    private static int[] movableMaskRules = new int[]{0,1};

    private static int CreateAndGetMask(int[] maskRules)
    {
        int mask = 0;

        foreach (int variant in maskRules)
        {
            if (variant >= 0 && variant < 32)
            {
                mask |= (1 << variant);
            }
        }

        return mask;
    }

    public static bool CheckMask(int result, int mask)
    {
        return (mask & (1 << result)) != 0;
    }
}
