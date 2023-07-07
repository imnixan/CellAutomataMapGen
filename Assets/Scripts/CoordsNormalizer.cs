public static class CoordsNormalizer
{
    public static int Normalize(int coordinate, int coordinateMax)
    {
        if (coordinate == coordinateMax)
        {
            coordinate = 0;
        }
        if (coordinate < 0)
        {
            coordinate = coordinateMax - 1;
        }
        return coordinate;
    }
}
