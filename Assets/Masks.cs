public static class Masks
{
    private static int[] LandMaskNeighborsVariants = new int[] { 3, 6, 7, 8 };

    public static int GetLandsMask()
    {
        int variantsMask = 0;

        foreach (int variant in LandMaskNeighborsVariants)
        {
            if (variant >= 0 && variant < 32)
            {
                variantsMask |= (1 << variant);
            }
        }

        return ~variantsMask;
    }
}
